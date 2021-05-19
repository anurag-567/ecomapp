using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection conn = null;
    SqlDataAdapter da = null;
    DataSet ds, ds1 = null;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        bindgrid();
        if (Session["Id"] == null)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);

        da = new SqlDataAdapter("select  C.CustName,C.PhoneNo, T.T_Id,T.Datetime From Customer_Master C JOIN Transaction_Master T on C.C_Id=T.C_Id where C.CustName=@CustName", conn);
        da.SelectCommand.Parameters.AddWithValue("@CustName", txtsearch.Text.Trim());

        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            
            GridView1.DataBind();
        }
        txtsearch.Text = "";

    }

    public void bindgrid()
    {
        conn = new SqlConnection(cs);
        da = new SqlDataAdapter("select  C.CustName,C.PhoneNo, T.T_Id,T.Datetime From Customer_Master C JOIN Transaction_Master T on C.C_Id=T.C_Id", conn);


        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }


    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid(); 

    }
}