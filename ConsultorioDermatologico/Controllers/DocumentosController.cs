using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ConsultorioDermatologico.Models;
using System.IO;
using ConsultorioDermatologico.Filters;

namespace ConsultorioDermatologico.Controllers
{
    [Acceder] //Tag para verificar que exista sesión iniciada la acción sea permitida
    public class DocumentosController : Controller
    {
        //Obtención de los datos de session para firmas y nombres en certificados
        public string nombreMedico = (string)System.Web.HttpContext.Current.Session["NombreUsuario"];
        public string cedulaMedico = (string)System.Web.HttpContext.Current.Session["cedula"];
        public string codigoMSP = (string)System.Web.HttpContext.Current.Session["codigoMSP"];

        /// <summary>
        /// GET: Documentos
        /// Vista con las opciones de documentos posibles a generar
        /// </summary>
        /// <param name="idEvolucion">id de Evolución actual</param>
        /// <param name="idPaciente">id del paciente en la visita</param>
        /// <returns>vista Index</returns>
        public ActionResult Index(int idEvolucion, int idPaciente)
        {
            ViewBag.idEvolucion = idEvolucion;
            ViewBag.idPaciente = idPaciente;
            return View();
        }

        /// <summary>
        /// Creación del documento con toda la información de la historia clínica del paciente
        /// </summary>
        /// <param name="idEvolucion">id de Evolución actual</param>
        /// <returns>Archivo PDF con la información del paciente</returns>
        public FileResult InfoPacientePDF(int idPaciente)
        {
            Document doc = new Document();
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);//guardar el doc en memoria
                doc.Open();
                Image png = Image.GetInstance(new Uri(Server.MapPath("~/Resources/Galenus.png")));
                png.ScalePercent(24f);
                doc.Add(png);
                //IMAGEN
                Paragraph info1 = new Paragraph(nombreMedico + ",M.D.");
                Paragraph info2 = new Paragraph("Médico Dermatólogo");
                Paragraph info3 = new Paragraph("PARIS N43 - 212 Y RÍO COCA");
                Paragraph info4 = new Paragraph("TELÉFONO 2263720");
                Paragraph fechaHoy = new Paragraph("Fecha de emisión: " + DateTime.Now.ToString("yyy-MM-dd"));
                info1.Alignment = Element.ALIGN_CENTER;
                info2.Alignment = Element.ALIGN_CENTER;
                info3.Alignment = Element.ALIGN_CENTER;
                info4.Alignment = Element.ALIGN_CENTER;
                fechaHoy.Alignment = Element.ALIGN_RIGHT;
                doc.Add(info1);
                doc.Add(info2);
                doc.Add(info3);
                doc.Add(info4);
                doc.Add(fechaHoy);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);
                doc.Add(espacio);

