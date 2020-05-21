using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.DefensiveConstructions
{
    public class UpgradeDamageConstruction: DefensiveConstructionDecorator
    {
        public UpgradeDamageConstruction(int hp, DefensiveConstruction defensiveConstruction) : base(hp, defensiveConstruction)
        {
        }

        public override int Damage => defensiveConstruction.Damage + 10;
        public override int Price => defensiveConstruction.Price;
        public override string Name => "Upgraded " + defensiveConstruction.Name ;
        public override DefensiveConstruction Clone()
        {
            return (DefensiveConstruction)this.MemberwiseClone();
        }
    }
}
