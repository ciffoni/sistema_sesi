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
using MySql.Data.MySqlClient;
using Sistema.classe;

namespace Sistema
{
    /// <summary>
    /// Classe responsável por gerenciar operações de backup (exportação) do banco de dados MySQL.
    /// Utiliza a biblioteca MySqlBackup.NET para realizar o dump do banco de dados.
    /// </summary>
    public partial class FrmBackup : Form
    {
        public FrmBackup()
        {
            InitializeComponent();
        }
        // Assumindo que 'con' é uma instância MySqlConnection válida
        // e 'cmd' é um MySqlCommand que pode ser configurado.
        // O ideal é que a conexão seja passada ou obtida de forma segura.

        /// <summary>
        /// Realiza a exportação (backup) completo do banco de dados MySQL para um arquivo .sql.
        /// </summary>
        /// <param name="con">Uma instância de conexão <see cref="MySqlConnection"/> aberta ou a ser aberta.</param>
        /// <param name="caminhoDestino">O caminho completo, incluindo o nome do arquivo, onde o backup será salvo (ex: "C:\\Backups\\meu_banco_20240716.sql").</param>
        /// <remarks>
        /// Este método configura a exportação para incluir a criação do banco de dados, procedimentos armazenados e todos os dados das tabelas.
        /// A conexão é aberta e fechada dentro deste método.
        /// </remarks>
        /// <exception cref="MySqlException">Lançada se ocorrer um erro durante a operação de conexão ou backup do MySQL.</exception>
        /// <exception cref="System.IO.IOException">Lançada se houver problemas de permissão de escrita ou acesso ao arquivo de destino.</exception>


        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Define a pasta de destino dentro do diretório de execução da aplicação
            // Isso cria uma pasta "ImagensProdutos" onde o seu .exe está rodando
            string pastaDestinoImagens = Path.Combine(Application.StartupPath, "BackupSQL");

            // Verifica se a pasta existe, se não, cria
            if (!Directory.Exists(pastaDestinoImagens))
            {
                Directory.CreateDirectory(pastaDestinoImagens);
            }
            // O comando 'cmd' é essencialmente usado pelo MySqlBackup internamente para executar o dump.
            // É uma prática comum criar um MySqlCommand vazio ou genérico para esse propósito
            // ou reusar um existente se o MySqlBackup exigir.
            // Para simplicidade, vamos criar um novo comando aqui, vinculado à conexão fornecida.
            
            
            conexao com =new conexao();
            string caminhoDestino = Path.Combine(pastaDestinoImagens, txtNome.Text);
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = com.getConexao();
            using (MySqlBackup mb = new MySqlBackup(cmd))
            {
                try
                {
                    // Configurações de exportação do backup
                    cmd.Connection = con;
                    con.Open();
                    mb.ExportInfo.AddCreateDatabase = true;
                    mb.ExportInfo.ExportProcedures = true;
                    mb.ExportInfo.ExportRows = true;
                    mb.ExportToFile(caminhoDestino);
                    // Outras opções úteis:
                    // mb.ExportInfo.ExportEvents = true; // Eventos
                    mb.ExportInfo.ExportTriggers = true; // Triggers
                   // mb.ExportInfo.ExportViews = true; // Views
                   MessageBox.Show("Backup realizado com sucesso para: " + caminhoDestino);
                                                         
                }
                catch (MySqlException ex)
                {
                    // Tratar exceções específicas do MySQL (ex: falha de conexão, permissão)
                    throw new Exception("Erro ao realizar backup do MySQL: " + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    // Tratar outras exceções (ex: problemas de IO ao escrever o arquivo)
                    throw new Exception("Erro inesperado durante o backup: " + ex.Message, ex);
                }
                finally
                {
                    // Garante que a conexão seja fechada, mesmo que ocorra um erro.
                    // O 'using' já ajuda a dispor, mas o Close é para o estado da conexão.
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

        }
        // Você pode adicionar um método para importar o backup também, se necessário.
        /// <summary>
        /// Realiza a importação (restauração) de um banco de dados MySQL a partir de um arquivo .sql.
        /// </summary>
        /// <param name="con">Uma instância de conexão <see cref="MySqlConnection"/> aberta ou a ser aberta.</param>
        /// <param name="caminhoOrigem">O caminho completo do arquivo .sql a ser importado.</param>
        /// <remarks>
        /// Este método importa todos os dados e estruturas contidos no arquivo SQL.
        /// Recomenda-se garantir que o banco de dados de destino esteja vazio ou pronto para ser sobrescrito.
        /// </remarks>
        /// <exception cref="MySqlException">Lançada se ocorrer um erro durante a operação de importação do MySQL.</exception>
        /// <exception cref="System.IO.IOException">Lançada se houver problemas de leitura do arquivo de origem.</exception>

        private void btnImportar_Click(object sender, EventArgs e)
        {
            string pastaDestinoImagens = Path.Combine(Application.StartupPath, "BackupSQL");
            //nome do arquivo a ser salvo
           // String nomeArquivo = "c:\\backup.sql";
            string StrCon = "server=localhost;user=root;pwd=''";
            string caminhoDestino = Path.Combine(pastaDestinoImagens, txtNome.Text);

            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection con = new MySqlConnection(StrCon);
            using (MySqlBackup mb = new MySqlBackup(cmd))
            {
                cmd.Connection = con;
                con.Open();
                mb.ImportFromFile(caminhoDestino);
                con.Close();

            }

        }
    }
}
