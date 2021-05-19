package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.Cartlistdata;

public class PaymentActivity extends Activity {

    EditText editaccountnumber,editifscnum,edittotalcost;
    Button btnpayment;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_payment);

        editaccountnumber=findViewById(R.id.editaccount);
        editifscnum=findViewById(R.id.editifsc);
        edittotalcost=findViewById(R.id.edittotalamount);
        edittotalcost.setText("Rs. "+ Cartlistdata.getOverall_cost());
        btnpayment=findViewById(R.id.btnpay);

        btnpayment.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                pay();
            }
        });




    }

    public void pay()
    {
        String AccountNo=editaccountnumber.getText().toString().trim();
        String IFSC=editifscnum.getText().toString().trim();
        if(AccountNo.equals("")||IFSC.equals(""))
        {
            AlertDialog alert=new AlertDialog.Builder(this).create();
            alert.setTitle("Enter All Details");
            alert.setMessage("All Fields Are Mandatory");
            alert.show();
        }
        else
        {
            Intent intent=new Intent(PaymentActivity.this,DetailsActivity.class);
            startActivity(intent);
        }
    }

}
