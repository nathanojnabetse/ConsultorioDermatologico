using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Filters;

namespace ConsultorioDermatologico.Controllers
{
    [Acceder] //Tag para verificar que exista sesión iniciada la acción sea permitida
    public class HomeController : Controller
    {
        
        /// <summary>
        /// Vista Home para el admin
        /// </summary>
        /// <returns>vista admin</returns>
        public ActionResult Admin()
        {
            return View();
        }
        /// <summary>
        /// Vista Home para el medico
        /// </summary>
        /// <returns>vista medico</returns>
        public ActionResult Medico()
        {
            return View();
        }
    }
}