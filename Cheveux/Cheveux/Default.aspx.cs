using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        List<HomePageFeatures> features = null;

        protected void Page_Load(object sender, EventArgs e)
        {
                Parallel.Invoke(() => loadHomePage(), () => function.sendOGBkngNoti());
        }

        private void loadHomePage()
        {
            #region Redirect
            //rediret to product page or stylist profile with product id
            //check if a redirect has been requested 
            String goTo = Request.QueryString["Goto"];
            if (goTo != null)
            {
                features = null;
                try
                {
                    //get the home page featurs
                    features = handler.GetHomePageFeatures();
                }
                catch (Exception err)
                {
                    function.logAnError("unable to load featurd items from the DB on default.aspx for redirect: " +
                        err);
                }

                if (goTo == "Prod1")
                {
                    if (features[0].ItemID == null)
                    {
                        Response.Redirect("../Cheveux/Products.aspx");
                    }
                    Response.Redirect("ViewProduct.aspx?ProductID=" + features[0].ItemID.ToString());
                }
                else if (goTo == "Prod2")
                {
                    if (features[1].ItemID == null)
                    {
                        Response.Redirect("../Cheveux/Products.aspx");
                    }
                    Response.Redirect("ViewProduct.aspx?ProductID=" + features[1].ItemID.ToString());
                }
                else if (goTo == "Prod3")
                {
                    if (features[2].ItemID == null)
                    {
                        Response.Redirect("../Cheveux/Products.aspx");
                    }
                    Response.Redirect("ViewProduct.aspx?ProductID=" + features[2].ItemID.ToString());
                }
                else if (goTo == "Sty1")
                {
                    if (features[7].ItemID == null)
                    {
                        Response.Redirect("../Cheveux/Stylists.aspx");
                    }
                    Response.Redirect("Profile.aspx?Action=View&empID=" + features[7].ItemID.ToString());
                }
                else if (goTo == "Sty2")
                {
                    if (features[8].ItemID == null)
                    {
                        Response.Redirect("../Cheveux/Stylists.aspx");
                    }
                    Response.Redirect("Profile.aspx?Action=View&empID=" + features[8].ItemID.ToString());
                }
                else if (goTo == "Sty3")
                {
                    if (features[9].ItemID == null)
                    {
                        Response.Redirect("../Cheveux/Stylists.aspx");
                    }
                    Response.Redirect("Profile.aspx?Action=View&empID=" + features[9].ItemID.ToString());
                }
            }

            #endregion

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
                    if (TS.SlotNo == slotNo.Replace(string.Empty, ""))
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

            #region Home Page Features
            features = null;
            try
            {
                //get the home page featurs
                features = handler.GetHomePageFeatures();
            }
            catch (Exception err)
            {
                function.logAnError("unable to load featurd items from the DB on default.aspx: " +
                    err);
            }

            #region Featured hairstyles
            try
            {
                LService1Header.Text = "<a class='btn btn-light' href='ViewProduct.aspx?ProductID=" +
                    features[3].FeatureID.ToString()
                    + "'>" + features[3].Name.ToString() + "</a></font>";
                LService1Description.Text = features[3].description.ToString()
                + " from R" + features[3].price.ToString();

                LService2Header.Text = "<a class='btn btn-light' href='ViewProduct.aspx?ProductID=" +
                    features[4].FeatureID.ToString()
                    + "'>" + features[4].Name.ToString() + "</a></font>";
                LService2Description.Text = features[4].description.ToString()
                + " from R" + features[4].price.ToString();

                LService3Header.Text = "<a class='btn btn-light' href='ViewProduct.aspx?ProductID=" +
                    features[5].FeatureID.ToString()
                    + "'>" + features[5].Name.ToString() + "</a></font>";
                LService3Description.Text = features[5].description.ToString()
                + " from R" + features[5].price.ToString();

                LService4Header.Text = "<a class='btn btn-light' href='ViewProduct.aspx?ProductID=" +
                    features[6].FeatureID.ToString()
                    + "'>" + features[6].Name.ToString() + "</a>";
                LService4Description.Text = features[6].description.ToString()
                + " from R" + features[6].price.ToString();
            }
            catch (Exception err)
            {
                function.logAnError("unable to display featured hairstyls on default.aspx: " +
                    err);
            }
            #endregion

            #region Featured Products
            //Load Products
            try
            {
                phProductImage1.Controls.Add(new LiteralControl
                    ("<img width='650' height='650' src=" + features[0].ImageURL.ToString() + "/>"));
                lProductHeader1.Text = features[0].Name.ToString();
                LProductDescription1.Text = features[0].description.ToString()
                + " from R" + features[0].price.ToString();

                phProductImage2.Controls.Add(new LiteralControl
                    ("<img width='650' height='650' src=" + features[1].ImageURL.ToString() + "/>"));
                lProductHeader2.Text = features[1].Name.ToString();
                LProductDescription2.Text = features[1].description.ToString()
                + " from R" + features[1].price.ToString();

                phProductImage3.Controls.Add(new LiteralControl
                    ("<img width='650' height='650' src=" + features[2].ImageURL.ToString() + "/>"));
                lProductHeader3.Text = features[2].Name.ToString();
                LProductDescription3.Text = features[2].description.ToString()
                + " from R" + features[2].price.ToString();
            }
            catch (Exception err)
            {
                function.logAnError("unable to display featured products on default.aspx: " +
                    err);
            }
            #endregion

            #region Featured Stylists
            //Load Products
            try
            {
                phStylistImage1.Controls.Add(new LiteralControl
                    ("<img width='650' height='650' src=" + features[7].ImageURL.ToString() + "/>"));
                lStylistHeader1.Text = features[7].firstName.ToString();
                lStylistDescription1.Text = "Specializes in "
                    + handler.viewStylistSpecialisation(features[7].ItemID.ToString()).serviceName;

                phStylistImage2.Controls.Add(new LiteralControl
                    ("<img width='650' height='650' src=" + features[8].ImageURL.ToString() + "/>"));
                lStylistHeader2.Text = features[8].firstName.ToString();
                lStylistDescription2.Text = "Specializes in "
                    + handler.viewStylistSpecialisation(features[8].ItemID.ToString()).serviceName;

                phStylistImage3.Controls.Add(new LiteralControl
                    ("<img width='650' height='650' src=" + features[9].ImageURL.ToString() + "/>"));
                lStylistHeader3.Text = features[9].firstName.ToString();
                lStylistDescription3.Text = "Specializes in "
                    + handler.viewStylistSpecialisation(features[9].ItemID.ToString()).serviceName;
            }
            catch (Exception err)
            {
                function.logAnError("unable to display featured stylist on default.aspx: " +
                    err);
            }
            #endregion

            #region Contact Us
            try
            {
                //load contact us jumbotron
                //display the contact info
                //add a table row
                TableRow newRow = new TableRow();
                tblContactUs.Rows.Add(newRow);

                //add the phone number
                TableCell newCell = new TableCell();
                newCell.Text = "<a href='tel:" + features[10].contactNo.ToString() + "'" +
                    "class='btn btn-primary btn-xl js-scroll-trigger'> <span class='glyphicon'>&#9742;</ span >" +
                    " Phone </a>";
                tblContactUs.Rows[0].Cells.Add(newCell);

                //add email address
                newCell = new TableCell();
                newCell.Text = "<a href='mailto:" + features[11].email.ToString() + "'" +
                    "class='btn btn-primary btn-xl js-scroll-trigger'> <span class='glyphicon'>&#128231;</ span >" +
                    " Email </a>";
                tblContactUs.Rows[0].Cells.Add(newCell);

                //add the address
                //get the curent bussines details
                BusinessDetails = handler.getBusinessTable();
                newCell = new TableCell();
                newCell.Text = "<a target='_blank' href='https://www.google.com/maps/dir/?api=1&destination=" +
                    BusinessDetails.AddressLine1.Replace(' ', '+') + "+" +
                    BusinessDetails.AddressLine2.Replace(' ', '+') +
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
            #endregion
            #endregion
        }
    }
}