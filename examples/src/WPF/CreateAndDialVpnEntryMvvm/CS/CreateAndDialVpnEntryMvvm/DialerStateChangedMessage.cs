namespace CreateAndDialVpnEntryMvvm
{
    using System;

    internal class DialerStateChangedMessage
    {
        public DialerStateChangedMessage()
        {
        }

        public DialerStateChangedMessage(string message)
        {
            this.Message = message;
        }
        
        public string Message
        {
            get;
            private set;
        }
    }
}