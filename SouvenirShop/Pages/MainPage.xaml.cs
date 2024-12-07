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
using Microsoft.Win32;
using SouvenirShop.Model;

namespace SouvenirShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        User us;
        public MainPage(User us1)
        {
            InitializeComponent();
            us = us1;
            setImage();
            this.DataContext = us;
            UserNameTxt.IsReadOnly = true;
            AgeTxt.IsReadOnly = true;
            GenderTxt.IsReadOnly = true;
            RoleTxt.IsReadOnly = true;
        }

        public void setImage()
        {
            MemoryStream byteStream = new MemoryStream(us.Photo);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = byteStream;
            image.EndInit();
            UsImage.Source = image;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfileEdit(us));
        }

        private void BtnSouvenirs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Souvenirs(us));
        }

        private void BtnWarehouses_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WareHousePage(us));
        }
    }
}
