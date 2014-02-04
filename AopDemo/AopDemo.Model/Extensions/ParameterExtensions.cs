using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AopDemo.Model.Extensions
{
    [DebuggerStepThrough]
    public static class ParameterExtensions
    {
        public static List<T> GetAttributes<T>(this ICustomAttributeProvider pi) 
            where T : Attribute {
            return pi.GetCustomAttributes(typeof(T), false)
                     .OfType<T>()
                     .ToList();
        }
    }
}
