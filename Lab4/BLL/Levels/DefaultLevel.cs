using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BLL.DTO;
using BLL.DTO.Factories;
using BLL.DTO.Heroes;

namespace BLL.Levels
{
    [Serializable]
    public class DefaultLevel : Level
    {
        public DefaultLevel() { }
        public override ObservableCollection<Hero> GenerateEnemies(City city)
        {
            ObservableCollection<Hero> enemies = new ObservableCollection<Hero>();
            for (int i = 0; i < city.Heroes.Count; i++)
            {
                enemies.Add(new Hero(new ElfFactory(), 100, 100));
            }

            return enemies;
        }
    }
}