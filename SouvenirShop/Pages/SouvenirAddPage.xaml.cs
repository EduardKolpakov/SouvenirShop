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
    /// Логика взаимодействия для SouvenirAddPage.xaml
    /// </summary>
    public partial class SouvenirAddPage : Page
    {
        User us;
        Souvenir souv;
        public SouvenirAddPage(User us1)
        {
            InitializeComponent();
            us = us1;
            List<SouvenirsKind> souvs = ConnectionClass.connect.SouvenirsKinds.ToList();
            foreach (var s in souvs)
            {
                SouvTypeSel.Items.Add(s.Name);
            }
            souv = new Souvenir();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить добавление?", "отмена", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void CostTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void SaleTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                souv.Image = File.ReadAllBytes(openFileDialog.FileName);
                MemoryStream byteStream = new MemoryStream(souv.Image);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                UsImage.Source = image;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(NameTxt.Text))
                {
                    MessageBox.Show("Введите название сувенира!");
                    return;
                }
                if (string.IsNullOrEmpty(CostTxt.Text))
                {
                    MessageBox.Show("Введите цену сувенира!");
                    return;
                }
                if (SouvTypeSel.SelectedItem == null)
                {
                    MessageBox.Show("Выберите тип сувенира!");
                    return;
                }
                if (string.IsNullOrEmpty(SaleTxt.Text))
                {
                    MessageBox.Show("Введите скидку на сувенир!");
                    return;
                }
                souv.Name = NameTxt.Text;
                souv.Cost = Convert.ToInt32(CostTxt.Text);
                souv.Type = SouvTypeSel.SelectedIndex + 1;
                souv.Sale = Convert.ToInt32(SaleTxt.Text);
                ConnectionClass.connect.Souvenirs.Add(souv);
                Warehouse wh = new Warehouse();
                wh.SouvenirID = souv.ID;
                wh.Amount = 0;
                ConnectionClass.connect.Warehouses.Add(wh);
                ConnectionClass.connect.SaveChanges();
                MessageBox.Show("Сувенир успешно добавлен");
                NavigationService.Navigate(new Souvenirs(us));
                return;
            }
        }
    }
}
