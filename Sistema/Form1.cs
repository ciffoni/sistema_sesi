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
           pedido pd= new pedido(null);
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

        private void listarProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarproduto lp=new listarproduto();
            lp.ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = SessaoUsuario.UsuarioLogado;
            toolStripStatusLabel2.Text = SessaoUsuario.CargoUsuario;
            AplicarPermissoes();
        }

        private void listarPedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmlistarPedido listar=new FrmlistarPedido();
            listar.ShowDialog();
        }

        private void logarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogin login=new FrmLogin();
            login.ShowDialog();
        }
        private void AplicarPermissoes()
        {
            // Certifique-se de que a SessaoUsuario.CargoUsuario esteja preenchida após o login.
            string cargo = SessaoUsuario.CargoUsuario;

            // Padrão: Desabilitar/Esconder tudo que não é comum a todos os cargos e habilitar/mostrar se houver permissão
            // Você pode começar definindo todos como invisíveis/desabilitados, e depois habilitar/mostrar para cargos específicos.

            // Itens de menu comuns (ex: Sair, Ajuda) podem ser visíveis para todos
            sairToolStripMenuItem.Visible = true; // Exemplo: assuming 'menuItemSair' is the name of your Exit menu item

            // Itens que são liberados a todos os cargo
            logarToolStripMenuItem.Enabled = true; 
            informaçãoSistemaToolStripMenuItem.Visible = true;
            
            // ... continue para outros itens de menu que você queira controlar ...

            switch (cargo)
            {
                case "Gerente":
                    // Administrador tem acesso total
                    cadastrarToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Enabled = true;
                    backupToolStripMenuItem.Visible = true;
                    produtoToolStripMenuItem.Visible = true;
                    listarPedidosToolStripMenuItem.Visible = true;
                    listarProdutosToolStripMenuItem.Visible = true;
                    // Outros itens específicos de Admin...
                    break;

                case "Cozinha":
                    // Gerente pode ver relatórios e cadastrar produtos, mas não gerenciar usuários
                    listarPedidosToolStripMenuItem.Visible = true; // Não pode gerenciar usuários
                    cadastrarToolStripMenuItem.Visible = false;
                    usuárioToolStripMenuItem.Enabled = false;
                    pedidoToolStripMenuItem.Enabled=false;
                    produtoToolStripMenuItem.Visible = false;
                    listarProdutosToolStripMenuItem.Visible = false;
                    backupToolStripMenuItem.Visible = false;

                    break;

                case "Vendedor":
                    // Vendedor só pode acessar funcionalidades de vendas e talvez cadastro de produtos simples
                    listarProdutosToolStripMenuItem.Visible = true;
                    cadastrarToolStripMenuItem.Visible = true;
                    usuárioToolStripMenuItem.Enabled = true;
                    produtoToolStripMenuItem.Visible = false;
                    listarPedidosToolStripMenuItem.Visible = false;
                    pedidoToolStripMenuItem.Enabled = false;
                    backupToolStripMenuItem.Visible = false;
                    // Certifique-se que o menu de "Vendas" ou "Abrir Caixa" esteja acessível
                    // Por exemplo, se tiver um menuItemVendas:
                    // menuItemVendas.Visible = true;
                    // menuItemVendas.Enabled = true;
                    break;

                default:
                    // Cargo desconhecido ou sem permissões padrão, pode ser tratado como um usuário sem privilégios.
                    // Isso pode incluir esconder a maioria dos menus ou exibir uma mensagem de erro e fechar o formulário.
                    MessageBox.Show("Seu cargo não possui permissão para acessar esta funcionalidade. Contate o administrador.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close(); // Fecha o formulário principal se o cargo não tiver acesso
                    break;
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void informaçãoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cozinhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cozinha cozinha = new Cozinha();
            cozinha.ShowDialog();
        }
    }

}
