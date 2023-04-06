using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogDesktop.Services
{
    public class CatalogService
    {
        private int cityId;
        private int streetId;
        private int houseId;

        public int CityId { get { return cityId; } set { cityId=value; } }
        public int StreetId { get { return streetId; } set { streetId=value;} }
        public int HouseId { get { return houseId; } set { houseId=value;} }
    }
}
