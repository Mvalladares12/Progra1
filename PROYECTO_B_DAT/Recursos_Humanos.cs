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
    public partial class Recursos_Humanos : Form
    {
        string server = "Data Source=DESKTOP-FUKI3JG;Initial Catalog = Bd_Tienda; Integrated Security = True";
        SqlConnection cone = new SqlConnection();
        Conexion con = new Conexion();
        public Recursos_Humanos()
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


        private void Recursos_Humanos_Load(object sender, EventArgs e)
        {
            dgvRH.DataSource = llenardatos("Empleados").Tables[0];
            con.mostrar("EMPLEADOS", dgvRH);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultUS_RH ventUsuarios = new ConsultUS_RH();
            ventUsuarios.Show();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            int IDEM = Convert.ToInt32(txtIDEM.Text);
            int Edad = Convert.ToInt32(txtEdad.Text);

            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_AGREGAREMPLEADOS", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_Empleado", IDEM);
            comm.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            comm.Parameters.AddWithValue("@Apellido", txtApellido.Text);
            comm.Parameters.AddWithValue("@Edad", Edad);
            comm.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
            comm.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            comm.Parameters.AddWithValue("@Cargo", txtCargo.Text);

            try
            {
                comm.ExecuteNonQuery();
            }
            catch (FormatException x)
            {
                MessageBox.Show(x.ToString());
            }
            MessageBox.Show("Registro guardado con exito.");
            con.mostrar("EMPLEADOS",dgvRH);
            cone.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_ELIMINAREMPLEADOS", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Nombre", txtNombre.Text);

            if (MessageBox.Show("El empleado "+txtNombre.Text+" se eliminará", "Confirmacion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Se eliminó el registro.");
                }
                catch(SqlException x)
                {
                    MessageBox.Show(x.ToString());
                    throw;
                }
            }
            con.mostrar("EMPLEADOS", dgvRH);
            cone.Close();
        }

        public int filaActual()
        {
            int index = dgvRH.CurrentRow.Index;
            return index;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = dgvRH.Rows[filaActual()].Cells["Nombre"].Value.ToString();
            txtApellido.Text = dgvRH.Rows[filaActual()].Cells["Apellido"].Value.ToString();
            txtEdad.Text = dgvRH.Rows[filaActual()].Cells["Edad"].Value.ToString();
            txtDireccion.Text = dgvRH.Rows[filaActual()].Cells["Direccion"].Value.ToString();
            txtTelefono.Text = dgvRH.Rows[filaActual()].Cells["Telefono"].Value.ToString();
            txtCargo.Text = dgvRH.Rows[filaActual()].Cells["Cargo"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = dgvRH.Rows[filaActual()].Cells["Id_Empleado"].Value.ToString();
            cone.ConnectionString = server;
            cone.Open();
            SqlCommand comm = new SqlCommand("SP_ACTUALIZAREMPLEADOS", cone);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Id_Empleado", id);
            comm.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            comm.Parameters.AddWithValue("@Apellido", txtApellido.Text);
            comm.Parameters.AddWithValue("@Edad", txtEdad.Text);
            comm.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
            comm.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            comm.Parameters.AddWithValue("@Cargo", txtCargo.Text);

            if (MessageBox.Show("¿Desea actualizar estos datos?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Empleado actualizado");
                }
                catch(SqlException x)
                {
                    MessageBox.Show(x.ToString());
                    throw;
                }
                con.mostrar("EMPLEADOS", dgvRH);
                cone.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIDEM.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtCargo.Clear();
        }
    }
}
