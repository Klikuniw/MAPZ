using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Reader
{
    public class CityReadingService: IUser
    {
        private XmlService xmlService;

        public CityReadingService()
        {
            xmlService = new XmlService();
        }
        public City GetCity(Token token, string name)
        {
            return xmlService.GetCity(token, name);
        }
    }
}
