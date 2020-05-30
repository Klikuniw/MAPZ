using BLL.DTO.Armors;
using BLL.DTO.Weapons;

namespace BLL.DTO.Factories
{
    public class ElfFactory: HeroFactory
    {
        public override Armor CreateArmor(int armorPoints)
        {
            return new DefaultArmor(armorPoints);
        }

        public override Weapon CreateWeapon()
        {
            return new Arbalet();
        }
    }
}
