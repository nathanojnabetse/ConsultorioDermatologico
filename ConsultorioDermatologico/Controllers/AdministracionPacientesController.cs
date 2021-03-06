﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;
using ConsultorioDermatologico.Filters;

namespace ConsultorioDermatologico.Controllers
{
    [Acceder] //Tag para verificar que exista sesión iniciada la acción sea permitida
    public class AdministracionPacientesController : Controller
    {
        /// <summary>
        /// // GET: AdministracionPacientes
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Filtro en base a nombre apellido o cedula
        /// </summary>
        /// <param name="busqueda">string de búsqueda</param>
        /// <returns>Vista con coincidencias en base al criterio de busqueda</returns>
        public ActionResult Filtro(String busqueda)
        {
            List<PacienteCLS> listaPacientesDesactivados = new List<PacienteCLS>();
            List<EvolucionCLS> listaEvolucionesDesactivadas = new List<EvolucionCLS>();
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                if(busqueda == null)
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
                else
                {
                    listaPacientesDesactivados = (from historiaClinica in bd.tblHistoriaClinica
                                                  join paciente in bd.tblPaciente
                                                  on historiaClinica.idPaciente equals paciente.idPaciente
                                                  where historiaClinica.habilitado == 0
                                                  && paciente.habilitado == 0
                                                  &&((paciente.cedula.Contains(busqueda)
                                                  || (paciente.nombres.Contains(busqueda)
                                                  || paciente.apellidos.Contains(busqueda))))
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
                                                    && ((paciente.cedula.Contains(busqueda)
                                                    || (paciente.nombres.Contains(busqueda)
                                                    || paciente.apellidos.Contains(busqueda))))
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
                
            }

            ViewBag.listaPacientesDesactivados = listaPacientesDesactivados;
            ViewBag.listaEvolucionesDesactivadas = listaEvolucionesDesactivadas;

            return PartialView("_TablaDesactivados");
        }

        /// <summary>
        /// Eliminación total de un paciente
        /// </summary>
        /// <param name="idPaciente">id del paciente a eleiminar</param>
        /// <returns>cantidad de registros afectado</returns>
        public int EliminarPaciente(int? idPaciente)
        {
            int rpta = 0;
            try
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    if(idPaciente!=null)
                    {
                        tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == idPaciente).First();
                        tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == tblPaciente.idPaciente).First();
                        List<tblEvolucion> listaEvoluciones = bd.tblEvolucion.Where(p => p.idHistoriaClinica == tblHistoriaClinica.idHistoriaClinica).ToList();
                        List<tblFotos> listaFotos = new List<tblFotos>();
                        foreach (var item in listaEvoluciones)
                        {
                            List<tblFotos> tblFotos = bd.tblFotos.Where(p => p.idEvolucion == item.idEvolucion).ToList();
                            foreach (var item2 in tblFotos)
                            {
                                bd.tblFotos.Remove(item2);
                            }
                            bd.tblEvolucion.Remove(item);
                        }
                        bd.tblHistoriaClinica.Remove(tblHistoriaClinica);
                        bd.tblPaciente.Remove(tblPaciente);

                        rpta = bd.SaveChanges();
                    }
                    else
                    {
                        rpta = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rpta = 0;
            }
            return rpta;
        }

        /// <summary>
        /// Restablecimiento de pacientes
        /// </summary>
        /// <param name="idPaciente">id del paciente a reestablecer</param>
        /// <returns>cantidad de registros afectados</returns>
        public int ReestablecerPaciente(int? idPaciente)
        {
            int rpta = 0;
            try
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    if (idPaciente != null)
                    {
                        tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == idPaciente).First();
                        tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == tblPaciente.idPaciente).First();
                        List<tblEvolucion> listaEvoluciones = bd.tblEvolucion.Where(p => p.idHistoriaClinica == tblHistoriaClinica.idHistoriaClinica && p.habilitado == 0).ToList();
                        foreach (var item in listaEvoluciones)
                        {
                            item.habilitado = 1;
                        }
                        tblHistoriaClinica.habilitado = 1;
                        tblPaciente.habilitado = 1;

                        rpta = bd.SaveChanges();
                    }
                    else
                    {
                        rpta = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rpta = 0;
            }
            return rpta;
        }

        /// <summary>
        /// Elimina un registro de visita
        /// </summary>
        /// <param name="idEvolucion">id de la Evolución a eliminar</param>
        /// <returns>cantidad de registros afectados</returns>
        public int EliminarVisita(int? idEvolucion)
        {
            int rpta = 0;
            try
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    if (idEvolucion != null)
                    {
                        tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion).First();
                        List<tblFotos> listaFotos = bd.tblFotos.Where(p => p.idEvolucion == tblEvolucion.idEvolucion).ToList();
                        foreach(var item in listaFotos)
                        {
                            bd.tblFotos.Remove(item);
                        }                      
                        bd.tblEvolucion.Remove(tblEvolucion);

                        rpta = bd.SaveChanges();
                    }
                    else
                    {
                        rpta = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rpta = 0;
            }
            return rpta;
        }

        /// <summary>
        /// Reestablece una visita
        /// </summary>
        /// <param name="idEvolucion">id de la visita a reestablecer</param>
        /// <returns>canidad de registros afectados</returns>
        public int ReestablecerVisita(int? idEvolucion)
        {
            int rpta = 0;
            try
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    if (idEvolucion != null)
                    {
                        tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion).First();
                        tblEvolucion.habilitado = 1;

                        rpta = bd.SaveChanges();
                    }
                    else
                    {
                        rpta = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rpta = 0;
            }
            return rpta;
        }
    }
}   
