using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SeleniumTest
{
    public class Lab1 : BaseTest
    {
        [Test]
        public override void Test()
        {
            var windowDict = new Dictionary<string, HrefWordCount>();

            Driver.Url = "http://www.siaxx.com/";
            Driver.Manage().Window.Maximize();

            var hrefs = Driver.FindElements(By.XPath("//*[self::a]")).Select(x => x.GetAttribute("href")).Distinct().ToArray();

            var wordCount = Driver.FindElements(By.XPath("//*[contains(text(), 'Able')]")).Count();
            windowDict.TryAdd(Driver.CurrentWindowHandle, new HrefWordCount { WordCount = wordCount, Href = Driver.Url });
            Console.WriteLine($"Страница {Driver.Url} добавлена");

            foreach (var href in hrefs)
            {
                Driver.SwitchTo().NewWindow(WindowType.Tab);
                Driver.Navigate().GoToUrl(href);

                wordCount = Driver.FindElements(By.XPath("//*[contains(text(), 'Able')]")).Count();
                windowDict.TryAdd(Driver.CurrentWindowHandle, new HrefWordCount { WordCount = wordCount, Href = href });
                Console.WriteLine($"Страница {href} добавлена");
            }

            Console.WriteLine();
            var windowDictSorted = windowDict.OrderByDescending(x => x.Value.WordCount);

            foreach (var window in windowDictSorted)
            {
                Console.WriteLine($"Слов на странице {window.Value.Href}: {window.Value.WordCount}");
                Driver.SwitchTo().Window(window.Key);
                Driver.Close();
            }
        }

        private struct HrefWordCount
        {
            public int WordCount { get; set; }
            public string Href { get; set; }
        }
    }
}
