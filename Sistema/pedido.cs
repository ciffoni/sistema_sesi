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

namespace Sistema
{
    public partial class pedido : Form
    {
        //variavel globsl do forms para o banco de dados
        MySqlConnection conexao;
        public pedido()
        {
            InitializeComponent();
        }
        List<string>  itens= new List<string>();
        //criar o metodo obter dados do banco de dados
        // aplicar atributo ao metodo
        public DataTable obterdados(string sql)
        {
            //criar uma tabela de dados
            DataTable dt = new DataTable();

            //caminho de configuração do servidor
            string data_source = "datasource=localhost;username=root;password='';database=sistema";
            ///abrinddo a cenexao
            conexao = new MySqlConnection(data_source);
            //criando o script sql para consultar as informações
           // string sql = "SELECT * from usuario ";
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                itens.Add(checkBox1.Text);
            }
        }

        private void pedido_Load(object sender, EventArgs e)
        {
            //carregar as informações do BD entidade usuario para o combo box cliente
                cboCliente.DataSource = obterdados("select id, nome from usuario");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
