using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.classe
{
    public class Localizacao
    {
        public static string CurrentLanguage { get; private set; } = "pt-BR"; // Idioma padrão

        public static void SetLanguage(string cultureName)
        {
            // Define a cultura para a thread atual
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            CurrentLanguage = cultureName;

            // Opcional: Se você quiser recarregar todos os formulários abertos para aplicar o novo idioma
            // Isso pode ser complexo. Geralmente, você define o idioma ANTES de abrir o formulário principal.
        }

        // Método para aplicar a cultura a um formulário específico (útil ao recarregar)
        public static void ApplyLanguageToForm(Form form)
        {
            // Cria um gerenciador de recursos para o tipo do formulário
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(form.GetType());

            // Percorre todos os controles do formulário
            foreach (Control control in form.Controls)
            {
                ApplyLanguageToControl(control, resources);
            }
            // Aplica ao próprio formulário
            resources.ApplyResources(form, "$this");
        }

        private static void ApplyLanguageToControl(Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            // Aplica os recursos ao controle
            resources.ApplyResources(control, control.Name);

            // Se o controle tiver sub-controles (ex: Panel, GroupBox), itera sobre eles
            if (control.HasChildren)
            {
                foreach (Control childControl in control.Controls)
                {
                    ApplyLanguageToControl(childControl, resources);
                }
            }
            // Lida com ToolStripMenuItems em ToolStrip (MenuStrip, ToolStrip)
            if (control is ToolStrip toolStrip)
            {
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    ApplyLanguageToToolStripItem(item, resources);
                }
            }
        }

        private static void ApplyLanguageToToolStripItem(ToolStripItem item, System.ComponentModel.ComponentResourceManager resources)
        {
            resources.ApplyResources(item, item.Name);
            if (item is ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
            {
                foreach (ToolStripItem dropDownItem in menuItem.DropDownItems)
                {
                    ApplyLanguageToToolStripItem(dropDownItem, resources);
                }
            }
        }
    }
}
