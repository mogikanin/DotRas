namespace CreateAndDialVpnEntryMvvm
{
    internal class DialerStateChangedMessage
    {
        public DialerStateChangedMessage()
        {
        }

        public DialerStateChangedMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}