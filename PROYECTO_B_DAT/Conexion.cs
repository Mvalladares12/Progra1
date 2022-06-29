using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PROYECTO_B_DAT
{
    class Conexion
    {
        public void mostrar(String tabla, DataGridView grid)
        {
            string server = "Data Source=DESKTOP-FUKI3JG;Initial Catalog=Bd_Tienda;Integrated Security=True";
            SqlConnection conectar = new SqlConnection();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable contenedor = new DataTable();
            conectar.ConnectionString = server;
            conectar.Open();
            string query = "SELECT * FROM " + tabla;
            SqlCommand cmd = new SqlCommand(query, conectar);
            try
            {
                cmd.ExecuteNonQuery();
                adapter.SelectCommand = cmd;
                adapter.Fill(contenedor);
                grid.DataSource = contenedor;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error " + ex.ToString());
            }
            conectar.Close();
        }
    }
}
