using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class Sword: Weapon
    {
        public override int Damage => 27;
        public override string Name => "Sword";
    }
}
