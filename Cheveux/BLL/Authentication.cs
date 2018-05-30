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
            string exists = "Err";
            SP_CheckForUserType result;
            try
            {
                result = handler.BLL_CheckForUserType(id);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString()
                    + ". We are unable to Log you in at this time try again later.");
            }
            //handel the null that will be returned if the user dose not exist
            try
            {
                exists = result.userType.ToString();
            }
            catch (System.NullReferenceException)
            {
                exists = 'F'.ToString();
            }

            //return results to the calling Page
            if (exists == 'C'.ToString() || exists == 'E'.ToString())
            {
                returnVal = exists.ToString();
            }
            else if (exists == 'F'.ToString())
            {
                returnVal = "unRegUser";
            }
            if (exists == "Err")
            {
                returnVal = "Error";
            }

            return returnVal;
        }

        public bool NewUser(USER user)
        {
            //return false if customer creation a failure
            bool succes = true;

            //creat new User
            try
            {
                try
                {
                    handler.BLL_AddUser(user);
                }
                catch (ApplicationException e)
                {
                    throw new ApplicationException(e.ToString()
                        + ". We are unable to create a new accounmt at this time try again later.");
                }
            }
            catch
            {
                succes = false;
            }

            //return results
            return succes;
        }
    }
}