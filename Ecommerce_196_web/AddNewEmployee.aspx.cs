using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection conn = null;
    SqlDataAdapter da = null;
    DataSet ds = null;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Id"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        conn = new SqlConnection(cs);

        if (!IsPostBack && Request.QueryString["Action"] == "edit")
        {
            string empid = Request.QueryString["Emp_Id"];
            da = new SqlDataAdapter("select * from Employee_Master where Emp_Id=@empid", conn);
            da.SelectCommand.Parameters.AddWithValue("@empid", empid);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                txtEmailId.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                txtEmailId.Enabled = false;
                btnDelete.Enabled = true;
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        char[] array = txtPhoneNo.Text.ToCharArray();

        if (Convert.ToString(array[0]) == "0")
        {
            lblMessage.Text = "Phone Number Should not be started With 0";
        }
        else
        {
            lblMessage.Text = "";

            conn = new SqlConnection(cs);
            if (Request.QueryString["Action"] == "edit")
            {
                string empid = Request.QueryString["Emp_Id"];

                da = new SqlDataAdapter("Update Employee_Master set EmpName=@empname,Address=@address,PhoneNo=@phoneno,EmailId=@emailid,Password=@password where Emp_Id=@empid", conn);
                da.SelectCommand.Parameters.AddWithValue("@empname", txtEmpName.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@emailid", txtEmailId.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@empid", empid);
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Manage_Employees.aspx?msg=update");
            }
            else
            {
                da = new SqlDataAdapter("select * from Employee_Master where EmailId=@emailid;select * from Employee_Master where PhoneNo=@PhoneNo", conn);
                da.SelectCommand.Parameters.AddWithValue("@emailid", txtEmailId.Text);
                da.SelectCommand.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    lblMessage.Text = "Email Id Or Phone Number Already Exists";
                }
                else
                {
                    da = new SqlDataAdapter("insert into Employee_Master values(@empname,@address,@phoneno,@emailid,@password)", conn);
                    da.SelectCommand.Parameters.AddWithValue("@empname", txtEmpName.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@emailid", txtEmailId.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    conn.Open();
                    da.SelectCommand.ExecuteNonQuery();
                    conn.Close();

                    Response.Redirect("Manage_Employees.aspx?msg=add");
                }


            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);
        string empid=Request.QueryString["Emp_Id"];
        da = new SqlDataAdapter("delete from Employee_Master where Emp_Id=@empid",conn);
        da.SelectCommand.Parameters.AddWithValue("@empid",empid);
        conn.Open();
        da.SelectCommand.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Manage_Employees.aspx?msg=delete");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] == "edit")
        {
            txtAddress.Text = "";
            txtEmpName.Text = "";
            txtPhoneNo.Text = "";
            txtPassword.Text = " ";

        }
        else
        {

            txtAddress.Text = "";
            txtEmpName.Text = "";
            txtPhoneNo.Text = "";
            txtEmailId.Text = "";
            txtPassword.Text = " ";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Manage_Employees.aspx");
    }
}