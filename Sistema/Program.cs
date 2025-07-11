using Sistema.classe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Carrega o idioma salvo nas configurações do usuário (se houver)
            // Por exemplo, você pode salvar a preferência em Properties.Settings.Default.Language
            string savedLanguage = Properties.Settings.Default.Language;
            if (!string.IsNullOrEmpty(savedLanguage))
            {
                Localizacao.SetLanguage(savedLanguage);
            }
            else
            {
                Localizacao.SetLanguage("pt-BR"); // Define um idioma padrão se não houver preferência
            }


            Application.Run(new FrmLogin());
        }
    }
}
