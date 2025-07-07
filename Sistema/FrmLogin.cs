using MySql.Data.MySqlClient;
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
    public partial class FrmLogin : Form
    {
        //variavel publica conexao
        MySqlConnection conexao;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                //verificar se os campos foram preenchidos
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("E-mail está vazia!");
                }
                if (txtSenha.Text == "")
                {
                    MessageBox.Show("Senha está vazio");
                }
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para inserir as informações
                // Criando o script SQL para inserir as informações com PARÂMETROS
                string sql = "SELECT  id,nome,email,cargo from usuario " +
                             "where email=@email and senha=@senha";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                // Adicionar os PARÂMETROS
                comando.Parameters.AddWithValue("@email", txtEmail.Text);
                comando.Parameters.AddWithValue("@senha", txtSenha.Text);
                
                //abrir o banco de dados
                conexao.Open();

                if (txtEmail.Text != "" && txtSenha.Text != "")
                {
                    //executa o sql
                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        // string senhaHashedDoBanco = reader["Senha"].ToString();
                        // Armazenar informações do usuário logado (opcional, mas recomendado)
                        // Você pode criar uma classe estática ou singleton para isso

                        SessaoUsuario.CargoUsuario = reader["cargo"].ToString();
                        SessaoUsuario.UsuarioLogado = reader["nome"].ToString();
                        SessaoUsuario.id = Convert.ToInt32(reader["id"]);
                        // Ou passar para o formulário principal
                        MessageBox.Show("Logado com sucesso!");
                        FrmPrincipal principal= new FrmPrincipal();
                        principal.Show();

                    }
                    else
                    {
                        MessageBox.Show("erro no acesso do usuário!");

                    }

                }
                //fechar a conexao do banco
                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
