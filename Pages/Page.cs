using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace AttentiTests.Pages
{
    public abstract class Page
    {
        protected delegate double ConvertMethod(double input);

        private const string resultExp = @"-?\d+\S*=\s(?<result>-?\d+(\.\d+)?)";
        private const string baseUrl = "https://www.metric-conversions.org/";
        private string url;
        private By fromUnit = By.Id("queryFrom");
        private By toUnit = By.Id("queryTo");
        private By input = By.CssSelector("input.argument");
        private By answerParagraph = By.Id("answer");
        private By formatDropDown = By.Id("format");

        public Page(string name)
        {
            url = baseUrl + name + "-conversion.htm";
        }

        public void NavigateTo()
        {
            SeleniumProcesses.Instance.NavigateTo(url);
        }

        public void SetUnits(Helper.Units from, Helper.Units to)
        {
            SeleniumProcesses.Instance.SetTextBoxValue(fromUnit, Enum.GetName(typeof(Helper.Units), from));
            
            SeleniumProcesses.Instance.SetTextBoxValue(toUnit, Enum.GetName(typeof(Helper.Units), to));
        }

        public double Convert(int value, string resultUnit, Boolean setDecimalFormat = false)
        {
            SeleniumProcesses.Instance.SetTextBoxValue(input, value.ToString());

            if(setDecimalFormat)
            {
                SeleniumProcesses.Instance.SelectFromDropDown(formatDropDown, "Decimal");
            }

            string result = SeleniumProcesses.Instance.GetText(answerParagraph);

            Regex regex = new Regex(resultExp + resultUnit);
            Match match = regex.Match(result);
            if(match.Success)
            {
                return Double.Parse(match.Groups["result"].Value);
            } 
            else 
            {
                throw new Exception("could not parse result value");
            }
        }

        public abstract double CalculateExpect(int input, Helper.Units from, Helper.Units to, int fractionLength);
    }
}