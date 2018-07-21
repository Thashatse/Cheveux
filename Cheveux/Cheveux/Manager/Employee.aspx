<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Cheveux.Manager.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Employees - Cheveux
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" runat="server" id="ReportsPage">
        <!-- if the user is loged In -->
        <div class="container" runat="server" id="LogedIn" visible="false">
            <div class="row">
                <div class="col-md-12">

                    <!-- Jumbo Tron -->
                    <div class="jumbotron">
                        <h1>Manage Employees</h1>
                        <!-- line Break -->
                        <br />
                        <form runat="server">
                            <!-- View By Selector -->
                            <p>View Employess By: </p>
                            <asp:DropDownList ID="drpEmpTyp" runat="server" AutoPostBack="True">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            </asp:DropDownList>

                            <!-- line Break -->
                            <br /><br />

                            <!-- View By search tearm -->
                            <p>View Employess By Search Term: </p>
                            <asp:TextBox ID="txtProductSearchTerm" runat="server"></asp:TextBox>
                            <asp:Button ID="btnProductSearch" runat="server" Text="Search" />
                        </form>
                        <!-- line Break -->
                        <br />
                        <!--Help-->
                        <a href="../Help/CheveuxHelpCenter.aspx#ManageEmployee" target="_blank" title="How To Manage Employees">
                            <span class="glyphicon">&#63; Help</span></a>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-10">
                    <asp:Label ID="employeeJumbotronLable" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <!--add employee btn -->
                    <button type='button' class='btn btn-default'> <a href='/Manager/AddEmployee.aspx' target="_blank">New Employee </a></button>
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
        <div class="container" runat="server" id="LogedOut">
            <div class="row">
                <div class="col-md-12">
                    <div class="jumbotron">
                        <h1>Manage Employees</h1>
                        <p>Please log-in to Manage Employees</p>
                        <button type="button" class="btn btn-default">
                            <a href="../Authentication/Accounts.aspx?PreviousPage=Employee.aspx" id="LogedOutButton">Login</a>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>