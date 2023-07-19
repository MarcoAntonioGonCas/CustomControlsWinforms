using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaControllers.Helpers;


namespace MaControllers.Controles.TrackBarControls
{
    public class TrackBarCustom : Control
    {
        #region Eventos

        public event EventHandler ScrollThumb;
        public event EventHandler ValueChange;


        protected void OnScrollThumb()
        {
            this.ScrollThumb?.Invoke(this, EventArgs.Empty);
        }
        protected void OnValueChange()
        {
            this.ValueChange?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Clases Helpers

        ThumbTrackBar _thumb = new ThumbTrackBar();
        TooltipTrackbar _tooltip = new TooltipTrackbar();
        #endregion

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
        private void AjustarTamaniooControl()
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

            CalculaSuperficies();

            UpdateMiniSize();

            this.Font = new Font("Berlin Sans FB", 11.25f, FontStyle.Regular | FontStyle.Italic);

            this.TrackColor = ColorUtils.HtmlToColor("#9E9E9E");
            this.TrackColorActive = _thumb.ColorActive;
        }

       
        private void UpdateMiniSize()
        {
            if (orientation == Orientation.Horizontal)
            {
                this.MinimumSize = new Size(100, 60);
            }
            else {

                this.MinimumSize = new Size(60, 100);
            }

        }
        private void CalculaSuperficies()
        {
            //Todo
            rectSuperficiePinturaBarra = CalculaRectanguloSuperficie();
            rectSuperficieBarra = CalculaBarra(rectSuperficiePinturaBarra);

        }
        //Eventos
        private void AsignarValorDesdePosicion(Point mouseLocation)
        {
            if (Orientation == Orientation.Horizontal)
            {
                int newValue = 0;
                if (mouseLocation.X >= rectSuperficieBarra.X &&
                    mouseLocation.X <= rectSuperficieBarra.Right)
                {
                    newValue = MathHelper.Map(rectSuperficieBarra.X, rectSuperficieBarra.Right, MinValue, MaxValue, mouseLocation.X);
                    

                }
                else if (mouseLocation.X < rectSuperficieBarra.X)
                {
                    newValue = MinValue;
                }
                else if (mouseLocation.X > rectSuperficieBarra.Right)
                {
                    newValue = MaxValue;
                }

                Value = newValue;
            }
            else
            {
                int newValue = 0;


                if (mouseLocation.Y >= rectSuperficieBarra.Y &&
                    mouseLocation.Y <= rectSuperficieBarra.Bottom)
                {
                    newValue = MathHelper.Map(rectSuperficieBarra.Bottom, rectSuperficieBarra.Y, MinValue, MaxValue, mouseLocation.Y);
                }
                else if (mouseLocation.Y < rectSuperficieBarra.Y)
                {
                    newValue = MaxValue;
                }
                else if (mouseLocation.Y > rectSuperficieBarra.Bottom)
                {
                    newValue = MinValue;
                }


                Value = newValue;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);


            if (e.Button == MouseButtons.Left && _thumb.RecThumb.Contains(e.Location))
            {

                this.arrastrando = true;
                this.Invalidate();


            }else if(e.Button == MouseButtons.Left && rectSuperficiePinturaBarra.Contains(e.Location))
            {
                AsignarValorDesdePosicion(e.Location);
                this.arrastrando = true;
            }

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.arrastrando = false;
            this.Cursor = Cursors.Default;


            this.Invalidate();

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

                AsignarValorDesdePosicion(mouseLocation);


            }
            VerificarHoverPulgar(e);
        }

        private void VerificarHoverPulgar(MouseEventArgs e)
        {
            if (_thumb.RecThumb.Contains(e.Location))
            {
                hoverPulgar = true;
            }
            else
            {
                hoverPulgar = false;
            }
            this.Invalidate();
        }


