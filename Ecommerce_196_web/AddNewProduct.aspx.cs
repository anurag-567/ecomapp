using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection conn = null;
    SqlDataAdapter da = null;
    DataSet ds,ds1,ds2 = null;
    string cs = WebConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    public static string imagepath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Id"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        conn = new SqlConnection(cs);

        if (!IsPostBack)
        {
            da = new SqlDataAdapter("select * from Category_Master", conn);
            ds = new DataSet();
            da.Fill(ds, "cats");
            if (ds.Tables["cats"].Rows.Count > 0)
            {
                DropDownList1.DataSource = ds.Tables["cats"];
                DropDownList1.DataTextField = "CatName";
                DropDownList1.DataValueField = "Cat_Id";
                DropDownList1.DataBind();
            }
            
        }

        if (!IsPostBack && Request.QueryString["Action"] == "edit")
        {
            string productid = Request.QueryString["Product_Id"];
            da = new SqlDataAdapter("select * from Product_Master where Product_Id=@productid", conn);
            da.SelectCommand.Parameters.AddWithValue("@productid", productid);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtProductName.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
                txtCost.Text = ds.Tables[0].Rows[0]["Cost"].ToString();
                //Image1.ImageUrl = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                imagepath = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                Image1.ImageUrl = imagepath;
                btnDelete.Enabled = true;
                DropDownList1.SelectedValue = ds.Tables[0].Rows[0]["Cat_Id"].ToString();

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);
        if (Request.QueryString["Action"] == "edit")
        {
            if (FileUpload1.HasFile)
            {
                File.Delete(Server.MapPath("~/" + imagepath));

                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/ProductImages/") + filename);
                string filepath = "ProductImages/" + filename;

                string productid = Request.QueryString["Product_Id"];

                da = new SqlDataAdapter("Update Product_Master set Cat_Id=@Cat_Id,ProductName=@productName,Cost=@cost,ImagePath=@imagepath where Product_Id=@productid", conn);
                da.SelectCommand.Parameters.AddWithValue("@Cat_Id",DropDownList1.SelectedValue);
                da.SelectCommand.Parameters.AddWithValue("@productName", txtProductName.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@cost", txtCost.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@productid", productid);
                da.SelectCommand.Parameters.AddWithValue("@imagepath", filepath);
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Manage_Products.aspx?msg=update");
            }
            else
            {
                string productid = Request.QueryString["Product_Id"];

                da = new SqlDataAdapter("Update Product_Master set Cat_Id=@Cat_Id,ProductName=@productName,Cost=@cost where Product_Id=@productid", conn);
                da.SelectCommand.Parameters.AddWithValue("@Cat_Id", DropDownList1.SelectedValue);
                da.SelectCommand.Parameters.AddWithValue("@productName", txtProductName.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@cost", txtCost.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@productid", productid);
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Manage_Products.aspx?msg=update");
            }
        }
        else
        {
            if (FileUpload1.HasFile)
            {

                string filename = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/ProductImages/") + filename);
                string filepath = "ProductImages/" + filename;

                da = new SqlDataAdapter("insert into Product_Master values(@catid,@productname,@cost,@imagepath)", conn);
                da.SelectCommand.Parameters.AddWithValue("@catid", DropDownList1.SelectedValue);
                da.SelectCommand.Parameters.AddWithValue("@productname", txtProductName.Text);
                da.SelectCommand.Parameters.AddWithValue("@cost", txtCost.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@imagepath", filepath);

                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("Manage_Products.aspx?msg=add");
            } 
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        File.Delete(Server.MapPath("~/"+imagepath));

        conn = new SqlConnection(cs);
        string productid = Request.QueryString["Product_Id"];
        da = new SqlDataAdapter("delete from Product_Master where Product_Id=@productid", conn);
        da.SelectCommand.Parameters.AddWithValue("@productid", productid);
        conn.Open();
        da.SelectCommand.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Manage_Products.aspx?msg=delete");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtCost.Text = "";
        txtProductName.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Manage_Products.aspx");
    }
}