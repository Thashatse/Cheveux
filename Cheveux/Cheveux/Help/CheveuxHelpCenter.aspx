<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheveuxHelpCenter.aspx.cs" Inherits="Cheveux.Help.CheveuxHelpCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cheveux Help Center</title>
    <!-- Bootstrap-->
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <!--CSS-->
    <link rel="“stylesheet”" type="“text/css”" href="“/CSS/Cheveux.css”">
</head>
<body>
    <form id="HelpForm" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <!--Jumbotron Header-->
                        <div class="jumbotron">
                            <img src="/IMG_0715.png" alt="logo" width="150" height="150" />
                            <h1>Help Center</h1>
                            <p>Welcome to the Cheveux Help Center</p>
                        </div>

                        <!--Nav Pills-->
                        <ul class="nav nav-pills nav-stacked">
                            <li class="active"><a href="../Default.aspx">Cheveux Home </a></li>
                            <li><a href="#UserAccounts">User Accounts </a></li>
                            <li><a href="#Bookings">Bookings </a></li>
                            <li><a href="#3">Help 3 </a></li>
                        </ul>

                    </div>
                </div>

                <!--Help Section Template -->
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <a name="UserAccounts"></a>
                        <!--Header-->
                        <h2>User Accounts </h2>
                        <!--Content-->
                        <!--Login with google-->
                        <h4>Login with google</h4>
                            <ul>
                                <li>Cheveux allows you to sign in with your existing google account.</li>
                                <li>Google provides us with your name, surname & email address while giving you one less password to remember.</li>
                                <li>To sign in with Google </li>
                                <ul>
                                    <img src="Helpimages/Login-SignUp.png" alt="sign in with Google" />
                                    <li>1) Select Login/Sign up in the navigation bar</li>
                                    <img src="Helpimages/GoogleSignin.png" alt="sign in with Google" />
                                    <li>2) Select the ‘sign in’ button with the Google logo</li>
                                    <li>3) Follow the steps in the Google pop up box</li>
                                    <img src="Helpimages/Signedinwithgoogle.png" alt="sign in with Google" />
                                    <li>You are now signed in. if you are new to Cheveux you will have to complete the registration process entering a username and contact number.</li>
                                </ul>
                            </ul>
                        <!--Usernames-->
                        <h4>Usernames</h4>
                            <ul>
                                <li>A user name is a unique way to identify you on the Cheveux platform</li>
                            </ul>
                        <!--Cellphone Numbers-->
                        <h4>Celphone Numbers</h4>
                        <ul>
                            <li>We require your 10-digit South African cell phone number</li>
                            <li>We Use your contact number to send you conformation and reminders of booking, invoices and promotions.</li>
                        </ul>
                        <!--Loggin Out-->
                        <h4>Logging Out Of Cheveux</h4>
                        <ul>
                            <img src="Helpimages/LogOut.png" alt="Logging Out Of Cheveux" />
                            <li>1) Select 'Log Out' in the navigation bar</li>
                            <img src="Helpimages/LogedOut.png" alt="Logged Out Of Cheveux" />
                            <li>You are now logged out of Cheveux</li>
                        </ul>
                    </div>
                </div>

                <!--line Break-->
                <br /><br />
                <!--New Section-->
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <a name="Bookings"></a>
                        <!--Header-->
                        <h2>Bookings </h2>
                        <!--Content-->
                        <!--Upcoming Bookings-->
                        <h4>View Upcoming Bookins</h4>
                        <ul>
                            <li>First <a href="#UserAccounts">Log In</a></li>
                            <img src="Helpimages/BookingsNavBar.png" alt="View Bookings" />
                            <li>1) Select 'Bookings'in the navigation bar</li>
                            <li>2) Your upcoming bookings are displayed by default</li>
                            <img src="Helpimages/BookingsPageViewBooking.png" alt="View Bookings" />
                            <li>3) To display booking details select ‘View Booking’</li>
                            <img src="Helpimages/UpcomingBooking.png" alt="View Bookings" />
                            <li>4) Booking details will now be displayed</li>
                        </ul>
                        <!--Past Bookings-->
                        <h4>View Past Bookins</h4>
                        <ul>
                            <li>First <a href="#UserAccounts">Log In</a></li>
                            <img src="Helpimages/BookingsNavBar.png" alt="View Past Bookings" />
                            <li>1) Select 'Bookings' in the navigation bar</li>
                            <li>2) Your upcoming bookings are displayed by default</li>
                            <img src="Helpimages/BookingsPagePastBookings.png" alt="View Past Bookings" />
                            <li>3) To see past bookings select the ‘Past Bookings’ tab</li>
                            <li>4) Past bookings will now be displayed</li>
                            <img src="Helpimages/BookingsPageViewPastBooking.png" alt="View Past Bookings" />
                            <li>5) To display booking details select ‘View Booking’</li>
                            <img src="Helpimages/ViewPastBookings.png" alt="View Past Bookings" />
                            <li>6) The booking details and invoice will now be displayed</li>
                        </ul>
                    </div>
                </div>

                    <!--line Break-->
                <br /><br />
                <!--New Section-->
                <!--Help Section Template 3-->
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <a name="3"></a>
                        <!--Header-->
                        <h2>Section 3 </h2>
                        <!--Content-->
                        <p>Content</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!--Bootstrap-->
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>
