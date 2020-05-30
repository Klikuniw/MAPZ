using System;
using System.Xml.Serialization;

namespace BLL.DTO.Weapons
{
    [Serializable]
    [XmlInclude(typeof(Sword))]
    [XmlInclude(typeof(Arbalet))]
    public abstract class Weapon
    {
        public abstract int Damage { get; set; }
        public abstract string Name { get; }
        public Weapon() { }
    }
}