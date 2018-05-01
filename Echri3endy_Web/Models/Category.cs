using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Echri3endy_Web.Models
{
    public class Category
    {
        public int id  { get; set; }
        [Required]
        [Display(Name = "Type")]
        public String categoryNom { get; set; }
        [Required]
        [Display(Name ="Discription")]
        public String categoryDiscription { get; set; }

        public virtual ICollection<Produit>  Produits{ get; set; }
    }
}