using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProEShop.Common.IdentityToolkit;

namespace ProEShop.Entities.AuditableEntity;

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
    public static AppShadowProperties GetShadowProperties(this IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor == null)
        {
            return null;
        }

        var httpContext = httpContextAccessor?.HttpContext;
        return new AppShadowProperties
        {
            UserAgent = httpContext?.Request?.Headers["User-Agent"].ToString(),
            UserIp = httpContext?.Connection?.RemoteIpAddress?.ToString(),
            Now = DateTime.Now,
            UserId = GetUserId(httpContext)
        };
    }
    private static long? GetUserId(HttpContext httpContext)
    {
        return httpContext?.User?.Identity?.GetUserId();
    }

    public static void SetAuditableEntityPropertyValues(
        this ChangeTracker changeTracker,
        AppShadowProperties props)
    {
        if (props == null)
        {
            return;
        }

        var modifiedEntries = changeTracker.Entries<IAuditableEntity>()
            .Where(x => x.State == EntityState.Modified);
        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.SetModifiedShadowProperties(props);
        }

        var addedEntries = changeTracker.Entries<IAuditableEntity>()
            .Where(x => x.State == EntityState.Added);
        foreach (var addedEntry in addedEntries)
        {
            addedEntry.SetAddedShadowProperties(props);
        }
    }

    public static void SetModifiedShadowProperties(this EntityEntry<IAuditableEntity> modifiedEntry, AppShadowProperties props)
    {
        if (props == null)
        {
            return;
        }

        modifiedEntry.Property(ModifiedDateTime).CurrentValue = props.Now;
        if (!string.IsNullOrWhiteSpace(props.UserAgent))
            modifiedEntry.Property(ModifiedByBrowserName).CurrentValue = props.UserAgent;
        if (!string.IsNullOrWhiteSpace(props.UserIp))
            modifiedEntry.Property(ModifiedByIp).CurrentValue = props.UserIp;
        if (props.UserId.HasValue)
            modifiedEntry.Property(ModifiedByUserId).CurrentValue = props.UserId;
    }

    public static void SetAddedShadowProperties(this EntityEntry<IAuditableEntity> addedEntry, AppShadowProperties props)
    {
        if (props == null)
        {
            return;
        }

        addedEntry.Property(CreatedDateTime).CurrentValue = props.Now;
        if (!string.IsNullOrWhiteSpace(props.UserAgent))
            addedEntry.Property(CreatedByBrowserName).CurrentValue = props.UserAgent;
        if (!string.IsNullOrWhiteSpace(props.UserIp))
            addedEntry.Property(CreatedByIp).CurrentValue = props.UserIp;
        if (props.UserId.HasValue)
            addedEntry.Property(CreatedByUserId).CurrentValue = props.UserId;
    }
}
