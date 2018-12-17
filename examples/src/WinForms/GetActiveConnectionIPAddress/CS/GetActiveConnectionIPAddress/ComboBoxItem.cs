namespace DotRas.Samples.GetActiveConnectionIPAddress
{
    internal class ComboBoxItem
    {
        public ComboBoxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}