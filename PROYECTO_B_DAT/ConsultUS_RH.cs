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
    public partial class ConsultUS_RH : Form
    {
        public ConsultUS_RH()
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


        
        private void ConsultUS_RH_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenardatos("Usuarios").Tables[0];
        }
    }
}
