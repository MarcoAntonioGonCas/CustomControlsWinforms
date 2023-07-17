using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaControllers.Controles
{

    public class Line
    {
        public Point Inicio { get; set; }
        public Point Fin { get; set;}

        public Line()
        {

        }
        public Line(Point inicio, Point fin)
        {
            Inicio = inicio;
            Fin = fin;
        }
    }
    public static class CrossGrapics
    {

        public static Line ObtieneLinea(Point cordenadas,Orientation orientacion,int tamanioLinea = 20,int size = 1)
        {
            if (tamanioLinea % 2 != 0) tamanioLinea += 1;

            int mitad = tamanioLinea / 2;

            


            mitad += size / 2;

            Point inicio;
            Point fin;
            if (orientacion == Orientation.Horizontal)
            {
                inicio = new Point(
                    (cordenadas.X - mitad),
                    cordenadas.Y 
                    );

                fin = new Point(  
                    (cordenadas.X + mitad ),
                    cordenadas.Y 
                    );

            }
            else
            {
                inicio = new Point( 
                    cordenadas.X,
                    (cordenadas.Y - mitad) 
                    );

                fin = new Point( 
                    cordenadas.X,
                    (cordenadas.Y + mitad)  
                    );
            }
            Line linea = new Line(inicio,fin);

            return linea;
        }


        public static void DibujaCruz(Point punto,Graphics g,Color color,int tamanio=10)
        {
            int lineSize = 0;
            using (Pen pen = new Pen(color, 2))
            {
                Line lineaVertical = ObtieneLinea(
                    punto,
                    Orientation.Vertical,
                    tamanio,
                    size: lineSize);

                Line lineaHorizontal = ObtieneLinea(
                    punto, 
                    Orientation.Horizontal,
                    tamanio, 
                    size: lineSize);
                

                g.DrawLine(pen,lineaHorizontal.Inicio, lineaHorizontal.Fin);
                g.DrawLine(pen,lineaVertical.Inicio, lineaVertical.Fin);

            }
        }

    }
}
