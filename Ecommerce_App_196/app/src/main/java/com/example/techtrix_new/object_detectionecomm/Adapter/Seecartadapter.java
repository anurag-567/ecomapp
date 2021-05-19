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

public class Seecartadapter extends ArrayAdapter<String> {
    private Activity context;
    private ArrayList<String> cart_Product_Id;
    private ArrayList<String> cart_ProductName;
    private ArrayList<String> cart_ImagePath;
    private ArrayList<String> cart_qty;

    public Seecartadapter(Activity context,ArrayList<String> cart_Pro_Id,ArrayList<String> cart_ProductName,ArrayList<String> cart_ImagePath,
                          ArrayList<String> cart_qty) {
        super(context,R.layout.seecart_adapter,cart_Pro_Id);
        // TODO Auto-generated constructor stub

        this.context=context;
        this.cart_Product_Id=cart_Pro_Id;
        this.cart_ProductName=cart_ProductName;
        this.cart_ImagePath=cart_ImagePath;
        this.cart_qty = cart_qty;

    }
    @Override
    public View getView(int position, View view, ViewGroup parent)
    {
        LayoutInflater inflater = context.getLayoutInflater();
        View rowView = inflater.inflate(R.layout.seecart_adapter, null, true);


        ImageView imageView=(ImageView)rowView.findViewById(R.id.cartimage);
        TextView textViewname=(TextView)rowView.findViewById(R.id.txtcartname);
        TextView txtQty = rowView.findViewById(R.id.txtQty);

        textViewname.setText(cart_ProductName.get(position));
        txtQty.setText("Quantity: "+cart_qty.get(position));



        Picasso.with(context)
                .load("http://demoproject.in/Ecommerce_website/"+cart_ImagePath.get(position))

                .placeholder(R.drawable.ic_launcher_background)
                .error(R.drawable.ic_launcher_foreground)
                .resize(300,100)
                .into(imageView);


        //end code for picasso

        return rowView;
    }

}
