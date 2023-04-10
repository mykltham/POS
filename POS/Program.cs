using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Application;
using POS.Application.Entities;

namespace POS
{
    class Program
    {
        static void Main(string[] args)
        {
            var terminal = new PointOfSaleTerminal();

            // Scenario 1
            SetPricing(ref terminal);
            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("C");
            terminal.ScanProduct("D");
            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("A");
            Console.WriteLine("ABCDABA");
            Console.WriteLine(terminal.CalculateTotal());
            Console.ReadLine();

            // Scenario 2
            terminal = new PointOfSaleTerminal();
            SetPricing(ref terminal);
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            terminal.ScanProduct("C");
            Console.WriteLine("CCCCCCC");
            Console.WriteLine(terminal.CalculateTotal());
            Console.ReadLine();

            // Scenario 3
            terminal = new PointOfSaleTerminal();
            SetPricing(ref terminal);
            terminal.ScanProduct("A");
            terminal.ScanProduct("B");
            terminal.ScanProduct("C");
            terminal.ScanProduct("D");
            Console.WriteLine("ABCD");
            Console.WriteLine(terminal.CalculateTotal());
            Console.ReadLine();

            // Scenario 4
            terminal = new PointOfSaleTerminal();
            SetPricing(ref terminal);
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            terminal.ScanProduct("E");
            Console.WriteLine("8 x Es");
            Console.WriteLine(terminal.CalculateTotal());
            Console.ReadLine();
        }

        private static void SetPricing(ref PointOfSaleTerminal terminal )
        {
            // A
            terminal.SetPricing("A", "Apple", 1.25m);
            terminal.SetPricing("A", "Apple", new List<VolumePrice>()
            {
                new VolumePrice()
                {
                    Unit = 3,
                    Price = 3
                }
            });

            // B
            terminal.SetPricing("B", "Banana", 4.25m);

            // C
            terminal.SetPricing("C", "Coconut", 1.00m);
            terminal.SetPricing("C", "Coconut", new List<VolumePrice>()
            {
                new VolumePrice()
                {
                    Unit = 6,
                    Price = 5
                }
            });

            // D
            terminal.SetPricing("D", "Durian", 0.75m);

            // E
            terminal.SetPricing("E", "European Pear", 2.00m);
            terminal.SetPricing("E", "European Pear", new List<VolumePrice>()
            {
                new VolumePrice()
                {
                    Unit = 3,
                    Price = 5
                },
                new VolumePrice()
                {
                    Unit = 5,
                    Price = 8
                },
                new VolumePrice()
                {
                    Unit = 9,
                    Price = 15
                },
            });

        }
    }
}
