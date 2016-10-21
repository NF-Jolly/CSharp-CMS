/*
Author: Chris Manek
Date: 2/8/2016
Description: VerifyDelete.aspx.cs Verifies a user so it can be deleted successfully
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class VerifyDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["contactId"] != null)
            {
                SqlConnection conn = ((MP)Master).OpenDB();
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"select lname,fname,relationship,count(*) as count  "
                                    + " from contacts Left Join events "
                                    + " ON contacts.ID = events.ID "
                                    + " where  "
                                    + " contacts.ID = " + Session["contactId"]
                                    + " group by lname,fname,relationship ";

                SqlDataReader dr = command.ExecuteReader();

                string tmp;

                while (dr.Read())
                {
                    tmp = @"Delete " + dr["lname"].ToString() + ", " +
                              dr["fname"].ToString() + " (" + dr["relationship"].ToString() + ") Has "
                                + dr["count"].ToString() + " Event(s) scheduled" ;
                    lDeleteMsg.Text = tmp;
                }
            }
        }
    }
    //Deletes a User from the Database
    protected void bDelete_Click(object sender, EventArgs e)
    {
        SqlConnection conn = ((MP)Master).OpenDB(); // Use this if opening DB from content pages

        conn.Open();


        SqlCommand command = conn.CreateCommand();
        try
        {   //Delete a User depending on the SessionID of the contactID from the DB
            if (Session["contactId"] != null)
            {
                command.CommandText = @"Delete from contacts "
                                        + "Where ID = " + Session["contactId"] + ";";
                command.ExecuteNonQuery();

                command.CommandText = @"Delete from addresses "
                                       + "Where ID = " + Session["contactId"] + ";";
                command.ExecuteNonQuery();

                command.CommandText = @"Delete from phoneNos "
                                       + "Where ID = " + Session["contactId"] + ";";
                command.ExecuteNonQuery();

                command.CommandText = @"Delete from events "
                                      + "Where ID = " + Session["contactId"] + ";";
                command.ExecuteNonQuery();

                Session["contactId"] = null;
            }
            

        }
        catch
        {
            lDeleteMsg.Text = "**" + command.CommandText;

        }
        conn.Close();
        Session["Refresh"] = null; 
        Response.Redirect("~/NewContact.aspx");//Redirect to the New Contact Page
    }
    protected void bCancel_Click(object sender, EventArgs e)
    {
        Session["Refresh"] = "YES"; // reload last contact
        Response.Redirect("~/NewContact.aspx");
    }
}