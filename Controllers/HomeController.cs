using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProdsCats.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ProdsCats.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> allProducts = dbContext.Products.ToList();
            ViewBag.allProducts = allProducts;
            return View();
        }

        [Route("createproduct")]
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                System.Console.WriteLine("Added", product.Name);
                return RedirectToAction("Index");
            }
            List<Product> allProducts = dbContext.Products.ToList();
            ViewBag.allProducts = allProducts;
            return View("Index");
        }

        [Route("category")]
        [HttpGet]
        public IActionResult Category()
        {
            List<Category> allCategories = dbContext.Categories.ToList();
            ViewBag.allCategories = allCategories;
            return View();
        }

        [Route("createcategory")]
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                System.Console.WriteLine("Added", category.Name);
                return RedirectToAction("Category");
            }
            List<Category> allCategories = dbContext.Categories.ToList();
            ViewBag.allCategories = allCategories;
            return View("Category");
        }

        [Route("productinfo/{pId}")]
        [HttpGet]
        public IActionResult ProductInfo(int pId)
        {
            Product product = dbContext.Products
                .Include(j => j.Categories)
                .ThenInclude(k => k.Category)
                .FirstOrDefault(i => i.Id == pId );
            ViewBag.ThisProduct = product;

            List<Category> allCategories = dbContext.Categories.ToList();
            List<Category> myCategories = new List<Category>();
            foreach (Category i in allCategories)
            {
                bool Unique = true;
                foreach (var j in product.Categories)
                {
                    if (j.Category.Id == i.Id)
                    {
                        Unique = false;
                        break;
                    }
                }
                if(Unique) 
                {
                    myCategories.Add(i);
                }
            }
            ViewBag.MyCategories = myCategories;
            return View();
        }

        [Route("addproduct/{pId}")]
        [HttpPost]
        public IActionResult AddProduct(Add category, int pId)
        {
            Category addCat = dbContext.Categories
                .FirstOrDefault(i => i.Id == category.FindId );
            Association newAssocation = new Association();
            newAssocation.CategoryId = addCat.Id;
            newAssocation.ProductId = pId;
            dbContext.Associations.Add(newAssocation);
            dbContext.SaveChanges();
            return RedirectToAction("ProductInfo", new {pId = pId});
        }

        [Route("categoryinfo/{cId}")]
        [HttpGet]
        public IActionResult CategoryInfo(int cId)
        {
            Category category = dbContext.Categories
                .Include(j => j.Products)
                .ThenInclude(k => k.Product)
                .FirstOrDefault(i => i.Id == cId );
            ViewBag.ThisCategory = category;

            List<Product> allProducts = dbContext.Products.ToList();
            List<Product> myProducts = new List<Product>();
            foreach (Product i in allProducts)
            {
                bool Unique = true;
                foreach (var j in category.Products)
                {
                    if (j.Product.Id == i.Id)
                    {
                        Unique = false;
                        break;
                    }
                }
                if(Unique) 
                {
                    myProducts.Add(i);
                }
            }
            ViewBag.MyProducts = myProducts;
            return View();
        }

        [Route("addcategory/{cId}")]
        [HttpPost]
        public IActionResult AddCategory(Add product, int cId)
        {
            Product addProd = dbContext.Products
                .FirstOrDefault(i => i.Id == product.FindId );
            Association newAssocation = new Association();
            newAssocation.ProductId = addProd.Id;
            newAssocation.CategoryId = cId;
            dbContext.Associations.Add(newAssocation);
            dbContext.SaveChanges();
            return RedirectToAction("CategoryInfo", new {cId = cId});
        }
    }
}
