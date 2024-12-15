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
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        public static Entities.Partner_companyEntities6 Context
        { get; set; } = new Entities.Partner_companyEntities6();
        public History()
        {
            InitializeComponent(); 
            LoadData();
            Loadbrak();
        }
        private void LoadData()
        {
            try
            {
                // Получаем данные из представления или таблицы
                var data = Context.skidki.ToList();

                // Привязываем данные к DataGrid
                HistoryDataGrid.ItemsSource = data;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Ошибка при загрузке данных: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nВнутреннее исключение: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Loadbrak()
        {
            try
            {
                // Загружаем данные из таблицы "Товары"
                var brac = Context.Material_type.ToList();

                // Привязываем данные к ComboBox
                ComboBox1.ItemsSource = brac;
                ComboBox1.DisplayMemberPath = "type";
                ComboBox1.SelectedValuePath = "brak";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
        private void BtnNazad_Click1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        private void BtnSet_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox1.SelectedValue != null && int.TryParse(TextBox1.Text, out int quantity))
            {
                // Получаем процент брака из SelectedValue
                double defectRate = Convert.ToDouble(ComboBox1.SelectedValue);

                // Рассчитываем итоговое количество с учётом брака
                double requiredQuantity = (double)(quantity * (1 + defectRate / 100));

                // Отображаем результат в метке
                label1.Content = $"Количество товара которое необходимо с учетом брака: {requiredQuantity}";
            }
            else
            {
                // Сброс, если данные некорректны
                label1.Content = "Количество товара с учетом брака:";
            }
        }
        
    }
}
