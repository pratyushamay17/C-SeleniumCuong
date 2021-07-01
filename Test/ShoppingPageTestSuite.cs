

using System.Collections.Generic;
using System.Linq;
using JupiterToys.Model.Pages;
using NUnit.Framework;

namespace JupiterToys.Test {
    
    public class ShoppingPageTestSuite : BaseTestSuite {
        [Test]
        public void TestProductPrice() {
            
                var price = open<HomePage>()
                                .ClickShopMenu()
                                .GetProduct(p=>p.Title.Equals("Valentine Bear"))
                                .Price;
                Assert.AreEqual(14.99, price);
            
        }

        [Test]
        public void TestBuyProduct() {
            
                var shopPage = open<HomePage>().ClickShopMenu();
                int cartCount = shopPage.CartCount;
                double price = shopPage.GetProduct(p=>p.Title.Equals("Funny Cow"))
                                       .ClickBuyButton()
                                       .Price;
                Assert.AreEqual(cartCount + 1, shopPage.CartCount);
                double subtotal = shopPage.ClickCartMenu().GetSubtotal("Funny Cow");
                Assert.AreEqual(price, subtotal);
            
        }

        [Test]
        public void BuyAllProductsWithCostLessThan() {
            var products = open<HomePage>()
                .ClickShopMenu()
                .GetProducts(p => p.Price < 10);
            products.ForEach(p => p.ClickBuyButton());
            Assert.AreEqual(products.Count, products[0].Parent.CartCount);
        }

        [Test]
        public void BuyCheapestProduct() {
            var products = open<HomePage>()
                .ClickShopMenu()
                .GetProducts();
            var cartCount = products.First(p => System.Math.Abs(p.Price - products.Min(p => p.Price)) < 0.001)
                .ClickBuyButton()
                .Parent.CartCount;
            Assert.AreEqual(1, cartCount);
        }

        [Test]
        public void ValidateProductPrice() {
            var price = open<HomePage>()
                .ClickShopMenu()
                .GetProduct(p => p.Title=="Fluffy Bunny")
                .Price;
            Assert.AreEqual(9.99, price);
        }
    }
}
