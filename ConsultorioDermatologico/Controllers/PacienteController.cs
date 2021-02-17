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

        //Genera la vista html
        public ActionResult Agregar()
        {
            llenarIdentidadGenero();
            llenarOrientacionSexual();


            return View();

        }

        //Hacer la insercion de datos por  medio del metodo post (Html.BEgin form) Agregar.csHtml
        [HttpPost]
        public ActionResult Agregar(RepresentanteCLS representanteCLS, ContactoEmergenciaCLS contactoEmergenciaCLS, PacienteCLS pacienteCLS)
        {

            var tuple = new Tuple<PacienteCLS, HistoriaClinicaCLS, RepresentanteCLS, ContactoEmergenciaCLS>(new PacienteCLS(), null, new RepresentanteCLS(), new ContactoEmergenciaCLS());


            if (!ModelState.IsValid)
            {
                return View(tuple);
            }
            else
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    int? idRepresentante = null;
                    int? idContactoEmergencia = null;

                    if (representanteCLS != null)
                    {
                        tblRepresentante tblRepresentante = new tblRepresentante();
                        tblRepresentante.nombreRepresentante = representanteCLS.nombreRepresentante;
                        tblRepresentante.apellidoRepresentante = representanteCLS.apellidoRepresentante;
                        tblRepresentante.telefonoRepresentante = representanteCLS.correoRepresentante;
                        tblRepresentante.correoRepresentante = representanteCLS.correoRepresentante;

                        bd.tblRepresentante.Add(tblRepresentante);

                        idRepresentante = tblRepresentante.idRepresentante;

                    }

                    if (contactoEmergenciaCLS != null)
                    {
                        tblContactoEmergencia tblContactoEmergencia = new tblContactoEmergencia();
                        tblContactoEmergencia.nombreContactoEmergencia = contactoEmergenciaCLS.nombreContactoEmergencia;
                        tblContactoEmergencia.apellidoContactoEmergencia = contactoEmergenciaCLS.apellidoContactoEmergencia;
                        tblContactoEmergencia.telefonoContactoEmergencia = contactoEmergenciaCLS.telefonoContactoEmergencia;
                        tblContactoEmergencia.correoContactoEmergencia = contactoEmergenciaCLS.correoContactoEmergencia;

                        bd.tblContactoEmergencia.Add(tblContactoEmergencia);

                        idContactoEmergencia = tblContactoEmergencia.idContactoEmergencia;
                    }

                    tblPaciente tblPaciente = new tblPaciente();
                    tblPaciente.nombres = pacienteCLS.nombres;
                    tblPaciente.apellidos = pacienteCLS.apellidos;
                    tblPaciente.cedula = pacienteCLS.cedula;
                    tblPaciente.fechaNacimiento = pacienteCLS.fechaNacimiento;
                    if (idRepresentante != null) { tblPaciente.idRepresentante = idRepresentante; }
                    tblPaciente.idOrientacionSexual = pacienteCLS.idOrientacionSexual;
                    tblPaciente.idIdentidadGenero = pacienteCLS.idIdentidadGenero;
                    tblPaciente.ciudadNacimiento = pacienteCLS.ciudadNacimiento;
                    tblPaciente.ciudadResidencia = pacienteCLS.ciudadResidencia;
                    tblPaciente.ocupacion = pacienteCLS.ocupacion;
                    tblPaciente.profesion = pacienteCLS.profesion;
                    tblPaciente.tipoDiscapacidad = pacienteCLS.tipoDiscapacidad;
                    tblPaciente.porcentajeDiscapacidad = pacienteCLS.porcentajeDiscapacidad;
                    tblPaciente.estadoCivil = pacienteCLS.estadoCivil;
                    tblPaciente.lateralidad = pacienteCLS.lateralidad;
                    tblPaciente.nivelEducacion = pacienteCLS.nivelEducacion;
                    tblPaciente.direccion = pacienteCLS.direccion;
                    tblPaciente.telefonoPersonal = pacienteCLS.telefonoPersonal;
                    tblPaciente.telefonoResidencial = pacienteCLS.telefonoResidencial;
                    tblPaciente.correoElectronico = pacienteCLS.correoElectronico;
                    tblPaciente.religion = pacienteCLS.religion;
                    if (idContactoEmergencia != null) { tblPaciente.idContactoEmergencia = idContactoEmergencia; }
                    tblPaciente.habilitado = 1;

                    bd.tblPaciente.Add(tblPaciente);

                    bd.SaveChanges();


                }
                return RedirectToAction("Index");
            }
        }



        private void llenarIdentidadGenero()
        {
            List<SelectListItem> listaIdentidadGenero;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaIdentidadGenero = (from identidadGenero in bd.tblIdentidadGenero
                                        select new SelectListItem
                                        {
                                            Text = identidadGenero.nombreIdentidadGenero,
                                            Value = identidadGenero.idIdentidadGenero.ToString()
                                        }).ToList();
                listaIdentidadGenero.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaIdentidadGenero = listaIdentidadGenero;
            }
        }

        private void llenarOrientacionSexual()
        {
            List<SelectListItem> listaOrientacionSexual;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaOrientacionSexual = (from orientacionSexual in bd.tblOrientacionSexual
                                          select new SelectListItem
                                          {
                                              Text = orientacionSexual.nombreOrientacionSexual,
                                              Value = orientacionSexual.idOrientacionSexual.ToString()
                                          }).ToList();
                listaOrientacionSexual.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaOrientacionSexual = listaOrientacionSexual;
            }




        }
    }
}