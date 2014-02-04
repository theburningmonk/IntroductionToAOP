using System;
using System.Diagnostics;
using System.Threading;
using AopDemo.Model.Attributes;
using NUnit.Framework;

namespace AopDemo.Model.Tests
{
    /// <summary>
    /// Unit tests for the AsyncAttribute
    /// </summary>
    [TestFixture]
    public class AsyncAttributeTest
    {
        private static readonly TimeSpan SleepTime = TimeSpan.FromSeconds(10);
        private static readonly ManualResetEvent ResetEvent = new ManualResetEvent(false);

        [Test]
        public void TestAsyncMethodDoesNotBlock()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            // sleep for 10 seconds
            Sleep(SleepTime);

            var elapsed = stopWatch.ElapsedMilliseconds;

            // wait for the reset event
            ResetEvent.WaitOne();
            
            stopWatch.Stop();

            Console.WriteLine("Elapsed time was {0} milliseconds", elapsed);
            Console.WriteLine("Total timer time was {0} milliseconds", stopWatch.ElapsedMilliseconds);

            // the method invokation shouldn't have taken the full sleep time
            Assert.IsTrue(elapsed < SleepTime.TotalMilliseconds);

            // but by the time the reset event is set the full sleep time should have passed
            Assert.IsTrue(stopWatch.ElapsedMilliseconds >= SleepTime.TotalMilliseconds);
        }

        [Async]
        private static void Sleep(TimeSpan sleepTime)
        {
            // sleep for the specified amount of time and then set the reset event
            Thread.Sleep(sleepTime);
            ResetEvent.Set();
        }
    }
}