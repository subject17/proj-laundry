﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Data;

namespace Laundry_Playtus
{
    //This is ticker
    public class System_L
    {
        private readonly static Lazy<System_L> _instance = new Lazy<System_L>(() => new System_L(GlobalHost.ConnectionManager.GetHubContext<ClientHub>().Clients));
        //private Orderlist orderlist;
        private readonly ConcurrentDictionary<string, Order> _orders = new ConcurrentDictionary<string, Order>();
        private readonly ConcurrentDictionary<string, Person> _users = new ConcurrentDictionary<string, Person>();
        //public System() : this(Orderlist.Instance) { }
        private readonly object _updateOrderLock = new object();
        private volatile bool _updatingOrder = false;

        public static System_L Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }
        private System_L(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            // Remainder of constructor ...
            //Initial orderlist
            //Order order_test = new Order("test_ID","test_state");
            // Order order_test = new Order("test_ID", "test_state");
            //_orders.TryAdd("test", order_test);
            //orderstate>0 means available, 0 means deactive -1 means done
            DataSet dataset = Datacon.getDataset("SELECT * FROM tb_Order WHERE order_state>0 ", "Order");
            foreach (DataRow pRow in dataset.Tables["Order"].Rows)
            {
                Order order_test = new Order(pRow["order_id"].ToString(), pRow["order_state"].ToString());
                _orders.TryAdd(pRow["order_id"].ToString(), order_test);
            }
            //System.Console.WriteLine("success");
            dataset = Datacon.getDataset("SELECT * FROM tb_User ", "User");
            foreach (DataRow pRow in dataset.Tables["User"].Rows)
            {
                Person person_t = new Person(pRow["user_id"].ToString(), pRow["user_name"].ToString(), pRow["user_contact"].ToString(), pRow["user_active"].ToString(), pRow["user_selfie"].ToString(), pRow["password"].ToString(), pRow["role_id"].ToString());
                _users.TryAdd(pRow["user_id"].ToString(), person_t);
            }
            //System.Console.WriteLine("success");
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _orders.Values;
        }
        public Person GetUser(string userid, string passwd)
        {
            if (_users.ContainsKey(userid))
            {
                if (_users[userid].Passwd == passwd)
                {
                    return _users[userid];
                }
            }
            return null;
        }
        private void UpdateOrder(object state)
        {
            lock (_updateOrderLock)
            {
                if (!_updatingOrder)
                {
                    _updatingOrder = true;

                    foreach (var order_t in _orders.Values)
                    {

                    }

                    _updatingOrder = false;
                }
            }
        }
        public IEnumerable<Order> GetAllOrders1(Person person)
        {
            if (person.Roleid == "3")
            {
                //This is driver
                ConcurrentDictionary<string, Order> orders_t = new ConcurrentDictionary<string, Order>();
                IEnumerable<Order> orderlist = _orders.Values;
                foreach (Order order_t in orderlist)
                {
                    if (order_t.HandlerID == person.ID)
                    {
                        orders_t.TryAdd(order_t.ID, order_t);
                    }
                }
                return orders_t.Values;
            }
            if (person.Roleid == "2")
            {
                //This is packer
                ConcurrentDictionary<string, Order> orders_t = new ConcurrentDictionary<string, Order>();
                IEnumerable<Order> orderlist = _orders.Values;
                foreach (Order order_t in orderlist)
                {
                    if (order_t.HandlerID == person.ID)
                    {
                        orders_t.TryAdd(order_t.ID, order_t);
                    }
                }
                return orders_t.Values;
            }
            if (person.Roleid == "1")
            {
                //This is officeworker
            }
            if (person.Roleid == "4")
            {
                //This is client
                return _orders.Values;
            }
            return null;
        }
    }
}