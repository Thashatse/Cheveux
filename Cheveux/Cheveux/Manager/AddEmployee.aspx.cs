using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;

namespace Cheveux.Manager
{
    public partial class AddEmpolyee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenUserID_Click(object sender, EventArgs e)
        {
            userID.Text = GenerateRandomUserID();
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
    }
}