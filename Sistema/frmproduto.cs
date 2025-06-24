using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace Sistema
{
    public partial class frmproduto : Form
    {
        //variavel publica conexao
        MySqlConnection conexao;
        public frmproduto()
        {

          
            InitializeComponent();
        }

        private void btnfoto_Click(object sender, EventArgs e)
        {
            //abrir a caixa de seleção 
            OpenFileDialog foto= new OpenFileDialog();
            //filtra as extensões
            foto.Filter = "Image file(*.jpg;*.png;*.jpeg)|*.jpeg; *.jpg;*.png";
            //verificar se houve escolha da foto
            if (foto.ShowDialog() == DialogResult.OK)
            {
                //variavel para receber a imagem
                Image arquivo = Image.FromFile(foto.FileName);
                //picture recebe a imagem da foto
                pictureBox1.Image = arquivo; 
                //mostra o caminho da foto
                lblfoto.Text = foto.FileName;
            }
        }
        private void limparCampos()
        {
            txtCodigo.Clear();
            txtQuantidade.Clear();
            txtPreco.Clear();
            txtDescricao.Clear();
        }
        private void btncadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                //verificar se os campos foram preenchidos
                if (txtDescricao.Text == "" )
            {
                MessageBox.Show("Descrição está vazia!");
            }
            if (txtPreco.Text == "")
            {
                MessageBox.Show("Preço está vazio");
            }
            if (txtQuantidade.Text == "")
            {
                MessageBox.Show("Quantidade está vazia!");
            }

                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para inserir as informações
                // Criando o script SQL para inserir as informações com PARÂMETROS
                string sql = "INSERT INTO produto(descricao, quantidade, datacadastro, promocao, foto, preco) " +
                             "VALUES(@descricao, @quantidade, @datacadastro, @promocao, @foto, @preco)";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                // Adicionar os PARÂMETROS
                comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                comando.Parameters.AddWithValue("@quantidade", Convert.ToInt32(txtQuantidade.Text));
                comando.Parameters.AddWithValue("@datacadastro", calendario.Value); // O tipo DateTime é passado corretamente
                comando.Parameters.AddWithValue("@promocao", promocao.Checked); // Boolean é passado corretamente
                comando.Parameters.AddWithValue("@foto", lblfoto.Text); // A string com barras é passada corretamente
                comando.Parameters.AddWithValue("@preco", Convert.ToDecimal(txtPreco.Text)); // O decimal é passado corretamente

                //abrir o banco de dados
                conexao.Open();

                if (txtDescricao.Text != "" && txtPreco.Text != "" && txtQuantidade.Text != "")
                {
                    //executa o sql
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Produto cadastrado com sucesso!");
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

        private void calendario_ValueChanged(object sender, EventArgs e)
        {
            
            label6.Text = calendario.ToString();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }
    }
}
