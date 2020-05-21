using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL
{
    public static class CurrentUser
    {
        public static Token Token { get; set; }
        public static List<City> Cities { get; set; }
        public static int Coins { get; set; }
        
        public static void Initialize()
        {
            XmlService service = new XmlService();
            Cities = service.ReadCities(Token);
            Coins = 100;
        }
    }
}
