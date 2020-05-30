using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Levels;

namespace BLL.DTO.Simulators
{
    public class CitySimulator
    {
        private HeroesSimulator _heroesSimulator;
        private ConstructionSimulator _constructionSimulator;
        private EnemiesSimulator _enemiesSimulator;

        public CitySimulator(HeroesSimulator heroesSimulator, ConstructionSimulator constructionSimulator, EnemiesSimulator enemiesSimulator)
        {
            _heroesSimulator = heroesSimulator;
            _constructionSimulator = constructionSimulator;
            _enemiesSimulator = enemiesSimulator;
        }

        public City CreateSimulation(int level)
        {
            City city = new City(new EasyLevel());
            city.Heroes = _heroesSimulator.SimulateHeroes(level);
            city.Constructions = _constructionSimulator.SimulateConstructions(level);

            return city;
        }
    }
}
