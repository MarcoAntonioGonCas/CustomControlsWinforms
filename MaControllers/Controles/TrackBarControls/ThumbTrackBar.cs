using MaControllers.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MaControllers.Controles.TrackBarControls
{
    [Flags]
    // Estados del pulgar | Thumb 
    internal enum StateThumb 
    {
        Hover = 1,
        Active = 2,
        ActiveHover = Hover | Active,
        None = 8,
    }

    internal class ThumbTrackBar
    {
        public Rectangle RecThumb; // Rectangulo posicion del pulgar y tamaño
        public int Size { get; set; } //Tamaño del pulgar
        public int BorderRadiusPercent { get; set; } // Porcentaje de redondeo de los bordes del pulgar
        public int BorderSize { get; set; } // Tamaño del borde del pulgar
        public int PaddingBorder { get; set; } // Espacio entre el borde del pulgar y su contenido
        public Color Color { get; set; } // Color de fondo del pulgar
        public Color ColorHover { get; set; } // Color de fondo del pulgar cuando se encuentra en estado de "hover" (puntero del mouse encima)
        public Color ColorActive { get; set; }// Color de fondo del pulgar cuando se encuentra en estado "activo" (se está arrastrando o haciendo clic)

        public ThumbTrackBar()
        {
            Size = 20;
            BorderRadiusPercent = 100;
            BorderSize = 2;
            PaddingBorder = 3;
            Color = Color.FromArgb(ColorUtils.HtmlToArgb("#45484A"));
            ColorActive = Color.FromArgb(ColorUtils.HtmlToArgb("#4CC2FF")); ;
            ColorHover = Color.FromArgb(ColorUtils.HtmlToArgb("#606366"));
        }
        public ThumbTrackBar(int size, int borderRaiusPercent, int borderSize, int paddingBorder, Color color, Color colorHover, Color colorActive)
        {
            Size = size;
            BorderRadiusPercent = borderRaiusPercent;
            BorderSize = borderSize;
            PaddingBorder = paddingBorder;
            Color = color;
            ColorHover = colorHover;
            ColorActive = colorActive;
        }

        private void ObtieneCordenadasPulgar(Rectangle barra,Orientation orientation, int value, int minValue, int maxValue)
        {
            if(orientation == Orientation.Horizontal)
            {
                int x = MathHelper.Map(
                    minValue,
                    maxValue,
                    barra.Left - 1,
                    (barra.Right  - Size) + 1,
                    value
                    );

                RecThumb = new Rectangle(
                    x,
                    ContentAligmenHelper.ObtieneYCentrada(Size, barra),
                    Size,
                    Size);
            }
            else{

                int y = MathHelper.Map(
                    minValue,
                    maxValue,
                    (barra.Bottom - Size) + 1,
                    barra.Top - 2,
                    value
                    );

                RecThumb = new Rectangle(
                    ContentAligmenHelper.ObtieneXCentrada(Size, barra),
                    y,
                    Size,
                    Size);


            }

            
        }

        public void DibujarPulgar(Graphics g,StateThumb state,Rectangle superficieBarra,Orientation orientation,int value,int minValue,int maxValue)
        {
            ObtieneCordenadasPulgar(superficieBarra, orientation, value, minValue, maxValue);

            Color colorPulgar = this.Color;
            if(state.HasFlag(StateThumb.ActiveHover) || state.HasFlag(StateThumb.Active))
            {
                colorPulgar = this.Color;
            }else if(state.HasFlag(StateThumb.Hover))
            {
                colorPulgar = this.ColorHover;
            }

            using (Brush ColorPulgar = new SolidBrush(colorPulgar))
            using (GraphicsPath pathPulgar = GraphicsRoundedHelper.ObtieneRutaRedondeada(this.RecThumb, this.BorderRadiusPercent))
            {

                g.FillPath(ColorPulgar, pathPulgar);

                GraphicsBorderHelper.DibujaBorde(
                        g,
                        RecThumb,
                        ColorActive,
                        BorderSize,
                        BorderRadiusPercent,
                        PaddingBorder,
                        state.HasFlag(StateThumb.Active)
                    );



            }


        }


        //DibujaProgreso
        public void DibujarProgresoIndependiente(Graphics g, Rectangle superficieBarra, Orientation orientation, int value, int minValue, int maxValue, int borderRadiusPercentBar, Color color)
        {
            Rectangle recProgreso;

            if (orientation == Orientation.Horizontal)
            {
                int espacioExtraInicio = 0;
                int espacioExtraFin = 0;


                int width = MathHelper.Map(
                    minValue,
                    maxValue,
                    0,
                    superficieBarra.Width ,
                    value);

                recProgreso = new Rectangle(
                    superficieBarra.X - espacioExtraInicio,
                    superficieBarra.Y,
                    width + espacioExtraInicio + espacioExtraFin,
                    superficieBarra.Height
                    );
            }
            else
            {
                int espacioExtraArriba = 0;
                int espacioExtraAbajo = 0;

                int alto = MathHelper.Map(
                    minValue,
                    maxValue,
                    0,
                    superficieBarra.Height - (RecThumb.Width /2),
                    value);
                //int y = MathHelper.ConvertTo(
                //   minValue,
                //   maxValue,
                //   superficieBarra.Bottom,
                //   superficieBarra.Top,
                //   value);

                recProgreso = new Rectangle(
                    superficieBarra.X,
                    superficieBarra.Bottom - alto - espacioExtraArriba ,
                    //y,
                    superficieBarra.Width,
                    alto + espacioExtraAbajo
                    );
            }


            using (Brush ColorPulgar = new SolidBrush(color))
            using (GraphicsPath pathPulgar = GraphicsRoundedHelper.ObtieneRutaRedondeada(recProgreso, borderRadiusPercentBar))
            {

                g.FillPath(ColorPulgar, pathPulgar);


            }
        }


        public void DibujarProgresoDependiente(Graphics g, Rectangle superficieBarra, Orientation orientation, int value, int minValue, int maxValue, int borderRadiusPercentBar, Color color)
        {
            Rectangle recProgreso;

            if (orientation == Orientation.Horizontal)
            {
                int espacioExtraIni = 0;
                int espacioExtraFin = 0;

                int mitadAncho = RecThumb.Width / 2;
                int ancho = (RecThumb.Right - mitadAncho) - superficieBarra.Left;

                recProgreso = new Rectangle(
                    superficieBarra.X - espacioExtraIni,
                    superficieBarra.Y,
                    ancho + espacioExtraFin + espacioExtraIni,
                    superficieBarra.Height
                    );
            }
            else
            {
                int espacioExtraAbajo = 0;
                int espacioExtraArriba = 0;


                int mitadAlto = RecThumb.Height / 2; //Calculamos la mitad de nuetro pulgar

                //Empezamos desde la mitad de alto del pulgar mas y actual 
                int y = (RecThumb.Y + mitadAlto) - espacioExtraArriba;

                //Alto = bottom de barra - el top del pulgar 
                int alto = (superficieBarra.Bottom - y ) + (espacioExtraAbajo); // El alto sera 

                recProgreso = new Rectangle(
                    superficieBarra.X,
                    y, //Empezamos desde arriba del pulgar + la mitad del pulgar
                    superficieBarra.Width,
                    alto
                    );
            }


            using (Brush ColorPulgar = new SolidBrush(color))
            using (GraphicsPath pathPulgar = GraphicsRoundedHelper.ObtieneRutaRedondeada(recProgreso, borderRadiusPercentBar))
            {

                g.FillPath(ColorPulgar, pathPulgar);


            }
        }
    }
}
