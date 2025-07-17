using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// incluir a biblioteca do mysql
using MySql.Data.MySqlClient;
using BCrypt.Net;
using Sistema.classe;
namespace Sistema
{
    /// <summary>
    /// Representa o formulário de gerenciamento de usuários da aplicação.
    /// Permite cadastrar, visualizar, editar e excluir informações de usuários no sistema.
    /// </summary>
    /// <remarks>
    /// Este formulário interage diretamente com a tabela 'usuario' do banco de dados MySQL.
    /// Utiliza hashing de senhas com BCrypt para segurança.
    /// </remarks>
    public partial class FrmUsuario : Form
    {
        /// <summary>
        /// Instância da conexão MySQL utilizada pelos métodos deste formulário.
        /// Recomenda-se usar a classe 'conexao' ou um gerenciador de conexões para centralizar.
        /// </summary>
        // criar a conexao do mysql
        MySqlConnection conexao;
        /// <summary>
        /// Inicializa uma nova instância do formulário <see cref="FrmUsuario"/>.
        /// </summary>
        public FrmUsuario()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Manipulador de evento para o clique do botão "Cadastrar".
        /// Tenta inserir um novo usuário no banco de dados com base nos dados dos campos de texto.
        /// </summary>
        /// <param name="sender">Cadastrar Novo Usuário.</param>
        /// <param name="e">Preencher as informações solicitadas.</param>
        
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            /// <param name="txtNOme">nome do usuário</param>
            /// <param name="txtEmail">email para entrar no sistema</param>
            /// <param name="cboCargo">define seu perfil no sistema</param>
            /// <param name="txtSenha">Senha para validar no sistema</param>

            /// <returns>retorna o cadastro no sistema</returns>
            /// <remarks>Validar todas as informações</remarks>

            try
            {

                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para inserir as informações
                string sql = "insert into usuario(nome,email,senha,cargo) values(@nome,@email,@senha,@cargo)";
              //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                //abrir o banco de dados
                
                string senhaOriginal =txtSenha.Text.Trim();
                string senhahash = BCrypt.Net.BCrypt.HashPassword(senhaOriginal);
                comando.Parameters.AddWithValue("@nome", txtNOme.Text.Trim());
                comando.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                comando.Parameters.AddWithValue("@cargo", cboCargo.Text.Trim());
                comando.Parameters.AddWithValue("@senha",senhahash);
            //    label6.Text = senhahash;
                
              //  lblMensagemErro.Text = senhahash.Length.ToString();
                conexao.Open();
                 
                //se tiver vazio 
                if (string.IsNullOrEmpty(txtNOme.Text))
                {
                    //alerta para o usuario mensagem verdadeira
                    MessageBox.Show("Nome está vazio!");
                    lblMensagemErro.Text = "Por favor, preencha o nome.";
                    return;
                }
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("email está vazio");
                    lblMensagemErro.Text = "Por favor, preencha o e-mail.";
                    return;
                }

                if (string.IsNullOrEmpty(txtSenha.Text))
                {
                    MessageBox.Show("Senha está vazio");
                    lblMensagemErro.Text = "Por favor, preencha a senha.";
                    return;

                }

