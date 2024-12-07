using SouvenirShop.Model;
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

namespace SouvenirShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для WareHousePage.xaml
    /// </summary>
    public partial class WareHousePage : Page
    {
        User us;
        public WareHousePage(User us1)
        {
            InitializeComponent();
            us = us1;
            LvSouv.ItemsSource = ConnectionClass.connect.Warehouses.ToList();
        }

        public void refresh()
        {
            LvSouv.ItemsSource = null;
            LvSouv.ItemsSource = ConnectionClass.connect.Warehouses.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var s = LvSouv.SelectedItem as Warehouse;
            s.Amount = Convert.ToInt32(AmountTxt.Text);
            ConnectionClass.connect.SaveChanges();
            refresh();
        }

        private void AmountTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void BtnSouvenirs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Souvenirs(us));
        }

        private void LvSouv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = LvSouv.SelectedItem as Warehouse;
            AmountTxt.Text = s.Amount.ToString();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(us));
        }
    }
}
