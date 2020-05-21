using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.LexerClasses
{
    public static class Lexer
    {
        public static string[] Separators = { " ", "+", ">", ",", "=", ";", "{", "}", "(", ")", "\"" };
        public static string[] Keywords = { "while", "for", "if", "html", "string", "int","listS" };
        public static string[] VariableTypes = { "html", "string", "int" };


        public static List<Token> Tokenize(List<string> blocks)
        {
            List<Token> tokens = new List<Token>();

            for (int i = 0; i < blocks.Count; i++)
            {
                /*if (IsRegularBlock(blocks[i]))
                {*/
                int sepIndex = MinSeparatorIndex(blocks[i]);
                /*switch (blocks[sepIndex])
                {
                    case "=":
                    {   
                        break;
                    }
                    case "==": { break; }
                    case ",": { break; }
                    case "(": { break; }
                    case ")": { break; }
                    case "+": { break; }
                }*/
                if (sepIndex == 0)
                {
                    //tokens.Add(new Token(){Id = });
                }


                /*}
                else
                {
                    
                }*/
            }

            return tokens;
        }

        public static List<Token> Tokenize(string text)
        {
            List<Token> tokens = new List<Token>();

            while (text.Length > 0)
            {
                int sepIndex = MinSeparatorIndex(text);
                if (sepIndex == Int32.MaxValue)
                {
                    tokens.Add(new Token()
                    {
                        Id = text,
                        Type = GetTokenType(text, text),
                        Value = text
                    });
                    text = "";
                }
                else if (sepIndex == 0 && text[sepIndex] != '\"')
                {
                    tokens.Add(new Token()
                    {
                        Id = text[sepIndex].ToString(),
                        Type = GetTokenTypeBySeparator(text[sepIndex].ToString()),
                        Value = text[sepIndex].ToString()
                    });

                    text = text.Substring(sepIndex + 1);
                }
                else if (text[sepIndex] == '\"')
                {
                    string token = "\"";
                    int k = 0;
                    for (int i = sepIndex + 1; i < text.Length; i++)
                    {
                        if (text[i] != '\"')
                        {
                            token += text[i];
                            k++;
                        }
                        else
                        {
                            k++;
                            token += "\"";
                            break;
                        }
                    }
                    tokens.Add(new Token()
                    {
                        Id = token,
                        Value = token,
                        Type = GetTokenType(token, text.Substring(0, k + 1))
                    });
                    text = text.Substring(k + 1);

                }
                else
                {
                    string token = text.Substring(0, sepIndex);

                    tokens.Add(new Token()
                    {
                        Id = token,
                        Value = token,
                        Type = GetTokenType(token, text.Substring(sepIndex))
                    });
                    text = text.Substring(sepIndex);
                }
            }


            return tokens;

        }
        
        private static TokenType GetTokenType(string token, string text)
        {
            if (IsKeyword(token)) return TokenType.Keyword;
            if (IsVariableType(token)) return TokenType.VariableType;
            if (IsConstant(token)) return TokenType.Constant;
            if (IsFunction(token, text)) return TokenType.Function;

            return TokenType.Variable;
        }
        private static TokenType GetTokenTypeBySeparator(string token)
        {
            switch (token)
            {
                case "=":
                    {
                        return TokenType.Operator;
                    }
                case ">":
                    {
                        return TokenType.Operator;
                    }
                case ",":
                    {
                        return TokenType.Comma;
                    }
                case "(": { return TokenType.Bracket; }
                case ")": { return TokenType.Bracket; }
                case "+": { return TokenType.Operator; }
                case ";": { return TokenType.EndTag; }
                case " ": { return TokenType.Space; }
                default: return TokenType.Unknown;
            }
        }

        private static bool IsRegularBlock(string text)
        {
            return !text.Contains("{") || !text.Contains("}");
        }
        public static int MinSeparatorIndex(string text)
        {
            int index = int.MaxValue;
            for (int i = 0; i < Separators.Length; i++)
            {
                int sepIndex = text.IndexOf(Separators[i]);
                if (sepIndex < index && sepIndex != -1)
                {
                    index = sepIndex;
                }
            }

            return index;
        }
        public static List<string> ToBlocks(string text)
        {
            List<string> blocks = new List<string>();

            string block = "";
            bool openBracket = false;
            int countOfOpenBracket = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{')
                {
                    openBracket = true;
                    block += text[i];
                    countOfOpenBracket++;
                }
                else if (openBracket && text[i] == '}' && countOfOpenBracket > 1)
                {
                    block += text[i];
                    countOfOpenBracket--;
                }
                else if (openBracket && text[i] == '}' && countOfOpenBracket == 1)
                {
                    block += "}";
                    blocks.Add(block.Trim('\n', '\t', '\r'));
                    block = "";
                    openBracket = false;
                    countOfOpenBracket = 0;
                }
                else if (text[i] != ';')
                {
                    block += text[i];
                }
                else if (text[i] == ';' && openBracket == false)
                {
                    block += ";";
                    blocks.Add(block.Trim('\n', '\t'));
                    block = "";
                }
                else if (text[i] == ';' && openBracket == true)
                {
                    block += text[i];
                }

            }

            return blocks;
        }

        private static bool IsKeyword(string token)
        {
            return Keywords.Contains(token);
        }
        private static bool IsVariableType(string token)
        {
            return VariableTypes.Contains(token);
        }
        private static bool IsConstant(string token)
        {
            int res;
            if (int.TryParse(token, out res))
            {
                return true;
            }
            if (token[0] == '"' && token[token.Length - 1] == '"')
            {
                return true;
            }

            return false;
        }
        private static bool IsFunction(string token, string text)
        {
            if (text[0] == '(')
            {
                int openBracket = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == '(')
                    {
                        openBracket++;
                    }

                    if (text[i] == ')')
                    {
                        openBracket--;
                        if (openBracket == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }




    }
}
