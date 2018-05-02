using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticTool
{
    internal class ComboBoxItem
    {
        public ComboBoxItem()
        {
        }

        public ComboBoxItem(string text)
            : this(text, null)
        {
        }

        public ComboBoxItem(string text, object tag)
        {
            this.Text = text;
            this.Tag = tag;
        }

        public string Text
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
