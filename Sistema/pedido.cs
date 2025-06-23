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
using static Sistema.listarproduto;

namespace Sistema
{
    public partial class pedido : Form
    {
        private List<ItemCarrinho> _itensDoCarrinho; // Variável privada para armazenar a lista

        //variavel globsl do forms para o banco de dados
        MySqlConnection conexao;
        public pedido(List<ItemCarrinho> carrinhoRecebido)
        {
            _itensDoCarrinho = carrinhoRecebido; // Armazena a lista recebida
                                                 // Limpa a fonte de dados e atribui novamente para forçar a atualização do DGV
       //     dgvCarrinho.DataSource = null;
            ConfigurarDataGridViewCarrinho();
            InitializeComponent();
        }
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
        private void ConfigurarDataGridViewCarrinho()
        {
            dgvCarrinho.AutoGenerateColumns = false; // Desabilita a geração automática de colunas
            dgvCarrinho.ReadOnly = true; // Torna o DGV somente leitura

            // Adiciona as colunas manualmente
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "NomeProduto",
                HeaderText = "Produto",
                Name = "ColNomeProduto"
            });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecoUnitario",
                HeaderText = "Preço Unit.",
                Name = "ColPrecoUnitario",
                DefaultCellStyle = { Format = "C2" } // Formato de moeda
            });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Quantidade",
                HeaderText = "Qtd",
                Name = "ColQuantidade"
            });
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                Name = "ColSubtotal",
                DefaultCellStyle = { Format = "C2" } // Formato de moeda
            });
            // Opcional: Adicionar uma coluna para o ID do produto, mas torná-la invisível
            dgvCarrinho.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ProdutoId",
                HeaderText = "ID",
                Name = "ColProdutoId",
                Visible = false
            });
        }
        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void pedido_Load(object sender, EventArgs e)
        {
            //carregar as informações do BD entidade usuario para o combo box cliente
                cboCliente.DataSource = obterdados("select id, nome from usuario");
            cboCliente.ValueMember= "id";
            cboCliente.DisplayMember = "nome";
              dgvCarrinho.DataSource = _itensDoCarrinho;
              dgvCarrinho.Refresh(); // Garante que o DGV redesenhe

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
