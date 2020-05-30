using System;

namespace BLL.DTO.Armors
{
    [Serializable]
    public class DefaultArmor: Armor
    {
        public override int ArmorPoint { get; set; }
        public override string Name => "Default";

        public DefaultArmor(int armorPoint)
        {
            ArmorPoint = armorPoint;
        }
        public DefaultArmor()
        {
        }
    }
}
