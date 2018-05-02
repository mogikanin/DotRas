using System;
using System.Threading;
using System.Windows.Threading;

namespace DialingVpnEntryWithWPF
{
    internal class DispatcherAsyncResult : IAsyncResult
    {
        private DispatcherOperation dispatcherOperation;

        public DispatcherAsyncResult(DispatcherOperation dispatcherOperation)
        {
            this.dispatcherOperation = dispatcherOperation;
        }

        public object AsyncState
        {
            get { return this.dispatcherOperation; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return this.dispatcherOperation.Status == DispatcherOperationStatus.Completed; }
        }
    }
}