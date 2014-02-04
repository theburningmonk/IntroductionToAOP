using System;
using System.Diagnostics;
using System.Reflection;

namespace AopDemo.Model.Attributes
{
    /// <summary>
    /// Check if a parameter is a Guid with a value that's not the default Guid value
    /// </summary>
    [Serializable]
    [DebuggerStepThrough]
    public sealed class NotDefaultGuidAttribute : ParameterAttribute {
        public override void CheckParameter(ParameterInfo parameter, object value) {
            if (!(value is Guid) || (Guid)value == default(Guid))
            {
                var errorMessage = string.Format(
                    "{0} is not a Guid or is has the default Guid value [{1}]",
                    parameter.Name,
                    value);

                throw new ArgumentException(errorMessage);
            }
        }
    }
}