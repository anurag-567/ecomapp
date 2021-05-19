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
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Adapter.ProductAdapter;
import com.example.techtrix_new.object_detectionecomm.Adapter.Seecartadapter;
import com.example.techtrix_new.object_detectionecomm.Data.Cartlistdata;
import com.example.techtrix_new.object_detectionecomm.Data.Ordercartproduct;
import com.example.techtrix_new.object_detectionecomm.Data.Product_data;
import com.example.techtrix_new.object_detectionecomm.Data.selectcart_prod;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;

import java.util.ArrayList;

public class Seecartlist extends Activity {

    ListView seecartlist;
    Button ordreproduct;

TextView Overall_cost;
    ProgressDialog progressDialog;
    int resp;
    public ArrayList<String> prod_id,prod_name,prod_quantity,prod_imagepath,cate_id,pro_tcost,sum_totalcost;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_seecartlist);

        seecartlist=findViewById(R.id.listviewcart);
        ordreproduct=findViewById(R.id.btnbuy);
        fillseecart();
        Overall_cost=findViewById(R.id.txtAmount);
        Overall_cost.setText("Rs. "+Cartlistdata.getOverall_cost());


        seecartlist.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                selectcart_prod.setSel_cartprod_id(Cartlistdata.getProductid_().get(position));
                selectcart_prod.setSel_cartprod_name(Cartlistdata.getProductname_().get(position));
                selectcart_prod.setSel_cartprodtotal_cost(Cartlistdata.getProduct_total_cost_().get(position));
                selectcart_prod.setSel_catprod_quantity(Cartlistdata.getProd_quantity().get(position));
                selectcart_prod.setSel_cartprod_imagepath(Cartlistdata.getProductimagepath_().get(position));
                selectcart_prod.setSel_cartprod_overallcost(Cartlistdata.getOverallcost().get(position));
                selectcart_prod.setSel_cartprod_initailcost(Cartlistdata.getInitial_cost().get(position));


                Intent intent=new Intent(Seecartlist.this,Selected_cartdata.class);
                startActivity(intent);

            }
        });


        ordreproduct.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                inserttransc();
            }
        });

    }

    public void fillseecart()
    {
        final connectionManager conn = new connectionManager();
        if (conn.checkNetworkAvailable(Seecartlist.this)) {

         /*   if(Location_.getLatitude()!=null)
            {*/
            progressDialog = new ProgressDialog(this);
            progressDialog.show();
            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        if (conn.SeeCart()) {
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
                    prod_id=Cartlistdata.getProductid_();
                    prod_name=Cartlistdata.getProductname_();

                    prod_imagepath=Cartlistdata.getProductimagepath_();
                    prod_quantity=Cartlistdata.getProd_quantity();
                    pro_tcost=Cartlistdata.getProduct_total_cost_();
                    sum_totalcost=Cartlistdata.getOverallcost();

                    Ordercartproduct.setPdid(prod_id);
                    Ordercartproduct.setPdid(pro_tcost);
                    Ordercartproduct.setPqnt(prod_quantity);



                 /*   if(offerid.isEmpty())
                    {
                        Toast.makeText(ViewofferlistActivity.this, "Data not found", Toast.LENGTH_SHORT).show();

                    }
                    else
                    {*/
                    Seecartadapter cartadapter=new Seecartadapter(Seecartlist.this,prod_id,prod_name,prod_imagepath,prod_quantity);
                    seecartlist.setAdapter(cartadapter);

                    /* }*/

                    break;
                case 1:
                    Toast.makeText(Seecartlist.this, "Something went wrong", Toast.LENGTH_SHORT).show();
                    break;
            }
        }

    };



    public void inserttransc()
    {
        final connectionManager conn = new connectionManager();
        if (conn.checkNetworkAvailable(Seecartlist.this)) {

            progressDialog = new ProgressDialog(Seecartlist.this);
            progressDialog.show();

            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        if(conn.inst_trans())
                        {
                            resp =0;
                        }
                         else
                        {
                            resp=1;
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    hd1.sendEmptyMessage(0);

                }
            };
            tthread.start();
        } else {
            Toast.makeText(Seecartlist.this,"Sorry no network access.", Toast.LENGTH_LONG).show();
        }

    }
    public Handler hd1 = new Handler() {
        public void handleMessage(Message msg) {

            if (progressDialog.isShowing())
                progressDialog.dismiss();

            switch (resp) {
                case 0:
                    //Toast.makeText(getApplicationContext(), "Order Successfully", Toast.LENGTH_LONG).show();
                    Intent intent=new Intent(Seecartlist.this,PaymentActivity.class);
                    startActivity(intent);

                    finish();
                    break;

                case 1:
                    Toast.makeText(getApplicationContext(), "Data not found", Toast.LENGTH_LONG).show();
                    break;

                case 3:
                    Toast.makeText(getApplicationContext(), "Try Later", Toast.LENGTH_LONG).show();
                    break;

                case 4:
                    Toast.makeText(getApplicationContext(), "Something Went Wrong", Toast.LENGTH_LONG).show();
                    break;
            }
        }
    };

    @Override
    public void onBackPressed()
    {
        // code here to show dialog
        Intent intent = new Intent(Seecartlist.this, MainActivity.class);
        startActivity(intent);
        super.onBackPressed();  // optional depending on your needs
    }

}
