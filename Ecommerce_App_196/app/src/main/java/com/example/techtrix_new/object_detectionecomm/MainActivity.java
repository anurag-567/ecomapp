package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.Cartlistdata;
import com.example.techtrix_new.object_detectionecomm.Data.Category_data;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;

import java.sql.RowId;
import java.util.ArrayList;

public class MainActivity extends Activity {

    Spinner  spcategory;
    Button btnLogout;
    ProgressDialog progressDialog;
    int resp;
    Dialog dg;

    ImageView  imgProd,imgCart;

    public static SharedPreferences myPreferences;
    SharedPreferences.Editor myEditor;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        spcategory=findViewById(R.id.spcategory);
        imgProd = findViewById(R.id.imgProd);
        imgCart = findViewById(R.id.imgCart);
        btnLogout=findViewById(R.id.btnLogout);

        myPreferences=getSharedPreferences("OnlineFebrication", Context.MODE_PRIVATE);
        myEditor=myPreferences.edit();

        fillcategory();



        imgProd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Category_data.setSelect_category(spcategory.getSelectedItem().toString().trim());
                Intent intent=new Intent(MainActivity.this,Productlistview.class);
                startActivity(intent);
            }
        });

        imgCart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                viewCart();
            }
        });

        btnLogout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                myEditor.putString("PhoneNo","");
                myEditor.putString("Password","");
                myEditor.apply();
                myEditor.commit();

                Intent intent=new Intent(MainActivity.this,Login.class);
                startActivity(intent);
                finish();
            }
        });


    }


    public void viewCart()
    {
        final connectionManager conn=new connectionManager();
        if(connectionManager.checkNetworkAvailable(this))
        {
            progressdialog dialog=new progressdialog();
            dg=dialog.createDialog(this);
            dg.show();

            Thread tthread=new Thread()
            {
                public void run()
                {
                    conn.SeeCart();
                    hd2.sendEmptyMessage(0);
                }
            };
            tthread.start();
        }
        else
        {
            Toast.makeText(this, "Sorry no network access.", Toast.LENGTH_LONG).show();
        }
    }
    public Handler hd2=new Handler()
    {
        public void handleMessage(Message msg)
        {
            dg.cancel();
            final ArrayList<String> Product_Id;
            Product_Id= Cartlistdata.getProductid_();
            if(Product_Id.isEmpty())
            {
                AlertDialog alert=new AlertDialog.Builder(MainActivity.this).create();
                alert.setTitle("No Record Found");
                alert.setMessage("No Cart Details found");
                alert.show();

            }
            else
            {
                Intent intent=new Intent(MainActivity.this,Seecartlist.class);
                startActivity(intent);
                finish();
            }
        }
    };




    public void fillcategory()
    {
        final connectionManager conn = new connectionManager();
        if (conn.checkNetworkAvailable(this)) {
            progressDialog = new ProgressDialog(MainActivity.this);
            progressDialog.show();

            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        if (conn.getCat()) {
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
        } else {
            Toast.makeText(getApplicationContext(), "Sorry no network access.", Toast.LENGTH_LONG).show();
        }

    }

    public Handler hd = new Handler() {
        public void handleMessage(Message msg) {

            if (progressDialog.isShowing())
                progressDialog.cancel();

            switch (resp) {
                case 0:
                    ArrayList<String> Names = new ArrayList<String>();
                    Names =Category_data.getName();

                    spcategory.setAdapter(new ArrayAdapter<String>(MainActivity.this,
                            android.R.layout.simple_spinner_dropdown_item, Names));

                    break;

                case 1:
                    Toast.makeText(getApplicationContext(), "data not received", Toast.LENGTH_LONG).show();

                    break;
            }
        }
    };

}
