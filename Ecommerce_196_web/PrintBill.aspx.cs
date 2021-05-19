using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class PrintBill : System.Web.UI.Page
{
    static int cid;
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

        conn = new SqlConnection(cs);
        da = new SqlDataAdapter("select * from Customer_Master where PhoneNo=@phoneno", conn);
        da.SelectCommand.Parameters.AddWithValue("@phoneno", Request.QueryString["phoneno"].ToString());
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            cid = Convert.ToInt32(ds.Tables[0].Rows[0]["C_Id"]);

            da = new SqlDataAdapter("Cust_Cart", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@cid", cid);
            ds2 = new DataSet();
            da.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds2.Tables[0];
                Repeater1.DataBind();
                lblTotalCost.Text = "Rs."+ds2.Tables[1].Rows[0]["Total_Cost"].ToString()+"/-";
                lblPhoneNo.Text = "Phone Number : "+Request.QueryString["phoneno"].ToString();

                if (Request.QueryString["mode"].ToString() == "PayByWallet")
                {
                    lblMode.Text = "Paid By Wallet";
                }
                else if ((Request.QueryString["mode"].ToString() == "PayByCash"))
                {
                    lblMode.Text = "Pay By Cash";
                }

                SqlCommand cd = new SqlCommand("delete from Temp_Cart where C_Id=@cid", conn);
                cd.Parameters.AddWithValue("@cid", cid);
                conn.Open();
                cd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        Response.Redirect("Home.aspx");

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
}