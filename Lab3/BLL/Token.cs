using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Token
    {
        public string Data { get; set; }
        public TimeSpan Time { get; set; }


        public Token(string data)
        {
            Data = data;
        }
    }
}
