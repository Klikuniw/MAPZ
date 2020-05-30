using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using BLL.DTO.DefensiveConstructions;
using BLL.DTO.Heroes;
using BLL.DTO.Interfaces;
using BLL.DTO.Weapons;
using BLL.Levels;

namespace BLL.DTO
{
    [Serializable]
    public class City : INotifyPropertyChanged, IObserver
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DispatcherTimer _dispatcherTimerShowTime = new DispatcherTimer();

        public List<Building> Buildings { get; set; }
        public List<DefensiveConstruction> Constructions { get; set; }
        public List<Hero> Heroes { get; set; }
        public ObservableCollection<Hero> Enemies { get; set; }
        public Level Level { get; set; }
        public string Name { get; set; }

        public City(Level level)
        {
            Heroes = new List<Hero>();
            Constructions = new List<DefensiveConstruction>();
            Enemies = new ObservableCollection<Hero>();
            Level = level;

            InitializeTimer(new TimeSpan(0, 0, 0, 5));
        }
        public City()
        {
            Heroes = new List<Hero>();
            Constructions = new List<DefensiveConstruction>();
            Enemies = new ObservableCollection<Hero>();

            InitializeTimer(new TimeSpan(0,0,0,5));
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void InitializeTimer(TimeSpan time)
        {
            _dispatcherTimerShowTime.Tick += dispatcherTimer_Tick;
            _dispatcherTimerShowTime.Interval = time;
            _dispatcherTimerShowTime.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ObservableCollection<Hero> res = Level.GenerateEnemies(this);
            foreach (var t in res)
            {
                Enemies.Add(t);
            }

        }

        public void Update(object obj)
        {
            IWeather weather = (IWeather)obj;
            weather.ChangeHeroesStats(Heroes);
        }
    }
}
