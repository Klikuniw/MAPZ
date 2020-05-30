using System.Collections.Generic;
using BLL.DTO.Heroes;
using BLL.DTO.Interfaces;

namespace BLL.Weathers
{
    public class FoggyWeather: IWeather
    {
        public string Name => "Foggy Weather";
        public double Intensity { get; set; }
        public int DamageBoost => 7;
        public void ChangeHeroesStats(List<Hero> heroes)
        {
            foreach (Hero hero in heroes)
            {
                hero.Weapon.Damage -= (int)(10 * Intensity);
            }
        }
    }
}