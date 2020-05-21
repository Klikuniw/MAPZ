using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Lab1.LexerClasses;

namespace Lab1.ParserClasses
{
    public static class Parser
    {
        public static IBlock GetTree(List<string> blocks, Dictionary<string, object> valTable)
        {
            IBlock head = GetBlock(blocks, 0, valTable);

            IBlock head2 = head.NextBlock;
            while (head2.NextBlock.NextBlock.Tokens != null)
            {
                head2 = head2.NextBlock;
            }
            head2.NextBlock.NextBlock = head2.GetNextOnOwnLevel();
            head2 = head.NextBlock;
            while (head2.NextBlock.NextBlock.Tokens != null)
            {
                head2 = head2.NextBlock;
            }
            head2.NextBlock.NextBlock = head2.GetNextOnOwnLevel();
            return head;
        }

        private static IBlock GetBlock(List<string> blocks, int pos, Dictionary<string, object> valTable)
        {
            if (pos != blocks.Count)
            {
                if (IsConditionalBlock(blocks[pos]))
                {
                    return ParseConditionalBlock(blocks[pos], blocks, pos, valTable);
                }

                if (IsLoopBlock(blocks[pos]))
                {
                    return ParseLoopBlock(blocks[pos], blocks, pos, valTable);
                }

                if (IsFunctionBlock(blocks[pos]))
                {
                    return ParseFunctionBlock(blocks[pos], blocks, pos, valTable);
                }

                if (IsAssignmentBlock(blocks[pos]))
                {
                    return ParseAssignmentBlock(blocks[pos], blocks, pos, valTable);
                }

                return new EndBlock();
            }

            return new EndBlock();
        }

        private static bool IsConditionalBlock(string block)
        {
            List<Token> tokens = Lexer.Tokenize(block);
            if (tokens[0].Type == TokenType.Keyword && tokens[0].Value == "if")
            {
                return true;
            }

            return false;

        }
        private static bool IsLoopBlock(string block)
        {
            if (block.Contains('('))
            {
                string word = block.Substring(0, block.IndexOf('('));
                if (word == "loop") return true;
                return false;
            }
            else
            {
                return false;
            }
        }
        private static bool IsAssignmentBlock(string block)
        {
            return LexerClasses.Lexer.Tokenize(block).Exists(x => x.Value == "=");
        }
        private static bool IsFunctionBlock(string block)
        {
            List<Token> tokens = Lexer.Tokenize(block);
            if (tokens.Exists(x => x.Type == TokenType.Function) && !tokens.Exists(x => x.Value == "="))
            {
                return true;
            }
            return false;
            //return LexerClasses.Lexer.Tokenize(block).Exists(x => x.Type == TokenType.Function);
        }

