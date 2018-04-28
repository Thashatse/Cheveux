<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cheveux.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Chveux</title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />

    <!--Google Authentecation code-->
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <meta name="google-signin-client_id" content="668357274065-dcicj2ak0lgus05beethuibpbcbt11g3.apps.googleusercontent.com">
</head>

<body>
    <form id="form1" runat="server">
        <!--Nav Bar-->
        <nav class="navbar navbar-default" role="navigation">
            <div class="container-fluid">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">
                        <img src="/IMG_0715.png" alt="logo" width="75" height="75" /></a>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="#Service">Service</a></li>
                        <li><a href="#Products">Products</a></li>
                        <li><a href="#Hairstylist">Hairstylist</a></li>
                        <li><a href="#ContactUs">Contact Us</a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="Accounts.aspx">Login / Sign Up</a></li>
                        <a href="Accounts.aspx" onclick="signOut();">Sign out</a>
                    </ul>
                    <form class="navbar-form navbar-right">
                        <div class="form-group">
                            <input type="text" placeholder="Search">
                            <button type="submit" class="btn btn-default">Search</button>
                        </div>
                    </form>
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container-fluid -->
        </nav>

        <div class="container">
            <!--jumbotron page heading-->
            <div class="jumbotron">
                <!--Jambotron Content Here-->
                <div class="btn btn-outline-dark">Make A Booking </div>
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

            <!--Hairstlist selector-->
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <a name="Hairstylist"></a>
                    <h2>Hairstlist </h2>
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
    </form>
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
        <!--Google Authentecation sign out code-->
    <script>
        function signOut() {
            var auth2 = gapi.auth2.getAuthInstance();
            auth2.signOut().then(function () {
                console.log('User signed out.');
            });
        }
    </script>
</body>
</html>
