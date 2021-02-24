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
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();
                ViewBag.nombrePaciente = tblPaciente.nombres+ " " +tblPaciente.apellidos;
                ViewBag.idPaCiente = tblPaciente.idPaciente;
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == tblPaciente.idPaciente).First();
                ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;

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

        public ActionResult Agregar(int idHistoriaClinica)
        {
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == idHistoriaClinica).First();
                ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;

                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente== tblHistoriaClinica.idPaciente).First();
                ViewBag.nombrePaciente = tblPaciente.nombres + " " + tblPaciente.apellidos;

                ViewBag.fechaActual = System.DateTime.Now.ToString("yyyy-MM-dd");
            }

                return View();
        }
    }
}