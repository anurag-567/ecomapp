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
    DataSet ds, ds1 = null;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
       // bindgrid();
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

      if (!IsPostBack)
        {
            conn = new SqlConnection(cs);


            da = new SqlDataAdapter("select * from Category_Master", conn);
            ds = new DataSet();
            da.Fill(ds, "cats");
            if (ds.Tables["cats"].Rows.Count > 0)
            {
                DropDownList1.DataSource = ds.Tables["cats"];
                DropDownList1.DataValueField = "Cat_Id";
                DropDownList1.DataTextField = "CatName";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Select");
            }
         

       }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        //SqlDataSource1.SelectCommand = "select * from Product_Master where Cat_Id='" + DropDownList1.SelectedValue + "'";

        conn = new SqlConnection(cs);
        da = new SqlDataAdapter("select * from Product_Master where Cat_Id='" + DropDownList1.SelectedValue + "'", conn);


        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
          
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Visible = true;
        }
        else
        {
            Response.Write("<script>alert('Data not found')</script>");
            GridView1.Visible = false;
        }



    }

    public void bindgrid()
    {
        conn = new SqlConnection(cs);
        da = new SqlDataAdapter("select * from Product_Master where Cat_Id='1'", conn);


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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);
        da = new SqlDataAdapter("select * from Product_Master where ProductName=@ProductName", conn);
        da.SelectCommand.Parameters.AddWithValue("@ProductName", txtsearch.Text.Trim());

        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        txtsearch.Text = "";
       
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        conn = new SqlConnection(cs);
        if (e.CommandName == "Image")
        {
            string imagepath;
            //int prod_id = Convert.ToInt32(e.CommandArgument);

         //  int index = Convert.ToInt32();
         //GridViewRow gr = GridView1.Rows[index];


            //int cellindex = GridView1.PageIndex;
           
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //var value = GridView1.Rows[rowindex].Cells[cellindex].Text;'

            
           

            da = new SqlDataAdapter("select ImagePath from Product_Master where Product_Id=@Product_Id", conn);
            da.SelectCommand.Parameters.AddWithValue("@Product_Id", Convert.ToInt32(e.CommandArgument));

            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                imagepath = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                Response.Redirect("http://demoproject.in/Ecommerce_website/" + imagepath);
            }

        }
    }
}