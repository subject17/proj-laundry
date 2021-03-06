﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laundry_Playtus
{
    public class Order
    {
        public string ID { get; set; }
        public DateTime Time { get; set; }
        public string ClientID { get; set; }
        public string State { get; set; }
        public string Comment { get; set; }
        public string Product { get; set; }
        public string HandlerID { get; set; }
        public Order(string id, string state)
        {
            ID = id;
            State = state;
        }
    }
}