using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;

namespace NFC_Shopping_Service
{
    //Ecommerce app service
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds,ds2,ds3 = null;
        SqlCommand cmd;
        static int maxid;
        string cs = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        public ResponseLogin Login(string phoneno, string password)
        {
            conn = new SqlConnection(cs);
            da = new SqlDataAdapter("select * from Customer_Master where PhoneNo=@PhoneNo and Password=@Password", conn);
            //da = new SqlDataAdapter("select * from Customer_Master where EmailId=@EmailId and Password=@password", conn);
            da.SelectCommand.Parameters.AddWithValue("@PhoneNo", phoneno);
            da.SelectCommand.Parameters.AddWithValue("@Password", password);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return new ResponseLogin
                {
                    C_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["C_Id"]),
                    CustName = ds.Tables[0].Rows[0]["CustName"].ToString(),
                    phoneno = ds.Tables[0].Rows[0]["PhoneNo"].ToString(),
                    password = ds.Tables[0].Rows[0]["Password"].ToString(),
                    address = ds.Tables[0].Rows[0]["Address"].ToString()
                    
                };
            }
            else
            {
                return new ResponseLogin();
            }
        }
        public respmsg signuppost(Userdata udata)
        {
            try
            {
                conn = new SqlConnection(cs);
                conn.Open();

                cmd = new SqlCommand("select * from customer_master where EmailId=@EmailId and PhoneNo=@PhoneNo", conn);
                cmd.Parameters.AddWithValue("@EmailId", udata.emailid);
                cmd.Parameters.AddWithValue("@PhoneNo", udata.contact);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    return new respmsg
                    {
                        Msg = "Emailid and contact already exist"
                    };
                }
                else
                {
                    cmd = new SqlCommand("insert into customer_master(CustName,Address,PhoneNo,EmailId,Password,Re_password)values(@CustName,@Address,@PhoneNo,@EmailId,@Password,@Re_password)", conn);
                    cmd.Parameters.AddWithValue("@CustName", udata.name);
                    cmd.Parameters.AddWithValue("@Address",udata.address);
                    cmd.Parameters.AddWithValue("@PhoneNo", udata.contact);
                    cmd.Parameters.AddWithValue("@EmailId",udata.emailid);
                    cmd.Parameters.AddWithValue("@Password", udata.password);
                    cmd.Parameters.AddWithValue("@Re_password", udata.repassword);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return new respmsg
                    {
                        Msg = "Data inserted"
                    };

                }

            }
            catch (Exception e)
            {
                return new respmsg
                {
                    Msg = e.ToString()
                };
            }

        }

        public ResponseGetProductDetails GetProductDetails(string C_Id, string Product_Id)
        {
            conn = new SqlConnection(cs);
            da = new SqlDataAdapter("select * from Temp_Cart where C_Id=@C_Id and Product_Id=@Product_Id;select * from Product_Master where Product_Id=@Product_Id", conn);
            da.SelectCommand.Parameters.AddWithValue("@C_Id",Convert.ToInt32(C_Id));
            da.SelectCommand.Parameters.AddWithValue("@Product_Id", Convert.ToInt32(Product_Id));
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return new ResponseGetProductDetails
                {
                    Product_Id=Product_Id,
                    ProductName = ds.Tables[1].Rows[0]["ProductName"].ToString(),
                    Cost = ds.Tables[1].Rows[0]["Cost"].ToString(),
                    ImagePath = ds.Tables[1].Rows[0]["ImagePath"].ToString(),
                    Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString(),
                };
            }
            else
            {
                return new ResponseGetProductDetails
                {
                    Product_Id = Product_Id,
                    ProductName = ds.Tables[1].Rows[0]["ProductName"].ToString(),
                    Cost = ds.Tables[1].Rows[0]["Cost"].ToString(),
                    ImagePath = ds.Tables[1].Rows[0]["ImagePath"].ToString(),
                    Quantity="1"
                };
            }
            
        }

        public ResponseAddToCart AddToCart(string C_Id, string Product_Id, string Quantity)
        {

            conn = new SqlConnection(cs);
            da = new SqlDataAdapter("select * from  Temp_Cart where C_id=@C_id and Product_Id=@Product_Id", conn);
            da.SelectCommand.Parameters.AddWithValue("@C_Id", Convert.ToInt32(C_Id));
            da.SelectCommand.Parameters.AddWithValue("@Product_Id", Convert.ToInt32(Product_Id));
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                da = new SqlDataAdapter("Update Temp_Cart set Quantity=@Quantity where C_Id=@C_Id and Product_Id=@Product_Id", conn);
                da.SelectCommand.Parameters.AddWithValue("@C_Id", Convert.ToInt32(C_Id));
                da.SelectCommand.Parameters.AddWithValue("@Product_Id", Convert.ToInt32(Product_Id));
                da.SelectCommand.Parameters.AddWithValue("@Quantity", Convert.ToInt32(Quantity));
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();

                return new ResponseAddToCart
                {
                    Result="Updated"
                };
            }
            else
            {
                da = new SqlDataAdapter("insert into Temp_Cart values(@C_Id,@Product_Id,@Quantity)", conn);
                da.SelectCommand.Parameters.AddWithValue("@C_Id", Convert.ToInt32(C_Id));
                da.SelectCommand.Parameters.AddWithValue("@Product_Id", Convert.ToInt32(Product_Id));
                da.SelectCommand.Parameters.AddWithValue("@Quantity", Convert.ToInt32(Quantity));
                conn.Open();
                da.SelectCommand.ExecuteNonQuery();
                conn.Close();

                return new ResponseAddToCart
                {
                    Result = "Added"
                };
            }
        }

        public List<ResponseSeeCart> SeeCart(string C_Id)
        {
            List<ResponseSeeCart> lst=new List<ResponseSeeCart>();
            conn=new SqlConnection(cs);
            da = new SqlDataAdapter("Cust_Cart", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@cid",C_Id);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string OverallCost = ds.Tables[1].Rows[0]["Total_Cost"].ToString();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ResponseSeeCart objresponse = new ResponseSeeCart
                    {
                        Product_Id = ds.Tables[0].Rows[i]["Product_Id"].ToString(),
                        ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString(),
                        ImagePath = ds.Tables[0].Rows[i]["ImagePath"].ToString(),
                        Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString(),
                        Initialcost = ds.Tables[0].Rows[i]["Cost"].ToString(),
                        Total_Cost = ds.Tables[0].Rows[i]["Total_Cost"].ToString(),
                        OverallCost = OverallCost
                    };
                    lst.Add(objresponse);
                }
                return lst;
            }
            else
            {
                return lst;
            }
        }

        public ResponseDeleteProduct DeleteProduct(string C_Id, string Product_Id)
        {
            conn = new SqlConnection(cs);
            SqlCommand command = new SqlCommand("delete from Temp_Cart where C_Id=@C_Id and Product_Id=@Product_Id", conn);
            command.Parameters.AddWithValue("@C_Id",C_Id);
            command.Parameters.AddWithValue("@Product_Id",Product_Id);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            return new ResponseDeleteProduct
            {
                Result="Deleted"
            };
            
        }

        public  ResponseForget forgetPassword(string phone_no)
        {
            conn = new SqlConnection(cs);
            da = new SqlDataAdapter("select * from Customer_Master where PhoneNo=@phoneno", conn);
            da.SelectCommand.Parameters.AddWithValue("@phoneno", phone_no);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                WebRequest MyRssRequest = WebRequest.Create("http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=chintan&pwd=Passw0rd&to=" + phone_no + "&sid=WEBSMS&msg= Your Password for NFC shopping is " + ds.Tables[0].Rows[0]["Password"].ToString() + "&fl=0");
                WebResponse MyRssResponse = MyRssRequest.GetResponse();
                Stream MyRssStream = MyRssResponse.GetResponseStream();
                return new ResponseForget
                {
                    resp="Found"
                };
            }
            else
            {
                return new ResponseForget();
            }
        }

        public List<category> getcategory()
        {
            conn = new SqlConnection(cs);
            List<category> lst = new List<category>();
            da = new SqlDataAdapter("select * from Category_Master", conn);
          
           
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count;i++)
                {
                    category cat = new category
                    {
                        Cat_Id = ds.Tables[0].Rows[i]["Cat_Id"].ToString(),
                        CatName = ds.Tables[0].Rows[i]["CatName"].ToString()
                    };
                    lst.Add(cat);


                }
                return lst;


               
            }
            else
            {
                return lst;
            }
        }

        public List<Product_details> getprod_details(string cat_name)
        {
            conn = new SqlConnection(cs);
            List<Product_details> lst = new List<Product_details>();
            da = new SqlDataAdapter("select * from Category_Master where CatName=@CatName", conn);
            da.SelectCommand.Parameters.AddWithValue("@CatName", cat_name);
            DataTable dt = new DataTable();
            int cat_id=0;
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                cat_id =Convert.ToInt32(dt.Rows[0]["Cat_Id"].ToString());
            }



            da = new SqlDataAdapter("select * from Product_Master where Cat_Id=@Cat_Id", conn);
            da.SelectCommand.Parameters.AddWithValue("@Cat_Id", cat_id);


            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Product_details cat = new Product_details
                    {
                        pro_id = ds.Tables[0].Rows[i]["Product_Id"].ToString(),
                        pro_name = ds.Tables[0].Rows[i]["ProductName"].ToString(),
                        cost = ds.Tables[0].Rows[i]["Cost"].ToString(),
                        imagepath = ds.Tables[0].Rows[i]["ImagePath"].ToString(),
                        cat_id = ds.Tables[0].Rows[i]["Cat_Id"].ToString()
                    };
                    lst.Add(cat);


                }
                return lst;



            }
            else
            {
                return lst;
            }


        }


        public respmsg buyproduct(buy_product_details buyproduct)
        {
            try
            {
                conn = new SqlConnection(cs);
                conn.Open();

                cmd = new SqlCommand("select * from Temp_Cart where C_id=@C_id and Product_Id=@Product_Id",conn);
                cmd.Parameters.AddWithValue("@C_id",buyproduct.customer_id);
                cmd.Parameters.AddWithValue("@Product_Id",buyproduct.product_id);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                   SqlCommand cmd1 = new SqlCommand("Update Temp_Cart set Quantity=@Quantity where C_Id=@C_Id and Product_Id=@Product_Id",conn);
                   cmd1.Parameters.AddWithValue("@C_Id", buyproduct.customer_id);
                   cmd1.Parameters.AddWithValue("@Product_Id", buyproduct.product_id);
                   cmd1.Parameters.AddWithValue("@Quantity",buyproduct.quantity);
                   cmd1.ExecuteNonQuery();

                    return new respmsg
                    {
                        Msg ="Updated"
                    };
                }
                else
                {
                    cmd = new SqlCommand("insert into Temp_Cart(C_Id,Product_Id,Quantity) values(@C_Id,@Product_Id,@Quantity)",conn);
                    cmd.Parameters.AddWithValue("@C_Id", buyproduct.customer_id);

                    cmd.Parameters.AddWithValue("@Product_Id",buyproduct.product_id);
                    cmd.Parameters.AddWithValue("@Quantity",buyproduct.quantity);
                   
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return new respmsg
                    {
                        Msg = "Data inserted"
                    };

                }

            }
            catch (Exception e)
            {
                return new respmsg
                {
                    Msg = e.ToString()
                };
            }


        }


        //public List<ResponseSeeCart> SeeCart(string C_Id)
        //{
        //    List<ResponseSeeCart> lst = new List<ResponseSeeCart>();
        //    conn = new SqlConnection(cs);
        //    da = new SqlDataAdapter("Cust_Cart", conn);
        //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    da.SelectCommand.Parameters.AddWithValue("@cid", C_Id);
        //    ds = new DataSet();
        //    da.Fill(ds);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        string OverallCost = ds.Tables[1].Rows[0]["Total_Cost"].ToString();

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            ResponseSeeCart objresponse = new ResponseSeeCart
        //            {
        //                Product_Id = ds.Tables[0].Rows[i]["Product_Id"].ToString(),
        //                ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString(),
        //                ImagePath = ds.Tables[0].Rows[i]["ImagePath"].ToString(),
        //                Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString(),
        //                Total_Cost = ds.Tables[0].Rows[i]["Total_Cost"].ToString(),
        //                OverallCost = OverallCost
        //            };
        //            lst.Add(objresponse);
        //        }
        //        return lst;
        //    }
        //    else
        //    {
        //        return lst;
        //    }
        //}


        public respmsg order_product(buy_product_details buyproduct)
        {
            try
            {
                conn = new SqlConnection(cs);
                conn.Open();

                cmd = new SqlCommand("select * from Temp_Cart where C_id=@C_id and Product_Id=@Product_Id", conn);
                cmd.Parameters.AddWithValue("@C_id", buyproduct.customer_id);
                cmd.Parameters.AddWithValue("@Product_Id", buyproduct.product_id);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("Update Temp_Cart set Quantity=@Quantity where C_Id=@C_Id and Product_Id=@Product_Id", conn);
                    cmd1.Parameters.AddWithValue("@C_Id", buyproduct.customer_id);
                    cmd1.Parameters.AddWithValue("@Product_Id", buyproduct.product_id);
                    cmd1.Parameters.AddWithValue("@Quantity", buyproduct.quantity);
                    cmd1.ExecuteNonQuery();

                    return new respmsg
                    {
                        Msg = "Updated"
                    };
                }
                else
                {
                    cmd = new SqlCommand("insert into Temp_Cart(C_Id,Product_Id,Quantity) values(@C_Id,@Product_Id,@Quantity)", conn);
                    cmd.Parameters.AddWithValue("@C_Id", buyproduct.customer_id);

                    cmd.Parameters.AddWithValue("@Product_Id", buyproduct.product_id);
                    cmd.Parameters.AddWithValue("@Quantity", buyproduct.quantity);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return new respmsg
                    {
                        Msg = "Data inserted"
                    };

                }

            }
            catch (Exception e)
            {
                return new respmsg
                {
                    Msg = e.ToString()
                };
            }


        }

        public respmsg inserttransaction(String C_ID)
        {
            try
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


                    cmd = new SqlCommand("inst_transc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cid", C_ID);
               
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count;i++)
                    {
                        cmd = new SqlCommand("insert into Transaction_Details_Master(C_Id,Product_Id,Quantity,Total_Cost,DateTime,T_Id) values(@C_Id,@Product_Id,@Quantity,@Total_Cost,@DateTime,@T_Id)", conn);
                        cmd.Parameters.AddWithValue("@C_Id", dt1.Rows[i]["C_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Total_Cost",dt1.Rows[i]["Total_Cost"].ToString());
                        cmd.Parameters.AddWithValue("@Product_Id", dt1.Rows[i]["Product_Id"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity", dt1.Rows[i]["Quantity"].ToString());
                        cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.AddMinutes(330).ToString());
                        cmd.Parameters.AddWithValue("@T_Id", maxid);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    using (SqlCommand cmd12 = new SqlCommand("insert into Transaction_Master(C_Id,DateTime) values(@C_Id,@DateTime)", conn))
                    {
                        cmd12.Parameters.AddWithValue("@C_Id",C_ID);
                        cmd12.Parameters.AddWithValue("@DateTime", DateTime.Now.AddMinutes(330).ToString());
                        conn.Open();
                        cmd12.ExecuteNonQuery();
                        conn.Close();

                    }

                    using (SqlCommand cmd13 = new SqlCommand("delete from Temp_Cart where C_Id=@cid", conn))
                    {
                        cmd13.Parameters.AddWithValue("@cid", C_ID);
                        conn.Open();
                        cmd13.ExecuteNonQuery();
                        conn.Close();
                    }
                    //conn = new SqlConnection(cs);
                    //cmd = new SqlCommand("delete from Temp_Cart where C_Id=@cid", conn);
                    //cmd.Parameters.AddWithValue("@cid", C_ID);
                    //conn.Open();
                    //cmd.ExecuteNonQuery();
                    //conn.Close();

                    return new respmsg
                    {
                        Msg = "Data inserted"
                    };
                }
                else
                {
                    return new respmsg
                    {
                        Msg = "Data not found"
                    };
                   
                }



            }
            catch (Exception e)
            {
                return new respmsg
                {
                    Msg = e.ToString()
                };
            }
        }

    }
}
