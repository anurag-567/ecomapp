using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Id"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (Request.QueryString["msg"] == "add")
        {
            lblMessgae.Text = "Added Successfully"; 
        }

        if (Request.QueryString["msg"] == "update")
        {
            lblMessgae.Text = "Updated Successfully";
        }

        if (Request.QueryString["msg"] == "delete")
        {
            lblMessgae.Text = "Deleted Successfully";
        }

    }
    protected void btnAddNewEmployeed_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddNewEmployee.aspx");
    }
}