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
                new SqlParameter("@ID", id)
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

        public SP_AddCustomer AddCustomer(CUSTOMER Cust)
        {
            SP_AddCustomer TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ID", Cust.CustomerID),
            new SqlParameter("@FN", Cust.FirstName),
            new SqlParameter("@LN", Cust.LastName),
            new SqlParameter("@UN", Cust.UserName),
            new SqlParameter("@EM", Cust.Email),
            new SqlParameter("@CN", Cust.ContactNo),
            new SqlParameter("@CI", Cust.CustomerImage)
        };

            using (DataTable table = DBHelper.ParamSelect("SP_AddCustomer",
            CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    TF = new SP_AddCustomer
                    {
                        Result = Convert.ToInt16(row[0])
                    };

                }
                return TF;
            }

        }
    }
}
   
