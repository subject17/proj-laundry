﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Laundry_Platypus;
using MySql.Data.MySqlClient;

namespace Laundry_Platypus
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /**
        * This function is to check the username and password whether correct and redirect to different pages
        * according to different role
        * */
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || passwd.Text == "")
            {

                Response.Write("<script lanuage=javascript>alert('Please input username and password');window.location.href='login.aspx'</script>");
            }
            int role_id = UserManage.Login(username.Text, passwd.Text);

            if (role_id == 0)
            {
                Response.Write("<script lanuage=javascript>alert('username or password wrong');window.location.href='login.aspx'</script>");
            }
            else
            {
                // SqlDataReader datard = Datacon.getRow("SELECT * FROM tb_User WHERE user_id='" + TextBox1.Text + "';");
                MySqlDataReader datard = UserManage.GetUser(username.Text);
                datard.Read();
                Session["user_name"] = datard["user_name"].ToString();
                Session["user_id"] = datard["user_id"].ToString(); ;
                Session["role_id"] = role_id;
                //Response.Cookies["user_id"].Value = userid.Text;
                //Response.Cookies["passwd"].Value = passwd.Text;
                System.Web.HttpCookie newcookie = new HttpCookie("user");
                newcookie.Values["user_id"] = datard["user_id"].ToString(); ;
                newcookie.Values["passwd"] = passwd.Text;
                newcookie.Expires = DateTime.Now.AddDays(1);
                Response.AppendCookie(newcookie);
                //manager
                if (role_id == 1)
                {
                    //Response.Write(Session["user_id"]);
                    Response.Redirect("Manager.aspx");
                }
                //packer page
                if (role_id == 2)
                {
                    //Response.Write(Session["user_id"]);
                    Response.Redirect("PackerOverview.aspx");
                }
                //driver page
                if (role_id == 3)
                {
                    //Response.Write(Session["user_id"]);
                    Response.Redirect("DriverOverview.aspx");
                }
                //customer page
                if (role_id == 4)
                {
                    //Response.Write(Session["user_id"]);
                    Response.Redirect("admin.aspx");
                }
                else
                {
                    //Response.Write(Session["user_id"]);
                    Response.Redirect("index.aspx");
                }
            }
        }
    }
}