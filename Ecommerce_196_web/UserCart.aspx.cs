using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Net;
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    static int cid,Balance,TotalCost;
    static int maxid;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    SqlConnection conn = null;
    SqlDataAdapter da = null;
    DataSet ds, ds2, ds3 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Id"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        lblTotalCost.Text = "";
        lblMessage.Text = "";
        btnPrint.Visible = false;
        btnPayByWallet.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);


        fill();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fill();
    }

    public void fill()
    {
        da = new SqlDataAdapter("select * from Customer_Master where PhoneNo=@phoneno", conn);
        da.SelectCommand.Parameters.AddWithValue("@phoneno", txtPhoneNo.Text.Trim());
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            cid = Convert.ToInt32(ds.Tables[0].Rows[0]["C_Id"]);
            Balance = Convert.ToInt32(ds.Tables[0].Rows[0]["Balance"]);

            da = new SqlDataAdapter("Cust_Cart", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@cid", cid);
            ds2 = new DataSet();
            da.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds2.Tables[0];
                GridView1.DataBind();
                btnPrint.Visible = true;
                lblTotalCost.Text = "Total Cost : Rs." + ds2.Tables[1].Rows[0]["Total_Cost"].ToString();

                TotalCost = Convert.ToInt32(ds2.Tables[1].Rows[0]["Total_Cost"].ToString());

                if (Balance < TotalCost)
                {
                    btnPayByWallet.Visible = false;
                }
                else
                {
                    btnPayByWallet.Visible = true;
                }

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblMessage.Text = "Shopping Cart Details Not Found";
                btnPrint.Visible = false;
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblMessage.Text = "Invalid Phone Number";
            btnPrint.Visible = false;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        conn = new SqlConnection(cs);


        da = new SqlDataAdapter("select Max(T_Id)as T_Id from Transaction_Master", conn);

        conn.Open();
        var topid = da.SelectCommand.ExecuteScalar();
        conn.Close();

        if (topid == DBNull.Value)
        {
            maxid = 1;
        }
        else
        {
            maxid = Convert.ToInt32(topid) + 1;
        }

        da = new SqlDataAdapter("Select_For_TDM", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@cid", cid);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                da = new SqlDataAdapter("insert into Transaction_Details_Master values(@cid,@empid,@productid,@quantity,@totalcost,@datetime,@tid)", conn);
                da.SelectCommand.Parameters.AddWithValue("@cid", cid);
                da.SelectCommand.Parameters.AddWithValue("@empid", Convert.ToInt32(Session["Id"]));
                da.SelectCommand.Parameters.AddWithValue("@productid", Convert.ToInt32(ds.Tables[0].Rows[i]["Product_Id"]));
                da.SelectCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]));
                da.SelectCommand.Parameters.AddWithValue("@totalcost", Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Cost"]));
                da.SelectCommand.Parameters.AddWithValue("@datetime", DateTime.Now.AddMinutes(330).ToString());
                da.SelectCommand.Parameters.AddWithValue("@tid", maxid);
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();


            }


            da = new SqlDataAdapter("insert into Transaction_Master values(@cid,@empid,@DateTime)", conn);
            da.SelectCommand.Parameters.AddWithValue("@cid", cid);
            da.SelectCommand.Parameters.AddWithValue("@empid", Convert.ToInt32(Session["Id"]));
            da.SelectCommand.Parameters.AddWithValue("@DateTime", DateTime.Now.AddMinutes(330).ToString());
            conn.Open();
            da.SelectCommand.ExecuteNonQuery();
            conn.Close();
            sms(txtPhoneNo.Text, TotalCost);
            Response.Redirect("PrintBill.aspx?phoneno=" + txtPhoneNo.Text + "&mode=PayByCash");
        }
    
    }
    public void sms(string ph,int cost)
    {
        WebRequest MyRssRequest = WebRequest.Create("http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=chintan&pwd=Passw0rd&to=" +ph + "&sid=WEBSMS&msg=Transaction Successful and Your Total cost is "+cost+"&fl=0");
        WebResponse MyRssResponse = MyRssRequest.GetResponse();
        Stream MyRssStream = MyRssResponse.GetResponseStream();
    }

    protected void btnPayByWallet_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);


        da = new SqlDataAdapter("select Max(T_Id)as T_Id from Transaction_Master", conn);

        conn.Open();
        var topid = da.SelectCommand.ExecuteScalar();
        conn.Close();

        if (topid == DBNull.Value)
        {
            maxid = 1;
        }
        else
        {
            maxid = Convert.ToInt32(topid) + 1;
        }

        da = new SqlDataAdapter("Select_For_TDM", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@cid", cid);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                da = new SqlDataAdapter("insert into Transaction_Details_Master values(@cid,@empid,@productid,@quantity,@totalcost,@datetime,@tid)", conn);
                da.SelectCommand.Parameters.AddWithValue("@cid", cid);
                da.SelectCommand.Parameters.AddWithValue("@empid", Convert.ToInt32(Session["Id"]));
                da.SelectCommand.Parameters.AddWithValue("@productid", Convert.ToInt32(ds.Tables[0].Rows[i]["Product_Id"]));
                da.SelectCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]));
                da.SelectCommand.Parameters.AddWithValue("@totalcost", Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Cost"]));
                da.SelectCommand.Parameters.AddWithValue("@datetime", DateTime.Now.AddMinutes(330).ToString());
                da.SelectCommand.Parameters.AddWithValue("@tid", maxid);
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();


            }


            int newbalace = Balance - TotalCost;

            da = new SqlDataAdapter("Update Customer_Master set Balance=@balance where C_Id=@C_Id", conn);
            da.SelectCommand.Parameters.AddWithValue("@C_Id", cid);
            da.SelectCommand.Parameters.AddWithValue("@balance", newbalace);
            conn.Open();
            da.SelectCommand.ExecuteNonQuery();
            conn.Close();

            da = new SqlDataAdapter("insert into Transaction_Master values(@cid,@empid,@datetime)", conn);
            da.SelectCommand.Parameters.AddWithValue("@cid", cid);
            da.SelectCommand.Parameters.AddWithValue("@empid", Convert.ToInt32(Session["Id"]));
            da.SelectCommand.Parameters.AddWithValue("@datetime", DateTime.Now.AddMinutes(330).ToString());
            conn.Open();
            da.SelectCommand.ExecuteNonQuery();
            conn.Close();
            sms(txtPhoneNo.Text, TotalCost);
            Response.Redirect("PrintBill.aspx?phoneno=" + txtPhoneNo.Text+"&mode=PayByWallet");

        }
    }
}