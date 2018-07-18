<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../Authentication/Accounts.aspx.cs" Inherits="Cheveux.Accounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - Cheveux</title>

        <!-- Theam -->

    <!-- Bootstrap core CSS -->
    <link href="../Theam/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom fonts for this template -->
    <link href="../Theam/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic,900,900italic' rel='stylesheet' type='text/css'>

    <!-- Plugin CSS -->
    <link href="../Theam/vendor/magnific-popup/magnific-popup.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../css/creative.min.css" rel="stylesheet">

    <!-- Our CSS-->
    <link rel="stylesheet" type="text/css" href="../CSS/Accounts.css">

    <!-- Default Theam

    <!--Bootstrap
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <!--CSS
    <link rel="stylesheet" type="text/css" href="../CSS/Accounts.css">
-->

    <!--Google Authentecation code-->
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <meta name="google-signin-client_id" content="668357274065-dcicj2ak0lgus05beethuibpbcbt11g3.apps.googleusercontent.com">
</head>
<body>
    <!--form for login in-->
    <form id="Login" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-2"></div>
                    <div class="col-6 text-center">
                        <!--jumbotron-->
                        <div class="jumbotron">
                            <!--heading-->
                            <h2>Get started with<b> Cheveux</b1></h2>
                            <!--line break-->
                            <br />
                            <!--Logo-->
                            <img src="../IMG_0715.png" alt="logo" width="300" height="300" />
                            <!--line break-->
                            <br />
                            <br />
                            <!-- sellect account Type -->
                            <div class="container" runat="server" id="divAccountType">
                                <!--sign in with email buton-->
                                <button type='button' class='btn'>
                                    <a href="../Authentication/Accounts.aspx?Type=Email">
                                        <span class='glyphicon'>&#128231;</span> Sign In With Email
                                    </a>
                                </button>
                                <!--line break-->
                                <br />
                                <br />
                                <!--sign in with google buton-->
                                <div class="g-signin2" data-onsuccess="onSignIn" runat="server"></div>
                            </div>

                            <!-- email account Type login -->
                            <div class="container" runat="server" id="divEmailAcount" visible="false">
                                <!-- Error Lable -->
                                <asp:Label ID="lError" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                <!--line break-->
                                <br />

                                <!-- Enter Email -->
                                <asp:TextBox ID="txtEmailUsername" runat="server" placeholder="Email or Username"></asp:TextBox>
                                <!--line break-->
                                <br />
                                <!--Email / User Name Validation-->
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmailUsername" runat="server"
                                    ErrorMessage="*Email or Username is required" ControlToValidate="txtEmailUsername"
                                    ForeColor="Red"></asp:RequiredFieldValidator>

                                <!-- email account Type login enter password -->
                                <div class="container" runat="server" id="divPassword" visible="false">
                                    <!-- Enter Password -->
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="password"></asp:TextBox>
                                    <!--line break-->
                                <br />
                                    <!--Password Validation-->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxtPassword" runat="server"
                                        ErrorMessage="*Password is required" ControlToValidate="txtPassword"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <!-- email account Type login next btn -->
                                <div class="container" runat="server" id="divNext">
                                    <!--next Button-->
                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="displayPassword" Font-Bold="true" Width="150" Height="50" />
                                </div>

                                <!-- email account Type login sign in button -->
                                <div class="container" runat="server" id="divSignIn" visible="false">
                                    <!--Submition Button-->
                                    <asp:Button ID="btnSignIn" runat="server" Text="Sign In" OnClick="signIn" Font-Bold="true" Width="150" Height="50" />
                                </div>

                                <!--line break-->
                                <br />
                                <!--Sign Up Button -->
                                <a href="../Authentication/NewAccount.aspx?Type=Email">Create Acount</a>

                            </div>
                            <!--line break-->
                            <br />
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="Sign in with your Google account">
                                <span class="glyphicon">&#63; Help</span>
                            </a>
                        </div>
                    </div>
                    <div class="col-2"></div>
                </div>
            </div>
        </div>
        <!--Sign out button-->
        <a href="" onclick="signOut();" hidden="true" id="SO">Sign out</a>
        <asp:Button runat="server" ID="btnAuthenticate" Style="display: none;" OnClick="btnAuthenticate_Click" />
    </form>
    <!--google get user data function-->
    <script> 
        function onSignIn(googleUser) {
            var profile = googleUser.getBasicProfile();
            document.cookie = "reg=" + profile.getId() + "|" + profile.getEmail() + "|" + profile.getGivenName() + "|" +
                profile.getFamilyName() + "|" + profile.getImageUrl() + "|Google|END";
            document.getElementById("btnAuthenticate").click();
        }
    </script>

    <!--google sign out function-->
    <script>
        function signOut() {
            var auth2 = gapi.auth2.getAuthInstance();
            auth2.signOut().then(function () {
                console.log('User signed out.');
            });
        }
    </script>
    <!--Bootstrap-->
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
</body>
</html>
