using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data.SqlTypes;

namespace MaControllers.Controles
{
    public class CircularPictureBox:PictureBox
    {

        //TODO: no terminado
        public CircularPictureBox()
        {
            this.BorderSize = 7;
            this.Background = Color.Crimson;
            this.Background2 = Color.RoyalBlue;
            this.BorderGradient = true;
            this.CapStyle = DashCap.Round;
            this.StyleBorde = DashStyle.Solid;
            this.AnguloBackground = 60;

            //picture box

            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Size = new Size(100, 100);
            //minimo

            this.MinimumSize = new Size(20, 20);

        }
       

        //Dibuja padding
        private void DibujaPadding(Graphics g,GraphicsPath pathPadding,float tamanioBorde)
        {
            //Le suma 1px al tamanio borde al igual que lo multiplica por dos 
            
            using (Pen pincelSuave = new Pen(this.Parent != null ? this.Parent.BackColor : SystemColors.Control, tamanioBorde))
            {
                pincelSuave.Alignment = PenAlignment.Center;
                //Limpiar
                g.DrawPath(pincelSuave, pathPadding);
            }
        }


        //Dibuja borde
        private Brush ObtieneLapizBorde(GraphicsPath superficieBorde)
        {
            if (this.BorderGradient)
            {
                return new LinearGradientBrush(superficieBorde.GetBounds(), Background, Background2,AnguloBackground);
            }
            else
            {
                return new SolidBrush(Background);
            }
        }
        private void LLenaPath(Graphics g, GraphicsPath superficieBorde)
        {
            using (Brush linearBack = ObtieneLapizBorde(superficieBorde))
            using (Pen pincelborde = new Pen(linearBack, BorderSize))
            {
                pincelborde.DashCap = this.CapStyle;
                pincelborde.DashStyle = this.StyleBorde;

                pincelborde.Alignment = PenAlignment.Inset;


                if (BorderSize > 0)
                {
                    g.FillPath(linearBack, superficieBorde);
                }
            }
        }
        private void DibujaPath(Graphics g,GraphicsPath superficieBorde,int borderSize)
        {
            if (borderSize <= 0) return;


            using (Brush linearBack = ObtieneLapizBorde(superficieBorde))
            using (Pen pincelborde = new Pen(linearBack, borderSize))
            {
                pincelborde.DashCap = this.CapStyle;
                pincelborde.DashStyle = this.StyleBorde;

                pincelborde.Alignment = PenAlignment.Center;


                
                g.DrawPath(pincelborde, superficieBorde);
                
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            base.Size = new Size(base.Width, base.Width);
        }

        
        //ObtieneRadio
        private GraphicsPath ObtieneRadioPathEvil(Rectangle rec,int tamanio)
        {
            Rectangle recTopLeft = new Rectangle(rec.X, rec.Y, tamanio, tamanio);
            Rectangle recTopRigth = new Rectangle(rec.X, rec.Y - tamanio , tamanio, tamanio);
            Rectangle recBottomRigth = new Rectangle(rec.Bottom - tamanio, rec.Right - tamanio, tamanio, tamanio);
            Rectangle recBottomLeft = new Rectangle(rec.X, rec.Y - tamanio ,tamanio, tamanio);
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(recTopLeft, 180, 90);
            path.AddArc(recTopRigth, 270, 90);
            path.AddArc(recBottomRigth, 0, 90);
            path.AddArc(recBottomLeft, 90, 90);
            path.CloseFigure();

            return path;
        }
        private GraphicsPath ObtieneRadioPath(Rectangle rec,int radio)
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


        private GraphicsPath ObtienePath(Rectangle superficie) {

            GraphicsPath path = new GraphicsPath();

            if(this._borderRadius <= 1)
            {
                path.AddRectangle(superficie);
            }
            else
            {
                path.Dispose();
                int newBorderRadius = CalcRadius(superficie.Size, _borderRadiouPercent);
                path = ObtieneRadioPath(superficie, newBorderRadius);
            }


            return path;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //Dibuja suavizado
            int espacioSuavizado = 1;
            Rectangle recSuavizado = Rectangle.Inflate(this.ClientRectangle, -espacioSuavizado, -espacioSuavizado);

            using (GraphicsPath pathSuavizado = ObtienePath(recSuavizado))
            {
                //int tamanioSuavizado = (BorderSize * 3) ;
                int tamanioSuavizado = (BorderSize * 2) + 3 ;
                

                //Dibuja padding
                DibujaPadding(g, pathSuavizado, tamanioSuavizado);
                this.Region = new Region(pathSuavizado);


                //Dibuja borde
                //int espacioBorde = this._borderSize
                int espacioBorde = (this._borderSize/2) + 2;
                using (GraphicsPath pathBorde = ObtienePath(Rectangle.Inflate(recSuavizado, -espacioBorde, -espacioBorde)))
                {
                    DibujaPath(g, pathBorde, BorderSize);

                }

            }

        }
        //Propiedades

        private int _borderRadius;
        private int _borderRadiouPercent;
        private int _borderSize;
        private DashStyle _styleBorde;
        private Color _background;
        private Color _background2;
        private DashCap _capStyle;
        private float _anguloBackground;
        private bool _borderGradient;

        [Category("Otros")]
        [Browsable(true)]
        [DefaultValue(2)]
        public int BorderSize
        {
            get
            {
                return _borderSize;
            }

            set
            {
                _borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Otros")]
        [Browsable(true)]
        public DashStyle StyleBorde
        {
            get
            {
                return _styleBorde;
            }

            set
            {
                _styleBorde = value;
                this.Invalidate();
            }
        }

        [Category("Otros")]
        [Browsable(true)]
        public Color Background
        {
            get
            {
                return _background;
            }

            set
            {
                _background = value;
                this.Invalidate();
            }
        }

        [Category("Otros")]
        [Browsable(true)]
        public Color Background2
        {
            get
            {
                return _background2;
            }

            set
            {
                _background2 = value;
                this.Invalidate();
            }
        }

        [Category("Otros")]
        [Browsable(true)]
        public DashCap CapStyle
        {
            get
            {
                return _capStyle;
            }

            set
            {
                _capStyle = value;
                this.Invalidate();
            }
        }

        [Category("Otros")]
        [Browsable(true)]
        [DefaultValue(60)]

        public float AnguloBackground
        {
            get
            {
                return _anguloBackground;
            }

            set
            {
                _anguloBackground = value;
                this.Invalidate();
            }
        }

        [Category("Otros")]
        [Browsable(true)]
        [DefaultValue(true)]
        public bool BorderGradient
        {
            get
            {
                return _borderGradient;
            }

            set
            {
                _borderGradient = value;
                this.Invalidate();
            }
        }


        private int CalcRadius(Size tamanio, int percent)
        {
            int max = Math.Max(tamanio.Width, tamanio.Height);

            int tamanioPx = max * percent / 100;

            return tamanioPx;
        }


        [Category("Otros")]
        [Browsable(true)]
        public int BorderRadiouPercent
        {
            get
            {
                return _borderRadiouPercent;
            }

            set
            {
                if (value > 0 && value <= 100)
                {
                    _borderRadius = CalcRadius(Size,value);
                    _borderRadiouPercent = value;
                }
                this.Invalidate();
            }
        }
    }
}
