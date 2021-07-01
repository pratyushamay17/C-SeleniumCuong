using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JupiterToys.Model.Components.Dialogs {
    public class LoginDialog<T> {
        private IWebElement element;
        private T parent;

        public LoginDialog(IWebElement element, T parent) {
            this.element = element;
            this.parent = parent;
        }

        /*
         * username
         * -----------------------------------------------------------------------------------
         */
        private IWebElement GetUsernameElement() {
            return element.FindElement(By.Id("loginUserName"));
        }

        public LoginDialog<T> SetUsername(string username) {
            GetUsernameElement().SendKeys(username);
            return this;
        }

        public string GetUsername() {
            return GetUsernameElement().GetAttribute("value");
        }




        /*
         * password
         * -----------------------------------------------------------------------------------
         */
        private IWebElement GetPasswordElement() {
            return element.FindElement(By.Id("loginPassword"));
        }

        public LoginDialog<T> SetPassword(string password) {
            GetPasswordElement().SendKeys(password);
            return this;
        }

        public string GetPassword() {
            return GetPasswordElement().GetAttribute("value");
        }

        /*
         * Login
         * -----------------------------------------------------------------------------------
         */

        public string ErrorMessage => element.FindElement(By.Id("login-error")).Text;

        public T ClickSubmitButton() {
            element.FindElement(By.ClassName("btn-primary")).Click();
            return parent;
        }

        public T ClickCancelButton() {
            element.FindElement(By.CssSelector("btn-cancel")).Click();
            return parent;
        }

        public T Login(string username, string password) {
            this.SetUsername(username);
            this.SetPassword(password);
            this.ClickSubmitButton();
            return parent;
        }
    }
}
