using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CatalogDesktop.GridModels
{
    public class CityWithCountOfStreets
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountOfStreets { get; set; }

    }
}
