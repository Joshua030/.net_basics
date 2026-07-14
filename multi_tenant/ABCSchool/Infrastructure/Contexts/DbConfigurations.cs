using Finbuckle.MultiTenant.EntityFrameworkCore.Extensions;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts
{
    internal class DbConfigurations
    {

        internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                builder.
                  ToTable("Users", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
        {
            public void Configure(EntityTypeBuilder<ApplicationRole> builder)
            {
                builder.
                  ToTable("Roles", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class ApplicationRoleClaimConfiguration : IEntityTypeConfiguration<ApplicationRoleClaim>
        {
            public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
            {
                builder.
                  ToTable("RoleClaims", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
            {
                builder.
                  ToTable("UserClaims", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
            {
                builder.
                  ToTable("UserLogins", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
            {
                builder.
                  ToTable("UserRoles", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
            {
                builder.
                  ToTable("UserTokens", "Identity")
                  .IsMultiTenant();
            }
        }

        internal class IdentityUserPasskeyConfiguration : IEntityTypeConfiguration<IdentityUserPasskey<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserPasskey<string>> builder)
            {
                builder.
                  ToTable("UserPasskeys", "Identity")
                  .IsMultiTenant();
            }
        }

    }
}
