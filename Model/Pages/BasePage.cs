using OpenQA.Selenium;
using System;
using JupiterToys.Model.Components.Dialogs;

namespace JupiterToys.Model.Pages {
    public abstract class BasePage<T> where T : BasePage<T> {
        protected IWebDriver driver;

        protected BasePage(IWebDriver driver) {
            this.driver = driver;
        }

        public ContactPage ClickContactMenu() {
            driver.FindElement(By.CssSelector("#nav-contact a")).Click();
            return new ContactPage(driver);
        }

        public LoginDialog<T> ClickLoginMenu() {
            driver.FindElement(By.CssSelector("#nav-login a")).Click();
            return new LoginDialog<T>(driver.FindElement(By.ClassName("popup")), this as T);
        }

        public AccountPage ClickAccountMenu() {
            driver.FindElement(By.CssSelector("#nav-user a")).Click();
            return new AccountPage(driver);
        }

        public ShopPage ClickShopMenu() {
            driver.FindElement(By.CssSelector("#nav-shop a")).Click();
            return new ShopPage(driver);
        }

        public CartPage ClickCartMenu() {
            driver.FindElement(By.CssSelector("#nav-cart a")).Click();
            return new CartPage(driver);
        }

        public int CartCount => Int32.Parse(driver.FindElement(By.ClassName("cart-count")).Text);
    }
}
