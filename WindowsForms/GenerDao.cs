using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public static class GenerDao
    {
        public static Gender getById()        {            Gender gender = new Gender();            try            {                SqlDataReader reader;                string query = "SELECT * FROM Sex WHERE ID=1";                SqlCommand cmd = new SqlCommand(query, Connection.ToDatabase());                cmd.CommandTimeout = 10000;
                reader = cmd.ExecuteReader();                int i = reader.FieldCount;                if (reader.HasRows)                    while (reader.Read())                    {
                        gender.Id = (int)reader["ID"];
                        gender.NameEng = reader["NameEng"].ToString();
                        gender.NameKhmer = reader["NameKhmer"].ToString();
                    }                reader.Close();
            }            catch (Exception e)            {                MessageBox.Show(e.ToString());            }            finally            {
                Connection.ToDatabase().Close();            }            return gender;        }
    }
}