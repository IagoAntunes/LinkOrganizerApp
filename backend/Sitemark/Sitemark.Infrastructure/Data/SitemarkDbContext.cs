using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sitemark.Application.Services;
using Sitemark.Domain.Entities;

namespace Sitemark.Infrastructure.Data
{
    internal class SitemarkDbContext : IdentityDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public SitemarkDbContext(
            DbContextOptions<SitemarkDbContext> options,
            ICurrentUserService currentUserService
            ) : base(options)
        {
            this._currentUserService = currentUserService;
        }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<LinkEntity> Links { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LinkEntity>(entity =>
            {
                entity.HasOne(link => link.User)
                      .WithMany() 
                      .HasForeignKey(link => link.UserId)
                      .IsRequired(); 

                entity.HasOne(link => link.Image)
                      .WithMany() 
                      .HasForeignKey(link => link.ImageId)
                      .IsRequired(false); 
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }



        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries<IBaseEntity>();
            var utcNow = DateTime.UtcNow;

            string currentUser = _currentUserService.UserId ?? "system";

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = utcNow;
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.UpdatedAt = null;
                        entry.Entity.UpdatedBy = null;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = utcNow;
                        entry.Entity.UpdatedBy = currentUser;
                        break;
                }
            }
        }

    }
}
