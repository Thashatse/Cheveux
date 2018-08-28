<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloseWindow.aspx.cs" Inherits="Cheveux.Authentication.CloseWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Logged In- Cheveux</title>
    <!-- Theam -->
    <!-- Bootstrap core CSS -->
    <link href="../Theam/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- Custom fonts for this template -->
    <link href="../Theam/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css'/>
    <link href='https://fonts.googleapis.com/css?family=Merriweather:400,300,300italic,400italic,700,700italic,900,900italic' rel='stylesheet' type='text/css'/>

    <!-- Plugin CSS -->
    <link href="../Theam/vendor/magnific-popup/magnific-popup.css" rel="stylesheet"/>

    <!-- Custom styles for this template -->
    <link href="../css/creative.min.css" rel="stylesheet"/>

    <!-- Our CSS-->
    <link rel="“stylesheet”" type="“text/css”" href="“/CSS/Cheveux.css”"/>
</head>
<body>
    <div>
        <!-- Top Margin -->
        <br />
        <br />
        <br />
        <br />
    </div>
    <div class="row">
        <div class="col-lg-3 col-sm-1 col-md-2"></div>
        <div class="col-lg-6 col-sm-10 col-md-8 text-center">
            <!--Logo-->
            <img src="https://cdn2.iconfinder.com/data/icons/tabico-nic/128/Tab_Cancel_X_Button_Cross_Close_Disable-512.png" alt="logo" width="150" height="150" class="img-fluid" />
            <!--Line Break-->
            <br />
            <br />
            <!-- Message -->
            <p>You are now logged in to <a class='navbar-brand js-scroll-trigger' href=''>Cheveux</a>as</p>
            <asp:PlaceHolder runat="server" ID="profile"></asp:PlaceHolder>
            <!--Line Break-->
            <br />
            <br />
            <p>Close this window to continue</p>
        </div>
        <div class="col-lg-3 col-sm-1 col-md-2"></div>
    </div>

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
