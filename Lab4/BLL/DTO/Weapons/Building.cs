using System;
using System.Xml.Serialization;

namespace BLL.DTO.Weapons
{
    [Serializable]
    public class Building
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public Building()
        { }

    }
}
