using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;
using ConsultorioDermatologico.Filters;

namespace ConsultorioDermatologico.Controllers
{
    [Acceder] //Tag para verificar que exista sesión iniciada la acción sea permitida
    public class PacienteController : Controller
    {
        /// <summary>
        /// GET: Paciente
        /// </summary>
        /// <returns>Vista Index con la lista de pacientes registrados</returns>
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

        /// <summary>
        /// Filtro en base a los criterios de búsqueda
        /// </summary>
        /// <param name="busqueda">Criterio de bpusqueda escrito, cédula o nombre</param>
        /// <returns>Lista con coincidencias de busqueda en la vista parcial</returns>
        public ActionResult Filtro(string busqueda)
        {
            List<PacienteCLS> listaPacientes = new List<PacienteCLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                if (busqueda == null) //Si la busqueda es nula devuelve todos los registros
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
                else //coincidencias de búsqueda
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

        /// <summary>
        /// Generación de la vista con datos en los dropdown
        /// </summary>
        /// <returns>Vista Agregar</returns>
        public ActionResult Agregar()
        {
            llenarDropDown();

            return View();
        }

        /// <summary>
        ///  Inserción de datos por medio de POST
        /// </summary>
        /// <param name="registroPacienteCLS">Objeto del tipo pacienteCLS</param>
        /// <returns>Si el modelo es invalido, los datos ingresados; si se ha realizado una correcta inserción, el id del usuario creado a la vista HistoriaClinica/InformacionPaciente</returns>        
        [HttpPost]
        public ActionResult Agregar(RegistroPacienteCLS registroPacienteCLS)
        {
            int nRegistrosEncontrados = 0;
            string cedula = registroPacienteCLS.paciente.cedula;

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                nRegistrosEncontrados = bd.tblPaciente.Where(p => p.cedula.Equals(cedula)).Count();//Verificación de que no exista cédula repetida
            }

            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
                if (nRegistrosEncontrados >= 1)
                {
                    registroPacienteCLS.mensajeError = "ESTE USUARIO YA SE ENCUENTRA REGISTRADO";
                }
                    llenarDropDown();
                    //retorno de la vista en caso de errores al llenar un campo
                    return View(registroPacienteCLS);
            }
            else
            {                   
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {                       
                    int? idContactoEmergencia = null;
                    //Creación de un contacto de emergencia para añadirse al paciente
                    tblContactoEmergencia tblContactoEmergencia = new tblContactoEmergencia();
                    tblContactoEmergencia.nombreContactoEmergencia = registroPacienteCLS.contactoEmergencia.nombreContactoEmergencia;
                    tblContactoEmergencia.apellidoContactoEmergencia = registroPacienteCLS.contactoEmergencia.apellidoContactoEmergencia;
                    tblContactoEmergencia.telefonoContactoEmergencia = registroPacienteCLS.contactoEmergencia.telefonoContactoEmergencia;
                    tblContactoEmergencia.correoContactoEmergencia = registroPacienteCLS.contactoEmergencia.correoContactoEmergencia;

                    bd.tblContactoEmergencia.Add(tblContactoEmergencia);//Guardado del Contacto de emergencia
                    int? idPaciente = null;//Obtencion del id del Contacto de Emergencia para paciente
                    idContactoEmergencia = tblContactoEmergencia.idContactoEmergencia;
                        
                    //Creación de un paciente
                    tblPaciente tblPaciente = new tblPaciente();
                    tblPaciente.nombres = registroPacienteCLS.paciente.nombres;
                    tblPaciente.apellidos = registroPacienteCLS.paciente.apellidos;
                    tblPaciente.cedula = registroPacienteCLS.paciente.cedula;
                    tblPaciente.fechaNacimiento = registroPacienteCLS.paciente.fechaNacimiento;
                    tblPaciente.idOrientacionSexual = registroPacienteCLS.paciente.idOrientacionSexual;
                    tblPaciente.idIdentidadGenero = registroPacienteCLS.paciente.idIdentidadGenero;
                    tblPaciente.ciudadNacimiento = registroPacienteCLS.paciente.ciudadNacimiento;
                    tblPaciente.ciudadResidencia = registroPacienteCLS.paciente.ciudadResidencia;
                    tblPaciente.ocupacion = registroPacienteCLS.paciente.ocupacion;
                    tblPaciente.profesion = registroPacienteCLS.paciente.profesion;
                    tblPaciente.idTipoDiscapacidad = registroPacienteCLS.paciente.idTipoDiscapacidad;
                    tblPaciente.porcentajeDiscapacidad = registroPacienteCLS.paciente.porcentajeDiscapacidad;
                    tblPaciente.idEstadoCivil = registroPacienteCLS.paciente.idEstadoCivil;
                    tblPaciente.idLateralidad = registroPacienteCLS.paciente.idLateralidad;
                    tblPaciente.idNivelEducacion = registroPacienteCLS.paciente.idNivelEducacion;
                    tblPaciente.direccion = registroPacienteCLS.paciente.direccion;
                    tblPaciente.telefonoPersonal = registroPacienteCLS.paciente.telefonoPersonal;
                    tblPaciente.telefonoResidencial = registroPacienteCLS.paciente.telefonoResidencial;
                    tblPaciente.correoElectronico = registroPacienteCLS.paciente.correoElectronico;
                    tblPaciente.idReligion = registroPacienteCLS.paciente.idReligion;
                    if (idContactoEmergencia != null) { tblPaciente.idContactoEmergencia = idContactoEmergencia; }
                    tblPaciente.habilitado = 1;

                    bd.tblPaciente.Add(tblPaciente); //Guardado del paciente en la bddd
                    idPaciente = tblPaciente.idPaciente;//id para la historia clinica

                    int? idAntecedenteReprodMasculino = null;
                    int? idAntecedenteGinecoObstetrico = null;
                    if (!(registroPacienteCLS.antecedenteReprodMasculino.parejaSexual == null
                        && registroPacienteCLS.antecedenteReprodMasculino.ets == null))
                    {
                        //Creación de antecedentes reproductivo masculino de ser necesario
                        tblAntecedenteReprodMasculino tblAntecedenteReprodMasculino = new tblAntecedenteReprodMasculino();
                        tblAntecedenteReprodMasculino.ets = registroPacienteCLS.antecedenteReprodMasculino.ets;
                        tblAntecedenteReprodMasculino.parejaSexual = registroPacienteCLS.antecedenteReprodMasculino.parejaSexual;

                        bd.tblAntecedenteReprodMasculino.Add(tblAntecedenteReprodMasculino); //Guardado del antecedente en la bdd
                        idAntecedenteReprodMasculino = tblAntecedenteReprodMasculino.idAntecedenteReprodMasculino; //id para la historia clinica
                    }

                    if (!(registroPacienteCLS.antecedenteGinecoObstetrico.menarquia == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.ciclo == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.fechaUltimaMenstruacion == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.gestas == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.partos == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.cesarea == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.abortos == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.hijosVivos == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.hijosMuertos == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.vidaSexualActiva == null
                        && registroPacienteCLS.antecedenteGinecoObstetrico.metodoPlanificacionFamiliar == null))
                    {
                        //Creación de antecedentes gineco obstetricos de ser necesario
                        tblAntecedenteGinecoObstetrico tblAntecedenteGinecoObstetrico = new tblAntecedenteGinecoObstetrico();
                        tblAntecedenteGinecoObstetrico.menarquia = registroPacienteCLS.antecedenteGinecoObstetrico.menarquia;
                        tblAntecedenteGinecoObstetrico.ciclo = registroPacienteCLS.antecedenteGinecoObstetrico.ciclo;
                        tblAntecedenteGinecoObstetrico.fechaUltimaMenstruacion = registroPacienteCLS.antecedenteGinecoObstetrico.fechaUltimaMenstruacion;
                        tblAntecedenteGinecoObstetrico.gestas = registroPacienteCLS.antecedenteGinecoObstetrico.gestas;
                        tblAntecedenteGinecoObstetrico.partos = registroPacienteCLS.antecedenteGinecoObstetrico.partos;
                        tblAntecedenteGinecoObstetrico.cesarea = registroPacienteCLS.antecedenteGinecoObstetrico.cesarea;
                        tblAntecedenteGinecoObstetrico.abortos = registroPacienteCLS.antecedenteGinecoObstetrico.abortos;
                        tblAntecedenteGinecoObstetrico.hijosMuertos = registroPacienteCLS.antecedenteGinecoObstetrico.hijosMuertos;
                        tblAntecedenteGinecoObstetrico.hijosVivos = registroPacienteCLS.antecedenteGinecoObstetrico.hijosVivos;
                        tblAntecedenteGinecoObstetrico.vidaSexualActiva = registroPacienteCLS.antecedenteGinecoObstetrico.vidaSexualActiva;
                        tblAntecedenteGinecoObstetrico.metodoPlanificacionFamiliar = registroPacienteCLS.antecedenteGinecoObstetrico.metodoPlanificacionFamiliar;

                        bd.tblAntecedenteGinecoObstetrico.Add(tblAntecedenteGinecoObstetrico);//Guardado del antecedente en la bdd
                        idAntecedenteGinecoObstetrico = tblAntecedenteGinecoObstetrico.idAntecedenteGinecoObstetrico; //id para la historia clinica
                    }

                    //Creación y guardado de la historia clinica
                    tblHistoriaClinica tblHistoriaClinica = new tblHistoriaClinica();
                    tblHistoriaClinica.idPaciente = idPaciente;
                    tblHistoriaClinica.idSeguroMedico = registroPacienteCLS.historiaClinica.idSeguroMedico;
                    tblHistoriaClinica.idTipoSangre = registroPacienteCLS.historiaClinica.idTipoSangre;
                    tblHistoriaClinica.antecedenteFamiliarClinico = registroPacienteCLS.historiaClinica.antecedenteFamiliarClinico;
                    tblHistoriaClinica.antecedenteFamiliarQuirurgico = registroPacienteCLS.historiaClinica.antecedenteFamiliarQuirurgico;
                    tblHistoriaClinica.antecedentePersonalClinico = registroPacienteCLS.historiaClinica.antecedentePersonalClinico;
                    tblHistoriaClinica.antecedentePersonalQuirurgico = registroPacienteCLS.historiaClinica.antecedentePersonalQuirurgico;
                    tblHistoriaClinica.antecedentePersonalAlergico = registroPacienteCLS.historiaClinica.antecedentePersonalAlergico;
                    tblHistoriaClinica.antecedentePersonalVacunas = registroPacienteCLS.historiaClinica.antecedentePersonalVacunas;
                    tblHistoriaClinica.idAntecedenteGinecoObstetrico = idAntecedenteGinecoObstetrico;
                    tblHistoriaClinica.idAntecedenteReprodMasculino = idAntecedenteReprodMasculino;
                    tblHistoriaClinica.tabaco = registroPacienteCLS.historiaClinica.tabaco;
                    tblHistoriaClinica.alcohol = registroPacienteCLS.historiaClinica.alcohol;
                    tblHistoriaClinica.otrasDrogas = registroPacienteCLS.historiaClinica.otrasDrogas;
                    tblHistoriaClinica.actividadFisica = registroPacienteCLS.historiaClinica.actividadFisica;
                    tblHistoriaClinica.medicacionHabitual = registroPacienteCLS.historiaClinica.medicacionHabitual;
                    tblHistoriaClinica.habilitado = 1;

                    bd.tblHistoriaClinica.Add(tblHistoriaClinica);

                    bd.SaveChanges();//Guardar los cambios realizados en la bdd 

                    return RedirectToAction("InformacionPaciente", "HistoriaClinica", new { idPaciente = tblHistoriaClinica.idPaciente });
                }                
            }
        }

