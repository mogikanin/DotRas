using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace CreateAndDialVpnEntryMvvm
{
    internal class DispatcherSynchronizingObject : ISynchronizeInvoke
    {
        private readonly Dispatcher _dispatcher;

        public DispatcherSynchronizingObject(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public bool InvokeRequired => true;

        public IAsyncResult BeginInvoke(Delegate method, object[] args)
        {
            return new DispatcherAsyncResult(_dispatcher.BeginInvoke(method, args));
        }

        public object EndInvoke(IAsyncResult result)
        {
            if (result is DispatcherAsyncResult asyncResult)
            {
                if (asyncResult.AsyncState is DispatcherOperation dispatcherOperation)
                {
                    return dispatcherOperation.Wait();
                }
            }

            return null;
        }

        public object Invoke(Delegate method, object[] args)
        {
            return _dispatcher.Invoke(method, args);
        }
    }
}