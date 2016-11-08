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
        string readerTemplate;

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
            readerTemplate = htmlTemplate.ReadToEnd();
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

        public void generate_file()
        {
        }

        public void save_html_fields(object sender, EventArgs e)
        {
            string temporalTemplate = readerTemplate;
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string initPath = Path.GetTempPath() + @"\FQUL";
            saveFileDialog1.InitialDirectory = Path.GetFullPath(initPath);
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "All files (*.*)|*.*|html files (*.html)|*.html";
            saveFileDialog1.FilterIndex = 2;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter streamWriter = new StreamWriter(myStream);
                    TextBox[] list_textBox = FTL.Controls.OfType<TextBox>().ToArray();
                    foreach (TextBox item in list_textBox)
                    {
                        temporalTemplate = temporalTemplate.Replace(item.Name, item.Text);
                    }
                    streamWriter.WriteLine(temporalTemplate);
                    streamWriter.Flush();
                    myStream.Close();
                }
            }
        }
    }
}
