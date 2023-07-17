using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaControllers.Helpers
{
    public static class GraphicsRoundedHelper
    {
        public static GraphicsPath ObtienPathYCaculaRadio(Rectangle rec,int percent)
        {
            int min = Math.Min(rec.Width, rec.Height);

            int tamanioPx = min * percent / 100;


            return ObtieneRadioPath(rec, tamanioPx);
        }
        public static GraphicsPath ObtieneRadioPath(Rectangle rec, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            if (radio <= 1) radio = 1;


            path.StartFigure();

            //Arriba izquierda
            path.AddArc(rec.X, rec.Y, radio, radio, 180, 90);
            //Arriba derecha
            path.AddArc(rec.Right - radio, rec.Y, radio, radio, 270, 90);

            //Abajo derecha
            path.AddArc(rec.Right - radio, rec.Bottom - radio, radio, radio, 0, 90);

            //Abajo izquierda
            path.AddArc(rec.X, rec.Bottom - radio, radio, radio, 90, 90);

            path.CloseFigure();



            return path;

        }

        public static int CalculaRadio(Size tamanio, int percent)
        {
            int min = Math.Min(tamanio.Width, tamanio.Height);

            int tamanioPx = min * percent / 100;

            return tamanioPx;
        }
    }
}
