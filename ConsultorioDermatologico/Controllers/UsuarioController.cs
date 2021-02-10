using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
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
                                        nombreUsuario = usuario.nombresUsuario +" "+ usuario.apellidosUsuario,                                        
                                        rolUsuario = usuario.rolUsuario,
                                        aliasUsuario = usuario.aliasUsuario,
                                        correoUsuario = usuario.correoUsuario
                                    }).ToList();
                }
                return PartialView("_TablaUsuarios", listaUsuario);
            }
        }
    }
}