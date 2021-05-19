package com.example.techtrix_new.object_detectionecomm;

import android.app.AlertDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.fillcust_Request;

public class DetailsActivity extends AppCompatActivity {

    TextView txtName,txtContact,txtAddress;
    Button btnConfirm;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_details);

        txtName = findViewById(R.id.txtDName);
        txtContact = findViewById(R.id.txtDPhone);
        txtAddress = findViewById(R.id.txtDAddress);
        btnConfirm = findViewById(R.id.btnConfirm);

        txtName.setText(fillcust_Request.getCustName());
        txtContact.setText(fillcust_Request.getPhoneNo());
        txtAddress.setText(fillcust_Request.getAddress());

        btnConfirm.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                pay();
            }
        });
    }

    public void pay()
    {

        Toast.makeText(this, "Payment Successfull..,Product Deliver In One Day", Toast.LENGTH_SHORT).show();

        Intent intent=new Intent(DetailsActivity.this,MainActivity.class);
        startActivity(intent);

    }
}
