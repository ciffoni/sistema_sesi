using MySql.Data.MySqlClient;
using Sistema.classe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Sistema
{
    public partial class listarproduto : Form
    {
        //crio a conexao com o banco
        MySqlConnection conexao;
        //criar uma lista de item selecionado
        public List<ItemCarrinho> CarrinhoAtual=new List<ItemCarrinho>();
        public listarproduto()
        {
            InitializeComponent();
        }
//criar a classe item carrinho para estruturar as informações
        public class ItemCarrinho
        {
            public int ProdutoId { get; set; }
            public string NomeProduto { get; set; }
            public decimal PrecoUnitario { get; set; }
            public int Quantidade { get; set; }
            public decimal Subtotal => PrecoUnitario * Quantidade; // Propriedade calculada
        }
        private void listarproduto_Load(object sender, EventArgs e)
        {
            //crio a tabela para popular
            DataTable dados= new DataTable();
            //obtenho as informações dos produtos
            dados = obterdados("select id,descricao,foto,preco,quantidade from produto");
            int registros = 0;//variavel para guardar os registros localizados
            int x = 10, y = 10;// minhas coordenadas
            int qtdeprodutos;// variavel de looping no banco de dados
            //busca das informações no banco 
            //verificar a quantidade de registros na tabela
            for(registros=0;registros<dados.Rows.Count;registros++)
            {
                //crio os paineis com o produtos
                Panel produtos= new Panel();
                //defino a localização do painel
                produtos.Location=new Point(x,y);
                //defino a altura e largura
                produtos.Height = 250;
                produtos.Width = 250;
                //crio o label para o codigo
                Label idproduto= new Label();
                //pega a informação do 1 campo da tabela
                idproduto.Text = dados.Rows[registros][0].ToString();
                //adicionar i id ao painel
                idproduto.Width = 30;
                Label descricao= new Label();
                descricao.Location=new Point(20,50);
                descricao.Text = dados.Rows[registros][1].ToString();
                //criar o espaço para a foto
                PictureBox foto= new PictureBox();
                foto.Name = "foto";
                foto.SizeMode = PictureBoxSizeMode.StretchImage;
                foto.Image = Image.FromFile(dados.Rows[registros][2].ToString());
                foto.Location= new Point(30,0);
                Label preco= new Label();
                preco.Name = "preco";
                preco.Text = dados.Rows[registros][3].ToString();
                preco.Location = new Point(20, 85);
                Label quantidade= new Label();
                quantidade.Name = "quantidade";
                quantidade.Text = dados.Rows[registros][4].ToString();
                quantidade.Location = new Point(20, 120);
                CheckBox selecionar= new CheckBox();
                selecionar.Text = dados.Rows[registros][0].ToString();
                selecionar.Location = new Point(20, 140);
                selecionar.Click += new EventHandler((sender1, e1) => selecionarClick(sender1, e1, idproduto.Text,descricao.Text,preco.Text));
                //adicionar os campos ao painel
                produtos.Controls.Add(idproduto);
                produtos.Controls.Add(foto);
                produtos.Controls.Add(descricao);
                produtos.Controls.Add(preco);
                produtos.Controls.Add(quantidade);
                produtos.Controls.Add(selecionar);
                //adicionar o produto ao painel
                flowLayoutPanel1.Controls.Add(produtos);
                //aterar o proximo regsitro
                y += 100;
                x = 0;
            }
        }
        
private void selecionarClick(object sender, EventArgs e,string Id,string descrico,string preco)
        {
            ItemCarrinho itemCarrinho = new ItemCarrinho();
            itemCarrinho.NomeProduto= descrico;
            itemCarrinho.Quantidade = 1;
            itemCarrinho.PrecoUnitario = Convert.ToDecimal(preco);
            itemCarrinho.ProdutoId = Convert.ToInt32(Id);
            CarrinhoAtual.Add(itemCarrinho);
       

        }
        //retornando uma tabela
        //criar ometodo para obter as informações do BD
        private DataTable obterdados(string sql)
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

        private void BtnComprar_Click(object sender, EventArgs e)
        {
            if (CarrinhoAtual.Any())
            {
                pedido pedidoatual=new pedido(CarrinhoAtual);
                pedidoatual.Show();
            }
        }
    }
}
