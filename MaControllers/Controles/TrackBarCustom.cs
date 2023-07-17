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
using MaControllers.Helpers;
using System.Data.SqlTypes;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

namespace MaControllers.Controles
{
    public class TrackBarCustom : Control
    {
        //Eventos
        public event EventHandler ScrolThumb;
        public event EventHandler ValueChange;


        protected void OnScrollThumb()
        {
            this.ScrolThumb?.Invoke(this, EventArgs.Empty);
        }
        protected void OnValueChange()
        {
            this.ValueChange?.Invoke(this, EventArgs.Empty);
        }

        //bands
        private bool arrastrando = false;
        private bool hoverPulgar = false;


        //Bar
        private int tamanioBarra = 5;
        private int borderRadiusPercentBar = 100;


        //thumb
        private const int borderSizeThumb = 2;
        private const int bordePaddingThumb = 1;

        private int tamanioPulgar = 20;
        private int borderRadiusPercentThumb = 100;





        //Helper
        private string message = string.Empty;


        //Propiedades rectangulos
        private Rectangle _recSuperficiePinturaBarra;
        private Rectangle _recContenedorPulgar;
        private Rectangle _recSuperficieBarra;

        private Rectangle _recValorInicial;
        private Rectangle _recValorFinal;



        //Propiedades publicas
        private Orientation orientation = Orientation.Horizontal;
        private int value = 0;
        private int minValue = 0;
        private int maxValue = 100;




        //Text value
        private bool showValues;
        



        //Colors
        private Color colorThumb;
        private Color colorThumbHover;
        private Color colorThumbActive;
        private Color trackColor;
        private Color trackColorActive;

        private void InicializarColores()
        {

            this.colorThumb = Color.FromArgb(ColorUtils.HtmlToArgb("#45484A"));
            this.colorThumbActive = Color.FromArgb(ColorUtils.HtmlToArgb("#4CC2FF")); ;
            this.colorThumbHover = Color.FromArgb(ColorUtils.HtmlToArgb("#606366"));

            this.trackColor = ColorTranslator.FromHtml("#9D9D9D");
            this.trackColorActive = this.colorThumbActive;

            this.ForeColor = SystemColors.ControlText;
        }

        private void InicializarFuentes()
        {
            this.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Regular);
        }


