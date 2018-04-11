using DnDTool2.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnDTool2.View
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage(Window window, string username, string password)
        {
            InitializeComponent();
            this.DataContext = new LoginPageVM(window, username, password);
        }

        public LoginPage(Window window) : this(window, "", "") { }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPageVM content = (LoginPageVM)this.DataContext;
            NavigationService.Navigate(new RegisterPage(content.window, content.Username, content.Password));
        }
    }
}
