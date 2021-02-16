using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
    public class PacienteController : Controller
    {
        // GET: Paciente
        public ActionResult Index()
        {

            List<PacienteCLS> listaPacientes = new List<PacienteCLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaPacientes = (from paciente in bd.tblPaciente
                                  where paciente.habilitado == 1
                                  select new PacienteCLS
                                  {
                                      idPaciente = paciente.idPaciente,
                                      cedula = paciente.cedula,
                                      nombres = paciente.nombres + " " + paciente.apellidos,                                      
                                  }
                                  ).ToList();
            }
                return View(listaPacientes);
        }

        public ActionResult Filtro(string busqueda)
        {
            List<PacienteCLS> listaPacientes = new List<PacienteCLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                
                if (busqueda == null)
                {

                    listaPacientes = (from paciente in bd.tblPaciente
                                      where paciente.habilitado == 1
                                      select new PacienteCLS
                                      {
                                          idPaciente = paciente.idPaciente,
                                          cedula = paciente.cedula,
                                          nombres = paciente.nombres + " " + paciente.apellidos,
                                      }
                                      ).ToList();
                }
                else
                {

                    listaPacientes = (from paciente in bd.tblPaciente
                                      where paciente.habilitado == 1
                                      && (paciente.nombres.Contains(busqueda)
                                      || paciente.apellidos.Contains(busqueda))
                                      || paciente.cedula.Contains(busqueda)
                                      select new PacienteCLS
                                      {
                                          idPaciente = paciente.idPaciente,
                                          cedula = paciente.cedula,
                                          nombres = paciente.nombres + " " + paciente.apellidos,
                                      }
                                      ).ToList();
                  
                }
                return PartialView("_TablaPacientes", listaPacientes);
            }
        }

    }
}