using System;
using System.Diagnostics;
using System.Reflection;

namespace AopDemo.Model.Attributes
{
    /// <summary>
    /// Base for other custom attributes such as NotNullAttribute, etc.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Parameter)]
    [DebuggerStepThrough]
    public abstract class ParameterAttribute : Attribute
    {
        public abstract void CheckParameter(ParameterInfo parameter, object value);
    }
}
