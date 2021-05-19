package com.example.techtrix_new.object_detectionecomm.connectivity;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

import com.example.techtrix_new.object_detectionecomm.Data.Cartlistdata;
import com.example.techtrix_new.object_detectionecomm.Data.Category_data;
import com.example.techtrix_new.object_detectionecomm.Data.Customer_data;
import com.example.techtrix_new.object_detectionecomm.Data.Product_data;
import com.example.techtrix_new.object_detectionecomm.Data.buy_product;
import com.example.techtrix_new.object_detectionecomm.Data.fillcust_Request;
import com.example.techtrix_new.object_detectionecomm.Data.selectcart_prod;

import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;

public class connectionManager {



   /* public static String ServiceUrl="http://demoproject.in/Onlinefebrication_service/Service1.svc";*/
   public static String ServiceUrl="http://demoproject.in/Ecommerce_service/Service1.svc";
    public static boolean checkNetworkAvailable(Context context)
    {
        try {
            ConnectivityManager connectivityManager=(ConnectivityManager)context.getSystemService(Context.CONNECTIVITY_SERVICE);
            NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
            return activeNetworkInfo != null;
        } catch (Exception e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return false;
        }
    }

    public boolean Login()
    {
        String responseString;
        try
        {
            final String TAG_C_Id="C_Id";
            final String TAG_CustName="CustName";
            final String TAG_Balance="Balance";
            final String TAG_address = "address";



            String url=String.format(ServiceUrl+"/Login/"+fillcust_Request.getPhoneNo()+"/"+fillcust_Request.getPassword());
            HttpClient httpclient = new DefaultHttpClient();
            HttpResponse response = httpclient.execute(new HttpGet(url));
            StatusLine statusLine = response.getStatusLine();
            if(statusLine.getStatusCode() == HttpStatus.SC_OK)
            {
                ByteArrayOutputStream out = new ByteArrayOutputStream();
                response.getEntity().writeTo(out);
                out.close();
                responseString = out.toString();

                JSONObject jsonObj=new JSONObject(responseString);
                String C_Id=jsonObj.getString(TAG_C_Id);
                String CustName=jsonObj.getString(TAG_CustName);
                String Balance=jsonObj.getString(TAG_Balance);
                String Address = jsonObj.getString(TAG_address);

                if(CustName!="null")
                {
                    fillcust_Request.setC_Id(C_Id);
                    fillcust_Request.setCustName(CustName);
                    fillcust_Request.setBalance(Balance);
                    fillcust_Request.setAddress(Address);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return false;
        }
    }

    public int register() {
        try {
            final String TAG_id = "Msg";
            StringBuilder result = new StringBuilder();

            HttpClient httpclient = new DefaultHttpClient();
            String url = String.format(ServiceUrl+"/"+"signuppost");
            HttpPost httpPost = new HttpPost(url);
            String json = "";
            JSONObject jsonObject = new JSONObject();
            jsonObject.accumulate("name",Customer_data.getName());
            jsonObject.accumulate("emailid",Customer_data.getEmail());
            jsonObject.accumulate("contact",Customer_data.getContact());
            jsonObject.accumulate("address",Customer_data.getAddress());
            jsonObject.accumulate("password",Customer_data.getPassword());
            jsonObject.accumulate("repassword",Customer_data.getRepassword());

            json = jsonObject.toString();
            StringEntity se = new StringEntity(json);
            httpPost.setEntity(se);
            httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-type", "application/json");

            HttpResponse response = httpclient.execute(httpPost);
            StatusLine statusLine = response.getStatusLine();
            if (statusLine.getStatusCode() == HttpStatus.SC_OK) {
                InputStream in = new BufferedInputStream(response.getEntity().getContent());
                BufferedReader br = new BufferedReader(new InputStreamReader(in));
                String line;
                while ((line = br.readLine()) != null) {
                    result.append(line);
                }
                JSONObject jobj = new JSONObject(result.toString());
                String msg = jobj.getString(TAG_id);

                if (msg.equals("Data inserted")) {

                    return 1;

                } else if (msg.equals("Emailid and contact already exist")) {
                    return 2;
                } else {
                    return 3;
                }
            } else {

                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());
            }

        } catch (Exception e) {
            return 0;
        }
    }

    public boolean getCat() {
        try {

            StringBuilder result = new StringBuilder();

            String url = String.format(ServiceUrl+"/"+"getcategory");
            HttpClient httpclient = new DefaultHttpClient();
            HttpResponse response = httpclient.execute(new HttpGet(url));
            StatusLine statusLine = response.getStatusLine();
            if (statusLine.getStatusCode() == HttpStatus.SC_OK) {

                InputStream in = new BufferedInputStream(response.getEntity().getContent());
                BufferedReader br = new BufferedReader(new InputStreamReader(in));
                String line;
                while ((line = br.readLine()) != null) {
                    result.append(line);
                }
                br.close();
                JSONArray jarrayobj = new JSONArray(result.toString());
                ArrayList<String> stringArray1, stringArray2;
                stringArray1 = new ArrayList<String>(jarrayobj.length());
                stringArray2 = new ArrayList<String>(jarrayobj.length());
                for (int i = 0; i < jarrayobj.length(); i++) {
                    JSONObject job = jarrayobj.getJSONObject(i);
                    String id = job.optString("Cat_Id");
                    String name = job.optString("CatName");
                    stringArray1.add(id);
                    stringArray2.add(name);
                }
                Category_data.setId(stringArray1);
                Category_data.setName(stringArray2);

                return true;
            } else {

                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());

            }
        } catch (Exception e) {
            return false;
        }
    }

