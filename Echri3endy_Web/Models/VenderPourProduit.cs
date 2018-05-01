using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Echri3endy_Web.Models
{
    public class VenderPourProduit
    {
        public int Id { get; set; }

        public string  Messager { get; set; }

        public DateTime  DemandeVnDate { get; set; }

        public int produitId { get; set; }

        public string UserId { get; set; }

        public virtual  Produit produit { get; set; }

        public virtual  ApplicationUser user { get; set; }

    }
}