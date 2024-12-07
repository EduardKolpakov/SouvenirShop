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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        User us;
        public OrdersPage(User us)
        {
            InitializeComponent();
            this.us = us;
            LvSouv.ItemsSource = ConnectionClass.connect.Orders.Where(z => z.ClientID == us.ID).ToList();
        }

        private void refresh()
        {
            LvSouv.ItemsSource = null; 
            LvSouv.ItemsSource = ConnectionClass.connect.Orders.Where(z => z.ClientID == us.ID).ToList();
        }

        private void BtnPurchase_Click(object sender, RoutedEventArgs e)
        {
            var o = (sender as Button).DataContext as Order;
            if (MessageBox.Show("Вы уверены, что хотите отменить заказ?", "Отмена", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                o.Status = 4;
                Warehouse wh = ConnectionClass.connect.Warehouses.Where(z => z.SouvenirID == o.SouvenirID).FirstOrDefault();
                wh.Amount += o.Amount;
                ConnectionClass.connect.SaveChanges();
                refresh();
            }
        }
    }
}
