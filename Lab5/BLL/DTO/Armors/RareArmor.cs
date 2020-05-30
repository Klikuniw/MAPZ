using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Armors;

namespace BLL.DTO
{
    [Serializable]
    public class RareArmor: Armor
    {
        public RareArmor(int armorPoints)
        {
            ArmorPoint = armorPoints;
        }
        public RareArmor()
        { }

        public override int ArmorPoint { get; set; }
        public override string Name => "Rare";
    }
}
