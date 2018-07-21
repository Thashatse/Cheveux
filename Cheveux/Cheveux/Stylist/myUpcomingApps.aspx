<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="myUpcomingApps.aspx.cs" Inherits="Cheveux.todaysSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    My Upcoming Appointments - Cheveux
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
            <!--jumbotron page heading-->
            <div class="jumbotron  bg-dark text-white">
                <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="X-Large">
                    <h1>All your upcoming bookings for today</h1>
                    <h2 id="theDate" runat="server"></h2>
                </asp:Label>

                <!--Line Break-->
                <div class="container">
                    <br />
                </div>
            </div>
            <div id="Agenda">
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <asp:Table runat="server" ID="myScheduleToday"></asp:Table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
