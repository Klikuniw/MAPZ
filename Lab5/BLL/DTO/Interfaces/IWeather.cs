using System.Collections.Generic;
using BLL.DTO.Heroes;

namespace BLL.DTO.Interfaces
{
    public interface IWeather
    {
        string Name { get; }
        double Intensity { get; set; }
        int DamageBoost { get;}
        void ChangeHeroesStats(List<Hero> heroes);
    }
}