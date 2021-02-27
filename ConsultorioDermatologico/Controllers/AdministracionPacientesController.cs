using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
    public class AdministracionPacientesController : Controller
    {
        
        // GET: AdministracionPacientes
        public ActionResult Index()
        {
            List<PacienteCLS> listaPacientesDesactivados = new List<PacienteCLS>();
            List<EvolucionCLS> listaEvolucionesDesactivadas = new List<EvolucionCLS>();
            
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaPacientesDesactivados = (from historiaClinica in bd.tblHistoriaClinica
                                              join paciente in bd.tblPaciente
                                              on historiaClinica.idPaciente equals paciente.idPaciente
                                              where historiaClinica.habilitado == 0
                                              && paciente.habilitado == 0
                                              select new PacienteCLS
                                              {
                                                  idPaciente = paciente.idPaciente,
                                                  cedula = paciente.cedula,
                                                  nombres = paciente.nombres + " " + paciente.apellidos,
                                                  idHistoriaClinica = historiaClinica.idHistoriaClinica
                                              }).ToList();

                listaEvolucionesDesactivadas = (from evolucion in bd.tblEvolucion
                                                join historiaClinica in bd.tblHistoriaClinica
                                                on evolucion.idHistoriaClinica equals historiaClinica.idHistoriaClinica
                                                join paciente in bd.tblPaciente
                                                on historiaClinica.idPaciente equals paciente.idPaciente
                                                where evolucion.habilitado == 0
                                              select new EvolucionCLS
                                              {
                                                  idPaciente = paciente.idPaciente,
                                                  cedula = paciente.cedula,
                                                  nombresPaciente = paciente.nombres + " " + paciente.apellidos,
                                                  idHistoriaClinica = historiaClinica.idHistoriaClinica,
                                                  idEvolucion = evolucion.idEvolucion,
                                                  motivoConsulta = evolucion.motivoConsulta,
                                                  fechaVisita = (DateTime)evolucion.fechaVisita
                                              }).ToList();
            }

            ViewBag.listaPacientesDesactivados = listaPacientesDesactivados;
            ViewBag.listaEvolucionesDesactivadas = listaEvolucionesDesactivadas;

                return View();
        }
    }
}