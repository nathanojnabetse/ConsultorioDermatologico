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
    [Acceder] //Tag para verificar que exista sesión iniciada la acción sea permitida
    public class UsuarioController : Controller
    {
        /// <summary>
        /// GET: Usuario        
        /// </summary>
        /// <returns>Vista Index con la lista de usuarios registrados</returns>
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

        /// <summary>
        /// Filtro de busqueda en base a los parametros de entrada
        /// </summary>
        /// <param name="nombreUsuario">Busqueda por nombre</param>
        /// <returns>vista parcial con la lista de coincidencias de la busqueda</returns>
        public ActionResult Filtro(String nombreUsuario)
        {
            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();

            using (var bd = new BDD_ConsultorioDermatologicoEntities())
            {
                if (nombreUsuario == null) //Si el criterio de busqueda es nulo devuelve todos los usuarios
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
                else //coincidencias con el criterio de busqueda
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
                return PartialView("_TablaUsuarios", listaUsuario);//Vista parcial con resultados de busqueda
            }
        }

        /// <summary>
        /// Método para guardar un nuevo usuario
        /// </summary>
        /// <param name="usuarioCLS">Objeto con los datos del modelo de UsuarioCLS</param>
        /// <param name="titulo">Parámetro para el control de guardado</param>
        /// <returns>Mensajes o códigos de error o cantidad de registros guardados</returns>
        public string Guardar(UsuarioCLS usuarioCLS,int titulo)
        {
            string rpta ="" ; //Número de registros afectados
            int aliasExistentes = 0;//Control de # repeticion de nombre de usuario (Alias)
            int correoExistente = 0;//Control para el # de repeticiones del correo de usuario
            int cedulaExistente = 0;//Control para el # de repeticiones de cedula
            try
            {
                if (!ModelState.IsValid)//Verificación de campos vacios en el modelo
                {
                    var query = (from state in ModelState.Values//Valores
                                 from error in state.Errors//Mensajes
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>"; //Lista con errores
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else //Modelo valido
                {
                    using (var bd = new BDD_ConsultorioDermatologicoEntities())
                    {
                        using (var transaccion = new TransactionScope())
                        {
                            if (titulo == -1)//Agregar un nuevo Usuario
                            {
                                //Comprobación de elementos ya registrados con los datos de entrada.
                                aliasExistentes = bd.tblUsuario.Where(p => p.aliasUsuario == usuarioCLS.aliasUsuario).Count();
                                correoExistente = bd.tblUsuario.Where(p => p.correoUsuario == usuarioCLS.correoUsuario).Count();
                                cedulaExistente = bd.tblUsuario.Where(p => p.cedulaUsuario == usuarioCLS.cedulaUsuario).Count();
                                if (aliasExistentes >= 1)
                                {
                                    rpta = "-1";//Usuario con alias repetido
                                }
                                else if(correoExistente >= 1)
                                {
                                    rpta = "-2";//Correo en uso
                                }
                                else if (cedulaExistente >= 1)
                                {
                                    rpta = "-3";//Cédula ya registrada
                                }
                                else
                                {
                                    //Creación de un nuevo usuario
                                    tblUsuario tblUsuario = new tblUsuario();
                                    tblUsuario.nombresUsuario = usuarioCLS.nombreUsuario;
                                    tblUsuario.apellidosUsuario = usuarioCLS.apellidoUsuario;
                                    tblUsuario.cedulaUsuario = usuarioCLS.cedulaUsuario;
                                    tblUsuario.rolUsuario = usuarioCLS.rolUsuario;
                                    tblUsuario.aliasUsuario = usuarioCLS.aliasUsuario;
                                    //Cifrado de clave
                                    SHA256Managed sha = new SHA256Managed();
                                    byte[] byteContra = Encoding.Default.GetBytes(usuarioCLS.contraseñaUsuario);
                                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                                    string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                                    tblUsuario.contraseñaUsuario = cadenaContraCifrada;
                                    tblUsuario.correoUsuario = usuarioCLS.correoUsuario;
                                    tblUsuario.codigoMSP = usuarioCLS.codigoMSP;
                                    //Almacenamiento en la base de datos
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

        /// <summary>
        /// Eliminación de un usuario de la base de datos
        /// </summary>
        /// <param name="idUsuario">Id del usuario a eliminarse</param>
        /// <returns>Cantidad de registros afectados</returns>
        public int Eliminar(int idUsuario)
        {
            int rpta = 0;
            try
            {
                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    //Busqueda del usuario que coindida con el id de entrada
                    tblUsuario usuario = (from user in bd.tblUsuario
                                          where user.idUsuario == idUsuario
                                          select user).First();
                    if(usuario!=null) //si el usuario existe se elimina
                    {
                        bd.tblUsuario.Remove(usuario);
                        rpta = bd.SaveChanges();
                    }
                    else//Error
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