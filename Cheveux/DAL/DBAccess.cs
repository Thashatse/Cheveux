using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DBAccess : IDBAccess
    {
        public SP_CheckForUser CheckForUser(string id)
        {
            SP_CheckForUser TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ID", id),
            };

            using (DataTable table = DBHelper.ParamSelect("SP_CheckForUser",
            CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    TF = new SP_CheckForUser
                    {
                        Exists = Convert.ToInt16(row["Exists"])
                    };

                }
                return TF;
            }

        }
    }
}
   
