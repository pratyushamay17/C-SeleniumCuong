using System;
using Driver;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JupiterToys.Test {

    // uncomment once nunit 3.13 is released and make scope ParallelScope.All
    // [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BaseTestSuite {
        protected IWebDriver driver;

        [SetUp]
        public void Setup() {

            IConfiguration config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            var gridUrl = config.GetSection("SELENIUM_GRID_URL").Value;
            var browser = config.GetSection("SELENIUM_BROWSER").Value;
            var headless = bool.Parse(config.GetSection("SELENIUM_HEADLESS").Value);
            var implicitWait = int.Parse(config.GetSection("SELENIUM_WAIT").Value);
            var Url = config.GetSection("SELENIUM_URL").Value;
            
            driver = (gridUrl != null && gridUrl.Length>0)
                ? FactoryBuilder.GetFactory(browser, gridUrl).GetDriver()
                : FactoryBuilder.GetFactory(browser).SetHeadless(headless).GetDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait); //EnvironmentVariables.Instance.ImplicitWait
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url); //EnvironmentVariables.Instance.Environment
        }

        [TearDown]
        public void TearDown(){
            driver.Quit();
        }

        protected  T open<T>() {
            return (T)typeof(T).GetConstructor(new [] {typeof(IWebDriver)}).Invoke(new IWebDriver[] {driver}); 
        }
    }
}
