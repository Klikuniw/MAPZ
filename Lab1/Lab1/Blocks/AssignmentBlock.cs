using Lab1.LexerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLL;
using System.Windows.Controls;

namespace Lab1
{
    public class AssignmentBlock : IBlock
    {
        public List<Token> Tokens { get; set; }
        public string Variable { get; set; }
        public Expression Expression { get; set; }

        public IBlock NextBlock { get; set; }

        public IBlock GetNextOnOwnLevel()
        {
            return NextBlock;
        }
        public void SetNextOnOwnLevel(IBlock block)
        {
            NextBlock = block;
        }
        public void Do(Dictionary<string, object> valTable, TextBox textBox)
        {
            if (Expression.Tokens.Exists(x => x.Type == TokenType.Operator))
            {
                object resultOfExpression = Expression.CalculateExpression(valTable);
                ParseAssignment(valTable, textBox, resultOfExpression);
            }
            else
            {
                Expression.Tokens.RemoveAll(x => x.Type == TokenType.Space);
                Tokens.RemoveAll(x => x.Type == TokenType.Space);
                object resultOfExpression = null;

                switch (Expression.Tokens[0].Type)
                {
                    case TokenType.Function:
                    {
                        List<object> args = new List<object>();
                        GetArguments(valTable, Expression.Tokens, args);

                        resultOfExpression = FunctionService.CallFunction(Expression.Tokens.First(x => x.Type == TokenType.Function).Value, args,textBox);

                        break;
                    }
                    case TokenType.Constant:
                    {
                        resultOfExpression = Expression.Tokens[0].Value;
                        break;
                    }
                    case TokenType.Variable:
                    {
                        resultOfExpression = valTable[Expression.Tokens[0].Value];
                        break;
                    }
                }

                ParseAssignment(valTable, textBox, resultOfExpression);
            }

        }
        private void ParseAssignment(Dictionary<string, object> valTable, TextBox textBox,object resultOfExpression)
        {
            Expression.Tokens.RemoveAll(x => x.Type == TokenType.Space);
            Tokens.RemoveAll(x => x.Type == TokenType.Space);

            if (Tokens[0].Type == TokenType.Keyword)
            {
                switch (Tokens[0].Value)
                {
                    case "string":
                        {
                            if (resultOfExpression.GetType() == typeof(string))
                            {
                                valTable.Add(Tokens[1].Value, resultOfExpression);
                            }
                            break;
                        }
                    case "listS":
                        {
                            if (resultOfExpression.GetType() == typeof(List<string>))
                            {
                                valTable.Add(Tokens[1].Value, resultOfExpression);
                            }
                            break;
                        }
                    case "int":
                        {
                            int resInt;

                            if (Int32.TryParse(resultOfExpression.ToString(), out resInt))
                            {
                                valTable.Add(Tokens[1].Value, resInt);
                            }
                            else
                            {
                                throw new Exception("Cant cast");
                            }
                            break;
                        }
                }
            }
            else if (Tokens[0].Type == TokenType.Variable)
            {
                if (valTable[Tokens[0].Value].GetType() == resultOfExpression.GetType())
                {
                    valTable[Tokens[0].Value] = resultOfExpression;
                }

            }

        }
        private void GetArguments(Dictionary<string, object> valTable, List<Token> tokens, List<object> args)
        {
            int IndexOfOpenBracket = tokens.IndexOf(Expression.Tokens.Find(x => x.Value == "("));
            int IndexOfCloseBracket = tokens.IndexOf(Expression.Tokens.FindLast(x => x.Value == ")"));
            for (int i = IndexOfOpenBracket + 1; i < IndexOfCloseBracket; i++)
            {
                if (tokens[i].Type != TokenType.Comma)
                {
                    if (tokens[i].Type == TokenType.Variable)
                    {
                        args.Add(valTable[tokens[i].Value]);
                    }
                    else
                    {
                        args.Add(tokens[i].Value);
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
