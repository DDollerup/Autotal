using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autotal.Factories;
using Autotal.Models;

namespace Autotal.Controllers
{
    public class HomeController : Controller
    {
        DBContext context = new DBContext();

        public ActionResult Index()
        {
            ViewBag.RandomPictures = context.ImageFactory.TakeRandom(3);
            context.ProductFactory.CountBy("BrandID", 1);

            bool test= context.ProductFactory.CreateTable();

            return View(context.SubpageFactory.Get(1));
        }

        public ActionResult Products()
        {
            List<ProductVM> products = null;

            if (TempData["sortedProducts"] == null)
            {
                products = CreateListProductVM(context.ProductFactory.GetAll());
                ViewBag.Title = "All Products";
            }
            else
            {
                products = CreateListProductVM(TempData["sortedProducts"] as List<Product>);
                ViewBag.Title = "Sorteret efter: " + TempData["sortTitle"].ToString().ToUpper();
            }

            return View(products);
        }

        // ID = ProductID
        public ActionResult ShowProduct(int id = 0)
        {
            if (id == null || id > 0)
            {
                Product p = context.ProductFactory.Get(id);
                p.Views++;
                context.ProductFactory.Update(p);
                return View(CreateProductVM(context.ProductFactory.Get(id)));
            }
            else
            {
                return Redirect(Request.UrlReferrer.PathAndQuery);
            }
        }

        public List<ProductVM> CreateListProductVM(List<Product> productsToCreateFrom)
        {
            // Opretter en liste af ProductVM, som indeholder produktet, brand og dets billeder.
            List<ProductVM> allProductsWithImages = new List<ProductVM>();
            // Vi laver en foreach, på alle produkter i Product tabellen igennem
            // context.ProductFactory.GetAll()
            foreach (Product product in productsToCreateFrom)
            {
                // Opretter en ProductVM som skal, efter den er færdig, sættes ind i listen
                // der blev oprettet på linje: 25.
                ProductVM pvm = new ProductVM();
                pvm.Product = product;
                pvm.Color = context.ColorFactory.Get(product.ColorID);
                pvm.Brand = context.BrandFactory.Get(product.BrandID);
                pvm.Images = context.ImageFactory.GetAllBy("ProductID", product.ID);

                allProductsWithImages.Add(pvm);
            }

            return allProductsWithImages;
        }

        public ProductVM CreateProductVM(Product productToCreateFrom)
        {
            ProductVM pvm = new ProductVM();
            pvm.Product = productToCreateFrom;
            pvm.Color = context.ColorFactory.Get(productToCreateFrom.ColorID);
            pvm.Brand = context.BrandFactory.Get(productToCreateFrom.BrandID);
            pvm.Images = context.ImageFactory.GetAllBy("ProductID", productToCreateFrom.ID);

            return pvm;
        }


        #region Sort Products
        public ActionResult SortProductBy()
        {
            List<Product> sortedProducts = context.ProductFactory.GetAll();

            string sortBy = Request.QueryString["sortBy"].ToString();

            switch (sortBy.ToLower())
            {
                case "km":
                    sortedProducts = sortedProducts.OrderBy(x => x.Km).ToList();
                    break;
                case "pris":
                    sortedProducts = sortedProducts.OrderBy(x => x.Price).ToList();
                    break;
                case "visninger":
                    sortedProducts = sortedProducts.OrderByDescending(x => x.Views).ToList();
                    break;
                case "hk":
                    sortedProducts = sortedProducts.OrderByDescending(x => x.BHP).ToList();
                    break;
                default:
                    break;
            }

            TempData["sortedProducts"] = sortedProducts.Take(5).ToList();
            TempData["sortTitle"] = sortBy;

            return RedirectToAction("Products");
        }

        // ID = BrandID
        public ActionResult SortByBrand(int id)
        {
            Brand brand = context.BrandFactory.Get(id);
            TempData["sortedProducts"] = context.ProductFactory.GetAllBy("BrandID", id);
            TempData["sortTitle"] = brand.Name;
            return RedirectToAction("Products");
        }
        #endregion

        #region Search
        // searchQuery er hvad brugeren søger på, igennem søg funktionaliteten på layout'et
        [HttpPost]
        public ActionResult Search(string searchQuery)
        {
            TempData["sortedProducts"] = context.ProductFactory.SearchByJoin<Brand, Color>(searchQuery, "Model", "BHP", "Year", "Km", "Price", "Description");
            TempData["sortTitle"] = "Søgeresultat: ";
            return RedirectToAction("Products");
        }
        #endregion

        [ChildActionOnly]
        public ActionResult UsedCarsList()
        {
            ViewBag.TotalProductCount = context.ProductFactory.Count();

            List<BrandVM> brandsWithCount = new List<BrandVM>();
            foreach (Brand item in context.BrandFactory.GetAll())
            {
                BrandVM brandVM = new BrandVM();
                brandVM.Brand = item;
                brandVM.ProductCount = context.ProductFactory.CountBy("BrandID", item.ID);

                brandsWithCount.Add(brandVM);
            }

            return PartialView(brandsWithCount);
        }

        [ChildActionOnly]
        public ActionResult MostViewedCarsList()
        {
            List<Product> top3MostViewed = context.ProductFactory.GetAll().OrderByDescending(x => x.Views).Take(3).ToList();

            List<ProductVM> productVMList = new List<ProductVM>();

            foreach (Product product in top3MostViewed)
            {
                ProductVM productVM = new ProductVM();
                productVM.Product = product;
                productVM.Brand = context.BrandFactory.Get(product.BrandID);
                productVM.Images = context.ImageFactory.GetAllBy("ProductID", product.ID);

                productVMList.Add(productVM);
            }

            return PartialView(productVMList);
        }

        [ChildActionOnly]
        public ActionResult MostExpensiveCar()
        {
            Product theChosen_Chinese_One_OrWasItJapanese = context.ProductFactory.GetAll().OrderByDescending(x => x.Price).FirstOrDefault();

            ProductVM productVM = new ProductVM();
            productVM.Product = theChosen_Chinese_One_OrWasItJapanese;
            productVM.Brand = context.BrandFactory.Get(theChosen_Chinese_One_OrWasItJapanese.BrandID);
            productVM.Images = context.ImageFactory.GetAllBy("ProductID", theChosen_Chinese_One_OrWasItJapanese.ID);

            return PartialView(productVM);
        }

        [ChildActionOnly]
        public ActionResult SortCars()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView(context.ContactFactory.Get(1));
        }
    }
}