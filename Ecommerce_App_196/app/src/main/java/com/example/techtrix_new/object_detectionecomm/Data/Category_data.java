package com.example.techtrix_new.object_detectionecomm.Data;

import java.util.ArrayList;

public class Category_data {

    public static ArrayList<String> id;
    public static ArrayList<String> name;
    public static String select_category;

    public static String getSelect_category() {
        return select_category;
    }

    public static void setSelect_category(String select_category) {
        Category_data.select_category = select_category;
    }

    public static ArrayList<String> getId() {
        return id;
    }

    public static void setId(ArrayList<String> id) {
        Category_data.id = id;
    }

    public static ArrayList<String> getName() {
        return name;
    }

    public static void setName(ArrayList<String> name) {
        Category_data.name = name;
    }
}
