namespace Chess
{
    partial class index
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(index));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mejorJugadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jugadorActual = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ganador = new System.Windows.Forms.PictureBox();
            this.grupoGanador = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jugadorActual)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ganador)).BeginInit();
            this.grupoGanador.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.mejorJugadaToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            resources.ApplyResources(this.archivoToolStripMenuItem, "archivoToolStripMenuItem");
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Click += new System.EventHandler(this.reset);
            // 
            // mejorJugadaToolStripMenuItem
            // 
            resources.ApplyResources(this.mejorJugadaToolStripMenuItem, "mejorJugadaToolStripMenuItem");
            this.mejorJugadaToolStripMenuItem.Name = "mejorJugadaToolStripMenuItem";
            this.mejorJugadaToolStripMenuItem.Click += new System.EventHandler(this.mejorJugada);
            // 
            // jugadorActual
            // 
            resources.ApplyResources(this.jugadorActual, "jugadorActual");
            this.jugadorActual.Name = "jugadorActual";
            this.jugadorActual.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.jugadorActual);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ganador
            // 
            resources.ApplyResources(this.ganador, "ganador");
            this.ganador.Name = "ganador";
            this.ganador.TabStop = false;
            // 
            // grupoGanador
            // 
            this.grupoGanador.BackColor = System.Drawing.Color.White;
            this.grupoGanador.Controls.Add(this.ganador);
            resources.ApplyResources(this.grupoGanador, "grupoGanador");
            this.grupoGanador.Name = "grupoGanador";
            this.grupoGanador.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // index
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grupoGanador);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "index";
            this.Load += new System.EventHandler(this.index_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jugadorActual)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ganador)).EndInit();
            this.grupoGanador.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.PictureBox jugadorActual;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox ganador;
        private System.Windows.Forms.GroupBox grupoGanador;
        private System.Windows.Forms.ToolStripMenuItem mejorJugadaToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;



    }
}

