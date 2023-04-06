using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CatalogDesktop.GridModels;
using System.Windows.Input;
using System.Windows;

namespace CatalogDesktop.ViewModels
{
    class ApartmentsViewModel
    {
        public IEnumerable<ApartmentWithArea> Apartments { get; set; }
        public ApartmentsViewModel(int id, CatalogContext db)
        {
            try
            {
                Apartments = from apartment in db.Apartments.Local
                             where (apartment.HouseId == id)
                             select new ApartmentWithArea
                             {
                                 ApartmentId = apartment.Id,
                                 ApartmentArea = apartment.Area
                             };
            }
            catch
            {
                Apartments = new List<ApartmentWithArea>();
                MessageBox.Show("Внутренняя ошибка приложения");
            }

        }

        public ApartmentsViewModel(double minArea, double maxArea, int id, CatalogContext db)
        {
            try
            {
                Apartments = from apartment in db.Apartments.Local
                         where ((apartment.Area >= minArea && apartment.Area <= maxArea) && apartment.HouseId == id)
                         select new ApartmentWithArea
                         {
                             ApartmentId = apartment.Id,
                             ApartmentArea = apartment.Area
                         };
            }
            catch
            {
                Apartments = new List<ApartmentWithArea>();
                MessageBox.Show("Внутренняя ошибка приложения");
            }
        }
    }
}
