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

using System.ComponentModel;

namespace MaControllers.Controles
{
    public class RadioButtonCustom : RadioButton
    {

        private Color _borderColor;
        private Color _borderColorActive;
        private Color _ballColorActive;

        public RadioButtonCustom()
        {
            this.BorderColor = Color.DarkCyan;
            this.BorderColorActive = Color.RoyalBlue;
            this.BallColorActive = Color.DarkCyan;


            this.MinimumSize = new Size(0, tamanio + tamanioPincelBorde*2);
            //Add a padding of 10 to the left to have a considerable distance between the text and the RadioButton.
            this.Padding = new Padding(tamanio, 0, 0, 0);


        }

        //Para la bola
        private const int tamanio = 15;
        private const int tamanioPincelBorde = 3;

        private const int tamanioPadding = 2;

        [Category("Otros")]
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
        [Category("Otros")]
        public Color BorderColorActive
        {
            get
            {
                return _borderColorActive;
            }

            set
            {
                _borderColorActive = value;
                this.Invalidate();
            }
        }
        [Category("Otros")]
        public Color BallColorActive
        {
            get
            {
                return _ballColorActive;
            }

            set
            {
                _ballColorActive = value;
                this.Invalidate();
            }
        }

        int ObtieneYCentrada(int tamanioEspacio)
        {
            //return (Height / 2) - (tamanioEspacio / 2);

            return (Height - tamanioEspacio) / 2;
        }


        private int DibujaBola(Graphics g)
        {
            //Empezamos desde el tamaño del pincel entre 2 porque se pinta desde el centro de la linea 
            //teorica adicional damos un espacio adicional 

            int anchoBorde = (tamanioPincelBorde / 2) + tamanioPadding;
            Rectangle superficieBorde = new Rectangle(
                anchoBorde,
                ObtieneYCentrada(tamanio),
                tamanio, 
                tamanio);
            //Ahora como el pincel pinta dentro de la linea teorica solo se suma la mitad del borde
            //ya que esta empezo desde la mitad de la linea teorica y ocupo la mitad del tamaño del tamaño del 
            //pincel adicional añadimos un paddign o un espacio extra
            int espacio = (tamanioPincelBorde / 2) + tamanioPadding;

            Rectangle superficieBolitaInterna = Rectangle.Inflate(superficieBorde, -espacio, -espacio);

          
            using (Pen pincelBorde = new Pen(Checked ? BorderColorActive: BorderColor, tamanioPincelBorde))
            using(Brush brus = new SolidBrush(BallColorActive))
            {

                g.DrawEllipse(pincelBorde, superficieBorde);

                if (Checked)
                {
                    g.FillEllipse(brus, superficieBolitaInterna);

                }
            }


            
            //Devuelve la derecha de la superficie del borde de bola 
            //más lo que ocupa el pincel
            return superficieBorde.Right + anchoBorde;
        }
        private void DibujaTexto(Graphics g,int x)
        {
            RectangleF superficieTexto = new RectangleF(
                x,
                0,
                Width - x,
                Height);

           
            using (Brush pincelTextp = new SolidBrush(this.ForeColor))
            using (StringFormat format = new StringFormat())
            {
                
                format.Alignment = ContentAligmenHelper.ConvertToHorizontalAlignment(this.TextAlign);
                format.LineAlignment = ContentAligmenHelper.ConvertToVerticalAlignment(this.TextAlign);


                g.DrawString(this.Text, this.Font, pincelTextp, superficieTexto, format);
            }

        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
           
            Graphics g = pevent.Graphics;
            g.Clear(this.Parent.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int x = DibujaBola(g);
            DibujaTexto(g,x);

        }
    }
}
