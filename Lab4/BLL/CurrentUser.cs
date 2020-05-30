using System;
using System.Collections.Generic;
using BLL.Command;
using BLL.DTO;
using BLL.Weathers;

namespace BLL
{
    public class CurrentUser
    {
        private readonly Server _server;

        public Token Token { get; set; }
        public List<City> Cities { get; set; }
        public int Coins { get; set; }
        public Weather Weather { get; set; }


        public CurrentUser()
        {
            Cities = new List<City>();
            Token = new Token();
            _server = new Server();

            Weather = new Weather();
            RandomizeCurrentWeather();
        }

        public UserMemento SaveState()
        {
            return new UserMemento(Cities, Coins);
        }
        public void RestoreState(UserMemento memento)
        {
            Cities.AddRange(memento.Cities);
            Coins = memento.Coins;
        }


        public void SetServerCommand(ICommand command)
        {
            _server.SetCommand(command);
        }
        public object RunServerCommand()
        {
            return _server.Run();
        }

        public void RandomizeCurrentWeather()
        {
            Random random = new Random();
            double intensity = random.Next(0,10)/10.0;
            switch (random.Next(0, 2))
            {
                case 0:
                {
                    Weather.SetCurrentWeather(new SunnyWeather(){ Intensity = intensity });
                    break;
                }
                case 1:
                {
                    Weather.SetCurrentWeather(new FoggyWeather() { Intensity = intensity });
                    break;
                }
            }

        }
    }
}
