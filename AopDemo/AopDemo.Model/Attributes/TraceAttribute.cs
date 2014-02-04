using System;
using System.Linq;
using System.Reflection;
using log4net;
using PostSharp.Aspects;

namespace AopDemo.Model.Attributes
{
    /// <summary>
    /// Aspect that, when applied on a method, emits a trace message before and
    /// after the method execution.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Assembly | AttributeTargets.Class,
                    AllowMultiple = true)]
    public class TraceAttribute : MethodInterceptionAspect {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TraceAttribute));

        private string _methodName;

        /// <summary>
        /// Method executed at build time. Initializes the aspect instance. After the execution
        /// of <see cref="CompileTimeInitialize"/>, the aspect is serialized as a managed 
        /// resource inside the transformed assembly, and deserialized at runtime.
        /// </summary>
        /// <param name="method">Method to which the current aspect instance 
        /// has been applied.</param>
        /// <param name="aspectInfo">Unused.</param>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo) {
            _methodName = method.DeclaringType.FullName + "." + method.Name;
        }

        /// <summary>
        /// Method invoked before the execution of the method to which the current
        /// aspect is applied.
        /// </summary>
        public override void OnInvoke(MethodInterceptionArgs eventArgs) {
            // get the arguments (in string form) that were passed to the method
            var args = String.Join(", ", eventArgs.Arguments.Select(arg => arg.ToString()).ToArray());

            Log.DebugFormat("Invoking [{0}] with arguments: [{1}]", _methodName, args);

            try {
                eventArgs.Proceed();
                Log.DebugFormat("Successfully invoked [{0}] with arguments: [{1}]", _methodName, args);
            } catch (Exception ex) {
                var errMsg = string.Format(
                    "[{0}] threw exception of type [{1}] when invoked with [{2}]",
                    _methodName, ex.GetType(), args);
                Log.Error(errMsg, ex);

                throw;
            }
        }
    }
}
