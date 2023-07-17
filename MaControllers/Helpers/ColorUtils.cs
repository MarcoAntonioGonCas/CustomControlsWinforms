using Microsoft.Win32;
using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MaControllers.Helpers
{
    public static class ColorUtils
    {
        private static Regex isHexadecimalString = new Regex("^#?[0-9a-fA-F]+$");
        public static int HtmlToArgb(string hex)
        {
            if (!isHexadecimalString.IsMatch(hex))
            {
                throw new ArgumentException("No es un valor hexadecimal valido");
            }
            hex = hex.Replace("#", "");

            byte alpha = 255;
            byte r = (byte)HexToInt(hex.Substring(0, 2));
            byte g = (byte)HexToInt(hex.Substring(2, 2));
            byte b = (byte)HexToInt(hex.Substring(4, 2));



            int numColor =
                alpha << 24 |
                r << 16 |
                g << 8 |
                b;


            //(0xFF << 24) se utiliza para establecer el
            //componente alfa del color
            //ARGB en 255(completamente opaco)

            return HexToInt(hex) | (0xFF << 24);

        }
        public static string ArgbToHtml(int argb)
        {
            byte r = (byte)(argb >> 16 & 0xFF);
            byte g = (byte)(argb >> 8 & 0xFF);
            byte b = (byte)(argb & 0xFF);


            string hexHtml = "#";

            hexHtml += ByteToHex(r);
            hexHtml += ByteToHex(g);
            hexHtml += ByteToHex(b);


            return hexHtml;

            //or


            //return "#" + IntToHex(argb, 6).Substring(2);
        }

       //jex to int
        private static int HexIntPow(string hex)
        {
            int power = 0;

            int valueInt = 0;


            for(int i = hex.Length - 1; i >= 0; i--)
            {
                int hexValue = CharHexToInt(hex[i]);
                valueInt += (hexValue * (int)(Math.Pow(16, power)));
                power++;
            }
            return valueInt;
        }
        private static int HexIntBits(string hex)
        {
            int valueInt = 0;

            //Empezamos desde los ultimos digitos
            //Para ir desplazando el numero hacia la izquierda

            for (int i = hex.Length - 1; i >= 0; i--)
            {
                int hexValue = CharHexToInt(hex[i]); //Obtenemos el valor del signo hexadecimal 0 - 15
                valueInt =  (valueInt << 4) + hexValue; //Dezplazamos a la derecha 4 bits que son lo que ocupa un valor hexadecimal
            }
            return valueInt;
        }
        private static int HexToInt(string hex)
        {


            int valueInt = 0;

            //
            for (int i = 0; i < hex.Length; i++)
            {
                int hexValue = CharHexToInt(hex[i]);

                // multiplicamos por 16 para desplazarlo un dígito a la izquierda y
                // dejar espacio para agregar el próximo dígito hexadecimal.
                valueInt = (valueInt * 16) + hexValue;

            }
            return valueInt;
        }



        private static int CharHexToInt(char simbolo) {
            simbolo = char.ToUpper(simbolo);
            if (simbolo >= '0' && simbolo <= '9')
            {
                return simbolo - '0';
            }else if(simbolo >= 'A' && simbolo <= 'F') {
                return (simbolo - 'A') + 10;
            }   
            else
            {
                throw new ArgumentException("No es un simbolo hexadecimal");
            }
        
        
        }




        //byte to hex
        private static string ByteToHex(byte byteValue)
        {

            string hexValue = "";

            hexValue += GetValueHex((byteValue >> 4) & 0xF);
            hexValue += GetValueHex((byteValue & 0xF));


            return hexValue;

        }


        private static char GetValueHex(int num,bool mayus = true)
        {
            if(num < 0 || num > 15)
            {
                throw new ArgumentOutOfRangeException(nameof(num));
            }

            if(num < 10)
            {
                return (char)('0' + num);
            }
            else
            {
                char hexSimbol = (char)('A' + (num - 10));
                if(!mayus) hexSimbol = char.ToLower(hexSimbol);
                return hexSimbol;
            }
           
        }


        //int to hex


        //Este metodo devolvera valores hexadecimales hasta que el numero sea diferente a 0
        private static string IntToHex(int numInt,int padding = 0)
        {

            string strHex = string.Empty;


            //Calculamos los bytes que ocupa un entero
            //Lo multiplicamos por dos porque cada valor hexadecimal ocupa 4 bits
            //La mitad de un byte
            int size = sizeof(int) * 2;


            for (int i = 0; i < size && numInt != 0; i++)
            {
                //Obtenemos el valor de los primeros 4 bits
                int valueHex = numInt & 0xF;
                strHex = GetValueHex(valueHex) + strHex;


                //Recorremos 
                numInt >>= 4;


            }

            ////o
            //for (int i = 0; i < size && (numInt >> (4 * i)) != 0; i++)
            //{

            //    int valueHex = (numInt >> (4 * i)) & 0xF;
            //    strHex = GetValueHex(valueHex) + strHex;

            //}

            if (padding > 0)
            {

                strHex = strHex.PadLeft(padding, '0');
            }
            return strHex;
        }

        //En este metodo siempre devolvera el total de bytes de entero 8 bytes
        private static string IntToHexEvilStart(int numInt, int padding = 0)
        {

            string strHex = string.Empty;

            //NOTA 1 valor hexadecil ocupa 4 BITS
            //Calculamos los bytes que ocupa un entero
            //Lo multiplicamos por dos porque cada valor hexadecimal ocupa 4 bits
            //La mitad de un byte
            int size = sizeof(int) * 2;

            //Empezamos desde el tamaño menos -> 8 - 1 = 7
            for (int i = size - 1 ; i >= 0; i--)
            {
                //Desplazamos todos los bits con mayor importancia hacia la derecha
                //De 4 bits en 4 bits
                int valueHex = (numInt >> (4 * i) )  & 0xF;

                strHex += GetValueHex(valueHex);
            }


            if (padding > 0)
            {

                strHex = strHex.PadLeft(padding, '0');
            }
            return strHex;
        }
    }
}
