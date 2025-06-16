using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.classe
{
    //criar a classe conexao publica
    public class conexao
    {
        // criar os atributos
        //variavel do servidor
        static private string servidor = "localhost";
        //vriavel nome do banco de dados 
        static private string banco = "sistema";
        //variavel para definir o usuario do banco de dadod
        static private string usuario = "root";
        //varaivel da senha
        static private string senha = "aula123";

       public  MySqlConnection con = null;

        // variavel de conexao do banco de dados
        static private string data_source = "datasource="+servidor+";username="+usuario+";password="+senha+";database="+banco;
        // criar os metodos da classe
        public MySqlConnection getConexao()
        {
            //inicializa a variavel de conexao
            con = new MySqlConnection(data_source);
            return con;
        }
        //metodo fechar a conexao
        public void desconectar()
        {
            con = null;
           
        }

    }
}