        /// <summary>
        /// Acción para obtener los datos de la bbd y ser mostrados en la vista para ser editados
        /// </summary>
        /// <param name="idPaciente">id del paciente a editarse</param>
        /// <returns></returns>      
        public ActionResult Editar(int idPaciente)
        {
            llenarDropDown();
            //Creación de objetos con información a editarse
            ContactoEmergenciaCLS contactoEmergenciaCLS = new ContactoEmergenciaCLS();
            AntecedenteGinecoObstetricoCLS antecedenteGinecoObstetricoCLS = new AntecedenteGinecoObstetricoCLS();
            AntecedenteReprodMasculinoCLS antecedenteReprodMasculinoCLS = new AntecedenteReprodMasculinoCLS();
            HistoriaClinicaCLS historiaClinicaCLS = new HistoriaClinicaCLS();
            PacienteCLS pacienteCLS = new PacienteCLS();
            RegistroPacienteCLS registroPacienteCLS = new RegistroPacienteCLS();

            using (var bd = new BDD_ConsultorioDermatologicoEntities()) 
            {
                //Búsqueda del paciente en base al id
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();
                //Obtención de los datos desde la bdd
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
                
                if(historiaClinicaCLS.idAntecedenteGinecoObstetrico != null)
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
            return View(registroPacienteCLS);//Objeto con la info del paciente
        }

        /// <summary>
        /// Acción para guardar los cambios realizados en la pantalla de edición
        /// </summary>
        /// <param name="registroPacienteCLS">Objeto del tipo RegistroPacienteCLS con datos modificados</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editar(RegistroPacienteCLS registroPacienteCLS)
        {

            if (!ModelState.IsValid)
            {
                llenarDropDown();
                //retorno de la vista en caso de errores al llenar un campo
                return View(registroPacienteCLS);
            }
            else
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    //Creación de un paciente
                    tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.cedula.Equals(registroPacienteCLS.paciente.cedula)).First();
                    
                    tblPaciente.nombres = registroPacienteCLS.paciente.nombres;
                    tblPaciente.apellidos = registroPacienteCLS.paciente.apellidos;
                    //tblPaciente.cedula = registroPacienteCLS.paciente.cedula;
                    tblPaciente.fechaNacimiento = registroPacienteCLS.paciente.fechaNacimiento;
                    tblPaciente.idOrientacionSexual = registroPacienteCLS.paciente.idOrientacionSexual;
                    tblPaciente.idIdentidadGenero = registroPacienteCLS.paciente.idIdentidadGenero;
                    tblPaciente.ciudadNacimiento = registroPacienteCLS.paciente.ciudadNacimiento;
                    tblPaciente.ciudadResidencia = registroPacienteCLS.paciente.ciudadResidencia;
                    tblPaciente.ocupacion = registroPacienteCLS.paciente.ocupacion;
                    tblPaciente.profesion = registroPacienteCLS.paciente.profesion;
                    tblPaciente.idTipoDiscapacidad = registroPacienteCLS.paciente.idTipoDiscapacidad;
                    tblPaciente.porcentajeDiscapacidad = registroPacienteCLS.paciente.porcentajeDiscapacidad;
                    tblPaciente.idEstadoCivil = registroPacienteCLS.paciente.idEstadoCivil;
                    tblPaciente.idLateralidad = registroPacienteCLS.paciente.idLateralidad;
                    tblPaciente.idNivelEducacion = registroPacienteCLS.paciente.idNivelEducacion;
                    tblPaciente.direccion = registroPacienteCLS.paciente.direccion;
                    tblPaciente.telefonoPersonal = registroPacienteCLS.paciente.telefonoPersonal;
                    tblPaciente.telefonoResidencial = registroPacienteCLS.paciente.telefonoResidencial;
                    tblPaciente.correoElectronico = registroPacienteCLS.paciente.correoElectronico;
                    tblPaciente.idReligion = registroPacienteCLS.paciente.idReligion;                   

                    tblContactoEmergencia tblContactoEmergencia = bd.tblContactoEmergencia.Where(p => p.idContactoEmergencia.Equals((int)tblPaciente.idContactoEmergencia)).First();
                    //tblContactoEmergencia tblContactoEmergencia = bd.tblContactoEmergencia.Where(p => p.idContactoEmergencia.Equals((int)idContactoEmergencia)).First();
                    tblContactoEmergencia.nombreContactoEmergencia = registroPacienteCLS.contactoEmergencia.nombreContactoEmergencia;
                    tblContactoEmergencia.apellidoContactoEmergencia = registroPacienteCLS.contactoEmergencia.apellidoContactoEmergencia;
                    tblContactoEmergencia.telefonoContactoEmergencia = registroPacienteCLS.contactoEmergencia.telefonoContactoEmergencia;
                    tblContactoEmergencia.correoContactoEmergencia = registroPacienteCLS.contactoEmergencia.correoContactoEmergencia;

                    //Creación y guardado de la historia clinica
                    tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente==tblPaciente.idPaciente).First();
                    //tblHistoriaClinica.idPaciente = idPaciente;
                    tblHistoriaClinica.idSeguroMedico = registroPacienteCLS.historiaClinica.idSeguroMedico;
                    tblHistoriaClinica.idTipoSangre = registroPacienteCLS.historiaClinica.idTipoSangre;
                    tblHistoriaClinica.antecedenteFamiliarClinico = registroPacienteCLS.historiaClinica.antecedenteFamiliarClinico;
                    tblHistoriaClinica.antecedenteFamiliarQuirurgico = registroPacienteCLS.historiaClinica.antecedenteFamiliarQuirurgico;
                    tblHistoriaClinica.antecedentePersonalClinico = registroPacienteCLS.historiaClinica.antecedentePersonalClinico;
                    tblHistoriaClinica.antecedentePersonalQuirurgico = registroPacienteCLS.historiaClinica.antecedentePersonalQuirurgico;
                    tblHistoriaClinica.antecedentePersonalAlergico = registroPacienteCLS.historiaClinica.antecedentePersonalAlergico;
                    tblHistoriaClinica.antecedentePersonalVacunas = registroPacienteCLS.historiaClinica.antecedentePersonalVacunas;                   
                    if (tblHistoriaClinica.idAntecedenteGinecoObstetrico != null)
                    {
                        //Creación de antecedentes gineco obstetricos de ser necesario
                        tblAntecedenteGinecoObstetrico tblAntecedenteGinecoObstetrico = bd.tblAntecedenteGinecoObstetrico.Where(p => p.idAntecedenteGinecoObstetrico.Equals((int)tblHistoriaClinica.idAntecedenteGinecoObstetrico)).First();
                        tblAntecedenteGinecoObstetrico.menarquia = registroPacienteCLS.antecedenteGinecoObstetrico.menarquia;
                        tblAntecedenteGinecoObstetrico.ciclo = registroPacienteCLS.antecedenteGinecoObstetrico.ciclo;
                        tblAntecedenteGinecoObstetrico.fechaUltimaMenstruacion = registroPacienteCLS.antecedenteGinecoObstetrico.fechaUltimaMenstruacion;
                        tblAntecedenteGinecoObstetrico.gestas = registroPacienteCLS.antecedenteGinecoObstetrico.gestas;
                        tblAntecedenteGinecoObstetrico.partos = registroPacienteCLS.antecedenteGinecoObstetrico.partos;
                        tblAntecedenteGinecoObstetrico.cesarea = registroPacienteCLS.antecedenteGinecoObstetrico.cesarea;
                        tblAntecedenteGinecoObstetrico.abortos = registroPacienteCLS.antecedenteGinecoObstetrico.abortos;
                        tblAntecedenteGinecoObstetrico.hijosMuertos = registroPacienteCLS.antecedenteGinecoObstetrico.hijosMuertos;
                        tblAntecedenteGinecoObstetrico.hijosVivos = registroPacienteCLS.antecedenteGinecoObstetrico.hijosVivos;
                        tblAntecedenteGinecoObstetrico.vidaSexualActiva = registroPacienteCLS.antecedenteGinecoObstetrico.vidaSexualActiva;
                        tblAntecedenteGinecoObstetrico.metodoPlanificacionFamiliar = registroPacienteCLS.antecedenteGinecoObstetrico.metodoPlanificacionFamiliar;                        
                    }                   
                    if (tblHistoriaClinica.idAntecedenteReprodMasculino != null)
                    {
                        tblAntecedenteReprodMasculino tblAntecedenteReprodMasculino = bd.tblAntecedenteReprodMasculino.Where(p => p.idAntecedenteReprodMasculino.Equals((int)tblHistoriaClinica.idAntecedenteReprodMasculino)).First();
                        tblAntecedenteReprodMasculino.ets = registroPacienteCLS.antecedenteReprodMasculino.ets;
                        tblAntecedenteReprodMasculino.parejaSexual = registroPacienteCLS.antecedenteReprodMasculino.parejaSexual;
                    }
                    tblHistoriaClinica.tabaco = registroPacienteCLS.historiaClinica.tabaco;
                    tblHistoriaClinica.alcohol = registroPacienteCLS.historiaClinica.alcohol;
                    tblHistoriaClinica.otrasDrogas = registroPacienteCLS.historiaClinica.otrasDrogas;
                    tblHistoriaClinica.actividadFisica = registroPacienteCLS.historiaClinica.actividadFisica;
                    tblHistoriaClinica.medicacionHabitual = registroPacienteCLS.historiaClinica.medicacionHabitual;
                    tblHistoriaClinica.habilitado = 1;
                                       
                    bd.SaveChanges();//Guardar los cambios realizados en la bdd 
                }
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Cambia el estado habilitado a 0 de un paciente y su información relacionada
        /// </summary>
        /// <param name="idPaciente">id del paciente a ser desactivado</param>
        /// <returns>Vista Index</returns>
        public ActionResult Desactivar(int idPaciente)
        {
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente.Equals(idPaciente)).First();
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == tblPaciente.idPaciente).First();
                tblPaciente.habilitado = 0;
                tblHistoriaClinica.habilitado = 0;
                List<tblEvolucion> listaEvoluciones = bd.tblEvolucion.Where(p => p.idHistoriaClinica == tblHistoriaClinica.idHistoriaClinica).ToList();
                foreach(var item in listaEvoluciones)
                {
                    item.habilitado = 0;
                }

                bd.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Dropdpwns con información de la base de datos
        /// </summary>
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

        /// <summary>
        /// Llamado a todas las funciones que llenan de datos los dropdowns
        /// </summary>
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