using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.Interfaces;

namespace BLL.DTO.DefensiveConstructions
{
    [Serializable]
    public class Catapult: DefensiveConstruction
    {
        public override int Damage => 60;
        public override int Price => 10;
        public override string Name => "Catapult";

        public override DefensiveConstruction Clone()
        {
            return (DefensiveConstruction) this.MemberwiseClone();
        }

        public Catapult(int hp) : base(hp)
        {}
        
        public Catapult()
        {}
    }
}
