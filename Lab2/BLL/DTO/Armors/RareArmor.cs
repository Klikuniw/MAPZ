using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class RareArmor: Armor
    {
        public RareArmor(int armorPoints)
        {
            ArmorPoint = armorPoints;
        }

        public override int ArmorPoint { get; set; }
        public override string Name => "Rare";
    }
}
