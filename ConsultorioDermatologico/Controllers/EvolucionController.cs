using ConsultorioDermatologico.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                                        idEvolucion = evolucion.idEvolucion,
                                        motivoConsulta = evolucion.motivoConsulta,
                                        fechaVisita = (DateTime)evolucion.fechaVisita
                                    }).ToList();
            }
                return View(listaEvoluciones);
        }

        //ACion para retornar la vista con info del paciente
        public ActionResult Agregar(int idHistoriaClinica)
        {
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == idHistoriaClinica).First();
                ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;

                tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente== tblHistoriaClinica.idPaciente).First();
                ViewBag.nombrePaciente = tblPaciente.nombres + " " + tblPaciente.apellidos;
                ViewBag.idPaciente = tblPaciente.idPaciente;

                ViewBag.fechaActual = System.DateTime.Now.ToString("yyyy-MM-dd");

                tblEvolucion tblEvolucion = bd.tblEvolucion.ToList().Last();
            }
                return View();
        }

        public (string,int) Guardar(RegistroEvolucionCLS registroEvolucionCLS, HttpPostedFileBase foto1, HttpPostedFileBase foto2, HttpPostedFileBase foto3, HttpPostedFileBase foto4, HttpPostedFileBase foto5, HttpPostedFileBase foto6, int idHistoriaClinica,string mapaCorporal, string foto1NombreFoto, string foto2NombreFoto, string foto3NombreFoto, string foto4NombreFoto, string foto5NombreFoto, string foto6NombreFoto)
        {
            string mensaje = "";
            int idEvolucion = 0;
            try
            {
                ModelState.Remove("foto1");
                ModelState.Remove("foto2");
                ModelState.Remove("foto3");
                ModelState.Remove("foto4");
                ModelState.Remove("foto5");
                ModelState.Remove("foto6");

                if (!ModelState.IsValid)
                {
                    
                    //un listado de errores en caso de existir campos incompletos
                    var query = (from state in ModelState.Values//valores
                                 from error in state.Errors//mensajes
                                 select error.ErrorMessage).ToList();
                    mensaje += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        mensaje += "<li class='list-group-item'>" + item + "</li>";
                    }
                    mensaje += "</ul>";
                }
                else
                {
                    ////Imagen del canvas 
                    //var streamMapa = new MemoryStream(); //creacion de un stream a partir del string
                    //var writer = new StreamWriter(streamMapa);
                    //writer.Write(mapaCorporal);
                    //writer.Flush();
                    //streamMapa.Position = 0;
                    ////paso al array de bytes del stream obtenido
                    //byte[] fotoMapaCorporalBD = null;
                    //BinaryReader lector = new BinaryReader(streamMapa);
                    //fotoMapaCorporalBD = lector.ReadBytes((int)streamMapa.Length);

                    using (var bd = new BDD_ConsultorioDermatologicoEntities())
                    {
                        tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == idHistoriaClinica).First();
                        ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;

                        tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == tblHistoriaClinica.idPaciente).First();
                        ViewBag.nombrePaciente = tblPaciente.nombres + " " + tblPaciente.apellidos;
                        ViewBag.idPaciente = tblPaciente.idPaciente;

                        ViewBag.fechaActual = System.DateTime.Now.ToString("yyyy-MM-dd");

                        //if tiitulo o edicion ver despues si se añade o no.idEvolucion + 1;
                        tblEvolucion tblEvolucion = new tblEvolucion();
                        tblEvolucion.idHistoriaClinica = idHistoriaClinica;

                        tblEvolucion.mapaCorporal = mapaCorporal;

                        tblEvolucion.nombreMapa = "MapaCorporal" + idHistoriaClinica +"-"+ System.DateTime.Now.ToString("yyyy-MM-dd") + ".png";
                        tblEvolucion.diagnostico = registroEvolucionCLS.evolucion.diagnostico;
                        tblEvolucion.motivoConsulta = registroEvolucionCLS.evolucion.motivoConsulta;
                        tblEvolucion.examenFisico = registroEvolucionCLS.evolucion.examenFisico;
                        tblEvolucion.prescripcion = registroEvolucionCLS.evolucion.prescripcion;
                        tblEvolucion.recomendaciones = registroEvolucionCLS.evolucion.recomendaciones;
                        tblEvolucion.fechaVisita = System.DateTime.Now;
                        tblEvolucion.habilitado = 1;
                        bd.tblEvolucion.Add(tblEvolucion);
                        
                        //recepcion de las foto y trnasformación a un archivo binario para las imagenes añadidas
                        byte[] fotoBD1 = null;
                        if (foto1 != null)
                        {
                            BinaryReader lector1 = new BinaryReader(foto1.InputStream);
                            fotoBD1 = lector1.ReadBytes((int)foto1.ContentLength);
                            tblFotos tblFoto1 = new tblFotos();
                            tblFoto1.idEvolucion = tblEvolucion.idEvolucion;
                            tblFoto1.foto = fotoBD1;
                            tblFoto1.nombreFoto = foto1NombreFoto;
                            bd.tblFotos.Add(tblFoto1);
                        }
                        byte[] fotoBD2 = null;
                        if (foto2 != null)
                        {
                            BinaryReader lector2 = new BinaryReader(foto2.InputStream);
                            fotoBD2 = lector2.ReadBytes((int)foto2.ContentLength);
                            tblFotos tblFoto2 = new tblFotos();
                            tblFoto2.idEvolucion = tblEvolucion.idEvolucion;
                            tblFoto2.foto = fotoBD2;
                            tblFoto2.nombreFoto = foto2NombreFoto;
                            bd.tblFotos.Add(tblFoto2);
                        }
                        byte[] fotoBD3 = null;
                        if (foto3 != null)
                        {
                            BinaryReader lector3 = new BinaryReader(foto3.InputStream);
                            fotoBD3 = lector3.ReadBytes((int)foto3.ContentLength);
                            tblFotos tblFoto3 = new tblFotos();
                            tblFoto3.idEvolucion = tblEvolucion.idEvolucion;
                            tblFoto3.foto = fotoBD3;
                            tblFoto3.nombreFoto = foto3NombreFoto;
                            bd.tblFotos.Add(tblFoto3);
                        }
                        byte[] fotoBD4 = null;
                        if (foto4 != null)
                        {
                            BinaryReader lector4 = new BinaryReader(foto4.InputStream);
                            fotoBD4 = lector4.ReadBytes((int)foto4.ContentLength);
                            tblFotos tblFoto4 = new tblFotos();
                            tblFoto4.idEvolucion = tblEvolucion.idEvolucion;
                            tblFoto4.foto = fotoBD4;
                            tblFoto4.nombreFoto = foto4NombreFoto;
                            bd.tblFotos.Add(tblFoto4);
                        }
                        byte[] fotoBD5 = null;
                        if (foto5 != null)
                        {
                            BinaryReader lector5 = new BinaryReader(foto5.InputStream);
                            fotoBD5 = lector5.ReadBytes((int)foto5.ContentLength);
                            tblFotos tblFoto5 = new tblFotos();
                            tblFoto5.idEvolucion = tblEvolucion.idEvolucion;
                            tblFoto5.foto = fotoBD5;
                            tblFoto5.nombreFoto = foto5NombreFoto;
                            bd.tblFotos.Add(tblFoto5);
                        }
                        byte[] fotoBD6 = null;
                        if (foto6 != null)
                        {
                            BinaryReader lector6 = new BinaryReader(foto6.InputStream);
                            fotoBD6 = lector6.ReadBytes((int)foto6.ContentLength);
                            tblFotos tblFoto6 = new tblFotos();
                            tblFoto6.idEvolucion = tblEvolucion.idEvolucion;                           
                            tblFoto6.foto = fotoBD6;
                            tblFoto6.nombreFoto = foto6NombreFoto;
                            bd.tblFotos.Add(tblFoto6);
                        }

                        mensaje = bd.SaveChanges().ToString();
                        if (mensaje == "0")
                        {
                            mensaje = "";
                        }

                        idEvolucion = tblEvolucion.idEvolucion;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                mensaje = "";
            }
           
            return (mensaje, idEvolucion);
        }

        //recuperar informacion a la vista Editar
        public ActionResult Editar(int idEvolucion)
        {


            //tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == idEvolucion).First();
            //ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;

            //tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == tblHistoriaClinica.idPaciente).First();
          
            //ViewBag.idPaciente = tblPaciente.idPaciente;


            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {

                
                EvolucionCLS evolucionCLS = new EvolucionCLS();
                
                tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion && p.habilitado == 1).First();
                ViewBag.idEvolucion = tblEvolucion.idEvolucion;
               //viewbags
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idHistoriaClinica == tblEvolucion.idHistoriaClinica).First();
                ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;
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

                List<tblFotos> listaFotosBD = bd.tblFotos.Where(p => p.idEvolucion == idEvolucion).ToList();
                List<FotoCLS> listaFotos = new List<FotoCLS>();

                foreach(var item in listaFotosBD)
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

        
        public string GuardarEdicion(RegistroEvolucionCLS registroEvolucionCLS, HttpPostedFileBase foto1, HttpPostedFileBase foto2, HttpPostedFileBase foto3, HttpPostedFileBase foto4, HttpPostedFileBase foto5, HttpPostedFileBase foto6, int idHistoriaClinica, int idEvolucion, string mapaCorporal, string foto1NombreFoto, string foto2NombreFoto, string foto3NombreFoto, string foto4NombreFoto, string foto5NombreFoto, string foto6NombreFoto, int? idFoto1, int? idFoto2, int? idFoto3, int? idFoto4, int? idFoto5, int? idFoto6)
        {
            string mensaje = "";
            try
            {
                ModelState.Remove("foto1");
                ModelState.Remove("foto2");
                ModelState.Remove("foto3");
                ModelState.Remove("foto4");
                ModelState.Remove("foto5");
                ModelState.Remove("foto6");

                if (!ModelState.IsValid)
                {

                    //un listado de errores en caso de existir campos incompletos
                    var query = (from state in ModelState.Values//valores
                                 from error in state.Errors//mensajes
                                 select error.ErrorMessage).ToList();
                    mensaje += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        mensaje += "<li class='list-group-item'>" + item + "</li>";
                    }
                    mensaje += "</ul>";
                }
                else
                {
                    using (var bd = new BDD_ConsultorioDermatologicoEntities())
                    {
                        tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idHistoriaClinica == idHistoriaClinica).First();
                        //ViewBag.idHistoriaClinica = tblHistoriaClinica.idHistoriaClinica;

                        //tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == tblHistoriaClinica.idPaciente).First();
                        //ViewBag.nombrePaciente = tblPaciente.nombres + " " + tblPaciente.apellidos;
                        //ViewBag.idPaciente = tblPaciente.idPaciente;

                        //ViewBag.fechaActual = System.DateTime.Now.ToString("yyyy-MM-dd");

                        //if tiitulo o edicion ver despues si se añade o no
                        tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion).First();

                        //tblEvolucion.idHistoriaClinica = idHistoriaClinica;
                        if(mapaCorporal != "")
                        {
                            tblEvolucion.mapaCorporal = mapaCorporal;
                        }

                        //tblEvolucion.nombreMapa = "MapaCorporal" + idHistoriaClinica +"-"+ idEvolucion+ ".png";
                        tblEvolucion.diagnostico = registroEvolucionCLS.evolucion.diagnostico;
                        tblEvolucion.motivoConsulta = registroEvolucionCLS.evolucion.motivoConsulta;
                        tblEvolucion.examenFisico = registroEvolucionCLS.evolucion.examenFisico;
                        tblEvolucion.prescripcion = registroEvolucionCLS.evolucion.prescripcion;
                        tblEvolucion.recomendaciones = registroEvolucionCLS.evolucion.recomendaciones;
                        //tblEvolucion.fechaVisita = System.DateTime.Now;
                        //tblEvolucion.habilitado = 1;
                        //bd.tblEvolucion.Add(tblEvolucion);

                        //recepcion de las foto y trnasformación a un archivo binario para las imagenes añadidas
                        byte[] fotoBD1 = null;
                        if (foto1 != null)
                        {
                            BinaryReader lector1 = new BinaryReader(foto1.InputStream);
                            fotoBD1 = lector1.ReadBytes((int)foto1.ContentLength);
                            
                            if(idFoto1 != null)
                            {
                                tblFotos tblFoto1Edit = bd.tblFotos.Where(p => p.idFoto == idFoto1).First();//new tblFotos();///condicional
                                tblFoto1Edit.foto = fotoBD1;
                                tblFoto1Edit.nombreFoto = foto1NombreFoto;
                            }
                            else
                            {
                                tblFotos tblFoto1New = new tblFotos();
                                tblFoto1New.idEvolucion = tblEvolucion.idEvolucion;
                                tblFoto1New.foto = fotoBD1;
                                tblFoto1New.nombreFoto = foto1NombreFoto;
                                bd.tblFotos.Add(tblFoto1New);
                            }

                        }

                        byte[] fotoBD2 = null;
                        if (foto2 != null)
                        {
                            BinaryReader lector2 = new BinaryReader(foto2.InputStream);
                            fotoBD2 = lector2.ReadBytes((int)foto2.ContentLength);
                            if (idFoto2 != null)
                            {
                                tblFotos tblFoto2Edit = bd.tblFotos.Where(p => p.idFoto == idFoto2).First();//new tblFotos();///condicional
                                tblFoto2Edit.foto = fotoBD2;
                                tblFoto2Edit.nombreFoto = foto2NombreFoto;
                            }
                            else
                            {
                                tblFotos tblFoto2New = new tblFotos();
                                tblFoto2New.idEvolucion = tblEvolucion.idEvolucion;
                                tblFoto2New.foto = fotoBD2;
                                tblFoto2New.nombreFoto = foto2NombreFoto;
                                bd.tblFotos.Add(tblFoto2New);
                            }
                           
                        }
                        byte[] fotoBD3 = null;
                        if (foto3 != null)
                        {
                            BinaryReader lector3 = new BinaryReader(foto3.InputStream);
                            fotoBD3 = lector3.ReadBytes((int)foto3.ContentLength);
                            if (idFoto3 != null)
                            {
                                tblFotos tblFoto3Edit = bd.tblFotos.Where(p => p.idFoto == idFoto3).First();//new tblFotos();///condicional
                                tblFoto3Edit.foto = fotoBD3;
                                tblFoto3Edit.nombreFoto = foto3NombreFoto;
                            }
                            else                            
                            {
                                tblFotos tblFoto3New = new tblFotos();
                                tblFoto3New.idEvolucion = tblEvolucion.idEvolucion;
                                tblFoto3New.foto = fotoBD3;
                                tblFoto3New.nombreFoto = foto3NombreFoto;
                                bd.tblFotos.Add(tblFoto3New);
                            }

                        }
                        byte[] fotoBD4 = null;
                        if (foto4 != null)
                        {
                            BinaryReader lector4 = new BinaryReader(foto4.InputStream);
                            fotoBD4 = lector4.ReadBytes((int)foto4.ContentLength);
                            if (idFoto4 != null)
                            {
                                tblFotos tblFoto4Edit = bd.tblFotos.Where(p => p.idFoto == idFoto4).First();//new tblFotos();///condicional
                                tblFoto4Edit.foto = fotoBD4;
                                tblFoto4Edit.nombreFoto = foto4NombreFoto;
                            }
                            else
                            {
                                tblFotos tblFoto4New = new tblFotos();
                                tblFoto4New.idEvolucion = tblEvolucion.idEvolucion;
                                tblFoto4New.foto = fotoBD4;
                                tblFoto4New.nombreFoto = foto4NombreFoto;
                                bd.tblFotos.Add(tblFoto4New);
                            }
                           
                        }
                        byte[] fotoBD5 = null;
                        if (foto5 != null)
                        {
                            BinaryReader lector5 = new BinaryReader(foto5.InputStream);
                            fotoBD5 = lector5.ReadBytes((int)foto5.ContentLength);
                            if (idFoto5 != null)
                            {
                                tblFotos tblFoto5Edit = bd.tblFotos.Where(p => p.idFoto == idFoto5).First();//new tblFotos();///condicional
                                tblFoto5Edit.foto = fotoBD5;
                                tblFoto5Edit.nombreFoto = foto5NombreFoto;
                            }
                            else
                            {
                                tblFotos tblFoto5New = new tblFotos();
                                tblFoto5New.idEvolucion = tblEvolucion.idEvolucion;
                                tblFoto5New.foto = fotoBD5;
                                tblFoto5New.nombreFoto = foto5NombreFoto;
                                bd.tblFotos.Add(tblFoto5New);
                            }
                            
                        }
                        byte[] fotoBD6 = null;
                        if (foto6 != null)
                        {
                            BinaryReader lector6 = new BinaryReader(foto6.InputStream);
                            fotoBD6 = lector6.ReadBytes((int)foto6.ContentLength); 
                            if (idFoto6 != null)
                            {
                                tblFotos tblFoto6Edit = bd.tblFotos.Where(p => p.idFoto == idFoto6).First();//new tblFotos();///condicional
                                tblFoto6Edit.foto = fotoBD6;
                                tblFoto6Edit.nombreFoto = foto6NombreFoto;
                            }
                            else
                            {
                                tblFotos tblFoto6New = new tblFotos();
                                tblFoto6New.idEvolucion = tblEvolucion.idEvolucion;
                                tblFoto6New.foto = fotoBD6;
                                tblFoto6New.nombreFoto = foto6NombreFoto;
                                bd.tblFotos.Add(tblFoto6New);
                            }
                           
                        }

                        mensaje = bd.SaveChanges().ToString();
                        if (mensaje == "0")
                        {
                            mensaje = "";
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mensaje = "";
            }

            return mensaje;
        }

        public ActionResult Desactivar(int idEvolucion)
        {
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion).First();

                tblEvolucion.habilitado = 0;

                bd.SaveChanges();
                tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idHistoriaClinica == tblEvolucion.idHistoriaClinica).First();
                
                return RedirectToAction("Index",new { idPaciente = tblHistoriaClinica.idPaciente });
            }
        }

    }
}