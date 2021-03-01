using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultorioDermatologico.Filters
{
    public class Acceder:ActionFilterAttribute
    {
        //sobreescritura del metodo onActionExecuting antes de llamar una url cargar una pantalla pasa por este metodo

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //si session usurio es nulo al login
            var usuario = HttpContext.Current.Session["Usuario"];
            if(usuario == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
        
    }
}