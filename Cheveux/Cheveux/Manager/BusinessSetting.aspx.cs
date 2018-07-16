﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TypeLibrary.Models;
using TypeLibrary.ViewModels;
using BLL;

namespace Cheveux
{
    public partial class BusinessSetting : System.Web.UI.Page
    {

        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        HttpCookie cookie = null;
        BUSINESS BusinessDetails = null;
        //create bools to track which setings are to be edited
        bool editVatRate = false;
        bool editVatRegNo = false;
        bool editAddress = false;
        bool editPhoneNumber = false;
        bool editWeekDayHours = false;
        bool editWeekEndHours = false;
        bool editPublicHolHours = false;
        bool editLogo = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged out
            cookie = Request.Cookies["CheveuxUserID"];

            if (cookie == null)
            {
                //if the user is not loged in as a manager do not display Bussines setting
            }
            else if (cookie["UT"] != "M")
            {
                Response.Redirect("../Default.aspx");
            }
            else if (cookie["UT"] == "M")
            {
                //if the user is loged in as a manager display Bussines setting
                LogedIn.Visible = true;
                LogedOut.Visible = false;

                try
                {
                    //get the curent bussines details
                    BusinessDetails = handler.getBusinessTable();

                    //check if an edit has been requested
                    string edit = Request.QueryString["EditType"];
                    if (edit == "EVR")
                    {
                        //edit vat rate
                        btnEditvatRate.Text = "Save";
                        editVatRate = true;
                        //hide all other edit butons
                        hideEditBTNs(0);
                    }
                    else if (edit == "EVRN")
                    {
                        //edit vat reg num
                        btnEditvatRegNo.Text = "Save";
                        editVatRegNo = true;
                        //hide all other edit butons
                        hideEditBTNs(1);
                    }
                    else if (edit == "ADD")
                    {
                        //edit address
                        btnEditadd.Text = "Save";
                        editAddress = true;
                        //hide all other edit butons
                        hideEditBTNs(2);
                    }
                    else if (edit == "PN")
                    {
                        //edit phone number
                        btnEditPhoneNum.Text = "Save";
                        editPhoneNumber = true;
                        //hide all other edit butons
                        hideEditBTNs(3);
                    }
                    else if (edit == "WDH")
                    {
                        //edit weekday hours
                        btnEditWDHours.Text = "Save";
                        editWeekDayHours = true;
                        //hide all other edit butons
                        hideEditBTNs(4);
                    }
                    else if (edit == "WEH")
                    {
                        //edit Weekend hours
                        btnEditWEHours.Text = "Save";
                        editWeekEndHours = true;
                        //hide all other edit butons
                        hideEditBTNs(5);
                    }
                    else if (edit == "PHH")
                    {
                        //edit public holiday hours
                        btnEditPHHours.Text = "Save";
                        editPublicHolHours = true;
                        //hide all other edit butons
                        hideEditBTNs(6);
                    }
                    else if (edit == "LOGO")
                    {
                        //edit Logo
                        btnEditLogo.Text = "Save";
                        editLogo = true;
                        //hide all other edit butons
                        hideEditBTNs(7);
                    }

                    //fill the table with the current setings
                    fillTable();
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n getting business data from the db Page_Load on bussines settings");
                    Response.Redirect("Error.aspx?Error='An error occurred when communicating with the Cheveux server'");
                }
            }
        }

        public void hideEditBTNs(int index)
        {
            //give a row index it hides all edit butons exept the one in the given index
            if (index != 0)
            {
                //hide edit btn for vat rate
                btnEditvatRate.Visible = false;

            }
            if (index != 1)
            {
                //hide edit btn for vat reg num
                btnEditvatRegNo.Visible = false;
            }
            if (index != 2)
            {
                //hide edit btn for address
                btnEditadd.Visible = false;
            }
            if (index != 3)
            {
                //hide edit btn for phone number
                btnEditPhoneNum.Visible = false;
            }
            if (index != 4)
            {
                //hide edit btn for weekday hours
                btnEditWDHours.Visible = false;
            }
            if (index != 5)
            {
                //hide edit btn for Weekend hours
                btnEditWEHours.Visible = false;
            }
            if (index != 6)
            {
                //hide edit btn for public holiday hours
                btnEditPHHours.Visible = false;
            }
            if (index != 7)
            {
                //hide edit btn for Logo
                btnEditLogo.Visible = false;
            }
        }

