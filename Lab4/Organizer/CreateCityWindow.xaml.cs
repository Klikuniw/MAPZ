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
using BLL.Levels;

namespace Organizer
{
    /// <summary>
    /// Interaction logic for CreateCityWindow.xaml
    /// </summary>
    public partial class CreateCityWindow : Window
    {
        private CurrentUser _user;
        public CreateCityWindow(CurrentUser user)
        {
            _user = user;
            InitializeComponent();
        }

        private void AddCityButton_OnClick(object sender, RoutedEventArgs e)
        {
            City city = new City();
            switch (Level.SelectedIndex)
            {
                case 0:
                {
                    city = new City(new EasyLevel());
                    break;
                }
                case 1:
                {
                    city = new City(new DefaultLevel());
                    break;
                }
                case 2:
                {
                    city = new City(new HardLevel());
                    break;
                }
            }
            city.Name = CityName.Text;
            _user.Cities.Add(city);
            _user.Weather.AddObserver(city);
            _user.Weather.NotifyObservers();
            this.Close();
        }
    }
}

