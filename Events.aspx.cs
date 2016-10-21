/*
Author: Chris Manek
Date: 2/8/2016
Description: Events.aspx.cs Lets you Schedule events and lets you either update or insert new contact information into the database.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Events : System.Web.UI.Page
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

        ((MP)Master).MsgLog("contactInfo", "PageLoad -" + ctrlName);
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

        command.CommandText = @"select ID,lname,fname,relationship from contacts where " +
                                "ID = " + ID + ";";

        using (SqlDataReader dr = command.ExecuteReader())
        {

            // Display current active contact's name and relationship
            lErrMsg.Text = Session["ErrMsg"].ToString();
            while (dr.Read())
            {

                lName.Text = dr["lname"] + ", " + dr["fname"];
                lRelationship.Text = dr["relationship"].ToString();

            }
        }

        if (Session["EventId"] != null
        && bSave.Text != "Update Event" )
        {  // Maintaining a existing event
            command.CommandText = @"select CONVERT(char(10), eventDate, 21) as Date,"
                                +"startTime ,stopTime,"
                                +"subject,location ,description  from events where " +
                                "eventID = " + Session["EventId"] ;

            using (SqlDataReader dr = command.ExecuteReader())
            {

                string tmp;
                while (dr.Read())
                {
         
                    tbDate.Text = dr["Date"].ToString();

                            // HTML5 date format REQUIRIES yyyy-mm-dd format
                            // which is returned by date format 21 in above SQL

                    tbStartTime.Text = dr["startTime"].ToString();
                    tbEndTime.Text = dr["stopTime"].ToString();
                    tbSubject.Text = dr["subject"].ToString();
                    tbLocation.Text = dr["location"].ToString();
                    taDescription.Value = dr["description"].ToString();
                    bSave.Text = "Update Event";
                    bDelete.Visible = true;
                }
            }

        }


        conn.Close();

    }

    protected void bBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/NewContact.aspx");  // Back to NewContact
    }

    protected void bSave_Click(object sender, EventArgs e)
    {

        SqlConnection conn = ((MP)Master).OpenDB();
        conn.Open();
        SqlCommand command = conn.CreateCommand();

        if (bSave.Text == "Update Event")//Update event if not insert new data for the new event
        {
            command.CommandText = @"Update events Set  "
                             +" eventDate = '"+ ((MP)Master).clean(tbDate.Text) +"' ,"
                             +" startTime = '"+ ((MP)Master).clean(tbStartTime.Text)+"' ,"
                             +" stopTime = '"+((MP)Master).clean(tbEndTime.Text) +"' ,"
                             +" subject = '"+((MP)Master).clean(tbSubject.Text)  +"' ,"
                             +" location = '"+((MP)Master).clean(tbLocation.Text) +"' ,"
                             +" description = '"+ ((MP)Master).clean(taDescription.Value) +"' "
                             + " where id = "+ Session["contactId"].ToString()
                             + " and eventId = "+Session["EventId"];
                                
                    Session["ErrMsg"] = "Event Updated";
        }
        else
        {

            command.CommandText = @"Insert into events 
                                ( ID , eventDate,
                                startTime ,stopTime,
                                subject,location ,description  )
                                
          values ( '" + Session["contactId"].ToString() + "','"
                                            + ((MP)Master).clean(tbDate.Text) + "','"
                                            + ((MP)Master).clean(tbStartTime.Text) + "','"
                                            + ((MP)Master).clean(tbEndTime.Text) + "','"
                                            + ((MP)Master).clean(tbSubject.Text) + "','"
                                            + ((MP)Master).clean(tbLocation.Text) + "','"
                                            + ((MP)Master).clean(taDescription.Value) + "');";
           Session["ErrMsg"] = "Event saved";
        }



        if (command.ExecuteNonQuery() != 1)
        {
            ((MP)Master).MsgLog("Event-save event", command.CommandText);
            Session["ErrMsg"] = "Unable to save/Update  event";
        }
        else
        {
 
            tbSubject.Text = "";
            tbLocation.Text = "";
            taDescription.Value = "";
            Session["EventId"] = null;
            
            if (bSave.Text == "Update Event")
            {
                bSave.Text = "Save";
                Response.Redirect("~/Schedule.aspx");
            }
            else
            {
                Response.Redirect("~/Events.aspx");
            }
        }

    }
    protected void bDelete_Click(object sender, EventArgs e)//Delete event
    {

        SqlConnection conn = ((MP)Master).OpenDB();
        conn.Open();
        SqlCommand command = conn.CreateCommand();

        command.CommandText = @"Delete from events "
                             + " where id = " + Session["contactId"].ToString()
                             + " and eventId = " + Session["EventId"];
        Session["ErrMsg"] = "Event Deleted";
        

        if (command.ExecuteNonQuery() != 1)
        {
            ((MP)Master).MsgLog("Delete event", command.CommandText);
            Session["ErrMsg"] = "Unable to delete event";
        }
        else
        {

            tbSubject.Text = "";
            tbLocation.Text = "";
            taDescription.Value = "";
            Session["EventId"] = null;
            bSave.Text = "Save";
            Response.Redirect("~/Schedule.aspx");

        }

    }
}