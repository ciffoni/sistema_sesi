namespace Sistema
{
    partial class Cozinha
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
            this.dvgPedidos = new System.Windows.Forms.DataGridView();
            this.dgvItensPedido = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMarcarEmPreparo = new System.Windows.Forms.Button();
            this.btnMarcarpronto = new System.Windows.Forms.Button();
            this.btnMarcarEntregue = new System.Windows.Forms.Button();
            this.btnAtualizarLista = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dvgPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // dvgPedidos
            // 
            this.dvgPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgPedidos.Location = new System.Drawing.Point(31, 55);
            this.dvgPedidos.Name = "dvgPedidos";
            this.dvgPedidos.RowHeadersWidth = 51;
            this.dvgPedidos.RowTemplate.Height = 24;
            this.dvgPedidos.Size = new System.Drawing.Size(625, 151);
            this.dvgPedidos.TabIndex = 0;
            this.dvgPedidos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgPedidos_CellClick);
            this.dvgPedidos.SelectionChanged += new System.EventHandler(this.dvgPedidos_SelectionChanged);
            // 
            // dgvItensPedido
            // 
            this.dgvItensPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItensPedido.Location = new System.Drawing.Point(31, 288);
            this.dgvItensPedido.Name = "dgvItensPedido";
            this.dgvItensPedido.RowHeadersWidth = 51;
            this.dgvItensPedido.RowTemplate.Height = 24;
            this.dgvItensPedido.Size = new System.Drawing.Size(625, 150);
            this.dgvItensPedido.TabIndex = 1;
            this.dgvItensPedido.SelectionChanged += new System.EventHandler(this.dgvItensPedido_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // btnMarcarEmPreparo
            // 
            this.btnMarcarEmPreparo.Location = new System.Drawing.Point(686, 67);
            this.btnMarcarEmPreparo.Name = "btnMarcarEmPreparo";
            this.btnMarcarEmPreparo.Size = new System.Drawing.Size(75, 64);
            this.btnMarcarEmPreparo.TabIndex = 4;
            this.btnMarcarEmPreparo.Text = "Marcar em preparo";
            this.btnMarcarEmPreparo.UseVisualStyleBackColor = true;
            this.btnMarcarEmPreparo.Click += new System.EventHandler(this.btnMarcarEmPreparo_Click);
            // 
            // btnMarcarpronto
            // 
            this.btnMarcarpronto.Location = new System.Drawing.Point(686, 137);
            this.btnMarcarpronto.Name = "btnMarcarpronto";
            this.btnMarcarpronto.Size = new System.Drawing.Size(75, 49);
            this.btnMarcarpronto.TabIndex = 5;
            this.btnMarcarpronto.Text = "Marcar pronto";
            this.btnMarcarpronto.UseVisualStyleBackColor = true;
            this.btnMarcarpronto.Click += new System.EventHandler(this.btnMarcarpronto_Click);
            // 
            // btnMarcarEntregue
            // 
            this.btnMarcarEntregue.Location = new System.Drawing.Point(686, 201);
            this.btnMarcarEntregue.Name = "btnMarcarEntregue";
            this.btnMarcarEntregue.Size = new System.Drawing.Size(75, 54);
            this.btnMarcarEntregue.TabIndex = 6;
            this.btnMarcarEntregue.Text = "Marcar Entregue";
            this.btnMarcarEntregue.UseVisualStyleBackColor = true;
            this.btnMarcarEntregue.Click += new System.EventHandler(this.btnMarcarEntregue_Click);
            // 
            // btnAtualizarLista
            // 
            this.btnAtualizarLista.Location = new System.Drawing.Point(686, 269);
            this.btnAtualizarLista.Name = "btnAtualizarLista";
            this.btnAtualizarLista.Size = new System.Drawing.Size(75, 65);
            this.btnAtualizarLista.TabIndex = 7;
            this.btnAtualizarLista.Text = "Atualizar lista";
            this.btnAtualizarLista.UseVisualStyleBackColor = true;
            this.btnAtualizarLista.Click += new System.EventHandler(this.btnAtualizarLista_Click);
            // 
            // Cozinha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAtualizarLista);
            this.Controls.Add(this.btnMarcarEntregue);
            this.Controls.Add(this.btnMarcarpronto);
            this.Controls.Add(this.btnMarcarEmPreparo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvItensPedido);
            this.Controls.Add(this.dvgPedidos);
            this.Name = "Cozinha";
            this.Text = "Cozinha";
            this.Load += new System.EventHandler(this.Cozinha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvgPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dvgPedidos;
        private System.Windows.Forms.DataGridView dgvItensPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMarcarEmPreparo;
        private System.Windows.Forms.Button btnMarcarpronto;
        private System.Windows.Forms.Button btnMarcarEntregue;
        private System.Windows.Forms.Button btnAtualizarLista;
    }
}