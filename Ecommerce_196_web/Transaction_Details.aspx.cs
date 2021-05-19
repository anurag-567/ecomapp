using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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
    int cust_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Id"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        conn = new SqlConnection(cs);

        //SqlDataSource1.SelectCommand = "Select P.ProductName,COUNT(TD.Product_Id)as Quantity,SUM(P.Cost) as Total_Cost  from dbo.Product_Master p JOIN Transaction_Details_Master TD on P.Product_Id=TD.Product_Id where  TD.T_Id= '" + Convert.ToInt32(Request.QueryString["T_Id"]) + "' group by TD.Product_Id,P.ProductName";
        //SqlDataSource1.SelectCommand = "select P.ProductName,TD.Quantity,TD.Total_Cost from Product_Master P JOIN Transaction_Details_Master TD on P.Product_Id=TD.Product_Id where TD.T_Id='" + Convert.ToInt32(Request.QueryString["T_Id"]) + "'";

        SqlDataSource1.SelectCommand = "select P.ProductName,TD.Quantity,TD.Total_Cost,c.C_Id,c.EmailId from Product_Master P JOIN Transaction_Details_Master TD on P.Product_Id=TD.Product_Id  inner join Customer_Master c on TD.C_Id=c.C_Id where TD.T_Id='" + Convert.ToInt32(Request.QueryString["T_Id"]) + "'";

        da = new SqlDataAdapter(SqlDataSource1.SelectCommand, conn);
        

        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            cust_id = Convert.ToInt32(ds.Tables[0].Rows[0]["C_Id"].ToString());
        }

       
    
    
    }
    protected void btnsendinvoice_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection(cs);
        string productname="",Total_Cost="",Quantity="";
  
        //SqlDataSource1.SelectCommand = "select P.ProductName,TD.Quantity,TD.Total_Cost,c.C_Id,c.EmailId from Product_Master P JOIN Transaction_Details_Master TD on P.Product_Id=TD.Product_Id  inner join Customer_Master c on TD.C_Id=c.C_Id where TD.T_Id='" + Convert.ToInt32(Request.QueryString["T_Id"]) + "'";


        da = new SqlDataAdapter("invoice", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.AddWithValue("@cid",cust_id);
        

        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
            {

                productname =productname+ ds.Tables[0].Rows[i]["ProductName"].ToString() + " | ";
                Total_Cost = Total_Cost + ds.Tables[0].Rows[i]["Total_Cost"].ToString()+" | ";
                Quantity = Quantity + ds.Tables[0].Rows[i]["Quantity"].ToString() + " | ";
               
            }
            string emailid = ds.Tables[0].Rows[0]["EmailId"].ToString();
            string overallcost = ds.Tables[1].Rows[0]["Overall_Cost"].ToString();


            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("my-demo.in");
            SmtpServer.Credentials = new System.Net.NetworkCredential("test@my-demo.in", "Test@123");
          
            SmtpServer.Port = 25;
           
            mail = new MailMessage();
            mail.From = new MailAddress("test@my-demo.in");
            mail.To.Add(emailid);
            mail.Subject = "Invoice Details";
            mail.Body = "Product Name :-" + productname + "\n" + "Quantity :-" + Quantity +"\n"+ "Total Cost :-" + Total_Cost + "\n" + "Overall Cost :-" + overallcost;
            SmtpServer.Send(mail);

            Response.Write("<script>alert('Invoice sended')</script>");
        }
        else
        {
            Response.Write("<script>alert('Data not found')</script>");
        }
      
        

    }
}