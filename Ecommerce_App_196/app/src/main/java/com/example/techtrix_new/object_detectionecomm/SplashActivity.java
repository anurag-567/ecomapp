package com.example.techtrix_new.object_detectionecomm;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class SplashActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);

        try
        {
            Thread thread=new Thread()
            {
                public void run()
                {
                    try
                    {
                        sleep(3000);
                    }
                    catch (InterruptedException e)
                    {
                        e.printStackTrace();
                    }
                    finally
                    {
                        finish();
                        startActivity(new Intent(SplashActivity.this,Login.class));
                    }
                }
            };
            thread.start();
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }
}
