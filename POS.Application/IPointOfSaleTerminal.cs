using System;
using System.Collections.Generic;
using POS.Application.Entities;

namespace POS.Application
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(string productCode, string productName, decimal unitPrice);

        void SetPricing(string productCode, string productName, IEnumerable<VolumePrice> volumePrices);

        void ScanProduct(string productCode);

        decimal CalculateTotal();
    }
}
