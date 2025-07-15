using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.classe
{
    //criar a classe conexao publica
    /// <summary>
    /// Gerencia a conexão com o banco de dados MySQL para a aplicação.
    /// Esta classe é responsável por fornecer uma instância de conexão e gerenciar seu estado.
    /// </summary>
    /// <remarks>
    /// Os parâmetros de conexão (servidor, banco, usuário, senha) são definidos como estáticos
    /// dentro da classe. Certifique-se de que estas credenciais estejam seguras e
    /// não expostas em ambientes de produção. Considere usar configurações externas
    /// (ex: App.config) para ambientes de produção.
    /// </remarks>
    public class conexao
    {
        // criar os atributos
        //variavel do servidor
        // ===========================================
        // ATRIBUTOS ESTÁTICOS DE CONEXÃO
        // ===========================================

        /// <summary>
        /// Endereço do servidor MySQL.
        /// </summary>
        static private string servidor = "localhost";
        //vriavel nome do banco de dados 
        /// <summary>
        /// Nome do banco de dados a ser utilizado.
        /// </summary>
        static private string banco = "sistema";
        //variavel para definir o usuario do banco de dadod
        /// <summary>
        /// Nome de usuário para autenticação no banco de dados.
        /// </summary>
        static private string usuario = "root";
        //varaivel da senha
        /// <summary>
        /// Senha do usuário do banco de dados.
        /// </summary>
        /// <remarks>
        /// **ATENÇÃO**: Usar senhas codificadas diretamente no código-fonte não é seguro
        /// para ambientes de produção. Considere usar métodos mais seguros de armazenamento
        /// e recuperação de credenciais.
        /// </remarks>
        static private string senha = "aula123";
        // ===========================================
        // INSTÂNCIA DA CONEXÃO
        // ===========================================

        /// <summary>
        /// Instância da conexão MySQL aberta.
        /// É nula se a conexão não estiver ativa ou tiver sido desconectada.
        /// </summary>
        public MySqlConnection con = null;

        /// <summary>
        /// String de conexão MySQL completa, montada a partir dos atributos estáticos.
        /// </summary>
        /// <remarks>
        /// Formato: "datasource=[servidor];username=[usuario];password=[senha];database=[banco]"
        /// </remarks>
        // variavel de conexao do banco de dados
        static private string data_source = "datasource="+servidor+";username="+usuario+";password="+senha+";database="+banco;
        // criar os metodos da classe


        // ===========================================
        // MÉTODOS PÚBLICOS
        // ===========================================

        /// <summary>
        /// Obtém uma nova instância de conexão com o banco de dados MySQL.
        /// </summary>
        /// <returns>Um objeto <see cref="MySql.Data.MySqlClient.MySqlConnection"/> configurado para a conexão.</returns>
        /// <remarks>
        /// Este método cria e retorna uma nova instância de conexão a cada chamada,
        /// mas não a abre. A abertura da conexão deve ser feita explicitamente
        /// pelo código que a utiliza (ex: `con.Open()`).
        /// </remarks>
        public MySqlConnection getConexao()
        {
            //inicializa a variavel de conexao
            con = new MySqlConnection(data_source);
            return con;
        }
        //metodo fechar a conexao
        /// <summary>
        /// Define a instância da conexão como nula, efetivamente liberando-a.
        /// </summary>
        /// <remarks>
        /// Este método não fecha ativamente a conexão com o banco de dados (chamar `con.Close()` ou `con.Dispose()`).
        /// Ele apenas redefine a referência local `con`. Para garantir o fechamento e liberação de recursos,
        /// é fortemente recomendado que a conexão seja fechada usando um bloco `using` ou `con.Close()`
        /// no local onde a conexão é aberta.
        /// </remarks>
        public void desconectar()
        {
            if (con != null)
            {
                try
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close(); // Fecha a conexão
                    }
                    con.Dispose(); // Libera os recursos
                }
                catch (MySqlException ex)
                {
                    // Tratar ou logar a exceção, se necessário.
                    // Por exemplo: Console.WriteLine("Erro ao fechar conexão: " + ex.Message);
                    Console.WriteLine("Erro ao fechar conexão: " + ex.Message);

                }
                finally
                {
                    con = null; // Garante que a referência seja nula após a tentativa de fechamento
                }
            }

        }

    }
}
