using Lab1.LexerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab1
{
    public class LoopBlock : IBlock
    {
        private int _from;
        private int _to;

        public int From { get; set; }
        public int To { get; set; }
        public int Step { get; set; }
        public IBlock Positive { get; set; }
        public IBlock Negative { get; set; }
        public List<Token> Tokens { get; set; }

        public LoopBlock(int from, int to)
        {
            _from = from;
            _to = to;
        }
        public void Do(Dictionary<string, object> valTable, TextBox textBox)
        {
            if (From < To)
            {
                NextBlock = Positive;
                From += Step;
            }
            else
            {
                Positive = Negative;
                From = _from;
                To = _to;
            }
        }

        public IBlock NextBlock
        {
            get { return Positive; }
            set {}
        }
        public IBlock GetNextOnOwnLevel()
        {
            return Negative;
        }
        public void SetNextOnOwnLevel(IBlock block)
        {
            Negative = block;
        }
        public string TokensToString()
        {
            string result = "";
            for (int i = 0; i < Tokens.Count; i++)
            {
                result += Tokens[i].Value;
            }
            return result;
        }
    }
}
