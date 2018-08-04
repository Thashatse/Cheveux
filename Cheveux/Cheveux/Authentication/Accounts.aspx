<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../Authentication/Accounts.aspx.cs" Inherits="Cheveux.Accounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - Cheveux</title>

    <!-- Theam -->
    <!-- Bootstrap core CSS -->
    <link href="../Theam/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom fonts for this template -->
    <link href="../Theam/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic,900,900italic' rel='stylesheet' type='text/css' />

    <!-- Plugin CSS -->
    <link href="../Theam/vendor/magnific-popup/magnific-popup.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="../css/creative.min.css" rel="stylesheet" />

    <!-- Our CSS-->
    <link rel="stylesheet" type="text/css" href="../CSS/Accounts.css" />

    <!--Google Authentecation code-->
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <meta name="google-signin-client_id" content="668357274065-dcicj2ak0lgus05beethuibpbcbt11g3.apps.googleusercontent.com" />

    <script>
        function CloseWindow() {
            window.close();
        }
    </script>
</head>
<body>
    <div>
        <!-- Top Margin -->
        <br />
        <br />
        <br />
    </div>
    <!--form for login in-->
    <form id="Login" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-2"></div>
                    <div class="col-6 text-center">
                        <!--jumbotron-->
                        <div class="jumbotron bg-dark text-white">
                            <!--heading-->
                            <asp:Label ID="lHeader" runat="server"></asp:Label>
                            <!--line break-->
                            <br />
                            <!--Logo-->
                            <img src="../IMG_0715.png" alt="logo" width="300" height="300" class="img-fluid" />
                            <!--line break-->
                            <br />
                            <br />

                            <!-- sellect account Type -->
                            <div class="container" runat="server" id="divAccountType">
                                <!--sign in with email buton-->
                                <asp:Button type='btnEmailSignIn' CssClass='btn btn-primary' OnClick="Unnamed_Click" runat="server" Text="&#128231;  Sign In With Email"></asp:Button>
                                <!--line break-->
                                <br />
                                <br />
                                <div class="row">
                                        <div class="col-4"></div>
                                    <div class="col-4" align="center">
                                <!--sign in with google buton-->
                                <div class="g-signin2" data-onsuccess="onSignIn" runat="server"></div>
                                        </div>
                                    <div class="col-4"></div>
                                    </div>
                            </div>

                            <!-- email account Type login -->
                            <div class="container" runat="server" id="divEmailAcount" visible="false">

                                <!-- Email Text Box & Validation-->
                                <div class="container" runat="server" id="divTxtBoxEmail">
                                    <!-- Error Lable -->
                                    <asp:Label ID="lError" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>

                                    <!-- Enter Email -->
                                    <asp:TextBox ID="txtEmailUsername" runat="server" placeholder="Email or Username" CssClass="form-control"></asp:TextBox>

                                    <!--Email / User Name Validation-->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmailUsername" runat="server"
                                        ErrorMessage="*Email or Username is required" ControlToValidate="txtEmailUsername"
                                        ForeColor="Red"></asp:RequiredFieldValidator>

                                </div>
                                <!-- email account Type login enter password -->
                                <div class="container" runat="server" id="divPassword" visible="false">
                                    <!-- Enter Password -->
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="password" CssClass="form-control"></asp:TextBox>

                                    <!--Password Validation-->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxtPassword" runat="server"
                                        ErrorMessage="*Password is required" ControlToValidate="txtPassword"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <!-- email account Type login next btn -->
                                <div class="container" runat="server" id="divNext">
                                    <!--next Button-->
                                    <asp:Button class='btn btn-primary' ID="btnNext" runat="server" Text="Next" OnClick="displayPassword" Font-Bold="true" Width="150" Height="50" />
                                </div>

                                <!-- email account Type login sign in button -->
                                <div class="container" runat="server" id="divSignIn" visible="false">
                                    <!--Submition Button-->
                                    <asp:Button class='btn btn-primary' ID="btnSignIn" runat="server" Text="Sign In" OnClick="signIn" Font-Bold="true" Width="150" Height="50" />
                                </div>

