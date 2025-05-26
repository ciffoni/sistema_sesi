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
                    dadosUsuario.DataSource = obterdados();
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
            //chama os dados da consulta para montar no datagridview
            dadosUsuario.DataSource = obterdados();
        }

        // criar um metodo para pesquisar as informações
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

        private void btnExcluir_Click(object sender, EventArgs e)
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
            if (comando.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Usuário excluido com sucesso");
                dadosUsuario.DataSource = obterdados();
            }
            else
            {
                MessageBox.Show("Erro na exclusão do usuário");
            }

        }

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
        }
    }
}
