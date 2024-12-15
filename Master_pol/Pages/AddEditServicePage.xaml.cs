using Master_pol.Entities;
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

namespace Master_pol.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        private Entities.Partners _currentPartenres = null;
        public AddEditServicePage()
        {
            InitializeComponent();
            NavigationService.GoBack();
            NavigationService.Navigate(new AddEditServicePage());
        }
        public AddEditServicePage(Entities.Partners partners)
        {
            InitializeComponent();

            _currentPartenres = partners; 
            Title = "Редактировать партнера";

            TboxTip.Text = _currentPartenres.type_partner;
            TboxName.Text = _currentPartenres.name;
            TboxPerson.Text = _currentPartenres.contact_person;
            TboxPhone.Text = _currentPartenres.phonne.ToString();

            if (_currentPartenres.reiting >= 0)
                TboxReiting.Text = _currentPartenres.reiting.ToString(); 
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK);
            }
            else
            {
                if (_currentPartenres == null)
                {
                    var partners = new Entities.Partners()
                    {
                        type_partner = TboxTip.Text,
                        name = TboxName.Text,
                        contact_person = TboxPerson.Text,
                        phonne = long.Parse(TboxPhone.Text),
                        reiting = int.Parse(TboxReiting.Text)
                    };
                    App.Context.Partners.Add(partners);
                    App.Context.SaveChanges();
                    NavigationService.GoBack();
                }
                else
                {
                    _currentPartenres.type_partner = TboxTip.Text;
                    _currentPartenres.name = TboxName.Text;
                    _currentPartenres.contact_person = TboxPerson.Text;
                    _currentPartenres.phonne = long.Parse(TboxPhone.Text);
                    _currentPartenres.reiting = int.Parse(TboxReiting.Text);
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TboxTip.Text))
                errorBuilder.AppendLine("Тип обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(TboxName.Text))
                errorBuilder.AppendLine("Наименование обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(TboxPerson.Text))
                errorBuilder.AppendLine("Имя директора обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(TboxPhone.Text))
            {
                errorBuilder.AppendLine("Номер телефона обязателен для заполнения.");
            }
            else
            {
                if (!long.TryParse(TboxPhone.Text, out _))
                    errorBuilder.AppendLine("Номер телефона должен содержать только цифры.");
            }

            if (string.IsNullOrWhiteSpace(TboxReiting.Text))
            {
                errorBuilder.AppendLine("Рейтинг обязателен для заполнения.");
            }
            else
            {
                int rating;
                if (!int.TryParse(TboxReiting.Text, out rating) || rating < 0 || rating > 10)
                    errorBuilder.AppendLine("Рейтинг должен быть числом от 0 до 10.");
            }
            var partnerFromDB = App.Context.Partners
                .FirstOrDefault(p => p.name.ToLower() == TboxName.Text.ToLower());

            if (partnerFromDB != null && partnerFromDB != _currentPartenres)
                errorBuilder.AppendLine("Партнер с таким наименованием уже существует в базе данных.");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }

        private void BtnNazad_Click2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }
    }
}
