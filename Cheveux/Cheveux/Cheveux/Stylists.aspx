<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Stylists.aspx.cs" Inherits="Cheveux.Cheveux.Stylists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Stylists - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <!-- Top Margin -->
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <div class="jumbotron bg-dark text-white">
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-10">
                        <header class="text-center">
                            <h1>Meet The Team</h1>
                        </header>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-sm-auto col-md-auto col-lg-auto">
                        <asp:PlaceHolder ID="phStylists" runat="server">
                            <asp:Table ID="tblStylists" runat="server"></asp:Table>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </div>
            
            <!--Error: If cant display the stylists-->
            <asp:PlaceHolder ID="phStylistsErr" runat="server" Visible="false">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="jumbotron">
                                <div class="row">
                                    <div class="col-md-2">
                                        <!-- Error Image-->
                                        <img src="https://cdn4.iconfinder.com/data/icons/smiley-vol-3-2/48/134-512.png" alt="Error" width="100" height="100"></img>
                                    </div>
                                    <div class="col-md-10">
                                        <!--Error details placehoders-->
                                        <asp:Label ID="errorHeader" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="errorMessage" runat="server"></asp:Label>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
