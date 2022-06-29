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
using System.Data.SqlClient;

namespace PROYECTO_B_DAT
{
    public partial class Consulta_Ventas : Form
    {
        Conexion con = new Conexion();
        SqlConnection conectar = new SqlConnection("Data Source=DESKTOP-FUKI3JG;Initial Catalog = Bd_Tienda; Integrated Security = True");
        public Consulta_Ventas()
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

        private void Consulta_Ventas_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenardatos("ventas").Tables[0];
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conectar.Open();
            string SP = "exec SP_BUSCARVENTA " + txtBuscar.Text + "";
            SqlDataAdapter ad = new SqlDataAdapter(SP, conectar);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            SqlCommand comando = new SqlCommand(SP, conectar);
            SqlDataReader lector;
            lector = comando.ExecuteReader();
            conectar.Close();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            con.mostrar("ventas", dataGridView1);
            txtBuscar.Clear();
            txtBuscar.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
