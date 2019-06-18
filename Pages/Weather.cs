using OpenQA.Selenium;

namespace AttentiTests.Pages
{
    public class Weather
    {
        private const string url = "https://weather.com/weather/today/l/f892433d7660da170347398eb8e3d722d8d362fe7dd15af16ce88324e1b96e70";

        private By temperature = By.CssSelector("div.today_nowcard-temp > span");

        private By unitSelector = By.ClassName("icon-arrow-down-triangle");

        public void NavigateTo()
        {
            SeleniumProcesses.Instance.NavigateTo(url);
        }

        public double GetTemperature()
        {
            string tempText = SeleniumProcesses.Instance.GetText(temperature);
            tempText = tempText.TrimEnd('°');

            return double.Parse(tempText);
        }
    }
}