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
    /// Логика взаимодействия для AddBd.xaml
    /// </summary>
    public partial class AddBd : Page
    {
        public AddBd()
        {
            InitializeComponent();
            Title = "Добавить нового партнера";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK);
                return;
            }

            try
            {
                var newPartner = new Entities.Partners()
                {
                    type_partner = TboxTip.Text,
                    name = TboxName.Text,
                    contact_person = TboxPerson.Text,
                    phonne = long.Parse(TboxPhone.Text),
                    reiting = int.Parse(TboxReiting.Text),
                    
                };

                App.Context.Partners.Add(newPartner);

                // Проверка ошибок валидации EF перед сохранением
                var validationErrors = App.Context.GetValidationErrors();
                foreach (var validationError in validationErrors)
                {
                    foreach (var error in validationError.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }

                App.Context.SaveChanges();

                MessageBox.Show("Партнер успешно добавлен!", "Успех", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении партнера: {ex.Message}", "Ошибка!", MessageBoxButton.OK);
            }
        }

        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();


            if (string.IsNullOrWhiteSpace(TboxTip.Text))
                errorBuilder.AppendLine("Тип партнера обязателен для заполнения.");

            if (string.IsNullOrWhiteSpace(TboxName.Text))
                errorBuilder.AppendLine("Наименование партнера обязательно для заполнения.");

            if (string.IsNullOrWhiteSpace(TboxPerson.Text))
                errorBuilder.AppendLine("Имя контактного лица обязательно для заполнения.");

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
                if (!int.TryParse(TboxReiting.Text, out int rating) || rating < 0 || rating > 10)
                    errorBuilder.AppendLine("Рейтинг должен быть числом от 0 до 10.");
            }
            


            var partnerFromDB = App.Context.Partners
                .FirstOrDefault(p => p.name.ToLower() == TboxName.Text.ToLower());

            if (partnerFromDB != null)
                errorBuilder.AppendLine("Партнер с таким наименованием уже существует в базе данных.");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
        private void BtnNazad_Click3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }
    }
}
