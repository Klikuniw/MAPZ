using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.LexerClasses;
using BLL;

namespace Lab1
{
    public class Expression
    {
        public List<Token> Tokens { get; set; }

        public object CalculateExpression(Dictionary<string, object> valTable)
        {
            object result = null;

            CheckOfFunctions(Tokens,valTable);
            CheckConstantsType(Tokens);

            Tokens.RemoveAll(x => x.Type == TokenType.Space);
            Tokens.RemoveAll(x => x.Type == TokenType.EndTag);

            List<Token> postfixTokens = ToPostfix(Tokens);
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < postfixTokens.Count(); i++)
            {
                if (postfixTokens.ElementAt(i).Type == TokenType.Constant /*|| postfixTokens.ElementAt(i).Type == TokenType.Variable*/)
                {
                    stack.Push(postfixTokens.ElementAt(i).Value);
                }
                else if (postfixTokens.ElementAt(i).Type == TokenType.Variable)
                {
                    stack.Push(valTable[postfixTokens.ElementAt(i).Value].ToString());
                }
                else if (postfixTokens.ElementAt(i).Type == TokenType.Operator /*|| tokens.ElementAt(i).Type == TokenType.Function*/)
                {
                    string num1 = stack.Pop();
                    string num2 = stack.Pop();

                    switch (postfixTokens.ElementAt(i).Value)
                    {
                        case "+":
                            {
                                if (CheckInt(num2))
                                {
                                    stack.Push((Convert.ToInt32(num2) + Convert.ToInt32(num1)).ToString());
                                }
                                if (CheckStr(num2))
                                {
                                    stack.Push(ConcatStr(num2,num1));
                                }
                                break;
                            }
                        case "-":
                            {
                                if (CheckInt((string)num2))
                                {
                                    stack.Push((Convert.ToInt32(num2) - Convert.ToInt32(num1)).ToString());
                                }
                                if (CheckStr((string)num2))
                                {
                                    throw new Exception("Cant subtitution strings");
                                }
                                break;
                            }
                        case "*":
                            {
                                if (CheckInt((string)num2))
                                {
                                    stack.Push((Convert.ToInt32(num2) * Convert.ToInt32(num1)).ToString());
                                }
                                if (CheckStr((string)num2))
                                {
                                    throw new Exception("Cant * strings");
                                }
                                break;
                            }
                        case "/":
                            {
                                if (CheckInt((string)num2))
                                {
                                    if (num1 == "0")
                                    {
                                        throw new Exception("Ділення на 0");
                                    }
                                    stack.Push((Convert.ToInt32(num2) / Convert.ToInt32(num1)).ToString());
                                }
                                if (CheckStr((string)num2))
                                {
                                    throw new Exception("Cant / strings");
                                }

                                break;
                            }
                    }
                }
            }

            string res = stack.Pop();
            if (res[0] == '\"')
            {
                return res;
            }
            else if(Int32.TryParse(res,out int resInt))
            {
                return resInt;
            }
            else
            {
                return null;
            }
        }

        private List<Token> ToPostfix(List<Token> tokens)
        {
            List<Token> resultTokens = new List<Token>();

            Stack<Token> stack = new Stack<Token>();

            foreach (var tok in tokens)
            {
                if (tok.Type == TokenType.Constant || tok.Type == TokenType.Variable)
                {
                    resultTokens.Add(tok);
                }

                else if (tok.Value == "(")
                {
                    stack.Push(tok);
                }

                else if (tok.Value == ")")
                {
                    while (stack.Count > 0 && stack.Peek().Value != "(")
                    {
                        resultTokens.Add(stack.Pop());

                    }

                    if (stack.Count > 0 && stack.Peek().Value != "(")
                    {
                        throw new Exception("invalid expression");
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else 
                {
                    while (stack.Count > 0 && Priority(tok) <= Priority(stack.Peek()))
                    {
                        resultTokens.Add(stack.Pop());
                    }
                    stack.Push(tok);
                }
            }
            while (stack.Count > 0)
            {
                resultTokens.Add(stack.Pop());
            }

            return resultTokens;
        }
        private int Priority(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Operator:
                    {
                        switch (token.Value)
                        {
                            case "+":
                            case "-":
                                return 1;

                            case "*":
                            case "/":
                                return 2;

                            case "sin":
                                return 3;
                        }
                        break;

                    }
                case TokenType.Function:
                    {
                        return 3;
                    }

            }

            return -1;
        }
        private void CheckConstantsType(List<Token> tokens)
        {
            List<Token> constants = tokens.FindAll(x => x.Type == TokenType.Constant);
            int countOfStr = 0;
            int countOfInt = 0;
            for (int i = 0; i < constants.Count; i++)
            {
                if (constants[i].Value[0] == '\"')
                {
                    countOfStr++;
                }
                else
                {
                    countOfInt++;
                }

                if (countOfInt > 0 && countOfStr > 0)
                {
                    throw new Exception("Cantcalculate");
                }
            }
        }
        private void CheckOfFunctions(List<Token> tokens, Dictionary<string, object> valTable)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.Function)
                {

                    object result = FunctionService.CallFunction(tokens[i].Value, GetArgsFromFunction(tokens, valTable),null);

                    if (result.GetType() != typeof(string) && result.GetType() != typeof(int) && result.GetType() != typeof(String))
                    {
                        throw new Exception("Can't calculate list of objects");
                    }
                    else
                    {
                        Token newToken = new Token();
                        newToken.Type = TokenType.Constant;
                        newToken.Value = (string)result;

                        RemoveFunction(tokens[i].Value, tokens);
                        tokens[i] = newToken;
                    }
                }
            }
        }
        private List<object> GetArgsFromFunction(List<Token> tokens, Dictionary<string, object> valTable)
        {
            List<object> args = new List<object>();

            int indexOfFirstBracket = tokens.IndexOf(tokens.Find(x => x.Value == "("));
            int indexOfLastBracket = 0;
            for (int i = indexOfFirstBracket; i < tokens.Count; i++)
            {
                if (tokens[i].Value == ")")
                {
                    indexOfLastBracket = i;
                    break;
                }
            }

            for (int j = indexOfFirstBracket + 1; j < indexOfLastBracket; j++)
            {
                if (tokens[j].Type == TokenType.Variable)
                {
                    args.Add(valTable[tokens[j].Value]);
                }
                else
                {
                    args.Add(tokens[j].Value);
                }
            }

            return args;
        }
        private void RemoveFunction(string name, List<Token> tokens)
        {
            int indexOfFunc = tokens.FindIndex(x => x.Value == name);
            for (int i = indexOfFunc + 1; i < tokens.Count; i++)
            {
                if (tokens[i].Value != ")")
                {
                    tokens.Remove(tokens[i]);
                    if (i != 0) i--;
                }
                else
                {
                    tokens.Remove(tokens[i]);
                    break;
                }
            }
        }

        private bool CheckInt(string value)
        {
            int result;
            return Int32.TryParse(value, out result);
        }
        private bool CheckStr(string value)
        {
            if (value[0] == '\"')
                return true;
            return false;
        }

        private string ConcatStr(string str1, string str2)
        {
            string result = "";

            for (int i = 0; i < str1.Length - 1; i++)
            {
                result += str1[i];
            }

            for (int i = 0; i < str2.Length; i++)
            {
                if (str2[i] != '\"')
                {
                    result += str2[i];
                }
            }

            result += '\"';
            return result;
        }
    }
}
