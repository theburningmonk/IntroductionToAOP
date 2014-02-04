namespace AopDemo.Model.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using PostSharp.Aspects;

    [Serializable]
    [AttributeUsage(AttributeTargets.Method)] // only allowed on methods
    [DebuggerStepThrough]
    public sealed class MemoizeAttribute : MethodInterceptionAspect {
        private const int DefaultMemoSize = 100; // default memo size is 100

        // private field to store memos
        private readonly Dictionary<string, object> _memos = new Dictionary<string, object>();
        // private queue to keep track of the order the memos are put in
        private readonly Queue<string> _queue = new Queue<string>();

        public MemoizeAttribute() : this(DefaultMemoSize) {            
        }

        public MemoizeAttribute(int memoSize) {
            MemoSize = memoSize;
        }

        public int MemoSize { get; set; } // how many items to keep in the memo

        // intercept the method invocation
        public override void OnInvoke(MethodInterceptionArgs eventArgs) {
            // get the arguments that were passed to the method
            var args = eventArgs.Arguments;

            var keyBuilder = new StringBuilder();

            // append the hashcode of each arg to the key
            // this limits us to value types (and strings)
            // i need a better way to do this (and preferably
            // a faster one)
            foreach (var t in args) keyBuilder.Append(t.GetHashCode());

            var key = keyBuilder.ToString();

            // if the key doesn't exist, invoke the original method
            // passing the original arguments and store the result
            if (!_memos.ContainsKey(key)) {
                _memos[key] = eventArgs.Method.Invoke(eventArgs.Instance, args.ToArray());
                _queue.Enqueue(key);

                // if we've exceeded the set memo size, then remove the earliest entry
                if (_queue.Count > MemoSize) {
                    var deQueueKey = _queue.Dequeue();
                    _memos.Remove(deQueueKey);
                }
            }

            // return the memo
            eventArgs.ReturnValue = _memos[key];
        }
    }
}
