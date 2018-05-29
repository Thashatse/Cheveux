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
        public SP_CheckForUserType CheckForUserType(string id)
        {
            SP_CheckForUserType TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ID", id)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_CheckForUserType",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new SP_CheckForUserType
                        {
                            userType = Convert.ToChar(row[0])
                        };

                    }
                    return TF;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public SP_AddUserGoogleAuth AddUser(USER User)
        {
            SP_AddUserGoogleAuth TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
            new SqlParameter("@ID", User.UserID),
            new SqlParameter("@FN", User.FirstName),
            new SqlParameter("@LN", User.LastName),
            new SqlParameter("@UN", User.UserName),
            new SqlParameter("@EM", User.Email),
            new SqlParameter("@CN", User.ContactNo),
            new SqlParameter("@UI", User.UserImage)
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_AddUserGoogleAuth",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new SP_AddUserGoogleAuth
                        {
                            Result = Convert.ToInt16(row[0])
                        };

                    }
                    return TF;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }

        public List<SP_ProductSearchByTerm> UniversalSearch(string searchTerm)
    {
        List<SP_ProductSearchByTerm> SearchResults = new List<SP_ProductSearchByTerm>();
        SqlParameter[] pars = new SqlParameter[]
        {
                new SqlParameter("@searchTerm", searchTerm)
        };

        try
        {
            using (DataTable table = DBHelper.ParamSelect("SP_ProductSearchByTerm",
        CommandType.StoredProcedure, pars))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        SP_ProductSearchByTerm result = new SP_ProductSearchByTerm
                        {
                            Name = row["Name"].ToString(),
                            ProductDescription = row["ProductDescription"].ToString(),
                            Price = row["Price"].ToString(),
                            ProductType = row["ProductType(T/A/S)"].ToString()[0],
                            ProductID = row["ProductID"].ToString()
                        };
                        SearchResults.Add(result);
                    }
                }
                return SearchResults;
            }
        }
        catch (ApplicationException e)
        {
            throw new ApplicationException(e.ToString());
        }
    }

        public USER GetUserDetails(string ID)
        {
            USER TF = null;
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@ID", ID)
            };

            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetUserDetails",
            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow row = table.Rows[0];
                        TF = new USER
                        {
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            UserName = row["UserName"].ToString(),
                            Email = row["Email"].ToString(),
                            ContactNo = row["ContactNo"].ToString(),
                            UserType = Convert.ToChar(row["UserType"]),
                            UserImage = row["UserImage"].ToString()
                        };

                    }
                    return TF;
                }
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString());
            }
        }
        public List<SP_GetEmpNames> GetEmpNames()
        {
            List<SP_GetEmpNames> list = new List<SP_GetEmpNames>();
                using (DataTable table = DBHelper.Select("SP_GetEmpNames", CommandType.StoredProcedure))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SP_GetEmpNames emp = new SP_GetEmpNames();
                            emp.EmployeeID = row["EmployeeID"].ToString();
                            emp.Name = row["Name"].ToString();
                            list.Add(emp);
                        }
                    }
                }
                return list;
        }
        public List<SP_GetEmpAgenda> GetEmpAgenda(string employeeID)
        {
            SP_GetEmpAgenda emp = null;
            List<SP_GetEmpAgenda> agenda = new List<SP_GetEmpAgenda>();
            SqlParameter[] pars = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeID),
            };
            try
            {
                using (DataTable table = DBHelper.ParamSelect("SP_GetEmpAgenda",
                                            CommandType.StoredProcedure, pars))
                {
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            emp = new SP_GetEmpAgenda();
                            emp.StartTime = TimeSpan.Parse((row["StartTime"]).ToString());
                            emp.EndTime = TimeSpan.Parse((row["EndTime"]).ToString());
                            emp.CustomerFName = Convert.ToString(row["CustomerFName"]);
                            emp.EmpFName = Convert.ToString(row["EmpFName"]);
                            emp.ServiceName = Convert.ToString(row["ServiceName"]);
                            emp.Arrived = Convert.ToChar(row["Arrived"]);
                            agenda.Add(emp);
                        }
                    }
                }
                return agenda;
            }
            catch(ApplicationException err)
            {
                throw new ApplicationException(err.ToString());
            }
        }
    }
}
   
