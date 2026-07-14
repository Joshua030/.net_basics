using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : BaseDbContext
    {

        public DbSet<School> Schools => Set<School>();
        public ApplicationDbContext(IMultiTenantContextAccessor<ABCSchoolTenantInfo> multiTenantContextAccessor,
            DbContextOptions<ApplicationDbContext> options)
            : base(multiTenantContextAccessor, options)
        {
        }
    }
}
