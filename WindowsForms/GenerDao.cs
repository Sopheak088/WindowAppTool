﻿using System;
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
        public static Gender getById()
                reader = cmd.ExecuteReader();
                        gender.Id = (int)reader["ID"];
                        gender.NameEng = reader["NameEng"].ToString();
                        gender.NameKhmer = reader["NameKhmer"].ToString();
                    }
            }
                Connection.ToDatabase().Close();
    }
}