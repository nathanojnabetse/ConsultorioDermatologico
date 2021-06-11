using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;
using ConsultorioDermatologico.Filters;

namespace ConsultorioDermatologico.Controllers
{
    [Acceder] //Tag para verificar que exista sesión iniciada la acción sea permitida
    public class HistoriaClinicaController : Controller
    {


        /// <summary>
        /// Accion para obtener y mostrar la información del paciente e historia clinica
        /// </summary>
        /// <param name="idPaciente">id único del paciente</param>
        /// <returns>Vista InformaciónPaciente con los datos personales y de historia clínica</returns>
        public ActionResult InformacionPaciente(int idPaciente)
        {
            llenarDropDown();
            //Creción de objetos con los datos para visualizacion
            ContactoEmergenciaCLS contactoEmergenciaCLS = new ContactoEmergenciaCLS();
            AntecedenteGinecoObstetricoCLS antecedenteGinecoObstetricoCLS = new AntecedenteGinecoObstetricoCLS();
            AntecedenteReprodMasculinoCLS antecedenteReprodMasculinoCLS = new AntecedenteReprodMasculinoCLS();
            HistoriaClinicaCLS historiaClinicaCLS = new HistoriaClinicaCLS();
            PacienteCLS pacienteCLS = new PacienteCLS();
            RegistroPacienteCLS registroPacienteCLS = new RegistroPacienteCLS();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                //Busqueda de coincidencias con el id de entrada.
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();

                pacienteCLS.idPaciente = tblPaciente.idPaciente;
                pacienteCLS.nombres = tblPaciente.nombres;
                pacienteCLS.apellidos = tblPaciente.apellidos;
                pacienteCLS.cedula = tblPaciente.cedula;
                pacienteCLS.fechaNacimiento = (DateTime)tblPaciente.fechaNacimiento;
                pacienteCLS.idOrientacionSexual = tblPaciente.idOrientacionSexual;
                pacienteCLS.idIdentidadGenero = (int)tblPaciente.idIdentidadGenero;
                pacienteCLS.ciudadNacimiento = tblPaciente.ciudadNacimiento;
                pacienteCLS.ciudadResidencia = tblPaciente.ciudadResidencia;
                pacienteCLS.ocupacion = tblPaciente.ocupacion;
                pacienteCLS.profesion = tblPaciente.profesion;
                pacienteCLS.idTipoDiscapacidad = tblPaciente.idTipoDiscapacidad;
                pacienteCLS.porcentajeDiscapacidad = (int)tblPaciente.porcentajeDiscapacidad;
                pacienteCLS.idEstadoCivil = tblPaciente.idEstadoCivil;
                pacienteCLS.idLateralidad = tblPaciente.idLateralidad;
                pacienteCLS.idNivelEducacion = tblPaciente.idNivelEducacion;
                pacienteCLS.direccion = tblPaciente.direccion;
                pacienteCLS.telefonoPersonal = tblPaciente.telefonoPersonal;
                pacienteCLS.telefonoResidencial = tblPaciente.telefonoResidencial;
                pacienteCLS.correoElectronico = tblPaciente.correoElectronico;
                pacienteCLS.idReligion = (int)tblPaciente.idReligion;
                pacienteCLS.idContactoEmergencia = (int)tblPaciente.idContactoEmergencia;

                tblContactoEmergencia tblContactoEmergencia = bd.tblContactoEmergencia.Where(p => p.idContactoEmergencia.Equals(pacienteCLS.idContactoEmergencia)).First();

                contactoEmergenciaCLS.nombreContactoEmergencia = tblContactoEmergencia.nombreContactoEmergencia;
                contactoEmergenciaCLS.telefonoContactoEmergencia = tblContactoEmergencia.telefonoContactoEmergencia;

                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente==tblPaciente.idPaciente).First();

                historiaClinicaCLS.idSeguroMedico = (int)tblHistoriaClinica.idSeguroMedico;
                historiaClinicaCLS.idTipoSangre = (int)tblHistoriaClinica.idTipoSangre;
                historiaClinicaCLS.antecedenteFamiliarClinico = tblHistoriaClinica.antecedenteFamiliarClinico;
                historiaClinicaCLS.antecedenteFamiliarQuirurgico = tblHistoriaClinica.antecedenteFamiliarQuirurgico;
                historiaClinicaCLS.antecedentePersonalClinico = tblHistoriaClinica.antecedentePersonalClinico;
                historiaClinicaCLS.antecedentePersonalQuirurgico = tblHistoriaClinica.antecedentePersonalQuirurgico;
                historiaClinicaCLS.antecedentePersonalAlergico = tblHistoriaClinica.antecedentePersonalAlergico;
                historiaClinicaCLS.antecedentePersonalVacunas = tblHistoriaClinica.antecedentePersonalVacunas;
                historiaClinicaCLS.idAntecedenteGinecoObstetrico = tblHistoriaClinica.idAntecedenteGinecoObstetrico;
                historiaClinicaCLS.idAntecedenteReprodMasculino = tblHistoriaClinica.idAntecedenteReprodMasculino;
                historiaClinicaCLS.tabaco = tblHistoriaClinica.tabaco;
                historiaClinicaCLS.alcohol = tblHistoriaClinica.alcohol;
                historiaClinicaCLS.otrasDrogas = tblHistoriaClinica.otrasDrogas;
                historiaClinicaCLS.actividadFisica = tblHistoriaClinica.actividadFisica;
                historiaClinicaCLS.medicacionHabitual = tblHistoriaClinica.medicacionHabitual;

