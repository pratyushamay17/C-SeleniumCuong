// using BoDi;
using JupiterToys.Model.Data;
using JupiterToys.Model.Pages;
using OpenQA.Selenium;
using System.Data;
using System.Linq;
using BoDi;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace JupiterToys.BDD.StepDefinitions
{
    [Binding]
    public class ContactTests : BaseSteps
     {
        public ContactTests(IObjectContainer objectContainer) : base(objectContainer) {
        }

        [Given(@"A Customer tries to contact us")]
        public void GivenACustomerTriesToContactUs()
        {
            HomePage homePage = new HomePage(driver);
            homePage.ClickContactMenu();          
        }

        [When(@"Customer enters invalid Email id and telephone")]
        public void WhenCustomerEntersInvalidEmailIdAndTelephone()
        { 
            ContactData data = new ContactData(null, null, "abc@def", "abcd", null);
            ContactPage contactPage = new ContactPage(driver);
            contactPage.SetEmail(data.Email)
                       .SetTelephone(data.Telephone);
            contactPage.ClickSubmitButton();
            this.objectContainer.RegisterInstanceAs(data,"data1");
        }

        [Then(@"error message for invalid email and invalid telephone is displayed")]
        public void ThenErrorMessageForInvalidEmailAndInvalidTelephoneIsDisplayed()
    
        {
            ContactData data = this.objectContainer.Resolve<ContactData>("data1");
            ContactPage contactPage = new ContactPage(driver);
            Assert.AreEqual("Please enter a valid email", contactPage.GetEmailError());
            Assert.AreEqual("Please enter a valid telephone number", contactPage.GetTelephoneError());
        }

        [When(@"Customer submits empty information")]
        public void WhenCustomerSubmitsEmptyInformation()
        {
            HomePage homePage = new HomePage(driver);
            ContactPage contactPage = homePage.ClickContactMenu();

            contactPage.ClickSubmitButton();
        }

        [Then(@"Errors for mandatory information are displayed")]
        public void ThenErrorsForMandatoryInformationAreDisplayed()
        {
            ContactPage contactPage = new ContactPage(driver);

            Assert.AreEqual("Forename is required", contactPage.GetForenameError());
            Assert.AreEqual("Email is required", contactPage.GetEmailError());
            Assert.AreEqual("Message is required", contactPage.GetMessageError());
        }

        [Then(@"No Error messages for mandatory information are displayed")]
        public void ThenNoErrorMessagesForMandatoryInformationAreDisplayed()
        {
            ContactPage contactPage = new ContactPage(driver);

            Assert.AreEqual("", contactPage.GetForenameError());
            Assert.AreEqual("", contactPage.GetEmailError());
            Assert.AreEqual("", contactPage.GetMessageError());
        }

        [When(@"Customer submits all mandatory information")]
        public void WhenCustomerSubmitsAllMandatoryInformation(Table table)
        {
            HomePage homePage = new HomePage(driver);
            ContactPage contactPage = homePage.ClickContactMenu();
            ContactData data = new ContactData(table.Rows.First()["ForeName"],
                                               null, 
                                               table.Rows.First()["Email"], 
                                               table.Rows.First()["Telephone"], 
                                               table.Rows.First()["Message"]);
            this.objectContainer.RegisterInstanceAs(data, "ValidData");
            contactPage.SetForename(data.Forename)
                       .SetEmail(data.Email)
                       .SetTelephone(data.Telephone)
                       .SetMessage(data.Message)
                       .ClickSubmitButton();
        }

        [Then(@"Acknowledgement message is displayed")]
        public void ThenAcknowledgementMessageIsDisplayed()
        {
            ContactData data = this.objectContainer.Resolve<ContactData>("ValidData");
            ContactPage contactPage = new ContactPage(driver);
            Assert.AreEqual("Thanks " + data.Forename + ", we appreciate your feedback.", contactPage.GetSuccessMessage());
        }
    }
}