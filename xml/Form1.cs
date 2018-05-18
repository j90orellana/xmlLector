using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xml
{
    public partial class frmxml : Form
    {
        public frmxml()
        {
            InitializeComponent();
        }

        private void btnselec_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos xml|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtruta.Text = openFileDialog1.FileName;
            }
        }
        private void btncargar_Click(object sender, EventArgs e)
        {
            DataTable tablita = new DataTable();
            tablita.Columns.Add("Denominación Moneda");
            tablita.Columns.Add("Nombre Moneda");
            XmlDocument listado = new XmlDocument();
            listado.Load(txtruta.Text);

            XmlNodeList elementos = listado.SelectNodes("moneda");
            foreach (XmlNode item in listado.ChildNodes)
            {
                foreach (XmlNode items in item)
                {
                    DataRow filita = tablita.NewRow();
                    if (items.Name != "moneda")
                    {
                        filita["Denominación Moneda"] = items.Attributes["value"].Value;
                        filita["Nombre Moneda"] = items.InnerText;
                        tablita.Rows.Add(filita);
                    }
                }
            }
            dtgconten.DataSource = tablita;
        }

        private void frmxml_Load(object sender, EventArgs e)
        {
            txtruta.Text = Application.StartupPath + @"\Xmls\MONEDA PAISES.xml";
        }
    }
}
