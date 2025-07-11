using Sistema.classe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class Configuracao : Form
    {
        public Configuracao()
        {
            InitializeComponent();
            // Preenche o ComboBox de idiomas
            cboIdioma.Items.Add(new { Name = "Português (Brasil)", Value = "pt-BR" });
            cboIdioma.Items.Add(new { Name = "English (United States)", Value = "en-US" });
            cboIdioma.DisplayMember = "Name";
            cboIdioma.ValueMember = "Value";

            // Seleciona o idioma atual
            // Encontre o item no ComboBox que corresponde a LocalizationHelper.CurrentLanguage
            foreach (var item in cboIdioma.Items)
            {
                if (((dynamic)item).Value == Localizacao.CurrentLanguage)
                {
                    cboIdioma.SelectedItem = item;
                    break;
                }
            }
        }

        private void Configuracao_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string selectedCulture = ((dynamic)cboIdioma.SelectedItem).Value;

            if (Localizacao.CurrentLanguage != selectedCulture)
            {
                Localizacao.SetLanguage(selectedCulture);

                // Salva a preferência do idioma (ex: nas configurações da aplicação)
                Properties.Settings.Default.Language = selectedCulture;
                Properties.Settings.Default.Save();

                MessageBox.Show("Idioma alterado. A aplicação será reiniciada para aplicar as mudanças.", "Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reiniciar a aplicação para que as mudanças de idioma sejam aplicadas em todos os formulários
            //    Application.Restart();
          //      Environment.Exit(0); // Garante que a aplicação anterior seja encerrada
            }
            else
            {
                MessageBox.Show("Nenhuma alteração de idioma detectada.", "Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
