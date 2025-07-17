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

namespace Sistema
{
    /// <summary>
    /// Classe Produto
    /// Cadastrar os produtos que serão gerenciados pela loja
    /// </summary>
    public partial class frmproduto : Form
    {
        //variavel publica conexao
        MySqlConnection conexao;
        public frmproduto()
        {

          
            InitializeComponent();
        }
        /// <summary>
        /// Botão para escolher a foto do produto
        /// </summary>
        /// <param name="sender">Escolher a foto</param>
        /// <param name="e">definida pela extensão JPG, PNG</param>
        /// <returns>retorna a imagem do produto na picture box</returns>
        /// <remarks>Validar se a imagem é valida ao sistema</remarks>
        private void btnfoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os Arquivos|*.*";
            openFileDialog.Title = "Selecione a Imagem do Produto";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoCompletoFotoOriginal = openFileDialog.FileName;
                string nomeArquivoFoto = Path.GetFileName(caminhoCompletoFotoOriginal);

                // Define a pasta de destino dentro do diretório de execução da aplicação
                // Isso cria uma pasta "ImagensProdutos" onde o seu .exe está rodando
                string pastaDestinoImagens = Path.Combine(Application.StartupPath, "ImagensProdutos");

                // Verifica se a pasta existe, se não, cria
                if (!Directory.Exists(pastaDestinoImagens))
                {
                    Directory.CreateDirectory(pastaDestinoImagens);
                }

                string caminhoDestinoFoto = Path.Combine(pastaDestinoImagens, nomeArquivoFoto);

                try
                {
                    // Copia o arquivo para a pasta de destino
                    File.Copy(caminhoCompletoFotoOriginal, caminhoDestinoFoto, true); // true para sobrescrever se já existir

                    // Atualiza o lblfoto.Text com o caminho RELATIVO que será salvo no banco
                    // Ou apenas o nome do arquivo, se sua lógica de carregamento lidar com isso
                    lblfoto.Text = Path.Combine("ImagensProdutos", nomeArquivoFoto); // Salva "ImagensProdutos\nome.jpg"
                    pictureBox1.Image = Image.FromFile(lblfoto.Text);
                    // Exibe a imagem na PictureBox (se tiver uma)
                    // pictureBoxFotoProduto.ImageLocation = caminhoDestinoFoto;
                    // Ou, para exibir imediatamente do arquivo copiado:
                    // pictureBoxFotoProduto.Image = Image.FromFile(caminhoDestinoFoto);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao copiar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblfoto.Text = ""; // Limpa se houver erro
                }
            }
            /*
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
            }*/
        }
        /// <summary>
        /// Metodo para limpar as informações dos campos do  produto
        /// </summary>
        /// <param name="sender">Limpar Campos</param>
        /// <param name="e">limpa as informações do produto</param>
        /// <returns>limpa as informações do produto apos registro </returns>
        /// <remarks>limpa as informações no sistema</remarks>
        private void limparCampos()
        {
            txtCodigo.Clear();
            txtQuantidade.Clear();
            txtPreco.Clear();
            txtDescricao.Clear();
        }
        /// <summary>
        /// Botão para cadastrar as informações do produto
        /// </summary>
        /// <param name="sender">cadastrar Produtos</param>
        /// <param name="e">Validar as informações preenchidas</param>
        /// <returns>registra as informações do produto </returns>
        /// <remarks>grava as informações no sistema</remarks>
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
        /// <summary>
        /// Botão edita as informações do produto
        /// </summary>
        /// <param name="sender">Editar as informações do produto</param>
        /// <param name="e">alterar as informações dos produtos</param>
        /// <returns>atualiza as informações do produto</returns>
        /// <remarks>Corrigir erros de lançamento ao sistema</remarks>
        private void btneditar_Click(object sender, EventArgs e)
        {
            try
            {
                //verificar se os campos foram preenchidos
                if (txtDescricao.Text == "")
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
                string sql = "UPDATE produto SET descricao=@descricao, quantidade= @quantidade, datacadastro=@datacadastro, promocao=@promocao, foto=@foto, preco= @preco  " +
                             "where id=@id";
                //montar o script sql para executar
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                // Adicionar os PARÂMETROS
                comando.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                comando.Parameters.AddWithValue("@quantidade", Convert.ToInt32(txtQuantidade.Text));
                comando.Parameters.AddWithValue("@datacadastro", calendario.Value); // O tipo DateTime é passado corretamente
                comando.Parameters.AddWithValue("@promocao", promocao.Checked); // Boolean é passado corretamente
                comando.Parameters.AddWithValue("@foto", lblfoto.Text); // A string com barras é passada corretamente
                comando.Parameters.AddWithValue("@preco", Convert.ToDecimal(txtPreco.Text)); // O decimal é passado corretamente
                comando.Parameters.AddWithValue("@id", Convert.ToDecimal(txtCodigo.Text)); // O decimal é passado corretamente

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

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void frmproduto_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = obterdados();
            // Aplica o estilo padrão para o cabeçalho e linhas
           
            ConfiguracaoHelper.AplicarEstiloCabecalhoPadrao(dataGridView1);
            ConfiguracaoHelper.AplicarEstiloLinhasPadrao(dataGridView1);
        }
        /// <summary>
        /// Metodo para obter as informações dos produtos
        /// </summary>
        /// <param name="sender">Obter dados</param>
        /// <param name="e">buscar as informações do produto</param>
        /// <returns>listar as informações do produto </returns>
        /// <remarks>exibir as informações no sistema</remarks>
        public DataTable obterdados()
        {
            //criar uma tabela de dados
            DataTable dt = new DataTable();

            //caminho de configuração do servidor
            string data_source = "datasource=localhost;username=root;password='';database=sistema";
            ///abrinddo a cenexao
            conexao = new MySqlConnection(data_source);
            //criando o script sql para consultar as informações
            string sql = "SELECT * from produto ";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //cria a variavel inteiro
            int codigo = 0;
            //converter a linha selecionada a coluna texto para inteiro
            codigo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            //atribuir o codigo do usuario para o campo id 
            txtCodigo.Text = codigo.ToString(); // convertendo texto
            //recebe no cmapo nome o valor do nome do usuario 
            txtDescricao.Text = dataGridView1.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
            txtQuantidade.Text = dataGridView1.Rows[e.RowIndex].Cells["quantidade"].Value.ToString();
            txtPreco.Text = dataGridView1.Rows[e.RowIndex].Cells["preco"].Value.ToString();
            pictureBox1.Image=Image.FromFile(txtPreco.Text = dataGridView1.Rows[e.RowIndex].Cells["foto"].Value.ToString());
            lblfoto.Text = txtPreco.Text = dataGridView1.Rows[e.RowIndex].Cells["foto"].Value.ToString();

        }
    }
}
