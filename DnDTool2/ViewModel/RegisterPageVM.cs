using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DnDTool2.ViewModel
{
    public class RegisterPageVM : ViewModel
    {
        private string username, password, confirm;
        public Window window;

        public string Username { get { return username; } set { this.username = value; OnPropertyChanged("Username"); } }
        public string Password { get { return password; } set { this.password = value; OnPropertyChanged("Password"); } }
        public string Confirm { get { return confirm; } set { this.confirm = value; OnPropertyChanged("Confirm"); } }

        public RegisterPageVM(Window window, string username, string password)
        {
            this.window = window;
            this.Username = username;
            this.Password = password;
            this.Confirm = "";
        }

        public RegisterPageVM(Window window) : this(window, "", "") { }
    }
}
