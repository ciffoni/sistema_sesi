using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sistema.classe
{
    public class ConfiguracaoHelper
    {
        public static void AplicarEstiloCabecalhoPadrao(DataGridView dgv)
        {
            if (dgv == null) return;

            // Desabilita os estilos visuais padrão do Windows para que nossas cores se apliquem
            dgv.EnableHeadersVisualStyles = false;
            /*
             * 
            dvgPedidos.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dvgPedidos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dvgPedidos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dvgPedidos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgPedidos.EnableHeadersVisualStyles = false;
 
             * 
             */
            // Estilo de Fundo do Cabeçalho
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue; // Um tom de cinza-azul escuro
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;        // Texto branco
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12.5F, FontStyle.Bold); // Fonte negrito
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Alinhamento centralizado

            // Linha do cabeçalho
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single; // Borda única
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize; // Altura automática
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Distribui as colunas para preencher o espaço

            // Previne que o usuário adicione ou remova linhas diretamente pelo DGV
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true; // Torna o DGV somente leitura por padrão

            // Configurações visuais adicionais (opcional)
            dgv.BackgroundColor = SystemColors.Control; // Cor de fundo do controle
            dgv.BorderStyle = BorderStyle.None; // Sem borda externa
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Borda horizontal entre as células
        }

        // --- Método para Aplicar Estilo Padrão às Linhas ---
        public static void AplicarEstiloLinhasPadrao(DataGridView dgv)
        {
            if (dgv == null) return;

            // Estilo para linhas alternadas (zebra striping) - melhora a legibilidade
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray; // Um cinza claro para linhas alternadas
            dgv.DefaultCellStyle.BackColor = Color.WhiteSmoke;              // Branco fumaça para linhas pares

            dgv.DefaultCellStyle.ForeColor = Color.Black; // Cor do texto padrão das células
            dgv.DefaultCellStyle.SelectionBackColor = Color.SkyBlue; // Cor de seleção da célula
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black; // Cor do texto na seleção

            // Remove a borda inferior do DataGridView para melhor visual
            dgv.EnableHeadersVisualStyles = false; // Já definido no método de cabeçalho, mas bom ter aqui também
        }

        // --- Método para Aplicar Formatação Condicional de Cores por Status (ex: Pedidos) ---
        // Este método é mais específico e ainda precisaria ser chamado no CellFormatting do seu DGV
        public static void AplicarCoresStatusPedidos(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;

                // Supondo que a coluna com o status se chame "Status"
                // Adapte o nome da coluna se for diferente
                string status = dgv.Rows[e.RowIndex].Cells["Status"].Value?.ToString();

                // Cor de fundo e texto padrão para garantir que a cor seja redefinida se o status mudar
                Color backColor = dgv.DefaultCellStyle.BackColor;
                Color foreColor = dgv.DefaultCellStyle.ForeColor;

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
                        foreColor = Color.DimGray; // Texto mais escuro para diferenciar de 'Cancelado'
                        break;
                    case "Cancelado":
                        backColor = Color.DarkGray;
                        foreColor = Color.White;
                        break;
                        // Adicione mais casos conforme necessário para outros status
                }

                // Aplica a cor de fundo e texto à linha inteira
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = backColor;
                dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = foreColor;

                // Indica que a formatação foi aplicada para esta célula/linha
                e.FormattingApplied = true;
            }
        }

        // Você pode adicionar outros métodos conforme a necessidade:
        // public static void ConfigurarColunas(DataGridView dgv, Dictionary<string, int> columnWidths) { ... }
        // public static void AdicionarColunaBotao(DataGridView dgv, string headerText, string buttonText) { ... }

    }
}
