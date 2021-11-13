﻿namespace ProEShop.Common.GuardToolkit;

public static class GuardExt
{
    /// <summary>
    /// Checks if the argument is null.
    /// </summary>
    public static void CheckArgumentIsNull(this object o, string name)
    {
        if (o == null)
            throw new ArgumentNullException(name);
    }
}
