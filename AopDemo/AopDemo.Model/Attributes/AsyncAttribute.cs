using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using PostSharp.Aspects;

namespace AopDemo.Model.Attributes
{
    [Serializable]
    //[DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class AsyncAttribute : MethodInterceptionAspect {
        public override bool CompileTimeValidate(MethodBase method) {
            // make sure we have access to the method info so we can check
            // the response type
            var methodInfo = method as MethodInfo;
            if (methodInfo == null) {
                throw new Exception("MethodInfo is null");
            }

            // make sure the method does not have any return value
            if (methodInfo.ReturnType != typeof(void)) {
                var message = string.Format( 
                    "[{0}] can only be applied to a method with no return value",
                    GetType().Name);
                throw new Exception(message);
            }

            return base.CompileTimeValidate(method);
        }

        /// <summary>
        /// Executes the method asynchronously on the threadpool
        /// </summary>
        public override void OnInvoke(MethodInterceptionArgs eventArgs) {
            ThreadPool.QueueUserWorkItem(state => eventArgs.Proceed());
        }
    }
}