using System;

namespace BLL.DTO.Weapons
{
    [Serializable]
    public class Sword: Weapon
    {
        private int _damage = 27;
        public override int Damage
        {
            get => _damage;
            set => _damage = value;
        }
        public override string Name => "Sword";
        public Sword() { }
    }
}
