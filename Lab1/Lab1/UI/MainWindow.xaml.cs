using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab1.LexerClasses;
using Lab1.ParserClasses;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CodeTextBox.Text = File.ReadAllText(@"..\..\progLang.txt");
            Browser.Navigate("http://vns.lpnu.ua/login/index.php");
        }
        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Browser.Navigate(txtUrl.Text);
        }

        private void wbSample_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            txtUrl.Text = e.Uri.OriginalString;
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((Browser != null) && (Browser.CanGoBack));
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Browser.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((Browser != null) && (Browser.CanGoForward));
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Browser.GoForward();
        }

        private void GoToPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoToPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Browser.Navigate(txtUrl.Text);
        }

		private void ButtonRun_OnClick(object sender, RoutedEventArgs e)
        {
            
            List<string> blocks = Lexer.ToBlocks(CodeTextBox.Text.Replace("\r",
                string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty));

            Dictionary<string, object> ValTable = new Dictionary<string, object>();

            IBlock head = Parser.GetTree(blocks, ValTable);
            Interpreter interpreter = new Interpreter();
            interpreter.Execute(head, ResultTextBox, ValTable);



        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
        }
    }
}
