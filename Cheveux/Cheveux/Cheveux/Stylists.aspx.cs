using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
namespace Cheveux.Cheveux
{
    public partial class Stylists : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        List<SP_AboutStylist> sList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            errorCssStyles();
            diplayStylists();
        }

        public void errorCssStyles()
        {
            errorHeader.Font.Bold = true;
            errorHeader.Font.Underline = true;
            errorHeader.Font.Size = 21;
            errorMessage.Font.Size = 14;
        }

        public void diplayStylists()
        {
            //Edits and reviews+ratings feature to be added
            try
            {
                sList = handler.aboutStylist();

                tblStylists.CssClass = "table table-light table-hover";

                int count = 0;
                foreach(SP_AboutStylist u in sList)
                {
                    TableRow row = new TableRow();
                    tblStylists.Rows.Add(row);
                    TableCell uImage = new TableCell();
                    uImage.Text = "<img src=" + u.UserImage +
                                    " alt='Stylist Picture' " +
                                    "width='200' height='160' /><br/>"
                                +   "<a href='#' target='_blank'>Click to review</a>";
                    uImage.Width = 220;
                    tblStylists.Rows[count].Cells.Add(uImage);
                   

                    TableCell newCell = new TableCell();
                    newCell.Text =  "<h4>" 
                                  + u.StylistName.ToString() + "</h4>"
                                  + "<br/>"
                                  + "<h5>Specialisation: "
                                  + "<a href='ViewProduct.aspx?ProductID="
                                  + u.ServiceID.Replace(" ", string.Empty) + "' target='_blank'>"
                                  + u.Specialisation.ToString() +"</a></h5>"
                                  + "<br/>"
                                  + "<h5>Bio:</h5>"
                                  + "<p>" + u.Bio + "</p>";
                    newCell.Width = 600;
                    tblStylists.Rows[count].Cells.Add(newCell);

                    count++;
                }
            }
            catch (Exception E)
            {
                phStylistsErr.Visible = true;
                errorHeader.Text = "Error displaying stylists.";
                errorMessage.Text = "It seems there is a problem communicating with the database."
                                    + "Please report problem to admin or try again later.";
                function.logAnError(E.ToString());
            }
        }
    }
}