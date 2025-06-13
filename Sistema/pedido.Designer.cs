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
            this.cboProduto = new System.Windows.Forms.ComboBox();
            this.datapedido = new System.Windows.Forms.DateTimePicker();
            this.quantidade = new System.Windows.Forms.TextBox();
            this.total = new System.Windows.Forms.TextBox();
            this.cadastrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCliente
            // 
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(219, 81);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(121, 24);
            this.cboCliente.TabIndex = 6;
            // 
            // cboforma
            // 
            this.cboforma.FormattingEnabled = true;
            this.cboforma.Location = new System.Drawing.Point(413, 187);
            this.cboforma.Name = "cboforma";
            this.cboforma.Size = new System.Drawing.Size(121, 24);
            this.cboforma.TabIndex = 7;
            // 
            // cboProduto
            // 
            this.cboProduto.FormattingEnabled = true;
            this.cboProduto.Location = new System.Drawing.Point(247, 287);
            this.cboProduto.Name = "cboProduto";
            this.cboProduto.Size = new System.Drawing.Size(121, 24);
            this.cboProduto.TabIndex = 8;
            // 
            // datapedido
            // 
            this.datapedido.Location = new System.Drawing.Point(285, 130);
            this.datapedido.Name = "datapedido";
            this.datapedido.Size = new System.Drawing.Size(200, 22);
            this.datapedido.TabIndex = 9;
            // 
            // quantidade
            // 
            this.quantidade.Location = new System.Drawing.Point(285, 337);
            this.quantidade.Name = "quantidade";
            this.quantidade.Size = new System.Drawing.Size(100, 22);
            this.quantidade.TabIndex = 10;
            // 
            // total
            // 
            this.total.Location = new System.Drawing.Point(185, 375);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(100, 22);
            this.total.TabIndex = 11;
            // 
            // cadastrar
            // 
            this.cadastrar.BackColor = System.Drawing.Color.Transparent;
            this.cadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cadastrar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cadastrar.Location = new System.Drawing.Point(710, 395);
            this.cadastrar.Name = "cadastrar";
            this.cadastrar.Size = new System.Drawing.Size(93, 43);
            this.cadastrar.TabIndex = 12;
            this.cadastrar.Text = "cadastrar";
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(438, 337);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cadastrar);
            this.Controls.Add(this.total);
            this.Controls.Add(this.quantidade);
            this.Controls.Add(this.datapedido);
            this.Controls.Add(this.cboProduto);
            this.Controls.Add(this.cboforma);
            this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.pictureBox1);
            this.Name = "pedido";
            this.Text = "pedido";
            this.Load += new System.EventHandler(this.pedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.ComboBox cboforma;
        private System.Windows.Forms.ComboBox cboProduto;
        private System.Windows.Forms.DateTimePicker datapedido;
        private System.Windows.Forms.TextBox quantidade;
        private System.Windows.Forms.TextBox total;
        private System.Windows.Forms.Button cadastrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}