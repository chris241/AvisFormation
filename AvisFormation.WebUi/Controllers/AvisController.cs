using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult LaisserUnAvis(string nomSeo)
        {
            var vm = new LaisserUnAvisViewModel();
            vm.NomSeo = nomSeo;
            using (var context = new AvisEntities())
            {
                var formationActuelle = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);
                if (formationActuelle == null)
                {
                    return RedirectToAction("Accueil", "Home");
                }
                vm.FormationName = formationActuelle.Nom;
            }
                return View(vm);
        }
        public ActionResult SaveComment(string commentaire, string nom, string note, string nomSeo)
        {
            Avis nouvelAvis = new Avis();
            nouvelAvis.DateAvis = DateTime.Now;
            nouvelAvis.Description = commentaire;
     
            nouvelAvis.Nom = nom;
            double bNote = 0;
            if(!double.TryParse(note,NumberStyles.Any, CultureInfo.InvariantCulture, out bNote))
            {
                throw new Exception("Impossible de parser la note " + note);
            }
            nouvelAvis.Note = bNote;
            using( var context = new AvisEntities())
            {
                var formationActuelle = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);
                if(formationActuelle == null)
                {
                    return RedirectToAction("Accueil", "Home");
                }
                nouvelAvis.IdFormation = formationActuelle.Id;
                context.Avis.Add(nouvelAvis);
                context.SaveChanges();

            }
            return RedirectToAction("DetailsFormation","Formation", new { nomSeo = nomSeo });
        }
    }
}