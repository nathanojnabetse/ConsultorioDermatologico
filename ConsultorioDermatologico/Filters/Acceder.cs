using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Filters
{
    /// <summary>
    /// Clase que verifica el inicio de sesión y permite acceso a ciertas acciones
    /// </summary>
    public class Acceder:ActionFilterAttribute
    {
        /// <summary>
        /// Sobreescritura del metodo onActionExecuting antes de llamar una url cargar una pantalla pasa por este metodo
        /// </summary>
        /// <param name="filterContext">Accion ejecutada</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //nombre del controlador y acción actual
            string nombreControlador = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string accion = filterContext.ActionDescriptor.ActionName;
            
            bool acceso = false;
            
            var rol = HttpContext.Current.Session["Rol"];

            //Controladores y acciones permitidas para el médico
            if (((string)rol == "MEDICO")
                && ((nombreControlador == "Paciente" && accion == "Index")
                || (nombreControlador == "Paciente" && accion == "Filtro")
                || (nombreControlador == "Paciente" && accion == "Agregar")
                || (nombreControlador == "Paciente" && accion == "Editar")
                || (nombreControlador == "Paciente" && accion == "Desactivar")
                || (nombreControlador == "Home" && accion == "Medico")
                || (nombreControlador == "HistoriaClinica" && accion == "EvolucionPaciente")
                || (nombreControlador == "HistoriaClinica" && accion == "InformacionPaciente")
                || (nombreControlador == "Evolucion" && accion == "Index")
                || (nombreControlador == "Evolucion" && accion == "Agregar")
                || (nombreControlador == "Evolucion" && accion == "Guardar")
                || (nombreControlador == "Evolucion" && accion == "Editar")
                || (nombreControlador == "Evolucion" && accion == "GuardarEdicion")
                || (nombreControlador == "Evolucion" && accion == "Desactivar")
                || (nombreControlador == "Documentos" && accion == "Index")
                || (nombreControlador == "Documentos" && accion == "InfoPacientePDF")
                || (nombreControlador == "Documentos" && accion == "prescripcionPDF")
                || (nombreControlador == "Documentos" && accion == "asistenciaPDF")
                || (nombreControlador == "Documentos" && accion == "reposoPDF")
                || (nombreControlador == "Documentos" && accion == "verifica")
                || (nombreControlador == "Cie10" && accion == "Index")
                || (nombreControlador == "Cie10" && accion == "Filtro")
                || (nombreControlador == "Home" && accion == "Medico")
                || (nombreControlador == "Login" && accion == "Login")
                || (nombreControlador == "Login" && accion == "CerrarSesion")))
            {
                acceso = true;
            }

            //Controladores y acciones permitidas para el admin
            if (((string)rol == "ADMINISTRADOR")
                && ((nombreControlador == "AdministracionPacientes" && accion == "Index")
                || (nombreControlador == "AdministracionPacientes" && accion == "Filtro")
                || (nombreControlador == "AdministracionPacientes" && accion == "EliminarPaciente")
                || (nombreControlador == "AdministracionPacientes" && accion == "ReestablecerPaciente")
                || (nombreControlador == "AdministracionPacientes" && accion == "EliminarVisita")
                || (nombreControlador == "AdministracionPacientes" && accion == "ReestablecerVisita")
                || (nombreControlador == "Usuario" && accion == "Index")
                || (nombreControlador == "Usuario" && accion == "Filtro")
                || (nombreControlador == "Usuario" && accion == "Guardar")
                || (nombreControlador == "Usuario" && accion == "Eliminar")
                || (nombreControlador == "Home" && accion == "Admin")
                || (nombreControlador == "Login" && accion == "Index")
                || (nombreControlador == "Login" && accion == "Login")
                || (nombreControlador == "Login" && accion == "CerrarSesion")))
            {
                acceso = true;
            }

            var usuario = HttpContext.Current.Session["Usuario"];

            //Si no se ha iniciado sesion o se quiere realizar una acción no correspondiente al rol se redirige al login
            if (usuario == null || acceso == false)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
        
    }
}