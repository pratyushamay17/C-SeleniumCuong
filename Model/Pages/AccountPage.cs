using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JupiterToys.Model.Pages {
    public class AccountPage : BasePage<AccountPage> {
        public AccountPage(IWebDriver driver) : base(driver) { 
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d =>
                d.FindElements(By.ClassName("hero-unit")).Count==0);
        }

        public string WelcomeMessage => driver.FindElement(By.TagName("h1")).Text;
    }
}