                if(txtSenha.Text !="" && txtEmail.Text!="" && txtNOme.Text != "")
                {
                    //executar a consulta no banco de dados
                    comando.ExecuteNonQuery();
                    dadosUsuario.DataSource = obterdados();
                    limparCampos();
                }
                //fechar a conexao do banco
                conexao.Close();
            }catch (Exception ex)
            {
                MessageBox.Show("Erro :"+ ex.Message);
            }
        }
        /// <summary>
        /// Manipulador de evento para o carregamento do formulário <see cref="FrmUsuario"/>.
        /// Carrega os dados dos usuários no DataGridView e aplica estilos visuais.
        /// </summary>
        /// <param name="sender">Carregamento do sistema.</param>
        /// <param name="e">Visualizar os registros no datagrid.</param>
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            //chama os dados da consulta para montar no datagridview
            dadosUsuario.DataSource = obterdados();
            // Aplica o estilo padrão para o cabeçalho e linhas
            ConfiguracaoHelper.AplicarEstiloCabecalhoPadrao(dadosUsuario);
            ConfiguracaoHelper.AplicarEstiloLinhasPadrao(dadosUsuario);
            chkAtivo.Visible = false;
            // Se tiver outros DataGridViews no formulário, aplique também:
            // DataGridViewHelper.AplicarEstiloCabecalhoPadrao(dgvItensPedido);
            // DataGridViewHelper.AplicarEstiloLinhasPadrao(dgvItensPedido);
        }

        // criar um metodo para pesquisar as informações
        /// <summary>
        /// Obtém todos os registros de usuários do banco de dados.
        /// </summary>
        /// <returns>Um <see cref="System.Data.DataTable"/> contendo os dados dos usuários.</returns>
        /// <remarks>
        /// Para ambientes de produção, considere implementar paginação ou filtros
        /// para grandes volumes de dados.
        /// </remarks>
        public DataTable obterdados()
        {
                //criar uma tabela de dados
                DataTable dt = new DataTable();
            
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para consultar as informações
                string sql = "SELECT * from usuario ";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                //abrir o banco de dados
                conexao.Open();
                //montar a consulta com as informações
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
               // montando a tabela com as informações solicitadas
                adapter.Fill(dt);
                //fecho a conexao
                conexao.Close();
                return dt;
    
        }

        /// <summary>
        /// Manipulador de evento para o clique do botão "Excluir".
        /// Exclui um usuário selecionado no DataGridView com base no seu ID.
        /// </summary>
        /// <param name="sender">Botão Excluir.</param>
        /// <param name="e">Apaga as informações do Usuário.</param>
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            /// <param name="txtid">Verificar se tem registro</param>

            /// <returns>Verificar se há registro selecionado</returns>
            /// <remarks>Excluir as informações do ID selecionado</remarks>

            if (txtid.Text != "")
            {
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
            ///abrinddo a cenexao
            conexao = new MySqlConnection(data_source);
            //criando o script sql para deletar as informações
            //Converter o id texto para inteiro 
            string sql = "DELETE FROM USUARIO WHERE id=" + Convert.ToInt32(txtid.Text)  ;
            //montar o script sql para executar
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            //abrir o banco de dados
            conexao.Open();
            //executa a exclusão da informação
            //se executar corretamente
            //verificar se há informação selecionada
           
                if (comando.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Usuário excluido com sucesso");
                    dadosUsuario.DataSource = obterdados();
                    limparCampos();
                }
                else
                {
                    MessageBox.Show("Erro na exclusão do usuário");
                }
            }
            else
            {
                MessageBox.Show("Escolher um usuário para excluir");
            }
        }
        /// <summary>
        /// Manipulador de evento para o clique em uma célula do DataGridView de usuários.
        /// Preenche os campos de texto do formulário com os dados do usuário selecionado.
        /// </summary>
        /// <param name="sender">Selecionar um item da celula .</param>
        /// <param name="e">Os dados do evento, contendo informações sobre a célula clicada.</param>

        private void dadosUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //cria a variavel inteiro
            int codigo = 0;
//converter a linha selecionada a coluna texto para inteiro
            codigo = Convert.ToInt32(dadosUsuario.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
           //atribuir o codigo do usuario para o campo id 
            txtid.Text = codigo.ToString(); // convertendo texto
            //recebe no cmapo nome o valor do nome do usuario 
            txtNOme.Text = dadosUsuario.Rows[e.RowIndex].Cells["nome"].Value.ToString();
            txtEmail.Text = dadosUsuario.Rows[e.RowIndex].Cells["email"].Value.ToString() ;
            txtSenha.Text = dadosUsuario.Rows[e.RowIndex].Cells["senha"].Value.ToString() ;
            cboCargo.Text= dadosUsuario.Rows[e.RowIndex].Cells["cargo"].Value.ToString();
            bool ativo = Convert.ToBoolean(dadosUsuario.Rows[e.RowIndex].Cells["ativo"].Value.ToString());
            chkAtivo.Visible = true;
            if (ativo==true)
                chkAtivo.Checked = true;
            else
                chkAtivo.Checked = true;

        }
        //metodo limpar campos
        /// <summary>
        /// Limpa todos os campos de entrada de texto e combobox do formulário.
        /// </summary>
        private void limparCampos()
        {
            txtid.Clear();
            txtNOme.Clear();
            txtEmail.Clear();
            txtSenha.Clear();
            
        }
        /// <summary>
        /// Manipulador de evento para o clique do botão "Editar".
        /// Tenta atualizar as informações de um usuário existente no banco de dados.
        /// </summary>
        /// <param name="sender">A fonte do evento.</param>
        /// <param name="e">Os dados do evento.</param>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para atualizar as informações
                string sql = "update usuario set nome=@nome,email=@email,senha=@senha, cargo = @cargo, ativo=@ativo"+
                    "where id=" + Convert.ToInt32(txtid.Text);
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                //abrir o banco de dados
                comando.Parameters.AddWithValue("@nome", txtNOme.Text);
                comando.Parameters.AddWithValue("@email", txtEmail.Text);
                comando.Parameters.AddWithValue("@cargo", cboCargo.Text);
                comando.Parameters.AddWithValue("@senha", BCrypt.Net.BCrypt.HashPassword(txtSenha.Text));
                comando.Parameters.AddWithValue("@senha", chkAtivo.Checked);

                conexao.Open();
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
                if (txtEmail.Text == "")
                    MessageBox.Show("email está vazio");

                if (txtSenha.Text == "")
                    MessageBox.Show("Senha está vazio");

                if (txtSenha.Text != "" && txtEmail.Text != "" && txtNOme.Text != "")
                {
                    //executar a consulta no banco de dados
                    comando.ExecuteNonQuery();
                    dadosUsuario.DataSource = obterdados();
                    limparCampos();
                }
                //fechar a conexao do banco
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);
            }
        }
    }
}
