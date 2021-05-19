using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NFC_Shopping_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "Login/{phoneno}/{password}", ResponseFormat = WebMessageFormat.Json)]
        ResponseLogin Login(string phoneno, string password);

        [OperationContract]
        [WebInvoke(Method="POST",UriTemplate = "signuppost",ResponseFormat=WebMessageFormat.Json)]
        respmsg signuppost(Userdata udata);

        [OperationContract]
        [WebGet(UriTemplate = "GetProductDetails/{C_Id}/{Product_Id}", ResponseFormat = WebMessageFormat.Json)]
        ResponseGetProductDetails GetProductDetails(string C_Id, string Product_Id);

        [OperationContract]
        [WebGet(UriTemplate = "AddToCart/{C_Id}/{Product_Id}/{Quantity}", ResponseFormat = WebMessageFormat.Json)]
        ResponseAddToCart AddToCart(string C_Id, string Product_Id, string Quantity);

        [OperationContract]
        [WebGet(UriTemplate = "SeeCart/{C_Id}", ResponseFormat = WebMessageFormat.Json)]
        List<ResponseSeeCart> SeeCart(string C_Id);

        [OperationContract]
        [WebGet(UriTemplate = "DeleteProduct/{C_Id}/{Product_Id}", ResponseFormat = WebMessageFormat.Json)]
        ResponseDeleteProduct DeleteProduct(string C_Id, string Product_Id);

        [OperationContract]
        [WebGet(UriTemplate = "forgetPassword/{phone_no}", ResponseFormat = WebMessageFormat.Json)]
        ResponseForget forgetPassword(string phone_no);

        [OperationContract]
        [WebGet(UriTemplate = "getcategory",ResponseFormat=WebMessageFormat.Json)]
        List<category> getcategory();

        [OperationContract]
        [WebGet(UriTemplate ="getprod_details/{cat_name}",ResponseFormat=WebMessageFormat.Json)]
        List<Product_details> getprod_details(string cat_name);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate ="buyproduct",ResponseFormat=WebMessageFormat.Json)]
        respmsg buyproduct(buy_product_details buyproduct);

        [OperationContract]
        [WebGet(UriTemplate = "inserttransaction/{C_Id}", ResponseFormat = WebMessageFormat.Json)]
        respmsg inserttransaction(String C_Id);

    }

    [DataContract]
    public class ResponseForget
    {
        [DataMember]
        public string resp
        {
            get;
            set;
        }
    }

    [DataContract]
    public class ResponseLogin
    {
        [DataMember]
        public int C_Id
        {
            get;
            set;
        }
        [DataMember]
        public string CustName
        {
            get;
            set;
        }
        [DataMember]
        public string Balance
        {
            get;
            set;
        }

        [DataMember]
        public string phoneno
        {
            get;
            set;
        }
        [DataMember]
        public string password
        {
            get;
            set;
        }

        [DataMember]
        public string address { get; set; } 

    }

    [DataContract]
    public class ResponseGetProductDetails
    {
        [DataMember]
        public string Product_Id
        {
            get;
            set;
        }
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }
        [DataMember]
        public string Cost
        {
            get;
            set;
        }
        [DataMember]
        public string ImagePath
        {
            get;
            set;
        }
        [DataMember]
        public string Quantity
        {
            get;
            set;
        }


    }

    [DataContract]
    public class ResponseAddToCart
    {
        [DataMember]
        public string Result
        {
            get;
            set;
        }
    }

    [DataContract]
    public class ResponseSeeCart
    {
        [DataMember]
        public string Product_Id
        {
            get;
            set;
        }
        [DataMember]
        public string ProductName
        {
            get;
            set;
        }
        [DataMember]
        public string ImagePath
        {
            get;
            set;
        }
        [DataMember]
        public string Quantity
        {
            get;
            set;
        }
        [DataMember]
        public string Total_Cost
        {
            get;
            set;
        }
        [DataMember]
        public string OverallCost
        {
            get;
            set;
        }
        [DataMember]
        public string Initialcost{get;set;}


    }

    [DataContract]
    public class ResponseDeleteProduct
    {
        [DataMember]
        public string Result
        {
            get;
            set;
        }
    }
    [DataContract]
    public class respmsg
    {
        [DataMember]
        public String Msg { get; set; }
    }
    [DataContract]
    public class Userdata
    {
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String emailid { get; set; }
        [DataMember]
        public String contact { get; set; }
        [DataMember]
        public String address { get; set; }
        [DataMember]
        public String password { get; set; }
        [DataMember]
        public String repassword { get; set; }
        [DataMember]
        public String balance { get; set; }

    }

    [DataContract]
    public class category
    {
        [DataMember]
        public String Cat_Id { get; set; }
        [DataMember]
        public String CatName { get; set; }
    }
    [DataContract]
    public class Product_details
    {
        [DataMember]
        public string pro_id { get; set; }
        [DataMember]
        public string pro_name { get; set; }
        [DataMember]
        public string cost { get; set; }
        [DataMember]
        public string imagepath { get; set; }
        [DataMember]
        public string cat_id { get; set; }
    }
    [DataContract]
    public class buy_product_details
    {
        [DataMember]
        public string product_id { get; set; }
        [DataMember]
        public string customer_id { get; set; }
        [DataMember]
        public string quantity { get; set; }
        [DataMember]
        public string status { get; set; }
    }

    [DataContract]
    public class Ordered_product
    {
        [DataMember]
        public string product_id { get; set; }
        [DataMember]
        public string Customer_id { get; set; }
        [DataMember]
        public string prod_quantity { get; set; }
    }

    [DataContract]
    public class inserttransc
    {
        [DataMember]
        public string cid { get; set; }
        [DataMember]
        public string pid { get; set; }
        [DataMember]
        public string pt_cost { get; set; }
        [DataMember]
        public string p_qnt { get; set; }
    }
}
