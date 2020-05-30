namespace BLL.DTO.Interfaces
{
    public interface IDefensiveConstruction
    {
        int Damage { get; }
        int Hp { get; set; }
        int Price { get;}
        string Name { get; }

        IDefensiveConstruction Clone();

    }
}
