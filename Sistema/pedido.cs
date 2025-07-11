using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using MySql.Data.MySqlClient;
using QRCoder;
using QRCoder;
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
        decimal totalPedido;
        //variavel globsl do forms para o banco de dados
        MySqlConnection conexao;
        public pedido(List<ItemCarrinho> carrinhoRecebido)
        {
            _itensDoCarrinho = carrinhoRecebido; // Armazena a lista recebida
             InitializeComponent();
            AplicarEstiloCabecalhoDataGridView();
        }
        //criar o metodo obter dados do banco de dados
        // aplicar atributo ao metodo

        private void AtualizarExibicaoCarrinho()
        {
            // Exemplo: Se você tem um DataGridView chamado 'dgvCarrinho'
            // e um Label para o total 'lblTotalCarrinho'
           //dgvCarrinho.DataSource = null; // Limpa a fonte atual
            dgvCarrinho.DataSource = _itensDoCarrinho; // Atribui a lista atualizada
            dgvCarrinho.Refresh(); // Garante a atualização da exibição

           totalPedido = _itensDoCarrinho.Sum(item => item.Subtotal);
            label1.Text = $"Total: {totalPedido:C2}";
        }
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
                table.AddCell(new Phrase("Quantidade"));
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
        private void AplicarEstiloCabecalhoDataGridView()
        {
            // Exemplo para um DataGridView chamado 'dgvPedidos'

            // 1. Cor de Fundo do Cabeçalho
            dgvCarrinho.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;

            // 2. Cor do Texto do Cabeçalho
            dgvCarrinho.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // 3. Fonte do Cabeçalho
            dgvCarrinho.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // 4. Alinhamento do Texto no Cabeçalho
            dgvCarrinho.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Opcional: Desabilitar o estilo visual do Windows para ver a cor de fundo personalizada
            // Em alguns casos, pode ser necessário para que o BackColor funcione perfeitamente.
            dgvCarrinho.EnableHeadersVisualStyles = false;

            // Outras propriedades úteis:
            // Altura do cabeçalho
            // dgvPedidos.ColumnHeadersHeight = 30;

            // Borda do cabeçalho (se quiser tirar ou mudar a cor)
            // dgvPedidos.BorderStyle = BorderStyle.None;
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
                string sql = "INSERT INTO pedido(idusuario,formapagamento,data_pedido,status,tipo,total) " +
                             "VALUES(@usuario,@forma,@data,@status,'entrada',@total)";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                // Adicionar os PARÂMETROS
                comando.Parameters.AddWithValue("@usuario", idUsuario);
                comando.Parameters.AddWithValue("@forma", cboforma.Text);
                comando.Parameters.AddWithValue("@data", datapedido.Value); // O tipo DateTime é passado corretamente
                comando.Parameters.AddWithValue("@status", cbostatus.Text); // Boolean é passado corretamente
                comando.Parameters.AddWithValue("@total", totalPedido); 

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
                    totalPedido += item.Subtotal;
                    }
                label1.Text = totalPedido.ToString();
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
                cboCliente.DataSource = obterdados("select id, nome from usuario where cargo='Cliente'");
            cboCliente.ValueMember= "id";
            cboCliente.DisplayMember = "nome";
            // dgvCarrinho.DataSource = _itensDoCarrinho;
            AtualizarExibicaoCarrinho();

//            dgvCarrinho.Refresh(); // Garante que o DGV redesenhe
            cboforma.Items.Add("dinheiro");
            cboforma.Items.Add("PIX");
            cboforma.Items.Add("Cartão credito");
            cbostatus.Items.Add("Novo");
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

        private void cboforma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboforma.Text == "PIX")
            {
                // Suponha que você tenha essas informações
                string chavePix = "sua_chave_pix@exemplo.com"; // Ou txtChavePix.Text
                string nomeRecebedor = "Nome da Sua Empresa";
                string cidadeRecebedor = "Sao Paulo";
                decimal valorPix = totalPedido; // Use o total da sua venda
                string txid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 25); // Gerar um TXID único

                // --- Construção de um Payload Pix Simplificado (apenas para demonstração) ---
                // ATENÇÃO: Isso NÃO é um payload BR Code completo e validado pelo Banco Central.
                // Para um Pix completo, você precisaria de uma biblioteca específica de Pix ou construir o payload
                // seguindo as especificações do Banco Central (EMVCo).
                // Um payload completo inclui CRC16 e outros campos.
                // Exemplo de uma string simples que *pode* ser lida por alguns apps como Pix direto:
                string payloadPixSimples = $"00020126{chavePix.Length.ToString().PadLeft(2, '0')}0014BR.GOV.BCB.PIX01{chavePix.Length.ToString().PadLeft(2, '0')}" +
                    $"{chavePix}52040000530398654{valorPix.ToString("F2", System.Globalization.CultureInfo.InvariantCulture).Replace(",", ".")}" +
                    $"5802BR62070503***59{nomeRecebedor.Length.ToString().PadLeft(2, '0')}{nomeRecebedor}60{cidadeRecebedor.Length.ToString().PadLeft(2, '0')}{cidadeRecebedor}6304XXXX"; 
                // O XXXX seria o CRC16

                // Para fins de demonstração, vamos usar uma string mais genérica ou apenas a chave Pix com valor
                // Se o seu objetivo é o Pix "copia e cola" oficial, procure por "Gerador de Payload Pix C#"
                // ou use uma biblioteca dedicada para isso.
                string textoParaQRCode = $"Chave Pix: {chavePix}\nValor: {valorPix:C2}\nNome: {nomeRecebedor}\nTxID: {txid}";
                // Ou, se você tem um payload BR Code completo de uma API, use-o diretamente:
                // string textoParaQRCode = "00020126580014BR.GOV.BCB.PIX0136..."; // Seu payload Pix completo aqui

                try
                {
                    // Gere a imagem do QR Code
                    Bitmap qrCodeBitmap = GerarQRCode(textoParaQRCode);

                    // Exiba no PictureBox
                    pbQRCode.Image = qrCodeBitmap;

                    // Opcional: Salvar a imagem em um arquivo
                    string caminhoSalvarQRCode = Path.Combine(Application.StartupPath, "QRCodes", $"pix_{txid}.png");
                    if (!Directory.Exists(Path.GetDirectoryName(caminhoSalvarQRCode)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(caminhoSalvarQRCode));
                    }
                    qrCodeBitmap.Save(caminhoSalvarQRCode, System.Drawing.Imaging.ImageFormat.Png);

                    MessageBox.Show("QR Code Pix gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao gerar QR Code: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public Bitmap GerarQRCode(string textoParaQRCode)
        {
            // Crie um gerador de QR Code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            // Crie os dados do QR Code a partir do texto
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textoParaQRCode, QRCodeGenerator.ECCLevel.Q);
            // ECCLevel (Error Correction Capability Level):
            // L (Low): 7% dos dados podem ser restaurados.
            // M (Medium): 15% dos dados podem ser restaurados.
            // Q (Quartile): 25% dos dados podem ser restaurados (bom padrão).
            // H (High): 30% dos dados podem ser restaurados.

            // Crie o objeto QR Code
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);

            // Gere o Bitmap da imagem do QR Code
            // pixelsPerModule: Define o tamanho dos "quadradinhos" do QR Code. Maior valor = maior imagem.
            Bitmap qrCodeImage = qrCode.GetGraphic(10); // 10 pixels por módulo é um bom tamanho inicial

            return qrCodeImage;
        }

    }
}
