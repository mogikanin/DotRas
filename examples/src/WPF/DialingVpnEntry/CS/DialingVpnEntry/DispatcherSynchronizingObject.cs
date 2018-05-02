using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace DialingVpnEntryWithWPF
{
    internal class DispatcherSynchronizingObject : ISynchronizeInvoke
    {
        private Dispatcher dispatcher;

        public DispatcherSynchronizingObject(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public bool InvokeRequired
        {
            get { return true; }
        }

        public IAsyncResult BeginInvoke(Delegate method, object[] args)
        {
            return new DispatcherAsyncResult(this.dispatcher.BeginInvoke(method, args));
        }

        public object EndInvoke(IAsyncResult result)
        {
            DispatcherAsyncResult asyncResult = result as DispatcherAsyncResult;
            if (asyncResult != null)
            {
                DispatcherOperation dispatcherOperation = asyncResult.AsyncState as DispatcherOperation;
                if (dispatcherOperation != null)
                {
                    return dispatcherOperation.Wait();
                }
            }

            return null;
        }

        public object Invoke(Delegate method, object[] args)
        {
            return this.dispatcher.Invoke(method, args);
        }
    }
}