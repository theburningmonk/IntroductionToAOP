using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AopDemo.Model.Extensions;
using PostSharp.Aspects;

namespace AopDemo.Model.Attributes
{
    [Serializable]
    [DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class CheckParametersAttribute : OnMethodBoundaryAspect 
    {
        public CheckParametersAttribute()
        {
            ParameterAttributes = new Dictionary<int, Tuple<ParameterInfo, ParameterAttribute[]>>();
        }

        private Dictionary<int, Tuple<ParameterInfo, ParameterAttribute[]>> ParameterAttributes { get; set; }

        private bool CheckParameters { get; set; }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            base.CompileTimeInitialize(method, aspectInfo);

            // resolve the parameters list and keep track of which parameters need to be validated
            // at compile time instead of runtime!
            var parameters = method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                var attributes = param.GetAttributes<ParameterAttribute>();
                if (attributes.Any())
                {
                    ParameterAttributes.Add(i, Tuple.Create(param, attributes.ToArray()));
                }
            }

            CheckParameters = ParameterAttributes.Any();
        }

        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            // don't do any validation if no necessary
            if (CheckParameters)
            {
                var arguments = eventArgs.Arguments;

                foreach (var kvp in ParameterAttributes)
                {
                    var arg = arguments[kvp.Key];
                    var param = kvp.Value.Item1;
                    foreach (var pa in kvp.Value.Item2)
                    {
                        pa.CheckParameter(param, arg);
                    }
                }
            }
        }
    }
}
