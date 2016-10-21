/*
Author: Chris Manek
Date: 2/8/2016
Description: Login.aspx.cs Includes functions related to logging in and redirecting to various pages.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bLogin_Click(object sender, EventArgs e)
    {
        //Backdoor
        if (tbUser.Text == "123"
        && tbPw1.Text == "123")
        {
            Session["UserNm"] = "Admin";
            Response.Redirect("~/Admin.aspx");
        }

        string IP;

        IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        // Handle proxy server
        if (IP == null)
        {
            IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            Response.End(); // Do not allow connection via Proxy server

        }



        if (tbUser.Text == ""
        ||  tbPw1.Text == ""
        ||  tbUser.Text == null
        ||  tbPw1.Text == null)
        {
            Response.End();
        }

        SqlConnection conn = ((MP)Master).OpenDB(); //Open DB from Master Page
        conn.Open();
        SqlCommand command = conn.CreateCommand();

        command.CommandText = @"select status,IPcnt,IPlast from  UserInfo where " +     //Clean the values being inputted
                                "userNm = '" + ((MP)Master).clean(tbUser.Text) + "' and "+
                                    " Pw = '"+ ((MP)Master).clean(tbPw1.Text)+"';";

        using (SqlDataReader dr = command.ExecuteReader())
        {

            while (dr.Read())
            {
                if (tbPw2.Text != null)
                {
                    // Add code to change pw
                }
                Session["UserNm"] = tbUser.Text;
                Response.Redirect("~/Schedule.aspx");

            }
        }

        //If password 1 & 2 are equal add password to database (Confirm Pass)
        if (tbPw2.Text == tbPw1.Text)
        {

            command.CommandText = @"Insert into userInfo ( userNm , Pw  )" +
                              "values ( '" + ((MP)Master).clean(tbUser.Text) + "','" +
                                        ((MP)Master).clean(tbPw1.Text) + "');";



            int rowCnt = 0;
            try
            {
                rowCnt = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                ((MP)Master).MsgLog("Login", command.CommandText); //Log errors to a log file for debugging

            }

        }
        else
        {
            command.CommandText = @"Insert into userInfo ( userNm , IPlast  )" +
                                        "values ( '" + ((MP)Master).clean(tbUser.Text) + "','" +
                                                   IP+ "');";



            int rowCnt = 0;
            try
            {
                rowCnt = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                ((MP)Master).MsgLog("Login", command.CommandText);
                Response.End();

            }

        }


    }
 }