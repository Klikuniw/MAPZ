using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Lab1.LexerClasses;

namespace Lab1
{
    public interface IBlock
    {
        List<Token> Tokens { get; set; }
        void Do(Dictionary<string, object> valTable, TextBox textBox);
        IBlock NextBlock { get; set; }

        IBlock GetNextOnOwnLevel();
        void SetNextOnOwnLevel(IBlock block);
        string TokensToString();
    }
}
