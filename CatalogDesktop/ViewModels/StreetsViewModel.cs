using CatalogDesktop.GridModels;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows;

namespace CatalogDesktop.ViewModels
{
    public class StreetsViewModel
    {
        public IEnumerable<StreetWithHouses> Streets { get; set; }

        public StreetsViewModel(int id, CatalogContext db)
        {
            try
            { 
            Streets = from street in db.Streets.Local
                      where (street.CityId == id)
                      select new StreetWithHouses
                      {
                          StreetId = street.Id,
                          StreetName = street.Name,
                          CountOfHouses = db.Houses.Local.Where(u => u.StreetId == street.Id).Count()
                      };
            }
            catch
            {
                Streets = new List<StreetWithHouses>();
                MessageBox.Show("Внутренняя ошибка приложения");
            }    
        }
    }
}
