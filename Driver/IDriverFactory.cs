using OpenQA.Selenium;

namespace Driver
{
    public interface IDriverFactory
    {
        IWebDriver GetDriver();

        DriverOptions GetOptions();

        IDriverFactory SetHeadless(bool isHeadless);
    }
}
