using System.Collections.Generic;

namespace JupiterToys.Model.Data
{
    public record ShopData(IList<ProductData> Products, int CartCount){}
}
