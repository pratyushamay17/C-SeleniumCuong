using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;

namespace Driver {
    public class ChromeDriverFactory : AbstractDriverFactory
    {
        private bool headless = false;

        public override DriverOptions GetOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-gpu",
                      "--window-size=1920,1200",
                      "--ignore-certificate-errors");
            if (headless) options.AddArguments("--headless");
            return options;
        }

        protected override IWebDriver BuildDriver()
        {
            return new ChromeDriver((ChromeOptions)GetOptions());
        }

        public override IDriverFactory SetHeadless(bool isHeadless)
        {
            headless = isHeadless;
            return this;
        }
    }
}
