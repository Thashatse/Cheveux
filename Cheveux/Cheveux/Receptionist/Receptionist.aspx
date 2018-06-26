<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="Receptionist.aspx.cs" Inherits="Cheveux.Receptionist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!--jumbotron page heading-->
        <div class="jumbotron">
            <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="X-Large">
                <h1 id="header" runat="server">Welcome</h1>
                <h2 id="theDate" runat="server"></h2>
            </asp:Label>
            
            <!--Line Break-->
                <div class="container">
                    <br />
                </div>
        </div>
        <div id="viewAgenda">
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <h3>
                        View Stylist Agenda
                    </h3>
                </div>
            </div>
            <div id="empDropdown" class="row">
                <div class="col-xs-12 col-md-12">
                    
                    <asp:DropDownList ID="drpEmpNames" runat="server" AutoPostBack="True" >
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>  
                </div>
            </div>
        </div>
 
        <!--Line Break-->
                <div class="container">
                    <br />
                </div>
        
        <div id="Agenda">
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:table runat="server" ID="AgendaTable"></asp:table>
                </div>
            </div>
        </div>

        <!--Line Break-->
                <div class="container">
                    <br />
                </div>

    </div>
</asp:Content>
