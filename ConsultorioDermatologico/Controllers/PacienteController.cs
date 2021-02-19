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
            llenarDropDown();

            return View();

        }

        //Hacer la insercion de datos por  medio del metodo post (Html.BEgin form) Agregar.csHtml
        [HttpPost]
        public ActionResult Agregar(RegistroPacienteCLS registroPacienteCLS)
        {
            if (!ModelState.IsValid)
            {
                llenarDropDown();

                return View(registroPacienteCLS);
            }
            else
            {
                //using (var bd = new BDD_ConsultorioDermatologicoEntities())
                //{
                //    
                //    int? idContactoEmergencia = null;

                //    if (contactoEmergenciaCLS != null)
                //    {
                //        tblContactoEmergencia tblContactoEmergencia = new tblContactoEmergencia();
                //        tblContactoEmergencia.nombreContactoEmergencia = contactoEmergenciaCLS.nombreContactoEmergencia;
                //        tblContactoEmergencia.apellidoContactoEmergencia = contactoEmergenciaCLS.apellidoContactoEmergencia;
                //        tblContactoEmergencia.telefonoContactoEmergencia = contactoEmergenciaCLS.telefonoContactoEmergencia;
                //        tblContactoEmergencia.correoContactoEmergencia = contactoEmergenciaCLS.correoContactoEmergencia;

                //        bd.tblContactoEmergencia.Add(tblContactoEmergencia);

                //        idContactoEmergencia = tblContactoEmergencia.idContactoEmergencia;
                //    }

                //    tblPaciente tblPaciente = new tblPaciente();
                //    tblPaciente.nombres = pacienteCLS.nombres;
                //    tblPaciente.apellidos = pacienteCLS.apellidos;
                //    tblPaciente.cedula = pacienteCLS.cedula;
                //    tblPaciente.fechaNacimiento = pacienteCLS.fechaNacimiento;
                //    tblPaciente.idOrientacionSexual = pacienteCLS.idOrientacionSexual;
                //    tblPaciente.idIdentidadGenero = pacienteCLS.idIdentidadGenero;
                //    tblPaciente.ciudadNacimiento = pacienteCLS.ciudadNacimiento;
                //    tblPaciente.ciudadResidencia = pacienteCLS.ciudadResidencia;
                //    tblPaciente.ocupacion = pacienteCLS.ocupacion;
                //    tblPaciente.profesion = pacienteCLS.profesion;
                //    tblPaciente.tipoDiscapacidad = pacienteCLS.tipoDiscapacidad;
                //    tblPaciente.porcentajeDiscapacidad = pacienteCLS.porcentajeDiscapacidad;
                //    tblPaciente.estadoCivil = pacienteCLS.estadoCivil;
                //    tblPaciente.lateralidad = pacienteCLS.lateralidad;
                //    tblPaciente.nivelEducacion = pacienteCLS.nivelEducacion;
                //    tblPaciente.direccion = pacienteCLS.direccion;
                //    tblPaciente.telefonoPersonal = pacienteCLS.telefonoPersonal;
                //    tblPaciente.telefonoResidencial = pacienteCLS.telefonoResidencial;
                //    tblPaciente.correoElectronico = pacienteCLS.correoElectronico;
                //    tblPaciente.religion = pacienteCLS.religion;
                //    if (idContactoEmergencia != null) { tblPaciente.idContactoEmergencia = idContactoEmergencia; }
                //    tblPaciente.habilitado = 1;

                //    bd.tblPaciente.Add(tblPaciente);

                //    bd.SaveChanges();


                //}
                return RedirectToAction("Index");
            }
        }


        #region Datos de los dropdown 
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

        private void llenarTipoDiscpacidad()
        {
            List<SelectListItem> listaTipoDiscapacidad;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaTipoDiscapacidad = (from tipoDiscapacidad in bd.tblTipoDiscapacidad
                                         select new SelectListItem
                                         {
                                             Text = tipoDiscapacidad.tipo,
                                             Value = tipoDiscapacidad.idTipoDiscapacidad.ToString()
                                         }).ToList();
                listaTipoDiscapacidad.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaTipoDiscapacidad = listaTipoDiscapacidad;
            }
        }

        private void llenarEstadoCivil()
        {
            List<SelectListItem> listaEstadoCivil;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaEstadoCivil = (from estadoCivil in bd.tblEstadoCivil
                                    select new SelectListItem
                                    {
                                        Text = estadoCivil.nombreEstadoCivil,
                                        Value = estadoCivil.idEstadoCivil.ToString()
                                    }).ToList();
                listaEstadoCivil.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaEstadoCivil = listaEstadoCivil;
            }
        }

        private void llenarLateralidad()
        {
            List<SelectListItem> listaLateralidad;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaLateralidad = (from lateralidad in bd.tblLateralidad
                                    select new SelectListItem
                                    {
                                        Text = lateralidad.nombreLateralidad,
                                        Value = lateralidad.idLateralidad.ToString()
                                    }).ToList();
                listaLateralidad.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaLateralidad = listaLateralidad;
            }
        }

        private void llenarNivelEducacion()
        {
            List<SelectListItem> listaNivelEducacion;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaNivelEducacion = (from nivelEducacion in bd.tblNivelEducacion
                                       select new SelectListItem
                                       {
                                           Text = nivelEducacion.nombreNivelEducacion,
                                           Value = nivelEducacion.idNivelEducacion.ToString()
                                       }).ToList();
                listaNivelEducacion.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaNivelEducacion = listaNivelEducacion;
            }
        }

        private void llenarReligion()
        {
            List<SelectListItem> listaReligion;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaReligion = (from religion in bd.tblReligion
                                 select new SelectListItem
                                 {
                                     Text = religion.nombreReligion,
                                     Value = religion.idReligion.ToString()
                                 }).ToList();
                listaReligion.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaReligion = listaReligion;
            }
        }

        public void llenarSeguros()
        {
            List<SelectListItem> listaSeguros;
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaSeguros = (from seguros in bd.tblSeguroMedico
                                select new SelectListItem
                                {
                                    Text = seguros.nombreSeguro,
                                    Value = seguros.idSeguroMedico.ToString()
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
                listaSangre = (from sangre in bd.tblTipoSangre
                               select new SelectListItem
                               {
                                   Text = sangre.sangre,
                                   Value = sangre.idTipoSangre.ToString()
                               }).ToList();
                listaSangre.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSangre = listaSangre;
            }
        }

        private void llenarDropDown()
        {
            llenarOrientacionSexual();
            llenarIdentidadGenero();            
            llenarTipoDiscpacidad();
            llenarNivelEducacion();
            llenarEstadoCivil();
            llenarLateralidad();            
            llenarReligion();
            llenarSeguros();
            llenarSangre();

        }

        #endregion
    }
}