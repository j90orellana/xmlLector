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
                txtruta.Text = openFileDialog1.FileName;
        }
        DataTable tablita;
        private void btncargar_Click(object sender, EventArgs e)
        {
            tablita = new DataTable();
            tablita.Columns.Add("DenominaciónMoneda");
            tablita.Columns.Add("NombreMoneda");
            XmlDocument listado = new XmlDocument();
            listado.Load(txtruta.Text);
            listado.SelectNodes("moneda");

            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(listado.NameTable);
            //nsmgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            //nsmgr.AddNamespace("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            //nsmgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

            //XmlNodeList listita = listado.SelectNodes("//ds:X509Certificate", nsmgr);
            //foreach (XmlNode item in listita)
            //{
            //    item.InnerText = "pero y que dice";
            //}
            //listado.Save(txtruta.Text);

            foreach (XmlNode item in listado.ChildNodes)
                foreach (XmlNode items in item)
                {
                    DataRow filita = tablita.NewRow();
                    if (items.Name != "moneda")
                        if (items.Attributes["value"] != null)
                        {
                            filita["DenominaciónMoneda"] = items.Attributes["value"].Value;
                            filita["NombreMoneda"] = items.InnerText;
                            tablita.Rows.Add(filita);
                        }
                }
            dtgconten.DataSource = tablita;
        }
        private void frmxml_Load(object sender, EventArgs e)
        {
            txtruta.Text = Application.StartupPath + @"\Xmls\MONEDA PAISES.xml";
        }
        private void bntnbuscar_Click(object sender, EventArgs e)
        {
            if (tablita != null)
            {
                string expresion = $"DenominaciónMoneda='{txtbuscar.Text}'";
                DataView ta = new DataView(tablita);
                ta.Sort = "DenominaciónMoneda asc";
                ta.RowFilter = expresion;
                DataRow[] FILITAS = tablita.Select(expresion);
                if (FILITAS.Length > 0)
                    dtgconten.DataSource = FILITAS.CopyToDataTable();
                else
                {
                    expresion = $"NombreMoneda like '%{txtbuscar.Text}%'";
                    ta = new DataView(tablita);
                    ta.Sort = "DenominaciónMoneda asc";
                    ta.RowFilter = expresion;
                    FILITAS = tablita.Select(expresion);
                    if (FILITAS.Length > 0)
                        dtgconten.DataSource = FILITAS.CopyToDataTable();
                    else
                        dtgconten.DataSource = tablita;

                }
            }
        }
    }
}
