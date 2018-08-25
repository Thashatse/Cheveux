<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Cheveux.Manager.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Employees - Cheveux
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
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <!-- if the user is loged In -->
            <div runat="server" id="LogedIn" visible="false">
                <div class="row">
                    <div class="col-md-12">

                        <!-- Jumbo Tron -->
                        <div class="jumbotron bg-dark text-white">
                            <h1>Manage Employees</h1>
                            <!-- line Break -->
                            <br />
                            <form runat="server">
                                <!-- View By Selector -->
                                <p>View Employess By: </p>
                                <asp:DropDownList ID="drpEmpTyp" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                                <!-- line Break -->
                                <br />
                                <br />

                                <div class="row">
                                    <div class="col-12">
                                        <!-- View By search tearm -->
                                        <p>View Employess By Search Term: </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-5">
                                        <asp:TextBox class="form-control" AutoPostBack="true" ID="txtProductSearchTerm" 
                                            runat="server" OnTextChanged="Page_Load"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button CssClass="btn btn-primary" ID="btnProductSearch" runat="server" 
                                            Text="Search" />
                                    </div>
                                </div>
                            </form>
                            <!-- line Break -->
                            <br />
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#ManageEmployee" target="_blank" title="How To Manage Employees">
                                <span class="glyphicon">&#63; Help</span></a>
                            <br />
                            <!--View Schedules-->
                            <a href="../Receptionist/Appointments.aspx?Action=ViewAllSchedules">View Schedules</a>
                        </div>
                    </div>
                </div>

                <!--User Friendly Notification-->
                <div class="container row" runat="server">
                    <asp:PlaceHolder ID="phNotif" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-success alert-dismissible">
                            <asp:Label ID="lblNotif" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                    </asp:PlaceHolder>
                </div>

                <div class="row">
                    <div class="col-md-10">
                        <asp:Label ID="employeeJumbotronLable" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <!--add employee btn -->
                        <button type='button' class='btn btn-primary'><a class='btn btn-primary' href='../Authentication/NewAccount.aspx?Type=NewEmp'>New Employee </a></button>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <!-- List Employee Table -->
                        <asp:Table ID="tblEmployeeTable" runat="server">
                        </asp:Table>
                    </div>
                </div>
            </div>

            <!-- if the user is loged Out -->
            <div runat="server" id="LogedOut" jumbotron bg-dark text-white>
                <div class="row">
                    <div class="col-md-12">
                        <div class="jumbotron">
                            <h1>Manage Employees</h1>
                            <p>Please log-in to Manage Employees</p>
                            <button type="button" class="btn btn-primary">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Employee.aspx" id="LogedOutButton">Login</a>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
