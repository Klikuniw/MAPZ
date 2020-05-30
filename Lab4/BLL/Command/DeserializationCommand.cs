using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BLL.Command
{
    public class DeserializationCommand : ICommand
    {
        private readonly Type _type;
        public DeserializationCommand(Type type)
        {
            _type = type;
        }
        public object Execute()
        {
            XmlSerializer formatter = new XmlSerializer(_type);
            object result;

            using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
            {
                result = formatter.Deserialize(fs);
            }

            return result;
        }
    }
}
