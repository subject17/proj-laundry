﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Common.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="Laundry_Platypus.Manager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manager Desktop</title>

    <!-- <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>  
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>-->
    <link href='http://fonts.googleapis.com/css?family=Cookie' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="css/dropoffOrder.css"> 
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div class="row">
                    <div id="orderTable" class="col-md-12">
                        <table border="1" class="table-striped table-condensed">
                            <thead>
                                <tr>
                                    <th class="auto-style1">OrderID</th>
                                    <th class="auto-style1"> Customer </th>
                                    <th>Address</th>
                                    <th class="auto-style1">Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="loading">
                                    <td colspan="5">loading...</td>
                                    
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!--Script references. -->
                    <!--Reference the jQuery library. -->
                    <script src="Scripts/jquery-3.1.1.min.js"></script>
                    <!--Reference the SignalR library. -->
                    <script src="Scripts/jquery.signalR-2.2.2.js"></script>
                    <!--Reference the autogenerated SignalR hub script. -->
                    <script src="signalr/hubs"></script>
                    <!--Reference the StockTicker script. -->
                    <script src="Scripts/System.js"></script>

               
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Add Order" onClick ="Button1_Click"/>
</asp:Content>
