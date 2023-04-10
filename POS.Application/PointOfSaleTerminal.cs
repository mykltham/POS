using System;
using System.Collections.Generic;
using System.Linq;
using POS.Application.Entities;

namespace POS.Application
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private List<Product> _productList;
        private List<string> _scannedItems;

        public PointOfSaleTerminal()
        {
            _productList = new List<Product>();
            _scannedItems = new List<string>();
        }


        public decimal CalculateTotal()
        {
            var total = decimal.Zero;

            // group by product code
            var groupedItems = _scannedItems
                                    .GroupBy(x => x)
                                    .Select(y => new GroupedItem { ProductCode = y.Key, Quantity = y.Count() });
            // compute total
            foreach(GroupedItem groupedItem in groupedItems)
            {
                total += ComputeItemTotal(groupedItem);
            }

            return total;
        }


        private decimal ComputeItemTotal(GroupedItem groupedItem)
        {
            var total = decimal.Zero;

            var item = _productList.Where(x => x.ProductCode.Equals(groupedItem.ProductCode)).FirstOrDefault();
            if (item != null)
            {
                var remainingQuantity = groupedItem.Quantity;

                // check volume price
                var volumePrice = item.VolumePrices.Where(x => x.Unit <= remainingQuantity).OrderByDescending(x => x.Unit).FirstOrDefault();
                while(volumePrice != null)
                {
                    total += volumePrice.Price;
                    remainingQuantity = remainingQuantity - volumePrice.Unit;
                    
                    volumePrice = item.VolumePrices.Where(x => x.Unit <= remainingQuantity).OrderByDescending(x => x.Unit).FirstOrDefault();
                }

                // compute remainder with unit pricing
                total += (remainingQuantity * item.UnitPrice);
            }

            return total;
        }


        public void ScanProduct(string productCode)
        {
            _scannedItems.Add(productCode);
        }


        public void SetPricing(string productCode, string productName, decimal unitPrice)
        {
            var item = _productList.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
            if (item != null)
            {
                //update
                item.ProductName = productName;
                item.UnitPrice = unitPrice;
            }
            else
            {
                //add
                item = new Product()
                {
                    ProductCode = productCode,
                    ProductName = productName,
                    UnitPrice = unitPrice,
                };
                _productList.Add(item);
            }
        }


        public void SetPricing(string productCode, string productName, IEnumerable<VolumePrice> volumePrices)
        {
            var item = _productList.Where(x => x.ProductCode.Equals(productCode)).FirstOrDefault();
            if (item != null)
            {
                //update
                item.ProductName = productName;
                item.VolumePrices = volumePrices;
            }
            else
            {
                //add
                item = new Product()
                {
                    ProductCode = productCode,
                    ProductName = productName,
                    VolumePrices = volumePrices,
                };
                _productList.Add(item);
            }
        }
    }
}