                if (historiaClinicaCLS.idAntecedenteGinecoObstetrico != null)
                {
                    tblAntecedenteGinecoObstetrico tblAntecedenteGinecoObstetrico = bd.tblAntecedenteGinecoObstetrico.Where(p => p.idAntecedenteGinecoObstetrico.Equals((int)historiaClinicaCLS.idAntecedenteGinecoObstetrico)).First();
                    //Existencia de antecente gineco obsterico
                    antecedenteGinecoObstetricoCLS.menarquia = tblAntecedenteGinecoObstetrico.menarquia;                   
                    antecedenteGinecoObstetricoCLS.gestas = tblAntecedenteGinecoObstetrico.gestas;
                    antecedenteGinecoObstetricoCLS.partos = tblAntecedenteGinecoObstetrico.partos;
                    antecedenteGinecoObstetricoCLS.cesarea = tblAntecedenteGinecoObstetrico.cesarea;
                    antecedenteGinecoObstetricoCLS.abortos = tblAntecedenteGinecoObstetrico.abortos;
                    antecedenteGinecoObstetricoCLS.hijosMuertos = tblAntecedenteGinecoObstetrico.hijosMuertos;
                    antecedenteGinecoObstetricoCLS.hijosVivos = tblAntecedenteGinecoObstetrico.hijosVivos;                   
                }
                if (historiaClinicaCLS.idAntecedenteReprodMasculino != null)
                {
                    tblAntecedenteReprodMasculino tblAntecedenteReprodMasculino = bd.tblAntecedenteReprodMasculino.Where(p => p.idAntecedenteReprodMasculino.Equals((int)historiaClinicaCLS.idAntecedenteReprodMasculino)).First();
                    //Existencia de antecente reprod masculino
                    antecedenteReprodMasculinoCLS.ets = tblAntecedenteReprodMasculino.ets;
                    antecedenteReprodMasculinoCLS.parejaSexual = tblAntecedenteReprodMasculino.parejaSexual;

                }

