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

namespace signature_html
{
    public partial class Form1 : Form
    {
        HtmlManager objeto;
        public Form1()
        {
            InitializeComponent();
        }

        private TextBox texto = new TextBox();
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void abrirPlantillaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*|html files (*.html)|*.html";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            objeto = new HtmlManager(myStream, flowLayoutPanel1);
                            objeto.generate_fields();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ayuda ayuda = new Ayuda();
            ayuda.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acercaDe = new AcercaDe();
            acercaDe.Show();
        }
    }
}
