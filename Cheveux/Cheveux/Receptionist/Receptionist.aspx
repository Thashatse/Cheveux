<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxReceptionist.Master" AutoEventWireup="true" CodeBehind="Receptionist.aspx.cs" Inherits="Cheveux.Receptionist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Receptionit Dashboard - Cheveux
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
                <!--jumbotron page heading-->
                <div class="jumbotron bg-dark text-white">
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
                            <h3>View Stylist Agenda
                            </h3>
                        </div>
                    </div>
                    <div id="empDropdown" class="row">
                        <div class="col-xs-12 col-md-12">

                            <asp:DropDownList ID="drpEmpNames" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
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
                            <asp:Table runat="server" ID="AgendaTable"></asp:Table>
                        </div>
                    </div>
                </div>

                <!--Line Break-->
                <div class="container">
                    <br />
                </div>
            </form>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
