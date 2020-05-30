using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL
{
    
    public class UserMemento
    {
        public List<City> Cities { get; set; }
        public int Coins { get; set; }

        public UserMemento(List<City> cities, int coins)
        {
            Cities = new List<City>();
            Cities.AddRange(cities);
            Coins = coins;
        }
        public UserMemento()
        { }
    }
}
