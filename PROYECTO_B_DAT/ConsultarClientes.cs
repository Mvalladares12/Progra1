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
    public partial class ConsultarClientes : Form
    {
        SqlConnection conectar = new SqlConnection("Data Source=DESKTOP-FUKI3JG;Initial Catalog = Bd_Tienda; Integrated Security = True");
        string server = "Data Source=DESKTOP-FUKI3JG;Initial Catalog = Bd_Tienda; Integrated Security = True";
        SqlConnection cone = new SqlConnection();
        Conexion con = new Conexion();
        public ConsultarClientes()
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

        private void button1_Click(object sender, EventArgs e)
        {
            Consultar_ClientesTabla ventanCliente = new Consultar_ClientesTabla();
            ventanCliente.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            int IDC = Convert.ToInt32(txtIDC.Text);

            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_INSERTARCLIENTE", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_cliente", IDC);
            comm.Parameters.AddWithValue("@Nomb_cliente", txtNombre.Text);
            comm.Parameters.AddWithValue("@Ape_Cliente", txtApellido.Text);

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (FormatException x)
            {
                MessageBox.Show(x.ToString());
            }
            MessageBox.Show("Cliente agregado con exito.");
            con.mostrar("CLIENTES", dgvC);
            cone.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvC.DataSource = llenardatos("CLIENTES").Tables[0];
            con.mostrar("CLIENTES", dgvC);
        }

        public int filaActual()
        {
            int index = dgvC.CurrentRow.Index;
            return index;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtApellido.Text = dgvC.Rows[filaActual()].Cells["Ape_Cliente"].Value.ToString();
            txtIDC.Text = dgvC.Rows[filaActual()].Cells["Id_cliente"].Value.ToString();
            txtNombre.Text = dgvC.Rows[filaActual()].Cells["Nom_cliente"].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = dgvC.Rows[filaActual()].Cells["Id_cliente"].Value.ToString();
            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_ACTUALIZARCLIENTE", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_cliente", id);
            comm.Parameters.AddWithValue("@Ape_Cliente", txtApellido.Text);
            comm.Parameters.AddWithValue("@Nomb_cliente", txtNombre.Text);

            if (MessageBox.Show("¿Desea actualizar estos datos?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Cliente actualizado");
                }
                catch (SqlException x)
                {
                    MessageBox.Show(x.ToString());
                    throw;
                }
                con.mostrar("CLIENTES", dgvC);
                cone.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_ELIMINARCLIENTE", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Nomb_cliente", txtNombre.Text);

            if (MessageBox.Show("El cliente " + txtNombre.Text + " se eliminará", "Confirmacion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Se eliminó el cliente.");
                }
                catch (SqlException x)
                {
                    MessageBox.Show(x.ToString());
                    throw;
                }
            }
            con.mostrar("CLIENTES", dgvC);
            cone.Close();
        }

        private void ConsultarClientes_Load(object sender, EventArgs e)
        {
            dgvC.DataSource = llenardatos("CLIENTES").Tables[0];
            con.mostrar("CLIENTES", dgvC);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            con.mostrar("CLIENTES", dgvC);
            txtIDC.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtIDC.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
