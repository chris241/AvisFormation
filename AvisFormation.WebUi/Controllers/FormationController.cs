using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
     
        {
            List<Formation> listFormation = new List<Formation>();
            using (var context = new AvisEntities())
            {
                listFormation = context.Formation.ToList();
            }
                
            return View(listFormation);
        }
        public ActionResult DetailsFormation(string nomSeo)
        {
            var vm =new  FormationAvecAvisViewModel();
            using (var context = new AvisEntities())
            {
                var formation = context.Formation.FirstOrDefault(f=>f.NomSeo == nomSeo);
                if(formation == null)
                {
                    return RedirectToAction("Accueil", "home");
                }
                vm.FormationNom = formation.Nom;
                vm.FormationDescription = formation.Description;
                vm.FormationNomSeo = nomSeo;
                vm.FormationUrl = formation.Url;
                vm.Note = formation.Avis.Average(a => a.Note);
                vm.NombreAvis = formation.Avis.Count;
                vm.Avis = formation.Avis.ToList();
            }

            return View(vm);
        }
    }
}