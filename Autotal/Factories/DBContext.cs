using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autotal.Models;

namespace Autotal.Factories
{
    public class DBContext
    {
        private AutoFactory<Brand> brandFactory;
        private AutoFactory<Color> colorFactory;
        private AutoFactory<Image> imageFactory;
        private AutoFactory<Product> productFactory;
        private AutoFactory<Subpage> subpageFactory;
        private ContactFactory contactFactory;

        public AutoFactory<Brand> BrandFactory
        {
            get
            {
                if (brandFactory == null)
                {
                    brandFactory = new AutoFactory<Brand>();
                }
                return brandFactory;
            }
        }
        public AutoFactory<Color> ColorFactory
        {
            get
            {
                if (colorFactory == null)
                {
                    colorFactory = new AutoFactory<Color>();
                }
                return colorFactory;
            }
        }
        public AutoFactory<Image> ImageFactory
        {
            get
            {
                if (imageFactory == null)
                {
                    imageFactory = new AutoFactory<Image>();
                }
                return imageFactory;
            }
        }
        public AutoFactory<Product> ProductFactory
        {
            get
            {
                if (productFactory == null)
                {
                    productFactory = new AutoFactory<Product>();
                }
                return productFactory;
            }
        }
        public AutoFactory<Subpage> SubpageFactory
        {
            get
            {
                if (subpageFactory == null)
                {
                    subpageFactory = new AutoFactory<Subpage>();
                }
                return subpageFactory;
            }
        }
        public ContactFactory ContactFactory
        {
            get
            {
                if (contactFactory == null)
                {
                    contactFactory = new ContactFactory();
                }
                return contactFactory;
            }
        }
    }
}