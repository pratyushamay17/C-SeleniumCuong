using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace JupiterToys.Model.Components {
    public class Product<T> {
        private IWebElement element;
        private T parent;

        public Product(IWebElement element, T parent) {
            this.element = element;
            this.parent = parent;
        }

        public T Parent { get => parent; }

        public string Title => element.FindElement(By.ClassName("product-title")).Text;

        public double Price
        {
            get
            {
                string price = element.FindElement(By.ClassName("product-price")).Text;
                price = Regex.Replace(price, @"[^0-9.]", "");
                return Double.Parse(price);
            }
        }

        public Product<T> ClickBuyButton() {
            element.FindElement(By.ClassName("btn")).Click();
            return this;
        }
    }
}
