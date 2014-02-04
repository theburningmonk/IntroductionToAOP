namespace AopDemo.Model.Tests {
    using System;
    using DynamicProxy;
    using DynamicProxy.Interceptors;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using log4net.Config;
    using NUnit.Framework;    

    [TestFixture]
    public class DynamicProxiesTest {
        private IWindsorContainer _container;

        [TestFixtureSetUp]
        public void SetupTestFixture() {
            // initialize and install the windsor container
            _container = new WindsorContainer();
            _container.Install(new Installer());

            // Set up a simple configuration that logs on the console.
            BasicConfigurator.Configure();            
        }

        [Test]
        public void TestBoo() {
            new MyEntity().Boo();
        }

        [Test]
        public void TestInterceptedBoo() {
            _container.GetService<IEntity>().Boo();
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TestFoo() {
            new MyEntity().Foo();
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TestInterceptedFoo() {
            _container.GetService<IEntity>().Foo();
        }

        private class Installer : IWindsorInstaller {
            public void Install(IWindsorContainer container, IConfigurationStore store) {
                container.Register(Component.For<IEntity>().ImplementedBy<MyEntity>());
                container.Register(Component.For<LoggingInterceptor>().LifeStyle.Transient);
            }
        }
    }
}
