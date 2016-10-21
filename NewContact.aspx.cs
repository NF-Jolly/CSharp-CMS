/*
Author: Chris Manek
Date: 2/8/2016
Description: NewContact.aspx.cs Creates and Manages Contact Information it Inserts or Updates the datbase and loads contacts into the form
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class NewContact : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       
        // Determine which control triggered post back
        Control ctrlNm = null;      
        string ctrlName = Page.Request.Params.Get("__EVENTTARGET");
        if (!String.IsNullOrEmpty(ctrlName)){
            ctrlNm = Page.FindControl(ctrlName);
        }
        ((MP)Master).MsgLog("NewCOntact", "PageLoad -" + ctrlName);
        Session["SaveMode"] = "";
        if (ctrlNm == null) 
        {
            // refresh only if post back is not from a control

            if (Session["Search"] != null)
            {
                //Load listbox 
                
            }

            if (Session["Refresh"] != null)
            {
                if (Session["contactId"] != null)
                {
                    // Load curent active contact
                    LoadContact();

                    bSaveContact.Text = "Update Contact";
                    bDelete.Visible = true;
                    bEvents.Visible = true;
                    bMore.Visible = true;
                    Session["SaveMode"] = "Update";

                }
                else
                {
                    bSaveContact.Text = "Save contact";
                    bEvents.Visible = false;
                    bMore.Visible = false; 
                    bDelete.Visible = false;
                }
            }
        }

   }

    public bool LoadContact()
    {
        // Get contact from DB using Session["contactId"] and load onto form maintance

        SqlConnection conn = ((MP)Master).OpenDB();
        conn.Open();
        SqlCommand command = conn.CreateCommand();

        command.CommandText = @"select lname,fname,"+
                    " Convert(varchar(10),DOB,21) as DOB,relationship "+
                    " from contacts where ID = " + Session["contactId"];

        SqlDataReader dr = command.ExecuteReader();

        lbContacts.Items.Clear();

        while (dr.Read())
        {
            tbLname.Text = dr["lname"].ToString().Trim();
            tbFname.Text = dr["fname"].ToString().Trim();

            tbRelationship.Text = dr["relationship"].ToString().Trim();

            string tmp;
           
            tmp = dr["DOB"].ToString();
            //var pos = tmp.IndexOf(' '); // we just want the date from m-d-yyyy hh:mm
            //tbDOB.Text = tmp.Substring(0,pos);
            tbDOB.Text =  tmp;

        }
        Session["Refresh"] = null;
        return (true);
    }
    
    protected void bSaveContact_Click(object sender, EventArgs e)
    {
        ((MP)Master).MsgLog("NewContact", "bSaveClick -" + bSaveContact.Text);
        if (tbLname.Text == ""){
            lStatus.Text = "---> Last name Required";
            return;
        }

         // using System.Data.SqlClient;  // Required for Sql commands
        SqlConnection conn = ((MP)Master).OpenDB(); // Use this if opening DB from content pages

        conn.Open();

        SqlCommand command = conn.CreateCommand();

        if (bSaveContact.Text == "Save Contact")//Save Contact to the Database
        {

                command.CommandText = @"Insert into contacts (lname,fname,DOB,relationship )
                       values ( '" +
                                   ((MP)Master).clean(tbLname.Text) + "','" +
                                   ((MP)Master).clean(tbFname.Text) + "','" +
                                   ((MP)Master).clean(tbDOB.Text) + "','" +
                                   ((MP)Master).clean(tbRelationship.Text) + "');";
        }

        if (bSaveContact.Text == "Update Contact"){//Update contact inside the database
                String dob, rel;
                dob = tbDOB.Text;
                rel = tbRelationship.Text;

                command.CommandText = "Update contacts   " +
                                   " set lname = '" + ((MP)Master).clean(tbLname.Text) + "'," +
                                   " fname = '" + ((MP)Master).clean(tbFname.Text) + "'," +
                                   " DOB = '" + ((MP)Master).clean(tbDOB.Text) + "'," +
                                   " relationship = '" + ((MP)Master).clean(tbRelationship.Text) + "'" +
                                   " where ID = " + Session["contactId"];
           
        }

        try{

          command.ExecuteNonQuery();
            lStatus.Text = "---> Contact saved";
        }
        catch (Exception ex)
        {
           
            lStatus.Text = lStatus.Text = "**" + ex.Message + " " + command.CommandText;

        }

        if (bSaveContact.Text == "Save contact") { //Save the contact
            command.CommandText = "Select @@Identity";
            string ID;
            ID = command.ExecuteScalar().ToString();
            

        }

        conn.Close();
        tbLname.Text = "";
        tbFname.Text = "";
        Session["contactId"] = null;
        Session["Refresh"] = "YES"; // Reset for next contact
        Response.Redirect("~/NewContact.aspx");
    }

    protected void tbLname_TextChanged(object sender, EventArgs e)
    {
        string tmp;
         tmp = tbLname.Text;

         ((MP)Master).MsgLog("NewContact", "TextChanged -" + bSaveContact.Text);
         if (bSaveContact.Text == "Update Contqact")
         {
             return;
         }

         SqlConnection conn = ((MP)Master).OpenDB();
         conn.Open();
         SqlCommand command = conn.CreateCommand();

         command.CommandText = @"select ID,lname,fname from contacts where lname >= '" +
                             tmp + "' and lname < '" +
                             tmp + "zzz' order by lname,fname;";

         Session["Search"] = tmp; // Save current search string for repost
         SqlDataReader dr = command.ExecuteReader();
         lbContacts.Items.Clear();
         while (dr.Read())
         {

             ListItem li = new ListItem(dr["lname"] + ", " + dr["fname"],
               dr["ID"].ToString());
             lbContacts.Items.Add(li);
           
         }

         conn.Close();

         tbFname.Focus();
      
    }
    protected void lbContacts_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["contactId"] = null; // Initialize if non selected

        foreach (ListItem item in lbContacts.Items)
        {
            if (item.Selected)
            {
                Session["contactId"] = item.Value.ToString();

                Session["Refresh"] = "YES";
                // Set flag to trigger repost
                // After contact selected from list box 
                Response.Redirect("~/NewContact.aspx");

                return;
            }

        }          

    }
    
    protected void bMore_Click(object sender, EventArgs e)
    {
        bSaveContact_Click(sender, e);
        Session["Refresh"] = "YES";
                        // Set flag to trigger selecting contact 
                        // upon return from contactInfo 
        Response.Redirect("~/ContactInfo.aspx");

    }
    protected void bDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VerifyDelete.aspx");//Redirect to VerifyDelete page

    }

    protected void bEvents_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Events.aspx");//Redirect to Events page
    }
}