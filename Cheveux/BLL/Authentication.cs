using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;


namespace BLL
{
    public class Authentication
    {
        IDBHandler handler = new DBHandler();

        public string Authenticate(string reg)
        {
            //create a variable to store the return value
            string returnVal = "error";

            //unpack uesrdata
            string[] regArray = reg.Split('|');
            string id = regArray[0];
            string email = regArray[1];
            string name = regArray[2];
            string surname = regArray[3];
            string imageurl = regArray[4];

            //check if the user exists
            int exists = -1;
            SP_CheckForUser result = handler.BLL_CheckForUser(id);
            exists = Convert.ToInt16(result.Exists.ToString());

            //return results
            if (exists == 1)
            {
                returnVal = "RegUser";
            }
            else if (exists == 0)
            {
                returnVal = "unRegUser";
            }
            if (exists == -1)
            {
                returnVal = "Error";
            }

            return returnVal;
        }

        public bool NewUser(CUSTOMER cust)
        {
            //return false if customer creation a failure
            bool succes = true;

            //creat new User
            try { handler.BLL_AddCustomer(cust); }
            catch{
                succes = false;
            }

            //return results
            return succes;
        }
    }
}