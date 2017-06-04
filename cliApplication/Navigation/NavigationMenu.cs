using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliApplication.Navigation
{
    public class NavigationMenu : DialogMenu
    {
        public delegate DialogMenu InputHandler(string input);
        public InputHandler CustomHandler;

        public NavigationMenu(string text, TextOutputDelegate write, List<DialogMenu> outcomes, InputHandler customHandler)
        {
            Text = text;
            Write = write;
            Outcomes = outcomes;
            CustomHandler = customHandler;
        }

        public override DialogMenu HandleUserInput(string input)
        {
            int numericInput = -1;
            bool isInteger = int.TryParse(input, out numericInput);
            if(isInteger && numericInput>0 && numericInput<Outcomes.Count)
            {
                return Outcomes[numericInput - 1];
            }
            else
            {
                return CustomHandler(input);
            }
        }

        public List<DialogMenu> Outcomes = new List<DialogMenu>();
    }
}