    public boolean getproductlist() {
        try {

            ArrayList<String> productid,productname,productprice,product_imagepath, cat_id;

            StringBuilder result = new StringBuilder();

            String url = String.format(ServiceUrl+"/"+"getprod_details/"+Category_data.getSelect_category());

            HttpClient httpclient = new DefaultHttpClient();
            HttpResponse response = httpclient.execute(new HttpGet(url));
            StatusLine statusLine = response.getStatusLine();
            if (statusLine.getStatusCode() == HttpStatus.SC_OK) {

                InputStream in = new BufferedInputStream(response.getEntity().getContent());
                BufferedReader br = new BufferedReader(new InputStreamReader(in));
                String line;
                while ((line = br.readLine()) != null) {
                    result.append(line);
                }
                br.close();
                JSONArray jarrayobj = new JSONArray(result.toString());
                int length = 0;
                if (jarrayobj.length() > 0) {
                    length = jarrayobj.length();
                }

                productid = new ArrayList<String>(length);
                productname = new ArrayList<String>(length);
                productprice = new ArrayList<String>(length);
                product_imagepath = new ArrayList<String>(length);
                cat_id = new ArrayList<String>(length);




                for (int i = 0; i < jarrayobj.length(); i++) {
                    JSONObject job = jarrayobj.getJSONObject(i);
                    String product_id = job.optString("pro_id");
                    String product_name = job.optString("pro_name");
                    String product_price = job.optString("cost");
                    String prod_imagepath = job.optString("imagepath");
                    String product_cat = job.optString("offerstartdate");



                    productid.add(product_id);
                    productname.add(product_name);
                    productprice.add(product_price);
                    product_imagepath.add(prod_imagepath);
                    cat_id.add(product_cat);


                }

                Product_data.setProductid_(productid);
                Product_data.setProductname_(productname);
                Product_data.setProductcost_(productprice);
                Product_data.setProductimagepath_(product_imagepath);
                Product_data.setCategoryid_(cat_id);


                return true;
            } else {

                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());
            }
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    public int buy_product() {
        try {
            final String TAG_id = "Msg";
            StringBuilder result = new StringBuilder();

            HttpClient httpclient = new DefaultHttpClient();
            String url = String.format(ServiceUrl+"/"+ "buyproduct");
            HttpPost httpPost = new HttpPost(url);
            String json = "";
            JSONObject jsonObject = new JSONObject();
            jsonObject.accumulate("customer_id", buy_product.getBuy_custid());
            jsonObject.accumulate("product_id",buy_product.getBuy_prodid());
            jsonObject.accumulate("quantity",buy_product.getBuy_quantity());


            json = jsonObject.toString();
            StringEntity se = new StringEntity(json);
            httpPost.setEntity(se);
            httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-type", "application/json");

            HttpResponse response = httpclient.execute(httpPost);
            StatusLine statusLine = response.getStatusLine();
            if (statusLine.getStatusCode() == HttpStatus.SC_OK) {
                InputStream in = new BufferedInputStream(response.getEntity().getContent());
                BufferedReader br = new BufferedReader(new InputStreamReader(in));
                String line;
                while ((line = br.readLine()) != null) {
                    result.append(line);
                }
                JSONObject jobj = new JSONObject(result.toString());
                String msg = jobj.getString(TAG_id);

                if (msg.equals("Data inserted")) {

                    return 1;

                } else if (msg.equals("Updated")) {
                    return 2;
                } else {
                    return 3;
                }
            } else {

                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());
            }

        } catch (Exception e) {
            return 0;
        }
    }

