using Lab1.LexerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab1
{
    public class EndBlock : IBlock
    {
        public List<Token> Tokens { get; set; }

        public void Do(Dictionary<string, object> valTable, TextBox textBox)
        {
            
        }

        public IBlock NextBlock { get; set; }
        public IBlock GetNextOnOwnLevel()
        {
            throw new NotImplementedException();
        }
        public void SetNextOnOwnLevel(IBlock block)
        {
            throw new NotImplementedException();
        }
        public string TokensToString()
        {
            return " ";
        }
    }
}
