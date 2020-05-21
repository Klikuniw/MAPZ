using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab1
{
    public class Interpreter
    {

        public void Execute(IBlock head, TextBox textBox, Dictionary<string, object> valTable)
        {
            while (head.GetType() != typeof(EndBlock))
            {
                head.Do(valTable, textBox);
                head = head.NextBlock;
            }
        }

    }
}
