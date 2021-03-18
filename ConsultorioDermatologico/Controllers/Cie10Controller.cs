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
        /// <summary>
        /// Muesta la vista con la lista completa de enfermedades
        /// </summary>
        /// <returns>Vista Index con la lista completa de enfermedades</returns>
        public ActionResult Index()
        {
            List<Cie10CLS> listaEnfermedades = new List<Cie10CLS>();

            listaEnfermedades.Add(new Cie10CLS
            {
                codigo = "",
                enfermedad = "Ingrese código o término exacto"
            });

            return View(listaEnfermedades);
        }

        /// <summary>
        /// Filtro de busqueda en base al string de entrada
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns>Vista parcial con los resultados de conicidencia</returns>
        public ActionResult Filtro(String busqueda)
        {
            List<Cie10CLS> listaEnfermedades = new List<Cie10CLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                if (busqueda == null || busqueda == "")
                {
                    listaEnfermedades.Add(new Cie10CLS
                    {
                        codigo = "",
                        enfermedad = "Ingrese código o término exacto"
                    });

                }
                else
                {
                    listaEnfermedades = (from cie10 in bd.tblCIE10
                                         where cie10.enfermedad.Contains(busqueda)
                                         || cie10.codigo.Contains(busqueda)
                                         select new Cie10CLS
                                         {
                                             codigo = cie10.codigo,
                                             enfermedad = cie10.enfermedad,
                                             titulo = cie10.titulo,
                                             capitulo = cie10.capitulo

                                         }).ToList();
                }

                return PartialView("_TablaCIE", listaEnfermedades);
            }
        }
    }
}