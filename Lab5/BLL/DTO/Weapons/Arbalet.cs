using System;

namespace BLL.DTO.Weapons
{
    [Serializable]
    public class Arbalet : Weapon
    {
        private int _damage = 13;
        public override int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public override string Name => "Arbalet";
        public Arbalet() { }
    }
}
