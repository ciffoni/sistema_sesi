using iText.StyledXmlParser.Jsoup.Nodes;
using MySql.Data.MySqlClient;
using Sistema.classe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Para o controle Chart


namespace Sistema
{
    public partial class fluxocaixa : Form
    {
        MySqlConnection conexao;
        string data_source = "datasource=localhost;username=root;password='';database=sistema";
        DateTime dataInicial ;
        DateTime dataFinal ; // Inclui o dia inteiro
        DataTable dtMovimentacoes;
        public fluxocaixa()
        {
            InitializeComponent();
            ConfigurarChart();
        }

        private void ConfigurarChart()
        {
            chartFluxoCaixa.Titles.Clear();
            chartFluxoCaixa.Series.Clear();
            chartFluxoCaixa.ChartAreas.Clear();

            // Adiciona uma área de gráfico
            ChartArea chartArea = new ChartArea("MainArea");
            chartFluxoCaixa.ChartAreas.Add(chartArea);

            // Configura os eixos
            chartArea.AxisX.MajorGrid.Enabled = false; // Desabilita grades verticais
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray; // Cor das grades horizontais
            chartArea.AxisY.LabelStyle.Format = "C2"; // Formato de moeda para o eixo Y
            chartArea.AxisY.Title = "Valor (R$)";
            chartArea.AxisX.Title = "Período";

            // Adiciona as séries (Entradas, Saídas, Saldo)
            Series seriesEntradas = new Series("Entradas");
            seriesEntradas.ChartType = SeriesChartType.Column; // Ou Line, Bar, etc.
            seriesEntradas.Color = Color.Green;
            seriesEntradas.IsValueShownAsLabel = true; // Mostrar valor em cima da coluna
            seriesEntradas.LabelFormat = "C2";
            chartFluxoCaixa.Series.Add(seriesEntradas);

            Series seriesSaidas = new Series("Saídas");
            seriesSaidas.ChartType = SeriesChartType.Column;
            seriesSaidas.Color = Color.Red;
            seriesSaidas.IsValueShownAsLabel = true;
            seriesSaidas.LabelFormat = "C2";
            chartFluxoCaixa.Series.Add(seriesSaidas);

            Series seriesSaldo = new Series("Saldo");
            seriesSaldo.ChartType = SeriesChartType.Line; // Saldo como linha
            seriesSaldo.Color = Color.Blue;
            seriesSaldo.BorderWidth = 3;
            seriesSaldo.IsValueShownAsLabel = true;
            seriesSaldo.LabelFormat = "C2";
            chartFluxoCaixa.Series.Add(seriesSaldo);
        }

