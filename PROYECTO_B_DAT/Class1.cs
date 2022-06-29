using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace conexion
{
    public class Class1
    {
        public static DataSet ejecutar(string cmd)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FUKI3JG;Initial Catalog=Bd_Tienda;Integrated Security=True");
            
                conexion.Open();
                DataSet DS = new DataSet();
                SqlDataAdapter DP = new SqlDataAdapter(cmd, conexion);
                DP.Fill(DS);
                conexion.Close();
                return DS;
        }
    }
}
