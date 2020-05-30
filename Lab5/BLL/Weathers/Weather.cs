using System.Collections.Generic;
using BLL.DTO.Interfaces;

namespace BLL.Weathers
{
    public class Weather: IObservable
    {
        private IWeather _weatherInfo;
        private List<IObserver> _observers;

        public Weather()
        {
            _observers = new List<IObserver>();
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (IObserver o in _observers)
            {
                o.Update(_weatherInfo);
            }
        }
       
        public void SetCurrentWeather(IWeather weather)
        {
            _weatherInfo = weather;
            NotifyObservers();
        }
        public string GetCurrentWeatherName()
        {
            return _weatherInfo.Name;
        }
    }
}