<!--line break-->
                                <br />
                                <!--rest Button -->
                                <a href="Accounts.aspx?action=Reset">Forgot Password?</a>
                                <!--line break-->
                                <br />
                                <br />
                                <!--Sign Up Button -->
                                <asp:Label ID="lCreateAccount" runat="server"></asp:Label>
                            </div>

                            <!-- Get Password reset Code -->
                            <div class="container" runat="server" id="divGetRestCode" visible="false">
                                <!-- Feedback -->
                                <asp:Label ID="lPaswordRestCodeFeedback" runat="server" Text="test" Visible="false"></asp:Label>
                                <div class="container"  runat="server" id="divGetEmailToReset" visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt email -->
                                            <asp:TextBox CssClass="form-control" ID="txtEmailToReset" runat="server" placeholder="Email"></asp:TextBox>
                                            <!-- Error Lable -->
                                            <asp:Label ID="lEamailResetError" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt email validation -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmailToReset" runat="server" ErrorMessage="*Email is Required" ControlToValidate="txtEmailToReset" ForeColor="Red"></asp:RequiredFieldValidator>
                                        &ensp;<asp:RegularExpressionValidator ID="RegularExpressionValidatortxtEmailAddress" runat="server" ErrorMessage="Sorry, please enter a valid email address"
                                        ValidationExpression="(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|'(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*')@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])"
                                        ControlToValidate="txtEmailToReset" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
<div class="row">
                                        <div class="col-12">
                                            <!-- reset Pasword -->
                                            <asp:Button ID="btnRestPassword" runat="server" Text="Rest Pasword" CssClass="btn btn-primary" OnClick="btnRestPassword_Click" />
                                        </div>
                                    </div>
                            </div>

                            <!-- reset Password -->
                            <div runat="server" id="divResetPasword" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <!-- Username -->
                                        <asp:Label ID="lPaswordResetUsernameLable" runat="server"></asp:Label>
                                        <!--line break-->
                                        <br />
                                        <br />
                                    </div>
                                </div>
                                <div runat="server" id="divexistingPass" visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt Pasword -->
                                            <asp:TextBox CssClass="form-control" ID="txtExistingPassword" runat="server" placeholder="Password" TextMode="password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt Pasword validation -->
                                            <asp:Label ID="wrongExsistingPass" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Password is Required" ControlToValidate="txtExistingPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    </div>
                                <div runat="server" id="divResetPaswordtxtPass" visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt Pasword -->
                                            <asp:TextBox CssClass="form-control" ID="txtNewPasword" runat="server" placeholder="New Password" TextMode="password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt Pasword validation -->
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtNewPasword" runat="server" ErrorMessage="*Password is Required" ControlToValidate="txtNewPasword" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt Pasword confirm -->
                                            <asp:TextBox CssClass="form-control" ID="txtConfirmNewPassword" runat="server" placeholder="Confirm New Password" TextMode="password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <!-- Txt Pasword confirmation validation -->
                                            <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="txtNewPasword" ControlToValidate="txtConfirmNewPassword" ErrorMessage="Passwords do not match" ForeColor="Red"></asp:CompareValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                        <div class="col-12">
                                            <!-- reset Pasword -->
                                            <asp:Button ID="btnChangePass" runat="server" Text="Change Pasword" CssClass="btn btn-primary" OnClick="btnChangePass_Click" />
                                        </div>
                                    </div>
                            </div>
                            <!--line break-->
                            <br />
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="Sign in with your Google account">
                                <span class="glyphicon">Help &#63;</span>
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
    <!-- Bootstrap core JavaScript -->
    <script src="../Theam/vendor/jquery/jquery.min.js"></script>
    <script src="../Theam/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../Theam/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="../Theam/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../Theam/vendor/scrollreveal/scrollreveal.min.js"></script>
    <script src="../Theam/vendor/magnific-popup/jquery.magnific-popup.min.js"></script>

    <!-- Custom scripts for this template -->
    <script src="../Theam/js/creative.min.js"></script>
</body>
</html>
