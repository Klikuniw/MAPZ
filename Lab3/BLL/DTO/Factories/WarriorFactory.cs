using BLL.DTO;

namespace BLL.DTO.Factories
{
    public class WarriorFactory: HeroFactory
    {
        public override Armor CreateArmor(int armorPoints)
        {
            return new RareArmor(armorPoints);
        }

        public override Weapon CreateWeapon()
        {
            return new Sword();
        }
    }
}
