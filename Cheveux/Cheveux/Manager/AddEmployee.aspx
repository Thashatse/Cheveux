<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="Cheveux.Manager.AddEmpolyee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Add Employee - Cheveux
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
            <form runat="server">
                <!-- Jumbo Tron -->
                <div class="jumbotron bg-dark text-white">
                    <br />
                    <div id="search">
                        <div class="row">
                            <div class="col-12">
                                <header>
                                    <h1>Add Employee</h1>
                                </header>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-5">
                                <!--- Search for user --->
                                <asp:TextBox class="form-control input-sm" ID="txtSearch" runat="server" Placeholder="Search for user"></asp:TextBox>
                            </div>
                            <div class="col-2">
                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="Find User" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <!--- List of Users --->
                <div class="row">
                    <div class="col-md-12">
                        <asp:PlaceHolder ID="phUsers" runat="server" Visible="true">
                            <asp:Table ID="tblUsers" runat="server"></asp:Table>
                            <asp:Label ID="lblValText" runat="server" Text="Label" Visible="false"></asp:Label>
                        </asp:PlaceHolder>
                    </div>
                </div>

                <!--- List of Users found from search --->
                <div class="row">
                    <div class="col-md-12">
                        <h3 id="resultsHeading" class="text-center" runat="server" visible="false">...
                        </h3>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:PlaceHolder ID="phSearchedUsers" runat="server" Visible="false">
                            <asp:Table ID="tblSearchedUsers" runat="server"></asp:Table>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </form>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
