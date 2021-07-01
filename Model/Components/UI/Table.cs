using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JupiterToys.Model.Components.UI
{
    public class Table
    {
        private IWebElement element;
        public Table(IWebElement element)
        {
            this.element = element;

        }

        private int GetColumnIndex(string column)
        {
            IReadOnlyCollection<IWebElement> headerCells = element.FindElements(By.TagName("th"));
            return Enumerable.Range(0, headerCells.Count)
                             .First(i => headerCells.ElementAt(i).Text.Equals(column));
        }

        public IWebElement GetCellValue(string findColumn, string findValue, string returnColumn)
        {
            int findColumnIndex = this.GetColumnIndex(findColumn);
            int returnColumnIndex = this.GetColumnIndex(returnColumn);
            return this.element.FindElements(By.CssSelector("tbody tr"))
                               .Select(row=>row.FindElements(By.TagName("td")))
                               .Where(cells=>cells.ElementAt(findColumnIndex).Text.Equals(findValue))
                               .Select(cells=>cells.ElementAt(returnColumnIndex)).First();
        }
    }
}
