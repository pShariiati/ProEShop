﻿using System.ComponentModel.DataAnnotations;

namespace ProEShop.Common.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class BaseValidationAttribute : ValidationAttribute
{
    protected bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
        {
            return false;
        }
        attributes.Add(key, value);
        return true;
    }
}