                using (var bd = new  BDD_ConsultorioDermatologicoEntities())
                {

                    tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == idPaciente).First();
                    Paragraph p0 = new Paragraph("- - - DATOS PERSONALES DEL PACIENTE - - -");
                    p0.Alignment = Element.ALIGN_CENTER;
                    doc.Add(p0);
                    doc.Add(espacio);
                    Paragraph p1 = new Paragraph("Nombre del paciente: "+ tblPaciente.nombres +" "+tblPaciente.apellidos);                    
                    doc.Add(p1);
                    Paragraph p2 = new Paragraph("Cédula: " +tblPaciente.cedula);
                    doc.Add(p2);
                    Paragraph p3 = new Paragraph("Fecha de nacimiento: " +tblPaciente.fechaNacimiento.Value.ToString("yyyy-MM-dd"));                    
                    doc.Add(p3);
                    Paragraph p4 = new Paragraph("Identidad de Género: "+tblPaciente.tblIdentidadGenero.nombreIdentidadGenero);                    
                    doc.Add(p4);
                    Paragraph p5 = new Paragraph("Orientación Sexual: "+tblPaciente.tblOrientacionSexual.nombreOrientacionSexual);                    
                    doc.Add(p5);
                    Paragraph p6 = new Paragraph("Ciudad de nacimiento: "+tblPaciente.ciudadNacimiento);
                    doc.Add(p6);
                    Paragraph p7 = new Paragraph("Ciudad de residencia: "+tblPaciente.ciudadResidencia);
                    doc.Add(p7);
                    Paragraph p8 = new Paragraph("Ocupación: " +tblPaciente.ocupacion);
                    doc.Add(p8);
                    Paragraph p9 = new Paragraph("Profesión: " +tblPaciente.profesion);
                    doc.Add(p9);
                    Paragraph p10 = new Paragraph("Tipo discapacidad: " +tblPaciente.idTipoDiscapacidad);
                    doc.Add(p10);
                    Paragraph p11 = new Paragraph("Porcentaje de discapacidad: "+tblPaciente.porcentajeDiscapacidad.ToString());                    
                    doc.Add(p11);
                    Paragraph p12 = new Paragraph("Estado civil: "+ tblPaciente.tblEstadoCivil.nombreEstadoCivil);
                    doc.Add(p12);
                    Paragraph p13 = new Paragraph("Lateralidad: "+ tblPaciente.tblLateralidad.nombreLateralidad);
                    doc.Add(p13);
                    Paragraph p14 = new Paragraph("Nivel de educación: " +tblPaciente.tblNivelEducacion.nombreNivelEducacion);
                    doc.Add(p14);
                    Paragraph p15 = new Paragraph("Dirección: " +tblPaciente.direccion);
                    doc.Add(p15);
                    Paragraph p16 = new Paragraph("Teléfono personal: " +tblPaciente.telefonoPersonal);
                    doc.Add(p16);
                    Paragraph p17 = new Paragraph("Teléfono residencial: "+tblPaciente.telefonoResidencial);
                    doc.Add(p17);
                    Paragraph p18 = new Paragraph("Correo electrónico: "+tblPaciente.correoElectronico);
                    doc.Add(p18);
                    Paragraph p19 = new Paragraph("Religión: "+tblPaciente.tblReligion.nombreReligion);
                    doc.Add(p19);
                    Paragraph p20 = new Paragraph("Nombre contacto de emergencia: " +tblPaciente.tblContactoEmergencia.nombreContactoEmergencia);
                    doc.Add(p20);
                    Paragraph p21 = new Paragraph("Teléfono contacto de emergencia: "+ tblPaciente.tblContactoEmergencia.telefonoContactoEmergencia);
                    doc.Add(p21);
                    tblHistoriaClinica tblHistoriaClinica = bd.tblHistoriaClinica.Where(p => p.idPaciente == idPaciente).First();
                    doc.Add(espacio);
                    doc.Add(espacio);
                    Paragraph p23 = new Paragraph("- - - HISTORIA CLÍNICA DEL PACIENTE - - -");
                    p23.Alignment = Element.ALIGN_CENTER;
                    doc.Add(p23);
                    doc.Add(espacio);
                    Paragraph p24 = new Paragraph("Id historia clínica: "+tblHistoriaClinica.idHistoriaClinica);
                    doc.Add(p24);
                    Paragraph p25 = new Paragraph("Seguro médico: " + tblHistoriaClinica.tblSeguroMedico.nombreSeguro);
                    doc.Add(p25);
                    Paragraph p26 = new Paragraph("Tipo de sangre: " + tblHistoriaClinica.tblTipoSangre.sangre);
                    doc.Add(p26);
                    Paragraph p27 = new Paragraph("Antecedente familiar clínico: " + tblHistoriaClinica.antecedenteFamiliarClinico);
                    doc.Add(p27);
                    Paragraph p28 = new Paragraph("Antecedente familiar quirúrgico: " + tblHistoriaClinica.antecedenteFamiliarQuirurgico);
                    doc.Add(p28);
                    Paragraph p29 = new Paragraph("Antecedente personal clínico: " + tblHistoriaClinica.antecedentePersonalClinico);
                    doc.Add(p29);
                    Paragraph p30 = new Paragraph("Antecedente personal quirúrgico: " + tblHistoriaClinica.antecedentePersonalQuirurgico);
                    doc.Add(p30);
                    Paragraph p31 = new Paragraph("Antecedente personal alérgico: " + tblHistoriaClinica.antecedentePersonalAlergico);
                    doc.Add(p31);
                    Paragraph p32 = new Paragraph("Antecedente personal vacunas: " + tblHistoriaClinica.antecedentePersonalVacunas);
                    doc.Add(p32);
                    if(tblHistoriaClinica.idAntecedenteGinecoObstetrico != null)
                    {                        
                        Paragraph p33 = new Paragraph("Menarquia: " + tblHistoriaClinica.tblAntecedenteGinecoObstetrico.menarquia);
                        doc.Add(p33);
                        Paragraph p36 = new Paragraph("Gestas: " + tblHistoriaClinica.tblAntecedenteGinecoObstetrico.gestas);
                        doc.Add(p36);
                        Paragraph p37 = new Paragraph("Partos: " + tblHistoriaClinica.tblAntecedenteGinecoObstetrico.partos);
                        doc.Add(p37);
                        Paragraph p38 = new Paragraph("Cesárea: " + tblHistoriaClinica.tblAntecedenteGinecoObstetrico.cesarea);
                        doc.Add(p38);
                        Paragraph p39 = new Paragraph("Abortos: " +tblHistoriaClinica.tblAntecedenteGinecoObstetrico.abortos);
                        doc.Add(p39);
                        Paragraph p40 = new Paragraph("Hijos vivos: " + tblHistoriaClinica.tblAntecedenteGinecoObstetrico.hijosVivos);
                        doc.Add(p40);
                        Paragraph p41 = new Paragraph("Hijos muertos: " + tblHistoriaClinica.tblAntecedenteGinecoObstetrico.hijosMuertos);
                        doc.Add(p41);
                    }
                    if (tblHistoriaClinica.idAntecedenteReprodMasculino != null)
                    {
                        Paragraph p44 = new Paragraph("Enfermedades de transmisión sexual: " + tblHistoriaClinica.tblAntecedenteReprodMasculino.ets);
                        doc.Add(p44);
                        Paragraph p45 = new Paragraph("Pareja sexual: " + tblHistoriaClinica.tblAntecedenteReprodMasculino.parejaSexual);
                        doc.Add(p45);
                    }                   
                    
