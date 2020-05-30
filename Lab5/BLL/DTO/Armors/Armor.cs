using System;
using System.Xml.Serialization;

namespace BLL.DTO.Armors
{
    [Serializable]
    [XmlInclude(typeof(DefaultArmor))]
    [XmlInclude(typeof(RareArmor))]
    public abstract class Armor
    {
        public abstract int ArmorPoint { get; set; }
        public abstract string Name { get;}
        public Armor() { }
    }
}