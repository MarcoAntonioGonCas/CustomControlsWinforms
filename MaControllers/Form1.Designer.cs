namespace MaControllers
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.trackBarCustom1 = new MaControllers.Controles.TrackBarCustom();
            this.buttonCustom1 = new MaControllers.Controles.ButtonCustom();
            this.circularPictureBox1 = new MaControllers.Controles.CircularPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBox1.Location = new System.Drawing.Point(27, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(429, 69);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 13;
            this.trackBar1.MouseCaptureChanged += new System.EventHandler(this.trackBar1_MouseCaptureChanged);
            // 
            // trackBarCustom1
            // 
            this.trackBarCustom1.Font = new System.Drawing.Font("Berlin Sans FB", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarCustom1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.trackBarCustom1.Location = new System.Drawing.Point(278, 149);
            this.trackBarCustom1.MaxValue = 100;
            this.trackBarCustom1.MinValue = 0;
            this.trackBarCustom1.Name = "trackBarCustom1";
            this.trackBarCustom1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarCustom1.ShowValues = true;
            this.trackBarCustom1.Size = new System.Drawing.Size(50, 247);
            this.trackBarCustom1.TabIndex = 14;
            this.trackBarCustom1.Text = "trackBarCustom1";
            this.trackBarCustom1.Value = 10;
            // 
            // buttonCustom1
            // 
            this.buttonCustom1.BackColor = System.Drawing.Color.White;
            this.buttonCustom1.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonCustom1.BorderColor2 = System.Drawing.Color.Orchid;
            this.buttonCustom1.BorderDashStyle = System.Drawing.Drawing2D.DashCap.Round;
            this.buttonCustom1.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.buttonCustom1.BorderRadiusPercent = 100;
            this.buttonCustom1.BorderSizePx = 2;
            this.buttonCustom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCustom1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonCustom1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCustom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCustom1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCustom1.GradientBorder = true;
            this.buttonCustom1.GradientBorderAngle = 45;
            this.buttonCustom1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCustom1.Location = new System.Drawing.Point(39, 61);
            this.buttonCustom1.Name = "buttonCustom1";
            this.buttonCustom1.Size = new System.Drawing.Size(177, 43);
            this.buttonCustom1.TabIndex = 10;
            this.buttonCustom1.Text = "buttonCustom1";
            this.buttonCustom1.UseVisualStyleBackColor = false;
            this.buttonCustom1.Click += new System.EventHandler(this.buttonCustom1_Click);
            // 
            // circularPictureBox1
            // 
            this.circularPictureBox1.AnguloBackground = 45F;
            this.circularPictureBox1.Background = System.Drawing.Color.MediumVioletRed;
            this.circularPictureBox1.Background2 = System.Drawing.Color.RoyalBlue;
            this.circularPictureBox1.BorderRadiouPercent = 20;
            this.circularPictureBox1.BorderSize = 0;
            this.circularPictureBox1.CapStyle = System.Drawing.Drawing2D.DashCap.Round;
            this.circularPictureBox1.Image = global::MaControllers.Properties.Resources._34;
            this.circularPictureBox1.Location = new System.Drawing.Point(39, 122);
            this.circularPictureBox1.MinimumSize = new System.Drawing.Size(20, 20);
            this.circularPictureBox1.Name = "circularPictureBox1";
            this.circularPictureBox1.Size = new System.Drawing.Size(177, 177);
            this.circularPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.circularPictureBox1.StyleBorde = System.Drawing.Drawing2D.DashStyle.Dash;
            this.circularPictureBox1.TabIndex = 8;
            this.circularPictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trackBarCustom1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonCustom1);
            this.Controls.Add(this.circularPictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controles.CircularPictureBox circularPictureBox1;
        private Controles.ButtonCustom buttonCustom1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controles.TrackBarCustom trackBarCustom1;
    }
}

