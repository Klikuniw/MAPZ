using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Reader
{
    public class CityReadingProxyService: IUser
    {
        List<City> cities;
        XmlService _xmlService;
        public CityReadingProxyService()
        {
            cities = new List<City>();
        }
        public City GetCity(Token token, string name)
        {
            City city = cities.FirstOrDefault(p => p.Name == name);
            if (city == null)
            {
                if (_xmlService == null)
                    _xmlService = new XmlService();
                city = _xmlService.GetCity(token, name);
                cities.Add(city);
            }
            return city;
        }

    }
}
