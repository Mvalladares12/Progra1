using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using conexion;

namespace PROYECTO_B_DAT
{
    public partial class Consultar_ClientesTabla : Form
    {
        public Consultar_ClientesTabla()
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

        private void Consultar_ClientesTabla_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenardatos("clientes").Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) == false)
            {
                try
                {
                    DataSet DS;
                    string cmd = "select * from clientes WHERE Nomb_cliente LIKE ('%" + textBox1.Text.Trim() + "%') ";

                    DS = Class1.ejecutar(cmd);
                    dataGridView1.DataSource = DS.Tables[0];
                }
                catch (Exception)
                {
                    MessageBox.Show("eRROR");
                }
            }
        }

       

        
    }
}
