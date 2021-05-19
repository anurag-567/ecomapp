package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

public class PaymentoptionActivity extends Activity {

    Button btncashpay,btnbankpay;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_paymentoption);
        btncashpay=findViewById(R.id.btncash);
        btnbankpay=findViewById(R.id.btnbankpayment);

        btncashpay.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Toast.makeText(PaymentoptionActivity.this, "Payment Successful..", Toast.LENGTH_SHORT).show();

                Intent intent=new Intent(PaymentoptionActivity.this,MainActivity.class);
                startActivity(intent);
            }
        });

        btnbankpay.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(PaymentoptionActivity.this,PaymentActivity.class);
                startActivity(intent);
            }
        });
    }
}
