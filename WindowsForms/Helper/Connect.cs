using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsForms.Helper
{
    public static class Connect
    {
        public static SqlConnection ToDatabase()
        {
            SqlConnection con = new SqlConnection();
            try
            {
                con = new SqlConnection("Data Source=.; Initial Catalog=DBTEST; Integrated Security=true;");

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return con;
        }

        public static void Close()
        {
            if (ToDatabase().State != ConnectionState.Closed)
            {
                ToDatabase().Close();
            }
        }
    }
}