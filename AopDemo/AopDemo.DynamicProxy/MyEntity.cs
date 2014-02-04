namespace AopDemo.DynamicProxy {
    using System;
    using Interceptors;
    using Castle.Core;    

    [Interceptor(typeof(LoggingInterceptor))]
    public class MyEntity : IEntity {
        public void Boo() {
            Console.WriteLine("Boo!");
        }

        public void Foo() {
            throw new Exception("Foo!");
        }
    }
}
