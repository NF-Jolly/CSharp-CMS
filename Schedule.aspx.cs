/*
Author: Chris Manek
Date: 2/8/2016
Description: Schedule.aspx.cs Lets the User Schedule Events depending on their schedule
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)//Code Runs on Page Load
    {
        string tmp;

        tmp = tbStart.Text;

        if (tbStart.Text == "")
        {
            tbStart.Text = DateTime.Now.ToString("yyyy-MM-dd");//Set Date Format
            tbEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");//Set Date Format
        }
         

        SqlConnection conn = ((MP)Master).OpenDB(); // Use this if opening DB from content pages

        string sql;
        string startDt, endDt;



       startDt = tbStart.Text;
       endDt = tbEnd.Text;
        
        //Change the Alias of the Table Columns to be displayed
        sql = @"Select eventId,ID,CONVERT(char(10), eventDate, 1) as Date,
              CONVERT(varchar, startTime, 100) AS Start,
              CONVERT(varchar, stopTime, 100) AS Stop,
                Subject,Location from events
                where eventDate >= '" + startDt + " 00:01'" +
                " and eventDate <= '" + endDt + " 23:59 '" +
                " order by eventDate , startTime;";

        try
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        catch { }

       //GridView1.Columns[0].Visible = false;
       //GridView1.Columns[1].Visible = false;
       //GridView1.Columns[2].DataFormatString = "{0:dd.MM.yyyy}";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;

      
        Session["EventId"]= row.Cells[1].Text;
        Session["contactId"] = row.Cells[2].Text;
        Response.Redirect("~/Events.aspx");

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        string tmp;

        // Required for all types
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;

        if (e.Row.RowType == DataControlRowType.Header)
        {
         
            e.Row.Cells[3].Text = "Date";

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {




            SqlConnection conn = ((MP)Master).OpenDB();//Open DB from Master Page
            conn.Open();
            SqlCommand command = conn.CreateCommand();

            command.CommandText = @"Select fName,lName, description "
                                    +" from events inner join contacts on events.ID = contacts.Id "
                                    + " where eventId = " + e.Row.Cells[1].Text;
                

            try
            {
                SqlDataReader dr = command.ExecuteReader();

               
                while (dr.Read())
                {

                    e.Row.ToolTip = dr["lName"].ToString().Trim() + ", " + dr["fName"] + "\n" + dr["description"]; 
                    //On hover it will show a tooltip to show the desciption of the event scheduled
                    

                }

               
            }
            catch { }

     

            
           
        }
    }
    
}