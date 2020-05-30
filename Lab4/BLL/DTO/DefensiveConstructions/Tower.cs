using System;
using BLL.DTO.Interfaces;

namespace BLL.DTO.DefensiveConstructions
{
    [Serializable]
    public class Tower: DefensiveConstruction
    {
        public override int Damage => 45;
        public override int Price => 7;
        public override string Name => "Tower";

        public override DefensiveConstruction Clone()
        {
            return (DefensiveConstruction)this.MemberwiseClone();
        }

        public Tower(int hp) : base(hp)
        {}
        public Tower()
        {}
    }
}
