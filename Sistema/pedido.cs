using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static Sistema.listarproduto;

namespace Sistema
{
    public partial class pedido : Form
    {
        private List<ItemCarrinho> _itensDoCarrinho; // Variável privada para armazenar a lista
        int idUsuario;
        int idpedido;
        //variavel globsl do forms para o banco de dados
        MySqlConnection conexao;
        public pedido(List<ItemCarrinho> carrinhoRecebido)
        {
            _itensDoCarrinho = carrinhoRecebido; // Armazena a lista recebida
       // Limpa a fonte de dados e atribui novamente para forçar a atualização do DGV
       //     dgvCarrinho.DataSource = null;
         //   ConfigurarDataGridViewCarrinho();
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
        private void button1_Click(object sender, EventArgs e)
        {
            //caminho de configuração do servidor
            string data_source = "datasource=localhost;username=root;password='';database=sistema";
            ///abrinddo a cenexao
            conexao = new MySqlConnection(data_source);

         
            try
            {
                //verificar se os campos foram preenchidos
                if (cboCliente.Text == "")
                {
                    MessageBox.Show("cliente está vazio!");
                }
                if (cboforma.Text == "")
                {
                    MessageBox.Show("forma de pagamento está vazio");
                }
                if (datapedido.Text == "")
                {
                    MessageBox.Show("data pedido está vazia!");
                }

                //criando o script sql para inserir as informações
                // Criando o script SQL para inserir as informações com PARÂMETROS
                string sql = "INSERT INTO pedido(idusuario,formapagamento,data_pedido,status) " +
                             "VALUES(@usuario,@forma,@data,@status)";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                // Adicionar os PARÂMETROS
                comando.Parameters.AddWithValue("@usuario", idUsuario);
                comando.Parameters.AddWithValue("@forma", cboforma.Text);
                comando.Parameters.AddWithValue("@data", datapedido.Value); // O tipo DateTime é passado corretamente
                comando.Parameters.AddWithValue("@status", cbostatus.Text); // Boolean é passado corretamente
        
                //abrir o banco de dados
                conexao.Open();
                comando.ExecuteNonQuery();
                // Inicia uma transação para garantir que todas as inserções sejam bem-sucedidas ou nenhuma seja
                idpedido = Convert.ToInt32(comando.LastInsertedId);

                    //executa o sql
                    // 2. Inserir os Itens do Pedido na tabela 'ItensPedido'
                    string queryItemPedido = "INSERT INTO itenspedido (idpedido,idproduto,quantidade,total) " +
                                             "VALUES (@PedidoId, @ProdutoId, @Quantidade, @Subtotal)";

                    foreach (var item in _itensDoCarrinho)
                    {
                        MySqlCommand cmdItem = new MySqlCommand(queryItemPedido, conexao);
                        cmdItem.Parameters.AddWithValue("@PedidoId", idpedido);
                        cmdItem.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                        cmdItem.Parameters.AddWithValue("@PrecoUnitario", item.PrecoUnitario);
                        cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                        cmdItem.Parameters.AddWithValue("@Subtotal", item.Subtotal); // Pega do objeto ItemCarrinho
                        cmdItem.ExecuteNonQuery();
                    }

                    // Se tudo correu bem, confirma a transação
                    MessageBox.Show("Pedido cadastrado com sucesso!");
//                    limparCampos();
                
                //fechar a conexao do banco
                conexao.Close();
            }
            catch (MySqlException ex)
            {
                // Em caso de erro, desfaz a transação
                MessageBox.Show($"Erro de Banco de Dados ao salvar pedido: {ex.Message}\nDetalhes: {ex.StackTrace}", "Erro de Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Outros erros inesperados
            
                MessageBox.Show($"Ocorreu um erro inesperado ao salvar o pedido: {ex.Message}\nDetalhes: {ex.StackTrace}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }

        }

        private void pedido_Load(object sender, EventArgs e)
        {
            //carregar as informações do BD entidade usuario para o combo box cliente
                cboCliente.DataSource = obterdados("select id, nome from usuario");
            cboCliente.ValueMember= "id";
            cboCliente.DisplayMember = "nome";
              dgvCarrinho.DataSource = _itensDoCarrinho;
              dgvCarrinho.Refresh(); // Garante que o DGV redesenhe
            cboforma.Items.Add("dinheiro");
            cboforma.Items.Add("PIX");
            cboforma.Items.Add("Cartão credito");
            cbostatus.Items.Add("andamento");
            cbostatus.Items.Add("concluido");
            cbostatus.Items.Add("cancelado");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            idUsuario = Convert.ToInt32(((DataRowView)cboCliente.SelectedItem)["id"]);

        }
    }
}
