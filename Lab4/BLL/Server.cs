using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Command;

namespace BLL
{
    public class Server
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public object Run()
        {
            return _command.Execute();
        }
    }
}