                    Paragraph p46 = new Paragraph("Uso de tabaco: "+tblHistoriaClinica.tabaco);
                    doc.Add(p46);
                    Paragraph p47 = new Paragraph("Uso de alcohol: " + tblHistoriaClinica.alcohol);
                    doc.Add(p47);
                    Paragraph p48 = new Paragraph("Uso de otras drogas: " + tblHistoriaClinica.otrasDrogas);
                    doc.Add(p48);
                    Paragraph p49 = new Paragraph("Actividad física: " + tblHistoriaClinica.actividadFisica);
                    doc.Add(p49);
                    Paragraph p50 = new Paragraph("Medicación habitual: " + tblHistoriaClinica.medicacionHabitual);
                    doc.Add(p50);

                    doc.Add(espacio);
                    doc.Add(espacio);
                    Paragraph p51 = new Paragraph("- - - HISTORIAL DE VISITAS / EVOLUCIÓN - - -");
                    p51.Alignment = Element.ALIGN_CENTER;
                    doc.Add(p51);
                    doc.Add(espacio);
                    PdfPTable table = new PdfPTable(3);//tabla de 3 col
                    float[] values = new float[3] { 70, 150, 150 }; //anchos de col
                    table.SetWidths(values);//anchos asignados a la tabla
                                            //creando las Celdas 
                                            //creando celdas y poniendo color ademas dealinear el contenido al centro
                    PdfPCell celda1 = new PdfPCell(new Phrase("Fecha"));
                    celda1.BackgroundColor = new BaseColor(130, 130, 130);
                    celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(celda1);

                    PdfPCell celda2 = new PdfPCell(new Phrase("Motivo de consulta"));
                    celda2.BackgroundColor = new BaseColor(130, 130, 130);
                    celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(celda2);

                    PdfPCell celda3 = new PdfPCell(new Phrase("Diagnóstico"));
                    celda3.BackgroundColor = new BaseColor(130, 130, 130);
                    celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(celda3);

