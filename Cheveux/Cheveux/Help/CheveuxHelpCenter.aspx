<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheveuxHelpCenter.aspx.cs" Inherits="Cheveux.Help.CheveuxHelpCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cheveux Help Center</title>
    <!-- Bootstrap-->
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <!--CSS-->
    <link rel="stylesheet" type="text/css" href="/CSS/HelpCenter.css">

    <!--Bootstrap font awesome for the icons-->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" integrity="sha384-DNOHZ68U8hZfKXOrtjWvjxusGo9WQnrNx2sqG0tfsghAvtVlRW3tvkXWZh58N9jp" crossorigin="anonymous">

</head>
<body>
    <form id="HelpForm" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <!--Jumbotron Header-->
                        <div class="jumbotron theTop">
                            <img src="/IMG_0715.png" alt="logo" width="150" height="150" />
                            <h1>Help Center</h1>
                            <p>Welcome to the Cheveux Help Center</p>
                        </div>

                        <!--Nav Pills for external system-->
                        <ul class="nav nav-pills nav-stacked">
                            <li class="active"><a href="../Default.aspx">Cheveux Home &nbsp;&nbsp;</a></li>
                            <li><a href="#UserAccounts">User Accounts &nbsp; &nbsp;</a></li>
                            <li><a href="#Bookings">Bookings  &nbsp; &nbsp;</a></li>
                            <li><a href="#InternalHelp">Internal System &nbsp; &nbsp;</a></li>
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
                <br />
                <br />
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
                <br />
                <br />
                <!-- Internal Help displayed if user is logged in-->
                <div class="row">
                    <div class="col-md-12">
                        <!-- if the user is loged Out -->
                        <a name="InternalHelp"></a>
                        <div class="jumbotron">
                            <h1>Internal Help</h1>
                            <div class="container" runat="server" id="JumbotronLogedOut">
                                <h3>Please log-in</h3>
                                <p>You must bee loged in as Receptionist, Stylist or Manager to view this section</p>
                                <button type="button" class="btn btn-default"><a href="../Accounts.aspx?PreviousPage=Help/CheveuxHelpCenter.aspx#InternalHelp" id="LogedOut">Login</a></button>
                            </div>
                        </div>
                    </div>

                    <!-- Internal Help displayed if user is logged in-->
                    <div class="container" runat="server" id="LogedIn" visible="false">
                        <div class="row">
                            <div class="col-md-12">
                                <!--Nav Pills for internal system-->
                                <ul class="nav nav-pills nav-stacked">
                                    <li class="active"><a href="../Default.aspx">Cheveux Home &nbsp;&nbsp;</a></li>
                                    <li><a href="#ReceptionistCheckIn">Receptionist Customer Check-In &nbsp; &nbsp;</a></li>
                                    <li><a href="#StylistCustomerVisit">Stylist Customer Visit &nbsp; &nbsp;</a></li>
                                    <li><a href="#BusinessSettings">Business Setting &nbsp; &nbsp;</a></li>
                                </ul>
                            </div>
                        </div>

                        <!--line Break-->
                        <br />
                        <br />
                        <!--New Section-->
                        <!--Help Section Template 3-->
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <a name="ReceptionistCheckIn"></a>
                                <!--Header-->
                                <h2>Receptionist Check-in</h2>
                                <!--Content-->
                                <p>
                                    <ul>
                                        <li>Step 1) Log In</li>
                                        <img src="Helpimages/Login-SignUp.png" alt="Login" />
                                        <li>Step 2)Select dropdown list to select employee's schedule you would like to view </li>
                                        <img src="Helpimages/Dropdown.png" alt="Dropdown" />
                                        <li>Stylists Agenda is displayed</li>
                                        <img src="Helpimages/viewAgenda.png" alt="viewAgenda" />
                                        <li>Step 3)Look which time slot the customer has come in for and select check-in</li>
                                        <img src="Helpimages/CheckIn.png" alt="Check In" />
                                        <li>Button will change to check-out</li>
                                        <img src="Helpimages/CheckOut.png" alt="Check-out" />
                                        <li>When the customer has comleted their appointment check them out to generate invoice using the 
                                "Check-out" button
                                        </li>
                                    </ul>
                                </p>
                            </div>
                        </div>


                        <!--line Break-->
                        <br />
                        <br />
                        <!--New Section-->
                        <!--Help Section Template 4-->
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <a name="StylistCustomerVisit"></a>
                                <!--Header-->
                                <h2>Stylist Customer Visit</h2>
                                <!--Content-->
                                <p>
                                    <ul>
                                        <li>Step 1) Log In</li>
                                        <img src="Helpimages/Login-SignUp.png" alt="Login" />
                                        <li>Displays customer booking (if the customer has arrived for their booking) </li>
                                        <li>Step 1) Select "Customer Visit Record" to create a visit record</li>
                                        <img src="Helpimages/createVisit.png" alt="Create visit" />
                                        <li>You will be redirected to booking details page which booking details</li>
                                        <img src="Helpimages/customerVisitBookingDetail.png" alt="Booking Detail" />
                                        <li>Step 2) To update service details click "Update"</li>
                                        <img src="Helpimages/serviceDTLs.png" alt="Service details" />
                                        <li>Step 3) Input service description and select "Update visit"</li>
                                        <img src="Helpimages/serviceInput.png" alt="Service description input" />
                                    </ul>
                                </p>
                            </div>
                        </div>

                        <!--line Break-->
                        <br />
                        <br />
                        <!--New Section-->
                        <!--Business settings-->
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <a name="BusinessSettings"></a>
                                <!--Header-->
                                <h2>Business Settings</h2>
                                <!--Content-->
                                <p>
                                    Business Settings is used by the manager to manage details about their salon including VAT, address, contact number, operating hours and the logo.
                                    <!--line Break-->
                                    <br />
                                    To access log in as a manager and select ‘Business Settings’ from the navigation bar.
                               <!--line Break-->
                                    <br />
                                    <a href="../BusinessSetting.aspx">Go to Business Settings</a>
                                </p>
                                <!--line Break-->
                                    <br />
                                <h3>Changing Business Settings</h3>
                                <p>
                                    <ul>
                                        <img src="Helpimages/BusinessSetingseditbutton.png" alt="Login" />
                                        <li>To change a business setting</li>
                                        <li>Step 1 of 3) Select the edit button to the right of the detail</li>
                                        <img src="Helpimages/BusinessSetingseditvalue.png" alt="Create visit" />
                                        <li>Step 2 of 3) Entered the updated value</li>
                                        <img src="Helpimages/BusinessSetingsSaveCancelButton.png" alt="Booking Detail" />
                                        <li>Step 3 of 3) Then select save to save the changes to the database or cancel to cancel the changes</li>
                                        <img src="Helpimages/BusinessSetingsSaved.png" alt="Service details" />
                                        <li>The changes are now saved to the database</li>
                                    </ul>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>

                <!--Sticky back-to-top button-->
                <div class='scrolltop'>
                    <div class='scroll icon'>
                        <i class="fa fa-4x fa-angle-up"></i>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!--Bootstrap-->
    <script src="../Scripts/jquery-3.3.1.min.js"></script>


    <!--jQuery to return user to the top of the page-->
    <script>

        $(document).ready(function () {
            $(window).scroll(function () {
                if ($(this).scrollTop() > 50) {
                    $('.scrolltop:hidden').stop(true, true).fadeIn();
                } else {
                    $('.scrolltop').stop(true, true).fadeOut();
                }
            });

            $(function () {
                $(".scroll").click(function () {
                    $("html,body").animate({ scrollTop: $(".theTop").offset().top }, "1000");
                    return false
                })
            });
        });
    </script>

    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>
