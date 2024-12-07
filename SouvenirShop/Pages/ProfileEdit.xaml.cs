using Microsoft.Win32;
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
    /// Логика взаимодействия для ProfileEdit.xaml
    /// </summary>
    public partial class ProfileEdit : Page
    {
        User us;
        public ProfileEdit(User us1)
        {
            InitializeComponent();
            GenderSel.Items.Add("Мужской");
            GenderSel.Items.Add("Женский");
            RoleSel.Items.Add("Владелец");
            RoleSel.Items.Add("Админ");
            RoleSel.Items.Add("Клиент");
            us = us1;
            this.DataContext = us;
            setImage();
            GenderSel.SelectedItem = us.Gender;
            RoleSel.SelectedIndex = Convert.ToInt32(us.Role);
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

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить редактирование?", "Отмена редактирования", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                us.Photo = File.ReadAllBytes(openFileDialog.FileName);
                MemoryStream byteStream = new MemoryStream(us.Photo);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                UsImage.Source = image;
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            bool owned = false;
            var ow = ConnectionClass.connect.Users.Where(z => z.Role == 1).FirstOrDefault();
            if (ow != null)
            {
                owned = true;
            }
            while (true)
            {
                if (String.IsNullOrEmpty(LogTxt.Text))
                {
                    MessageBox.Show("Введите логин!");
                    return;
                }
                if (String.IsNullOrEmpty(PasTxt.Password))
                {
                    MessageBox.Show("Введите пароль!");
                    return;
                }
                if (String.IsNullOrEmpty(UserNameTxt.Text))
                {
                    MessageBox.Show("Введите имя пользователя!");
                    return;
                }
                if (String.IsNullOrEmpty(AgeTxt.Text))
                {
                    MessageBox.Show("Введите возраст!");
                    return;
                }
                if (GenderSel.SelectedItem == null)
                {
                    MessageBox.Show("Выберите ваш пол!");
                    return;
                }
                if (RoleSel.SelectedItem == null)
                {
                    MessageBox.Show("Выберите вашу роль!");
                    return;
                }
                if (Convert.ToInt32(AgeTxt.Text) < 6)
                {
                    MessageBox.Show("Для совершения покупок пользователю должно быть 6 и более лет!");
                    return;
                }
                if (owned && RoleSel.SelectedIndex == 0)
                {
                    MessageBox.Show("У нашей лавки уже есть владелец!");
                    return;
                }
                if (Convert.ToInt32(AgeTxt.Text) > 120)
                {
                    MessageBox.Show($"НИФИГА ТЫ {AgeTxt.Text} ЛЕТ ПРОЖИЛ! Песоооок....");
                    AgeTxt.Text = "120";
                    return;
                }
                us.Login = LogTxt.Text;
                us.Password = PasTxt.Password;
                us.Username = UserNameTxt.Text;
                us.Age = Convert.ToInt32(AgeTxt.Text);
                us.Gender = GenderSel.SelectedValue.ToString();
                us.Role = RoleSel.SelectedIndex + 1;
                ConnectionClass.connect.SaveChanges();
                MessageBox.Show($"Данные пользователя {us.Username} успешно сохранены!");
                NavigationService.Navigate(new MainPage(us));
                return;
            }
        }

        private void AgeTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

    }
}
