using CatalogDesktop.GridModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;

namespace CatalogDesktop.ViewModels
{
    public class CitiesViewModel
    {
        public IEnumerable<CityWithCountOfStreets> Cities { get; set; }

        public CitiesViewModel(CatalogContext db)
        {
            try
            {
                Cities = from city in db.Cities.Local
                         join street in db.Streets.Local on city.Id equals street.Id
                         select new CityWithCountOfStreets
                         {
                             CityId = city.Id,
                             CityName = city.Name,
                             CountOfStreets = city.Streets.Count()
                         };
            }
            catch
            {
                Cities = new List<CityWithCountOfStreets>();
                MessageBox.Show("Внутренняя ошибка приложения");
            }
        }
       
    }
}
