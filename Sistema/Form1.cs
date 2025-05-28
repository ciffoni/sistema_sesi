using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chama o novo formulario
            FrmUsuario usuario = new FrmUsuario();
           //exibi o formulario na tela
            usuario.ShowDialog();

        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmproduto frmproduto = new frmproduto();
            frmproduto.ShowDialog();
        }
    }

}
