using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ConsultorioDermatologico.Models;
using System.IO;

namespace ConsultorioDermatologico.Controllers
{
    public class DocumentosController : Controller
    {
        // GET: Documentos
        public ActionResult Index()
        {
            return View();
        }

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
                Paragraph info1 = new Paragraph("DRA. Silvana Narváez Arboleda"+",M.D.");
                Paragraph info2 = new Paragraph("Médico Dermatólogo");
                Paragraph info3 = new Paragraph("PARIS N43 - 212 Y RÍO COCA");
                Paragraph info4 = new Paragraph("TELÉFONO 2263720");
                Paragraph fechaHoy = new Paragraph("Quito," + DateTime.Now.ToString("yyyy-MM-dd"));
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
                    tblEvolucion tblEvolucion = bd.tblEvolucion.Where(p => p.idEvolucion == idEvolucion).First();

                    Paragraph nombresPaciente = new Paragraph("Nombres y apellidos del paciente:    "+tblPaciente.nombres+" "+tblPaciente.apellidos);
                    doc.Add(nombresPaciente);
                    Paragraph cedulaPaciente = new Paragraph("Cédula de identidad:  " + tblPaciente.cedula);
                    doc.Add(cedulaPaciente);
                    Paragraph edad = new Paragraph("Edad:   "+(DateTime.Today.Year - tblPaciente.fechaNacimiento.Value.Year).ToString() +" años");
                    doc.Add(edad);
                    doc.Add(espacio);
                    Paragraph fechaconsulta = new Paragraph("Fecha de atención: "+tblEvolucion.fechaVisita.ToString());
                    doc.Add(fechaconsulta);
                    doc.Add(espacio);
                    Paragraph motivoConsulta = new Paragraph("Motivo de consulta:   " + tblEvolucion.motivoConsulta);
                    doc.Add(motivoConsulta);
                    doc.Add(espacio);
                    Paragraph diagnostico = new Paragraph("Diagnóstico:     " + tblEvolucion.diagnostico);
                    doc.Add(diagnostico);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    doc.Add(espacio);
                    PdfPTable table = new PdfPTable(2);//tabla de 3 col
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
                    doc.Close();
                    buffer = ms.ToArray();
                }

            }
            return File(buffer, "application/pdf");
        }
    }
}