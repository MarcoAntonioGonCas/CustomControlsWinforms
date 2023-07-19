using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaControllers.Helpers
{

    //Clase de ayuda para la alineacion de controles
    public static class ContentAligmenHelper
    {
        //Obtiene la cordenada Y centrada de un elemento con un tamaño dentro de un rectangulo
        public static float ObtieneYCentrada(float tamanio, RectangleF rec)
        {
            return rec.Location.Y + ((rec.Height - tamanio) / 2);
        }
        public static int ObtieneYCentrada(int tamanio, Rectangle rec)
        {
            return rec.Location.Y + ((rec.Height - tamanio) / 2);
        }

        //Obtiene la cordenada X centrada de un elemento con un tamaño dentro de un rectangulo
        public static float ObtieneXCentrada(float tamanio, RectangleF rec)
        {
            return rec.Location.X + ((rec.Width - tamanio) / 2);
        }

        public static int ObtieneXCentrada(int tamanio, Rectangle rec)
        {
            return rec.Location.X + ((rec.Width - tamanio) / 2);
        }


        // Convierte la alineación horizontal de un texto en ContentAlignment a StringAlignment.
        public static StringAlignment ConvertToHorizontalAlignment(ContentAlignment alineacionTexto)
        {
            switch (alineacionTexto)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    return StringAlignment.Near;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return StringAlignment.Center;


                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;

                default:
                    return StringAlignment.Center;

            }
        }


        // Convierte la alineación vertical de un texto en ContentAlignment a StringAlignment.
        public static StringAlignment ConvertToVerticalAlignment(ContentAlignment alineacionTexto)
        {
            switch (alineacionTexto)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    return StringAlignment.Near;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    return StringAlignment.Center;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;

                default:
                    return StringAlignment.Center;
            }
        }
    }
}
