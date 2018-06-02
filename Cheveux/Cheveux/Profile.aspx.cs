using System;
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
    public partial class Profile2 : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        USER userDetails = null;
        HttpCookie cookie = null;

        //set the master page based on the user type
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            //check the cheveux user id cookie for the user
            HttpCookie cookie = Request.Cookies["CheveuxUserID"];
            char userType;
            //check if the cookie is empty or not
            if (cookie != null)
            {
                //store the user Type in a variable and display the appropriate master page for the user
                userType = cookie["UT"].ToString()[0];
                //if customer
                if (userType == 'C')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
                //if receptionist
                else if (userType == 'R')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxReceptionist.Master";
                }
                //if stylist
                else if (userType == 'S')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxStylist.Master";
                }
                //if Manager
                else if (userType == 'M')
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/CheveuxManager.Master";
                }
                //default
                else
                {
                    //set the master page
                    this.MasterPageFile = "~/MasterPages/Cheveux.Master";
                }
            }
            //set the default master page if the cookie is empty
            else
            {
                //set the master page
                this.MasterPageFile = "~/MasterPages/Cheveux.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the user is loged in and display past and futcher bokings
            cookie = Request.Cookies["CheveuxUserID"];
            if (cookie != null)
            {
                //ask the user to sign in first
                //if the user is loged in diplay the profile container and hide the login container
                JumbotronLogedIn.Visible = true;
                JumbotronLogedOut.Visible = false;
                //check the action
                string action = Request.QueryString["Action"];
                if (action == null)
                {
                    //load user details
                    loadUserDetails();
                }
                else if (action == "Edit")
                {
                    editUserDetails();
                }
                else if (action == "Delete")
                {
                    deleteUser();
                }
            }    
        }

        //loads the users details into the page
        public void loadUserDetails()
        {
            //get the profile details
            try
            {
                userDetails = handler.GetUserDetails(cookie["ID"].ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving your user details for user id: "+ cookie["ID"].ToString());
                Response.Redirect("Error.aspx?Error='An error occurred retrieving your user details'");
            }

            //dipslay the use profile details
            if (userDetails != null)
            {
                //diplay the user details
                //image
                profileImage.ImageUrl = userDetails.UserImage.ToString();
                //username
                profileLable.Text = userDetails.UserName.ToString().ToUpper();
                //details
                //add a new row
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //track row count
                int rowCount = 0;
                //First name
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Frist Name:";
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = userDetails.FirstName.ToString();
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;

                //add a new row
                newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //Last name
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Last Name:";
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = userDetails.LastName.ToString();
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;

                //add a new row
                newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //E-mail
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "E-mail:";
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = userDetails.Email.ToString();
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;

                //display user type if employee
                if (userDetails.UserType.ToString() == "E")
                {
                    //add a new row
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //UserType
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Employee Type:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = function.GetFullEmployeeTypeText
                        (handler.getEmployeeType(cookie["ID"].ToString()).Type.ToString()[0]);
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;
                }

                //add a new row
                newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //Username
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Username:";
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = userDetails.UserName.ToString();
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;

                //add a new row
                newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //Contact No.
                newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Contact No.:";
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = userDetails.ContactNo.ToString();
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;

                //add a new row
                newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //Contact No.
                newCell = new TableCell();
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();

                string cellText;
                cellText = "<button type = 'button' class='btn btn-default'>" +
                        "<a href = 'Profile.aspx?Action=Edit'>Edit Profile</a></button>   ";
                if (userDetails.UserType == 'C')
                {
                    cellText += "<button type = 'button' class='btn btn-default'> " +
                        "<a href = 'Bookings.aspx'>View Booking History</a></button>";
                }

                newCell.Text = cellText;
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;
            }
            else
            {
                //if userDetails is empty let the user know an error occoured
                function.logAnError("An empty result was returned from the DB for user id: " + cookie["ID"].ToString()
                    +" in the Profile Page");
                Response.Redirect("Error.aspx?Error='An error occurred retrieving your user details'");
            }
        }

        //display the edit user view
        public void editUserDetails()
        {

        }

        //display the delete user view
        public void deleteUser()
        {

        }

    }
}