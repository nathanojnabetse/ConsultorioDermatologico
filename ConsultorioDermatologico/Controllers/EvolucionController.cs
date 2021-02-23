using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
    public class EvolucionController : Controller
    {
        // GET: Evolucion
        public ActionResult Index(int idPaciente)
        {
            List<EvolucionCLS> listaEvoluciones = new List<EvolucionCLS>();
           
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                //List<SelectListItem> listaIdentidadGenero;
                //using (var bd = new BDD_ConsultorioDermatologicoEntities())
                //{
                //    listaIdentidadGenero = (from identidadGenero in bd.tblIdentidadGenero
                //                            select new SelectListItem
                //                            {
                //                                Text = identidadGenero.nombreIdentidadGenero,
                //                                Value = identidadGenero.idIdentidadGenero.ToString()
                //                            }).ToList();
                //    listaIdentidadGenero.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                //    ViewBag.listaIdentidadGenero = listaIdentidadGenero;
                //}
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();
                ViewBag.nombrePaciente = tblPaciente.nombres+ " " +tblPaciente.apellidos;

                listaEvoluciones = (from evolucion in bd.tblEvolucion
                                    join historiaClinica in bd.tblHistoriaClinica
                                    on evolucion.idHistoriaClinica equals historiaClinica.idHistoriaClinica
                                    join paciente in bd.tblPaciente
                                    on historiaClinica.idPaciente equals paciente.idPaciente
                                    where evolucion.habilitado ==1
                                    && paciente.idPaciente == idPaciente
                                    select new EvolucionCLS
                                    {
                                        motivoConsulta = evolucion.motivoConsulta,
                                        fechaVisita = (DateTime)evolucion.fechaVisita
                                    }).ToList();
            }
                return View(listaEvoluciones);
        }
    }
}