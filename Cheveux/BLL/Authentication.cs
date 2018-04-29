using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis;


namespace BLL
{
    public class Authentication
    {
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
            bool exists = false;

            //return results
            if (exists == true)
            {
                returnVal = "RegUser";
            }
            else if (exists == false)
            {
                returnVal = "unRegUser";
            }

            return returnVal;
        }

        public bool NewUser(string reg)
        {
            //unpack uesrdata
            string[] regArray = reg.Split('|');
            string id = regArray[0];
            string email = regArray[1];
            string name = regArray[2];
            string surname = regArray[3];
            string imageurl = regArray[4];
            string userName = regArray[5];
            string number = regArray[6];

            //creat new User
            bool succes = true;

            //return results
            return succes;
        }
    }
}