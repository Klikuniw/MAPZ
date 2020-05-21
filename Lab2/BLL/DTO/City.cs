using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.DTO.DefensiveConstructions;
using BLL.DTO.Interfaces;

namespace BLL.DTO
{
    public class City
    {
        public List<Building> Buildings { get; set; }
        public List<DefensiveConstruction> Constructions { get; set; }
        public List<Hero> Heroes { get; set; }
        
        public string Name { get; set; }

        public City()
        {
            Heroes = new List<Hero>();
            Constructions = new List<DefensiveConstruction>();
        }
    }
}
