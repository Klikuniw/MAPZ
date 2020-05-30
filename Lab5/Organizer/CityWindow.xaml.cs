using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLL;
using BLL.DTO;
using BLL.DTO.DefensiveConstructions;
using BLL.DTO.Interfaces;
using BLL.DTO.Weapons;

namespace Organizer
{
    /// <summary>
    /// Interaction logic for CityWindow.xaml
    /// </summary>
    public partial class CityWindow : Window
    {
        private readonly City city = null;
        private double i = 1.0;
        private readonly CurrentUser _currentUser;

        public CityWindow(City city, CurrentUser user)
        {
            InitializeComponent();

            this.city = city;

            _currentUser = user;

            lbBuildings.ItemsSource = city.Buildings;
            CityName.Text = city.Name;
            CoinsBlock.Text = _currentUser.Coins.ToString();
            lbArmy.ItemsSource = city.Heroes;
            lbConstructions.ItemsSource = city.Constructions;
            lbEnemies.ItemsSource = city.Enemies;
        }

        private void Finish_OnClick(object sender, RoutedEventArgs e)
        {
            city.Buildings.Remove((lbBuildings.SelectedItem as Building));
            _currentUser.Cities.Find(x=>x.Name == city.Name).Buildings.Remove((lbBuildings.SelectedItem as Building));
            lbBuildings.ItemsSource = null;
            lbBuildings.ItemsSource = city.Buildings;

            _currentUser.Coins += 20;
            CoinsBlock.Text = _currentUser.Coins.ToString();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            Building b = new Building();
            b.Text = "1";
            b.Time = DateTime.Now.AddHours(i);
            i++;
            city.Buildings.Add(b);
            lbBuildings.ItemsSource = null;
            lbBuildings.ItemsSource = city.Buildings;
        }

        private void BuyTower_OnClick(object sender, RoutedEventArgs e)
        {
            DefensiveConstruction construction = new Tower(100);
            city.Constructions.Add(construction);
            for (int j = 0; j < 4; j++)
            {
                city.Constructions.Add(construction.Clone());
            }

            lbConstructions.ItemsSource = null;
            lbConstructions.ItemsSource = city.Constructions;
        }

        private void BuyCatapult_OnClick(object sender, RoutedEventArgs e)
        {
            DefensiveConstruction construction = new Catapult(100);

            city.Constructions.Add(construction);
            for (int j = 0; j < 4; j++)
            {
                city.Constructions.Add(construction.Clone());
            }

            lbConstructions.ItemsSource = null;
            lbConstructions.ItemsSource = city.Constructions;
        }
    }
}
