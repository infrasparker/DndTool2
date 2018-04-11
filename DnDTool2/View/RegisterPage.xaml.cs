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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage(Window window, string username, string password)
        {
            InitializeComponent();
            this.DataContext = new RegisterPageVM(window, username, password);
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterPageVM content = (RegisterPageVM)this.DataContext;
            NavigationService.Navigate(new LoginPage(content.window, content.Username, content.Password));
        }
    }
}
