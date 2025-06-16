using Sistema.classe;
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
        //metodo construtor
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

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           pedido pd= new pedido();
            pd.ShowDialog();
        }

        private void testarConexaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //chamndo a classe de conexao
            //criando um novo acesso
            conexao conexao = new conexao();
            //estou chamando o metodo da classe conexao para abrir o banco
            if(conexao.getConexao() == null)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados");
            }
            else
            {
                MessageBox.Show("Conectado com sucesso");
                conexao.desconectar();
            }
            //chamo o metodo desconectar
           
        }

        private void fecharConexaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conexao conexao= new conexao();
            conexao.desconectar();
        }
    }

}
