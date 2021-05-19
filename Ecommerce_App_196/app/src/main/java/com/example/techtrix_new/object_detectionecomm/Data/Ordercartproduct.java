package com.example.techtrix_new.object_detectionecomm.Data;

import java.util.ArrayList;

public class Ordercartproduct {

    public static ArrayList<String> cid,pdid,ptcost,pqnt,qty;

    public static ArrayList<String> getPqnt() {
        return pqnt;
    }

    public static void setPqnt(ArrayList<String> pqnt) {
        Ordercartproduct.pqnt = pqnt;
    }

    public static ArrayList<String> getCid() {
        return cid;
    }

    public static void setCid(ArrayList<String> cid) {
        Ordercartproduct.cid = cid;
    }

    public static ArrayList<String> getPdid() {
        return pdid;
    }

    public static void setPdid(ArrayList<String> pdid) {
        Ordercartproduct.pdid = pdid;
    }

    public static ArrayList<String> getPtcost() {
        return ptcost;
    }

    public static void setPtcost(ArrayList<String> ptcost) {
        Ordercartproduct.ptcost = ptcost;
    }

    public static ArrayList<String> getQty() {
        return qty;
    }

    public static void setQty(ArrayList<String> qty) {
        Ordercartproduct.qty = qty;
    }
}
