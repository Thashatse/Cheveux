<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="Cheveux.Accounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - Cheveux</title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />

    <!--CSS-->

    <!--Google Authentecation code-->
    <script src="https://apis.google.com/js/platform.js" async defer></script>
    <meta name="google-signin-client_id" content="668357274065-dcicj2ak0lgus05beethuibpbcbt11g3.apps.googleusercontent.com">
</head>
<body>
    <!--form for login in-->
    <form id="Login" runat="server">
        <div>
            <div class="container container-table">
                <!--jumbotron-->
                <div class="jumbotron vertical-center">
                    <div class="row vertical-center-row">
                        <div class="col-xs-12 col-md-12 text-center">
                            <!--Logo-->
                            <img src="/IMG_0715.png" alt="logo" width="300" height="300" />
                        </div>
                        <div class="col-xs-12 col-md-12 text-center">
                            <!--line break-->
                            <br />
                            <!--sign in buton-->
                            <div class="g-signin2" data-onsuccess="onSignIn" runat="server"></div>
                        </div>
                        <div class="col-xs-12 col-md-12 text-center">
                            <!--Sign out button-->
                            <a href="" onclick="signOut();" hidden="true" id="SO">Sign out</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Button runat="server" ID="btnAuthenticate" Style="display: none;" OnClick="btnAuthenticate_Click" />

        <!--Test Profile data-->
        <asp:Label ID="ph2" runat="server" Visible="false"></asp:Label>

    </form>
    <!--form for regestering a user-->
    <form id="Register" runat="server" visible="false">
        <div>
            <div class="container container-table">
                <div class="jumbotron">
                    <!--Logo-->
                    <img src="/IMG_0715.png" alt="logo" width="100" height="100" />
                    <h1>Register</h1>
                    <asp:Label ID="almostThere" runat="server" Text="Label"></asp:Label>
                    <div class="row vertical-center-row">
                        <div class="col-xs-12 col-md-12">
                            <!--Get a username from the user-->
                            <asp:Label ID="Label3" runat="server" Text="User Name:"></asp:Label>
                            <asp:TextBox ID="userName" runat="server" placeholder="Placecholder"></asp:TextBox>
                            <!--line break-->
                            <br /><br />
                            <!--Get a contact number from the user-->
                            <asp:Label ID="Label4" runat="server" Text="Contact Number:"></asp:Label>
                            <asp:TextBox ID="contactNumber" runat="server" placeholder="041 243 8389"></asp:TextBox>
                            <!--Submition Button-->
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!--google get user data function-->
    <script> 
        function onSignIn(googleUser) {
            var profile = googleUser.getBasicProfile();
            document.cookie = "reg="+profile.getId() + "|" + profile.getEmail() + "|" + profile.getGivenName() + "|" +
                profile.getFamilyName() + "|" + profile.getImageUrl();
            document.getElementById("btnAuthenticate").click();
            //Show the sign out button
            document.getElementById("SO").hidden = false;
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
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>