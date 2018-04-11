using DnDTool2.Model;
using DnDTool2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DnDTool2.ViewModel
{
    public class LoginPageVM : ViewModel
    {
        public Window window;
        private string username, password;

        public RelayCommand OpenMenuCommand { get; set; }

        public string Username { get { return username; } set { this.username = value; OnPropertyChanged("Username"); } }
        public string Password { get { return password; } set { this.password = value; OnPropertyChanged("Password"); } }

        public LoginPageVM(Window window, string username, string password)
        {
            this.window = window;
            this.Username = username;
            this.Password = password;
            this.OpenMenuCommand = new RelayCommand(OpenMenu);
        }

        private void OpenMenu(object parameter)
        {
            (new MenuWindow()).Show();
            window.Close();
        }
    }
}
