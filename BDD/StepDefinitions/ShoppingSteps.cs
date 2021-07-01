using BoDi;
using JupiterToys.Model.Data;
using JupiterToys.Model.Pages;
using JupiterToys.Model.Components;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace JupiterToys.BDD.StepDefinitions
{
    [Binding]
    public class ShoppingSteps : BaseSteps
     {
        public ShoppingSteps(IObjectContainer objectContainer) : base(objectContainer) {

        }

        [Given(@"A product with name (.*) is in the shop catalog")]
        public void GivenAProductWithNameIsInTheShopCatalog(string productName)
        {
            var homePage = new HomePage(driver);
            var shopPage = homePage.ClickShopMenu();
            var product = shopPage.GetProduct(p => p.Title.Equals(productName));
            objectContainer.RegisterInstanceAs(product, "product");
        }

        [Then(@"Price should be (.*)")]
        public void ThenPriceShouldBe(double productPrice)
        {
            var product = objectContainer.Resolve<Product<ShopPage>>("product");
            Assert.AreEqual(productPrice, product.Price);
        }

        [Given(@"A customer buys the product with name (.*)")]
        public void WhenACustomerBuysTheProductWithName(string productName)
        {
            HomePage homePage = new HomePage(driver);
            ShopPage shopPage = homePage.ClickShopMenu();
            objectContainer.RegisterInstanceAs(new ShopData(null, shopPage.CartCount), "shopData");

            var testProduct = shopPage.GetProduct(p => p.Title.Equals(productName));
            objectContainer.RegisterInstanceAs(new ProductData(null, testProduct.Price), "productData");
            testProduct.ClickBuyButton();
            
        }

        [Then(@"The product with name (.*) must be added to the cart")]
        public void ThenTheProductWithNameMustBeAddedToTheCart(string productName)
        {
            ShopData shopData = objectContainer.Resolve<ShopData>("shopData");
            ProductData productData = objectContainer.Resolve<ProductData>("productData");

            ShopPage shopPage = new ShopPage(driver);
            Assert.AreEqual(shopData.CartCount + 1, shopPage.CartCount);
            
            CartPage cartPage = shopPage.ClickCartMenu();
            double subtotal = cartPage.GetSubtotal(productName);
            Assert.AreEqual(productData.Price, subtotal);
        }

        [Given(@"A customer buys all products cheaper than \$(.*)")]
        public void WhenACustomerBuysAllProductsCheaperThan(double amount)
        {
            var productData = new List<ProductData>();

            HomePage homePage = new HomePage(driver);
            ShopPage shopPage = homePage.ClickShopMenu();
            var products = shopPage.GetProducts(p => p.Price < amount);
            foreach (var product in products)
            {
                product.ClickBuyButton();
                productData.Add(
                    new ProductData(product.Title, product.Price)
                );
            }
            objectContainer.RegisterInstanceAs(new ShopData(productData, productData.Count), "shopData");
        }

        [Then(@"The products must be added to the cart")]
        public void ThenTheProductsMustBeAddedToTheCart()
        {
            ShopData shopData = objectContainer.Resolve<ShopData>("shopData");

            ShopPage shopPage = new ShopPage(driver);
            Assert.AreEqual(shopData.Products.Count, shopPage.CartCount);
        }

        [Given(@"A customer buys the cheapest product")]
        public void WhenACustomerBuysTheCheapestProduct()
        {
            HomePage homePage = new HomePage(driver);
            ShopPage shopPage = homePage.ClickShopMenu();

            var products = shopPage.GetProducts(p => true);
            var min = products.Min(pr => pr.Price);
            var testProduct = products.First(p => System.Math.Abs(p.Price - min) < 0.001);
            var productData = new ProductData(testProduct.Title, testProduct.Price);
            testProduct.ClickBuyButton();
            objectContainer.RegisterInstanceAs(new ShopData(null, 1), "shopData");
            objectContainer.RegisterInstanceAs(productData, "productData");
        }

        [Then(@"The product must be added to the cart")]
        public void ThenTheProductMustBeAddedToTheCart()
        {
            ShopData shopData = objectContainer.Resolve<ShopData>("shopData");
            ProductData productData = objectContainer.Resolve<ProductData>("productData");

            ShopPage shopPage = new ShopPage(driver);
            Assert.AreEqual(shopData.CartCount, shopPage.CartCount);

            CartPage cartPage = shopPage.ClickCartMenu();
            double subtotal = cartPage.GetSubtotal(productData.Title);
            Assert.AreEqual(productData.Price, subtotal);
        }
    }
}


