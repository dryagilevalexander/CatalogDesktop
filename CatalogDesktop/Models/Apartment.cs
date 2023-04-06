﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogDesktop.Models
{
    public class Apartment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("area")]
        public double Area { get; set; }
        [Column("house_id")]
        public int HouseId { get; set; }
        public House House { get; set; }
    }
}