                registroPacienteCLS.paciente = pacienteCLS;
                registroPacienteCLS.contactoEmergencia = contactoEmergenciaCLS;
                registroPacienteCLS.historiaClinica = historiaClinicaCLS;
                registroPacienteCLS.antecedenteReprodMasculino = antecedenteReprodMasculinoCLS;
                registroPacienteCLS.antecedenteGinecoObstetrico = antecedenteGinecoObstetricoCLS;
            }
            return View(registroPacienteCLS);
        }

        /// <summary>
        /// Accion para obtener y mostrar la información de la visita del paciente
        /// </summary>
        /// <param name="idEvolucion">id de la evolución del paciente</param>
        /// <returns>Vista con la iformación de la hoja de envolución</returns>
        public ActionResult EvolucionPaciente(int idEvolucion)
        {
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                EvolucionCLS evolucionCLS = new EvolucionCLS();

                tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion).First();
                ViewBag.idEvolucion = tblEvolucion.idEvolucion;
                //viewbags
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idHistoriaClinica == tblEvolucion.idHistoriaClinica).First();
                ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;
                ViewBag.idAntecedenteGinecoObstetrico = tblHistoriaClinica.idAntecedenteGinecoObstetrico;
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == tblHistoriaClinica.idPaciente).First();
                ViewBag.nombrePaciente = tblPaciente.nombres + " " + tblPaciente.apellidos;
                ViewBag.idPaciente = tblPaciente.idPaciente;

                evolucionCLS.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;
                evolucionCLS.nombreMapa = tblEvolucion.nombreMapa;
                evolucionCLS.extension = Path.GetExtension(tblEvolucion.nombreMapa);
                evolucionCLS.mapaCorporal = tblEvolucion.mapaCorporal;

                evolucionCLS.diagnostico = tblEvolucion.diagnostico;
                evolucionCLS.motivoConsulta = tblEvolucion.motivoConsulta;
                evolucionCLS.examenFisico = tblEvolucion.examenFisico;
                evolucionCLS.prescripcion = tblEvolucion.prescripcion;
                evolucionCLS.recomendaciones = tblEvolucion.recomendaciones;
                evolucionCLS.fechaVisita = (DateTime)tblEvolucion.fechaVisita;
                if(tblHistoriaClinica.idAntecedenteGinecoObstetrico!= null)
                {
                    evolucionCLS.ciclo = tblEvolucion.ciclo;
                    evolucionCLS.fechaUltimaMenstruacion = (DateTime)tblEvolucion.fechaUltimaMenstruacion;
                    evolucionCLS.metodoPlanificacionFamiliar = tblEvolucion.metodoPlanificacionFamiliar;
                    evolucionCLS.vidaSexualActiva = tblEvolucion.vidaSexualActiva;
                }
                

                List<tblFotos> listaFotosBD = bd.tblFotos.Where(p => p.idEvolucion == idEvolucion).ToList();
                List<FotoCLS> listaFotos = new List<FotoCLS>();

                foreach (var item in listaFotosBD)
                {
                    FotoCLS fotoCLS = new FotoCLS();
                    fotoCLS.idFoto = item.idFoto;
                    fotoCLS.nombreFoto = item.nombreFoto;
                    fotoCLS.extension = Path.GetExtension(item.nombreFoto);
                    fotoCLS.foto = Convert.ToBase64String(item.foto);
                    listaFotos.Add(fotoCLS);
                }

                listaFotos.Add(null);
                listaFotos.Add(null);
                listaFotos.Add(null);
                listaFotos.Add(null);
                listaFotos.Add(null);
                listaFotos.Add(null);

                RegistroEvolucionCLS registroEvolucionCLS = new RegistroEvolucionCLS();
                registroEvolucionCLS.evolucion = evolucionCLS;
                registroEvolucionCLS.foto1 = listaFotos[0];
                registroEvolucionCLS.foto2 = listaFotos[1];
                registroEvolucionCLS.foto3 = listaFotos[2];
                registroEvolucionCLS.foto4 = listaFotos[3];
                registroEvolucionCLS.foto5 = listaFotos[4];
                registroEvolucionCLS.foto6 = listaFotos[5];

                return View(registroEvolucionCLS);
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
        
        //Método para llenar todos los dropdown con la info de la bdd
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




        //////////////////////////
        public ActionResult TEST(int idPaciente)
        {
            llenarDropDown();
            //Creción de objetos con los datos para visualizacion
            ContactoEmergenciaCLS contactoEmergenciaCLS = new ContactoEmergenciaCLS();
            AntecedenteGinecoObstetricoCLS antecedenteGinecoObstetricoCLS = new AntecedenteGinecoObstetricoCLS();
            AntecedenteReprodMasculinoCLS antecedenteReprodMasculinoCLS = new AntecedenteReprodMasculinoCLS();
            HistoriaClinicaCLS historiaClinicaCLS = new HistoriaClinicaCLS();
            PacienteCLS pacienteCLS = new PacienteCLS();
            RegistroPacienteCLS registroPacienteCLS = new RegistroPacienteCLS();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                //Busqueda de coincidencias con el id de entrada.
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();

                pacienteCLS.idPaciente = tblPaciente.idPaciente;
                pacienteCLS.nombres = tblPaciente.nombres;
                pacienteCLS.apellidos = tblPaciente.apellidos;
                pacienteCLS.cedula = tblPaciente.cedula;
                pacienteCLS.fechaNacimiento = (DateTime)tblPaciente.fechaNacimiento;
                pacienteCLS.idOrientacionSexual = tblPaciente.idOrientacionSexual;
                pacienteCLS.idIdentidadGenero = (int)tblPaciente.idIdentidadGenero;
                pacienteCLS.ciudadNacimiento = tblPaciente.ciudadNacimiento;
                pacienteCLS.ciudadResidencia = tblPaciente.ciudadResidencia;
                pacienteCLS.ocupacion = tblPaciente.ocupacion;
                pacienteCLS.profesion = tblPaciente.profesion;
                pacienteCLS.idTipoDiscapacidad = tblPaciente.idTipoDiscapacidad;
                pacienteCLS.porcentajeDiscapacidad = (int)tblPaciente.porcentajeDiscapacidad;
                pacienteCLS.idEstadoCivil = tblPaciente.idEstadoCivil;
                pacienteCLS.idLateralidad = tblPaciente.idLateralidad;
                pacienteCLS.idNivelEducacion = tblPaciente.idNivelEducacion;
                pacienteCLS.direccion = tblPaciente.direccion;
                pacienteCLS.telefonoPersonal = tblPaciente.telefonoPersonal;
                pacienteCLS.telefonoResidencial = tblPaciente.telefonoResidencial;
                pacienteCLS.correoElectronico = tblPaciente.correoElectronico;
                pacienteCLS.idReligion = (int)tblPaciente.idReligion;
                pacienteCLS.idContactoEmergencia = (int)tblPaciente.idContactoEmergencia;

                tblContactoEmergencia tblContactoEmergencia = bd.tblContactoEmergencia.Where(p => p.idContactoEmergencia.Equals(pacienteCLS.idContactoEmergencia)).First();

                contactoEmergenciaCLS.nombreContactoEmergencia = tblContactoEmergencia.nombreContactoEmergencia;
                contactoEmergenciaCLS.telefonoContactoEmergencia = tblContactoEmergencia.telefonoContactoEmergencia;

                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == tblPaciente.idPaciente).First();

                historiaClinicaCLS.idSeguroMedico = (int)tblHistoriaClinica.idSeguroMedico;
                historiaClinicaCLS.idTipoSangre = (int)tblHistoriaClinica.idTipoSangre;
                historiaClinicaCLS.antecedenteFamiliarClinico = tblHistoriaClinica.antecedenteFamiliarClinico;
                historiaClinicaCLS.antecedenteFamiliarQuirurgico = tblHistoriaClinica.antecedenteFamiliarQuirurgico;
                historiaClinicaCLS.antecedentePersonalClinico = tblHistoriaClinica.antecedentePersonalClinico;
                historiaClinicaCLS.antecedentePersonalQuirurgico = tblHistoriaClinica.antecedentePersonalQuirurgico;
                historiaClinicaCLS.antecedentePersonalAlergico = tblHistoriaClinica.antecedentePersonalAlergico;
                historiaClinicaCLS.antecedentePersonalVacunas = tblHistoriaClinica.antecedentePersonalVacunas;
                historiaClinicaCLS.idAntecedenteGinecoObstetrico = tblHistoriaClinica.idAntecedenteGinecoObstetrico;
                historiaClinicaCLS.idAntecedenteReprodMasculino = tblHistoriaClinica.idAntecedenteReprodMasculino;
                historiaClinicaCLS.tabaco = tblHistoriaClinica.tabaco;
                historiaClinicaCLS.alcohol = tblHistoriaClinica.alcohol;
                historiaClinicaCLS.otrasDrogas = tblHistoriaClinica.otrasDrogas;
                historiaClinicaCLS.actividadFisica = tblHistoriaClinica.actividadFisica;
                historiaClinicaCLS.medicacionHabitual = tblHistoriaClinica.medicacionHabitual;

                if (historiaClinicaCLS.idAntecedenteGinecoObstetrico != null)
                {
                    tblAntecedenteGinecoObstetrico tblAntecedenteGinecoObstetrico = bd.tblAntecedenteGinecoObstetrico.Where(p => p.idAntecedenteGinecoObstetrico.Equals((int)historiaClinicaCLS.idAntecedenteGinecoObstetrico)).First();
                    //Existencia de antecente gineco obsterico
                    antecedenteGinecoObstetricoCLS.menarquia = tblAntecedenteGinecoObstetrico.menarquia;
                    antecedenteGinecoObstetricoCLS.gestas = tblAntecedenteGinecoObstetrico.gestas;
                    antecedenteGinecoObstetricoCLS.partos = tblAntecedenteGinecoObstetrico.partos;
                    antecedenteGinecoObstetricoCLS.cesarea = tblAntecedenteGinecoObstetrico.cesarea;
                    antecedenteGinecoObstetricoCLS.abortos = tblAntecedenteGinecoObstetrico.abortos;
                    antecedenteGinecoObstetricoCLS.hijosMuertos = tblAntecedenteGinecoObstetrico.hijosMuertos;
                    antecedenteGinecoObstetricoCLS.hijosVivos = tblAntecedenteGinecoObstetrico.hijosVivos;
                }
                if (historiaClinicaCLS.idAntecedenteReprodMasculino != null)
                {
                    tblAntecedenteReprodMasculino tblAntecedenteReprodMasculino = bd.tblAntecedenteReprodMasculino.Where(p => p.idAntecedenteReprodMasculino.Equals((int)historiaClinicaCLS.idAntecedenteReprodMasculino)).First();
                    //Existencia de antecente reprod masculino
                    antecedenteReprodMasculinoCLS.ets = tblAntecedenteReprodMasculino.ets;
                    antecedenteReprodMasculinoCLS.parejaSexual = tblAntecedenteReprodMasculino.parejaSexual;

                }

                registroPacienteCLS.paciente = pacienteCLS;
                registroPacienteCLS.contactoEmergencia = contactoEmergenciaCLS;
                registroPacienteCLS.historiaClinica = historiaClinicaCLS;
                registroPacienteCLS.antecedenteReprodMasculino = antecedenteReprodMasculinoCLS;
                registroPacienteCLS.antecedenteGinecoObstetrico = antecedenteGinecoObstetricoCLS;
            }
            return View(registroPacienteCLS);
        }
        //////////////////////////
    }
}