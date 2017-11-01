using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autotal.Models;
using Autotal.Factories;
using System.Web.Security;

namespace Autotal.Areas.Admin.Controllers
{

    [Authorize]
    public class CMSController : Controller
    {
        public DBContext context = new DBContext();

        #region Login
        [AllowAnonymous]
        public ActionResult Login(string returnurl)
        {
            TempData["ReturnURL"] = returnurl;
            return View();
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public ActionResult LoginSubmit(string email, string password, string rememberMe)
        {
            CMSUser user = context.CMSUserFactory.Login(email, password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(email, Convert.ToBoolean(rememberMe));
                Session["CMSUser"] = user;
                string returnurl = TempData["ReturnURL"]?.ToString();
                if (returnurl == null)
                {
                    returnurl = "/Admin/CMS/Index";
                }

                return Redirect(returnurl);
            }
            else
            {
                TempData["LoginError"] = "Wrong email or password";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("CMSUser");
            return RedirectToAction("Login");
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            List<ProductVM> products = CreateListProductVM(context.ProductFactory.GetAll());
            return View(products);
        }

        public ActionResult EditProduct(int id = 0)
        {
            ProductVM productVM = null;

            ViewBag.Brands = context.BrandFactory.GetAll();
            ViewBag.Colors = context.ColorFactory.GetAll();

            if (id == 0)
            {
                productVM = new ProductVM();
                productVM.Brand = new Brand();
                productVM.Color = new Color();
                productVM.Images = new List<Image>();
                productVM.Product = new Product();
                productVM.Images = context.ImageFactory.GetAllBy("ProductID", 0);
                return View(productVM);
            }
            else
            {
                productVM = CreateProductVM(context.ProductFactory.Get(id));
                productVM.Images.AddRange(context.ImageFactory.GetAllBy("ProductID", 0));
                return View(productVM);
            }
        }

        [HttpPost]
        public ActionResult EditProductSubmit(Product product, List<int> imageIDs)
        {
            if (product.ID > 0)
            {
                context.ProductFactory.Update(product);
                for (int i = 0; i < imageIDs.Count; i++)
                {
                    Image img = context.ImageFactory.Get(imageIDs[i]);
                    img.ProductID = product.ID;
                    context.ImageFactory.Update(img);
                }

                foreach (Image img in context.ImageFactory.GetAllBy("ProductID", product.ID))
                {
                    if (imageIDs.Contains(img.ID))
                    {
                        continue;
                    }
                    img.ProductID = 0;
                    context.ImageFactory.Update(img);
                }
                TempData["MSG"] = "The Product has been Edited";
            }
            else
            {
                context.ProductFactory.Insert(product);
                for (int i = 0; i < imageIDs.Count; i++)
                {
                    Image img = context.ImageFactory.Get(imageIDs[i]);
                    img.ProductID = context.ProductFactory.GetLatest().ID;
                    context.ImageFactory.Update(img);
                }

                TempData["MSG"] = "The Product has been Added";
            }

            return RedirectToAction("Products");
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

        #region Subpage

        public ActionResult Subpages()
        {
            return View(context.SubpageFactory.GetAll());
        }

        public ActionResult EditSubpage(int id = 0)
        {
            if (id > 0)
            {
                return View(context.SubpageFactory.Get(id));
            }
            else
            {
                Subpage subpage = new Subpage();
                subpage.ID = 0;
                subpage.Title = "";
                subpage.Content = "";
                return View(subpage);
            }
        }

        [HttpPost]
        public ActionResult EditSubpageSubmit(Subpage subpage)
        {
            if (subpage.ID > 0)
            {
                context.SubpageFactory.Update(subpage);

                TempData["MSG"] = "Subpage has been updated";
            }
            else
            {
                context.SubpageFactory.Insert(subpage);
                TempData["MSG"] = "Subpage has been added";
            }

            return RedirectToAction("Subpages");
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