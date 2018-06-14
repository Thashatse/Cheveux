<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cheveux.Default2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    Cheveux
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
        body{
            margin-top:150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
            <!--jumbotron page heading-->
            <div class="jumbotron">
                <!--Jambotron Content Here-->
                <!--Welcome back-->
                <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>

                <!--Line Break-->
                <div class="container">
                    <br />
                    <br />
                </div>
            </div>

            <!--Service selector-->
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <a name="Service"></a>
                    <h2>Service </h2>

                </div>
                <!--Service 1-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Service Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Service Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Choose Hairstyle </div>
                </div>

                <!--Service 2-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Service Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Service Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Choose Hairstyle </div>
                </div>

                <!--Service 3-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Service Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Service Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Choose Hairstyle </div>
                </div>

                <!--Line Break-->
                <div class="container">
                    <br />
                    <br />
                </div>

            </div>

            <!--Product selector-->
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <a name="Products"></a>
                    <h2>Product </h2>

                </div>
                <!--Product 1-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Product Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Product Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Reserve Product </div>
                </div>

                <!--Product 1-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Product Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Product Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Reserve Product </div>
                </div>

                <!--Product 1-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Product Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Product Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Reserve Product </div>
                </div>

            </div>

            <!--Line Break-->
            <div class="container">
                <br />
                <br />
            </div>

            <!--Hairstylist selector-->
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <a name="Hairstylist"></a>
                    <h2>Hairstylist </h2>
                </div>

                <!--Hairstylist 3-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Hairstylist Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Hairstylist Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Make a Booking </div>
                </div>

                <!--Hairstylist 3-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Hairstylist Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Hairstylist Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Make a Booking </div>
                </div>

                <!--Hairstylist 3-->
                <div class="col-xs-6 col-md-4">
                    <h3>
                        <!--Hairstylist Name Here-->
                        Placeholder</h3>
                    <div class="row">
                        <!-- Hairstylist Details Here-->
                    </div>
                    <div class="btn btn-outline-dark">Make a Booking </div>
                </div>
            </div>

            <!--Line Break-->
            <div class="container">
                <br />
                <br />
            </div>

            <!--Contact Us-->
            <div class="row">
                <div class="col-xs-12 col-md-12">

                    <!--jumbotron Contact Us-->
                    <div class="jumbotron">
                        <a name="ContactUs"></a>
                        <h2>Contact Us</h2>
                        <!--Jambotron (COntact US) Content Here-->
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
