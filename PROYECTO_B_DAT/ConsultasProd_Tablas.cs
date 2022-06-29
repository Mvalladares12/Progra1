using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using conexion;

namespace PROYECTO_B_DAT
{
    public partial class ConsultasProd_Tablas : Form
    {
        public ConsultasProd_Tablas()
        {
            InitializeComponent();
        }

        public DataSet llenardatos(string tabla)
        {
            DataSet DS;

            string cmd = string.Format("Select * from " + tabla);
            DS = Class1.ejecutar(cmd);

            return DS;
        }

        private void ConsultasProd_Tablas_Load(object sender, EventArgs e)
        {
            dgvProd.DataSource = llenardatos("PRODUCTOS").Tables[0];
        }

        /*private void ConsultasProd_Tablas_FormClosed(object sender, FormClosedEventArgs e)
       {

       }

      private void button1_Click(object sender, EventArgs e)
       {
           if (string.IsNullOrEmpty(textBox1.Text.Trim()) == false)
           {
               try
               {
                   DataSet DS;
                   string cmd = "select * from Productos WHERE Nom_Prod LIKE ('%" + textBox1.Text.Trim() + "%') ";

                   DS = Class1.ejecutar(cmd);
                   dgvProd.DataSource = DS.Tables[0];
               }
               catch (Exception)
               {
                   MessageBox.Show("eRROR");
               }
           }
       }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }*/

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dgvProd.Rows.Count == 0)
            {
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;

            }
        }
    }
}
