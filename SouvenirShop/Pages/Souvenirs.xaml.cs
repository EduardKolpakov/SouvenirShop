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
    /// Логика взаимодействия для Souvenirs.xaml
    /// </summary>
    public partial class Souvenirs : Page
    {
        User us;
        public Souvenirs(User us1)
        {
            InitializeComponent();
            this.us = us1;
            LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.ToList();
            List<SouvenirsKind> souvs = ConnectionClass.connect.SouvenirsKinds.ToList();
            TypeSel.Items.Add("Всё");
            foreach (var s in souvs)
            {
                TypeSel.Items.Add(s.Name);
            }
            SortSel.Items.Add("Стандартная");
            SortSel.Items.Add("Название");
            SortSel.Items.Add("Цена");
            SortSel.Items.Add("Скидка");
            SortSel.SelectedIndex = 0;
            TypeSel.SelectedIndex = 0;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(us));
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            LvSouv.ItemsSource = null;
            string kind = TypeSel.SelectedItem.ToString();
            string sortby = SortSel.SelectedItem.ToString();
            if (kind == "Всё")
            {
                if (sortby == "Стандартная")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.ToList();
                }
                if (sortby == "Название")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.OrderBy(x => x.Name).ToList();
                }
                if (sortby == "Цена")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.OrderBy(x => x.Cost).ToList();
                }
                if (sortby == "Скидка")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.OrderBy(x => x.Sale).ToList();
                }
            }
            else
            {
                if (sortby == "Стандартная")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.Where(x => x.SouvenirsKind.Name == kind).ToList();
                }
                if (sortby == "Название")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.Where(x => x.SouvenirsKind.Name == kind).OrderBy(x => x.Name).ToList();
                }
                if (sortby == "Цена")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.Where(x => x.SouvenirsKind.Name == kind).OrderBy(x => x.Cost).ToList();
                }
                if (sortby == "Скидка")
                {
                    LvSouv.ItemsSource = ConnectionClass.connect.Souvenirs.Where(x => x.SouvenirsKind.Name == kind).OrderBy(x => x.Sale).ToList();
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SouvenirAddPage(us));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var s = LvSouv.SelectedItem as Souvenir;
            if (s != null)
            {
                NavigationService.Navigate(new SouvEditPage(s, us));
            }
            else
            {
                MessageBox.Show("Выберите сувенир!");
            }
        }

        private void BtnWarehouseClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WareHousePage(us));
        }
    }
}
