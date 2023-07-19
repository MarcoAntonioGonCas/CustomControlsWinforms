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

        /// <summary>
        ///  Obtiene una ruta grafica con un radio en porcentaje en base a un rectangulo.
        /// </summary>
        /// <example>
        /// Ejemplo valido
        /// <code>
        /// int ancho = 10;
        /// int alto = 20;
        /// Rectangle contenedor = new Rectangle(0,0,ancho,alto);
        /// 
        /// //Obtiene un radio totalmente redondeado
        /// ObtieneRutaConRadio(contenedor,100)
        /// 
        /// // Obtiene un radio casi redondeado
        /// ObtieneRutaConRadio(contenedor,50)
        /// </code>
        /// </example>
        /// <remarks>
        /// Se recomienda pasar un valor de 0-100 
        /// en donde 100% representa totamente redondeado
        /// </remarks>
        /// <param name="rec">El rectángulo en el cual se generará la ruta gráfica.</param>
        /// <param name="tamanioRadio">El tamaño del radio.</param>
        /// <returns>La ruta gráfica redondeada.</returns>

        public static GraphicsPath ObtieneRutaRedondeada(Rectangle rec,int pordentaje)
        {
            int min = Math.Min(rec.Width, rec.Height);

            int tamanioPx = min * pordentaje / 100;


            return ObtieneRutaConRadio(rec, tamanioPx);
        }

        /// <summary>
        ///  Obtiene una ruta grafica con un radio un pixeles en base a un rectangulo.
        /// </summary>
        /// <example>
        /// Ejemplo valido
        /// <code>
        /// int ancho = 10;
        /// int alto = 20;
        /// Rectangle contenedor = new Rectangle(0,0,ancho,alto);
        /// 
        /// //Obtiene un radio totalmente redondeado
        /// ObtieneRutaConRadio(contenedor,10)
        /// 
        /// //Si se pasa un valor mayor al valor minimo entre (ancho y alto)
        /// //El borde ya no se dibuja correctamente
        /// ObtieneRutaConRadio(contenedor,11)
        /// </code>
        /// </example>
        /// <remarks>
        /// Se recomienda pasar un valor no maximo al valor minimo (ancho o alto del rectangulo).
        /// </remarks>
        /// <param name="rec">El rectángulo en el cual se generará la ruta gráfica.</param>
        /// <param name="tamanioRadio">El tamaño del radio.</param>
        /// <returns>La ruta gráfica redondeada.</returns>

        public static GraphicsPath ObtieneRutaConRadio(Rectangle rec, int tamanioRadio)
        {
            GraphicsPath path = new GraphicsPath();
            if (tamanioRadio <= 1) tamanioRadio = 1;


            path.StartFigure();

            //Arriba izquierda
            path.AddArc(rec.X, rec.Y, tamanioRadio, tamanioRadio, 180, 90);
            //Arriba derecha
            path.AddArc(rec.Right - tamanioRadio, rec.Y, tamanioRadio, tamanioRadio, 270, 90);

            //Abajo derecha
            path.AddArc(rec.Right - tamanioRadio, rec.Bottom - tamanioRadio, tamanioRadio, tamanioRadio, 0, 90);

            //Abajo izquierda
            path.AddArc(rec.X, rec.Bottom - tamanioRadio, tamanioRadio, tamanioRadio, 90, 90);

            path.CloseFigure();



            return path;

            
        }


        /// <summary>
        /// Calcula el radio en pixeles en base a un tamaño y un porcenteje.
        /// </summary>
        /// <remarks>
        /// El porcentaje se calcula en base 
        /// al valor menor entre el ancho y el alto del tamaño del elemento.
        /// </remarks>
        /// <param name="tamanio">El tamaño del elemento.</param>
        /// <param name="porcentaje">El porcentaje del tamaño a utilizar para el radio.</param>
        /// <returns>El tamaño del radio en píxeles.</returns>
        /// <example>
        /// <code>
        /// Size elementoSize = new Size(200, 50);
        /// int porcentajeRadio = 25;
        /// int radio = CalculaRadio(elementoSize, porcentajeRadio);
        /// Console.WriteLine($"El radio calculado es: {radio}"); // 25
        /// 
        /// /// Size elementoSize2 = new Size(50, 200);
        /// int porcentajeRadio2 = 25;
        /// int radio2 = CalculaRadio(elementoSize, porcentajeRadio);
        /// Console.WriteLine($"El radio calculado es: {radio2}"); // 25
        /// </code>
        /// </example>
        public static int CalculaRadio(Size tamanio, int porcentaje)
        {

            // Calcula el valor minimo entre ancho y alto
            int min = Math.Min(tamanio.Width, tamanio.Height);


            // Calcula el tamaño en píxeles utilizando el porcentaje especificado
            int tamanioPx = min * porcentaje / 100;

            return tamanioPx;
        }
    }
}
