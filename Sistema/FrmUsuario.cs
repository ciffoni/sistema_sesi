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
namespace Sistema
{
    public partial class FrmUsuario : Form
    {
        // criar a conexao do mysql
        MySqlConnection conexao;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para inserir as informações
                string sql = "insert into usuario(nome,email,senha) values('" + txtNOme.Text + "','" + txtEmail.Text + "','" + txtSenha.Text + "')";
              //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
    //abrir o banco de dados
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

                if(txtSenha.Text !="" && txtEmail.Text!="" && txtNOme.Text != "")
                {
                    //executar a consulta no banco de dados
                    comando.ExecuteNonQuery();
                }
                //fechar a conexao do banco
                conexao.Close();
            }catch (Exception ex)
            {
                MessageBox.Show("Erro :"+ ex.Message);
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
