using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BLL.DTO;
using BLL.DTO.Heroes;

namespace BLL.Levels
{
    [Serializable]
    [XmlInclude(typeof(EasyLevel))]
    [XmlInclude(typeof(DefaultLevel))]
    [XmlInclude(typeof(HardLevel))]
    public abstract class Level
    {
        public abstract ObservableCollection<Hero> GenerateEnemies(City city);
        public Level() { }
    }
    
}
