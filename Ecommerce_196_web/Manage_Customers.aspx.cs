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
    DataSet ds = null;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        conn = new SqlConnection(cs);
        bindgrid();

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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand ="select * from Customer_Master where CustName=@CustName";
        da = new SqlDataAdapter(SqlDataSource1.SelectCommand, conn);
        da.SelectCommand.Parameters.AddWithValue("@CustName", txtsearch.Text.Trim());
     
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource=ds;
            GridView1.DataBind();
        }
        txtsearch.Text = "";
       
    }
    public void bindgrid()
    {
       
        da = new SqlDataAdapter("select * from Customer_Master", conn);
      

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