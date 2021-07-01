using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using JupiterToys.Model.Data;

namespace JupiterToys.Test.DataProviders
{
    public class ContactDataProvider : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return new CsvReader(new StreamReader("Test/Data/ContactData.csv"), CultureInfo.CurrentCulture)
                        .GetRecords<ContactData>()
                        .Select(contact => new object[] { contact }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
