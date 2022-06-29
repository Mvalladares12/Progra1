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
    public partial class ConsultasProductos : Form
    {
        SqlConnection conectar =new SqlConnection("Data Source=DESKTOP-FUKI3JG;Initial Catalog = Bd_Tienda; Integrated Security = True");
        string server = "Data Source=DESKTOP-FUKI3JG;Initial Catalog = Bd_Tienda; Integrated Security = True";
        SqlConnection cone = new SqlConnection();
        Conexion con = new Conexion();
        public ConsultasProductos()
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

        private void ConsultasProductos_Load(object sender, EventArgs e)
        {
            dgvCP.DataSource = llenardatos("PRODUCTOS").Tables[0];
            con.mostrar("PRODCUTOS", dgvCP);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            int IDP = Convert.ToInt32(txtIDP.Text);
            int IDC = Convert.ToInt32(txtIDC.Text);

            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_Nuev_Produc", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_productos",IDP);
            comm.Parameters.AddWithValue("@Id_categoria",IDC);
            comm.Parameters.AddWithValue("@Nom_prod",txtNombre.Text);
            comm.Parameters.AddWithValue("@PrecioProd",txtPrecio.Text);

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (FormatException x)
            {
                MessageBox.Show(x.ToString());
            }
            MessageBox.Show("Producto agregado con exito.");
            con.mostrar("PRODUCTOS", dgvCP);
            cone.Close();
        }
        public int filaActual()
        {
            int index = dgvCP.CurrentRow.Index;
            return index;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDP.Text = dgvCP.Rows[filaActual()].Cells["Id_Productos"].Value.ToString();
            txtIDC.Text = dgvCP.Rows[filaActual()].Cells["Id_Categoria"].Value.ToString();
            txtNombre.Text = dgvCP.Rows[filaActual()].Cells["Nom_Prod"].Value.ToString();
            txtPrecio.Text = dgvCP.Rows[filaActual()].Cells["PrecioProd"].Value.ToString();
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = dgvCP.Rows[filaActual()].Cells["Id_Productos"].Value.ToString();
            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("sp_Actualizar_productos", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_productos", id);
            comm.Parameters.AddWithValue("@Id_categoria",txtIDC.Text);
            comm.Parameters.AddWithValue("@Nom_prod",txtNombre.Text);
            comm.Parameters.AddWithValue("@PrecioProd",txtPrecio.Text);

            if (MessageBox.Show("¿Desea actualizar estos datos?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Producto actualizado");
                }
                catch (SqlException x)
                {
                    MessageBox.Show(x.ToString());
                    throw;
                }
                con.mostrar("PRODUCTOS", dgvCP);
                cone.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("EliminarProducto", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_productos", txtIDP.Text);

            if (MessageBox.Show("El producto " + txtIDP.Text + " se eliminará", "Confirmacion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Se eliminó el producto.");
                }
                catch (SqlException x)
                {
                    MessageBox.Show(x.ToString());
                    throw;
                }
            }
            con.mostrar("PRODUCTOS", dgvCP);
            cone.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.mostrar("PRODUCTOS", dgvCP);
            txtIDP.Clear();
            txtIDC.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtBuscar.Clear();
            txtIDP.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            conectar.Open();
            string SP = "exec SP_CONSULTARPROD "+ txtBuscar.Text + "";
            SqlDataAdapter ad = new SqlDataAdapter(SP,conectar);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dgvCP.DataSource = dt;
            SqlCommand comando = new SqlCommand(SP, conectar);
            SqlDataReader lector;
            lector = comando.ExecuteReader();
            conectar.Close();
        }
    }
}
