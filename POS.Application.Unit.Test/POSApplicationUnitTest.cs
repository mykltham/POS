using System;
using System.Collections.Generic;
using POS.Application;
using POS.Application.Entities;
using Xunit;

namespace POS.Application.Unit.Test
{
    public abstract class POSApplicationUnitTestBase
    {
        public IPointOfSaleTerminal UnderTest;

        public POSApplicationUnitTestBase(IPointOfSaleTerminal pointOfSaleTerminal)
        {
            // Arrange
            UnderTest = pointOfSaleTerminal;
            SetupPricing();
        }

        private void SetupPricing()
        {
            // A
            UnderTest.SetPricing("A", "Apple", 1.25m);
            UnderTest.SetPricing("A", "Apple", new List<VolumePrice>()
            {
                new VolumePrice()
                {
                    Unit = 3,
                    Price = 3
                }
            });

            // B
            UnderTest.SetPricing("B", "Banana", 4.25m);

            // C
            UnderTest.SetPricing("C", "Coconut", 1.00m);
            UnderTest.SetPricing("C", "Coconut", new List<VolumePrice>()
            {
                new VolumePrice()
                {
                    Unit = 6,
                    Price = 5
                }
            });

            // D
            UnderTest.SetPricing("D", "Durian", 0.75m);

            // E
            UnderTest.SetPricing("E", "European Pear", 2.00m);
            UnderTest.SetPricing("E", "European Pear", new List<VolumePrice>()
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

    public class TestScenario_ABCDABA : POSApplicationUnitTestBase
    {
        decimal Actual = decimal.Zero;
        readonly decimal Expected = 13.25m;

        public TestScenario_ABCDABA() : base(new PointOfSaleTerminal())
        {
            // arrange
            UnderTest.ScanProduct("A");
            UnderTest.ScanProduct("B");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("D");
            UnderTest.ScanProduct("A");
            UnderTest.ScanProduct("B");
            UnderTest.ScanProduct("A");

            // act
            Actual = UnderTest.CalculateTotal();
        }

        [Fact]
        public void When_Scanning_ABCDABA_Return_ThirteenDollarsTwentyFiveCents()
        {
            Assert.Equal(Expected, Actual); 
        }
    }

    public class TestScenario_CCCCCCC : POSApplicationUnitTestBase
    {
        decimal Actual = decimal.Zero;
        readonly decimal Expected = 6m;

        public TestScenario_CCCCCCC() : base(new PointOfSaleTerminal())
        {
            // arrange
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("C");

            // act
            Actual = UnderTest.CalculateTotal();
        }

        [Fact]
        public void When_Scanning_CCCCCCC_Return_SixDollars()
        {
            Assert.Equal(Expected, Actual);
        }
    }

    public class TestScenario_ABCD : POSApplicationUnitTestBase
    {
        decimal Actual = decimal.Zero;
        readonly decimal Expected = 7.25m;

        public TestScenario_ABCD() : base(new PointOfSaleTerminal())
        {
            // arrange
            UnderTest.ScanProduct("A");
            UnderTest.ScanProduct("B");
            UnderTest.ScanProduct("C");
            UnderTest.ScanProduct("D");

            // act
            Actual = UnderTest.CalculateTotal();
        }

        [Fact]
        public void When_Scanning_ABCD_Return_SevenDollarTwentyFiveCents()
        {
            Assert.Equal(Expected, Actual);
        }
    }

}
