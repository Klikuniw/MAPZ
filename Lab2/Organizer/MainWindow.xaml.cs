using System.Windows;
using BLL;
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
            
            CurrentUser.Token = service.GetToken(Login.Text, Password.Password);
            CurrentUser.Initialize();


            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
    }
}
