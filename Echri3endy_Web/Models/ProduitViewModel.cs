using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Echri3endy_Web.Models
{
    public class ProduitViewModel
    {
        public string  ProduitTitele { get; set; }
        public IEnumerable <VenderPourProduit > Items { get; set; }
    }
}