using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics; // Para abrir o PDF automaticamente
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static iText.StyledXmlParser.Css.Font.CssFontFace;
using static Sistema.listarproduto;
using Font = System.Drawing.Font;
using PageSize = iTextSharp.text.PageSize;
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
        public void GerarPdfNotaFiscal(int pedidoId, List<ItemCarrinho> itensDoCarrinho, decimal totalPedido,string nomecliente,string formapgto,string status)
        {

            string pastaDestinoPDF = Path.Combine(Application.StartupPath, "notafiscal");
            string pastalogo = Path.Combine(Application.StartupPath, "Imagens");
            // Verifica se a pasta existe, se não, cria
            if (!Directory.Exists(pastaDestinoPDF))
            {
                Directory.CreateDirectory(pastaDestinoPDF);
            }
            string caminhoArquivoPdf = Path.Combine(pastaDestinoPDF,$"NotaFiscal_Pedido_{pedidoId}.pdf");

            // Cria o documento PDF
            Document doc = new Document(PageSize.A4); // Define o tamanho da página

            try
            {

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminhoArquivoPdf, FileMode.Create));
                doc.Open(); // Abre o documento

                // --- Início da Inclusão da Imagem ---

                // 1. Caminho da imagem:
                // Suponha que sua imagem (ex: "logo.png") esteja na pasta "Imagens"
                // dentro do diretório de execução da aplicação.
                string caminhoLogo = Path.Combine(pastalogo, "logo.png");

                if (File.Exists(caminhoLogo))
                {
                    // 2. Carrega a imagem
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(caminhoLogo);

                    // 3. Define a posição e/ou o tamanho da imagem
                    // Exemplo 1: Redimensionar a imagem para uma largura e altura fixas
                    logo.ScaleAbsolute(100f, 50f); // 100 pixels de largura, 50 pixels de altura

                    // Exemplo 2: Redimensionar proporcionalmente para caber em uma largura máxima
                    // float maxWidth = 150f;
                    // float scale = maxWidth / logo.Width;
                    // logo.ScalePercent(scale * 100);

                    // Posiciona a imagem. Exemplo: no canto superior esquerdo com margem.
                    // A posição é em pontos (72 pontos = 1 polegada) a partir do canto inferior esquerdo da página.
                    // doc.PageSize.Width/2 - logo.ScaledWidth/2 centralizaria horizontalmente
                    // doc.PageSize.Height - logo.ScaledHeight - 20f posiciona 20 pontos da margem superior
                    logo.SetAbsolutePosition(40f, doc.PageSize.Height - logo.ScaledHeight - 40f); // 40f da margem esquerda, 40f da margem superior

                    // 4. Adiciona a imagem ao documento
                    doc.Add(logo);

                    // Ajuste a margem superior para que o texto não sobreponha a imagem
                    doc.SetMargins(doc.LeftMargin, doc.RightMargin, doc.TopMargin + logo.ScaledHeight + 20f, doc.BottomMargin);
                    // Reabre para que as novas margens sejam aplicadas a partir de agora
                    // (Note: SetMargins() afeta o conteúdo adicionado *depois* dela.
                    // Para ter a imagem no topo e o texto logo abaixo, você pode adicionar a imagem,
                    // e depois adicionar um "espaço" ou uma nova linha para empurrar o texto.
                    // Ou usar uma tabela de uma célula para centralizar elementos no cabeçalho.)

                }
                else
                {
                    // Opcional: Avisar se a imagem não for encontrada
                    MessageBox.Show($"O arquivo de logo não foi encontrado em: {caminhoLogo}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // --- Fim da Inclusão da Imagem ---



                Font fonte = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
                   ((System.Byte)(0)));

                // Adiciona um título
                Paragraph titulo = new Paragraph("NOTA FISCAL / RECIBO DE COMPRA");
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph(" ")); // Linha em branco

                // Informações do Pedido
                doc.Add(new Paragraph($"Número do Pedido: {pedidoId}"));
                doc.Add(new Paragraph($"Data: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}"));
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Cliente: {nomecliente}")); // Novo
                doc.Add(new Paragraph($"Forma de Pagamento: {formapgto}")); // Novo
                doc.Add(new Paragraph($"Status: {status}")); // Novo
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph(" "));
                // Tabela de Itens do Pedido
                PdfPTable table = new PdfPTable(4); // 4 colunas: Produto, Qtd, Preço Unit, Subtotal
                table.WidthPercentage = 100; // Ocupa 100% da largura da página
                table.SetWidths(new float[] { 40f, 15f, 20f, 25f }); // Define larguras relativas das colunas

                // Cabeçalho da tabela
                table.AddCell(new Phrase("Produto"));
                table.AddCell(new Phrase("Qtd"));
                table.AddCell(new Phrase("Preço Unit."));
                table.AddCell(new Phrase("Subtotal"));

                // Linhas da tabela com os itens do carrinho
                foreach (var item in itensDoCarrinho)
                {
                    table.AddCell(new Phrase(item.NomeProduto));
                    table.AddCell(new Phrase(item.Quantidade.ToString()));
                    table.AddCell(new Phrase(item.PrecoUnitario.ToString("C2")));
                    table.AddCell(new Phrase(item.Subtotal.ToString("C2")));
                }
                doc.Add(table);
                doc.Add(new Paragraph(" "));

                // Total do Pedido
                Paragraph total = new Paragraph($"TOTAL GERAL: {totalPedido.ToString("C2")}");
                total.Alignment = Element.ALIGN_RIGHT;
                doc.Add(total);
                label1.Text = total.ToString();
                doc.Close(); // Fecha e salva o documento

                MessageBox.Show("Nota Fiscal gerada com sucesso!", "PDF Gerado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: Abrir o PDF automaticamente
                Process.Start(caminhoArquivoPdf);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar PDF: {ex.Message}\nDetalhes: {ex.StackTrace}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GerarPdfNotaFiscal(idpedido, _itensDoCarrinho, _itensDoCarrinho.Sum(i => i.Subtotal),cboCliente.Text,cboforma.Text,cbostatus.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
