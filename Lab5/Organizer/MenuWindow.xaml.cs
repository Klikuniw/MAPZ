using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using BLL;
using BLL.Command;
using BLL.DTO;

namespace Organizer
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private readonly CurrentUser _currentUser;
        private Stack<UserMemento> userHistory = new Stack<UserMemento>();

        public MenuWindow(CurrentUser user)
        {
            InitializeComponent();

            userHistory.Push(user.SaveState());
            _currentUser = user;
            lbCities.ItemsSource = user.Cities;
            CurrentWeatherName.Text = _currentUser.Weather.GetCurrentWeatherName();
        }

        private void LbCities_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CityWindow c = new CityWindow(((sender as ListBox).SelectedItem as City),_currentUser);
            c.ShowDialog();

            userHistory.Push(_currentUser.SaveState());
            lbCities.ItemsSource = null;
            lbCities.ItemsSource = _currentUser.Cities;
        }

        private void MenuWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _currentUser.SetServerCommand(new SerializationCommand(typeof(CurrentUser),_currentUser));
            _currentUser.RunServerCommand();
        }
        private void CreateCityButton_OnClick(object sender, RoutedEventArgs e)
        {
            CreateCityWindow window = new CreateCityWindow(_currentUser);
            window.ShowDialog();

            lbCities.ItemsSource = null;
            lbCities.ItemsSource = _currentUser.Cities;

        }
    }
}
