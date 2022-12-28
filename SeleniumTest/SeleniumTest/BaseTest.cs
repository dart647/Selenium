using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver { get; set; }

        [SetUp]
        protected void StartBrowser()
        {
            Driver = new ChromeDriver(@"D:\Work\Education\Заочка\Тестирование\Selenium\chromedriver_win32");
        }

        public abstract void Test();

        [TearDown]
        protected void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}