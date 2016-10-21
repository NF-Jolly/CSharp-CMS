/*
Author: Chris Manek
Date: 2/8/2016
Description: MP.master.cs Master page for project has cleaning functions, and page rendering fixes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  // Required for Sql commands
using System.IO; // Support file creating and access
using System.Configuration;


public partial class MP : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)//Code when page loads
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if ( !IsPostBack ){
            string contentPg = this.Page.GetType().Name.ToString().ToLower();

            if (Session["UserNm"] == null //If username is null and content page isnt the login redirect to login page
            && contentPg != "login_aspx")
            {

                Response.Redirect("~/Login.aspx");

            }

        }


    }

    public void MsgLog(string src, string msg)
    {/*
        using (StreamWriter w = File.AppendText("h:\\MyStuff\\log.txt"))
        {
            w.WriteLine("\n\r{0} {1}", DateTime.Now.ToLongTimeString(),
                   DateTime.Now.ToLongDateString());
            w.WriteLine("\n\r {0} {1}", src, msg);

        }
        return;
      */
    }

    public SqlConnection OpenDB() // Public access allows access from content pages
    {

        //string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\MyStuff\Project\App_Data\Database.mdf;Integrated Security=True";
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);
        return (conn);
    }

    //Clean any string that is inputted so I cannot be hacked or compromised
    public string clean(string iStr)
    {
        string oStr;
        oStr = iStr.Trim();
        oStr = oStr.Replace("'", "`");
        oStr = oStr.Replace(";", "");
        oStr = oStr.Replace("\"", "``");
        oStr = oStr.Replace("<", "");
        oStr = oStr.Replace(">", "");
        


        return (oStr);
    }
    protected override void Render(HtmlTextWriter writer)//Fix for page rendering eliminates double clicking a button
    {
        if (IsPostBack)
        {
            base.Render(writer);
        }
        else
        {
            using (var output = new StringWriter())
            {
                base.Render(new HtmlTextWriter(output));

                var outputAsString = output.ToString();
                string trg;
                string tmp;
                tmp = outputAsString;
                if (Session["SaveMode"] != null)
                {
                    if (Session["SaveMode"] == "Update")
                    // If page is in update mode, disable post back on text change in last name field
                    {
                        trg = "onchange=\"javascript:setTimeout(&#39;__doPostBack(\\&#39;ctl00$ContentPlaceHolder1$tbLname\\&#39;,\\&#39;\\&#39;)&#39;, 0)\"";
                        tmp = outputAsString.Replace(trg, "");
                    }
                }
                writer.Write(tmp);

            }
        }
    }

}

    