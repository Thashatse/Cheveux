﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using CryptSharp;

namespace BLL
{
    public class Authentication
    {
        IDBHandler handler = new DBHandler();

        Functions function = new Functions();

        public bool checkForAccountEmail(string emailOrUsername, bool register)
        {
            bool exists = false;
            //check if the account exists and it is a emmail count type
            try
            {
                if (handler.checkForAccountTypeEmail(emailOrUsername) == null)
                {
                    //the use accoun dose not exist
                } else
                    if (handler.checkForAccountTypeEmail(emailOrUsername).AccountType.Replace(" ", string.Empty) == "Email")
                {
                    exists = true;
                }
                else
                    if (handler.checkForAccountTypeEmail(emailOrUsername).AccountType.Replace(" ", string.Empty) == "Google")
                {
                    if (register == true)
                    {
                        exists = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString()+
                    "Error in checkForAccountEmail method of Authentication");
            }
            return exists;
        }

        public string[] AuthenticateEmail(string Email, string password)
        {
            //array data 0 = ID, 1 = User Type, 2 = Name
            string[] cheveuxUserCookieDetails = { "Error", "", "" };
            //check if the account credentials are correct
            try
            {
                if (verifyPass(
                    password, 
                    handler.getPasHash(Email).Password.ToString().Replace(" ", string.Empty)
                    ) == true)
                {
                    USER loginEmail = handler.logInEmail(Email, 
                        handler.getPasHash(Email).Password.ToString().Replace(" ", string.Empty));
                    if (loginEmail == null)
                    {
                        //the password is wrong
                    }
                    else if (loginEmail.UserID != null
                        && loginEmail.UserType.ToString() != null
                        && loginEmail.FirstName != null)
                    {
                        cheveuxUserCookieDetails[0] = loginEmail.UserID.ToString();
                        cheveuxUserCookieDetails[1] = loginEmail.UserType.ToString();
                        cheveuxUserCookieDetails[2] = loginEmail.FirstName.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.ToString() +
                    "Error in checkForAccountEmail method of Authentication");
            }
            return cheveuxUserCookieDetails;
        }

        public string AuthenticateGoogle(string reg)
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
            string accountType = regArray[5];

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
                    function.logAnError("Faild to add new acoount NewUser method of BLL.Authentication class" + e);
                    throw new ApplicationException( ". We are unable to create a new accounmt at this time try again later.");
                }
            }
            catch
            {
                succes = false;
            }

            //return results
            return succes;
        }

        public string GenerateRandomUserID()
        {
            string result;
            do
            {
                int[] id = new int[21];
                Random rn = new Random();
                for (int i = 0; i < id.Length; i++)
                {
                    id[i] = rn.Next(0, 9);
                }
                result = string.Join("", id);
            } while (handler.BLL_CheckForUserType(result) != null);
            return result;
        }
        
        public string generatePassRestCode()
        {
            string result;
            int[] id = new int[9];
            Random rn = new Random();
            for (int i = 0; i < id.Length; i++)
            {
                id[i] = rn.Next(0, 9);
            }
            result = string.Join("", id);
            return result;
        }

        public string generatePassHash(string password)
        {
            return Crypter.Blowfish.Crypt(password);
        }

        private bool verifyPass(string password, string hash)
        {
            return Crypter.CheckPassword(password, hash);
        }
    }
}