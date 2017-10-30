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
        public DBContext context = new DBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            List<ProductVM> products = CreateListProductVM(context.ProductFactory.GetAll());
            return View(products);
        }

        public ActionResult Images()
        {
            return View(context.ImageFactory.GetAll());
        }

        [HttpPost]
        public ActionResult CreateImages(List<HttpPostedFileBase> list)
        {
            foreach (HttpPostedFileBase file in list)
            {
                string fileName = "";
                if (Upload.Image(file, Request.PhysicalApplicationPath + @"/Content/Images/Products/", out fileName))
                {
                    Upload.Image(file, Request.PhysicalApplicationPath + @"/Content/Images/Products/", "tn_" + fileName, 400);

                    Image image = new Image();
                    image.ImageURL = fileName;
                    image.ProductID = 0;
                    image.SubpageID = 0;
                    image.Alt = "";

                    context.ImageFactory.Insert(image);
                }
            }

            TempData["MSG"] = "Images has been uploaded";
            return RedirectToAction("Images");
        }

        #region CMS Color
        public ActionResult Colors()
        {
            return View(context.ColorFactory.GetAll());
        }

        public ActionResult EditColor(int id = 0)
        {
            ViewBag.AddColor = (id == 0);

            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(context.ColorFactory.Get(id));
            }
        }

        [HttpPost]
        public ActionResult EditColorSubmit(Color color)
        {
            if (color.ID > 0)
            {
                context.ColorFactory.Update(color);

                TempData["MSG"] = "Color hass been updated.";
            }
            else
            {
                context.ColorFactory.Insert(color);

                TempData["MSG"] = "Color hass been added.";
            }

            return RedirectToAction("Colors");
        }

        public ActionResult DeleteColor(int id)
        {
            context.ColorFactory.Delete(id);
            return RedirectToAction("Colors");
        }
        #endregion

        #region CMS Brand
        public ActionResult Brands()
        {
            return View(context.BrandFactory.GetAll());
        }

        public ActionResult EditBrand(int id = 0)
        {
            ViewBag.AddBrand = (id == 0);
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(context.BrandFactory.Get(id));
            }
        }

        [HttpPost]
        public ActionResult EditBrandSubmit(Brand brand)
        {
            if (brand.ID > 0)
            {
                context.BrandFactory.Update(brand);
            }
            else
            {
                context.BrandFactory.Insert(brand);
            }
            return RedirectToAction("Brands");
        }

        public ActionResult DeleteBrand(int id)
        {
            context.BrandFactory.Delete(id);
            TempData["MSG"] = "Brand has been deleted.";
            return RedirectToAction("Brands");
        } 
        #endregion

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