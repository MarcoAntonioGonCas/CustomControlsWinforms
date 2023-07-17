using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Diagnostics;

namespace MaControllers.Controles
{
    //TODO: no terminado corregir categorias,nombres etc
    public class ButtonCustom:Button
    {
        //Borde
        private int _borderSizePx;
        private int _borderRadiusPercent;
        private Color _borderColor;
        private Color _borderColor2;
        private DashStyle _borderLineStyle;
        private DashCap _borderDashStyle;
        private bool _gradientBorder;
        private int _gradientBorderAngle;


        //Background
        private Color _backgroundColor;
        private Color _backgroundColor2;
        private bool _gradientBackground;

        public int BorderRadiusPercent
        {
            get
            {
                return _borderRadiusPercent;
            }

            set
            {
                if(value>=0 && value<=100) {
                    _borderRadiusPercent = value;
                }
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }

            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        public Color BorderColor2
        {
            get
            {
                return _borderColor2;
            }

            set
            {
                _borderColor2 = value;
                this.Invalidate();
            }
        }

        public DashStyle BorderLineStyle
        {
            get
            {
                return _borderLineStyle;
            }

            set
            {
                _borderLineStyle = value;
                this.Invalidate();
            }
        }

        public DashCap BorderDashStyle
        {
            get
            {
                return _borderDashStyle;
            }

            set
            {
                _borderDashStyle = value;
                this.Invalidate();
            }
        }

        public bool GradientBorder
        {
            get
            {
                return _gradientBorder;
            }

            set
            {
                _gradientBorder = value;
                this.Invalidate();
            }
        }

        public int GradientBorderAngle
        {
            get
            {
                return _gradientBorderAngle;
            }

            set
            {
                _gradientBorderAngle = value;
                this.Invalidate();
            }
        }

        public int BorderSizePx
        {
            get
            {
                return _borderSizePx;
            }

            set
            {
                _borderSizePx = value;
                this.Invalidate();
            }
        }

        public ButtonCustom()
        {

            this._borderRadiusPercent = 20;
            this._borderSizePx = 8;
            this._gradientBorder = true;
            this._gradientBorderAngle = 45;

            this._borderColor = Color.Crimson;
            this._borderColor2 = Color.RoyalBlue;
            this._borderLineStyle = DashStyle.Solid;
            this._borderDashStyle = DashCap.Round;

            //Propiedades base
            this.Cursor = Cursors.Hand;
            this.FlatStyle = FlatStyle.Flat;

        }

        //Obtieene el path redondeado
        private GraphicsPath ObtieneRadioPath(Rectangle rec, int radio)
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
        private int CalculaRadio(Size tamanio, int percent)
        {
            int min = Math.Min(tamanio.Width, tamanio.Height);

            int tamanioPx = min * percent / 100;

            return tamanioPx;
        }

        private GraphicsPath ObtienePath(Rectangle superficie)
        {

            GraphicsPath path = new GraphicsPath();

            if (this.BorderRadiusPercent <= 1)
            {
                path.AddRectangle(superficie);
            }
            else
            {
                path.Dispose();
                int nuevoTamanioRadio = CalculaRadio(superficie.Size, BorderRadiusPercent);
                path = ObtieneRadioPath(superficie, nuevoTamanioRadio);
            }


            return path;
        }



        private void DibujaSuavizado(Graphics g,Rectangle rec,int tamanioPincelSuavizado)
        {
            Color color = this.Parent.BackColor; 
            using (GraphicsPath pathSuavizado = ObtienePath(rec))
            using (Pen pincelSuavizado = new Pen(color,tamanioPincelSuavizado))
            {
                this.Region = new Region(pathSuavizado);
                g.DrawPath(pincelSuavizado, pathSuavizado);
            }
        }


        private Brush ObtieneLapizBorde(GraphicsPath superficieBorde)
        {
            if (this.GradientBorder)
            {
                return new LinearGradientBrush(superficieBorde.GetBounds(), this.BorderColor, this.BorderColor2, this.GradientBorderAngle);
            }
            else
            {
                return new SolidBrush(this.BorderColor);
            }
        }
        private void DibujaBorde(Graphics g, Rectangle rec, int tamanioPincel)
        {
            if(tamanioPincel <= 0)
            {
                return;
            }
            using (GraphicsPath pathBorde = ObtienePath(rec))
            using(Brush brus = ObtieneLapizBorde(pathBorde))
            using(Pen pincelborde = new Pen(brus,tamanioPincel))
            {
                pincelborde.DashStyle = this.BorderLineStyle;
                pincelborde.DashCap = this.BorderDashStyle;
                g.DrawPath(pincelborde, pathBorde);
            }
        }




        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);


            Graphics g = pevent.Graphics;


            g.Clear(Color.Red);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Define el espacio para suavizar el borde
            int espacioSuavizado = 2;

            // Define el espacio del borde
            // Se divide entre dos porque el pincel empieza desde la mitad de la línea y se suma 2
            // para dejar un espacio adicional
            int espacioBorde = (this.BorderSizePx / 2) + 2;

            // Define el tamaño del pincel para suavizar el borde
            // Se multiplica por dos porque empieza desde el centro, por lo que el tamaño
            // solo representa la mitad. Se suma 3 para dar un poco de espacio adicional.
            int tamanioPincelSuavizado = (this.BorderSizePx * 2) + 3;

            // Calcula el rectángulo para suavizar el borde
            Rectangle recSuavizado = Rectangle.Inflate(this.ClientRectangle, -espacioSuavizado, -espacioSuavizado);

            // Calcula el rectángulo para el borde
            Rectangle recBorde = Rectangle.Inflate(recSuavizado, -espacioBorde, -espacioBorde);

            // Dibuja el suavizado del borde
            DibujaSuavizado(g, recSuavizado, tamanioPincelSuavizado);

            // Dibuja el borde
            DibujaBorde(g, recBorde, this.BorderSizePx);
        }

    }
}
