using System.Collections.Generic;
using BLL.DTO.Heroes;
using BLL.DTO.Interfaces;

namespace BLL.Weathers
{
    public class SunnyWeather: IWeather
    {
        public string Name => "Sunny Weather";
        public double Intensity { get; set; }
        public int DamageBoost => 10;
        public void ChangeHeroesStats(List<Hero> heroes)
        {
            foreach (Hero hero in heroes)
            {
                hero.Weapon.Damage += (int)(10 * Intensity);
            }
        }
    }
}