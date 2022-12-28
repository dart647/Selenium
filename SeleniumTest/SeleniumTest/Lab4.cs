using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace SeleniumTest
{
    public class Lab4 : BaseTest
    {
        [Test]
        public override void Test()
        {
            Driver.Url = "https://habr.com/ru/search";
            Driver.Manage().Window.Maximize();

            var tagList = new string[] { "информационная безопасность*", "криптография*", "c*", "IT-стандарты*" };

            foreach (var tag in tagList)
            {
                var searchField = Driver.FindElement(By.ClassName("tm-input-text-decorated__input"));
                searchField.Clear();
                searchField.SendKeys(tag);
                searchField.Submit();

                Thread.Sleep(TimeSpan.FromSeconds(2));

                for (int i = 0; i < 5; i++)
                {
                    Assert.IsTrue(TryFindTag(tag, i)); 
                }
            }
        }

        private bool TryFindTag(string tag, int index)
        {
            var tagDoms = Driver.FindElements(By.XPath($"//article[{index + 1}]/*/*/*/*[contains(@class, 'tm-article-snippet__hubs-item-link')]/span[1]"));

            return !tagDoms.Any() || tagDoms.Any(x => x.Text.ToLower() == tag.TrimEnd('*').ToLower());
        }
    }
}
