package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.NumberPicker;
import android.widget.TextView;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.buy_product;
import com.example.techtrix_new.object_detectionecomm.Data.fillcust_Request;
import com.example.techtrix_new.object_detectionecomm.Data.selected_prod;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;
import com.squareup.picasso.Picasso;

public class Buy_product extends Activity {

    ImageView prod_image;
    TextView textname,textcost,textquant;
    Button btnbuy;
    NumberPicker NP;

    ProgressDialog dg;
    int resp;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_buy_product);

        prod_image=findViewById(R.id.productimage);
        textname=findViewById(R.id.textproductname);
        textcost=findViewById(R.id.textproductcost);

        textquant=findViewById(R.id.textquantity);
        NP=findViewById(R.id.numberPicker1);




        NP.setMaxValue(10);
        NP.setMinValue(1);
        NP.setWrapSelectorWheel(false);

        NP.setOnValueChangedListener(new NumberPicker.OnValueChangeListener() {
            @Override
            public void onValueChange(NumberPicker picker, int oldVal, int newVal) {

                textquant.setText(String.valueOf(newVal));
                buy_product.setBuy_quantity(textquant.getText().toString().trim());
            }
        });

        btnbuy=findViewById(R.id.btnaddcart);




        Picasso
                .with(Buy_product.this)
                .load("http://demoproject.in/Ecommerce_website/"+ selected_prod.getSel_prod_imagepath())
                .placeholder(R.drawable.ic_launcher_background)
                .error(R.drawable.ic_launcher_foreground)
                .into(prod_image);

        String amt = "Rs : " + selected_prod.getSel_prod_cost();
        textname.setText(selected_prod.getSel_prod_name());
        textcost.setText(amt);


        buy_product.setBuy_prodid(selected_prod.getSel_prod_id());
        buy_product.setBuy_custid(fillcust_Request.getC_Id());
        buy_product.setBuy_status("Booked");



        btnbuy.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Buy_product();
            }
        });




    }


    public void Buy_product()
    {
        final connectionManager conn=new connectionManager();
        if (conn.checkNetworkAvailable(Buy_product.this))
        {
            dg=new ProgressDialog(Buy_product.this);
            dg.setMessage("Processing ....");
            dg.show();
            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        resp = conn.buy_product();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    hd.sendEmptyMessage(0);

                }
            };
            tthread.start();

        }
        else
        {
            Toast.makeText(Buy_product.this,"Sorry no network access.", Toast.LENGTH_LONG).show();
        }
    }
    public Handler hd = new Handler() {
        public void handleMessage(Message msg) {

            if (dg.isShowing())
                dg.dismiss();

            switch (resp) {
                case 1:
                    Toast.makeText(getApplicationContext(), "Added Successfully", Toast.LENGTH_LONG).show();
                    finish();
                    break;

                case 2:
                    Toast.makeText(getApplicationContext(), "Added Successfully", Toast.LENGTH_LONG).show();
                    break;

                case 3:
                    Toast.makeText(getApplicationContext(), "Try Later", Toast.LENGTH_LONG).show();
                    break;

                case 0:
                    Toast.makeText(getApplicationContext(), "Something Went Wrong", Toast.LENGTH_LONG).show();
                    break;
            }
        }
    };
}
