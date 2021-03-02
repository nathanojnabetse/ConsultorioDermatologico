using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
    public class Cie10Controller : Controller
    {
        // GET: Cie10
        public ActionResult Index()
        {
            List<Cie10CLS> listaEnfermedades = new List<Cie10CLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaEnfermedades =( from cie10 in bd.tblCIE10
                                    select new Cie10CLS
                                    {
                                        codigo = cie10.codigo,
                                        enfermedad =cie10.enfermedad
                                    }).ToList();
            }

            return View(listaEnfermedades);
        }

        public ActionResult Filtro(String busqueda)
        {
            List<Cie10CLS> listaEnfermedades = new List<Cie10CLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                if (busqueda == null)
                {
                    listaEnfermedades = (from cie10 in bd.tblCIE10
                                         select new Cie10CLS
                                         {
                                             codigo = cie10.codigo,
                                             enfermedad = cie10.enfermedad
                                         }).ToList();
                }
                else
                {
                    listaEnfermedades = (from cie10 in bd.tblCIE10
                                         where cie10.enfermedad.Contains(busqueda)
                                         select new Cie10CLS                                        
                                         {
                                             codigo = cie10.codigo,
                                             enfermedad = cie10.enfermedad
                                         }).ToList();
                }
                                
                return PartialView("_TablaCIE", listaEnfermedades);
            }
        }
    }
}