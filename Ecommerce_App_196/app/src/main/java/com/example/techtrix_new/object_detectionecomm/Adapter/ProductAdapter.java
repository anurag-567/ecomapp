package com.example.techtrix_new.object_detectionecomm.Adapter;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.techtrix_new.object_detectionecomm.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;

public class ProductAdapter extends ArrayAdapter<String> {
    private Activity context;
    private ArrayList<String> Product_Id;
    private ArrayList<String> ProductName;
    private ArrayList<String> ImagePath;


    public ProductAdapter(Activity context,ArrayList<String> Product_Id,ArrayList<String> ProductName,ArrayList<String> ImagePath) {
        super(context,R.layout.productlist_adapter,Product_Id);
        // TODO Auto-generated constructor stub

        this.context=context;
        this.Product_Id=Product_Id;
        this.ProductName=ProductName;
        this.ImagePath=ImagePath;

    }
    @Override
    public View getView(int position, View view, ViewGroup parent)
    {
        LayoutInflater inflater = context.getLayoutInflater();
        View rowView = inflater.inflate(R.layout.productlist_adapter, null, true);


        ImageView imageView=(ImageView)rowView.findViewById(R.id.image);
        TextView textViewname=(TextView)rowView.findViewById(R.id.txtname);

        textViewname.setText(ProductName.get(position));


        Picasso.with(context)
                .load("http://demoproject.in/Ecommerce_website/"+ImagePath.get(position))

                .placeholder(R.drawable.ic_launcher_background)
                .error(R.drawable.ic_launcher_foreground)
                .resize(300,100)
                .into(imageView);


        //end code for picasso

        return rowView;
    }

}
