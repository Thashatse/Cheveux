<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Cheveux.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error - Cheveux</title>
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
    </div>
    <form id="ErrorForm" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="jumbotron">
                            <div class="row">
                    <div class="col-md-2">
                            <!-- Error Image-->
                            <img src="https://cdn4.iconfinder.com/data/icons/smiley-vol-3-2/48/134-512.png" alt="Error" width="100" height="100"></img>
                            </div>
                                <div class="col-md-10">
                            <!--Error details placehoders-->
                            <asp:Label ID="ErrorHeader" runat="server"></asp:Label><br />
                            <asp:Label ID="Error1" runat="server"></asp:Label>
                                    </div>
                                </div>
                            <!--Line Break-->
                            <br />
                            <!--Help-->
                            <a href="Help/CheveuxHelpCenter.aspx" target="_blank">Help Center</a>
                            <!--Line Break-->
                            <br />
                            <br />
                            <a href="Default.aspx">Return Home</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

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