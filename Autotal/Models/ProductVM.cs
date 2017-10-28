using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autotal.Models
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        public List<Image> Images { get; set; }

        public Image GetFirstImage()
        {
            if (Images?.Count > 0)
            {
                return Images[0];
            }

            return new Image() { ImageURL = "no-image.png", Alt = Product.Model };
        }
    }
}