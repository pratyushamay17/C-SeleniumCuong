using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Driver
{
    public class FirefoxDriverFactory : AbstractDriverFactory
    {
        private bool headless = false;

        public override DriverOptions GetOptions()
        {
            FirefoxOptions options = new FirefoxOptions();
            //options.AddArguments("--window-size=1920,1200");
            if (headless) options.AddArguments("--headless");
            return options;
        }

        protected override IWebDriver BuildDriver()
        {
            return new FirefoxDriver((FirefoxOptions)GetOptions());
        }

        public override IDriverFactory SetHeadless(bool isHeadless)
        {
            this.headless = isHeadless;
            return this;
        }
    }
}
