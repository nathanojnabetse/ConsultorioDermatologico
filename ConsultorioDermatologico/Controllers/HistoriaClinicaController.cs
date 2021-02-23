﻿using System;
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

        //retorna la info del paciente
        public ActionResult InformacionPaciente(int idPaciente)
        {
            llenarDropDown();
            ContactoEmergenciaCLS contactoEmergenciaCLS = new ContactoEmergenciaCLS();
            AntecedenteGinecoObstetricoCLS antecedenteGinecoObstetricoCLS = new AntecedenteGinecoObstetricoCLS();
            AntecedenteReprodMasculinoCLS antecedenteReprodMasculinoCLS = new AntecedenteReprodMasculinoCLS();
            HistoriaClinicaCLS historiaClinicaCLS = new HistoriaClinicaCLS();
            PacienteCLS pacienteCLS = new PacienteCLS();
            RegistroPacienteCLS registroPacienteCLS = new RegistroPacienteCLS();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {

                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();

                pacienteCLS.idPaciente = tblPaciente.idPaciente;
                pacienteCLS.nombres = tblPaciente.nombres;
                pacienteCLS.apellidos = tblPaciente.apellidos;
                pacienteCLS.cedula = tblPaciente.cedula;
                pacienteCLS.fechaNacimiento = (DateTime)tblPaciente.fechaNacimiento;
                pacienteCLS.idOrientacionSexual = (int)tblPaciente.idOrientacionSexual;
                pacienteCLS.idIdentidadGenero = (int)tblPaciente.idIdentidadGenero;
                pacienteCLS.ciudadNacimiento = tblPaciente.ciudadNacimiento;
                pacienteCLS.ciudadResidencia = tblPaciente.ciudadResidencia;
                pacienteCLS.ocupacion = tblPaciente.ocupacion;
                pacienteCLS.profesion = tblPaciente.profesion;
                pacienteCLS.idTipoDiscapacidad = (int)tblPaciente.idTipoDiscapacidad;
                pacienteCLS.porcentajeDiscapacidad = (int)tblPaciente.porcentajeDiscapacidad;
                pacienteCLS.idEstadoCivil = (int)tblPaciente.idEstadoCivil;
                pacienteCLS.idLateralidad = (int)tblPaciente.idLateralidad;
                pacienteCLS.idNivelEducacion = (int)tblPaciente.idNivelEducacion;
                pacienteCLS.direccion = tblPaciente.direccion;
                pacienteCLS.telefonoPersonal = tblPaciente.telefonoPersonal;
                pacienteCLS.telefonoResidencial = tblPaciente.telefonoResidencial;
                pacienteCLS.correoElectronico = tblPaciente.correoElectronico;
                pacienteCLS.idReligion = (int)tblPaciente.idReligion;
                pacienteCLS.idContactoEmergencia = (int)tblPaciente.idContactoEmergencia;

                tblContactoEmergencia tblContactoEmergencia = bd.tblContactoEmergencia.Where(p => p.idContactoEmergencia.Equals(pacienteCLS.idContactoEmergencia)).First();

                contactoEmergenciaCLS.nombreContactoEmergencia = tblContactoEmergencia.nombreContactoEmergencia;
                contactoEmergenciaCLS.apellidoContactoEmergencia = tblContactoEmergencia.apellidoContactoEmergencia;
                contactoEmergenciaCLS.telefonoContactoEmergencia = tblContactoEmergencia.telefonoContactoEmergencia;
                contactoEmergenciaCLS.correoContactoEmergencia = tblContactoEmergencia.correoContactoEmergencia;

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
                    antecedenteGinecoObstetricoCLS.ciclo = tblAntecedenteGinecoObstetrico.ciclo;
                    antecedenteGinecoObstetricoCLS.fechaUltimaMenstruacion = tblAntecedenteGinecoObstetrico.fechaUltimaMenstruacion;
                    antecedenteGinecoObstetricoCLS.gestas = tblAntecedenteGinecoObstetrico.gestas;
                    antecedenteGinecoObstetricoCLS.partos = tblAntecedenteGinecoObstetrico.partos;
                    antecedenteGinecoObstetricoCLS.cesarea = tblAntecedenteGinecoObstetrico.cesarea;
                    antecedenteGinecoObstetricoCLS.abortos = tblAntecedenteGinecoObstetrico.abortos;
                    antecedenteGinecoObstetricoCLS.hijosMuertos = tblAntecedenteGinecoObstetrico.hijosMuertos;
                    antecedenteGinecoObstetricoCLS.hijosVivos = tblAntecedenteGinecoObstetrico.hijosVivos;
                    antecedenteGinecoObstetricoCLS.vidaSexualActiva = tblAntecedenteGinecoObstetrico.vidaSexualActiva;
                    antecedenteGinecoObstetricoCLS.metodoPlanificacionFamiliar = tblAntecedenteGinecoObstetrico.metodoPlanificacionFamiliar;
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