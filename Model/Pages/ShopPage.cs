using System;
using System.Linq;
using OpenQA.Selenium;
using JupiterToys.Model.Components;
using System.Collections.Generic;

namespace JupiterToys.Model.Pages {
    public class ShopPage : BasePage<ShopPage> {
        public ShopPage(IWebDriver driver) : base(driver) {
        }

        public Product<ShopPage> GetProduct(Func<Product<ShopPage>, bool> comparator) => ProductsIterator.First(p => comparator(p));

        public List<Product<ShopPage>> GetProducts(Func<Product<ShopPage>, bool> comparator) => ProductsIterator.Where(p => comparator(p)).ToList();

        public List<Product<ShopPage>> GetProducts() => ProductsIterator.Where(p => true).ToList();
         
        private IEnumerable<Product<ShopPage>> ProductsIterator => driver.FindElements(By.ClassName("product")).Select(e => new Product<ShopPage>(e, this));
    }
}
