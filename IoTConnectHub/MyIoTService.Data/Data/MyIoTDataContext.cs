using Microsoft.EntityFrameworkCore;
using MyIoTService.Data.Entities;
using MyIoTService.Services;

namespace MyIoTService.Data.Data
{
    public class MyIoTDataContext : DbContext
    {
        private readonly IUserContextService _userContextService;
        public MyIoTDataContext(DbContextOptions<MyIoTDataContext> options, IUserContextService userContextService) : base(options)
        {
            _userContextService = userContextService;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceData> DeviceData { get; set; }

        public override int SaveChanges()
        {
            UpdateBaseEntityProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateBaseEntityProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateBaseEntityProperties()
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var entities = ChangeTracker.Entries()
                    .Where(e => e.Entity is BaseEntity &&
                                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added && currentUserId.HasValue)
                {
                    entity.CreatedBy = currentUserId.Value;
                }

                if (currentUserId != null)
                {
                    entity.ModifiedBy = currentUserId.Value;
                }
            }
        }
    }
}
