﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;

namespace Cheveux
{
    public partial class Default2 : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        BUSINESS BusinessDetails = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region access control
            HttpCookie UserID = Request.Cookies["CheveuxUserID"];
            //send the user to the correct page based on their usertype
            if (UserID != null)
            {
                string userType = UserID["UT"].ToString();
                if ("R" == userType)
                {
                    //Receptionist
                    Response.Redirect("/Receptionist/Receptionist.aspx");
                }
                else if (userType == "M")
                {
                    //Manager
                    Response.Redirect("/Manager/Dashboard.aspx");
                }
                else if (userType == "S")
                {
                    //stylist
                    Response.Redirect("/Stylist/Stylist.aspx");
                }
                else if (userType == "C")
                {
                    //customer
                    //allowed access to this page
                    //Response.Redirect("Default.aspx");

                }
                else
                {
                    function.logAnError("Unknown user type found during Loading of default.aspx: " +
                        UserID["UT"].ToString());
                }
            }
            #endregion

            #region Home Page Info
            //load home page info
            #region Welcom Back
            //welcome back existing users
            String name = Request.QueryString["WB"];
            if (name != null)
            {
                Welcome.Text = "Welcome Back To Cheveux " + name;
            }
            else
            {
                    //welcome new customers
                    name = Request.QueryString["NU"];
                    if (name != null)
                    {
                        Welcome.Text = "Congratulations " + name
                        + "  You Are Now Registered With Cheveux";
                    }
                }
            #endregion

            #region booking Confirimation 
            //confirm booking
            string stylistID = Request.QueryString["Sty"];
            string Date = Request.QueryString["D"];
            string slotNo = Request.QueryString["T"];
            DateTime Time = Convert.ToDateTime("2001/01/01");

            if (stylistID != null
                && Date != null
                && slotNo != null)
            {
                List<SP_GetSlotTimes> TSL = handler.BLL_GetAllTimeSlots();
                foreach (SP_GetSlotTimes TS in TSL)
                {
                    if(TS.SlotNo == slotNo.Replace(string.Empty, ""))
                    {
                        Time = TS.Time;
                    }
                }
                Welcome.Text = "You are now booked to see " 
                    + handler.viewEmployee(stylistID.Replace(string.Empty, "")).firstName 
                    + " at " + Time.ToString("hh:mm") 
                    + " on the " + Convert.ToDateTime(Date).ToString("dd MMM yyyy");
            }
            #endregion

            #region Featured Products
            //Load Products

            #endregion

            //load contact us jumbotron
            try
            {
                //get the curent bussines details
                BusinessDetails = handler.getBusinessTable();
                //display the contact info
                //add a table row
                TableRow newRow = new TableRow();
                tblContactUs.Rows.Add(newRow);

                //add the phone number
                TableCell newCell = new TableCell();
                newCell.Text = "<a href='tel:" + BusinessDetails.Phone.ToString()+"'" +
                    "class='btn btn-primary btn-xl js-scroll-trigger'> <span class='glyphicon'>&#9742;</ span >" +
                    " Phone </a>";
                tblContactUs.Rows[0].Cells.Add(newCell);

                //add email address
                newCell = new TableCell();
                newCell.Text = "<a href='mailto:" + handler.GetUserDetails("112413834414360855751").Email.ToString()+"'"+
                    "class='btn btn-primary btn-xl js-scroll-trigger'> <span class='glyphicon'>&#128231;</ span >" +
                    " Email </a>";
                tblContactUs.Rows[0].Cells.Add(newCell);

                //add the address
                newCell = new TableCell();
                newCell.Text = "<a target='_blank' href='https://www.google.com/maps/dir/?api=1&destination=" +
                    BusinessDetails.AddressLine1.Replace(' ', '+')+"+"+
                    BusinessDetails.AddressLine2.Replace(' ', '+')+
                "'class='btn btn-primary btn-xl js-scroll-trigger'>" +
                "<span class='glyphicon'>&#xe062;</span> Get Directions</a>";
                tblContactUs.Rows[0].Cells.Add(newCell);
                
            }
            catch (Exception err)
            {
                function.logAnError("unable to load contact info on default.aspx: " +
                    err);
            }
            #endregion
        }
    }
}