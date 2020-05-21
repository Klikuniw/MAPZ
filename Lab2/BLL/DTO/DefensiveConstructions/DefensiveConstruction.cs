using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.DefensiveConstructions
{
    public abstract class DefensiveConstruction
    {
        public abstract int Damage { get; }
        public int Hp { get; set; }
        public abstract int Price { get; }
        public abstract string Name { get; }
        public abstract DefensiveConstruction Clone();

        public DefensiveConstruction(int hp)
        {
            Hp = hp;
        }
    }
}
