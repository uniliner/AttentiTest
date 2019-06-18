using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

using AttentiTests.Pages;

namespace AttentiTests
{
    [TestFixture]
    public class UnitTest5
    {
        private IWebDriver driver;
        private Random random;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            SeleniumProcesses.Instance.SetWebDriver(driver);

            random = new Random();
        }

        [Test]
        public void Test5a()
        {
            Temperature tmp = new Temperature();
            tmp.NavigateTo();
            Thread.Sleep(2000);
            tmp.SetUnits(Helper.Units.Celsius, Helper.Units.Fahrenheit);
            int value = random.Next(-1000, 1000);
            double result = tmp.Convert(value, "°F");

            Assert.AreEqual(tmp.CalculateExpect(value, Helper.Units.Celsius, Helper.Units.Fahrenheit, 3), result);
        }

        [Test]
        public void Test5b()
        {
            Length len = new Length();
            len.NavigateTo();
            Thread.Sleep(2000);
            len.SetUnits(Helper.Units.Meters, Helper.Units.Feet);
            int value = random.Next(0, 100000);
            double result = len.Convert(value, "ft", true);

            int fractionLength = Helper.GetFractionLength(result);
            if(fractionLength > 0)
            {
                result = Math.Round(result, fractionLength);
            }

            Assert.AreEqual(len.CalculateExpect(value, Helper.Units.Meters, Helper.Units.Feet, fractionLength), result);
        }

        [Test]
        public void Test5c()
        {
            Weight weight = new Weight();
            weight.NavigateTo();
            Thread.Sleep(2000);
            weight.SetUnits(Helper.Units.Ounces, Helper.Units.Grams);
            int value = random.Next(0, 100000);
            double result = weight.Convert(value, "g");

            int fractionLength = Helper.GetFractionLength(result);
            if(fractionLength > 0)
            {
                result = Math.Round(result, fractionLength);
            }

            Assert.AreEqual(weight.CalculateExpect(value, Helper.Units.Ounces, Helper.Units.Grams, fractionLength), result);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}