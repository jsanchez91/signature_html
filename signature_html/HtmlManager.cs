using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace signature_html
{
    class HtmlManager
    {
        Stream stream;
        FlowLayoutPanel FTL;

        public HtmlManager(Stream strm, FlowLayoutPanel flowLayoutPanel)
        {
            stream = strm;
            FTL = flowLayoutPanel;
        }

        public void generate_fields()
        {
            /* 
             * Creamos un Stream reader apartir del Stream 
             * que hemos obtenido en el constructor, leemos la 
             * plantilla y utilizamos expresiones regulares (Regex)
             * para obtener y mostrar por pantalla campos a editar
             */
            StreamReader htmlTemplate = new StreamReader(stream);
            string readerTemplate = htmlTemplate.ReadToEnd();
            MatchCollection matches = Regex.Matches(readerTemplate, @"{([^{}]*)}");
            var results = matches.Cast<Match>().Select(m => m.Groups[1].Value).Distinct().ToList();
            foreach (Match match in matches)
            {
                Label labelTag = new Label();
                TextBox textBoxTag = new TextBox();
                textBoxTag.Name = match.Value;
                labelTag.Text = match.Value;
                FTL.Controls.Add(labelTag);
                FTL.Controls.Add(textBoxTag);
            }

            Button button_tags = new Button();
            button_tags.Text = "Generar";
            button_tags.Click += new EventHandler(this.save_html_fields);
            FTL.Controls.Add(button_tags);
        }


        public void save_html_fields(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "html files (*.html)|*.html|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    stream.Close();
                }
            }
        }
    }
}
