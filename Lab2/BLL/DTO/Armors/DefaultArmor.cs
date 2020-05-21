using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DefaultArmor: Armor
    {
        public override int ArmorPoint { get; set; }
        public override string Name => "Default";

        public DefaultArmor(int armorPoint)
        {
            ArmorPoint = armorPoint;
        }
    }
}
