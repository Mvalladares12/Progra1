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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string codigo = " ";
        
        private void btnAcceder_Click(object sender, EventArgs e)
        {

            try
            {
                string cmd = string.Format("Select * FROM Usuarios WHERE Usuario='{0}' AND Contraseña='{1}'", textBox1.Text.Trim(), textBox2.Text.Trim());
                DataSet DS = Class1 .ejecutar(cmd);


                string cuenta = DS.Tables[0].Rows[0]["Usuario"].ToString().Trim();
                string contra = DS.Tables[0].Rows[0]["Contraseña"].ToString().Trim();
                if (cuenta == textBox1.Text.Trim() && contra == textBox2.Text.Trim())
                {
                    Menu_MDI ventanaMenu = new Menu_MDI();
                    this.Hide();
                    ventanaMenu.Show();
                    //MessageBox.Show("Acceso correcto");



                }
            }
            catch (Exception)
            {
                MessageBox.Show("Datos Incorrectos");
                textBox2.Clear();
                textBox2.Focus();

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
