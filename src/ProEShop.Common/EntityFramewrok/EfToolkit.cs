using Microsoft.EntityFrameworkCore;

namespace ProEShop.Common.EntityFramewrok;

public static class EfToolkit
{
    public static void RegisterAllEntities(this ModelBuilder builder, Type type)
    {
        var entities = type.Assembly.GetTypes()
            .Where(x => x.BaseType == type);
        foreach (var entity in entities)
            builder.Entity(entity);
    }
}
