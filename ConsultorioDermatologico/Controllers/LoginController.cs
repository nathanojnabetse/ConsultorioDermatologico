﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;
using ConsultorioDermatologico.ClasesAuxiliares;

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
                        Session["NombreUsuario"] = tblUsuario.nombresUsuario + " "+ tblUsuario.apellidosUsuario;
                        Session["cedula"] = tblUsuario.cedulaUsuario;
                        Session["codigoMSP"] = tblUsuario.codigoMSP;
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

        public string RecuperarContra(string correo, string cedula)
        {
            string mensaje = "";
            using (var bd= new BDD_ConsultorioDermatologicoEntities())
            {
                int cantidad = 0;
                cantidad = bd.tblUsuario.Where(p => p.correoUsuario == correo && p.cedulaUsuario == cedula).Count();
                if (cantidad == 0)
                {
                    mensaje = "No existe un a persona registrada con esa informacion";
                }
                else
                {
                    tblUsuario tblUsuario = bd.tblUsuario.Where(p => p.correoUsuario == correo && p.cedulaUsuario == cedula).First();
                    
                    //Modificar su clave
                    Random ra = new Random();
                    int n1 = ra.Next(1000, 9999);

                    string nuevaContra = n1.ToString();
                    //cifrar clave
                    SHA256Managed sha = new SHA256Managed();
                    byte[] byteContra = Encoding.Default.GetBytes(nuevaContra);
                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                    string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");

                    tblUsuario.contraseñaUsuario = cadenaContraCifrada;
                    mensaje = bd.SaveChanges().ToString();

                    Correo.enviarCorreo(correo, "RECUPERACION DE CLAVE - CONSULTORIO DERMATOLÓGICO", "Su clave ha sido reestablecida, su nueva clave es: "+nuevaContra, Server.MapPath("~/Resources/PersonasCorreo.txt"));
                }


            }
            return mensaje;
        }
    }
}