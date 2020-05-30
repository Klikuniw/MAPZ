using System.Threading;

namespace BLL.LogInServices
{
    public class MultithreadedLogInService
    {
        
        private static volatile MultithreadedLogInService _instance = null;
        private static object Locker = new object();

        private MultithreadedLogInService()
        {
            
        }

        public static MultithreadedLogInService Instance
        {
            get
            {
                Thread.Sleep(500);
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new MultithreadedLogInService();
                        }
                    }
                }

                return _instance;
            }
        }

        public Token GetToken(string login, string password)
        {
            return new Token("asd");
        }

    }
}
