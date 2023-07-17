using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaControllers.Helpers
{
    public static class ContentAligmenHelper
    {
        public static float ObtieneYCentrada(float tamanio, RectangleF rec)
        {
            return rec.Location.Y + ((rec.Height - tamanio) / 2);
        }

        public static float ObtieneXCentrada(float tamanio, RectangleF rec)
        {
            return rec.Location.X + ((rec.Width - tamanio) / 2);
        }
        public static int ObtieneYCentrada(int tamanio, Rectangle rec)
        {
            return rec.Location.Y + ((rec.Height - tamanio) / 2);
        }

        public static int ObtieneXCentrada(int tamanio, Rectangle rec)
        {
            return rec.Location.X + ((rec.Width - tamanio) / 2);
        }
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
