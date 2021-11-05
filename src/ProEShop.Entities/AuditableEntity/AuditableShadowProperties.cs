using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProEShop.Entities.AuditableEntity
{
    public static class AuditableShadowProperties
    {
        public const string CreatedByBrowserName = nameof(CreatedByBrowserName);

        public const string ModifiedByBrowserName = nameof(ModifiedByBrowserName);

        public const string CreatedByIp = nameof(CreatedByIp);

        public const string ModifiedByIp = nameof(ModifiedByIp);

        public const string CreatedByUserId = nameof(CreatedByUserId);

        public const string ModifiedByUserId = nameof(ModifiedByUserId);

        public const string CreatedDateTime = nameof(CreatedDateTime);

        public const string ModifiedDateTime = nameof(ModifiedDateTime);

        //Microsoft.EntityFrameworkCore for using Model builder
        public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                                .GetEntityTypes()
                                                .Where(e => typeof(IAuditableEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(CreatedByBrowserName).HasMaxLength(1000);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(ModifiedByBrowserName).HasMaxLength(1000);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(CreatedByIp).HasMaxLength(255);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<string>(ModifiedByIp).HasMaxLength(255);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<long?>(CreatedByUserId);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<long?>(ModifiedByUserId);

                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime>(CreatedDateTime);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime?>(ModifiedDateTime);
            }
        }
    }
}