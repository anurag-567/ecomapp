package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.NumberPicker;
import android.widget.TextView;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.buy_product;
import com.example.techtrix_new.object_detectionecomm.Data.selectcart_prod;
import com.example.techtrix_new.object_detectionecomm.Data.selected_prod;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;
import com.squareup.picasso.Picasso;

public class Selected_cartdata extends Activity {

    ImageView pro_image;
    TextView txtpro_name,txtprod_cost,txtprod_qut;
    Button btnorder,btndelete;
    NumberPicker NP;
    ProgressDialog dg;

    int resp;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_selected_cartdata);

        pro_image=findViewById(R.id.productimage);
        txtpro_name=findViewById(R.id.textproductname);
        txtprod_cost=findViewById(R.id.prototalcost);
        txtprod_qut=findViewById(R.id.textquantity);
        btnorder=findViewById(R.id.btnbuy);
        btndelete=findViewById(R.id.btndelete);

        NP=findViewById(R.id.numberPicker1);

        NP.setMaxValue(100);
        NP.setMinValue(1);
        NP.setWrapSelectorWheel(false);

        NP.setOnValueChangedListener(new NumberPicker.OnValueChangeListener() {
            @Override
            public void onValueChange(NumberPicker picker, int oldVal, int newVal) {

                txtprod_qut.setText(String.valueOf(newVal));
                buy_product.setBuy_quantity(txtprod_qut.getText().toString().trim());
            }
        });


        Picasso
                .with(Selected_cartdata.this)
                .load("http://demoproject.in/Ecommerce_website/"+ selectcart_prod.getSel_cartprod_imagepath())
                .placeholder(R.drawable.ic_launcher_background)
                .error(R.drawable.ic_launcher_foreground)
                .into(pro_image);

        String amt = "Rs : " + selectcart_prod.getSel_cartprod_initailcost();
        txtpro_name.setText(selectcart_prod.getSel_cartprod_name());
        txtprod_cost.setText(amt);
        txtprod_qut.setText(selectcart_prod.getSel_catprod_quantity());

        btnorder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Order_product();
            }
        });

        btndelete.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                DeleteProduct();
            }
        });
    }

    public void Order_product()
    {
        final connectionManager conn=new connectionManager();
        if (conn.checkNetworkAvailable(Selected_cartdata.this))
        {
            dg=new ProgressDialog(Selected_cartdata.this);
            dg.setMessage("Processing ....");
            dg.show();
            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        resp = conn.order_product();
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
            Toast.makeText(Selected_cartdata.this,"Sorry no network access.", Toast.LENGTH_LONG).show();
        }
    }
    public Handler hd = new Handler() {
        public void handleMessage(Message msg) {

            if (dg.isShowing())
                dg.dismiss();

            switch (resp) {
                case 1:
                    Toast.makeText(getApplicationContext(), "Save Successfully", Toast.LENGTH_LONG).show();
                    Intent intent=new Intent(Selected_cartdata.this,MainActivity.class);
                    startActivity(intent);
                    finish();
                    break;

                case 2:
                    Toast.makeText(getApplicationContext(), "Save Successfully", Toast.LENGTH_LONG).show();
                    Intent intent1=new Intent(Selected_cartdata.this,MainActivity.class);
                    startActivity(intent1);
                    finish();
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


    public void DeleteProduct() {
        DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                switch (which) {
                    case DialogInterface.BUTTON_POSITIVE:
                        // Yes button clicked
                        //Toast.makeText(Product_Details_Page.this, "Yes Clicked",Toast.LENGTH_LONG).show();

                        final connectionManager conn=new connectionManager();
                        if(connectionManager.checkNetworkAvailable(Selected_cartdata.this))
                        {

                            //progressdialog dialog1=new progressdialog();
                            dg=new ProgressDialog(Selected_cartdata.this);
                            dg.show();

                            Thread tthread=new Thread()
                            {
                                public void run()
                                {
                                    conn.DeleteProduct();
                                    hd2.sendEmptyMessage(0);
                                }
                            };
                            tthread.start();
                        }
                        else
                        {
                            Toast.makeText(Selected_cartdata.this, "Sorry no network access.", Toast.LENGTH_LONG).show();
                        }


                        break;

                    case DialogInterface.BUTTON_NEGATIVE:
                        // No button clicked
                        // do nothing
                        Toast.makeText(Selected_cartdata.this, "Canceled",
                                Toast.LENGTH_LONG).show();
                        break;
                }
            }
        };

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setMessage("Are you sure for delete product from cart?")
                .setPositiveButton("Yes", dialogClickListener)
                .setNegativeButton("No", dialogClickListener).show();
    }

    public Handler hd2=new Handler()
    {
        public void handleMessage(Message msg)
        {
            dg.cancel();
            Toast.makeText(getApplicationContext(), "Deleted Successfully", Toast.LENGTH_LONG).show();
            Intent intent=new Intent(Selected_cartdata.this,MainActivity.class);
            startActivity(intent);
            finish();
        }
    };
}
