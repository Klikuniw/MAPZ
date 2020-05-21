using Lab1.LexerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BLL;

namespace Lab1
{
    public class FunctionBlock : IBlock
    {
        public List<Token> Tokens { get; set; }

        public void Do(Dictionary<string, object> valTable, TextBox textBox)
        {
            GetArguments(valTable, Tokens);
            FunctionService.CallFunction(Name, Arguments,textBox);
        }

        public string Name { get; set; }
        public List<object> Arguments { get; set; }
        public IBlock NextBlock { get; set; }
        
        public IBlock GetNextOnOwnLevel()
        {
            return NextBlock;
        }
        public void SetNextOnOwnLevel(IBlock block)
        {
            NextBlock = block;
        }
        private void GetArguments(Dictionary<string, object> valTable, List<Token> tokens)
        {
            Arguments = new List<object>();

            int IndexOfOpenBracket = tokens.IndexOf(tokens.Find(x => x.Value == "("));
            int IndexOfCloseBracket = tokens.IndexOf(tokens.FindLast(x => x.Value == ")"));

            for (int i = IndexOfOpenBracket + 1; i < IndexOfCloseBracket; i++)
            {
                if (tokens[i].Type != TokenType.Comma)
                {
                    if (tokens[i].Type == TokenType.Variable)
                    {
                        Arguments.Add(valTable[tokens[i].Value]);
                    }
                    else
                    {
                        Arguments.Add(tokens[i].Value);
                    }
                }
            }
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
