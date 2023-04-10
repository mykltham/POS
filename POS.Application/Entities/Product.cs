using System;
using System.Collections.Generic;

namespace POS.Application.Entities
{
    /// <summary>
    /// Product Entity
    /// </summary>
    public class Product
    {
        public Product()
        {
            this.VolumePrices = new List<VolumePrice>();
        }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public IEnumerable<VolumePrice> VolumePrices { get; set; }

    }
}