        [Category("Otros")]
        public Orientation Orientation
        {
            get
            {
                return orientation;
            }

            set
            {

                if (orientation == Orientation.Horizontal && value == Orientation.Vertical)
                {
                    int aux = Height;
                    Height = Width;
                    Width = aux;
                }
                else if (orientation == Orientation.Vertical && value == Orientation.Horizontal)
                {
                    int aux = Height;
                    Height = Width;
                    Width = aux;
                }

                orientation = value;

                this.Invalidate();

            }
        }
        [Category("Otros")]
        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                if (value >= minValue && value <= maxValue)
                {
                    this.value = value;
                    this.Invalidate();
                    OnValueChange();
                }
            }
        }
        [Category("Otros")]
        public int MinValue
        {
            get
            {
                return minValue;
            }

            set
            {

                if (value > maxValue)
                {
                    maxValue = minValue;

                }
                minValue = value;
                this.Invalidate();
            }
        }
        [Category("Otros")]
        public int MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                if (value < minValue)
                {
                    minValue = value;
                }
                maxValue = value;
                this.Invalidate();
            }
        }

        public bool ShowValues
        {
            get
            {
                return showValues;
            }

            set
            {

                showValues = value;


                this.Invalidate();
            }
        }



        public new Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                CalcularTamañoControl();
                this.Invalidate();

            }
        }

        private Size CalcularTamanioLetras()
        {
            if (this.Font == null ) return Size.Empty;

            Size tamanioValorMinimo = TextRenderer.MeasureText(this.MinValue.ToString(), this.Font);
            Size tamanioValorMaximo = TextRenderer.MeasureText(this.MinValue.ToString(), this.Font);

            // Comparar el tamaño del texto máximo y mínimo
            int maxTextWidth = Math.Max(tamanioValorMinimo.Width, tamanioValorMinimo.Width);
            int maxTextHeight = Math.Max(tamanioValorMaximo.Height, tamanioValorMaximo.Height);



            return new Size(maxTextWidth, maxTextHeight);
        }
        private void CalcularTamañoControl()
        {

            Size tamanioletras = CalcularTamanioLetras();


            if(Height < tamanioletras.Height)
            {
                Height = tamanioletras.Height +10;
            }
            
            if(Width  < tamanioletras.Width)
            {
                Width = tamanioletras.Width +10;
            }
        }

        public TrackBarCustom() : base()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            InicializarColores();
            InicializarFuentes();
            CalculaSuperficies();
        }

       
        private void CalculaSuperficies()
        {


            //Todo
            _recSuperficiePinturaBarra = CalculaRectanguloSuperficie();
            _recSuperficieBarra = CalculaBarra(_recSuperficiePinturaBarra);
            _recContenedorPulgar = CalculaRectanguloPulgar();

        }
        //Eventos
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


            if (e.Button == MouseButtons.Left && _recContenedorPulgar.Contains(e.Location))
            {

                this.arrastrando = true;
                message = $"{e.Location}";

                this.Invalidate();
            }else if(e.Button == MouseButtons.Left && _recSuperficiePinturaBarra.Contains(e.Location))
            {
                EstablecerValor(e.Location);
                this.arrastrando = true;
            }

        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.arrastrando = false;
            this.Invalidate();

            this.Cursor = Cursors.Default;

        }
        private void EstablecerValor(Point mouseLocation)
        {
            if (Orientation == Orientation.Horizontal)
            {
                int newValue = 0;
                if (mouseLocation.X >= _recSuperficieBarra.X &&
                    mouseLocation.X <= _recSuperficieBarra.Right)
                {
                    newValue = MathHelper.ConvertTo(_recSuperficieBarra.X, _recSuperficieBarra.Right, MinValue, MaxValue, mouseLocation.X);
                    

                }
                else if (mouseLocation.X < _recSuperficieBarra.X)
                {
                    newValue = MinValue;
                }
                else if (mouseLocation.X > _recSuperficieBarra.Right)
                {
                    newValue = MaxValue;
                }

                Value = newValue;
            }
            else
            {
                int newValue = 0;


                if (mouseLocation.Y >= _recSuperficieBarra.Y &&
                    mouseLocation.Y <= _recSuperficieBarra.Bottom)
                {
                    newValue = MathHelper.ConvertTo(_recSuperficieBarra.Bottom, _recSuperficieBarra.Y, MinValue, MaxValue, mouseLocation.Y);
                }
                else if (mouseLocation.Y < _recSuperficieBarra.Y)
                {
                    newValue = MaxValue;
                }
                else if (mouseLocation.Y > _recSuperficieBarra.Bottom)
                {
                    newValue = MinValue;
                }


                Value = newValue;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);


            if (e.Button == MouseButtons.Left && arrastrando)
            {
                //Cambiamos el cursos a una manita
                this.Cursor = Cursors.Hand;
                Point mouseLocation = e.Location;

                OnScrollThumb();

                EstablecerValor(mouseLocation);


            }
            HoverMousePulgar(e);
        }

        private void HoverMousePulgar(MouseEventArgs e)
        {
            if (_recContenedorPulgar.Contains(e.Location))
            {
                hoverPulgar = true;
            }
            else
            {
                hoverPulgar = false;
            }
            this.Invalidate();
        }


        private Rectangle CalcularTamanioLetras(Size tamanioLetras)
        {


            Rectangle rectangle = this.ClientRectangle;
            if (Orientation == Orientation.Horizontal)
            {
                rectangle = Rectangle.Inflate(this.ClientRectangle, -(tamanioLetras.Width * 2), 0);

            }
            else if (Orientation == Orientation.Vertical)
            {
                rectangle = Rectangle.Inflate(this.ClientRectangle, 0, -(tamanioLetras.Height + 10) );
            }

            return rectangle;

        }
        private Rectangle CalculaRectanguloSuperficie()
        {
            Rectangle rectangle = this.ClientRectangle;

            if (showValues)
            {
                Size tamanioLetras = CalcularTamanioLetras();
                rectangle = CalcularTamanioLetras(tamanioLetras);
            }
            else
            {
                rectangle = Rectangle.Inflate(this.ClientRectangle, -2, -2);
            }



            return rectangle;
        }

        private Rectangle CalculaRectanguloPulgar()
        {
            int x = MathHelper.ConvertTo(
                MinValue,
                MaxValue,
                _recSuperficieBarra.X - 1, //min x
                (_recSuperficieBarra.Right + 1) - tamanioPulgar, //max x
                Value
                );

            int y = MathHelper.ConvertTo(
                MinValue,
                MaxValue,
                (_recSuperficieBarra.Bottom + 1) - tamanioPulgar, //max y
                (_recSuperficieBarra.Y - 2), //min y
                Value
                );




            Rectangle recBola;


            if (Orientation == Orientation.Horizontal)
            {
                recBola = new Rectangle(
                    x, // margen 1 px
                    ObtieneYCentrada(tamanioPulgar, _recSuperficieBarra),
                    tamanioPulgar,
                    tamanioPulgar
                    );
            }
            else
            {
                recBola = new Rectangle(
                    ObtieneXCentrada(tamanioPulgar, _recSuperficieBarra),
                    y, //margen 1 px 
                    tamanioPulgar,
                    tamanioPulgar
                    );
            }

            return recBola;
        }

        private Rectangle CalculaBarra(Rectangle superficie)
        {
            Rectangle rec;


            //Si esta en horizontal
            if (Orientation == Orientation.Horizontal)
            {
                rec = new Rectangle(
                    superficie.X, //empezamos desde x 0
                    ObtieneYCentrada(tamanioBarra, superficie),// centro y
                    superficie.Width,//el ancho sera igual al ancho del control
                    tamanioBarra //alto sera igual al tamaño del pulgar
                );
            }
            //Si esta en vertical
            else
            {
                //Empezamos desde x centro
                rec = new Rectangle(
                    ObtieneXCentrada(tamanioBarra, superficie), // centro x
                    superficie.Y, //empezamos desde y 0
                    tamanioBarra, //el ancho sera el tamaño del pulgar
                    superficie.Height // el alto sera el alto del control
                );
            }
            //int espacioBorde = (borderSize / 2) + bordePadding;

            //rec = Rectangle.Inflate(rec, -espacioBorde, -espacioBorde);

            return rec;
        }

        private Rectangle CalculaProgresoBarra(Rectangle pulgar, Rectangle superficieBarra)
        {
            Rectangle recBarra;
            //Si esta en modo horizontal


            if (Orientation == Orientation.Horizontal)
            {
                int mitadPulgar = pulgar.Width / 2;
                int progresoWidth = (pulgar.Right - mitadPulgar) - superficieBarra.X;


                recBarra = new Rectangle(
                   superficieBarra.X,
                   superficieBarra.Y,
                   progresoWidth, // Calculamos el ancho de la barra de progreso 
                   superficieBarra.Height);



            }
            else
            {
                //Empezamos desde la mitad de alto del pulgar mas y actual 
                int arriba = (pulgar.Height / 2) + pulgar.Top;
                //el tamaño total de nuestra barra menos en donde se encuentra nuestro pulgar en y + top

                int progresoHeight = (superficieBarra.Bottom + 1) - arriba; //Restamos del alto total en donde emepieza el pulgar

                recBarra = new Rectangle(
                   superficieBarra.X,
                   arriba,//empezamos desde arriba de nuestro pulgar porque el alto es hacia abajo
                   superficieBarra.Width,
                   progresoHeight // Alto barra de progreso

                   );
            }


            return recBarra;
        }


        #region Helpers center

        private int ObtieneYCentrada(int tamanio, Rectangle rec)
        {
            return rec.Location.Y + ((rec.Height - tamanio) / 2);
        }

        private int ObtieneXCentrada(int tamanio, Rectangle rec)
        {
            return rec.Location.X + ((rec.Width - tamanio) / 2);
        }
        #endregion


        private void DibujaBarra(Graphics g, Rectangle superficie)
        {
            using (Brush pincelSuperficie = new SolidBrush(trackColor))
            using (GraphicsPath path = GraphicsRoundedHelper.ObtienPathYCaculaRadio(superficie, borderRadiusPercentBar))
            {

                g.FillPath(pincelSuperficie, path);


            }
        }


        private void DibujaPulgar(Graphics g, Rectangle superficieBarra)
        {

            Rectangle recPorcentaje = Rectangle.Inflate(
                CalculaProgresoBarra(_recContenedorPulgar, superficieBarra),
                1,
                1);


            Color colorPulgar = arrastrando ? colorThumb : hoverPulgar ? colorThumbHover : colorThumb;

            using (Brush pincelPulgar = new SolidBrush(colorPulgar))
            using (Brush pincelProgreso = new SolidBrush(trackColorActive))
            using (GraphicsPath pathPulgar = GraphicsRoundedHelper.ObtienPathYCaculaRadio(this._recContenedorPulgar, this.borderRadiusPercentThumb))
            using (GraphicsPath pathPorcentaje = GraphicsRoundedHelper.ObtienPathYCaculaRadio(recPorcentaje, this.borderRadiusPercentBar))
            {
                //Dibujar el progreso
                if (value > 0)
                {
                    g.FillPath(pincelProgreso, pathPorcentaje);

                }

                //Dibuja el pulgar
                g.FillPath(pincelPulgar, pathPulgar);

                //Dibuja el borde dentro del pulgar
                GraphicsBorderHelper.DibujaBorde(
                        g,
                        this._recContenedorPulgar,
                        colorThumbActive,
                        borderSizeThumb,
                        100,
                        3,
                        this.arrastrando
                    );
            }



            //Others
            message = value.ToString();


            using (StringFormat format = StringFormat.GenericTypographic)
            using (Font fuenteTexto = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold))
            using (Brush brusText = new SolidBrush(Color.Black))

            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString(message, fuenteTexto, brusText, superficieBarra, format);
            }
        }
        private void DibujaLetras(Graphics g)
        {
            RectangleF recValueMin = RectangleF.Empty;
            RectangleF recValueMax = RectangleF.Empty;
            if(orientation == Orientation.Horizontal)
            {
                int anchorecMin = _recSuperficiePinturaBarra.Left;
                int anchorecMax = this.Width - _recSuperficiePinturaBarra.Right;
                recValueMin = new RectangleF(
                    0,
                    0,
                    anchorecMin,
                    Height
                    );
                recValueMax = new RectangleF(
                    _recSuperficiePinturaBarra.Right, 
                    0,
                    anchorecMax, 
                    Height
                    );
            }
            else
            {
                int altoValueMin = this.Height -  _recSuperficiePinturaBarra.Bottom;
                int altoValueMax = _recSuperficiePinturaBarra.Top;
                recValueMin = new RectangleF(
                    0,
                    _recSuperficiePinturaBarra.Bottom, 
                    Width,
                    altoValueMin);

                recValueMax = new RectangleF(
                    0, 
                    0, 
                    Width, 
                    altoValueMax);
            }

            
            using(StringFormat format = new StringFormat())
            using (Brush pincelLetras = new SolidBrush(ForeColor))
            {
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                g.DrawString(minValue.ToString(), Font, pincelLetras, recValueMin, format);
                g.DrawString(maxValue.ToString(), Font, pincelLetras, recValueMax, format);
            }

        }
        //paint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(this.Parent.BackColor);

            CalculaSuperficies();

            DibujaBarra(g, _recSuperficieBarra);

            DibujaPulgar(g, _recSuperficieBarra);
            if (showValues)
            {
                DibujaLetras(g);
            }

        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);


            pevent.Graphics.Clear(Color.AntiqueWhite);
        }
    }
}
