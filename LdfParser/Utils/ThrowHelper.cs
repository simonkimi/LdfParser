using System;
using System.Runtime.CompilerServices;

namespace LdfParser.Utils;

public static class ThrowHelper
{
    public static void IfNull(object? value, [CallerMemberName] string? paramName = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}