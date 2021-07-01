using TechTalk.SpecFlow;
using OpenQA.Selenium;
using JupiterToys.Model.Pages;
using NUnit.Framework;
using BoDi;
using System.Collections.Generic;
using JupiterToys.Model.Components;
using System;
using System.Linq;

namespace JupiterToys.BDD.StepDefinitions
{
    [Binding]
    public class CartSteps : BaseSteps
    {
        public CartSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [Given(@"a customer adds following items to the cart")]
        public void GivenACustomerAddsFollowingItemsToTheCart(Table table)
        {
            var productPrices = new Dictionary<string, double>();
            var shopPage = new HomePage(driver).ClickShopMenu();
            foreach (TableRow row in table.Rows)
            {
                var product = shopPage.GetProduct(p => p.Title.Equals(row["ProductName"]));
                productPrices.Add(product.Title, product.Price);

                for (int ctr = 0; ctr < Convert.ToInt32(row["Quantity"]); ctr++)
                {
                    product.ClickBuyButton();
                }
            }
            objectContainer.RegisterInstanceAs(productPrices, "productPrices");
        }

        [Then(@"sub total should be correct for all items")]
        public void ThenSubTotalShouldBeCorrectForAllItems()
        {
            var productPrices = objectContainer.Resolve<Dictionary<string, double>>("productPrices");
            var shopPage = new ShopPage(driver);
            var cartPage = shopPage.ClickCartMenu();
            foreach (string productName in productPrices.Keys)
            {
                var actualPrice = cartPage.GetPrice(productName);
                Assert.AreEqual(productPrices[productName], actualPrice);
                Assert.AreEqual(actualPrice * cartPage.GetQuantity(productName), cartPage.GetSubtotal(productName));
            }
        }

        [Then(@"total price of items should be correct")]
        public void ThenTotalPriceOfItemsShouldBeCorrect()
        {
            var productPrices = objectContainer.Resolve<Dictionary<string, double>>("productPrices");
            var cartPage = new CartPage(driver);
            double total = productPrices.Sum(p => cartPage.GetSubtotal(p.Key));
            Assert.AreEqual(total, cartPage.Total);
        }

    }
}
