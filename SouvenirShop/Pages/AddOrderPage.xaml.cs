using SouvenirShop.Model;
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

namespace SouvenirShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        User us;
        Souvenir souv;
        decimal totalCost;
        Warehouse wh;
        public AddOrderPage(User us, Souvenir souv)
        {
            InitializeComponent();
            this.us = us;
            this.souv = souv;
            this.DataContext = souv;
            NameTxt.IsReadOnly = true;
            CostTxt.IsReadOnly = true;
            TypeTxt.IsReadOnly= true;
            SaleTxt.IsReadOnly = true;
            TotalCostTxt.IsReadOnly = true;
            wh = ConnectionClass.connect.Warehouses.Where(z => z.SouvenirID == souv.ID).FirstOrDefault();
            LeftOversTxt.Text = $"Остаток на складе: {wh.Amount}";
            setImage();
        }

        public void setImage()
        {
            MemoryStream byteStream = new MemoryStream(souv.Image);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = byteStream;
            image.EndInit();
            UsImage.Source = image;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(AmountTxt.Text))
                {
                    MessageBox.Show("Пожалуйства, укажите количество сувениров!");
                    return;
                }
                int amount = Convert.ToInt32(AmountTxt.Text);
                wh.Amount -= amount;
                Order ord = new Order();
                ord.ID = souv.ID;
                ord.ClientID = us.ID;
                ord.TotalCost = totalCost;
                ord.Amount = amount;
                ord.StartDate = DateTime.Now.Date;
                ord.SouvenirID = souv.ID;
                ord.Status = 1;
                ConnectionClass.connect.Orders.Add(ord);
                ConnectionClass.connect.SaveChanges();
                MessageBox.Show("Заказ успешно создан!");
                NavigationService.Navigate(new Souvenirs(us));
                return;
            }
        }

        private void AmountTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amount = Convert.ToInt32(AmountTxt.Text);
            int available = 0;
            if (wh.Amount != null)
            {
                available = Convert.ToInt32(wh.Amount);
            }
            decimal cost = Convert.ToDecimal(souv.Cost);
            int sale = Convert.ToInt32(souv.Sale);
            if (amount > available)
            {
                MessageBox.Show($"На складе недостаточно товара!\nОстаток: {available}");
                return;
            }
            totalCost = Math.Round(cost * amount * Convert.ToDecimal(1.0 - sale/100), 2);
            TotalCostTxt.Text = totalCost.ToString();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить заказ?", "Отмена", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void SaleTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
    }
}
