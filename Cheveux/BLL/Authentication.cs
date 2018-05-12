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
            char exists = 'E';
            SP_CheckForUserType result;
            try
            {
                result = handler.BLL_CheckForUserType(id);
            }
            catch (ApplicationException e)
            {
                throw new ApplicationException(e.ToString()
                    +". We are unable to Log you in at this time try again later.");
            }

            //handel the null that will be returned if the user dose not exist
            try
            {
                exists = result.userType;
            }
            catch (System.NullReferenceException)
            {
                exists = 'F';
            }

            //return results to the calling Page
            if (exists == 'C' || exists == 'E')
            {
                returnVal = exists.ToString();
            }
            else if (exists == 'F')
            {
                returnVal = "unRegUser";
            }
            if (exists == 'E')
            {
                returnVal = "Error";
            }

            return returnVal;
        }

        public bool NewUser(User user)
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
                        +". We are unable to create a new accounmt at this time try again later.");
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