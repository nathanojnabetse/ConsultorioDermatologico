using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using ConsultorioDermatologico.Models;
using ConsultorioDermatologico.Filters;

namespace ConsultorioDermatologico.Controllers
{
    [Acceder]    
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                listaUsuario = (from usuario in bd.tblUsuario
                                                 select new UsuarioCLS
                                                 {
                                                     idUsuario = usuario.idUsuario,
                                                     nombreUsuario = usuario.nombresUsuario + " " + usuario.apellidosUsuario,
                                                     rolUsuario = usuario.rolUsuario,
                                                     aliasUsuario = usuario.aliasUsuario,
                                                     correoUsuario = usuario.correoUsuario
                                                 }).ToList();
            }
                return View(listaUsuario);
        }

        public ActionResult Filtro(String nombreUsuario)
        {
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                if (nombreUsuario == null)
                {
                    listaUsuario = (from usuario in bd.tblUsuario
                                    select new UsuarioCLS
                                    {
                                        idUsuario = usuario.idUsuario,
                                        nombreUsuario = usuario.nombresUsuario + " " + usuario.apellidosUsuario,
                                        rolUsuario = usuario.rolUsuario,
                                        aliasUsuario = usuario.aliasUsuario,
                                        correoUsuario = usuario.correoUsuario
                                    }).ToList();
                }
                else
                {
                    listaUsuario = (from usuario in bd.tblUsuario
                                    where (usuario.nombresUsuario.Contains(nombreUsuario)
                                    || usuario.apellidosUsuario.Contains(nombreUsuario))
                                    select new UsuarioCLS
                                    {
                                        idUsuario = usuario.idUsuario,
                                        nombreUsuario = usuario.nombresUsuario +" "+ usuario.apellidosUsuario,                                        
                                        rolUsuario = usuario.rolUsuario,
                                        aliasUsuario = usuario.aliasUsuario,
                                        correoUsuario = usuario.correoUsuario
                                    }).ToList();
                }
                return PartialView("_TablaUsuarios", listaUsuario);
            }
        }

        public string Guardar(UsuarioCLS usuarioCLS,int titulo)
        {
            string rpta ="" ; //numero de registros afectados
            int aliasExistentes = 0;//control de # repeticion de nombre de usuario (Alias)
            int correoExistente = 0;//Control para el # de repeticiones del correo de usuario
            int cedulaExistente = 0;//Control para el # de repeticiones del correo de usuario
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values//valores
                                 from error in state.Errors//mensajes
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    using (var bd = new BDD_ConsultorioDermatologicoEntities())
                    {
                        using (var transaccion = new TransactionScope())
                        {
                            if (titulo == -1)//Agregar un nuevo Usuario
                            {
                                aliasExistentes = bd.tblUsuario.Where(p => p.aliasUsuario == usuarioCLS.aliasUsuario).Count();
                                correoExistente = bd.tblUsuario.Where(p => p.correoUsuario == usuarioCLS.correoUsuario).Count();
                                cedulaExistente = bd.tblUsuario.Where(p => p.cedulaUsuario == usuarioCLS.cedulaUsuario).Count();
                                if (aliasExistentes >= 1)
                                {
                                    rpta = "-1";//usuario con alias repetido
                                }
                                else if(correoExistente >= 1)
                                {
                                    rpta = "-2";//correo en uso
                                }
                                else if (cedulaExistente >= 1)
                                {
                                    rpta = "-3";//cedula ya registrada
                                }
                                else
                                {
                                    tblUsuario tblUsuario = new tblUsuario();
                                    tblUsuario.nombresUsuario = usuarioCLS.nombreUsuario;
                                    tblUsuario.apellidosUsuario = usuarioCLS.apellidoUsuario;
                                    tblUsuario.cedulaUsuario = usuarioCLS.cedulaUsuario;
                                    tblUsuario.rolUsuario = usuarioCLS.rolUsuario;
                                    tblUsuario.aliasUsuario = usuarioCLS.aliasUsuario;
                                    //cifrado de clave
                                    SHA256Managed sha = new SHA256Managed();
                                    byte[] byteContra = Encoding.Default.GetBytes(usuarioCLS.contraseñaUsuario);
                                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                                    string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                                    tblUsuario.contraseñaUsuario = cadenaContraCifrada;
                                    tblUsuario.correoUsuario = usuarioCLS.correoUsuario;
                                    tblUsuario.codigoMSP = usuarioCLS.codigoMSP;
                                    bd.tblUsuario.Add(tblUsuario);
                                    rpta = bd.SaveChanges().ToString();
                                    transaccion.Complete();
                                }
                                
                            }
                            else
                            {
                                rpta = "";
                            }                            
                        }
                    }
                }
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                rpta = "";
            }
            
                return rpta;
        }

        public int Eliminar(int idUsuario)
        {
            int rpta = 0;
            try
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    tblUsuario usuario = (from user in bd.tblUsuario
                                          where user.idUsuario == idUsuario
                                          select user).First();
                    if(usuario!=null)
                    {
                        bd.tblUsuario.Remove(usuario);
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