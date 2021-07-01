using System;
using OpenQA.Selenium;
using JupiterToys.Model.Components.UI;

namespace JupiterToys.Model.Pages {
    public class CartPage : BasePage<CartPage> {
        public CartPage(IWebDriver driver) : base(driver) {
        }

        public double GetSubtotal(string item) {
            Table cartTable = new Table(driver.FindElement(By.ClassName("cart-items")));
            string cellValue = cartTable.GetCellValue("Item", item, "Subtotal").Text;
            return Double.Parse(cellValue.Replace("$", ""));
        }

        public int GetQuantity(string item) {
            Table cartTable = new Table(driver.FindElement(By.ClassName("cart-items")));
            string cellValue = cartTable.GetCellValue("Item", item, "Quantity").FindElement(By.TagName("input")).GetAttribute("value");
            return Int32.Parse(cellValue);
        }

        public double GetPrice(string item)
        {
            Table cartTable = new Table(driver.FindElement(By.ClassName("cart-items")));
            string cellValue = cartTable.GetCellValue("Item", item, "Price").Text;
            return Double.Parse(cellValue.Replace("$", ""));
        }

        public double Total => Double.Parse(this.driver.FindElement(By.ClassName("total")).Text.Replace("Total: ", ""));
    }
}
