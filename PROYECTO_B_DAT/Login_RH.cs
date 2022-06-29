using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_B_DAT
{
    public partial class Login_RH : Form
    {
        public Login_RH()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text=="tienda" && textBox2.Text=="123"){

                    Recursos_Humanos ventRh = new Recursos_Humanos();
                    ventRh.Show();
                    this.Hide();

                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Datos Incorrectos");
                textBox2.Clear();
                textBox2.Focus();

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
