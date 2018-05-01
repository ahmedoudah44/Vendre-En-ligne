using Echri3endy_Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int ProduitId)
        {
            var produit = db.Produits.Find(ProduitId);

            if (produit == null)
            {
                return HttpNotFound();
            }
            Session["ProduitId"] = ProduitId;
            return View(produit);

        }
        [Authorize ]
        public ActionResult Demande()
        {
            return View();
        }
        [HttpPost ]
        public ActionResult Demande(string Messager   )
        { 
            var UserId = User.Identity.GetUserId();
            var ProduitId = (int)Session["ProduitId"];

            var check = db.VenderPourProduits.Where(a => a.produitId == ProduitId && a.UserId == UserId).ToList();

            if (check.Count < 1)
            {
                var Vnproduit = new VenderPourProduit();

                Vnproduit.UserId = UserId;
                Vnproduit.produitId = ProduitId;
                Vnproduit.Messager = Messager;
                Vnproduit.DemandeVnDate = DateTime.Now;

                db.VenderPourProduits.Add(Vnproduit);
                db.SaveChanges();
                ViewBag.Result = "Ajouté avec succés";
            }
            else
            {
                ViewBag.Result = "Vous ete deja envoiye Demande pour cette Produit";
            }

         

            return View();
        }
        [Authorize(Roles ="userPublic")]
        public ActionResult GetProduitByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Produits = db.VenderPourProduits.Where(a => a.UserId == UserId);
            ViewBag.message = "test";
            ViewBag.p = Produits.Count();
            return View(Produits.ToList());
        }
        [Authorize]
        public ActionResult GetProduitByPublisher()
        {
            var UserId = User.Identity.GetUserId();

            var Produits = from app in db.VenderPourProduits
                           join produit in db.Produits 
                           on app.produitId equals produit .Id 
                           where produit .User.Id == UserId 
                           select app;

            var grouped = from prod in Produits
                          group prod by prod.produit.ProduitTiter
                         into gr
                          select new ProduitViewModel
                          {
                              ProduitTitele = gr.Key,
                              Items = gr
                          };


            return View(grouped .ToList());
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Produits.Where(a => a.ProduitTiter.Contains(searchName)
            || a.ProduitConteni.Contains(searchName)
            || a.category.categoryNom.Contains(searchName)
            || a.category.categoryDiscription.Contains(searchName)).ToList();
            return View(result);
        }
        public ActionResult Contact()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var logineInfo = new NetworkCredential("ahmedoudah44@gmail.com","44233213a");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("ahmedoudah44@gmail.com"));
            mail.Subject = contact.Subject;
            string body = " Nom :" + contact.Name + "</br>" +
              "Email :" + contact.Email + "</br>" +
              "Addresse Message :" + contact.Subject + "</br>" +
              "Message : <b>" + contact.Message + "</br>";
            mail.Body = body;
            mail.Body = contact.Message;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
          
            smtpClient.Credentials = logineInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }


    }
}