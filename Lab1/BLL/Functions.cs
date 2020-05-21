using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace BLL
{
    public static class Functions
    {
        
        public static List<string> GetByTag(List<object> paramList)
        {
            List<string> resultList = new List<string>();
            string html = paramList[0].ToString();
            string tag = paramList[1].ToString();

            var htmlParser = new HtmlParser();
            var document = htmlParser.ParseDocument(html);

            foreach (IElement element in document.QuerySelectorAll(tag.Replace("\"", string.Empty)))
            {
                resultList.Add(element.OuterHtml);
            }

            return resultList;
        }

        public static List<string> GetByAttr(List<object> paramList)
        {
            List<string> resultList = new List<string>();
            string html = paramList[0].ToString();
            string tag = paramList[1].ToString();
            string src = paramList[2].ToString();

            var htmlParser = new HtmlParser();
            var document = htmlParser.ParseDocument(html);

            foreach (IElement element in document.QuerySelectorAll(tag.Replace("\"", string.Empty)))
            {
                resultList.Add(element.GetAttribute(src.Replace("\"", string.Empty)));
            }

            return resultList;
        }

        public static string LoadPage(List<object> paramList)
        {
            string htmlDoc;

            using (WebClient client = new WebClient())
            {
                htmlDoc = client.DownloadString(paramList[0].ToString().Replace("\"", string.Empty));
            }

            return htmlDoc;
        }

        public static string LoadPage(string link)
        {
            string htmlDoc;

            using (WebClient client = new WebClient())
            {
                htmlDoc = client.DownloadString(link.ToString().Replace("\"", string.Empty));
            }

            return htmlDoc;
        }

        public static string IntToStr(List<object> paramList)
        {
            return $"\"{paramList[0]}\"".ToString();
        }
        public static object PrintList(List<object> paramList, TextBox textBox)
        {
            if (paramList[0].GetType() == typeof(List<string>))
            {
                for (int i = 0; i < (paramList[0] as List<string>).Count; i++)
                {
                    textBox.AppendText($"{(paramList[0] as List<string>)[i]}\n\n\n");
                }
            }

            return (paramList[0] as List<string>).Count;
        }
        public static object PrintVariable(List<object> paramList, TextBox textBox)
        {
            textBox.AppendText($"{paramList[0]}\n");
            return true;
        }

    }
}
