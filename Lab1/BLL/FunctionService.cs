using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BLL
{
    public static class FunctionService
    {
        public static object CallFunction(string name, List<object> paramList,TextBox textBox)
        {
            switch (name)
            {
                case "GetByTag":
                {
                    return Functions.GetByTag(paramList);
                }
                case "GetByAttr":
                {
                    return Functions.GetByAttr(paramList);
                }
                case "IntToStr":
                {
                    return Functions.IntToStr(paramList);
                }
                case "PrintList":
                {
                    return Functions.PrintList(paramList, textBox);
                }
                case "PrintVariable":
                {
                    return Functions.PrintVariable(paramList, textBox);
                }
                case "LoadPage":
                {
                    return Functions.LoadPage(paramList);
                }
            }

            return null;
        }
    }
}
