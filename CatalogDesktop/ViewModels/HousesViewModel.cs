using CatalogDesktop.GridModels;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CatalogDesktop.Models;
using System.Windows.Input;
using System.Windows;

namespace CatalogDesktop.ViewModels
{
    public class HousesViewModel
    {
        public IEnumerable<HouseWithApartmentsArea> Houses { get; set; }
        public HousesViewModel(int id, CatalogContext db)
        {
            try
            {
                Houses = from house in db.Houses.Local
                         where (house.StreetId == id)
                         select new HouseWithApartmentsArea
                         {
                             HouseId = house.Id,
                             NumberOfHouse = house.Number,
                             CountOfApartments = house.Apartments.Count(),
                             ApartmentsArea = db.Apartments.Local.Where(u => u.HouseId == house.Id).Sum(u => u.Area)
                         };
            }
            catch
            {
                Houses = new List<HouseWithApartmentsArea>();
                MessageBox.Show("Внутренняя ошибка приложения");
            }
        }
    }
}