        private static FunctionBlock ParseFunctionBlock(string block, List<string> blocks, int pos,Dictionary<string,object> valTable)
        {
            List<Token> tokens = Lexer.Tokenize(block);

            if (!tokens.Exists(x=>x.Type==TokenType.Constant)&& !tokens.Exists(x => x.Type == TokenType.Variable))
            {
                return new FunctionBlock()
                {
                    NextBlock = GetBlock(blocks, pos + 1,valTable),
                    Name = tokens.First(x => x.Type == TokenType.Function).Value,
                    Arguments = null,
                    Tokens = tokens
                };
            }
            else
            {
                FunctionBlock function = new FunctionBlock();
                function.NextBlock = GetBlock(blocks, pos + 1, valTable);
                function.Name = tokens.First(x => x.Type == TokenType.Function).Value;
                function.Tokens = tokens;
                /*function.Arguments = new List<object>();
                
                for (
                    int i = tokens.IndexOf(tokens.Find(x=>x.Value=="(")) + 1;
                    i < tokens.IndexOf(tokens.FindLast(x => x.Value == ")"));
                    i++)
                {
                    if (tokens[i].Type != TokenType.Comma)
                    {
                        function.Arguments.Add(tokens[i].Value);
                    }
                }*/

                return function;
            }
            
        }
        private static AssignmentBlock ParseAssignmentBlock(string block, List<string> blocks, int pos, Dictionary<string, object> valTable)
        {
            List<Token> tokens = Lexer.Tokenize(block);
            Expression expression = new Expression();
            expression.Tokens =
                Lexer.Tokenize(block.Substring(block.IndexOf('=') + 1, block.Length - block.IndexOf('=') - 1));
            

            return new AssignmentBlock()
            {
                NextBlock = GetBlock(blocks, pos + 1, valTable),
                Tokens = tokens,
                Expression = expression,
                Variable = block.Substring(0, block.IndexOf('='))
            };
        }
        private static ConditionalBlock ParseConditionalBlock(string block, List<string> blocks, int pos, Dictionary<string, object> valTable)
        {
            List<Token> tokens = Lexer.Tokenize(block);
            ConditionalBlock conditionalBlock = new ConditionalBlock();

            conditionalBlock.Conditional = block.Replace("\r",
                    string.Empty).Replace("\n", string.Empty)
                .Substring(block.IndexOf('(') + 1, block.IndexOf(')') - block.IndexOf('(') - 1);

            conditionalBlock.Tokens = tokens;

            conditionalBlock.Negative = GetBlock(blocks, pos + 1, valTable);
            conditionalBlock.Positive = GetBlock(GetPositiveBlocks(block), 0, valTable);

            conditionalBlock.LeftExpression = new Expression(){ Tokens = Lexer.Tokenize(GetLeftExpression(conditionalBlock.Conditional))};
            conditionalBlock.RightExpression = new Expression() { Tokens = Lexer.Tokenize(GetRightExpression(conditionalBlock.Conditional))};
            
            IBlock head = conditionalBlock.Positive;
            while (head.GetNextOnOwnLevel().Tokens != null)
            {
                head = head.GetNextOnOwnLevel();
            }
            head.SetNextOnOwnLevel(conditionalBlock.Negative);

            return conditionalBlock;

        }
        private static LoopBlock ParseLoopBlock(string block, List<string> blocks, int pos, Dictionary<string, object> valTable)
        {
            string loopBody = block.Substring(0, block.IndexOf('{'));
            List<Token> tokens = Lexer.Tokenize(loopBody);

            #region Find index of num
            int from = tokens.IndexOf(tokens.Find(x => x.Type == TokenType.Bracket)) + 1;
            int to = tokens.LastIndexOf(tokens.FindLast(x => x.Type == TokenType.Comma)) - 1;
            int step = tokens.LastIndexOf(tokens.FindLast(x => x.Type == TokenType.Comma)) + 1;
            #endregion

            #region init loopblock

            LoopBlock loopBlock = new LoopBlock(Convert.ToInt32(tokens[@from].Value), Convert.ToInt32(tokens[@to].Value))
            {
                Negative = GetBlock(blocks, pos + 1, valTable),
                Positive = GetBlock(GetPositiveBlocks(block), 0, valTable),
                Tokens = tokens,
                From = Convert.ToInt32(tokens[@from].Value),
                To = Convert.ToInt32(tokens[@to].Value),
                Step = Convert.ToInt32(tokens[@step].Value)
            };

            #endregion


            IBlock head = loopBlock.Positive;
            while (head.GetNextOnOwnLevel().Tokens!=null)
            {
                head = head.GetNextOnOwnLevel();
            }
            head.SetNextOnOwnLevel(loopBlock);

            //head. = loopBlock;

            return loopBlock;
        }

        private static List<string> GetPositiveBlocks(string block)
        {
            string subBlock = "";

            for (int i = block.IndexOf('{') + 1; i < block.LastIndexOf('}'); i++)
            {
                subBlock += block[i];
            }

            return Lexer.ToBlocks(subBlock);
        }

        private static string GetLeftExpression(string conditional)
        {
            string result = "";

            for (int i = 0; i < conditional.Length; i++)
            {
                if (conditional[i] != '>' && conditional[i] != '<')
                {
                    result += conditional[i];
                    
                }
                else
                {
                    break;
                }
            }

            return result;
        }
        private static string GetRightExpression(string conditional)
        {
            string result = "";
            int indexOfCondition = 0;

            for (int i = 0; i < conditional.Length; i++)
            {
                if (conditional[i] != '>' && conditional[i] != '<')
                {
                    indexOfCondition = i;
                }
                else
                {
                    indexOfCondition = i;
                    break;
                }
            }
            for (int i = indexOfCondition + 1; i < conditional.Length; i++)
            {
                result += conditional[i];
            }
            return result;
        }
    }
}
