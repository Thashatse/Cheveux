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
        SP_ViewEmployee employee = null;
        SP_ViewStylistSpecialisationAndBio specialisationAndBio = null;
        string userType;
        HttpCookie cookie = null;
        Authentication auth = new Authentication();
        List<SP_GetCustomerBooking> bookingsList = null;
        List<SP_GetBookingServices> bookingServiceList = null;

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
                //show the nav tabs menue only for customers
                if (cookie["UT"] == "C")
                {
                    divTabs.Visible = true;

                    #region  Bookings Funcrions
                    //if the user is loged in diplay upcoming and futer services
                    JumbotronLogedIn.Visible = true;
                    JumbotronLogedOut.Visible = false;

                    //load the users uppcoming bookings in top the upcoming bookins tab
                    displayUpcomingBookings();

                    //load the users past bookings in top the past bookins tab
                    displayPastBookings();
                    #endregion
                }

                #region Profile Functions
                //ask the user to sign in first
                //if the user is loged in diplay the profile container and hide the login container
                JumbotronLogedIn.Visible = true;
                JumbotronLogedOut.Visible = false;
                //check the action
                string action = Request.QueryString["Action"];
                if (action == null)
                {
                    //load user details
                    LogOutBTN.Visible = true;
                    loadUserDetails();
                }
                else if (action == "View")
                {
                    if (Request.QueryString["empID"] != null)
                    {
                        divTabs.Visible = false;
                        //load employee details
                        loadEmpDetails();
                    }
                    else if (Request.QueryString["UserID"] != null)
                    {
                        divTabs.Visible = false;
                        //load user detils
                        loadUserDetails(Request.QueryString["UserID"]);
                    }
                }
                else if (action == "Edit")
                {
                    if (Page.IsPostBack != true)
                    {
                        divTabs.Visible = false;
                        Edit.Visible = true;
                        editUserDetails();
                    }
                    else
                    {
                        try
                        {
                            userDetails = handler.GetUserDetails(cookie["ID"].ToString());
                            commitEdit(sender, e);
                        }
                        catch (ApplicationException Err)
                        {
                            function.logAnError(Err.ToString()
                                + " An error occurred retrieving user details form DB needed to commit edit for user id: " 
                                + cookie["ID"].ToString());
                            Response.Redirect("Error.aspx?Error='An error occurred updating your profile'");
                        }
                    }
                }
                else if (action == "Delete")
                {
                    divTabs.Visible = false;
                    divProfileHeader.Visible = false;
                    divConfirm.Visible = true;
                    deleteUser();
                }
            } else if (cookie == null)
            {
                //if the user is requesting to see a stylis profile
                string action = Request.QueryString["Action"];
                if (action == "View")
                {
                    //diplay the profile container and hide the login container
                    JumbotronLogedIn.Visible = true;
                    JumbotronLogedOut.Visible = false;
                    if(Request.QueryString["empID"] != null)
                    {
                        divTabs.Visible = false;
                        //load employee details
                        loadEmpDetails();
                    } else if (Request.QueryString["UserID"] != null)
                    {
                        divTabs.Visible = false;
                        //load user detils
                        loadUserDetails(Request.QueryString["UserID"]);
                    }
                }
            }
            #endregion
        }

        #region Profile Funcrions
        /*
         * Profile Funcrions
         */
        //load Employee details into the page
        public void loadEmpDetails()
        {
            //check if it is a manager or a a cusomer and display or hide details accordingly
            if(cookie != null)
            {
                userType = cookie["UT"].ToString();
            }
            //check which employee was requested and display there details
            string empID = Request.QueryString["empID"];
            try
            {
                employee = handler.viewEmployee(empID);
                //check if its a stylist and retrevie the stylist specialisation
                if (employee.employeeType.Replace(" ", string.Empty) == "S")
                {
                    specialisationAndBio = handler.viewStylistSpecialisationAndBio(empID);
                    
                }

                //diplay the employee details
                //image
                profileImage.ImageUrl = employee.empImage.ToString();
                //Styslis Name
                profileLable.Text = (employee.firstName.ToString() +" " + employee.lastName.ToString()).ToUpper();
                //details
                TableRow newRow;
                TableCell newCell;
                //track row count
                int rowCount = 0;
                if (specialisationAndBio != null)
                {
                    //add a new row to the table
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //Specialisation Name
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Bio";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = specialisationAndBio.Stylistbio.ToString();
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;

                    //add a new row to the table
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //Specialisation Name
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Specialisation:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + specialisationAndBio.serviceID.ToString().Replace(" ", string.Empty) +
                        "'>" + specialisationAndBio.serviceName + "</a>";
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;

                    //add a new row
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //service description
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Specialisation Description:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + specialisationAndBio.serviceID.ToString().Replace(" ", string.Empty) +
                        "'>"+specialisationAndBio.serviceDescription+ "</a>";
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;
                }

                //the following will onl be displayed to managers & Receptionists
                if (userType == "M" || userType=="R")
                {
                    //add a new row
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //Contact Number
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Phone Number:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href = 'tel:" + employee.phoneNumber.ToString() +
                        "'>"+employee.phoneNumber+" </ a > ";
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;

                    //add a new row
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //Email
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Email:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = "<a href = 'mailto:" + employee.email.ToString() +
                        "'>"+employee.email+" </ a >";
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;
                }

                //the following will onl be displayed to managers
                if (userType == "M")
                {
                    if (employee.addLine1 != ""
                        && employee.addLine2 != "")
                    {
                        //add a new row
                        newRow = new TableRow();
                        newRow.Height = 50;
                        profileTable.Rows.Add(newRow);
                        //Address
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Text = "Address:";
                        newCell.Width = 300;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = employee.addLine1;
                        newCell.Width = 700;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        //increment rowcount
                        rowCount++;
                        //add a new row
                        newRow = new TableRow();
                        newRow.Height = 50;
                        profileTable.Rows.Add(newRow);
                        //Address
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Width = 300;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = employee.addLine2;
                        newCell.Width = 700;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        //increment rowcount
                        rowCount++;
                    }
                    else
                    {
                        //add a new row
                        newRow = new TableRow();
                        newRow.Height = 50;
                        profileTable.Rows.Add(newRow);
                        //Address
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Text = "Address:";
                        newCell.Width = 300;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a href = '/Manager/UpdateEmployee.aspx?" +
                                    "empID=" + employee.UserID.ToString().Replace(" ", string.Empty) +
                                    "' target='_blank'> No Address on record, add one</a>";
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
                    newCell.Text = employee.userName;
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //increment rowcount
                    rowCount++;

                    //add a new row
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //Email
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "User Type:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = function.GetFullEmployeeTypeText(employee.employeeType.ToString()[0]);
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //edit link
                    newCell = new TableCell();
                    newCell.Text =
                         "<a href='/Manager/UpdateEmployee.aspx?" +
                                    "empID=" + employee.UserID.ToString().Replace(" ", string.Empty) +
                                    "'> Edit Employee </a>";

                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);

                    if(employee.employeeType.Replace(" ", string.Empty) == "S")
                    {
                        // if employee is a stylist allow manager to view schedule
                        newCell = new TableCell();
                        newCell.Text =
                             "<a href='/Receptionist/Appointments.aspx?Action=ViewStylistSchedule&" +
                                        "empID=" + employee.UserID.ToString().Replace(" ", string.Empty) +
                                        "'> View Schedule </a>";

                        newCell.Width = 700;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                    }

                    //increment rowcount
                    rowCount++;
                }
            }
            catch (Exception Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving and displayin your emplyee details for employee id: " + empID
                    + " in loadEmployeeDetails() method on Profilepage");
                profileLable.Text = "An error occurred retrieving employee details";
            }
        }

        //loads the users details into the page
        public void loadUserDetails(string userID = null)
        {
            //get the profile details
            try
            {
                if (userID != null)
                {
                    userDetails = handler.GetUserDetails(userID);
                }
                else
                {
                    userDetails = handler.GetUserDetails(cookie["ID"].ToString());
                }
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving your user details for user id: "+ cookie["ID"].ToString());
                profileLable.Text = "An error occurred retrieving user details";
            }

            try
            {
                //dipslay the use profile details
                if (userDetails != null)
            {
                //diplay the user details
                //image
                profileImage.ImageUrl = userDetails.UserImage.ToString();
                //username
                profileLable.Text = (userDetails.FirstName.ToString() +" "+userDetails.LastName.ToString()).ToUpper();
                    //check if its a stylist and retrevie the stylist specialisation and bio
                    if (userDetails.UserType == 'E')
                    {
                        if (handler.getEmployeeType(cookie["ID"].ToString()).Type.ToString().Replace(" ", string.Empty)
                            == "S")
                        {
                            specialisationAndBio = handler.viewStylistSpecialisationAndBio(cookie["ID"].ToString());
                        }
                    }
                    //details
                    //add a new row
                    TableRow newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //track row count
                int rowCount = 0;
                    TableCell newCell = new TableCell();
                    if (specialisationAndBio != null)
                    {
                        //add a new row to the table
                        newRow = new TableRow();
                        newRow.Height = 50;
                        profileTable.Rows.Add(newRow);
                        //Specialisation Name
                       
                        newCell.Font.Bold = true;
                        newCell.Text = "Bio";
                        newCell.Width = 300;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = specialisationAndBio.Stylistbio.ToString();
                        newCell.Width = 700;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        //increment rowcount
                        rowCount++;

                        //add a new row to the table
                        newRow = new TableRow();
                        newRow.Height = 50;
                        profileTable.Rows.Add(newRow);
                        //Specialisation Name
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Text = "Specialisation:";
                        newCell.Width = 300;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + specialisationAndBio.serviceID.ToString().Replace(" ", string.Empty) +
                            "'>" + specialisationAndBio.serviceName + "</a>";
                        newCell.Width = 700;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        //increment rowcount
                        rowCount++;

                        //add a new row
                        newRow = new TableRow();
                        newRow.Height = 50;
                        profileTable.Rows.Add(newRow);
                        //service description
                        newCell = new TableCell();
                        newCell.Font.Bold = true;
                        newCell.Text = "Specialisation Description:";
                        newCell.Width = 300;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        newCell = new TableCell();
                        newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + specialisationAndBio.serviceID.ToString().Replace(" ", string.Empty) +
                            "'>" + specialisationAndBio.serviceDescription + "</a>";
                        newCell.Width = 700;
                        profileTable.Rows[rowCount].Cells.Add(newCell);
                        //increment rowcount
                        rowCount++;
                    }
                    //Contact No.
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Contact No.:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    if (userID == null)
                    {
                        if(userDetails.ContactNo == null || userDetails.ContactNo == "")
                        {
                            newCell.Text = "<a href = 'Profile.aspx?Action=Edit'>Add Contact No.</a>";
                        }
                        else
                        {
                            newCell.Text = userDetails.ContactNo.ToString();
                        }
                    }
                    else
                    {
                        if (userDetails.ContactNo == null)
                        {
                            newCell.Text = "None";
                        }
                        else
                        {
                            newCell.Text = "<a href = 'tel:" + userDetails.ContactNo.ToString() +
                            "'>" + userDetails.ContactNo + " </ a > ";
                        }
                    }
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
                if (userID == null)
                {
                    newCell.Text = userDetails.Email.ToString();
                }
                else
                {
                    newCell.Text = "<a href = 'mailto:" + userDetails.Email.ToString() +
                        "'>" + userDetails.Email + " </ a >";
                }
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;

                    if (userID == null)
                    {
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
                    }

                        //display user type if employee
                        if (userDetails.UserType.ToString() == "E")
                {
                        employee = handler.viewEmployee(cookie["ID"].ToString());
                        if (employee.addLine1 != ""
                        && employee.addLine2 != "")
                        {
                            //add a new row
                            newRow = new TableRow();
                            newRow.Height = 50;
                            profileTable.Rows.Add(newRow);
                            //Address
                            newCell = new TableCell();
                            newCell.Font.Bold = true;
                            newCell.Text = "Address:";
                            newCell.Width = 300;
                            profileTable.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = employee.addLine1;
                            newCell.Width = 700;
                            profileTable.Rows[rowCount].Cells.Add(newCell);
                            //increment rowcount
                            rowCount++;
                            //add a new row
                            newRow = new TableRow();
                            newRow.Height = 50;
                            profileTable.Rows.Add(newRow);
                            //Address
                            newCell = new TableCell();
                            newCell.Font.Bold = true;
                            newCell.Width = 300;
                            profileTable.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = employee.addLine2;
                            newCell.Width = 700;
                            profileTable.Rows[rowCount].Cells.Add(newCell);
                            //increment rowcount
                            rowCount++;
                        }
                        else
                        {
                            //add a new row
                            newRow = new TableRow();
                            newRow.Height = 50;
                            profileTable.Rows.Add(newRow);
                            //Address
                            newCell = new TableCell();
                            newCell.Font.Bold = true;
                            newCell.Text = "Address:";
                            newCell.Width = 300;
                            profileTable.Rows[rowCount].Cells.Add(newCell);
                            newCell = new TableCell();
                            newCell.Text = "No Address on record, ask your manager to add one";
                            newCell.Width = 700;
                            profileTable.Rows[rowCount].Cells.Add(newCell);
                            //increment rowcount
                            rowCount++;
                        }

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

                    if (userID == null)
                    {
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
                cellText = "<a href='Authentication/Accounts.aspx?Action=ChangePass'>Change Password</a> &nbsp; &nbsp; <a href = 'Profile.aspx?Action=Edit'>Edit Profile</a> &nbsp; &nbsp;";

                newCell.Text = cellText;
                newCell.Width = 700;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                //increment rowcount
                rowCount++;
                }
            }
            else
            {
                //if userDetails is empty let the user know an error occoured
                function.logAnError("An empty result was returned from the DB for user id: " + cookie["ID"].ToString()
                    +" in the Profile Page");
                    profileLable.Text = "An error occurred retrieving user details";
                }

            }
            catch (Exception Err)
            {
                function.logAnError("An empty result was returned from the DB for user id: " + cookie["ID"].ToString()
                    + " in the Profile Page" + Err);
                profileLable.Text = "An error occurred retrieving user details";
            }
        }

        //display the edit user view
        public void editUserDetails()
        {
            //get the profile details
            try
            {
                userDetails = handler.GetUserDetails(cookie["ID"].ToString());
                if (cookie["UT"].ToString()[0] == 'S')
                {
                    specialisationAndBio = handler.viewStylistSpecialisationAndBio(cookie["ID"].ToString());
                }
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred retrieving your user details for user id: " + cookie["ID"].ToString());
                Response.Redirect("Error.aspx?Error='An error occurred retrieving your user details'");
            }

            //dipslay the use profile details
            if (userDetails != null)
            {
                if (userDetails.AccountType.Replace(" ", string.Empty) == "Google")
                {
                    //show the edit table
                    editGoogleProfileTable.Visible = true;
                    //dipslay the use profile details
                    if (userDetails != null)
                    {
                        //diplay the user details
                        //image
                        profileImage.ImageUrl = userDetails.UserImage.ToString();
                        //username
                        profileLable.Text = (userDetails.FirstName.ToString() + " " + userDetails.LastName.ToString()).ToUpper();
                        //details
                        //track row count
                        int rowCount = 0;
                        //First name
                        editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "Frist Name:";
                        editGoogleProfileTable.Rows[rowCount].Cells[1].Text = userDetails.FirstName.ToString() + " " + userDetails.LastName.ToString();
                        //increment rowcount
                        rowCount++;

                        //E-mail
                        editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "E-mail:";
                        editGoogleProfileTable.Rows[rowCount].Cells[1].Text = userDetails.Email.ToString();
                        //increment rowcount
                        rowCount++;

                        editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "*The Above Details are managed by Google, " +
                                "<a href ='https://myaccount.google.com/' target='_blank'>Manage Your Google Account</a>";
                        //increment rowcount
                        rowCount++;

                        //Username
                        editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "Username:";
                        userName.Attributes.Add("placeholder", userDetails.UserName.ToString());
                        //increment rowcount
                        rowCount++;

                        //Contact No.
                        editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "Contact No.:";
                        contactNumber.Attributes.Add("placeholder", userDetails.ContactNo.ToString());
                        //increment rowcount
                        rowCount++;

                        //display user type if employee
                        if (userDetails.UserType.ToString() == "E")
                        {
                            //UserType
                            editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "Employee Type:";
                            editGoogleProfileTable.Rows[rowCount].Cells[1].Text = function.GetFullEmployeeTypeText
                                (handler.getEmployeeType(cookie["ID"].ToString()).Type.ToString()[0]);
                            //increment rowcount
                            rowCount++;

                            editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "*Your Employee Type is managed by the salon manager";
                            //increment rowcount
                            rowCount++;

                            //display bio if employee & is a stylist
                            if (handler.getEmployeeType(cookie["ID"].ToString()).Type.ToString()[0] == 'S')
                            {
                                //Stylist Bio
                                editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "Bio:";
                                txtBio.Attributes.Add("placeholder", specialisationAndBio.Stylistbio.ToString());
                                //increment rowcount
                                rowCount++;
                            }
                            
                        }
                        if (cookie["UT"].ToString()[0] != 'S')
                        {
                            txtBio.Visible = false;
                            aGoogleBioHelp.Visible = false;
                        }
                    }
                }
                else if (userDetails.AccountType.Replace(" ", string.Empty) == "Email")
                {
                    //show the edit table
                    editEmailProfileTable.Visible = true;
                    //diplay the user details
                    //image
                    profileImage.ImageUrl = userDetails.UserImage.ToString();
                    //username
                    profileLable.Text = (userDetails.FirstName.ToString() + " " + userDetails.LastName.ToString()).ToUpper();
                    //details
                    //track row count
                    int rowCount = 0;
                    //First name
                    editEmailProfileTable.Rows[rowCount].Cells[0].Text = "Name:";
                    txtName.Attributes.Add("placeholder", userDetails.FirstName.ToString());
                    txtLastName.Attributes.Add("placeholder", userDetails.LastName.ToString());
                    //increment rowcount
                    rowCount++;

                    //E-mail
                    editEmailProfileTable.Rows[rowCount].Cells[0].Text = "E-mail:";
                    txtEmailAddress.Attributes.Add("placeholder", userDetails.Email.ToString());
                    //increment rowcount
                    rowCount++;

                    //Username
                    editEmailProfileTable.Rows[rowCount].Cells[0].Text = "Username:";
                    txtUsername.Attributes.Add("placeholder", userDetails.UserName.ToString());
                    //increment rowcount
                    rowCount++;

                    //Contact No.
                    editEmailProfileTable.Rows[rowCount].Cells[0].Text = "Contact No.:";
                    contactNumber.Attributes.Add("placeholder", userDetails.ContactNo.ToString());
                    //increment rowcount
                    rowCount++;

                    //display user type if employee
                    if (userDetails.UserType.ToString() == "E")
                    {
                        //UserType
                        editEmailProfileTable.Rows[rowCount].Cells[0].Text = "Employee Type:";
                        editEmailProfileTable.Rows[rowCount].Cells[1].Text = function.GetFullEmployeeTypeText
                            (handler.getEmployeeType(cookie["ID"].ToString()).Type.ToString()[0]);
                        //increment rowcount
                        rowCount++;

                        editEmailProfileTable.Rows[rowCount].Cells[0].Text = "*Your Employee Type is managed by the salon manager";
                        //increment rowcount
                        rowCount++;

                        //display bio if employee & is a stylist
                        if (handler.getEmployeeType(cookie["ID"].ToString()).Type.ToString()[0] == 'S')
                        {
                            //Stylist Bio
                            editEmailProfileTable.Rows[rowCount].Cells[0].Text = "Bio:";
                            txtABioEmail.Attributes.Add("placeholder", specialisationAndBio.Stylistbio.ToString());
                            //increment rowcount
                            rowCount++;
                        }
                        else
                        {
                            txtABioEmail.Visible = false;
                        }
                    }
                    if (cookie["UT"].ToString()[0] != 'S')
                    {
                        txtABioEmail.Visible = false;
                        aEmailBioHelp.Visible = false;
                    }
                }
                else
                {
                    function.logAnError("unknown account type for use ID: " + cookie["ID"].ToString()
                     + " on the Profile Page loading edit form");
                    profileLable.Text = "An error occurred retrieving user details, please try again later.";
                }
            }
            else
            {
                //if userDetails is empty let the user know an error occoured
                function.logAnError("An empty result was returned from the DB for user id: " + cookie["ID"].ToString()
                    + " in the Profile Page");
                Response.Redirect("Error.aspx?Error='An error occurred retrieving your user details'");
            }
        }

        //commit edit to DB
        public void commitEdit(object sender, EventArgs e)
        {
            bool check = false;
            USER userUpdate = new USER();
            EMPLOYEE bioUpdate = new EMPLOYEE();
            userUpdate.UserID = cookie["ID"].ToString();

            //if google account type
            if (userDetails.AccountType.Replace(" ", string.Empty) == "Google")
            {
                //if no changes were made
                if (txtUsername.Text == ""
                    && contactNumber.Text == ""
                    && userName.Text == ""
                    && txtBio.Value.ToString() == "")
                {
                    Response.Redirect("Profile.aspx");
                }

                if (auth.checkForAccountEmail(userName.Text.ToString().Replace(" ", string.Empty), true)
                    == false)
                {
                    //get user name
                    if (userName.Text == "" || userName.Text == null)
                {
                    userUpdate.UserName = userDetails.UserName.ToString().Replace(" ", string.Empty);
                }
                else
                {
                    userUpdate.UserName = userName.Text;
                }

                //get contact no.
                if (contactNumber.Text == "" || contactNumber.Text == null)
                {
                    userUpdate.ContactNo = userDetails.ContactNo.ToString().Replace(" ", string.Empty);
                }
                else
                {
                    userUpdate.ContactNo = contactNumber.Text;
                }

                //get stylist bio if stylist
                if (cookie["UT"].ToString()[0] == 'S')
                {
                    bioUpdate.EmployeeID = cookie["ID"].ToString();
                    if (txtBio.Value.ToString() == "" || txtBio.Value.ToString() == null)
                    {
                            specialisationAndBio = handler.viewStylistSpecialisationAndBio(cookie["ID"].ToString());
                            bioUpdate.Bio = specialisationAndBio.Stylistbio.ToString();
                    }
                    else
                    {
                        bioUpdate.Bio = txtBio.Value.ToString();
                    }
                }

                userUpdate.FirstName = userDetails.FirstName.ToString().Replace(" ", string.Empty);
                userUpdate.LastName = userDetails.LastName.ToString().Replace(" ", string.Empty);
                userUpdate.Email = userDetails.Email.ToString().Replace(" ", string.Empty);

                //comit to the db
                try
                {
                    check = handler.updateUser(userUpdate);
                        if (check == true)
                        {
                            check = handler.updateStylistBio(bioUpdate);
                        }
                }
                catch (ApplicationException Err)
                {
                    function.logAnError(Err.ToString()
                        + " An error occurred editing user profile for user id: " + cookie["ID"].ToString());
                    Response.Redirect("Profile.aspx");
                }
                    //if error occours
                    if (check == true)
                    {
                        Response.Redirect("Profile.aspx");
                    }
                    else if (check == false)
                    {
                        editEmailProfileTable.Visible = false;
                        editGoogleProfileTable.Visible = false;
                        JumbotronLogedIn.Visible = false;
                        confirmHeaderPlaceHolder.Text = "<h1> An error occurred updating your user profile </h1>";
                        confirmPlaceHolder.Text = "Please try again later";
                        yes.Visible = false;
                        no.Visible = false;
                        OK.Visible = true;
                    }
                }
            //if username exists
            else if (auth.checkForAccountEmail(userName.Text.ToString().Replace(" ", string.Empty), true)
                == true)
            {
                userName_TextChanged(sender, e);
            }
        }

            //if email acount type
            else if (userDetails.AccountType.Replace(" ", string.Empty) == "Email")
            {
                //if no changes were made
                if (txtUsername.Text == ""
                    && txtContactNumber.Text == ""
                    && txtName.Text == ""
                    && txtLastName.Text == ""
                    && txtEmailAddress.Text == ""
                    && txtABioEmail.Value.ToString() == "")
                {
                    Response.Redirect("Profile.aspx");
                }

                if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                    == false
                    && auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                    == false)
                {
                    //username
                    if (txtUsername.Text == "")
                    {
                        userUpdate.UserName = userDetails.UserName.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.UserName = txtUsername.Text;
                    }

                    //contact no.
                    if (txtContactNumber.Text == "")
                    {
                        userUpdate.ContactNo = userDetails.ContactNo.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.ContactNo = txtContactNumber.Text;
                    }

                    //first name
                    if (txtName.Text == "")
                    {
                        userUpdate.FirstName = userDetails.FirstName.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.FirstName = txtName.Text;
                    }

                    //LastName name
                    if (txtLastName.Text == "")
                    {
                        userUpdate.LastName = userDetails.LastName.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.LastName = txtLastName.Text;
                    }

                    //email address
                    if (txtEmailAddress.Text == "")
                    {
                        userUpdate.Email = userDetails.Email.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.Email = txtEmailAddress.Text;
                    }

                    //get stylist bio if stylist
                    if (cookie["UT"].ToString()[0] == 'S')
                    {
                        bioUpdate.EmployeeID = cookie["ID"].ToString();
                        if (txtABioEmail.Value.ToString() == "" || txtABioEmail.Value.ToString() == null)
                        {
                            specialisationAndBio = handler.viewStylistSpecialisationAndBio(cookie["ID"].ToString());
                            bioUpdate.Bio = specialisationAndBio.Stylistbio.ToString();
                        }
                        else
                        {
                            bioUpdate.Bio = txtABioEmail.Value.ToString();
                        }
                    }

                    //comit updates
                    try
                    {
                        check = handler.updateUser(userUpdate);
                        if (check == true)
                        {
                            check = handler.updateStylistBio(bioUpdate);
                        }
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString()
                            + " An error occurred editing user profile for user id: " + cookie["ID"].ToString());
                    }

                    if (check == true)
                    {
                        Response.Redirect("Profile.aspx");
                    }
                    else if (check == false)
                    {
                        editEmailProfileTable.Visible = false;
                        editGoogleProfileTable.Visible = false;
                        JumbotronLogedIn.Visible = false;
                        confirmHeaderPlaceHolder.Text = "<h1> An error occurred updating your user profile </h1>";
                        confirmPlaceHolder.Text = "Please try again later";
                        yes.Visible = false;
                        no.Visible = false;
                        OK.Visible = true;
                    }
                }
                //if username or email address exists
                else if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                    == true
                    || auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
                {
                    userName_TextChanged(sender, e);
                    txtEmailAddress_TextChanged(sender, e);
                }
            }
        }

        //display the delete user view
        public void deleteUser()
        {
            confirmHeaderPlaceHolder.Text = "Delete Account?";
            confirmPlaceHolder.Text = "Are you sure you would like to delete your Cheveux Account?";
            string confirmation = Request.QueryString["Confirm"];
            if(confirmation == "false")
            {
                Response.Redirect("Profile.aspx");
            }
            else if(confirmation == "true")
            {
                try
                {
                    //deactivate the account
                    bool success = handler.deactivateUser(cookie["ID"].ToString());
                    if(success == false)
                    {
                        function.logAnError("An Error Occouer Deleting user profile ID:" + cookie["ID"].ToString());
                        Response.Redirect("Error.aspx?Error=An error occurred deleting your profile'");
                    }
                    //sign the user out and return them to the home page
                    Response.Redirect("/Authentication/Accounts.aspx?action=Logout");
                }
                catch (ApplicationException err)
                {
                    function.logAnError("An Error Occouer Deleting user profile ID:"+ cookie["ID"].ToString()+" | "+err);
                    Response.Redirect("Error.aspx?Error=An error occurred deleting your profile'");
                }
            }
        }

        //check for existing user names
        protected void userName_TextChanged(object sender, EventArgs e)
        {
            if(userDetails.AccountType.Replace(" ", string.Empty) == "Google")
            {
                if (auth.checkForAccountEmail(userName.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
                {
                    //if the account exists tell the user to try a difreting username
                    LUserNameExistsGoogleAccount.Visible = true;
                    LUserNameExistsGoogleAccount.Text = "Sorry, That Username is taken. Try Another one";
                }
                else
                {
                    LUserNameExistsGoogleAccount.Visible = false;
                }
            }
            else if (userDetails.AccountType.Replace(" ", string.Empty) == "Email")
            {
                if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
                {
                    //if the account exists tell the user to try a difreting username
                    LUsernmaeExistsEmailAccount.Visible = true;
                    LUsernmaeExistsEmailAccount.Text = "Sorry, That Username is taken. Try Another one";
                    LEmailExists.Visible = true;
                    LEmailExists.Text = "Sorry, That Email is taken. Try Another one";
                }
                else
                {
                    LUsernmaeExistsEmailAccount.Visible = false;
                }
            }
        }

        //check for existing email
        protected void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            if (auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                    == true)
            {
                //if the account exists tell the user to try a difreting username
                LEmailExists.Visible = true;
                LEmailExists.Text = "This email address is already registerd";
            }
            else
            {
                LEmailExists.Visible = false;
            }
        }

        protected void no_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx?Action=Delete&Confirm=false");
        }

        protected void OK_Click1(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx?Action=Delete&Confirm=true");
        }
        #endregion

        #region Bookings Funcrions
        /*
         * Bookings Funcrions
         */
        #region Upcoming
        public void displayUpcomingBookings()
        {
            //get the uppcoming bookins from the Data Base
            try
            {
                bookingsList = handler.getCustomerUpcomingBookings(cookie["ID"].ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString());
                upcomingBookingsLable.Text =
                    "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }

            //check if there are upcoming bookings
            if (bookingsList.Count > 0)
            {
                //if there are bookings desplay them
                //create a new row in the uppcoming bookings table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                upcomingBookings.Rows.Add(newRow);
                //create a header row and set cell withs
                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Date";
                newHeaderCell.Width = 200;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Time";
                newHeaderCell.Width = 100;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 200;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Name";
                newHeaderCell.Width = 300;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Price";
                newHeaderCell.Width = 100;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Width = 400;
                upcomingBookings.Rows[0].Cells.Add(newHeaderCell);

                //create a loop to display each result
                //creat a counter to keep track of the current row
                int rowCount = 1;
                foreach (SP_GetCustomerBooking booking in bookingsList)
                {
                    if (booking.bookingDate.Date > DateTime.Now.Date)
                    {
                        addUpcomingBookingToTable(booking, rowCount);
                        rowCount++;
                    }
                    else if (booking.bookingDate.Date == DateTime.Now.Date
                        && booking.bookingStartTime.AddMinutes(20).TimeOfDay >= DateTime.Now.TimeOfDay)
                    {
                        addUpcomingBookingToTable(booking, rowCount);
                        rowCount++;
                    }
                }

                if (rowCount == 1)
                {
                    // if there aren't let the user know
                    upcomingBookingsLable.Text =
                        "<p> No Upcoming Bookings </p>";
                    upcomingBookings.Visible = false;
                }
                else
                {
                    // set the booking copunt
                    upcomingBookingsLable.Text =
                        "<p> "+ (rowCount-1) +" Upcoming Bookings </p>";
                }
            }
            else
            {
                // if there aren't let the user know
                upcomingBookingsLable.Text =
                    "<p> No Upcoming Bookings </p>";
            }
        }

        public void addUpcomingBookingToTable(SP_GetCustomerBooking booking, int rowCount)
        {
            //get the booking services
            try
            {
                bookingServiceList = handler.getBookingServices(booking.bookingID.ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError("Error Loading Booking Services in Profile.aspx addUpcomingBookingToTable Error:"+
                    Err.ToString());
            }

            // create a new row in the results table and set the height
            TableRow newRow = new TableRow();
            newRow.Height = 50;
            upcomingBookings.Rows.Add(newRow);
            //fill the row with the data from the results object
            TableCell newCell = new TableCell();
            newCell.Text = booking.bookingDate.ToString("dd MMM yyyy");
            upcomingBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = booking.bookingStartTime.ToString("HH:mm");
            upcomingBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = "<a href='Profile.aspx?Action=View" +
                            "&empID=" + booking.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                            "'>" + booking.stylistFirstName.ToString() + "</a>";
            upcomingBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            if(bookingServiceList.Count == 1)
            {
                newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>"
                + bookingServiceList[0].ServiceName.ToString() + "</a>";
            }
            else if (bookingServiceList.Count == 2)
            {
                newCell.Text = "<a href='../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                    "'>" + bookingServiceList[0].ServiceName.ToString() +
                    ", " + bookingServiceList[1].ServiceName.ToString() + "</a>";
            }
            else if (bookingServiceList.Count > 2)
            {
                newCell.Text = "<a href='../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                    "'> Multiple </a>";
            }
            upcomingBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            //calculate Price
            double price = 0;
            foreach(SP_GetBookingServices servicePrice in bookingServiceList)
            {
                price += servicePrice.Price;
            }
            newCell.Text = "R " + Math.Round(Convert.ToDouble(price), 2).ToString();
            upcomingBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            //display cancel, edit and print buttons
            newCell.Text =
                "<a href = '../ViewBooking.aspx?Action=Cancel&BookingID=" +
                booking.bookingID.ToString().Replace(" ", string.Empty) +
                "&PreviousPage=Profile.aspx'>Cancel Booking   </a> &nbsp; &nbsp;  " +

                "<button type = 'button' class='btn btn-default'>" +
                "<a href = '../ViewBooking.aspx?Action=Edit&BookingID=" +
                booking.bookingID.ToString().Replace(" ", string.Empty) +
                "'>Edit Booking</a></button>";
            upcomingBookings.Rows[rowCount].Cells.Add(newCell);

        }
        #endregion

        #region Past
        public void displayPastBookings()
        {
            //get the Past bookins from the Data Base
            try
            {
                bookingsList = handler.getCustomerPastBookings(cookie["ID"].ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString() + "\n Getting Past Booking ob Bookings Page");
                pastBookingsLable.Text =
                    "<h2> An Error Occured Communicating With The Data Base, Try Again Later. </h2>";
            }
            //check if there are past bookings
            if (bookingsList.Count > 0)
            {
                //if there are bookings desplay them
                //create a new row in the past bookings table and set the height
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                pastBookings.Rows.Add(newRow);
                //create a header row and set cell withs
                TableHeaderCell newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Date";
                newHeaderCell.Width = 200;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Time";
                newHeaderCell.Width = 100;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Stylist";
                newHeaderCell.Width = 200;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Text = "Service Name";
                newHeaderCell.Width = 300;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);
                newHeaderCell = new TableHeaderCell();
                newHeaderCell.Width = 400;
                pastBookings.Rows[0].Cells.Add(newHeaderCell);

                //create a loop to display each result
                //creat a counter to keep track of the current row
                int rowCount = 1;
                foreach (SP_GetCustomerBooking booking in bookingsList)
                {
                    if (booking.bookingDate.Date < DateTime.Now.Date)
                    {
                        addPastBookingToTable(booking, rowCount);
                        rowCount++;
                    }
                    else if (booking.bookingDate.Date == DateTime.Now.Date
                        && booking.bookingStartTime.AddMinutes(20).TimeOfDay <= DateTime.Now.TimeOfDay)
                    {
                        addPastBookingToTable(booking, rowCount);
                        rowCount++;
                    }
                }
                if (rowCount == 1)
                {
                    // if there aren't let the user know
                    pastBookingsLable.Text =
                        "<p> No Past Bookings </p>";
                    pastBookings.Rows.Clear();
                }
                else
                {
                    // set the booking copunt
                    pastBookingsLable.Text =
                        "<p> " + (rowCount-1) + " Past Bookings </p>";
                }
            }
            else
            {
                // if there aren't let the user know
                pastBookingsLable.Text =
                    "<p> No Past Bookings </p>";
            }
        }

        public void addPastBookingToTable(SP_GetCustomerBooking booking, int rowCount)
        {
            //get the booking services
            try
            {
                bookingServiceList = handler.getBookingServices(booking.bookingID.ToString());
            }
            catch (ApplicationException Err)
            {
                function.logAnError("Error Loading Booking Services in Profile.aspx addUpcomingBookingToTable Error:" +
                    Err.ToString());
            }
            // create a new row in the results table and set the height
            TableRow newRow = new TableRow();
            newRow.Height = 50;
            pastBookings.Rows.Add(newRow);
            //fill the row with the data from the results object
            TableCell newCell = new TableCell();
            newCell.Text = booking.bookingDate.ToString("dd-MM-yyyy");
            pastBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = booking.bookingStartTime.ToString("HH:mm");
            pastBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            newCell.Text = "<a href='Profile.aspx?Action=View" +
                            "&empID=" + booking.stylistEmployeeID.ToString().Replace(" ", string.Empty) +
                            "'>" + booking.stylistFirstName.ToString() + "</a>";
            pastBookings.Rows[rowCount].Cells.Add(newCell);
            newCell = new TableCell();
            if (bookingServiceList.Count == 1)
            {
                newCell.Text = "<a href='ViewProduct.aspx?ProductID=" + bookingServiceList[0].ServiceID.Replace(" ", string.Empty) + "'>"
                + bookingServiceList[0].ServiceName.ToString() + "</a>";
            }
            else if (bookingServiceList.Count == 2)
            {
                newCell.Text = "<a href='../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                    "'>" + bookingServiceList[0].ServiceName.ToString() +
                    ", " + bookingServiceList[1].ServiceName.ToString() + "</a>";
            }
            else if (bookingServiceList.Count > 2)
            {
                newCell.Text = "<a href='../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past'> Multiple </a>";
            }
            pastBookings.Rows[rowCount].Cells.Add(newCell);
            if (booking.arrived.ToString()[0] != 'N')
            {
                newCell = new TableCell();
                newCell.Text =
                    "<button type = 'button' class='btn btn-default'>" +
                    "<a href = '../ViewBooking.aspx?BookingID=" + booking.bookingID.ToString().Replace(" ", string.Empty) +
                    "&BookingType=Past'" +
                    ">View Invoice</a></button>";
                pastBookings.Rows[rowCount].Cells.Add(newCell);
            }
        }
        #endregion
        #endregion
    }
}