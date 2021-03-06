﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cheveux.Default2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    Cheveux
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--page header-->
    <a name="Booking"></a>
    <header class="masthead text-center text-white d-flex">
        <div class="container my-auto">
            <div class="row">
                <div class="col-lg-10 mx-auto">
                    <h1 class="text-uppercase">
                        <strong>Cheveux</strong>
                    </h1>
                    <hr>
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

    <!-- Service Section -->
    <section id="Service" class="bg-primary text-white">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">At Your Service</h2>

                    <hr class="my-4">
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-3 col-md-6 text-center">
                    <h3 class="mb-3">
                        <asp:Label ID="LService1Header" runat="server" Text="Label"></asp:Label>
                    </h3>
                    <p class="mb-0">
                        <asp:Label ID="LService1Description" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <h3 class="mb-3">
                        <asp:Label ID="LService2Header" runat="server" Text="Label"></asp:Label>
                    </h3>
                    <p class="mb-0">
                        <asp:Label ID="LService2Description" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <h3 class="mb-3">
                        <asp:Label ID="LService3Header" runat="server" Text="Label"></asp:Label>
                    </h3>
                    <p class="mb-0">
                        <asp:Label ID="LService3Description" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="col-lg-3 col-md-6 text-center">
                    <h3 class="mb-3">
                        <asp:Label ID="LService4Header" runat="server" Text="Label"></asp:Label>
                    </h3>
                    <p class="mb-0">
                        <asp:Label ID="LService4Description" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <hr class="my-4">
                    <a class="btn btn-light btn-xl js-scroll-trigger" href="Cheveux/Services.aspx">View All Services</a>
                </div>
            </div>
        </div>
    </section>

    <!--Product-->
    <section id="Products" class="bg-dark text-white">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 mx-auto text-center">
                    <h2 class="section-heading">Our Products</h2>
                    <hr class="my-4">
                    <!--Line Break-->
                    <br />
                    <br />
                </div>
            </div>
        </div>

        <div class="container-fluid p-0">
            <div class="row no-gutters">
                <div class="col-lg-4 col-md-12">
                    <a class="portfolio-box" href="Default.aspx?Goto=Prod1">
                        <asp:PlaceHolder runat="server" ID="phProductImage1"></asp:PlaceHolder>
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-name">
                                    <asp:Label ID="lProductHeader1" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="project-category text-faded">
                                    <asp:Label ID="LProductDescription1" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-12">
                    <a class="portfolio-box" href="Default.aspx?Goto=Prod2">
                        <asp:PlaceHolder runat="server" ID="phProductImage2"></asp:PlaceHolder>
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-name">
                                    <asp:Label ID="lProductHeader2" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="project-category text-faded">
                                    <asp:Label ID="LProductDescription2" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-12">
                    <a class="portfolio-box" href="Default.aspx?Goto=Prod3">
                        <asp:PlaceHolder runat="server" ID="phProductImage3"></asp:PlaceHolder>
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-name">
                                    <asp:Label ID="lProductHeader3" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="project-category text-faded">
                                    <asp:Label ID="LProductDescription3" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <!--Line Break-->
        <br />
        <br />
        <div class="container">
            <div class="row">
                <div class="col-lg-8 mx-auto text-center">
                    <a class="btn btn-primary btn-xl js-scroll-trigger" href="Cheveux/Products.aspx">View All Products</a>
                </div>
            </div>
        </div>
    </section>

    <!--Stylists-->
    <section id="Hairstylist" class="bg-dark text-white">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 mx-auto text-center">
                    <h2 class="section-heading">Meet Our Stylist</h2>
                    <hr class="my-4">
                    <!--Line Break-->
                    <br />
                    <br />
                </div>
            </div>
        </div>

        <div class="container-fluid p-0">
            <div class="row no-gutters">
                <div class="col-lg-4 col-md-12">
                    <a class="portfolio-box" href="Default.aspx?Goto=Sty1">
                        <asp:PlaceHolder runat="server" ID="phStylistImage1"></asp:PlaceHolder>
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-name">
                                    <asp:Label ID="lStylistHeader1" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="project-category text-faded">
                                    <asp:Label ID="lStylistDescription1" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-12">
                    <a class="portfolio-box" href="Default.aspx?Goto=Sty2">
                        <asp:PlaceHolder runat="server" ID="phStylistImage2"></asp:PlaceHolder>
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-name">
                                    <asp:Label ID="lStylistHeader2" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="project-category text-faded">
                                    <asp:Label ID="lStylistDescription2" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-12">
                    <a class="portfolio-box" href="Default.aspx?Goto=Sty3">
                        <asp:PlaceHolder runat="server" ID="phStylistImage3"></asp:PlaceHolder>
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-name">
                                    <asp:Label ID="lStylistHeader3" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="project-category text-faded">
                                    <asp:Label ID="lStylistDescription3" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <!--Line Break-->
        <br />
        <br />
        <div class="row">
            <div class="col-lg-8 mx-auto text-center">
                <a class="btn btn-primary btn-xl js-scroll-trigger" href="Cheveux/Stylists.aspx">View All Stylists</a>
            </div>
        </div>
    </section>


    <!--Contact Us-->
    <section id="ContactUs">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 mx-auto text-center">
                    <h2 class="section-heading">Were open for business!</h2>
                    <hr class="my-4">
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-center">
                    <h5>Weekdays
                    </h5>
                </div>
                <div class="col-4 text-center">
                    <h5>Weekends
                    </h5>
                </div>
                <div class="col-4 text-center">
                    <h5>Public Holidays
                    </h5>
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-center">
                    <asp:Label ID="lWeekdaye" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-4 text-center">
                    <asp:Label ID="lWeekend" runat="server" Text="Label"></asp:Label>
                </div>
                <div class="col-4 text-center">
                    <asp:Label ID="lPublicHol" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <!-- Line Break -->
            <br />
            <div class="row">
                <div class="col-lg-8 mx-auto text-center">
                    <h2 class="section-heading">Connect With Us!</h2>
                    <hr class="my-4">
                </div>
            </div>

            <div class="row">
                <div class="col-2"></div>
                <div class="col-8 text-center">
                    <div class="row">

                        <div class="col-2 text-center"></div>
                        <div class="col-4 text-center">
                            <a href='Default.aspx?Goto=Phone' class='btn btn-primary btn-xl js-scroll-trigger'><span class='glyphicon'>&#9742;</span> Phone </a>
                        </div>
                        <div class="col-4 text-center">
                            <a href='Default.aspx?Goto=Email' class='btn btn-primary btn-xl js-scroll-trigger'><span class='glyphicon'>&#128231;</span> Email </a>
                        </div>
                        <div class="col-2 text-center"></div>

                    </div>
                </div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-lg-12 ml-auto text-center">
                    <br />
                    <div class="embed-responsive embed-responsive-16by9">
                        <iframe
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3307.568475806453!2d25.667619415705655!3d-34.00361618061904!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1e6532ce6cb4c0f1%3A0x9d4d0eeed57a0841!2sUniversity+Way%2C+Port+Elizabeth!5e0!3m2!1sen!2sza!4v1531901636092"
                            width="1050" height="525" frameborder="0" style="border: 0" allowfullscreen></iframe>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