        private void fluxocaixa_Load(object sender, EventArgs e)
        {

        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
             dataInicial = dtpDataInicial.Value.Date;
            dataFinal = dtpDataFinal.Value.Date.AddDays(1).AddSeconds(-1); // Inclui o dia inteiro

            GerarRelatorio(dataInicial, dataFinal);
        }
        private void GerarRelatorio(DateTime inicio, DateTime fim)
        {
             dtMovimentacoes = new DataTable();

            conexao = new MySqlConnection(data_source);
          
                try
                {
                    conexao.Open();

                    // Consulta para Entradas (Vendas de Pedidos)
                    string queryEntradas = @"
                    SELECT data_pedido AS Data, Total AS Valor, 'Entrada' AS Tipo
                    FROM pedido
                    WHERE data_pedido BETWEEN @inicio AND @fim";

                    // Consulta para Outras Entradas e Saídas (LancamentosFinanceiros)
                    string queryLancamentos = @"
                    SELECT DataLancamento AS Data, Valor, Tipo
                    FROM LancamentosFinanceiros
                    WHERE DataLancamento BETWEEN @inicio AND @fim";

                    // Combina os dados
                    MySqlDataAdapter adapterEntradas = new MySqlDataAdapter(queryEntradas, conexao);
                    adapterEntradas.SelectCommand.Parameters.AddWithValue("@inicio", inicio);
                    adapterEntradas.SelectCommand.Parameters.AddWithValue("@fim", fim);
                    adapterEntradas.Fill(dtMovimentacoes);

                    MySqlDataAdapter adapterLancamentos = new MySqlDataAdapter(queryLancamentos, conexao);
                    adapterLancamentos.SelectCommand.Parameters.AddWithValue("@inicio", inicio);
                    adapterLancamentos.SelectCommand.Parameters.AddWithValue("@fim", fim);
                    adapterLancamentos.Fill(dtMovimentacoes);

                    // Processar os dados
                    ProcessarDadosParaGrafico(dtMovimentacoes);

                    MessageBox.Show("Relatório gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao gerar relatório: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
            private void ProcessarDadosParaGrafico(DataTable dados)
        {
            // Limpa os dados antigos do gráfico
            foreach (var series in chartFluxoCaixa.Series)
            {
                series.Points.Clear();
            }

            decimal totalEntradasGeral = 0;
            decimal totalSaidasGeral = 0;

            // Agrupa os dados por dia ou mês
            var grupos = dados.AsEnumerable().GroupBy(row =>
            {
                DateTime data = row.Field<DateTime>("Data");
                if (rbDiario.Checked)
                {
                    return data.Date; // Agrupa por dia
                }
                else // rbMensal.Checked
                {
                    return new DateTime(data.Year, data.Month, 1); // Agrupa por primeiro dia do mês
                }
            })
            .OrderBy(g => g.Key); // Ordena os grupos pela data

            foreach (var grupo in grupos)
            {
                string label;
                if (rbDiario.Checked)
                {
                    label = grupo.Key.ToString("dd/MM"); // Ex: 01/07
                    chartFluxoCaixa.ChartAreas["MainArea"].AxisX.LabelStyle.Format = "dd/MM";
                }
                else
                {
                    label = grupo.Key.ToString("MMM/yyyy"); // Ex: Jul/2025
                    chartFluxoCaixa.ChartAreas["MainArea"].AxisX.LabelStyle.Format = "MMM/yyyy";
                }

                decimal entradas = grupo.Where(r => r.Field<string>("Tipo") == "Entrada").Sum(r => r.Field<decimal>("Valor"));
                decimal saidas = grupo.Where(r => r.Field<string>("Tipo") == "Saida").Sum(r => r.Field<decimal>("Valor"));
                decimal saldo = entradas - saidas;

                chartFluxoCaixa.Series["Entradas"].Points.AddXY(label, entradas);
                chartFluxoCaixa.Series["Saídas"].Points.AddXY(label, saidas);
                chartFluxoCaixa.Series["Saldo"].Points.AddXY(label, saldo);

                totalEntradasGeral += entradas;
                totalSaidasGeral += saidas;
            }

            // Atualiza os Labels de total (se você os tiver no formulário)
            lblTotalEntradas.Text = $"Total Entradas: {totalEntradasGeral:C2}";
            lblTotalSaidas.Text = $"Total Saídas: {totalSaidasGeral:C2}";
            lblSaldoFinal.Text = $"Saldo Final: {(totalEntradasGeral - totalSaidasGeral):C2}";

            // Atualiza o título do gráfico
            string tituloGrafico = rbDiario.Checked ? "Fluxo de Caixa Diário" : "Fluxo de Caixa Mensal";
            chartFluxoCaixa.Titles.Clear();
            chartFluxoCaixa.Titles.Add(tituloGrafico);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string endereco = "relatoriofinanceiro.csv";
            // Define a pasta de destino dentro do diretório de execução da aplicação
            // Isso cria uma pasta "ImagensProdutos" onde o seu .exe está rodando
            string pastaDestino = Path.Combine(Application.StartupPath, "RelatorioExcel");

            // Verifica se a pasta existe, se não, cria
            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }
            string caminhoDestino = Path.Combine(pastaDestino, endereco);

          

            conexao com = new conexao();

            using (StreamWriter writer = new StreamWriter(caminhoDestino, false, Encoding.GetEncoding("iso-8859-15")))

            {

                // Cabeçalho 
                writer.WriteLine("Relatório Financeiro");
                writer.WriteLine("Data;Valor;Tipo");


                // Conexão com o banco de dados

                MySqlConnection conexao = com.getConexao();


                // Consulta para Entradas (Vendas de Pedidos)
                string queryEntradas = @"
                    SELECT data_pedido AS Data, Total AS Valor, 'Entrada' AS Tipo
                    FROM pedido
                    WHERE data_pedido BETWEEN @inicio AND @fim";

                // Consulta para Outras Entradas e Saídas (LancamentosFinanceiros)
                string queryLancamentos = @"
                    SELECT DataLancamento AS Data, Valor, Tipo
                    FROM LancamentosFinanceiros
                    WHERE DataLancamento BETWEEN @inicio AND @fim";

                /* Combina os dados
                MySqlDataAdapter adapterEntradas = new MySqlDataAdapter(queryEntradas, conexao);
                adapterEntradas.SelectCommand.Parameters.AddWithValue("@inicio", dataInicial);
                adapterEntradas.SelectCommand.Parameters.AddWithValue("@fim", dataFinal);
                adapterEntradas.Fill(dtMovimentacoes);

                MySqlDataAdapter adapterLancamentos = new MySqlDataAdapter(queryLancamentos, conexao);
                adapterLancamentos.SelectCommand.Parameters.AddWithValue("@inicio", dataInicial);
                adapterLancamentos.SelectCommand.Parameters.AddWithValue("@fim", dataFinal);
                adapterLancamentos.Fill(dtMovimentacoes);
                */


                MySqlCommand sqlComand = new MySqlCommand(queryLancamentos, conexao);
                sqlComand.Parameters.AddWithValue("@inicio", dataInicial);
                sqlComand.Parameters.AddWithValue("@fim", dataFinal);


                conexao.Open();
                // Combina os dados


                using (IDataReader reader = sqlComand.ExecuteReader())

                {

                    while (reader.Read())

                    {

                        // escrevendo os registros

                        writer.WriteLine(Convert.ToString(reader["data"]) + ";" + Convert.ToString(reader["valor"]) + ";" + Convert.ToString(reader["Tipo"]));

                    }

                }

                conexao.Close();
                // mensagem de arquivo gerado com sucesso.

                MessageBox.Show("Relatório gerado com sucesso.", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            }
        }
    }
