package com.example.techtrix_new.object_detectionecomm.Data;

import java.util.ArrayList;

public class Product_data {

    public static ArrayList<String> productid_,productname_,productimagepath_,productcost_,categoryid_;

    public static ArrayList<String> getProductid_() {
        return productid_;
    }

    public static void setProductid_(ArrayList<String> productid_) {
        Product_data.productid_ = productid_;
    }

    public static ArrayList<String> getProductname_() {
        return productname_;
    }

    public static void setProductname_(ArrayList<String> productname_) {
        Product_data.productname_ = productname_;
    }

    public static ArrayList<String> getProductimagepath_() {
        return productimagepath_;
    }

    public static void setProductimagepath_(ArrayList<String> productimagepath_) {
        Product_data.productimagepath_ = productimagepath_;
    }

    public static ArrayList<String> getProductcost_() {
        return productcost_;
    }

    public static void setProductcost_(ArrayList<String> productcost_) {
        Product_data.productcost_ = productcost_;
    }

    public static ArrayList<String> getCategoryid_() {
        return categoryid_;
    }

    public static void setCategoryid_(ArrayList<String> categoryid_) {
        Product_data.categoryid_ = categoryid_;
    }
}
