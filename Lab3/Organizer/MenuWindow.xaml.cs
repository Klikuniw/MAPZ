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

namespace Organizer
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();

            lbCities.ItemsSource = CurrentUser.Cities;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show((sender as City).Name.ToString());
        }

        private void LbCities_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CityWindow c = new CityWindow(((sender as ListBox).SelectedItem as City));
            c.ShowDialog();
            lbCities.ItemsSource = null;
            lbCities.ItemsSource = CurrentUser.Cities;
        }
    }
}