    public boolean SeeCart()
    {
        String responseString;
        JSONArray newJsonArray;
        ArrayList<String> stringArray;
        ArrayList<String> stringarray1;
        ArrayList<String> stringarray2;
        ArrayList<String> stringarray3;
        ArrayList<String> stringarray4;
        ArrayList<String> stringarray5;
        ArrayList<String> stringarray6;
        ArrayList<String> stringarray7;

        try
        {
            final String TAG_Product_Id="Product_Id";
            final String TAG_ProductName="ProductName";
            final String TAG_ImagePath="ImagePath";
            final String TAG_Quantity="Quantity";
            final String TAG_Total_Cost="Total_Cost";
            final String TAG_Initial_Cost="Initialcost";
            final String TAG_OverallCost="OverallCost";


            String url=String.format(ServiceUrl+"/SeeCart/"+fillcust_Request.getC_Id());
            HttpClient httpclient = new DefaultHttpClient();
            HttpResponse response = httpclient.execute(new HttpGet(url));
            StatusLine statusLine = response.getStatusLine();
            String OverallCost="";
            if(statusLine.getStatusCode() == HttpStatus.SC_OK)
            {
                ByteArrayOutputStream out = new ByteArrayOutputStream();
                response.getEntity().writeTo(out);
                out.close();
                responseString = out.toString();

                newJsonArray=new JSONArray(responseString);
                stringArray=new ArrayList<String>(newJsonArray.length());
                stringarray1=new ArrayList<String>(newJsonArray.length());
                stringarray2=new ArrayList<String>(newJsonArray.length());
                stringarray3=new ArrayList<String>(newJsonArray.length());
                stringarray4=new ArrayList<String>(newJsonArray.length());
                stringarray5=new ArrayList<String>(newJsonArray.length());
                stringarray6=new ArrayList<String>(newJsonArray.length());
                stringarray7=new ArrayList<String>(newJsonArray.length());

                for(int i=0;i<newJsonArray.length();i++)
                {
                    JSONObject jsonObj = newJsonArray.getJSONObject(i);
                    String Product_Id=jsonObj.optString(TAG_Product_Id);
                    String Product_Name=jsonObj.optString(TAG_ProductName);
                    String ImagePath=jsonObj.optString(TAG_ImagePath);
                    String Quantity=jsonObj.optString(TAG_Quantity);
                    String Initial_cost=jsonObj.optString(TAG_Initial_Cost);
                    String Total_Cost=jsonObj.optString(TAG_Total_Cost);
                    OverallCost=jsonObj.optString(TAG_OverallCost);

                    stringArray.add(Product_Id);
                    stringarray1.add(Product_Name);
                    //stringarray2.add("http://my-demo.in/NFC_Shopping_Web_New/"+ImagePath);
                    stringarray2.add(ImagePath);
                    stringarray3.add(Quantity);
                    stringarray4.add(Total_Cost);
                    stringarray5.add(OverallCost);
                    stringarray6.add(Initial_cost);
                }


                Cartlistdata.setProductid_(stringArray);
                Cartlistdata.setProductname_(stringarray1);
                Cartlistdata.setProductimagepath_(stringarray2);
                Cartlistdata.setProd_quantity(stringarray3);
                Cartlistdata.setProduct_total_cost_(stringarray4);
                Cartlistdata.setOverallcost(stringarray5);
                Cartlistdata.setInitial_cost(stringarray6);
                Cartlistdata.setOverall_cost(OverallCost);
               return true;
            }
            else
            {
                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());
            }
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return false;
        }
    }

    public int order_product() {
        try {
            final String TAG_id = "Msg";
            StringBuilder result = new StringBuilder();

            HttpClient httpclient = new DefaultHttpClient();
            String url = String.format(ServiceUrl+"/"+ "buyproduct");
            HttpPost httpPost = new HttpPost(url);
            String json = "";
            JSONObject jsonObject = new JSONObject();
            jsonObject.accumulate("customer_id",fillcust_Request.getC_Id());
            jsonObject.accumulate("product_id",selectcart_prod.getSel_cartprod_id());
            jsonObject.accumulate("quantity",buy_product.getBuy_quantity());


            json = jsonObject.toString();
            StringEntity se = new StringEntity(json);
            httpPost.setEntity(se);
            httpPost.setHeader("Accept", "application/json");
            httpPost.setHeader("Content-type", "application/json");

            HttpResponse response = httpclient.execute(httpPost);
            StatusLine statusLine = response.getStatusLine();
            if (statusLine.getStatusCode() == HttpStatus.SC_OK) {
                InputStream in = new BufferedInputStream(response.getEntity().getContent());
                BufferedReader br = new BufferedReader(new InputStreamReader(in));
                String line;
                while ((line = br.readLine()) != null) {
                    result.append(line);
                }
                JSONObject jobj = new JSONObject(result.toString());
                String msg = jobj.getString(TAG_id);

                if (msg.equals("Data inserted")) {

                    return 1;

                } else if (msg.equals("Updated")) {
                    return 2;
                } else {
                    return 3;
                }
            } else {

                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());
            }

        } catch (Exception e) {
            return 0;
        }
    }

    public void DeleteProduct()
    {
        String responseString;
        try
        {
            final String TAG_RESULT="Result";

            String url=String.format(ServiceUrl+"/DeleteProduct/"+fillcust_Request.getC_Id()+"/"+selectcart_prod.getSel_cartprod_id());
            HttpClient httpclient = new DefaultHttpClient();
            HttpResponse response = httpclient.execute(new HttpGet(url));
            StatusLine statusLine = response.getStatusLine();
            if(statusLine.getStatusCode() == HttpStatus.SC_OK)
            {
                ByteArrayOutputStream out = new ByteArrayOutputStream();
                response.getEntity().writeTo(out);
                out.close();
                responseString = out.toString();

                JSONObject jsonObj=new JSONObject(responseString);
                String Result=jsonObj.getString(TAG_RESULT);
            }
            else
            {
                response.getEntity().getContent().close();
                throw new IOException(statusLine.getReasonPhrase());
            }
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }

    public boolean inst_trans()
    {
        String responseString;
        try
        {
            final String TAG_MSG="Msg";



            String url=String.format(ServiceUrl+"/inserttransaction/"+fillcust_Request.getC_Id());
            HttpClient httpclient = new DefaultHttpClient();
            HttpResponse response = httpclient.execute(new HttpGet(url));
            StatusLine statusLine = response.getStatusLine();
            if(statusLine.getStatusCode() == HttpStatus.SC_OK)
                 {
                ByteArrayOutputStream out = new ByteArrayOutputStream();
                response.getEntity().writeTo(out);
                out.close();
                responseString = out.toString();

                JSONObject jsonObj=new JSONObject(responseString);
                String msg=jsonObj.getString(TAG_MSG);


                if(msg.equals("Data inserted"))
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return false;
        }
    }


}
