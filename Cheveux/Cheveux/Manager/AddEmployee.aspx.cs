using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class AddEmpolyee : System.Web.UI.Page
    {
        Functions function = new Functions();
        IDBHandler handler = new DBHandler();
        USER user;
        EMPLOYEE emp;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenUserID_Click(object sender, EventArgs e)
        {
            txtUserID.Text = GenerateRandomUserID();
        }

        public string GenerateRandomUserID()
        {
            int[] id = new int[21];
            Random rn = new Random();
            for(int i = 0; i < id.Length; i++)
            {
                id[i] = rn.Next(0, 9);
            }
            string result = string.Join("", id);

            return result;
        }
        public byte[] UploadUserImage()
        {
            //not yet functional (NB) 

            HttpPostedFile postedFile = imageUpload.PostedFile;

            //create variable to retrieve the filename and store it in a variable
            string fileName = Path.GetFileName(postedFile.FileName);
            
            //create variable to get the file extension
            string fileExtension = Path.GetExtension(fileName);

            //create a variable to get the file size
            //int fileSize = postedFile.ContentLength;

            byte[] bytes = new byte[1]; 
            if (fileExtension.ToLower() == ".jpg" ||
               fileExtension.ToLower() == ".bnp" ||
               fileExtension.ToLower() == ".gif" ||
               fileExtension.ToLower() == ".png")
            {
                //read the image data that will be inserted into the database

                /*InputStream object is going to return us a stream object that points 
                 *to the uploaded file using which we can read the contents of that file
                 */
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);

                bytes = binaryReader.ReadBytes((int)stream.Length);
                
            }
            else
            {
                //message saying only images (.jpg, .bnp, .gif, .png) can be uploaded 
                Response.Write("<script>alert('Only images (.jpg, .bnp, .gif, .png) can be uploaded.';</script>");
            }
            return bytes;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            byte[] image = UploadUserImage();
            string selectedRadioButton = rblEmpType.SelectedValue;
            try
            {
                user = new USER();
                emp = new EMPLOYEE();

                user.UserID = txtUserID.Text.ToString();
                user.FirstName = txtFirstName.Text.ToString();
                user.LastName = txtLastName.Text.ToString();
                user.UserName = txtUserName.Text.ToString();
                user.Email = txtEmail.Text.ToString();
                user.ContactNo = txtContactNo.Text.ToString();
                user.UserImage = Convert.ToString(image);
                emp.AddressLine1 = txtAddLine1.Text.ToString();
                emp.AddressLine2 = txtAddLine2.Text.ToString();
                emp.Type = selectedRadioButton;

                if (handler.BLL_AddEmployee(user, emp))
                {
                    Response.Write("<script>alert('Successful.');</script>");
                    Server.Transfer("../Manager/Dashboard.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Failed.Try again.');</script>");
                }
            }
            catch(ApplicationException err)
            {
                function.logAnError(err.ToString());
                Response.Redirect("../Error.aspx"); 
            }
        }

    }
}