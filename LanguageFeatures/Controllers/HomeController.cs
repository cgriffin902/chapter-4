using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;
namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navagate to a Url To show an exsample";
        }
        public ViewResult AutoProperty()
        {
            //instanciates a new product
            Product myProduct = new Product();

            //Sets the property value
            myProduct.Name = "Chris";
            myProduct = new Product()
           {
               Name = "Chris",
               Description = "The best in the world",
               Category = "People",
               Price = 9999M,
               ProductId = 1,
           };

            //get the property
            string productName = myProduct.Name;

            //Generates the property
            return View("Index", (object)String.Format("Product name:: {0}", productName));
        }
        //other Actions and methods
        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int>() { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>() { { "apple", 10 }, { "orange", 20 }, { "plum", 30 } };
            return View("Index", (object)stringArray[1]);
        }

        public ViewResult UseExtensionEnumerable()
        {
            //enum List
            IEnumerable<Product> products = new ShoppingCart()
            {
                Products = new List<Product>{
                    new Product{Name = "Comic Book", Price=99M},
                    new Product{Name = "Movie", Price=10M},
                    new Product{Name = "Something", Price=20M},
                    new Product{Name = "Karate", Price=30M}
               }
            };
            //Create and populate An Array of product Objects
            Product[] productArray ={
                    new Product{Name = "Comic Book", Price=99M},
                    new Product{Name = "Movie", Price=10M},
                    new Product{Name = "Something", Price=20M},
                    new Product{Name = "Karate", Price=30M},

                                    };
            //Get Total value of the products in the cart
            decimal cartTotal = products.TotalPrice();
            decimal arrayTotal = productArray.TotalPrice();

            return View("Index", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }
        public ViewResult UseExtentionMethod()
        {
            //Create and Populate
            ShoppingCart cart = new ShoppingCart()
            {
                Products = new List<Product>() {
                           new Product{Name ="Comic Book", Price = 99M},
                           new Product{Name ="Movie", Price = 10M},
                           new Product{Name ="Something", Price = 20M},
                           new Product{Name ="Karate", Price = 100M}
                   }
            };
            //Get the total of the products in Products
            decimal cartTotal = cart.TotalPrice();
            return View("Index", (object)String.Format("Total:{0:c}", cartTotal));
        }
        public ViewResult UserFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>{
                  new Product{ Name= "Cool Book", Category= "Book", Price = 99M},
                  new Product{ Name= "Movie", Category= "Movie", Price = 10M},
                  new Product{ Name= "Somthing", Category= "Book", Price = 20M},
                  new Product{ Name= "Karate", Category= "Movie", Price = 30M},
                }
            };
            string cat = "Movie";
            string prodName = "Karate";
            /* Func<Product, bool> catagoryFilter = delegate(Product prod)
             {
                 return prod.Category == cat;
             };*/
            // Func<Product,bool> catagoryFilter = prod => prod.Category == cat;
            decimal total = 0;
            foreach (Product prod in
                /*products.FilterByCatagory(cat)*/
                products.Filter(prod => prod.Category == cat && prod.Name == prodName))
            {
                total += prod.Price;
            }
            return View("Index", (object)String.Format("it will be {0} for the {1}s", total, cat));

        }
        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]{
                new{Name = "MVC", Catagory = "Pattern"},
                new{Name = "Hat", Catagory = "Clothing"},
                new{Name = "Apple", Catagory = "Fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Index", (object)result.ToString());
        }
        public ViewResult FindProducts()
        {
            Product[] products ={
                                 new Product{Name = "Cool Book", Category ="Book", Price= 99M},
                                 new Product{Name = "Movie", Category ="Movie", Price= 10M},
                                 new Product{Name = "Somthing", Category ="Book", Price= 20M},
                                 new Product{Name = "Karate", Category ="Movie", Price= 30M},
                                };
            //create the result using SQL
            /*var foundProducts = from match in products
                                orderby match.Price descending
                                select new { match.Name, match.Price };
*/
            //create the result using LINQ
            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(4)
                .Select(e => new { e.Name, e.Price });
            int count = 0;
            var results = products.Sum(e => e.Price);
            products[2] = new Product { Name = "House", Price = 550M };
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} Name: {1}", p.Price, p.Name);
                /*if (++count == 4)
                {
                    break;
                }*/
            }
            //return View("Index", (object)result.ToString());
            return View("Index", (object)String.Format("Sum of All = {0:c}", results));
        }
    }

}