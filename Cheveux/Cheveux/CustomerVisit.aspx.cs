﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using TypeLibrary.ViewModels;
using TypeLibrary.Models;
namespace Cheveux
{
    public partial class CustomerVisit : System.Web.UI.Page
    {
        String test = DateTime.Now.ToString("dddd d MMMM");
        protected void Page_Load(object sender, EventArgs e)
        {
            theDate.InnerHtml = test;
        }

    }
}