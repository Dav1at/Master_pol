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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
            Discount();
            LViewServices.ItemsSource = App.Context.Partners.ToList();
        }
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            var currentPartners = (sender as Button).DataContext as Entities.Partners;
            NavigationService.Navigate(new AddBd());
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentPartners = (sender as Button).DataContext as Entities.Partners;
            NavigationService.Navigate(new AddEditServicePage(currentPartners));
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new History());
        }
        private void BtnObnova_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }
        private void Discount()
        {
            var discount = App.Context.Discount.ToList();
            var groupedPartners = discount.GroupBy(d => d.partners)
                                          .Select(g => new
                                          {
                                              Partner = g.Key,
                                              SupplySum = g.Sum(x => x.quantity),
                                              Id = g.First().id
                                          }).ToList();

            List<DiscountResult> results = new List<DiscountResult>();
            foreach (var group in groupedPartners)
            {
                int calculatedDiscount = CalculateDiscount(group.SupplySum);
                results.Add(new DiscountResult
                {
                    id = group.Id,
                    partners = group.Partner,
                    discount = calculatedDiscount,
                });
            }

            SaveResults(results);
        }
        private void SaveResults(List<DiscountResult> results)
        {
            using (var context = new Partner_companyEntities6())
            {
                var existingIds = context.DiscountResult.Select(dr => dr.id).ToArray();

                var uniqueResults = results.Where(r => !existingIds.Contains(r.id)).ToList();

                context.DiscountResult.AddRange(uniqueResults);
                context.SaveChanges();
            }
        }
        private int CalculateDiscount(decimal supplySum)
        {
            if (supplySum >= 300000)
                return 15;
            else if (supplySum >= 50000 && supplySum < 300000)
                return 10;
            else if (supplySum >= 10000 && supplySum < 50000)
                return 5;
            else
                return 0;
        }
    }
}
