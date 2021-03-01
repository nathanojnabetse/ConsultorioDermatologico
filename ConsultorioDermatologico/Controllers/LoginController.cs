using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public string Login(UsuarioCLS usuarioCLS)
        {
            string mensaje = "";
            ModelState.Remove("apellidoUsuario");
            ModelState.Remove("cedulaUsuario");
            ModelState.Remove("nombreUsuario");
            ModelState.Remove("rolUsuario");
            ModelState.Remove("correoUsuario");

            if (!ModelState.IsValid)
            {
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
                string aliasUsuario = usuarioCLS.aliasUsuario;
                string contraseñaUsuario = usuarioCLS.contraseñaUsuario;
                //Cifrar y comparar con lo de la bdd
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(contraseñaUsuario);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");

                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    int numeroVeces = bd.tblUsuario.Where(p => p.aliasUsuario == aliasUsuario && p.contraseñaUsuario == cadenaContraCifrada).Count();
                    //mensaje = numeroVeces.ToString();

                    if(numeroVeces == 1)
                    {
                        tblUsuario tblUsuario = bd.tblUsuario.Where(p => p.aliasUsuario == aliasUsuario && p.contraseñaUsuario == cadenaContraCifrada).First();
                        mensaje = tblUsuario.rolUsuario;
                        //Todo el objeto Usuario para el session
                        Session["Usuario"] = tblUsuario;
                        //Variable session para permitir ciertas vistas de acuerdo al rol de usuario
                        Session["Rol"] = tblUsuario.rolUsuario;
                    }
                }
            }
                return mensaje;
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            Session["Rol"] = null;
            return RedirectToAction("Index");
        }
    }
}