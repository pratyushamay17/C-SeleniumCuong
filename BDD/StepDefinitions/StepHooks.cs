using System;
using BoDi;
using Driver;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace JupiterToys.BDD.StepDefinitions {

    [Binding]
    public class StepHooks {
        private readonly IObjectContainer objectContainer;

        public StepHooks(IObjectContainer objectContainer) {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void Setup() {
            IConfiguration config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            var gridUrl = config.GetSection("SELENIUM_GRID_URL").Value;
            var browser = config.GetSection("SELENIUM_BROWSER").Value;
            var headless = bool.Parse(config.GetSection("SELENIUM_HEADLESS").Value);
            var implicitWait = int.Parse(config.GetSection("SELENIUM_WAIT").Value);
            var Url = config.GetSection("SELENIUM_URL").Value;

            IWebDriver driver = (gridUrl != null && gridUrl.Length > 0)
                ? FactoryBuilder.GetFactory(browser, gridUrl).GetDriver()
                : FactoryBuilder.GetFactory(browser).SetHeadless(headless).GetDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            driver.Navigate().GoToUrl(Url); 
            objectContainer.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void Shutdown() {
            IWebDriver driver = this.objectContainer.Resolve<IWebDriver>();
            driver.Quit();
        }
    }
}


