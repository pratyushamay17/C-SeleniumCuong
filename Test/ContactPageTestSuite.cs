
using JupiterToys.Model.Data;
using JupiterToys.Model.Pages;
using JupiterToys.Test.DataProviders;
using NUnit.Framework;

namespace JupiterToys.Test
{
    
    public class ContactPageTestSuite : BaseTestSuite
    {

        [Test]
        public void VerifyMandatoryErrorMessages()
        {

            var contactPage = open<HomePage>().ClickContactMenu().ClickSubmitButton();

            Assert.AreEqual("Forename is required", contactPage.GetForenameError());
            Assert.AreEqual("Email is required", contactPage.GetEmailError());
            Assert.AreEqual("Message is required", contactPage.GetMessageError());

            contactPage.SetForename("Juan")
                .SetEmail("jflorez@planittesting.com")
                .SetMessage("sample");

            Assert.AreEqual("", contactPage.GetForenameError());
            Assert.AreEqual("", contactPage.GetEmailError());
            Assert.AreEqual("", contactPage.GetMessageError());

        }

        [Test]
        public void TestDataValidation()
        {
            var contactPage = open<HomePage>().ClickContactMenu();

            contactPage.ClickSubmitButton()
                .SetForename("Juan")
                .SetEmail("jflorez")
                .SetTelephone("yetef")
                .SetMessage("sample");
            Assert.AreEqual("Please enter a valid email", contactPage.GetEmailError());
            Assert.AreEqual("Please enter a valid telephone number", contactPage.GetTelephoneError());
        }


        [TestCaseSource(typeof(ContactDataProvider))]
        public void TestSuccessfulSubmission(ContactData data)
        {
            var message = open<HomePage>()
                .ClickContactMenu()
                .SetContactData(data)
                .ClickSubmitButton()
                .GetSuccessMessage();
            Assert.AreEqual("Thanks " + data.Forename + ", we appreciate your feedback.", message);
        }
    }
}