                    List<tblEvolucion> listaEvoluciones = bd.tblEvolucion.Where(p => p.idHistoriaClinica == tblHistoriaClinica.idHistoriaClinica && p.habilitado == 1).ToList();
                    foreach (var item in listaEvoluciones)
                    {
                        Paragraph fechaVisita = new Paragraph(item.fechaVisita.Value.ToString());
                        table.AddCell(fechaVisita);
                        Paragraph motivoConsulta = new Paragraph(item.motivoConsulta);
                        table.AddCell(motivoConsulta);
                        Paragraph diagnostico = new Paragraph(item.diagnostico);
                        table.AddCell(diagnostico);
                    }
                    doc.Add(table);
                }
                doc.Close();
                buffer = ms.ToArray();
            }
            return File(buffer, "application/pdf");
        }

        /// <summary>
        /// Creación de la prescripción recetada en la visita actual
        /// </summary>
        /// <param name="idEvolucion">id de Evolución actual</param>
        /// <param name="idPaciente">id del paciente en la visita</param>
        /// <returns>Archivo PDF con la prescripción de la visita</returns>
        public FileResult prescripcionPDF(int idEvolucion, int idPaciente)
        {
            Document doc = new Document();           
            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);//guardar el doc en memoria
                doc.Open();
                Image png = Image.GetInstance(new Uri(Server.MapPath("~/Resources/Galenus.png")));
                png.ScalePercent(24f);
                doc.Add(png);
                //IMAGEN
                Paragraph info1 = new Paragraph(nombreMedico + ",M.D.");
                Paragraph info2 = new Paragraph("Médico Dermatólogo");
                Paragraph info3 = new Paragraph("PARIS N43 - 212 Y RÍO COCA");
                Paragraph info4 = new Paragraph("TELÉFONO 2263720");
                Paragraph fechaHoy = new Paragraph("Fecha de emisión: " + DateTime.Now.ToString("yyyy-MM-dd"));
                info1.Alignment = Element.ALIGN_CENTER;
                info2.Alignment = Element.ALIGN_CENTER;
                info3.Alignment = Element.ALIGN_CENTER;
                info4.Alignment = Element.ALIGN_CENTER;
                fechaHoy.Alignment = Element.ALIGN_RIGHT;
                doc.Add(info1);
                doc.Add(info2);
                doc.Add(info3);
                doc.Add(info4);
                doc.Add(fechaHoy);
                
                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);

                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == idPaciente).First();
                    tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion && p.habilitado == 1).First();                                       
                    Paragraph nombresPaciente = new Paragraph("Nombres y apellidos del paciente:    "+tblPaciente.nombres+" "+tblPaciente.apellidos);
                    doc.Add(nombresPaciente);
                    Paragraph cedulaPaciente = new Paragraph("Cédula de identidad:  " + tblPaciente.cedula);
                    doc.Add(cedulaPaciente);
                    Paragraph edad = new Paragraph("Edad:   "+(DateTime.Today.Year - tblPaciente.fechaNacimiento.Value.Year).ToString() +" años");
                    doc.Add(edad);
                    doc.Add(espacio);
                    Paragraph fechaconsulta = new Paragraph("Fecha de atención: "+tblEvolucion.fechaVisita.ToString());
                    doc.Add(fechaconsulta);       
                    Paragraph motivoConsulta = new Paragraph("Motivo de consulta:   " + tblEvolucion.motivoConsulta);
                    doc.Add(motivoConsulta);
                    Paragraph diagnostico = new Paragraph("Diagnóstico:     " + tblEvolucion.diagnostico);
                    doc.Add(diagnostico);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    
                    PdfPTable table = new PdfPTable(2);//tabla de 2 col
                    float[] anchoColumna = new float[2] { 50, 50 }; //anchos de col
                    table.SetWidths(anchoColumna);//anchos asignados a la tabla

                    PdfPCell celda1 = new PdfPCell(new Phrase("Prescripción"));
                    celda1.BackgroundColor = new BaseColor(130, 130, 130);
                    celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(celda1);

                    PdfPCell celda2 = new PdfPCell(new Phrase("Recomendaciones"));
                    celda2.BackgroundColor = new BaseColor(130, 130, 130);
                    celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(celda2);

                    
                    Paragraph prescripcion = new Paragraph(tblEvolucion.prescripcion);
                    table.AddCell(prescripcion);
                    Paragraph recomendaciones = new Paragraph(tblEvolucion.recomendaciones);
                    table.AddCell(recomendaciones);

                    doc.Add(table);                 
                }
                doc.Close();
                buffer = ms.ToArray();
            }
            return File(buffer, "application/pdf");
        }
        /// <summary>
        /// Creación de un certificado de asistencia al consultorio
        /// </summary>
        /// <param name="idEvolucion">id de Evolución actual</param>
        /// <param name="idPaciente">id del paciente en la visita</param>
        /// <returns>Archivo PDF con el certificado de asistencia</returns>
        public FileResult asistenciaPDF(int idEvolucion, int idPaciente)
        {
            Document doc = new Document();
            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);//guardar el doc en memoria
                doc.Open();
                Image png = Image.GetInstance(new Uri(Server.MapPath("~/Resources/Galenus.png")));
                png.ScalePercent(24f);
                doc.Add(png);
                //IMAGEN
                Paragraph info1 = new Paragraph(nombreMedico + ",M.D.");
                Paragraph info2 = new Paragraph("Médico Dermatólogo");
                Paragraph info3 = new Paragraph("PARIS N43 - 212 Y RÍO COCA");
                Paragraph info4 = new Paragraph("TELÉFONO 2263720");
                Paragraph fechaHoy = new Paragraph("Fecha de emisión: " + DateTime.Now.ToString("yyyy-MM-dd"));
                info1.Alignment = Element.ALIGN_CENTER;
                info2.Alignment = Element.ALIGN_CENTER;
                info3.Alignment = Element.ALIGN_CENTER;
                info4.Alignment = Element.ALIGN_CENTER;
                fechaHoy.Alignment = Element.ALIGN_RIGHT;
                doc.Add(info1);
                doc.Add(info2);
                doc.Add(info3);
                doc.Add(info4);
                doc.Add(fechaHoy);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);
                Paragraph titulo = new Paragraph("CERTIFICADO DE ASISTENCIA");
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(espacio);

                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == idPaciente).First();
                    tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion && p.habilitado == 1).First();
                    Paragraph nombresPaciente = new Paragraph("Nombres y apellidos del paciente:    " + tblPaciente.nombres + " " + tblPaciente.apellidos);
                    doc.Add(nombresPaciente);
                    Paragraph cedulaPaciente = new Paragraph("Cédula de identidad:  " + tblPaciente.cedula);
                    doc.Add(cedulaPaciente);
                    Paragraph edad = new Paragraph("Edad:   " + (DateTime.Today.Year - tblPaciente.fechaNacimiento.Value.Year).ToString() + " años");
                    doc.Add(edad);
                    doc.Add(espacio);
                    Paragraph fechaconsulta = new Paragraph("Fecha de atención: " + tblEvolucion.fechaVisita.ToString());
                    doc.Add(fechaconsulta);       
                    Paragraph motivoConsulta = new Paragraph("Motivo de consulta:   " + tblEvolucion.motivoConsulta);
                    doc.Add(motivoConsulta);
                    Paragraph diagnostico = new Paragraph("Diagnóstico:     " + tblEvolucion.diagnostico);
                    doc.Add(diagnostico);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);

                    Paragraph certifico = new Paragraph("CERTIFICO QUE EL PACIENTE ACUDIÓ EN ESTA FECHA A CONSULTA MEDICA DERMATOLÓGICA ");
                    doc.Add(certifico);

                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);

                    Paragraph lineaFirma = new Paragraph("__________________________");
                    doc.Add(lineaFirma);
                    info1.Alignment = Element.ALIGN_LEFT;
                    info2.Alignment = Element.ALIGN_LEFT;
                    doc.Add(info1);
                    doc.Add(info2);
                    doc.Add(espacio);
                    Paragraph codigomsp = new Paragraph("MSP " + codigoMSP);
                    doc.Add(codigomsp);
                    Paragraph cedulaMD = new Paragraph("CI: " + cedulaMedico);
                    doc.Add(cedulaMD);

                }
                doc.Close();
                buffer = ms.ToArray();
            }
            return File(buffer, "application/pdf");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEvolucion">id de Evolución actual</param>
        /// <param name="idPaciente">id del paciente en la visita</param>
        /// <param name="fechaDesde">Fecha inicial de reposo</param>
        /// <param name="fechaHasta">Fecha final de reposo</param>
        /// <returns>Archivo PDF con el certificado de reposo</returns>
        public FileResult reposoPDF(int idEvolucion, int idPaciente, DateTime fechaDesde,DateTime fechaHasta)
        {
            Document doc = new Document();
            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);//guardar el doc en memoria
                doc.Open();
                Image png = Image.GetInstance(new Uri(Server.MapPath("~/Resources/Galenus.png")));
                png.ScalePercent(24f);
                doc.Add(png);
                //IMAGEN
                Paragraph info1 = new Paragraph(nombreMedico + ",M.D.");
                Paragraph info2 = new Paragraph("Médico Dermatólogo");
                Paragraph info3 = new Paragraph("PARIS N43 - 212 Y RÍO COCA");
                Paragraph info4 = new Paragraph("TELÉFONO 2263720");
                Paragraph fechaHoy = new Paragraph("Fecha de emisión: " + DateTime.Now.ToString("yyyy-MM-dd"));
                info1.Alignment = Element.ALIGN_CENTER;
                info2.Alignment = Element.ALIGN_CENTER;
                info3.Alignment = Element.ALIGN_CENTER;
                info4.Alignment = Element.ALIGN_CENTER;
                fechaHoy.Alignment = Element.ALIGN_RIGHT;
                doc.Add(info1);
                doc.Add(info2);
                doc.Add(info3);
                doc.Add(info4);
                doc.Add(fechaHoy);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);
                Paragraph titulo = new Paragraph("CERTIFICADO DE REPOSO");
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(espacio);

                using (var bd = new BDD_ConsultorioDermatologicoEntities())
                {
                    tblPaciente tblPaciente = bd.tblPaciente.Where(p => p.idPaciente == idPaciente).First();
                    tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion && p.habilitado == 1).First();
                    Paragraph nombresPaciente = new Paragraph("Nombres y apellidos del paciente:    " + tblPaciente.nombres + " " + tblPaciente.apellidos);
                    doc.Add(nombresPaciente);
                    Paragraph cedulaPaciente = new Paragraph("Cédula de identidad:  " + tblPaciente.cedula);
                    doc.Add(cedulaPaciente);
                    Paragraph edad = new Paragraph("Edad:   " + (DateTime.Today.Year - tblPaciente.fechaNacimiento.Value.Year).ToString() + " años");
                    doc.Add(edad);
                    doc.Add(espacio);
                    Paragraph fechaconsulta = new Paragraph("Fecha de atención: " + tblEvolucion.fechaVisita.ToString());
                    doc.Add(fechaconsulta);
                    Paragraph diagnostico = new Paragraph("Diagnóstico:     " + tblEvolucion.diagnostico);
                    doc.Add(diagnostico);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);

                    Paragraph r1 = new Paragraph("Reposo por: "+(fechaHasta-fechaDesde).TotalDays.ToString()+" día/días");                    
                    doc.Add(r1);
                    doc.Add(espacio);
                    Paragraph r2 = new Paragraph("Desde: " + fechaDesde.ToString("yyyy-MM-dd"));
                    Paragraph r3 = new Paragraph("Hasta: " + fechaHasta.ToString("yyyy-MM-dd"));
                    doc.Add(r2);
                    doc.Add(r3);
                    doc.Add(espacio);
                    Paragraph att = new Paragraph("atentamente;");
                    doc.Add(att);                    
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);

                    Paragraph lineaFirma = new Paragraph("__________________________");
                    doc.Add(lineaFirma);
                    info1.Alignment = Element.ALIGN_LEFT;
                    info2.Alignment = Element.ALIGN_LEFT;
                    doc.Add(info1);
                    doc.Add(info2);
                    doc.Add(espacio);
                    Paragraph codigomsp = new Paragraph("MSP "+codigoMSP);
                    doc.Add(codigomsp);
                    Paragraph cedulaMD = new Paragraph("CI: "+cedulaMedico);
                    doc.Add(cedulaMD);
                }
                doc.Close();
                buffer = ms.ToArray();
            }
            return File(buffer, "application/pdf");
        }

        /// <summary>
        /// Se reciben las fechas desde la vista y se comprueba que no existan inconsistencias
        /// </summary>
        /// <param name="fechaDesde">Fecha inicial de reposo</param>
        /// <param name="fechaHasta">Fecha final de reposo</param>
        /// <returns>mensaje de error si existiese alguno</returns>
        public string verifica(DateTime fechaDesde, DateTime fechaHasta)
        {
            string mensaje = "";
            if(fechaDesde == null || fechaHasta == null)
            {
                mensaje = "Llene todos los campos";               
            }        
            if(fechaDesde > fechaHasta)
            {
                mensaje = "Elección incorrecta de fechas";
            }
            if (fechaHasta <= DateTime.Today)
            {
                mensaje = "La fecha [Hasta] debe ser mayor a la fecha del día de hoy";
            }
            if (fechaDesde < DateTime.Today)
            {
                mensaje = "La fecha [Desde] debe ser mayor o igual a la fecha del día de hoy";
            }
            return mensaje;                        
        }
    }
}