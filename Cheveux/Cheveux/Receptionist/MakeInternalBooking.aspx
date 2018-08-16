<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="MakeInternalBooking.aspx.cs" Inherits="Cheveux.MakeInternalBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Make A Booking - Cheveux
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-md-1 col-sm-1"></div>
        <div class="col-md-10 col-sm-10">
            <form runat="server">
                <div class="jumbotron  bg-dark text-white">
                    <h1>Make A Booking</h1>
                    <br />
                    <asp:Label runat="server" ID="BookingSummary"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12 col-lg-5">
                        <div runat="server" id="divSelectStyle">
                            <!--Style-->
                            <div class="container">
                                <!--Line Break-->
                                <br />
                                <h3 style="text-align: left; float: left;">Select Style: </h3>
                                <p style="text-align: right; float: right;">
                                    <!-- Search -->
                                    <asp:TextBox ID="txtStyleSearch" runat="server" AutoPostBack="true" placeholder="search"
                                        OnDataBinding="txtStyleSearch_DataBinding" OnTextChanged="txtStyleSearch_DataBinding" CssClass="form-control"></asp:TextBox>
                                </p>
                                <!--Line Break-->
                                <br />
                                <asp:ListBox runat="server" ID="lBServices" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="300"></asp:ListBox> 
                                <!--Line Break-->
                                <br />
                                <asp:Button ID="btnSelectCustomer" runat="server" Text="Select Customer" CssClass="btn btn-primary" OnClick="btnSelectCustomer_Click" />
                            </div>
                        </div>
                        <div runat="server" id="divSelectUser" visible="false">
                            <!--User-->
                            <div class="container">
                                <!--Line Break-->
                                <br />
                                <h3 style="text-align: left; float: left;">Select Customer: </h3>
                                <p style="text-align: right; float: right;">
                                    <!-- Search -->
                                    <asp:TextBox ID="txtCustomer" runat="server" AutoPostBack="true" placeholder="search"
                                        OnDataBinding="txtCustomer_DataBinding" OnTextChanged="txtCustomer_DataBinding" CssClass="form-control"></asp:TextBox>
                                </p>
                                <!--Line Break-->
                                <br />
                                <asp:ListBox runat="server" ID="lbCustomers" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="300"></asp:ListBox>
                                <!--Line Break-->
                                <br />
                                <asp:Button ID="btnComfirmation" runat="server" Text="Summary" CssClass="btn btn-primary" OnClick="btnComfirmation_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 col-lg-2 text-center">
                        <!-- Line Break -->
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnAddServiceToBooking" runat="server" Text="Add Service" CssClass="btn btn-Secondary" OnClick="btnAddServiceToBooking_Click" />

                    </div>
                    <div class="col-md-12 col-lg-5">
                        <!--Line Break-->
                        <br />
                        <!-- Summary -->
                        <!--Lable-->
                        <h3>Summary: </h3>
                        <!--Line Break-->
                        <br />
                        <!-- Summary -->
                        <asp:Label ID="lSummary" runat="server" Font-Size="Large" Font-Bold="true"></asp:Label>
                        <!--Line Break-->
                        <br />
                        <!-- When -->
                        <asp:Label ID="lDateandTime" runat="server" ></asp:Label>
                        <!--Line Break-->
                        <br />
                        <!-- Services -->
                        <asp:ListBox runat="server" ID="lBservicestoBook" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="100"></asp:ListBox>
                        <!--Line Break-->
                        <br />
                        <asp:Label ID="BackButton" runat="server"></asp:Label>
                        <asp:Button ID="btnMakeBooking" runat="server" Text="Make Booking" CssClass="btn btn-primary" OnClick="btnMakeBooking_Click" Visible="false" />
                    </div>
                </div>
            </form>
        </div>
        <div class="col-md-1 col-sm-1"></div>
    </div>
</asp:Content>
