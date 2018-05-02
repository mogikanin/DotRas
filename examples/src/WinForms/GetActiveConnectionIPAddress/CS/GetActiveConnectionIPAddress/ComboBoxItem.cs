using System;

namespace DotRas.Samples.GetActiveConnectionIPAddress
{
    internal class ComboBoxItem
    {
        private string _text;
        private object _value;

        public ComboBoxItem(string text, object value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public object Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}