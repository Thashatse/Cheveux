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
        SP_ViewStylistSpecialisation specialisation = null;
        string userType;
        HttpCookie cookie = null;
        Authentication auth = new Authentication();

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
                    LogOutBTN.Visible = true;
                    loadUserDetails();
                }
                else if (action == "View")
                {
                    if (Request.QueryString["empID"] != null)
                    {
                        //load employee details
                        loadEmpDetails();
                    }
                    else if (Request.QueryString["UserID"] != null)
                    {
                        //load user detils
                        loadUserDetails(Request.QueryString["UserID"]);
                    }
                }
                else if (action == "Edit")
                {
                    if (Page.IsPostBack != true)
                    {
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
                        //load employee details
                    loadEmpDetails();
                    } else if (Request.QueryString["UserID"] != null)
                    {
                        //load user detils
                        loadUserDetails(Request.QueryString["UserID"]);
                    }
                }
            }    
        }

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
                    specialisation = handler.viewStylistSpecialisation(empID);
                }

                //diplay the employee details
                //image
                profileImage.ImageUrl = employee.empImage.ToString();
                //Styslis Name
                profileLable.Text = (employee.firstName.ToString() + employee.lastName.ToString()).ToUpper();
                //details
                TableRow newRow;
                TableCell newCell;
                //track row count
                int rowCount = 0;
                if (specialisation != null)
                {
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
                    newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + specialisation.serviceID.ToString().Replace(" ", string.Empty) +
                        "'>" + specialisation.serviceName + "</a>";
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
                    newCell.Text = "<a href = 'ViewProduct.aspx?ProductID=" + specialisation.serviceID.ToString().Replace(" ", string.Empty) +
                        "'>"+specialisation.serviceDescription+ "</a>";
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
                    //increment rowcount
                    rowCount++;

                    //add a new row
                    newRow = new TableRow();
                    newRow.Height = 50;
                    profileTable.Rows.Add(newRow);
                    //Email
                    newCell = new TableCell();
                    newCell.Font.Bold = true;
                    newCell.Text = "Active:";
                    newCell.Width = 300;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    newCell = new TableCell();
                    newCell.Text = function.GetFullActiveTypeText(employee.active.ToString()[0]);
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
                    //edit link
                    newCell = new TableCell();
                    newCell.Text =
                         "<a href = '/Manager/UpdateEmployee.aspx?" +
                                    "empID=" + employee.UserID.ToString().Replace(" ", string.Empty) +
                                    "' target='_blank'> Edit Employee </a>";
                    newCell.Width = 700;
                    profileTable.Rows[rowCount].Cells.Add(newCell);
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
                profileLable.Text = userDetails.UserName.ToString().ToUpper();
                //details
                //add a new row
                TableRow newRow = new TableRow();
                newRow.Height = 50;
                profileTable.Rows.Add(newRow);
                //track row count
                int rowCount = 0;
                //Name
                TableCell newCell = new TableCell();
                newCell.Font.Bold = true;
                newCell.Text = "Name:";
                newCell.Width = 300;
                profileTable.Rows[rowCount].Cells.Add(newCell);
                newCell = new TableCell();
                newCell.Text = userDetails.FirstName.ToString() +" "+userDetails.LastName.ToString();
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
                cellText = "<a href = 'Profile.aspx?Action=Edit'>Edit Profile</a> &nbsp; &nbsp;";
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
                        profileLable.Text = userDetails.UserName.ToString().ToUpper();
                        //details
                        //track row count
                        int rowCount = 0;
                        //First name
                        editGoogleProfileTable.Rows[rowCount].Cells[0].Text = "Frist Name:";
                        editGoogleProfileTable.Rows[rowCount].Cells[1].Text = userDetails.FirstName.ToString() + " " + userDetails.LastName.ToString(); ;
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
                    profileLable.Text = userDetails.UserName.ToString().ToUpper();
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

            if (userDetails.AccountType.Replace(" ", string.Empty) == "Google")
            {
                if ((userName.Text == "" || userName.Text == null)
                        && (contactNumber.Text == "" || contactNumber.Text == null))
                {
                    Response.Redirect("Profile.aspx");
                }

                if (userName.Text == "")
                {
                    USER userUpdate = new USER();
                    userUpdate.UserID = cookie["ID"].ToString();

                    if (userName.Text == "" || userName.Text == null)
                    {
                        userUpdate.UserName = userDetails.UserName.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.UserName = userName.Text;
                    }
                    if (contactNumber.Text == "" || contactNumber.Text == null)
                    {
                        userUpdate.ContactNo = userDetails.ContactNo.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.ContactNo = contactNumber.Text;
                    }

                    userUpdate.FirstName = userDetails.FirstName.ToString().Replace(" ", string.Empty);
                    userUpdate.LastName = userDetails.LastName.ToString().Replace(" ", string.Empty);
                    userUpdate.Email = userDetails.Email.ToString().Replace(" ", string.Empty);

                    try
                    {
                        check = handler.updateUser(userUpdate);
                        Response.Redirect("Profile.aspx");
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString()
                            + " An error occurred editing user profile for user id: " + cookie["ID"].ToString());
                        Response.Redirect("Profile.aspx");
                    }
                }
                else if (auth.checkForAccountEmail(userName.Text.ToString().Replace(" ", string.Empty), true)
                    == false)
                {
                    USER userUpdate = new USER();
                    userUpdate.UserID = cookie["ID"].ToString();

                    if (userName.Text == "" || userName.Text == null)
                    {
                        userUpdate.UserName = userDetails.UserName.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.UserName = userName.Text;
                    }
                    if (contactNumber.Text == "" || contactNumber.Text == null)
                    {
                        userUpdate.ContactNo = userDetails.ContactNo.ToString().Replace(" ", string.Empty);
                    }
                    else
                    {
                        userUpdate.ContactNo = contactNumber.Text;
                    }

                    userUpdate.FirstName = userDetails.FirstName.ToString().Replace(" ", string.Empty);
                    userUpdate.LastName = userDetails.LastName.ToString().Replace(" ", string.Empty);
                    userUpdate.Email = userDetails.Email.ToString().Replace(" ", string.Empty);

                    try
                    {
                        check = handler.updateUser(userUpdate);
                        Response.Redirect("Profile.aspx");
                    }
                    catch (ApplicationException Err)
                    {
                        function.logAnError(Err.ToString()
                            + " An error occurred editing user profile for user id: " + cookie["ID"].ToString());
                        Response.Redirect("Profile.aspx");
                    }
                }
                else
                {
                    userName_TextChanged(sender, e);
                }
            }
            else if (userDetails.AccountType.Replace(" ", string.Empty) == "Email")
            {
                if (txtUsername.Text == ""
                    && txtContactNumber.Text == ""
                    && txtName.Text == ""
                    && txtLastName.Text == ""
                    && txtEmailAddress.Text == "")
                {
                    Response.Redirect("Profile.aspx");
                }

                if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                    == false
                    && auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                    == false)
                {
                    check = updateEmailAccount();
                    if (txtUsername.Text == ""  && txtEmailAddress.Text == "")
                    {
                        check = updateEmailAccount();
                    }
                    else if (auth.checkForAccountEmail(txtUsername.Text.ToString().Replace(" ", string.Empty), true)
                       == false)
                    {
                        check = updateEmailAccount();
                    }
                    else if (txtUsername.Text == "")
                    {
                        check = updateEmailAccount();
                    }
                    else if (auth.checkForAccountEmail(txtEmailAddress.Text.ToString().Replace(" ", string.Empty), true)
                       == false)
                    {
                        check = updateEmailAccount();
                    }
                    else if (txtEmailAddress.Text == "")
                    {
                        check = updateEmailAccount();
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

        private bool updateEmailAccount()
        {
            USER userUpdate = new USER();
            bool check = false;
            userUpdate.UserID = cookie["ID"].ToString();

            if (txtUsername.Text == "")
            {
                userUpdate.UserName = userDetails.UserName.ToString().Replace(" ", string.Empty);
            }
            else
            {
                userUpdate.UserName = txtUsername.Text;
            }

            if (txtContactNumber.Text == "")
            {
                userUpdate.ContactNo = userDetails.ContactNo.ToString().Replace(" ", string.Empty);
            }
            else
            {
                userUpdate.ContactNo = txtContactNumber.Text;
            }

            if (txtName.Text == "")
            {
                userUpdate.FirstName = userDetails.FirstName.ToString().Replace(" ", string.Empty);
            }
            else
            {
                userUpdate.FirstName = txtName.Text;
            }

            if (txtLastName.Text == "")
            {
                userUpdate.LastName = userDetails.LastName.ToString().Replace(" ", string.Empty);
            }
            else
            {
                userUpdate.LastName = txtLastName.Text;
            }

            if (txtEmailAddress.Text == "")
            {
                userUpdate.Email = userDetails.Email.ToString().Replace(" ", string.Empty);
            }
            else
            {
                userUpdate.Email = txtEmailAddress.Text;
            }

            try
            {
                check = handler.updateUser(userUpdate);
            }
            catch (ApplicationException Err)
            {
                function.logAnError(Err.ToString()
                    + " An error occurred editing user profile for user id: " + cookie["ID"].ToString());
            }
            return check;
        }

        //display the delete user view
        public void deleteUser()
        {

        }

        protected void OK_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
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
            Response.Redirect("Profile.aspx?Action=Edit");
        }

        protected void btnSaveGoogle_Click(object sender, EventArgs e)
        {
            commitEdit(sender, e);
        }
    }
}