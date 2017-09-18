﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laundry_Platypus
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("garment_id", Type.GetType("System.String"));
            dt.Columns.Add("type_name", Type.GetType("System.String"));
            dt.Columns.Add("abbreviation", Type.GetType("System.String"));
            dt.Columns.Add("activate", Type.GetType("System.String"));
            dt.Columns.Add("amount", Type.GetType("System.String"));
            Order order=null;
            string garment;
            string orderid = Request["orderid"].ToString();
            if (orderid != null) { order=System_L.Instance.GetOrder(orderid); }
            if (order != null)
            {
                garment = order.Garment;
                string[] garment_detail=garment.Split(';');
                foreach (string t in garment_detail)
                {
                    string[] t2 = t.Split(',');
                    MySqlDataReader reader = Datacon.getRow("SELECT * FROM tb_Garment_type WHERE garment_id = '"+t2[0]+"';");
                    if (reader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["garment_id"] = reader["garment_id"];
                        dr["type_name"] = reader["type_name"]; 
                        dr["abbreviation"] = reader["abbreviation"];
                        dr["activate"] = reader["activate"];
                        dr["amount"] = t2[1];
                       dt.Rows.Add(dr);
                    }
                }
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
        }
    }
}