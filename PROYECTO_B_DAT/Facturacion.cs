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
    public partial class Facturacion : Form
    {
        SqlConnection conect = new SqlConnection("Data Source=DESKTOP-FUKI3JG;Initial Catalog=Bd_Tienda;Integrated Security=True");
        public Facturacion()
        {
            InitializeComponent();
        }

        public static int cont_fila = 0, acum_fila = 0;
        public static double total;

        DateTime fechaHora =  DateTime.Now;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.Rows.Add(txtFecha.Text, txtID_cliente.Text, txtCodigo.Text.Trim(), txtNP.Text.Trim(), txtPrecio.Text.Trim(), txtCantidad.Text.Trim());

                double importe = Convert.ToDouble(dataGridView1.Rows[cont_fila].Cells[4].Value) * Convert.ToDouble(dataGridView1.Rows[cont_fila].Cells[5].Value);
                dataGridView1.Rows[cont_fila].Cells[6].Value = importe;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error " + x);
            }

            cont_fila++;

            total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDouble(row.Cells[6].Value);
            }


            txtTotales.Text = "$ " + total.ToString();

            txtCodigo.Clear();
            txtNP.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtCodigo.Focus();

           

            // mandar datos a ventas
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    string Id_cliente = Convert.ToString(row.Cells[0].Value);
                    int Id_producto = Convert.ToInt32(row.Cells[1].Value);

                    string nom_product = Convert.ToString(row.Cells[2].Value);
                    string precio = Convert.ToString(row.Cells[3].Value);
                    int cantidad = Convert.ToInt32(row.Cells[4].Value);

                    string tota = Convert.ToString(row.Cells[5].Value);

                    string cmd = string.Format("SP_ventas_productos '{0}','{1}','{2}','{3}','{4}','{5}'",
                    Id_cliente, Id_producto, nom_product, precio, cantidad, tota);

                    DataSet DS = Class1.ejecutar(cmd);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ingrese un dato valido");
                }
            }   


           
        }

        private void btnBuscarF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID_cliente.Text.Trim()) == false)
            {
                try
                {
                    string cmd = string.Format("select * from clientes WHERE Id_cliente='{0}'", txtID_cliente.Text.Trim());

                    DataSet DS = Class1.ejecutar(cmd);

                    txtNombre.Text = DS.Tables[0].Rows[0]["Nomb_cliente"].ToString().Trim();
                    txtCodigo.Focus();
                    txtFecha.Text = fechaHora.ToShortDateString();
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error " + x);
                }
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //selecionar id autocompletar textbox

            if (string.IsNullOrEmpty(txtCodigo.Text.Trim()) == false)
            {
                try
                {
                    string cmd = string.Format("select * from Productos WHERE Id_Productos='{0}'", txtCodigo.Text.Trim());

                    DataSet DS = Class1.ejecutar(cmd);

                    txtNP.Text = DS.Tables[0].Rows[0]["Nom_prod"].ToString().Trim();
                    txtPrecio.Text = DS.Tables[0].Rows[0]["PrecioProd"].ToString().Trim();
                    txtCantidad.Focus();


                }
                catch (Exception x)
                {
                    MessageBox.Show("Error " + x);
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cont_fila > 0)
            {
                total = total - (Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value));
                txtTotales.Text = "$ " + total.ToString();

                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                cont_fila--;




                
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtNP.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
        }

        private void SQL_Click(object sender, EventArgs e)
        {
            conect.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO detalle_venta(Id_detVentas, Id_venta, Nom_Prod, cantidad, fecha, Nomb_cliente)VALUES(" + this.txtID_cliente.Text + "," + this.txtCodigo.Text + ",'" + this.txtNP.Text + "'," + this.txtCantidad.Text + ",'" + this.txtFecha.Text + "','" + this.txtNombre.Text + "')", conect);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Registro guardado con exito.");
            conect.Close();
            /*
            try
            {
                 foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                string Id_cliente = Convert.ToString(row.Cells["id_cliente"].Value);
                int Id_producto = Convert.ToInt32(row.Cells["id_producto"].Value);

                string nom_product = Convert.ToString(row.Cells["nom_product"].Value);
                string precio = Convert.ToString(row.Cells["precio"].Value);
                int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

                string total = Convert.ToString(row.Cells["importe"].Value);

                string cmd = string.Format("SP_ventas_productos '{0}','{1}','{2}','{3}','{4}','{5}'",    
                Id_cliente, Id_producto, nom_product, precio, cantidad, total);

                DataSet DS = Class1.ejecutar(cmd); 
            }

               

            }
            catch (Exception x)
            {
                MessageBox.Show("" + x);
            }  */
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                
                string Id_cliente = Convert.ToString(row.Cells["id_cliente"].Value);
                int Id_producto = Convert.ToInt32(row.Cells["id_producto"].Value);

                string nom_product = Convert.ToString(row.Cells["nom_product"].Value);
                string precio = Convert.ToString(row.Cells["precio"].Value);
                int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);

                string total = Convert.ToString(row.Cells["importe"].Value);
                string Fecha = Convert.ToString(row.Cells["Fecha"].Value);

                string cmd = string.Format("SP_ventas_productos '{0}','{1}','{2}','{3}','{4}','{5}','{6}'",    
                Id_cliente, Id_producto, nom_product, precio, cantidad, total,Fecha);

                DataSet DS = Class1.ejecutar(cmd); 

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConsultasProd_Tablas conPro = new ConsultasProd_Tablas();
            conPro.ShowDialog();
            if (conPro.DialogResult == DialogResult.OK)
            {
                txtCodigo.Text = conPro.dgvProd.Rows[conPro.dgvProd.CurrentRow.Index].Cells[0].Value.ToString();
                txtPrecio.Text = conPro.dgvProd.Rows[conPro.dgvProd.CurrentRow.Index].Cells[3].Value.ToString();
                txtNP.Text = conPro.dgvProd.Rows[conPro.dgvProd.CurrentRow.Index].Cells[2].Value.ToString();
            }
        }

        private void btnDetalleFactura(object sender, EventArgs e)
        {

        }

       

        

       

       

       
    }
}
           
          
        
    

