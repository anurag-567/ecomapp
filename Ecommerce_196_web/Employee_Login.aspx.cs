using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class Employee_Login : System.Web.UI.Page
{
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection conn = null;
    SqlDataAdapter da = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);
        da = new SqlDataAdapter("select * from Employee_Master where EmailId=@emailid And Password=@password", conn);
        da.SelectCommand.Parameters.AddWithValue("@emailid", txtEmailId.Text);
        da.SelectCommand.Parameters.AddWithValue("@password", txtPassword.Text);
        ds = new DataSet();
        da.Fill(ds, "Login");
        if (ds.Tables["Login"].Rows.Count > 0)
        {
            string id = ds.Tables["Login"].Rows[0]["Emp_Id"].ToString();
            Session["Id"] = id;
            Session["Type"] = "Employee";
            Response.Redirect("Home.aspx");
        }
        else
        {
            txtPassword.Text = "";
            txtEmailId.Text = "";
            txtEmailId.Focus();
            lblMessage.Text = "Invalid Email Id Or Password";
        }
    }
}