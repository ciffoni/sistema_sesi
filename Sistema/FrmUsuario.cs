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
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //se tiver vazio 
            if (txtNOme.Text == "")
            {
                //alerta para o usuario mensagem verdadeira
                MessageBox.Show("Nome está vazio!");
            }
            else
            {
                // alerta para o usuario preenchido
                MessageBox.Show("campo preenchido!");
            }
        }
    }
}
