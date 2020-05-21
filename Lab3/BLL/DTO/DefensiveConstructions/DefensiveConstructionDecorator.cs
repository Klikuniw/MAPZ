namespace BLL.DTO.DefensiveConstructions
{
    public abstract class DefensiveConstructionDecorator: DefensiveConstruction
    {
        protected DefensiveConstruction defensiveConstruction;
        public DefensiveConstructionDecorator(int hp, DefensiveConstruction defensiveConstruction) : base(hp)
        {
            this.defensiveConstruction = defensiveConstruction;
        }
    }
}
