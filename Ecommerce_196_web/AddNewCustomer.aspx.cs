using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection conn = null;
    SqlDataAdapter da = null;
    DataSet ds = null;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Id"]==null)
        {
            Response.Redirect("Login.aspx");
        }
        conn = new SqlConnection(cs);

        if (!IsPostBack && Request.QueryString["Action"] == "edit")
        {
            string cid = Request.QueryString["C_Id"];
            da = new SqlDataAdapter("select * from Customer_Master where C_Id=@cid", conn);
            da.SelectCommand.Parameters.AddWithValue("@cid", cid);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCustName.Text = ds.Tables[0].Rows[0]["CustName"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                txtEmailId.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                txtBalance.Text = ds.Tables[0].Rows[0]["Balance"].ToString();
                //txtPhoneNo.Enabled = false;
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
                string cid = Request.QueryString["C_Id"];

                da = new SqlDataAdapter("select * from Customer_Master where PhoneNo=@PhoneNo and C_Id<>@C_Id", conn);
                da.SelectCommand.Parameters.AddWithValue("@C_Id", cid);
                da.SelectCommand.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Text = "Phone Number Already Exists";
                }
                else
                {

                    da = new SqlDataAdapter("Update Customer_Master set CustName=@custname,Address=@address,PhoneNo=@phoneno,EmailId=@emailid,Balance=@balance where C_Id=@cid", conn);
                    da.SelectCommand.Parameters.AddWithValue("@custname", txtCustName.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@emailid", txtEmailId.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@balance", txtBalance.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@cid", cid);
                    conn.Open();
                    da.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Manage_Customers.aspx?msg=update");
                }
            }
            else
            {

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[7];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                string password = new String(stringChars);


                da = new SqlDataAdapter("select * from Customer_Master where PhoneNo=@phoneno;select * from Customer_Master where EmailId=@EmailId", conn);
                da.SelectCommand.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text);
                da.SelectCommand.Parameters.AddWithValue("@EmailId", txtEmailId.Text);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    lblMessage.Text = "Phone Number Or Email Id Already Exists";
                }
                else
                {
                    da = new SqlDataAdapter("insert into Customer_Master values(@custname,@address,@phoneno,@emailid,@balance,@password)", conn);
                    da.SelectCommand.Parameters.AddWithValue("@custname", txtCustName.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@emailid", txtEmailId.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@balance", txtBalance.Text.Trim());
                    da.SelectCommand.Parameters.AddWithValue("@password", password);
                    conn.Open();
                    da.SelectCommand.ExecuteNonQuery();
                    conn.Close();


                    try
                    {
                        WebRequest MyRssRequest = WebRequest.Create("http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=chintan&pwd=Passw0rd&to=" + txtPhoneNo.Text + "&sid=WEBSMS&msg=Your Password For tap and shop login is "+password+"&fl=0");
                        WebResponse MyRssResponse = MyRssRequest.GetResponse();
                        Stream MyRssStream = MyRssResponse.GetResponseStream();

                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("my-demo.in");

                        mail.From = new MailAddress("test@my-demo.in");
                        mail.Subject = "New Registration";
                        mail.To.Add(txtEmailId.Text);
                        mail.Body = "You are successfully registered in NFC Shopping. Your Password for " + txtPhoneNo.Text + " is " + password + " .";

                        SmtpServer.Port = 25;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("test@my-demo.in", "Test@123");

                        SmtpServer.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Exception caught.", ex);
                    }

                    Response.Redirect("Manage_Customers.aspx?msg=add");
                }


            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);
        string cid = Request.QueryString["C_Id"];
        da = new SqlDataAdapter("delete from Customer_Master where C_Id=@cid", conn);
        da.SelectCommand.Parameters.AddWithValue("@cid", cid);
        conn.Open();
        da.SelectCommand.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Manage_Customers.aspx?msg=delete");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Action"] == "edit")
        {
            txtAddress.Text = "";
            txtCustName.Text = "";
            txtEmailId.Text = "";
            txtBalance.Text = "";
        }
        else
        {
            txtAddress.Text = "";
            txtCustName.Text = "";
            txtEmailId.Text = "";
            txtBalance.Text = "";
            txtPhoneNo.Text = "";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Manage_Customers.aspx");
    }
}