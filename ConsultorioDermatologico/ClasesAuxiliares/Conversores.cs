using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace ConsultorioDermatologico.ClasesAuxiliares
{
    public static class Conversores
    {
        public static string NumeroALetras(this decimal fechaDecimal)
        {            

            var entero = Convert.ToInt64(Math.Truncate(fechaDecimal));        
            var res = NumeroALetras(Convert.ToDouble(entero)) ;
            return res;
        }
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private static string NumeroALetras(double valor)
        {
            string res; valor = Math.Truncate(valor);
            if (valor == 0) res = "CERO";
            else if (valor == 1) res = "UNO";
            else if (valor == 2) res = "DOS";
            else if (valor == 3) res = "TRES";
            else if (valor == 4) res = "CUATRO";
            else if (valor == 5) res = "CINCO";
            else if (valor == 6) res = "SEIS";
            else if (valor == 7) res = "SIETE";
            else if (valor == 8) res = "OCHO";
            else if (valor == 9) res = "NUEVE";
            else if (valor == 10) res = "DIEZ";
            else if (valor == 11) res = "ONCE";
            else if (valor == 12) res = "DOCE";
            else if (valor == 13) res = "TRECE";
            else if (valor == 14) res = "CATORCE";
            else if (valor == 15) res = "QUINCE";
            else if (valor < 20) res = "DIECI" + NumeroALetras(valor - 10);
            else if (valor == 20) res = "VEINTE";
            else if (valor < 30) res = "VEINTI" + NumeroALetras(valor - 20);
            else if (valor == 30) res = "TREINTA";
            else if (valor == 40) res = "CUARENTA";
            else if (valor == 50) res = "CINCUENTA";
            else if (valor == 60) res = "SESENTA";
            else if (valor == 70) res = "SETENTA";
            else if (valor == 80) res = "OCHENTA";
            else if (valor == 90) res = "NOVENTA";
            else if (valor < 100) res = NumeroALetras(Math.Truncate(valor / 10) * 10) + " Y " + NumeroALetras(valor % 10);
            else if (valor == 100) res = "CIEN";
            else if (valor < 200) res = "CIENTO " + NumeroALetras(valor - 100);
            else if ((valor == 200) || (valor == 300) || (valor == 400) || (valor == 600) || (valor == 800)) res = NumeroALetras(Math.Truncate(valor / 100)) + "CIENTOS";
            else if (valor == 500) res = "QUINIENTOS";
            else if (valor == 700) res = "SETECIENTOS";
            else if (valor == 900) res = "NOVECIENTOS";
            else if (valor < 1000) res = NumeroALetras(Math.Truncate(valor / 100) * 100) + " " + NumeroALetras(valor % 100);
            else if (valor == 1000) res = "MIL";
            else if (valor < 2000) res = "MIL " + NumeroALetras(valor % 1000);
            else if (valor < 1000000)
            {
                res = NumeroALetras(Math.Truncate(valor / 1000)) + " MIL";
                if ((valor % 1000) > 0)
                {
                    res = res + " " + NumeroALetras(valor % 1000);
                }
            }
            else
            {
                res = "Fecha no compatible";
            }            
            return res;
        }
    }
}