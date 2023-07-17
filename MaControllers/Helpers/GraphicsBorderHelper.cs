using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaControllers.Helpers
{
    public static class GraphicsBorderHelper
    {
        
        /// <summary>
        /// Dibuja un border dentro de un rectangulo
        /// </summary>
        /// <param name="g">Grafico para dibujar el borde</param>
        /// <param name="superficie">Superficie en donde se dibujara el borde</param>
        /// <param name="color">Color del borde</param>
        /// <param name="bordeSize">Tamaño del borde</param>
        /// <param name="borderPercent">El tamaño de redondeo del borde en porcentaje 0 - 100</param>
        /// <param name="extra">Espacio extra del borde padding</param>
        /// <param name="rellenar">Indica si el borde se rellenara</param>
        public static void DibujaBorde(
           Graphics g,
           Rectangle superficie,
           Color color,
           int bordeSize,
           int borderPercent,
           int extra = 0,
           bool rellenar = false

           )
        {
            //Empezamos con el borde
            int paddingBorde = (bordeSize / 2) + extra;

            Rectangle recBorde = Rectangle.Inflate(superficie, -paddingBorde, -paddingBorde);
            
            using (GraphicsPath pathBorde = GraphicsRoundedHelper.ObtienPathYCaculaRadio(recBorde, borderPercent))
            using (Pen brus = new Pen(color, bordeSize))
            {
                g.DrawPath(brus, pathBorde);

                if( rellenar)
                {
                    g.FillPath(brus.Brush, pathBorde);
                }
            }
            


        }

    }
}
