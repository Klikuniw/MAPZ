using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Factories;

namespace BLL.DTO
{
    public class Hero
    {
        private Armor _armor;
        private Weapon _weapon;

        public Hero(HeroFactory factory, int armorPoint, int hp)
        {
            _weapon = factory.CreateWeapon();
            _armor = factory.CreateArmor(armorPoint);
            Hp = hp;
        }

        public int Armor
        {
            get => _armor.ArmorPoint;
            set => _armor.ArmorPoint = value;
        }
        public int Damage => _weapon.Damage;
        public int Hp { get; set; }
    }
}
