using System;

namespace BLL.LogInServices
{
    public class LazyLogInService
    {
        static Lazy<LazyLogInService> instance = new Lazy<LazyLogInService>();
        public static LazyLogInService Instance => instance.Value;


        public Token GetToken(string login, string password)
        {
            return new Token("asd");
        }
    }
}
