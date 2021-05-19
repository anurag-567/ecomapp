package com.example.techtrix_new.object_detectionecomm;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.techtrix_new.object_detectionecomm.Data.fillcust_Request;
import com.example.techtrix_new.object_detectionecomm.connectivity.connectionManager;

public class Login extends Activity
{

    Button btnlogin;
    TextView tvGoToSignup;

    Dialog dg;
    int resp;
    Context context;

    public static SharedPreferences myPreferences;
    SharedPreferences.Editor myEditor;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        btnlogin=findViewById(R.id.btnlogin);
        tvGoToSignup = findViewById(R.id.tvGotoSignup);

        myPreferences=getSharedPreferences("OnlineFebrication", Context.MODE_PRIVATE);
        myEditor=myPreferences.edit();

        String PhoneNo=myPreferences.getString("PhoneNo", "");
        String Password=myPreferences.getString("Password", "");

        if(!PhoneNo.isEmpty())
        {
            fillcust_Request.setPhoneNo(PhoneNo);
            fillcust_Request.setPassword(Password);

            StoredLogin();
        }


        btnlogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                UserLogin();
            }
        });
        tvGoToSignup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent=new Intent(Login.this,Registration.class);
                startActivity(intent);
            }
        });

    }
    public void UserLogin()
    {
        EditText txtPhoneNo=(EditText)findViewById(R.id.editemail);
        EditText txtPassword=(EditText)findViewById(R.id.editpassword);

        String PhoneNo=txtPhoneNo.getText().toString().trim();
        String Password=txtPassword.getText().toString().trim();
        if(PhoneNo.equals("")||Password.equals(""))
        {
            AlertDialog alert=new AlertDialog.Builder(this).create();
            alert.setTitle("Enter All Details");
            alert.setMessage("All Fields Are Mandatory");
            alert.show();
        }
        else
        {

            final connectionManager conn=new connectionManager();
            if(connectionManager.checkNetworkAvailable(this))
            {

                fillcust_Request.setPhoneNo(PhoneNo);
                fillcust_Request.setPassword(Password);

                progressdialog dialog=new progressdialog();
                dg=dialog.createDialog(this);
                dg.show();

                Thread tthread=new Thread()
                {
                    public void run()
                    {
                        if(conn.Login())
                        {
                            resp=0;
                        }
                        else
                        {
                            resp=1;
                        }
                        hd.sendEmptyMessage(0);
                    }
                };
                tthread.start();

            }
            else
            {
                Toast.makeText(this, "Sorry no network access.", Toast.LENGTH_LONG).show();
            }

        }

    }

    Handler hd=new Handler()
    {
        public void handleMessage(Message msg)
        {
            EditText txtPhoneNo=findViewById(R.id.editemail);
            EditText txtPassword=findViewById(R.id.editpassword);

            dg.cancel();
            switch (resp) {
                case 0:

                    myEditor.putString("PhoneNo", fillcust_Request.getPhoneNo());
                    myEditor.putString("Password", fillcust_Request.getPassword());
                    myEditor.commit();

                    Intent intent=new Intent(Login.this,MainActivity.class);
                    startActivity(intent);
                    finish();
                    break;

                case 1:
                    Toast.makeText(getApplicationContext(), "Invalid Phone Number Or Password", Toast.LENGTH_LONG).show();
                    break;
            }
        }
    };


    public void StoredLogin()
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
                    if(conn.Login())
                    {
                        resp=0;
                    }
                    else
                    {
                        resp=1;
                    }
                    hd.sendEmptyMessage(0);
                }
            };
            tthread.start();

        }
        else
        {
            Toast.makeText(this, "Sorry no network access.", Toast.LENGTH_LONG).show();
        }
    }


   /* @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    @Override
    public void onBackPressed() {

        Intent intent=new Intent(Intent.ACTION_MAIN);
        intent.addCategory(Intent.CATEGORY_HOME);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
        finish();

    }*/
}
