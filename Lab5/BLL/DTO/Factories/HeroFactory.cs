﻿using BLL.DTO.Armors;
using BLL.DTO.Weapons;

namespace BLL.DTO.Factories
{
    public abstract class HeroFactory
    {
        public abstract Armor CreateArmor(int armorPoints);
        public abstract Weapon CreateWeapon();
    }
}
