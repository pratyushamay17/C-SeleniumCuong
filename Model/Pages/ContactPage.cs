using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using JupiterToys.Model.Data;

namespace JupiterToys.Model.Pages
{
    public class ContactPage : BasePage<ContactPage>
    {
        public ContactPage(IWebDriver driver) : base(driver)
        {
            //Here waiting for, for example, a loading spiner to disappear
        }

        public ContactPage SetContactData(ContactData data)
        {
            if (data.Forename != null)
            {
                this.SetForename(data.Forename);
            }
            if (data.Email != null)
            {
                this.SetEmail(data.Email);
            }
            if (data.Message != null)
            {
                this.SetMessage(data.Message);
            }
            if (data.Surname != null)
            {
                this.SetSurname(data.Surname);
            }
            if (data.Telephone != null)
            {
                this.SetTelephone(data.Telephone);
            }
            return this;
        }

        private string GetErrorText(By locator)
        {
            IReadOnlyCollection<IWebElement> elements = this.driver.FindElements(locator);
            return elements.Count == 0 ? "" : elements.ElementAt(0).Text;

        }

        /*
         * Submit Button
         * -----------------------------------------------------------------------------------
         */
        public ContactPage ClickSubmitButton()
        {
            IWebElement element = this.driver.FindElement(By.ClassName("btn-primary"));
            //new Actions(driver).MoveToElement(element).Perform();
            element.Click();
            return this;
        }

        /*
         * Forname
         * -----------------------------------------------------------------------------------
         */

        private IWebElement GetFornameElement()
        {
            return this.driver.FindElement(By.Name("forename"));
        }

        public ContactPage SetForename(string forename)
        {
            GetFornameElement().SendKeys(forename);
            return this;
        }

        public string GetForename()
        {
            return GetFornameElement().GetAttribute("value");

        }

        public string GetForenameError()
        {
            return GetErrorText(By.Id("forename-err"));
        }

        /*
         * Surname
         * -----------------------------------------------------------------------------------
         */

        private IWebElement GetSurnameElement()
        {
            return this.driver.FindElement(By.Name("surname"));
        }

        public ContactPage SetSurname(string surname)
        {
            GetSurnameElement().SendKeys(surname);
            return this;
        }

        public string GetSurname()
        {
            return GetSurnameElement().GetAttribute("value");

        }

        /*
         * Email
         * -----------------------------------------------------------------------------------
         */

        private IWebElement GetEmailElement()
        {
            return this.driver.FindElement(By.Name("email"));
        }

        public ContactPage SetEmail(string email)
        {
            GetEmailElement().SendKeys(email);
            return this;
        }

        public string GetEmail()
        {
            return GetEmailElement().GetAttribute("value");
        }

        public string GetEmailError()
        {
            return GetErrorText(By.Id("email-err"));
        }

        /*
         * Message
         * -----------------------------------------------------------------------------------
         */

        public IWebElement GetMessageElement()
        {
            return this.driver.FindElement(By.Name("message"));
        }
        public ContactPage SetMessage(string message)
        {
            GetMessageElement().SendKeys(message);
            return this;
        }

        public string GetMessage()
        {
            return GetMessageElement().GetAttribute("value");
        }

        public string GetMessageError()
        {
            return GetErrorText(By.Id("message-err"));
        }

        /*
         * Telephone
         * -----------------------------------------------------------------------------------
         */
        public IWebElement GetTelephoneElement()
        {
            return this.driver.FindElement(By.Name("telephone"));
        }

        public ContactPage SetTelephone(string telephone)
        {
            GetTelephoneElement().SendKeys(telephone);
            return this;
        }

        public string GetTelephone()
        {
            return GetTelephoneElement().GetAttribute("value");
        }

        public string GetTelephoneError()
        {
            return GetErrorText(By.Id("telephone-err"));
        }

        /*
         * Successful message
         * -----------------------------------------------------------------------------------
         */

        public string GetSuccessMessage()
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(60))
                .Until(d => d.FindElement(By.ClassName("alert-success")))
                .Text;
        }
    }
}
