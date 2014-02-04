namespace AopDemo.DynamicProxy.Interceptors {
    using System;
    using Castle.DynamicProxy;
    using log4net;

    public class LoggingInterceptor : IInterceptor {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LoggingInterceptor));

        public void Intercept(IInvocation invocation) {
            var methodName = invocation.Method.Name;
            Logger.DebugFormat("Begin invoking method [{0}]", methodName);

            try {
                invocation.Proceed();
                Logger.DebugFormat("Successfully invoked method [{0}]", methodName);
            } catch (Exception ex) {
                var errMsg = string.Format(
                    "Exception caught whilst invoking method [{0}]", methodName);
                Logger.Error(errMsg, ex);
                throw;
            }
        }
    }
}