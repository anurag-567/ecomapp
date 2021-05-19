package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Adapter.ProductAdapter;
import com.example.techtrix_new.object_detectionecomm.Data.Product_data;
import com.example.techtrix_new.object_detectionecomm.Data.selected_prod;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;

import java.util.ArrayList;

public class Productlistview extends Activity {

    ListView listproduct;
    ProgressDialog progressDialog;
    int resp;
    public ArrayList<String> prod_id,prod_name,prod_cost,prod_imagepath,cate_id;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_productlistview);

        listproduct=findViewById(R.id.listviewproduct);
        filladapter();

        listproduct.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                selected_prod.setSel_prod_id(Product_data.getProductid_().get(position));
                selected_prod.setSel_prod_name(Product_data.getProductname_().get(position));
                selected_prod.setSel_prod_cost(Product_data.getProductcost_().get(position));
                selected_prod.setSel_cat_id(Product_data.getCategoryid_().get(position));
                selected_prod.setSel_prod_imagepath(Product_data.getProductimagepath_().get(position));

                Intent intent=new Intent(Productlistview.this,Buy_product.class);
                startActivity(intent);

            }
        });
    }


    public void filladapter()
    {
        final connectionManager conn = new connectionManager();
        if (conn.checkNetworkAvailable(Productlistview.this)) {

         /*   if(Location_.getLatitude()!=null)
            {*/
            progressDialog = new ProgressDialog(this);
            progressDialog.show();
            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        if (conn.getproductlist()) {
                            resp = 0;
                        } else {
                            resp = 1;
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    hd.sendEmptyMessage(0);

                }
            };
            tthread.start();
            /*  }*/
          /*  else
            {
                if (mySwipeRefreshLayout.isRefreshing()) {
                    mySwipeRefreshLayout.setRefreshing(false);
                }
                Toast.makeText(ViewofferlistActivity.this, "Location not yet Captured !!!" +
                        "refresh page", Toast.LENGTH_SHORT).show();
            }*/

        }
        else {


            Toast.makeText(this, "Sorry no network access.", Toast.LENGTH_LONG).show();

        }

    }

    public Handler hd = new Handler() {
        public void handleMessage(Message msg) {

            if (progressDialog.isShowing()) {
                progressDialog.dismiss();
            }
            switch (resp) {
                case 0:
                    prod_id=Product_data.getProductid_();
                    prod_name=Product_data.getProductname_();
                    prod_cost=Product_data.getProductcost_();
                    prod_imagepath=Product_data.getProductimagepath_();
                    cate_id=Product_data.getCategoryid_();


                 /*   if(offerid.isEmpty())
                    {
                        Toast.makeText(ViewofferlistActivity.this, "Data not found", Toast.LENGTH_SHORT).show();

                    }
                    else
                    {*/
                    ProductAdapter Dadapter=new ProductAdapter(Productlistview.this,prod_id,prod_name,prod_imagepath);
                    listproduct.setAdapter(Dadapter);

                    /* }*/

                    break;
                case 1:
                    Toast.makeText(Productlistview.this, "Something went wrong", Toast.LENGTH_SHORT).show();
                    break;
            }
        }

    };

}