        public void fillTable()
        {
            try
            {
                //check wheather an edit has been requested before displaying the currant value as ethier a placeholder or text
                //vat rate
                if (editVatRate == false)
                {
                    tblBussinesSettings.Rows[0].Cells[1].Text = BusinessDetails.Vat + "%";
                }
                else
                {
                    vatRate.Attributes.Add("placeholder", BusinessDetails.Vat + "%");
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text= "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[0].Cells.Add(cancelLinkCell);
                }

                //vat Reg No
                if (editVatRegNo == false)
                {
                    tblBussinesSettings.Rows[1].Cells[1].Text = BusinessDetails.VatRegNo;
                }
                else
                {
                    vatRegNo.Attributes.Add("placeholder", BusinessDetails.VatRegNo);
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[1].Cells.Add(cancelLinkCell);
                }

                //address
                if (editAddress == false)
                {
                    //address line 1
                    tblBussinesSettings.Rows[2].Cells[1].Text = BusinessDetails.AddressLine1;
                    //ddress line 2
                    tblBussinesSettings.Rows[3].Cells[1].Text = BusinessDetails.AddressLine2;
                }
                else
                {
                    addLineOne.Attributes.Add("placeholder", BusinessDetails.AddressLine1);
                    addLineTwo.Attributes.Add("placeholder", BusinessDetails.AddressLine2);
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[3].Cells.Add(cancelLinkCell);
                }

                //Phone Number
                if (editPhoneNumber == false)
                {
                    tblBussinesSettings.Rows[4].Cells[1].Text = BusinessDetails.Phone;
                }
                else
                {
                    phoneNumber.Attributes.Add("placeholder", BusinessDetails.Phone);
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[4].Cells.Add(cancelLinkCell);
                }

                //Weekend Hours
                if (editWeekDayHours == false)
                {
                    tblBussinesSettings.Rows[5].Cells[1].Text = BusinessDetails.WeekdayStart.ToString("HH:mm")
                    + " - " + BusinessDetails.WeekdayEnd.ToString("HH:mm");
                }
                else
                {
                    wDStart.Attributes.Add("placeholder", BusinessDetails.WeekdayStart.ToString("HH:mm"));
                    wDEnd.Attributes.Add("placeholder", BusinessDetails.WeekdayEnd.ToString("HH:mm"));
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[5].Cells.Add(cancelLinkCell);
                }

                //Weekend Hours
                if (editWeekEndHours == false)
                {
                    tblBussinesSettings.Rows[6].Cells[1].Text = BusinessDetails.WeekendStart.ToString("HH:mm")
                    + " - " + BusinessDetails.WeekendEnd.ToString("HH:mm");
                }
                else
                {
                    wEStart.Attributes.Add("placeholder", BusinessDetails.WeekendStart.ToString("HH:mm"));
                    wEEnd.Attributes.Add("placeholder", BusinessDetails.WeekendEnd.ToString("HH:mm"));
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[6].Cells.Add(cancelLinkCell);
                }

                //public holiday Hours
                if (editPublicHolHours == false)
                {
                    tblBussinesSettings.Rows[7].Cells[1].Text = BusinessDetails.PublicHolStart.ToString("HH:mm")
                    + " - " + BusinessDetails.PublicHolEnd.ToString("HH:mm");
                }
                else
                {
                    pHStart.Attributes.Add("placeholder", BusinessDetails.PublicHolStart.ToString("HH:mm"));
                    pHEnd.Attributes.Add("placeholder", BusinessDetails.PublicHolEnd.ToString("HH:mm"));
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[7].Cells.Add(cancelLinkCell);
                }

                //logo
                if(editLogo == false)
                {
                    
                }
                else
                {
                    // add a Cancel button
                    TableCell cancelLinkCell = new TableCell();
                    cancelLinkCell.Text = "<a href ='BusinessSetting.aspx'> Cancel </a>";
                    tblBussinesSettings.Rows[8].Cells.Add(cancelLinkCell);
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString() + "\n filling the tblBussinesSettings on the business setings page");
                Response.Redirect("Error.aspx?Error='An error displaying the page'");
            }
        }

        protected void btnEnitvatRate_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if(editVatRate == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=EVR");
            }
            else if(editVatRate == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateVatRate(BusinessDetails.BusinessID, int.Parse(vatRate.Text.Substring(0, 2)));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating Vat Rate business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEnitvatRegNo_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editVatRegNo == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=EVRN");
            }
            else if (editVatRegNo == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateVatRegNo(BusinessDetails.BusinessID, vatRegNo.Text);
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating Vat Reg no business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEditadd_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editAddress == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=ADD");
            }
            else if (editAddress == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateAddress(BusinessDetails.BusinessID, addLineOne.Text, addLineTwo.Text);
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating address - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEditPhoneNum_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editPhoneNumber == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=PN");
            }
            else if (editPhoneNumber == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updatePhoneNumber(BusinessDetails.BusinessID, phoneNumber.Text);
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating phone number - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEditWDHours_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editWeekDayHours == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=WDH");
            }
            else if (editWeekDayHours == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateWeekdayHours(BusinessDetails.BusinessID, Convert.ToDateTime(wDStart.Text), Convert.ToDateTime(wDEnd.Text));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating weekday hours - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEditWEHours_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editWeekEndHours == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=WEH");
            }
            else if (editWeekEndHours == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updateWeekendHours(BusinessDetails.BusinessID, Convert.ToDateTime(wEStart.Text), Convert.ToDateTime(wEEnd.Text));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating weekday hours - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEditPHHours_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editPublicHolHours == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=PHH");
            }
            else if (editPublicHolHours == true)
            {
                //save the edit
                try
                {
                    //update the database
                    handler.updatePublicHolidayHours(BusinessDetails.BusinessID, Convert.ToDateTime(pHStart.Text), Convert.ToDateTime(pHEnd.Text));
                    //refresh the page to show the updates
                    Response.Redirect("BusinessSetting.aspx");
                }
                catch (Exception Err)
                {
                    function.logAnError(Err.ToString() + "\n Updating weekday hours - business setings page");
                    Response.Write("<script>alert('An error occurred updating the database try again later');</script>");
                    Response.Redirect("BusinessSetting.aspx");
                }
            }
        }

        protected void btnEditLogo_Click(object sender, EventArgs e)
        {
            //check if the the edit is beening requested or if the edit should be saved
            if (editLogo == false)
            {
                //displaye the text box
                Response.Redirect("BusinessSetting.aspx?EditType=LOGO");
            }
            else if (editLogo == true)
            {
                //save the edit
            }
        }
    }
}