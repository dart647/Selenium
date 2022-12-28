using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumTest
{
    public class Lab2 : BaseTest
    {
        [Test]
        public override void Test()
        {
            Driver.Url = "https://psychicscience.org/random";
            Driver.Manage().Window.Maximize();

            var integersField = Driver.FindElement(By.Id("num"));
            var betweenLeftField = Driver.FindElement(By.Id("st"));
            var betweenRightField = Driver.FindElement(By.Id("en"));
            var sequenceDropdown = Driver.FindElement(By.Id("rpt"));
            var sequenceDropdownSelect = new SelectElement(sequenceDropdown);
            var goButton = Driver.FindElement(By.Id("go"));


            integersField.SendKeys("10");
            betweenLeftField.SendKeys("1");
            betweenRightField.SendKeys("10");

            foreach (var option in sequenceDropdownSelect.Options)
            {
                sequenceDropdownSelect.SelectByText(option.Text);
                goButton.Click();

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}
