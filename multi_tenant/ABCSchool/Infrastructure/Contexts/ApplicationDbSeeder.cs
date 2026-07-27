using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Constants;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ABCSharedLibrary.Constants;

namespace Infrastructure.Contexts
{
    public class ApplicationDbSeeder(
IMultiTenantContextAccessor<ABCSchoolTenantInfo> tenantInfoContextAccessor,
RoleManager<ApplicationRole> roleManager,
UserManager<ApplicationUser> userManager,
ApplicationDbContext applicationDbContext
)
    {
        private readonly IMultiTenantContextAccessor<ABCSchoolTenantInfo> _tenantInfoContextAccessor = tenantInfoContextAccessor;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;


        public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
        {
            if (_applicationDbContext.Database.GetMigrations().Any())
            {
                // Perform database initialization logic here
                if ((await _applicationDbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    await _applicationDbContext.Database.MigrateAsync(cancellationToken);
                }
                if (await _applicationDbContext.Database.CanConnectAsync(cancellationToken))
                {
                    //seeding
                    //Default Roles > Assign permissions/claims
                    await InitializeDefaultRolesAsync(cancellationToken);
                    // User > Assign Roles
                    await InitializeAdminUserasync();
                }
            }
        }

        private async Task InitializeDefaultRolesAsync(CancellationToken ct)
        {
            foreach (var roleName in RoleConstants.DefaultRoles)
            {

                if (await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName, ct) is not ApplicationRole incomingRole)
                {
                    incomingRole = new ApplicationRole
                    {
                        Name = roleName,
                        Description = $"{roleName} Role"
                    };

                    await _roleManager.CreateAsync(incomingRole);
                }

                // Assing Permissions

                if (roleName == RoleConstants.Basic)
                {
                    // Assign basic permissions
                    await AssignPermissionsToRole(SchoolPermissions.Basic, incomingRole, ct);
                }
                else if (roleName == RoleConstants.Admin)
                {
                    // Assign admin permissions
                    await AssignPermissionsToRole(SchoolPermissions.Admin, incomingRole, ct);

                    if (_tenantInfoContextAccessor.MultiTenantContext?.TenantInfo.Id == TenancyConstants.Root.Id)
                    {
                        // Assign tenant-specific permissions
                        await AssignPermissionsToRole(SchoolPermissions.Root, incomingRole, ct);
                    }
                }
            }
        }


        private async Task AssignPermissionsToRole(
            IReadOnlyList<SchoolPermission> rolePermissions,
            ApplicationRole role,
            CancellationToken ct
            )
        {
            var currentClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var rolePermisson in rolePermissions)
            {
                if (!currentClaims.Any(c => c.Type == ClaimConstants.Permission && c.Value == rolePermisson.Name))
                {
                    await _applicationDbContext.RoleClaims.AddAsync(new ApplicationRoleClaim
                    {
                        RoleId = role.Id,
                        ClaimType = ClaimConstants.Permission,
                        ClaimValue = rolePermisson.Name,
                        Description = rolePermisson.Description,
                        Group = rolePermisson.Group
                    }, ct);

                    await _applicationDbContext.SaveChangesAsync(ct);
                }
            }

        }


        private async Task InitializeAdminUserasync()
        {
            if (string.IsNullOrEmpty(_tenantInfoContextAccessor.MultiTenantContext?.TenantInfo.Email)) return;

            //TODO - CHANGE FOR FindByEmailAsync
            if (await _userManager.Users
                .SingleOrDefaultAsync(u => u.Email == _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Email)
                is not ApplicationUser incomingUser)
            {
                incomingUser = new ApplicationUser
                {
                    FirstName = _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.FirstName,
                    LastName = _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.LastName,
                    Email = _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Email,
                    UserName = _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    NormalizedEmail = _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Email.ToUpperInvariant(),
                    NormalizedUserName = _tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Email.ToUpperInvariant(),
                    IsActive = true
                };

                var passwordHash = new PasswordHasher<ApplicationUser>();

                incomingUser.PasswordHash = passwordHash.HashPassword(incomingUser, TenancyConstants.DefaultPassword);
                await _userManager.CreateAsync(incomingUser);
            }

            if (!await _userManager.IsInRoleAsync(incomingUser, RoleConstants.Admin))
            {
                await _userManager.AddToRoleAsync(incomingUser, RoleConstants.Admin);
            }

        }


    }

}
