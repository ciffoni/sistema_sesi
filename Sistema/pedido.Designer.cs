namespace Sistema
{
    partial class pedido
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pedido));
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.cboforma = new System.Windows.Forms.ComboBox();
            this.datapedido = new System.Windows.Forms.DateTimePicker();
            this.dgvCarrinho = new System.Windows.Forms.DataGridView();
            this.cbostatus = new System.Windows.Forms.ComboBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.cadastrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCliente
            // 
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(213, 65);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(121, 24);
            this.cboCliente.TabIndex = 6;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // cboforma
            // 
            this.cboforma.FormattingEnabled = true;
            this.cboforma.Location = new System.Drawing.Point(350, 135);
            this.cboforma.Name = "cboforma";
            this.cboforma.Size = new System.Drawing.Size(121, 24);
            this.cboforma.TabIndex = 7;
            // 
            // datapedido
            // 
            this.datapedido.Location = new System.Drawing.Point(271, 95);
            this.datapedido.Name = "datapedido";
            this.datapedido.Size = new System.Drawing.Size(200, 22);
            this.datapedido.TabIndex = 9;
            // 
            // dgvCarrinho
            // 
            this.dgvCarrinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrinho.Location = new System.Drawing.Point(53, 268);
            this.dgvCarrinho.Name = "dgvCarrinho";
            this.dgvCarrinho.RowHeadersWidth = 51;
            this.dgvCarrinho.RowTemplate.Height = 24;
            this.dgvCarrinho.Size = new System.Drawing.Size(651, 150);
            this.dgvCarrinho.TabIndex = 14;
            // 
            // cbostatus
            // 
            this.cbostatus.FormattingEnabled = true;
            this.cbostatus.Location = new System.Drawing.Point(187, 169);
            this.cbostatus.Name = "cbostatus";
            this.cbostatus.Size = new System.Drawing.Size(121, 24);
            this.cbostatus.TabIndex = 15;
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.BackgroundImage = global::Sistema.Properties.Resources.printer;
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Location = new System.Drawing.Point(718, 315);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(72, 57);
            this.btnImprimir.TabIndex = 16;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // cadastrar
            // 
            this.cadastrar.BackColor = System.Drawing.Color.White;
            this.cadastrar.BackgroundImage = global::Sistema.Properties.Resources.income;
            this.cadastrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cadastrar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cadastrar.Location = new System.Drawing.Point(715, 388);
            this.cadastrar.Name = "cadastrar";
            this.cadastrar.Size = new System.Drawing.Size(77, 46);
            this.cadastrar.TabIndex = 12;
            this.cadastrar.UseVisualStyleBackColor = false;
            this.cadastrar.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, -4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(802, 457);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(410, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "label1";
            // 
            // pedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 458);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.cbostatus);
            this.Controls.Add(this.dgvCarrinho);
            this.Controls.Add(this.cadastrar);
            this.Controls.Add(this.datapedido);
            this.Controls.Add(this.cboforma);
            this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.pictureBox1);
            this.Name = "pedido";
            this.Text = "pedido";
            this.Load += new System.EventHandler(this.pedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.ComboBox cboforma;
        private System.Windows.Forms.DateTimePicker datapedido;
        private System.Windows.Forms.Button cadastrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvCarrinho;
        private System.Windows.Forms.ComboBox cbostatus;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Label label1;
    }
}