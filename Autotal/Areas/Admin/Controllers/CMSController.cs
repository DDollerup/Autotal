using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autotal.Models;
using Autotal.Factories;

namespace Autotal.Areas.Admin.Controllers
{
    public class CMSController : Controller
    {
        DBContext context = new DBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View(CreateListProductVM(context.ProductFactory.GetAll()));
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
    }
}