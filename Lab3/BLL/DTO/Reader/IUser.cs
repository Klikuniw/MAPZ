using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Reader
{
    public interface IUser
    {
        City GetCity(Token token, string name);
    }
}
