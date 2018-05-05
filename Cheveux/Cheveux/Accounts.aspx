<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="Cheveux.Accounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In - Cheveux</title>
    <!--Bootstrap-->
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
        </form>
        

        <!--google get user data function-->
        <script> 
            function onSignIn(googleUser) {
                var profile = googleUser.getBasicProfile();
                document.cookie = "reg=" + profile.getId() + "|" + profile.getEmail() + "|" + profile.getGivenName() + "|" +
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
        <!--Bootstrap-->
        <script src="../Scripts/jquery-3.3.1.min.js"></script>
        <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>
