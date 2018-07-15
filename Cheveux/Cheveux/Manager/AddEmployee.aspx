﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="Cheveux.Manager.AddEmpolyee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Add Employee - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div class="container-fluid">
        <div class="block">
            <div class="row">
                <div class="col-md-12">
                    <header>
                        <h3>Add Employee</h3>
                    </header>
                </div>
            </div>

            <!--- Search Users name --->   
            <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search for user"></asp:TextBox>
            <asp:Button ID="btnSearch" class="btn btn-default" runat="server" Text="Find User" OnClick="btnSearch_Click" />


            <!--- List of Users --->
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phUsers" runat="server" Visible="true">
                        <asp:Table ID="tblUsers" runat="server"></asp:Table>
                    </asp:PlaceHolder>
                </div>
            </div>
        
            <!--- List of Users found from search --->
            <div class="row">
                <div class="col-md-12">
                    <asp:PlaceHolder ID="phSearchedUsers" runat="server" Visible="false">
                        <asp:Table ID="tblSearchedUsers" runat="server"></asp:Table>
                    </asp:PlaceHolder>
                </div>
            </div>
        

        </div>
    </div>
    </form>
</asp:Content>
