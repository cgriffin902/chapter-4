using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrice(this IEnumerable<Product> productEnum)
        {
            decimal total = 0;
            foreach (Product prod in productEnum)
            {
                total += prod.Price;
            }
            return total;
        }
        public static IEnumerable<Product> FilterByCatagory(this IEnumerable<Product> productEmum, string categoryParam)
        {
            foreach (Product prod in productEmum)
            {
                if (prod.Category == categoryParam)
                {
                    yield return prod;
                }
            }
        }
        public static IEnumerable<Product> Filter(this IEnumerable<Product> ProductEnum, Func<Product, bool> selctorParam)
        {
            foreach (Product prod in ProductEnum)
            {
                if (selctorParam(prod))
                {
                    yield return prod;
                }
            }
        }
    }

}