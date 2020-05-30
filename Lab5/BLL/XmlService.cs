using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BLL.DTO;
using BLL.DTO.Factories;
using BLL.DTO.Heroes;
using BLL.DTO.Weapons;
using BLL.Levels;

namespace BLL
{
    public class XmlService
    {
        public List<City> ReadCities(Token token)
        {
            List<City> cities = new List<City>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"..\..\UserInfo.xml");
            
            foreach (XmlNode xmlNode in xDoc.DocumentElement)
            {
                City c = new City(new EasyLevel());
                c.Buildings = new List<Building>();
                if (xmlNode.Name == "city")
                {
                    c.Name = xmlNode.Attributes["name"].Value;
                    foreach (XmlElement xmlElement in xmlNode.ChildNodes)
                    {
                        if (xmlElement.Name == "buildings")
                        {
                            foreach (XmlElement xmlElementBuilding in xmlElement.ChildNodes)
                            {
                                c.Buildings.Add(new Building()
                                {
                                    Text = xmlElementBuilding.Attributes["text"].Value,
                                    Time = Convert.ToDateTime(xmlElementBuilding.Attributes["deadline"].Value),
                                });
                            }

                        }

                        if (xmlElement.Name == "army")
                        {
                            foreach (XmlElement xmlElementHero in xmlElement.ChildNodes)
                            {
                                int armorPoint = Convert.ToInt32(xmlElementHero.Attributes["armor"].Value);
                                int hp = Convert.ToInt32(xmlElementHero.Attributes["hp"].Value);

                                if (xmlElementHero.Name == "elf")
                                {
                                    c.Heroes.Add(new Hero(new ElfFactory(), armorPoint, hp));
                                }
                                if (xmlElementHero.Name == "warrior")
                                {
                                    c.Heroes.Add(new Hero(new WarriorFactory(), armorPoint, hp));
                                }

                            }
                        }

                    }
                    cities.Add(c);
                }
                
            }

            return cities;
        }

        public City GetCity(Token token, string name)
        {
            return new City(new EasyLevel());
        }
    }
}
