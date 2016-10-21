
/*
Author: Chris Manek
Date: 2/8/2016
Description: ContactInfo.aspx.cs Displays Phone numbers
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; 

public partial class ContactInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID;

        if (Session["ErrMsg"] == null)
        {
            Session["ErrMsg"] = "";
        }

        // Determine which control triggered post back
        Control ctrlNm = null;
        string ctrlName = Page.Request.Params.Get("__EVENTTARGET");
        if (!String.IsNullOrEmpty(ctrlName))
        {
            ctrlNm = Page.FindControl(ctrlName);
        }

        ((MP)Master).MsgLog("contactInfo", "PageLoad -"+ctrlName);
        ID = "";
        try
        {
            ID = Session["contactId"].ToString();
            Session["Refresh"] = "YES";  // Keeps this contact active when they go back to newcontact
        }
        catch (Exception ex)
        {
            ((MP)Master).MsgLog("contactInfo", "Redirect to newContact.aspx " + ex.Message);
            
            Response.Redirect("~/NewContact.aspx");  // If they did not come from NewContact
                                                     // Send them packing back

        }
        SqlConnection conn = ((MP)Master).OpenDB();
        conn.Open();
        SqlCommand command = conn.CreateCommand();

        command.CommandText = @"select ID,lname,fname,relationship from contacts where "+
                                "ID = "+ID+";";

        SqlDataReader dr = command.ExecuteReader();

        // Display current active contact's name and relationship
        lErrMsg.Text = Session["ErrMsg"].ToString();
        while (dr.Read())
        {

            lName.Text = dr["lname"] + ", " + dr["fname"];
            lRelationship.Text =  dr["relationship"].ToString();
   
        }

       


        conn.Close();

    }
    protected void tbPhoneNo_TextChanged(object sender, EventArgs e)
    {
        // Add Phone number and reload page
        // using System.Data.SqlClient;  // Required for Sql commands

        Session["ErrMsg"] = "";

        SqlConnection conn = ((MP)Master).OpenDB(); // Use this if opening DB from content pages

        conn.Open();
        SqlCommand command = conn.CreateCommand();

        command.CommandText = @"Insert into PhoneNos (  ID,type,phoneNo )"+
                   "values ( "+ Session["contactId"].ToString()+",'"+
                             ddType.Text+"','"+
                             tbPhoneNo.Text+ "');";
        ((MP)Master).MsgLog("contactInfo-PhoneNo-" , command.CommandText);

        int rowCnt = 0;
        try
        {
            rowCnt = command.ExecuteNonQuery();

        }
        catch (Exception ex)
        {

            Session["ErrMsg"] = "Can not save multiple " + ddType.Text + " numbers";
  
        }

        Response.Redirect("~/ContactInfo.aspx");
    }
    protected void tbZip_TextChanged(object sender, EventArgs e)
    {

        Session["ErrMsg"] = "";
       
        SqlConnection conn = ((MP)Master).OpenDB(); // Use this if opening DB from content pages

        conn.Open();
        SqlCommand command = conn.CreateCommand();

        command.CommandText = @"Insert into Addresses (  ID,type,mailStop,streetAddress,City,St,Zip )"+
                   "values ( " + Session["contactId"].ToString() + ",'" +
                             ddAddress.Text + "','" +
                             tbMailStop.Text + "','" +
                             tbStreet.Text + "','" +
                             tbCity.Text + "','" +
                             tbSt.Text + "','" +
                             tbZip.Text + "');";

        

        int rowCnt = 0;
        try
        {
            rowCnt = command.ExecuteNonQuery();

        }
        catch (Exception ex)
        {

            Session["ErrMsg"] = "Can not save multiple " + ddAddress.Text + " addresses ";
               
        }


        
            // Repost page to update gridview
            Response.Redirect("~/ContactInfo.aspx");
        


    }
    protected void bBack_Click(object sender, EventArgs e)
    {

      Response.Redirect("~/NewContact.aspx");  // Back to NewContact
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}