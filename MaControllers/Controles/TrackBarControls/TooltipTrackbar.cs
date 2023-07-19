using MaControllers.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaControllers.Controles.TrackBarControls
{
    //Muestra el valor 
    internal class TooltipTrackbar
    {
        public Size Size { get; set; }
        public Color Color { get; set; }

        public Color ForeColor { get; set; }
        public Font Font { get; set; }

        public RectangleF RecTooltip { get; set; }

        private int espacio = 10;// Espacio adicional entre el tooltip y la barra


        public TooltipTrackbar() {

            // Configuración predeterminada del tooltip
            this.Size = new Size(50,20);
            this.Color = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlLightLight;
            this.Font = new Font("Berlin Sans FB", 11.25f, FontStyle.Regular | FontStyle.Italic);
        }

        // Calcula el tamaño del tooltip basado en el valor máximo
        public void CalculaSize(int maxValue)
        {
            Size tamanioLetras = TextRenderer.MeasureText(maxValue.ToString(), this.Font);
            this.Size = tamanioLetras;
        }
        //TODO: Pasar como parametro el tamaño del pulgar
        // Calcula la posición y el tamaño del tooltip en función de la barra, la orientación y los valores mínimo, máximo y actual
        private void CalulaSuperficie(Rectangle barra, Orientation orientation, int minValue, int maxValue, int value)
        {

            if (orientation == Orientation.Horizontal)
            {

                int x = MathHelper.Map(
                    minValue,
                    maxValue,
                    barra.Left - 1,
                    (barra.Right - Size.Width) + 1,
                    value
                );

                RecTooltip = new RectangleF(
                    x, // Posición X del tooltip
                    barra.Top - (Size.Height + espacio), // Posición Y del tooltip (encima de la barra, con espacio adicional)
                    Size.Width, // Ancho del tooltip
                    Size.Height // Alto del tooltip
                );
            }
            else
            {
                // Invierte alto y ancho para la orientación vertical

                int y = MathHelper.Map(
                    minValue,
                    maxValue,
                    (barra.Bottom - Size.Width) + 1, // Altura máxima menos el ancho del tooltip
                    barra.Top - 2,
                    value
                );

                RecTooltip = new RectangleF(
                    barra.Right + (espacio), // Posición X del tooltip (a la derecha de la barra, con espacio adicional)
                    y, // Posición Y del tooltip
                    Size.Height, // Alto del tooltip (invertido con ancho)
                    Size.Width // Ancho del tooltip (invertido con alto)
                );
            }
        }

        public void DibujaToolTip(Graphics g,Rectangle recBarra,Orientation orientacion,int minvalue,int maxValue,int value)
        {
            //Calcula el tamaño del tooltip
            CalculaSize(maxValue);
            CalulaSuperficie(recBarra,orientacion, minvalue, maxValue, value);


            using(StringFormat formato = new StringFormat())
            using(Brush brushText = new SolidBrush(this.ForeColor))
            using(Brush brus = new SolidBrush(this.Color))
            using(GraphicsPath pathRounderdTooltip = GraphicsRoundedHelper.ObtieneRutaRedondeada(Rectangle.Round(this.RecTooltip),10))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                // Centrado en vertical y horizontal
                formato.LineAlignment = StringAlignment.Center;
                formato.Alignment = StringAlignment.Center;

                // Rellena el fondo del tooltip con el color configurado
                g.FillRectangle(brus, this.RecTooltip);

                if(orientacion == Orientation.Vertical)
                {
                    formato.FormatFlags = StringFormatFlags.DirectionVertical;
                }

                g.DrawString(value.ToString(), this.Font, brushText, RecTooltip,formato);
            }

        }

    }
}
