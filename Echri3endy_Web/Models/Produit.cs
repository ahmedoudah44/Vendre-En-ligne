using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Echri3endy_Web.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [DisplayName("Nom de Produit")]
        public String ProduitTiter { get; set; }

        [DisplayName("Description")]
        public String ProduitConteni { get; set; }

        [DisplayName("Image Produit ")]
        public String ProduitImager  { get; set; }

        [DisplayName("Prix de Produit ")]
        public double prix { get; set; }

        [DisplayName("Type Produit")]
        public int CategoryId { get; set; }

        public string  UserID { get; set; }

        public virtual Category category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}