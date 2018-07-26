<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="UpdateEmployee.aspx.cs" Inherits="Cheveux.Manager.UpdateEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Update Employee - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server">
    <div>
        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />
        <div class="container">
            <div class="row">
                <div class="col-md-2 col-sm-1"></div>
                <div class="col-md-12 col-sm-10">
                    <div class="jumbotron bg-dark text-white">
                        <br />
                        <div class="row">
                            <div class="col-12">
                                <header>
                                    <h1 id="updateEmpHeading">
                                        Update Employee Details
                                    </h1>
                                </header>
                                <br />
                            </div>
                        </div>
                            <div class="row">
                                <div class="col-5">
                                    <asp:table id="tblUserImage" runat="server"></asp:table>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div id="empDetails" class="container">
            <div class="row">
                <div class="col-md-12 form-group">
                    <asp:label id="lblAddLine1" runat="server" text="Address Line 1"></asp:label>&nbsp; &nbsp; &nbsp; 
                    <asp:textbox id="txtAddLine1" runat="server" placeholder="Address Line 1"></asp:textbox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 form-group">
                    <asp:label id="lblAddLine2" runat="server" text="Address Line 2"></asp:label>&nbsp; &nbsp; &nbsp; 
                    <asp:textbox id="txtAddLine2" runat="server" placeholder="Address Line 2"></asp:textbox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 form-group">
                    <asp:label id="lblType" runat="server" text="Employee Type"></asp:label>&nbsp; &nbsp; &nbsp; &nbsp; 
                    <asp:radiobuttonlist id="rdoType" runat="server" CellSpacing="5" RepeatDirection="Horizontal" RepeatLayout="flow">
                        <asp:listitem id="TypeOption1" runat="server" Text="Receptionist" value="R"/>
                        <asp:listitem id="TypeOption2" runat="server" Text="Stylist" value="S"/>
                    </asp:radiobuttonlist>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:button id="btnBack" class="btn btn-primary" runat="server" text="Back" OnClick="btnBack_Click" />&nbsp; &nbsp; &nbsp; 
                    <asp:button id="btnUpdate" class="btn btn-primary" runat="server" text="Update" OnClick="btnUpdate_Click" />
                </div>
            </div>

        </div>
            <br />
            <br />
        </div>
</form>
</asp:Content>
