using System;
using BLL.DTO.Armors;
using BLL.DTO.Factories;
using BLL.DTO.Weapons;

namespace BLL.DTO.Heroes
{
    [Serializable]
    public class Hero
    {
        public Armor Armor { get; set; }
        public Weapon Weapon { get; set; }

        public Hero()
        {

        }
       
        public Hero(HeroFactory factory, int armorPoint, int hp)
        {
            Weapon = factory.CreateWeapon();
            Armor = factory.CreateArmor(armorPoint);
            Hp = hp;
        }
        
        public int Hp { get; set; }

    }
}
