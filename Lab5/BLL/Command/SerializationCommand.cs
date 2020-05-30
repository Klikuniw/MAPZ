using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BLL.Command
{
    public class SerializationCommand : ICommand
    {
        private readonly Type _type;
        private object _object;

        public SerializationCommand(Type type, object _object)
        {
            _type = type;
            this._object = _object;
        }
        public object Execute()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(CurrentUser));
            using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, _object);
            }

            return true;
        }
    }
}