        private Rectangle CalculaSuperficieTexto(Size tamanioLetras)
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
                rectangle = CalculaSuperficieTexto(tamanioLetras);
            }
            else
            {
                rectangle = Rectangle.Inflate(this.ClientRectangle, -2, -2);
            }



            return rectangle;
        }

       
        private Rectangle CalculaBarra(Rectangle superficie)
        {
            Rectangle recBarra;


            //Si esta en horizontal
            if (Orientation == Orientation.Horizontal)
            {
                recBarra = new Rectangle(
                    superficie.X, //empezamos desde x 0
                    ContentAligmenHelper.ObtieneYCentrada(tamanioBarra, superficie),// centro y
                    superficie.Width,//el ancho sera igual al ancho del control
                    tamanioBarra //alto sera igual al tamaño del pulgar
                );
            }
            //Si esta en vertical
            else
            {
                //Empezamos desde x centro
                recBarra = new Rectangle(
                    ContentAligmenHelper.ObtieneXCentrada(tamanioBarra, superficie), // centro x
                    superficie.Y, //empezamos desde y 0
                    tamanioBarra, //el ancho sera el tamaño del pulgar
                    superficie.Height // el alto sera el alto del control
                );
            }
            //int espacioBorde = (borderSize / 2) + bordePadding;

            //rec = Rectangle.Inflate(rec, -espacioBorde, -espacioBorde);

            return recBarra;
        }


        private void DibujarSuperficieBarra(Graphics g, Rectangle superficie)
        {
            using (Brush pincelSuperficieBarra = new SolidBrush(TrackColor))
            using (GraphicsPath path = GraphicsRoundedHelper.ObtieneRutaRedondeada(superficie, borderRadiusPercentBar))
            {

                g.FillPath(pincelSuperficieBarra, path);


            }
        }


        private void DibujarPulgar(Graphics g, Rectangle superficieBarra)
        {

            StateThumb estado;

            if(hoverPulgar && arrastrando)
            {
                estado = StateThumb.ActiveHover;
            }
            else if(arrastrando)
            {
                estado = StateThumb.Active;
            }
            else if (hoverPulgar)
            {
                estado = StateThumb.Hover;
            }
            else
            {
                estado = StateThumb.None;
            }





            this._thumb.DibujarProgresoIndependiente(
                g,
                superficieBarra,
                Orientation,
                value,
                minValue,
                maxValue,
                borderRadiusPercentBar,
                ColorThumbActive);
            //Others

            this._thumb.DibujarPulgar(
                g,
                estado,
                superficieBarra,
                Orientation,
                value,
                minValue,
                maxValue
                );

        }
        private void DibujarValores(Graphics g,Rectangle superficiePintura)
        {




            RectangleF rectValorMinimo = RectangleF.Empty;
            RectangleF rectValorMaximo = RectangleF.Empty;
            Rectangle rectanguloControl = this.ClientRectangle;


            if (orientation == Orientation.Horizontal)
            {


                int anchoValueMin = superficiePintura.Left; // primero de izquierda a derecha


                rectValorMinimo = new RectangleF(
                    rectanguloControl.X,
                    rectanguloControl.Y,
                    anchoValueMin,
                    superficiePintura.Height
                    ); 

                //
                rectValorMaximo = new RectangleF(
                    superficiePintura.Right, // x derecha del control
                    superficiePintura.Top,
                    rectValorMinimo.Width,
                    rectValorMinimo.Height 
                    );
            }
            else
            {
                int altoValueMin = rectanguloControl.Height -  rectSuperficiePinturaBarra.Bottom;


                rectValorMinimo = new RectangleF(
                    rectanguloControl.X,
                    rectSuperficiePinturaBarra.Bottom,
                    rectanguloControl.Width,
                    altoValueMin);

                int altoValueMax = rectSuperficiePinturaBarra.Top;
                rectValorMaximo = new RectangleF(
                    rectanguloControl.X, 
                    rectanguloControl.Y,

                    rectValorMinimo.Width,
                    rectValorMinimo.Height);
            }

            using(StringFormat formatoTexto = new StringFormat())
            using (Brush pincelTexto = new SolidBrush(ForeColor))
            {
                formatoTexto.LineAlignment = StringAlignment.Center;
                formatoTexto.Alignment = StringAlignment.Center;
                g.DrawString(minValue.ToString(), Font, pincelTexto, rectValorMinimo, formatoTexto);
                g.DrawString(maxValue.ToString(), Font, pincelTexto, rectValorMaximo, formatoTexto);
            }

        }




        // Paint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(this.Parent.BackColor);

            CalculaSuperficies();

            DibujarSuperficieBarra(g, rectSuperficieBarra);

          
            DibujarPulgar(g, rectSuperficieBarra);

            if (showValues)
            {
                DibujarValores(g, rectSuperficiePinturaBarra);
            }

            if (arrastrando && ShowTooltip)
            {

                _tooltip.DibujaToolTip(
                        g,
                        rectSuperficieBarra,
                        Orientation,
                        minValue,
                        maxValue,
                        value
                    );
            }

        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);


            pevent.Graphics.Clear(Color.AntiqueWhite);
        }




        // Propiedades
        #region Campos privados



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

        //Colors
        private Color trackColor;
        private Color trackColorActive;




        //Propiedades rectangulos
        private Rectangle rectSuperficiePinturaBarra;
        private Rectangle rectSuperficieBarra;


        //Propiedades publicas
        private Orientation orientation = Orientation.Horizontal;
        private int value = 0;
        private int minValue = 0;
        private int maxValue = 100;


        //Tooltip

        [Category("Tooltip")]
        public bool ShowTooltip { get; set; } = true;
        [Category("Tooltip")]
        public Color BackColorTooltip
        {
            get=>_tooltip.Color;
            set
            {
                _tooltip.Color = value;
                this.Invalidate();
            }
        }
        [Category("Tooltip")]
        public Color ForeColorTooltip
        {
            get=> _tooltip.ForeColor;
            set
            { 
                _tooltip.ForeColor = value;
                this.Invalidate();
            }
        }


        //Text value
        private bool showValues;

        #endregion


        #region Propiedades públicas


        //Propiedades
        [Category("Otros")]
        public Orientation Orientation
        {
            get
            {
                return orientation;
            }

            set
            {
                //Si la orientacion actual es diferente a la nueva
                //se intercambian alto y ancho 
                if (orientation != value)
                {
                    orientation = value;
                    UpdateMiniSize();
                    int aux = Height;
                    Height = Width;
                    Width = aux;
                }

                

                this.Invalidate();

            }
        }

        [RefreshProperties(RefreshProperties.All)]
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
        [RefreshProperties(RefreshProperties.All)]
        [Category("Otros")]
        public int MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                minValue = value;

                if (minValue > maxValue)
                {
                    maxValue = minValue;

                }
                //Si el valor acual es menor al nuevo valor menor
                //Actualizamos el nuevo valor al minimo
                if (this.Value < minValue)
                {
                    this.Value = minValue;
                }


                this.Invalidate();

            }
        }
        [RefreshProperties(RefreshProperties.All)]
        [Category("Otros")]
        public int MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                maxValue = value;

                if (maxValue < minValue)
                {
                    minValue = maxValue;
                }
                //Si el valor acual es mayor al nuevo valor mayor
                //Actualizamos el nuevo valor al maximo
                if (this.Value > maxValue)
                {
                    this.Value = maxValue;
                }
                this.Invalidate();
            }
        }
        [Category("Otros")]
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


        [Category("Otros")]
        public new Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                AjustarTamaniooControl();
                this.Invalidate();

            }
        }


        //Thumb
        [Category("Thumb")]

        public Color ColorThumb
        {
            get
            {
                return _thumb.Color;
            }

            set
            {
                _thumb.Color = value;
                this.Invalidate();
            }
        }
        [Category("Thumb")]
        public Color ColorThumbHover
        {
            get
            {
                return _thumb.ColorHover;
            }

            set
            {
                _thumb.ColorHover = value;
                this.Invalidate();
            }
        }
        [Category("Thumb")]
        public Color ColorThumbActive
        {
            get
            {
                return _thumb.ColorActive;
            }

            set
            {
                _thumb.ColorActive = value;
                this.Invalidate();
            }
        }
        [Category("Thumb")]
        public int SizeThumb
        {
            get => _thumb.Size;
            set
            {
                _thumb.Size = value;
                this.Invalidate();
            }

        }
        [Category("Thumb")]
        public int SizeRadiusPercent
        {
            get => _thumb.BorderRadiusPercent;
            set
            {
                if(value >= 0 && value <= 100)
                {
                    _thumb.BorderRadiusPercent = value;
                    this.Invalidate();
                }
                
            }

        }

        [Category("Track")]
        public Color TrackColor
        {
            get
            {
                return trackColor;
            }

            set
            {
                trackColor = value;
                this.Invalidate();
            }
        }
        [Category("Track")]
        public Color TrackColorActive
        {
            get
            {
                return trackColorActive;
            }

            set
            {
                trackColorActive = value;
                this.Invalidate();
            }
        }

        #endregion

    }
}
