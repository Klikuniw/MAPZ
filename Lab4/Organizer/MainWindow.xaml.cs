using System.IO;
using System.Windows;
using System.Xml.Serialization;
using BLL;
using BLL.Command;
using BLL.DTO;
using BLL.LogInServices;

namespace Organizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            MultithreadedLogInService service = MultithreadedLogInService.Instance;

            CurrentUser currentUser = new CurrentUser();
            currentUser.SetServerCommand(new DeserializationCommand(typeof(CurrentUser)));
            
            object o = currentUser.RunServerCommand();
            currentUser.Cities = (o as CurrentUser).Cities;
            currentUser.Coins = (o as CurrentUser).Coins;

            foreach (City city in currentUser.Cities)
            {
                currentUser.Weather.AddObserver(city);
            }
            currentUser.Weather.NotifyObservers();


            MenuWindow menuWindow = new MenuWindow(currentUser);
            menuWindow.Show();
            this.Close();
        }
        
    }
}
