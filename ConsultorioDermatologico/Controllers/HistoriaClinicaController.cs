using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
    public class HistoriaClinicaController : Controller
    {
        // GET: HistoriaClinica
        public ActionResult Index()
        {
            return View();
        }

        public void llenarSeguros()
        {
            List<SelectListItem> listaSeguros;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaSeguros = (from Seguros in bd.tblSeguroMedico
                                select new SelectListItem
                                        {
                                            Text = Seguros.nombreSeguro,
                                            Value = Seguros.idSeguroMedico.ToString()
                                        }).ToList();
                listaSeguros.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSeguros = listaSeguros;
            }
        }

        public void llenarSangre()
        {
            List<SelectListItem> listaSangre;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaSangre = (from Seguros in bd.tblSeguroMedico
                                select new SelectListItem
                                {
                                    Text = Seguros.nombreSeguro,
                                    Value = Seguros.idSeguroMedico.ToString()
                                }).ToList();
                listaSangre.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSeguros = listaSangre;
            }
        }
    }
}