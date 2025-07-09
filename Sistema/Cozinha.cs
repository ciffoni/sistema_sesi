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
using System.Media; // Importar para usar SoundPlayer
using System.IO;   // Importar para Path
namespace Sistema
{
    public partial class Cozinha : Form
    {
        //variavel publica conexao
        MySqlConnection conexao;
        private SoundPlayer player;
        // Para controlar se houve mudança no número de "Novos" pedidos
        private int _quantidadeNovosPedidosAnterior = 0; 

        public Cozinha()
        {

            InitializeComponent();
            dvgPedidos.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
            dvgPedidos.ReadOnly=true;
            dgvItensPedido.ReadOnly=true;

            dvgPedidos.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvPedidos_CellFormatting);

            // Inicializa o SoundPlayer
            try
            {
                // O caminho do arquivo de som deve ser relativo ao diretório de execução do seu EXE
                string somPath = Path.Combine(Application.StartupPath, "sounds", "novoPedido.wav"); // Exemplo: dentro da pasta "Sounds"
                if (File.Exists(somPath))
                {
                    player = new SoundPlayer(somPath);
                    player.Load(); // Carrega o som na memória para reprodução mais rápida
                }
                else
                {
                    MessageBox.Show($"Arquivo de som não encontrado em: {somPath}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o som: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                player = null; // Garante que o player não será usado se houver erro
            }

            // Configura o timer para atualização automática, se ainda não o fez
            System.Windows.Forms.Timer timerAtualizacao = new System.Windows.Forms.Timer();
            timerAtualizacao.Interval = 10000; // A cada 10 segundos (ajuste conforme necessidade)
            timerAtualizacao.Tick += (s, e) => CarregarpedidosPendentes(true); // Passa 'true' para indicar que é uma atualização automática
            timerAtualizacao.Start();
        }

        private void CarregarpedidosPendentes(bool isAutomaticUpdate)
        {
            try
            {
                //caminho de configuração do servidor
                string data_source = "datasource=localhost;username=root;password='';database=sistema";
                ///abrinddo a cenexao
                conexao = new MySqlConnection(data_source);
                //criando o script sql para inserir as informações
                string sql = "SELECT idpedido,idusuario,data_pedido,status FROM pedido WHERE status IN ('Novo', 'Andamento', 'Em Preparo') ORDER BY data_pedido ASC";
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
        //           dvgPedidos.Columns["total"].DefaultCellStyle.Format = "C2"; // Formato de moeda
                // --- Lógica da Notificação Sonora ---
                int quantidadeNovosPedidosAtuais = dtPedidos.AsEnumerable().Count(row => row.Field<string>("Status") == "Novo");

                if (isAutomaticUpdate && quantidadeNovosPedidosAtuais > _quantidadeNovosPedidosAnterior)
                {
                    // Houve um aumento no número de pedidos "Novo" desde a última verificação
                    // Ou, se você quiser tocar sempre que houver algum "Novo" pedido e o som estiver disponível
                    if (player != null)
                    {
                        player.Play(); // Toca o som
                    }
                }
                _quantidadeNovosPedidosAnterior = quantidadeNovosPedidosAtuais; // Atualiza o contador para a próxima verificação
                // --- Fim da Lógica da Notificação Sonora ---
                label1.Text = "Pedidos carregados." + _quantidadeNovosPedidosAnterior.ToString();
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

             CarregarpedidosPendentes(false);
        }

        private void btnAtualizarLista_Click(object sender, EventArgs e)
        {
            CarregarpedidosPendentes(false);
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
                        CarregarpedidosPendentes(false); // Recarrega a lista para refletir a mudança
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
                    string query = "SELECT itenspedido.idproduto,produto.descricao, itenspedido.quantidade, itenspedido.total FROM itenspedido " +
                    "inner join produto on produto.id=itenspedido.idproduto WHERE itenspedido.idpedido = @pedidoId";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@pedidoId", pedidoId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                    DataTable dtItens = new DataTable();
                    adapter.Fill(dtItens);

                    dgvItensPedido.DataSource = dtItens;

                    // Ajustar colunas (opcional)
//                    dgvItensPedido.Columns["itenspedido.total"].DefaultCellStyle.Format = "C2";
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
        private void dgvPedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Certifica-se de que estamos na coluna correta (a coluna "Status")
            // e que não é a linha do cabeçalho
            if (dvgPedidos.Columns[e.ColumnIndex].Name == "status" && e.RowIndex >= 0)
            {
                string status = e.Value?.ToString();
                Color backColor = dvgPedidos.DefaultCellStyle.BackColor; // Cor padrão
                Color foreColor = dvgPedidos.DefaultCellStyle.ForeColor; // Cor padrão
                switch (status)
                {
                    case "Novo":
                        backColor = Color.LightCoral;
                        foreColor = Color.Black;
                        break;
                    case "Em Preparo":
                        backColor = Color.LightYellow;
                        foreColor = Color.Black;
                        break;
                    case "Pronto":
                        backColor = Color.LightGreen;
                        foreColor = Color.Black;
                        break;
                    case "Entregue":
                        backColor = Color.LightGray;
                        foreColor = Color.Gray;
                        break;
                    case "Cancelado":
                        backColor = Color.DarkGray;
                        foreColor = Color.White;
                        break;
                }
                // Aplica a cor à linha inteira
                dvgPedidos.Rows[e.RowIndex].DefaultCellStyle.BackColor = backColor;
                dvgPedidos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = foreColor;
                // É importante definir e.FormattingApplied = true; para evitar que o DataGridView
                // tente formatar a célula novamente com seu estilo padrão.
                e.FormattingApplied = true;
            }
        }
    }
    }

