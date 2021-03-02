using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultorioDermatologico.Models;

namespace ConsultorioDermatologico.Filters
{
    public class Acceder:ActionFilterAttribute
    {
        //sobreescritura del metodo onActionExecuting antes de llamar una url cargar una pantalla pasa por este metodo

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //si session usurio es nulo al login
            string nombreControlador = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string accion = filterContext.ActionDescriptor.ActionName;
            
            bool acceso = false;
            //controladores y acciones permitidas para el medico
            //Paciente Index Filtro Agregar Editar Desactivar //los llenar dropdown
            //Home Medico
            //HistoriaClinica EvolucionPaciente InformacionPaciente  //dropdowns
            //Evolucion Agregar Editar Index Guardar GuardarEdicion Desactivar
            //Documentos Index InfoPacientePDF prescripcionPDF asistenciaPDF reposoPDF verifica


            //Login Login Index CerrarSesion
            var rol = HttpContext.Current.Session["Rol"];

            if (((string)rol == "MEDICO")
                &&((nombreControlador == "Paciente" && accion == "Index")
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

            //AdministracionPaciente Index Filtro EliminarPaciente ReestablecerPaciente EliminarVisita ReestablecerVisita
            //Usuario Index Filtro Guardar Eliminar 
            //Home aDMIN
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

            //controladores y acciones permitidas para el admin
            var usuario = HttpContext.Current.Session["Usuario"];
            //Si no se ha iniciado sesion o se quiere realizar una acción no correspondiente al rol se redirige al login
            if(usuario == null || acceso == false)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
        
    }
}