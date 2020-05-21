using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.LexerClasses
{
    public enum TokenType
    {
        VariableType,
        Function,// func("...")
        Operator,// +  = == !=
        Keyword,
        Constant,// 10 "abc"
        Comment,//      //abc /*abc*/
        Bracket,// { } ( )
        Comma,// ,
        EndTag,
        Unknown,
        Space,
        Variable
    }
}
