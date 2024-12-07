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
using SouvenirShop.Model;

namespace SouvenirShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                string log = LoginTxt.Text;
                string pas = PassTxt.Password;
                if (string.IsNullOrEmpty(log))
                {
                    MessageBox.Show("Введите логин!");
                    return;
                }
                if (string.IsNullOrEmpty(pas))
                {
                    MessageBox.Show("Введите пароль!");
                    return;
                }
                var u = ConnectionClass.connect.Users.Where(z => z.Login == log && z.Password == pas).FirstOrDefault();
                if (u != null)
                {
                    MessageBox.Show("Добро пожаловать!");
                    NavigationService.Navigate(new MainPage(u));
                    return;
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
