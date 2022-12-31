using System.Linq.Expressions;

namespace ProEShop.Common.Helpers;

public static class ExpressionHelpers
{
    public static Expression<Func<T, bool>> CreateExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        if (propertyValue is string)
            propertyValue = propertyValue.ToString()?.Trim();
        var constantValue = Expression.Constant(propertyValue);
        var equal = Expression.Equal(property, constantValue);
        return Expression.Lambda<Func<T, bool>>(equal, parameter);
    }

    /// <summary>
    /// تولید اکسپرشن مورد نیاز
    /// IsExists
    /// برای موجودیت های واسط
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName1"></param>
    /// <param name="propertyName2"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> CreateExistExpressionForMiddleEntities<T>(string propertyName1, string propertyName2, object propertyValue1, object propertyValue2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var property = Expression.Property(parameter, propertyName1);
        if (propertyValue1 is string)
            propertyValue1 = propertyValue1.ToString()?.Trim();
        var constantValue = Expression.Constant(propertyValue1);
        var equal = Expression.Equal(property, constantValue);

        var property2 = Expression.Property(parameter, propertyName2);
        if (propertyValue2 is string)
            propertyValue2 = propertyValue2.ToString()?.Trim();
        var constantValue2 = Expression.Constant(propertyValue2);

        var equal2 = Expression.Equal(property2, constantValue2);

        // Difference between And and AndAlso
        // https://stackoverflow.com/a/302080/16180500
        var finalResult = Expression.AndAlso(equal, equal2);

        return Expression.Lambda<Func<T, bool>>(finalResult, parameter);
    }

    public static IQueryable<T> CreateSearchExpressions<T>(IQueryable<T> query, object model, bool callDeletedStatusExpression = true)
    {
        var containsExpressions = CreateContainsExpressions(query, model);
        var equalExpressions = CreateEqualExpressions(containsExpressions, model);
        var enumEqualExpressions = CreateEnumEqualExpressions(equalExpressions, model);
        var equalDateTimeExpressions = CreateEqualDateTimeExpressions(enumEqualExpressions, model);
        var betweenNumbersExpressions = CreateBetweenNumbersExpressions(equalDateTimeExpressions, model);
        if (callDeletedStatusExpression)
        {
            return CreateDeletedStatusExpression(betweenNumbersExpressions, model);
        }

        return betweenNumbersExpressions;
    }

    public static IQueryable<T> CreateContainsExpressions<T>(IQueryable<T> query, object model)
    {
        var result = query;
        var propertiesToSearch = model.GetType().GetProperties()
            .Where(x => Attribute.IsDefined(x, typeof(ContainsSearchAttribute)))
            .ToList();
        if (propertiesToSearch.Count > 0)
        {
            foreach (var propertyInfo in propertiesToSearch)
            {
                var propertyValue = propertyInfo.GetValue(model);
                if (!string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    var parameter = Expression.Parameter(typeof(T));
                    var property = Expression.Property(parameter, propertyInfo.Name);
                    if (propertyValue is string)
                        propertyValue = propertyValue.ToString()?.Trim();

                    // property: OrderNumber
                    // OrderNumber.ToString().Contains()

                    if (property.Type.Name.IsNumericType())
                    {
                        var toStringMethodExp = Expression.Call(property, "ToString", null);

                        var constantValue = Expression.Constant(Convert.ChangeType(propertyValue, typeof(string)));
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var containsMethodExp = Expression.Call(toStringMethodExp, containsMethod, constantValue);
                        var exp = Expression.Lambda<Func<T, bool>>(containsMethodExp, parameter);
                        result = result.Where(exp);
                    }
                    else
                    {
                        var constantValue = Expression.Constant(Convert.ChangeType(propertyValue, typeof(string)));
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var containsMethodExp = Expression.Call(property, containsMethod, constantValue);
                        var exp = Expression.Lambda<Func<T, bool>>(containsMethodExp, parameter);
                        result = result.Where(exp);
                    }
                }
            }
        }

        return result;
    }

    public static IQueryable<T> CreateEqualExpressions<T>(IQueryable<T> query, object model)
    {
        var result = query;
        var propertiesToSearch = model.GetType().GetProperties()
            .Where(x => Attribute.IsDefined(x, typeof(EqualSearchAttribute)))
            .ToList();
        if (propertiesToSearch.Count > 0)
        {
            foreach (var propertyInfo in propertiesToSearch)
            {
                var propertyValue = propertyInfo.GetValue(model);
                if (!string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    var parameter = Expression.Parameter(typeof(T));
                    var property = Expression.Property(parameter, propertyInfo.Name);
                    if (propertyValue is string)
                        propertyValue = propertyValue.ToString()?.Trim();
                    var constantValue = Expression.Constant(propertyValue);
                    var equal = Expression.Equal(property, constantValue);
                    var exp = Expression.Lambda<Func<T, bool>>(equal, parameter);
                    result = result.Where(exp);
                }
            }
        }

        return result;
    }

    public static IQueryable<T> CreateBetweenNumbersExpressions<T>(IQueryable<T> query, object model)
    {
        var result = query;
        var propertiesToSearch = model.GetType().GetProperties()
            .Where(x => Attribute.IsDefined(x, typeof(BetweenNumbersAttribute)))
            .ToList();
        if (propertiesToSearch.Count > 0)
        {
            foreach (var propertyInfo in propertiesToSearch)
            {
                var propertyValue = propertyInfo.GetValue(model);
                if (!string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    var propertyName = propertyInfo.Name;

                    if (propertyName.EndsWith("From"))
                    {
                        // x => x.FinalPrice >= 13,275,000
                        propertyName = propertyName[..^4];

                        var parameter = Expression.Parameter(typeof(T));
                        var property = Expression.Property(parameter, propertyName);
                        var constantValue = Expression.Constant(propertyValue);
                        var equal = Expression.GreaterThanOrEqual(property, constantValue);
                        var exp = Expression.Lambda<Func<T, bool>>(equal, parameter);
                        result = result.Where(exp);
                    }
                    else if (propertyName.EndsWith("To"))
                    {
                        // x => x.FinalPrice <= 13,275,000
                        propertyName = propertyName[..^2];

                        var parameter = Expression.Parameter(typeof(T));
                        var property = Expression.Property(parameter, propertyName);
                        var constantValue = Expression.Constant(propertyValue);
                        var equal = Expression.LessThanOrEqual(property, constantValue);
                        var exp = Expression.Lambda<Func<T, bool>>(equal, parameter);
                        result = result.Where(exp);
                    }
                }
            }
        }

        return result;
    }

    public static IQueryable<T> CreateEnumEqualExpressions<T>(IQueryable<T> query, object model)
    {
        var result = query;
        var propertiesToSearch = model.GetType().GetProperties()
            .Where(x => Attribute.IsDefined(x, typeof(EnumEqualSearchAttribute)))
            .ToList();
        if (propertiesToSearch.Count > 0)
        {
            foreach (var propertyInfo in propertiesToSearch)
            {
                var propertyValue = propertyInfo.GetValue(model);
                if (!string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    var parameter = Expression.Parameter(typeof(T));
                    var property = Expression.Property(parameter, propertyInfo.Name);
                    var constantValue = Expression.Constant((byte)propertyValue);
                    var convertedProperty = Expression.Convert(property, typeof(byte));
                    var equal = Expression.Equal(convertedProperty, constantValue);
                    var exp = Expression.Lambda<Func<T, bool>>(equal, parameter);
                    result = result.Where(exp);
                }
            }
        }

        return result;
    }

    public static IQueryable<T> CreateEqualDateTimeExpressions<T>(IQueryable<T> query, object model)
    {
        var result = query;
        var propertiesToSearch = model.GetType().GetProperties()
            .Where(x => Attribute.IsDefined(x, typeof(EqualDateTimeSearchAttribute)))
            .ToList();
        if (propertiesToSearch.Count > 0)
        {
            foreach (var propertyInfo in propertiesToSearch)
            {
                var propertyValue = propertyInfo.GetValue(model);
                if (!string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    var parameter = Expression.Parameter(typeof(T));
                    var property = Expression.Property(parameter, propertyInfo.Name);
                    var dateProperty = Expression.Property(property, "Date");
                    if (propertyValue is string)
                    {
                        //.Where(x => x.DeliveryDate.Date == gregorianDeliveryDate.Result.Date);
                        var (isSuccessful, dateTimeResult) = propertyValue.ToString().ToGregorianDate();
                        if (isSuccessful)
                        {
                            var constantValue = Expression.Constant(dateTimeResult.Date);
                            var equal = Expression.Equal(dateProperty, constantValue);
                            var exp = Expression.Lambda<Func<T, bool>>(equal, parameter);
                            result = result.Where(exp);
                        }
                    }
                }
            }
        }

        return result;
    }

    public static IQueryable<T> CreateDeletedStatusExpression<T>(IQueryable<T> query, object model)
    {
        var result = query;
        var deletedStatusValue = model.GetType().GetProperty("DeletedStatus")!.GetValue(model)!.ToString();
        if (deletedStatusValue != "True")
        {
            var finalValue = deletedStatusValue == "OnlyDeleted";
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, "IsDeleted");
            var constantValue = Expression.Constant(finalValue);
            var equal = Expression.Equal(property, constantValue);
            var exp = Expression.Lambda<Func<T, bool>>(equal, parameter);
            result = result.Where(exp);
        }
        return result;
    }

    public static IOrderedQueryable<T> CreateOrderByExpression<T>(this IQueryable<T> query, string propertyName, string isAsc)
    {
        var parameter = Expression.Parameter(typeof(T));
        var conversion = Expression.Convert(Expression.Property(parameter, propertyName), typeof(object));
        var exp = Expression.Lambda<Func<T, object>>(conversion, parameter);

        IOrderedQueryable<T> result = null;
        result = isAsc == "Asc" ? query.OrderBy(exp) : query.OrderByDescending(exp);
        return result;
    }
}
