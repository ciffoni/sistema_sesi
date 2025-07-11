namespace Sistema
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produtoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listarProdutosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarPedidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cozinhaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatórioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fluxoDeCaixaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testarConexaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharConexaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informaçãoSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivosToolStripMenuItem,
            this.cadastrarToolStripMenuItem,
            this.pedidoToolStripMenuItem1,
            this.relatórioToolStripMenuItem,
            this.ajudaToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // arquivosToolStripMenuItem
            // 
            resources.ApplyResources(this.arquivosToolStripMenuItem, "arquivosToolStripMenuItem");
            this.arquivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.configuraçãoToolStripMenuItem,
            this.logarToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.arquivosToolStripMenuItem.Name = "arquivosToolStripMenuItem";
            // 
            // backupToolStripMenuItem
            // 
            resources.ApplyResources(this.backupToolStripMenuItem, "backupToolStripMenuItem");
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // configuraçãoToolStripMenuItem
            // 
            resources.ApplyResources(this.configuraçãoToolStripMenuItem, "configuraçãoToolStripMenuItem");
            this.configuraçãoToolStripMenuItem.Name = "configuraçãoToolStripMenuItem";
            this.configuraçãoToolStripMenuItem.Click += new System.EventHandler(this.configuraçãoToolStripMenuItem_Click);
            // 
            // logarToolStripMenuItem
            // 
            resources.ApplyResources(this.logarToolStripMenuItem, "logarToolStripMenuItem");
            this.logarToolStripMenuItem.Name = "logarToolStripMenuItem";
            this.logarToolStripMenuItem.Click += new System.EventHandler(this.logarToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            resources.ApplyResources(this.sairToolStripMenuItem, "sairToolStripMenuItem");
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // cadastrarToolStripMenuItem
            // 
            resources.ApplyResources(this.cadastrarToolStripMenuItem, "cadastrarToolStripMenuItem");
            this.cadastrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuárioToolStripMenuItem,
            this.produtoToolStripMenuItem,
            this.pedidoToolStripMenuItem});
            this.cadastrarToolStripMenuItem.Name = "cadastrarToolStripMenuItem";
            // 
            // usuárioToolStripMenuItem
            // 
            resources.ApplyResources(this.usuárioToolStripMenuItem, "usuárioToolStripMenuItem");
            this.usuárioToolStripMenuItem.Name = "usuárioToolStripMenuItem";
            this.usuárioToolStripMenuItem.Click += new System.EventHandler(this.usuárioToolStripMenuItem_Click);
            // 
            // produtoToolStripMenuItem
            // 
            resources.ApplyResources(this.produtoToolStripMenuItem, "produtoToolStripMenuItem");
            this.produtoToolStripMenuItem.Name = "produtoToolStripMenuItem";
            this.produtoToolStripMenuItem.Click += new System.EventHandler(this.produtoToolStripMenuItem_Click);
            // 
            // pedidoToolStripMenuItem
            // 
            resources.ApplyResources(this.pedidoToolStripMenuItem, "pedidoToolStripMenuItem");
            this.pedidoToolStripMenuItem.Name = "pedidoToolStripMenuItem";
            this.pedidoToolStripMenuItem.Click += new System.EventHandler(this.pedidoToolStripMenuItem_Click);
            // 
            // pedidoToolStripMenuItem1
            // 
            resources.ApplyResources(this.pedidoToolStripMenuItem1, "pedidoToolStripMenuItem1");
            this.pedidoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listarProdutosToolStripMenuItem,
            this.listarPedidosToolStripMenuItem,
            this.cozinhaToolStripMenuItem});
            this.pedidoToolStripMenuItem1.Name = "pedidoToolStripMenuItem1";
            // 
            // listarProdutosToolStripMenuItem
            // 
            resources.ApplyResources(this.listarProdutosToolStripMenuItem, "listarProdutosToolStripMenuItem");
            this.listarProdutosToolStripMenuItem.Name = "listarProdutosToolStripMenuItem";
            this.listarProdutosToolStripMenuItem.Click += new System.EventHandler(this.listarProdutosToolStripMenuItem_Click);
            // 
            // listarPedidosToolStripMenuItem
            // 
            resources.ApplyResources(this.listarPedidosToolStripMenuItem, "listarPedidosToolStripMenuItem");
            this.listarPedidosToolStripMenuItem.Name = "listarPedidosToolStripMenuItem";
            this.listarPedidosToolStripMenuItem.Click += new System.EventHandler(this.listarPedidosToolStripMenuItem_Click);
            // 
            // cozinhaToolStripMenuItem
            // 
            resources.ApplyResources(this.cozinhaToolStripMenuItem, "cozinhaToolStripMenuItem");
            this.cozinhaToolStripMenuItem.Name = "cozinhaToolStripMenuItem";
            this.cozinhaToolStripMenuItem.Click += new System.EventHandler(this.cozinhaToolStripMenuItem_Click);
            // 
            // relatórioToolStripMenuItem
            // 
            resources.ApplyResources(this.relatórioToolStripMenuItem, "relatórioToolStripMenuItem");
            this.relatórioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fluxoDeCaixaToolStripMenuItem});
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            // 
            // fluxoDeCaixaToolStripMenuItem
            // 
            resources.ApplyResources(this.fluxoDeCaixaToolStripMenuItem, "fluxoDeCaixaToolStripMenuItem");
            this.fluxoDeCaixaToolStripMenuItem.Name = "fluxoDeCaixaToolStripMenuItem";
            this.fluxoDeCaixaToolStripMenuItem.Click += new System.EventHandler(this.fluxoDeCaixaToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            resources.ApplyResources(this.ajudaToolStripMenuItem, "ajudaToolStripMenuItem");
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testarConexaoToolStripMenuItem,
            this.fecharConexaoToolStripMenuItem,
            this.informaçãoSistemaToolStripMenuItem});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            // 
            // testarConexaoToolStripMenuItem
            // 
            resources.ApplyResources(this.testarConexaoToolStripMenuItem, "testarConexaoToolStripMenuItem");
            this.testarConexaoToolStripMenuItem.Name = "testarConexaoToolStripMenuItem";
            this.testarConexaoToolStripMenuItem.Click += new System.EventHandler(this.testarConexaoToolStripMenuItem_Click);
            // 
            // fecharConexaoToolStripMenuItem
            // 
            resources.ApplyResources(this.fecharConexaoToolStripMenuItem, "fecharConexaoToolStripMenuItem");
            this.fecharConexaoToolStripMenuItem.Name = "fecharConexaoToolStripMenuItem";
            this.fecharConexaoToolStripMenuItem.Click += new System.EventHandler(this.fecharConexaoToolStripMenuItem_Click);
            // 
            // informaçãoSistemaToolStripMenuItem
            // 
            resources.ApplyResources(this.informaçãoSistemaToolStripMenuItem, "informaçãoSistemaToolStripMenuItem");
            this.informaçãoSistemaToolStripMenuItem.Name = "informaçãoSistemaToolStripMenuItem";
            this.informaçãoSistemaToolStripMenuItem.Click += new System.EventHandler(this.informaçãoSistemaToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // FrmPrincipal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuárioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem produtoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testarConexaoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fecharConexaoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem listarProdutosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informaçãoSistemaToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem listarPedidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cozinhaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatórioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fluxoDeCaixaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraçãoToolStripMenuItem;
    }
}

