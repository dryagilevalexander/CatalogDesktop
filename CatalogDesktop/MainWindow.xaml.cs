using CatalogDesktop.GridModels;
using CatalogDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using CatalogDesktop.ViewModels;
using CatalogDesktop.GridModels;
using CatalogDesktop.Services;
using CatalogDesktop.Models;
using System.Text.RegularExpressions;
using System.Data.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace CatalogDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CatalogContext db;
        private CatalogService catalogService;
        public MainWindow()
        {
            db = new CatalogContext();
            db.Cities.Load();
            db.Streets.Load();
            db.Houses.Load();
            db.Apartments.Load();
            InitializeComponent();
            DataContext = new CitiesViewModel(db);
            catalogService = new CatalogService();
        }
        public void CitiesGridDoubleClickRow(object sender, MouseButtonEventArgs e)
        {
            var currentItem = (CityWithCountOfStreets)CitiesGrid.CurrentItem;
            catalogService.CityId = currentItem.CityId;
            CitiesGrid.Visibility = Visibility.Collapsed;
            DataContext = new StreetsViewModel(catalogService.CityId, db);
            StreetsGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        public void StreetsGridDoubleClickRow(object sender, MouseButtonEventArgs e)
        {
            var currentItem = (StreetWithHouses)StreetsGrid.CurrentItem;
            catalogService.StreetId = currentItem.StreetId;
            StreetsGrid.Visibility = Visibility.Collapsed;
            DataContext = new HousesViewModel(catalogService.StreetId, db);
            HousesGrid.Visibility = Visibility.Visible;
        }

        public void HousesGridDoubleClickRow(object sender, MouseButtonEventArgs e)
        {
            var currentItem = (HouseWithApartmentsArea)HousesGrid.CurrentItem;
            catalogService.HouseId = currentItem.HouseId;
            HousesGrid.Visibility = Visibility.Collapsed;
            DataContext = new ApartmentsViewModel(catalogService.HouseId, db);
            ApartmentsGrid.Visibility = Visibility.Visible;
            AreaLabel1.Visibility = Visibility.Visible;
            AreaLabel2.Visibility = Visibility.Visible;
            Area1.Visibility = Visibility.Visible;
            Area2.Visibility = Visibility.Visible;
            AreaButton.Visibility = Visibility.Visible;
        }

        private void AreaButton_Click(object sender, RoutedEventArgs e)
        {
            double minArea = Convert.ToDouble(Area1.Text);
            double maxArea = Convert.ToDouble(Area2.Text);
            DataContext = new ApartmentsViewModel(minArea, maxArea, catalogService.HouseId, db);

        }

        private static readonly Regex onlyNumbers = new Regex("[^0-9,-]+");

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }

        private void Area1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Area2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StreetsViewModel)
            {
                StreetsGrid.Visibility = Visibility.Collapsed;
                DataContext = new CitiesViewModel(db);
                CitiesGrid.Visibility = Visibility.Visible;
                AreaButton.Visibility = Visibility.Collapsed;
            }
            else if (DataContext is HousesViewModel)
            {
                HousesGrid.Visibility = Visibility.Collapsed;
                DataContext = new StreetsViewModel(catalogService.CityId, db);
                StreetsGrid.Visibility = Visibility.Visible;
            }
            else if (DataContext is ApartmentsViewModel)
            {
                ApartmentsGrid.Visibility = Visibility.Collapsed;
                DataContext = new HousesViewModel(catalogService.StreetId, db);
                AreaLabel1.Visibility = Visibility.Collapsed;
                AreaLabel2.Visibility = Visibility.Collapsed;
                Area1.Visibility = Visibility.Collapsed;
                Area2.Visibility = Visibility.Collapsed;
                AreaButton.Visibility = Visibility.Collapsed;
                HousesGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
