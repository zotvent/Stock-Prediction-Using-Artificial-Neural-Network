using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliApplication.Navigation
{
    public abstract class DialogMenu
    {
        public delegate void TextOutputDelegate(string text);

        public string Text;

        public TextOutputDelegate Write;

        public abstract DialogMenu HandleUserInput(string input);
    }
}
