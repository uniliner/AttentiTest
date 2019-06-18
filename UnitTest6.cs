using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using AttentiTests.Pages;

namespace AttentiTests
{
    [TestFixture]
    public class UnitTest6
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            SeleniumProcesses.Instance.SetWebDriver(driver);
        }

        [Test]
        public void Test6()
        {
            RestClient client = new RestClient("https://community-open-weather-map.p.rapidapi.com");
            RestRequest request = new RestRequest("weather?units=metric&mode=xml&q=New+York");
            request.AddHeader("X-RapidAPI-Host", "community-open-weather-map.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "ILmwpWBGHDmshAWPBUp91ahZJLHmp1ikb2YjsnZC1ThqMnGUMY");
            IRestResponse response = client.Get(request);

            if(response.IsSuccessful)
            {
                XDocument document = XDocument.Parse(response.Content);

                var tempValue = from items in document.Descendants("temperature")
                            select items.Attribute("value").Value;

                double temperatureFromApi = double.Parse(tempValue.First());

                Weather weather = new Weather();
                weather.NavigateTo();
                double temperatureFromWeatherCom = new Temperature().FahrenheitToCelcius(weather.GetTemperature());

                double tenPercent = temperatureFromApi * 0.1;

                Assert.IsTrue(temperatureFromWeatherCom >= temperatureFromApi - tenPercent && temperatureFromWeatherCom <= temperatureFromApi + tenPercent);
            }
            else
            {
                throw new Exception("could not get response");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}