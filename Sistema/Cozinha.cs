using iText.StyledXmlParser.Jsoup.Nodes;
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
    public partial class Cozinha : Form
    {
        //variavel publica conexao
        MySqlConnection conexao;

        public Cozinha()
        {

            InitializeComponent();
            dvgPedidos.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
            dvgPedidos.ReadOnly=true;
            dgvItensPedido.ReadOnly=true;
        }

        private void CarregarpedidosPendentes()
        {
            try
            {
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para inserir as informações
                string sql = "SELECT idpedido,idusuario, data_pedido,status FROM pedido WHERE status IN ('Novo', 'Andamento', 'Em Preparo') ORDER BY data_pedido ASC";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                //abrir o banco de dados
                conexao.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable dtPedidos = new DataTable();
                adapter.Fill(dtPedidos);

                dvgPedidos.DataSource = dtPedidos;
                // Ajustar colunas (opcional)
                dvgPedidos.Columns["data_pedido"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
          //      dvgPedidos.Columns["Total"].DefaultCellStyle.Format = "C2"; // Formato de moeda

                label1.Text = "Pedidos carregados.";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar pedidos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void btnMarcarpronto_Click(object sender, EventArgs e)
        {
            MudarStatusPedido("Pronto");
        }

        private void Cozinha_Load(object sender, EventArgs e)
        {
            CarregarpedidosPendentes();
        }

        private void btnAtualizarLista_Click(object sender, EventArgs e)
        {
            CarregarpedidosPendentes();
        }

        private void dgvItensPedido_SelectionChanged(object sender, EventArgs e)
        {
        }
        private void MudarStatusPedido(string novoStatus)
        {
            if (dvgPedidos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um pedido para alterar o status.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int pedidoId = Convert.ToInt32(dvgPedidos.SelectedRows[0].Cells["idpedido"].Value);

            //caminho de configuração do servidor
            string data_source = "datasource=localhost;username=root;password='';database=sistema";
            ///abrinddo a cenexao
            conexao = new MySqlConnection(data_source);
            try
            {
                    conexao.Open();
                    string query = "UPDATE pedido SET status = @novoStatus WHERE idpedido = @pedidoId";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@novoStatus", novoStatus);
                    comando.Parameters.AddWithValue("@pedidoId", pedidoId);

                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show($"Pedido {pedidoId} marcado como '{novoStatus}' com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarpedidosPendentes(); // Recarrega a lista para refletir a mudança
                    }
                    else
                    {
                        MessageBox.Show("Falha ao atualizar o status do pedido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
              }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar status do pedido: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        

        private void CarregarItensDoPedido(int pedidoId)
        {
            //caminho de configuração do servidor
            string data_source = "datasource=localhost;username=root;password='';database=sistema";
            ///abrinddo a cenexao
            conexao = new MySqlConnection(data_source);

            try
            {
                    conexao.Open();
                    // Seleciona os itens para o PedidoId
                    // Se você tiver a coluna 'StatusItem', inclua-a na query
                    string query = "SELECT idproduto, quantidade, total FROM itenspedido WHERE idpedido = @pedidoId";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@pedidoId", pedidoId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                    DataTable dtItens = new DataTable();
                    adapter.Fill(dtItens);

                    dgvItensPedido.DataSource = dtItens;

                    // Ajustar colunas (opcional)
                    dgvItensPedido.Columns["total"].DefaultCellStyle.Format = "C2";
                    //dgvItensPedido.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
                }
                catch (Exception ex)//
                {
                    MessageBox.Show($"Erro ao carregar itens do pedido: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        private void dvgPedidos_SelectionChanged(object sender, EventArgs e)
        {
            /*
            if (dvgPedidos.SelectedRows.Count > 0)
            {
               
                DataGridViewRow selectedRow = dvgPedidos.SelectedRows[0];
                int pedidoId = Convert.ToInt32(selectedRow.Cells["idpedido"].Value);
                string statusPedido = selectedRow.Cells["status"].Value.ToString();

                label2.Text = $"Pedido # {pedidoId} - Status: {statusPedido}"; // Atualiza um label com o resumo

                CarregarItensDoPedido(pedidoId);
                MudarStatusPedido(statusPedido); // Habilita/desabilita botões
            }
            else
            {
                dgvItensPedido.DataSource = null; // Limpa os itens se nenhum pedido estiver selecionado
                label1.Text = "Nenhum pedido selecionado.";
                MudarStatusPedido(string.Empty); // Desabilita todos os botões
            }

            */
        }

        private void btnMarcarEmPreparo_Click(object sender, EventArgs e)
        {
            MudarStatusPedido("Em Preparo");
        }

        private void btnMarcarEntregue_Click(object sender, EventArgs e)
        {
            MudarStatusPedido("Entregue");
        }

        private void dvgPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = 0;
            //converter a linha selecionada a coluna texto para inteiro
            codigo = Convert.ToInt32(dvgPedidos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            CarregarItensDoPedido(codigo);
        }
    }
    }

