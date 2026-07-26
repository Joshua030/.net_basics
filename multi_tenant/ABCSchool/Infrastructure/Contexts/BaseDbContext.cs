using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Finbuckle.MultiTenant.Identity.EntityFrameworkCore;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Contexts
{
    public abstract class BaseDbContext : MultiTenantIdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        ApplicationRoleClaim,
        IdentityUserToken<string>,
        IdentityUserPasskey<string>
        >
    {

        private new ABCSchoolTenantInfo TenantInfo { get; set; }
        protected BaseDbContext(
     IMultiTenantContextAccessor<ABCSchoolTenantInfo> multiTenantContextAccessor,
     DbContextOptions options)
     : base(multiTenantContextAccessor, options)
        {
            TenantInfo = multiTenantContextAccessor?.MultiTenantContext?.TenantInfo;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!string.IsNullOrEmpty(TenantInfo?.ConnectionString))
            {
                optionsBuilder.UseSqlServer(TenantInfo.ConnectionString, options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
