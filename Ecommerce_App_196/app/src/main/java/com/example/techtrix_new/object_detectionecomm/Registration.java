package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.Customer_data;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Registration extends Activity {


    EditText name,emailid,contact_no,address,password,re_password;
    Button register;

    ProgressDialog dg;
    int resp;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registration);

        name=findViewById(R.id.editname);
        emailid=findViewById(R.id.editemail);
        contact_no=findViewById(R.id.editcontact);
        address=findViewById(R.id.editaddress);
        password=findViewById(R.id.editpassword);
        re_password=findViewById(R.id.editrepassword);

        register=findViewById(R.id.btnregister);


        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                registrationvalidation();
            }
        });

    }

    public void registrationvalidation()
    {

        final String cust_name=name.getText().toString().trim(),
                     cust_email=emailid.getText().toString().trim(),
                     cust_contact=contact_no.getText().toString().trim(),
                     cust_address=address.getText().toString().trim(),
                     cust_password=password.getText().toString().trim(),
                     cust_repassword=re_password.getText().toString().trim();


        final EditText[] Alledit={name,emailid,contact_no,address,password,re_password};

        for (EditText edit:Alledit)
        {
            if (edit.getText().toString().trim().length()==0)
            {
                edit.setError("Empty Field");
                edit.requestFocus();
            }
        }

        if (!isValidPh(cust_contact)) {
            contact_no.setError("Invalid Contact Number");
        } else if (!isValidEmail(cust_email)) {
            emailid.setError("Invalid Mail Id");
        }  else if (cust_password.length() < 4) {
            password.setError("Password Length must be atleast 4");
        } else if (!cust_password.equals(cust_repassword)) {
            re_password.setError("Re-Password is not match");
        }
         else
        {
            Customer_data.setName(cust_name);
            Customer_data.setEmail(cust_email);
            Customer_data.setAddress(cust_address);
            Customer_data.setContact(cust_contact);
            Customer_data.setPassword(cust_password);
            Customer_data.setRepassword(cust_repassword);

            register();
        }
    }

    public void register()
    {
        final connectionManager conn = new connectionManager();
        if (conn.checkNetworkAvailable(Registration.this)) {
            dg = new ProgressDialog(Registration.this);
            dg.setMessage("Processing ....");
            dg.show();

            Thread tthread = new Thread() {
                @Override
                public void run() {
                    try {
                        resp = conn.register();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    hd.sendEmptyMessage(0);

                }
            };
            tthread.start();
        } else {
            Toast.makeText(Registration.this,"Sorry no network access.", Toast.LENGTH_LONG).show();
        }

    }
    public Handler hd = new Handler() {
        public void handleMessage(Message msg) {

            if (dg.isShowing())
                dg.dismiss();

            switch (resp) {
                case 1:
                    Toast.makeText(getApplicationContext(), "Register Successfully", Toast.LENGTH_LONG).show();
                    Intent intent=new Intent(Registration.this,Login.class);
                    startActivity(intent);

                    finish();
                    break;

                case 2:
                    Toast.makeText(getApplicationContext(), "Contact or Mail Id already exists", Toast.LENGTH_LONG).show();
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



    private boolean isValidPh(String ph) {
        //^[7-9][0-9]{9}$
        String EMAIL_PATTERN = "^[7-9][0-9]{9}$";
        Pattern pattern = Pattern.compile(EMAIL_PATTERN);
        Matcher matcher = pattern.matcher(ph);
        return matcher.matches();
    }

    private boolean isValidEmail(String email) {
        String EMAIL_PATTERN = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@"
                + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";

        Pattern pattern = Pattern.compile(EMAIL_PATTERN);
        Matcher matcher = pattern.matcher(email);
        return matcher.matches();
    }

    private boolean isValidUname(String name) {
        String N_Pattern = "^([A-Za-z\\+]+[A-Za-z0-9]{1,10})$";
        Pattern pattern = Pattern.compile(N_Pattern);
        Matcher matcher = pattern.matcher(name);
        return matcher.matches();
    }
}
