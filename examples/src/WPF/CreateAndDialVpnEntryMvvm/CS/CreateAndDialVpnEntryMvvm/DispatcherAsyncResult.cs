using System;
using System.Threading;
using System.Windows.Threading;

namespace CreateAndDialVpnEntryMvvm
{
    internal class DispatcherAsyncResult : IAsyncResult
    {
        private readonly DispatcherOperation _dispatcherOperation;

        public DispatcherAsyncResult(DispatcherOperation dispatcherOperation)
        {
            _dispatcherOperation = dispatcherOperation;
        }

        public object AsyncState => _dispatcherOperation;

        public WaitHandle AsyncWaitHandle => null;

        public bool CompletedSynchronously => false;

        public bool IsCompleted => _dispatcherOperation.Status == DispatcherOperationStatus.Completed;
    }
}