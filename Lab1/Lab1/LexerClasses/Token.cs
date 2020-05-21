using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.LexerClasses
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public string Id { get; set; }
    }
}
