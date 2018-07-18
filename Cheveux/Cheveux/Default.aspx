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
                        <div class="service-box mt-5 mx-auto">
                            <h3 class="mb-3">
                                <asp:Label ID="LService1Header" runat="server" Text="Label"></asp:Label>
                            </h3>
                            <p class="mb-0">
                                <asp:Label ID="LService1Description" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 text-center">
                        <div class="service-box mt-5 mx-auto">
                            <h3 class="mb-3">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </h3>
                            <p class="mb-0">
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 text-center">
                        <div class="service-box mt-5 mx-auto">
                            <h3 class="mb-3">
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </h3>
                            <p class="mb-0">
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 text-center">
                        <div class="service-box mt-5 mx-auto">
                            <h3 class="mb-3">
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                            </h3>
                            <p class="mb-0">
                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                    </div>

                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <hr class="my-4">
                        <a class="btn btn-light btn-xl js-scroll-trigger" href="#services">View All Services</a>
                    </div>
                </div>
            </div>
        </section>
        
        <!--Product-->
        <section  id="Products" class="bg-dark text-white">
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
            <div class="row no-gutters popup-gallery">
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/1.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/1.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/2.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/2.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/3.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/3.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/4.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/4.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/5.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/5.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/6.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/6.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
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
                        <a class="btn btn-primary btn-xl js-scroll-trigger" href="#">View All Products</a>
                    </div>
                </div>
                </div>
        </section>
        
        <!--Stylists-->
                <section  id="Hairstylist" class="bg-dark text-white">
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
            <div class="row no-gutters popup-gallery">
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/1.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/1.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/2.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/2.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/3.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/3.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/4.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/4.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/5.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/5.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <a class="portfolio-box" href="../Theam/img/portfolio/fullsize/6.jpg">
                        <img class="img-fluid" src="../Theam/img/portfolio/thumbnails/6.jpg" alt="">
                        <div class="portfolio-box-caption">
                            <div class="portfolio-box-caption-content">
                                <div class="project-category text-faded">
                                    Category
                 
                                </div>
                                <div class="project-name">
                                    Project Name
                 
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
                        <a class="btn btn-primary btn-xl js-scroll-trigger" href="#">View All Stylists</a>
                    </div>
                </div>
                </div>
        </section>
    

        <!--Contact Us-->
        <section id="ContactUs" class="bg-dark text-white">
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 mx-auto text-center">
                        <h2 class="section-heading">Conect With Us!</h2>
                        <hr class="my-4">
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-9 ml-auto text-center">
                        <asp:Table ID="tblContactUs" runat="server"></asp:Table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 ml-auto text-center">
                        <hr class="my-4">
                        <iframe
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3307.568475806453!2d25.667619415705655!3d-34.00361618061904!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x1e6532ce6cb4c0f1%3A0x9d4d0eeed57a0841!2sUniversity+Way%2C+Port+Elizabeth!5e0!3m2!1sen!2sza!4v1531901636092"
                            width="1050" height="525" frameborder="0" style="border: 0" allowfullscreen></iframe>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
