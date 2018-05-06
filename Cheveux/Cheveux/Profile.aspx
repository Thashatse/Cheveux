<%@ Page Title="" Language="C#" MasterPageFile="~/Cheveux.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Cheveux.Profile2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Tabs-->
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <!--Tabs-->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active"><a href="#Details" role="tab" data-toggle="tab">Details</a></li>
                        <li><a href="#Upcoming Service(s)" role="tab" data-toggle="tab">Upcoming Service(s)</a></li>
                        <li><a href="#Past Service(s)" role="tab" data-toggle="tab">Past Service(s)</a></li>
                    </ul>

                    <!--Tabs Content-->
                    <div class="tab-content">
                        <div class="active tab-pane" id="Details">
                            <h2>Deatils</h2>
                            <!--Content-->
                        </div>

                        <div class="tab-pane" id="Upcoming Service(s)">
                            <h2>Upcoming Service(s)</h2>
                            <!--Content-->
                        </div>

                        <div class="tab-pane" id="Past Service(s)">
                            <h2>Past Service(s)</h2>
                            <!--Content-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
