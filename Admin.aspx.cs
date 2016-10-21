/*
Author: Chris Manek
Date: 2/8/2016
Description: Admin.aspx.cs Creates test data and builds our tables for the database
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bCreateDB_Click(object sender, EventArgs e)
    {
        // using System.Data.SqlClient;  // Required for Sql commands
        SqlConnection conn = ((MP)Master).OpenDB(); // Use this if opening DB from content pages

        conn.Open();

        //Drop Table 
        SqlCommand command = conn.CreateCommand();
        command.CommandText = "DROP TABLE contacts";
        try
        {
            command.ExecuteNonQuery();
        }
        catch { }; // If table missing do not generate an error move on and create it below

        command.CommandText = "DROP TABLE events";
        try
        {
            command.ExecuteNonQuery();
        }
        catch { }; // If table missing do not generate an error move on and create it below
        command.CommandText = "DROP TABLE phoneNos";
        try
        {
            command.ExecuteNonQuery();
        }
        catch { }; // If table missing do not generate an error move on and create it below
        command.CommandText = "DROP TABLE Addresses";
        try
        {
            command.ExecuteNonQuery();
        }
        catch { }; // If table missing do not generate an error move on and create it below

        command.CommandText = "DROP TABLE UserInfo";
        try
        {
            command.ExecuteNonQuery();
        }
        catch { }; // If table missing do not generate an error move on and create it below



        command.CommandText = @"CREATE TABLE contacts 
                               (ID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
                                lName char(50) NULL,
                                fName char(50) Null,
                                DOB date NULL,
                                relationship char(20) Null
                                    ) ;"; // Create contact table with one field, more to come
        command.ExecuteNonQuery();

        command.CommandText = @"CREATE TABLE events 
                               (eventID int IDENTITY(1,1) NOT NULL,
                                ID int not NULL,
                                subject char(25) NULL,
                                description char(250) NULL,
                                location char(50) Null,
                                eventDate date NULL,
                                startTime time NULL,
                                stopTime time NULL,
                                timeSpent float NULL, 
                                PRIMARY KEY (id,eventId )                              
                                    ) ;"; // Create contact table with one field, more to come
        command.ExecuteNonQuery();
       
        command.CommandText =
       @"CREATE TABLE phoneNos
                    ( ID int not null,
                    type char(25) not NULL,
                    phoneNo char(25) NULL ,
                    PRIMARY KEY (ID,type));"; //Create phoneNos Table
        command.ExecuteNonQuery();

        command.CommandText =
              @"CREATE TABLE UserInfo
                    ( userNm char(50) not null Primary key,
                    Pw char(50) not NULL,
                    IPlow char(50)  NULL,
                    IPhigh char(50) NULL,
                    IPlast char(50) NULL ,
                    status char(1)  NULL,
                    IPcnt	int     null);"; //Create UserInfo Table

        command.ExecuteNonQuery();


        command.CommandText =
          @"CREATE TABLE addresses
                   ( ID int not null,
                    type char(25) not NULL,
                    MailStop char(50) NULL,
                    streetAddress char(50) NULL,
                    City char(50) NULL,
                    St char(2) NULL,
                    zip char(10) NULL,	         PRIMARY KEY (ID,type) );"; 
        command.ExecuteNonQuery();

        // Create backup copy of the current database

        var dbName = conn.Database;

        command.CommandText = "backup database "+
                dbName+" to disk = 'h:\\MyStuff\\db.bak'";
        try
        {
            command.ExecuteNonQuery();
        }
        catch {


                ((MP)Master).MsgLog("Admin","DB backup Failed");
                ((MP)Master).MsgLog("Admin", command.CommandText);
           
            }; // If table missing do not generate an error move on and create it below

        conn.Close();
    }


    protected void bCreateTestData_Click(object sender, EventArgs e)
    {

        //Test Data 2D Array
        string[][] students = new string[][] {
                       new string[]{"Chris", "Manek"},
                       new string[]{"Lance", "Martinez"},
                       new string[]{"Brandi", "Scro"}, 
                       new string[]{"Paige", "Ford"}, 
                       new string[]{"Mason", "Hernandez"}, 
                       new string[]{"Jason", "Kinslow"}, 
                       new string[]{"Jordan","Lawes"}, 
                       new string[]{"Logan", "Moore"}, 
                       new string[]{"Moises","Rodriquez"}, 
                       new string[]{"Toan",   "Trieu"}
                                             };
        //Generate Random Number
        Random rnd = new Random();
        string[] type = new string[] { "Friend", "Classmate", "Coworker", "Family", "Complicated" }; //Relationship String Array

        //Call open DB connection from Master Page
        SqlConnection conn = ((MP)Master).OpenDB();
        conn.Open();
        SqlCommand command = conn.CreateCommand();

        //Use for loops to grab data from array in a random number and then create that data
        string dt;
        for (int ndx1 = 0; ndx1 < students.GetLength(0); ndx1++)
        {
            for (int ndx2 = 0; ndx2 < students.GetLength(0); ndx2++)
            {


                dt = rnd.Next(1, 12).ToString("D2") + "-" +
                           rnd.Next(1, 27).ToString("D2") + "-" +
                           rnd.Next(1915, 2015).ToString();

                command.CommandText = @"Insert into contacts 
                                (lname,fname,DOB,relationShip )
                            values ( '" + students[ndx1][1] + "','" + students[ndx2][0] + "','" + dt + "','" + type[rnd.Next(0, 4)] + "');";

                if (command.ExecuteNonQuery() != 1)
                {
                    ((MP)Master).MsgLog("Admin-Create Test Data", command.CommandText);
                }

            }
        }

        Response.Write("Test data loaded");
    }
}