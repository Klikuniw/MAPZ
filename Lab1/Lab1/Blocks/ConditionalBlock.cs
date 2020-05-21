using Lab1.LexerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab1
{
    public class ConditionalBlock : IBlock
    {
        public List<Token> Tokens { get; set; }

        public void Do(Dictionary<string, object> valTable, TextBox textBox)
        {
            object resultLeft = LeftExpression.CalculateExpression(valTable);
            object resultRight = RightExpression.CalculateExpression(valTable);

            switch (GetConditional())
            {
                case ">":
                {
                    if (resultRight.GetType() == resultLeft.GetType() && resultRight.GetType() == typeof(string))
                    {
                        if ((resultLeft as string).Length > (resultRight as string).Length)
                        {
                            NextBlock = Positive;
                        }
                        else
                        {
                            Positive = Negative;
                        }
                    }
                    else if (resultRight.GetType() == resultLeft.GetType() && resultRight.GetType() == typeof(int))
                    {
                        if (Convert.ToInt32(resultLeft) > Convert.ToInt32(resultRight))
                        {
                            NextBlock = Positive;
                        }
                        else
                        {
                            Positive = Negative;
                        }
                    }
                    break;
                }
                case "<":
                {
                    if (resultRight.GetType() == resultLeft.GetType() && resultRight.GetType() == typeof(string))
                    {
                        if ((resultLeft as string).Length < (resultRight as string).Length)
                        {
                            NextBlock = Positive;
                        }
                        else
                        {
                            NextBlock = Negative;
                        }
                    }
                    else if (resultRight.GetType() == resultLeft.GetType() && resultRight.GetType() == typeof(int))
                    {
                        if (Convert.ToInt32(resultLeft) < Convert.ToInt32(resultRight))
                        {
                            NextBlock = Positive;
                        }
                        else
                        {
                            NextBlock = Negative;
                        }
                    }
                    break;
                }
                case " ":
                {

                    break;
                }
            }


        }

        public IBlock NextBlock
        {
            get { return Positive; }
            set { }
        }

        public IBlock GetNextOnOwnLevel()
        {
            return Negative;
        }
        public void SetNextOnOwnLevel(IBlock block)
        {
            Negative = block;
        }

        public string Conditional { get; set; }
        public IBlock Positive { get; set; }
        public IBlock Negative { get; set; }

        public Expression LeftExpression { get; set; }
        public Expression RightExpression { get; set; }

        private string GetConditional()
        {
            List<Token> tokens = Lexer.Tokenize(Conditional);
            if (tokens.Exists(x => x.Type == TokenType.Operator && x.Value == ">"))
            {
                return ">";
            }
            if (tokens.Exists(x => x.Type == TokenType.Operator && x.Value == "<"))
            {
                return "<";
            }

            return " ";
        }
        public string TokensToString()
        {
            string result = "";
            for (int i = 0; i < Tokens.IndexOf(Tokens.Find(x=>x.Value=="{")); i++)
            {
                result += Tokens[i].Value;
            }
            return result;
        }

    }
}
