<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cheveux.Default2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    Cheveux
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!--page header-->
        <a name="Booking"></a>
        <header class="masthead text-center text-white d-flex">
            <div class="container my-auto">
                <div class="row">
                    <div class="col-lg-10 mx-auto">
                        <h1 class="text-uppercase">
                            <strong>Cheveux</strong>
                        </h1>
                        <hr >
                    </div>
                    <div class="col-lg-8 mx-auto">
                        <!--Welcome back-->
                        <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                        <!--Line Break -->
                        <p class="text-faded mb-5">Smart people choose Cheveux!</p>
                        <a class="btn btn-primary btn-xl js-scroll-trigger" href="MakeABooking.aspx">Make a booking</a>
                    </div>
                </div>
            </div>
        </header>

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
                    <asp:Table ID="tblContactUs" runat="server"></asp:Table>
                    <!-- Line Break -->
                    <br />
                    <br />
                    <!-- Map -->
                    <iframe
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3307.568475806453!2d25.667619415705655!3d-34.00361618061904!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1e6532ce6cb4c0f1%3A0x9d4d0eeed57a0841!2sUniversity+Way%2C+Port+Elizabeth!5e0!3m2!1sen!2sza!4v1531901636092"
                        width="1050" height="525" frameborder="0" style="border: 0" allowfullscreen></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
