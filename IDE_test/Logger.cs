using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDE_test
{
    public class ConsoleWriterEventArgs : EventArgs
    {
        public string Value { get; private set; }
        public ConsoleWriterEventArgs(string value)
        {
            Value = value;
        }
    }
    public class TextBoxOutputter : TextWriter
    {
        RichTextBox textBox = null;

        SynchronizationContext _syncContext;

        public TextBoxOutputter(RichTextBox output)
        {
            textBox = output;
            _syncContext = SynchronizationContext.Current;
        }

        public override void Write(string value)
        {
            _syncContext.Send(_ =>
            {
                textBox.AppendText(value);
            }, null);
        }

        public override void WriteLine(string value)
        {
            _syncContext.Send(_ =>
            {
                textBox.AppendText($"{value}\n");
            }, null);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
