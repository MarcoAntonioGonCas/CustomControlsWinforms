using MaControllers.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaControllers.Controles
{
    [Flags]
    internal enum StateThumb
    {
        Hover = 0,
        Active = 2,
        ActiveHover = Hover | Active,
        None = 8,
    }

    internal class Thumb
    {
        public Rectangle RecThumb;
        public int Size { get; set; }
        public int BorderRaiusPercent { get; set; }
        public int BorderSize { get; set; }
        public int PaddingBorder { get; set; }
        public Color Color { get; set; }
        public Color ColorHover { get; set; }
        public Color ColorActive { get; set; }



        private void ObtieneCordenadasPulgar(Rectangle barra,Orientation orientation, int value, int minValue, int maxValue)
        {
            if(orientation == Orientation.Horizontal)
            {
                int x = MathHelper.ConvertTo(
                    minValue,
                    maxValue,
                    barra.Left,
                    barra.Right - Size,
                    value
                    );

                RecThumb = new Rectangle(
                    x,
                    ContentAligmenHelper.ObtieneYCentrada(Size, barra),
                    Size,
                    Size);
            }
            else{

                int y = MathHelper.ConvertTo(
                    minValue,
                    maxValue,
                    barra.Bottom,
                    barra.Top - Size,
                    value
                    );

                RecThumb = new Rectangle(
                    ContentAligmenHelper.ObtieneXCentrada(Size, barra),
                    y,
                    Size,
                    Size);


            }

            
        }

        private void DibujarPulgar(Graphics g,StateThumb state,Rectangle superficieBarra,Orientation orientation,int value,int minValue,int maxValue)
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
            using (GraphicsPath pathPulgar = GraphicsRoundedHelper.ObtienPathYCaculaRadio(this.RecThumb, this.BorderRaiusPercent))
            {

                g.FillPath(ColorPulgar, pathPulgar);

                GraphicsBorderHelper.DibujaBorde(
                        g,
                        RecThumb,
                        ColorActive,
                        BorderSize,
                        BorderRaiusPercent,
                        PaddingBorder,
                        state.HasFlag(StateThumb.Active)
                    );



            }


        }



    }
}
