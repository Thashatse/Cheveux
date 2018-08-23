<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAccount.aspx.cs" Inherits="Cheveux.NewAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - Cheveux</title>


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

</head>
<body>
    <div class="container" id="Div1">
            <!-- Top Margin -->
            <br />
            <br />
            <br />
        </div>
    <!--form for regestering a new google user-->
    <form id="RegisterGoogleUser" runat="server" visible="false">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-xs-2 col-md-2 text-center"></div>
                    <div class="col-xs-8 col-md-8 text-center">
                        <div class="jumbotron bg-dark text-white vertical-center">
                            <!--Logo-->
                            <img src="../IMG_0715.png" alt="logo" width="100" height="100" />
                            <!--line break-->
                            <br />
                            <asp:Label ID="almostThere" runat="server" Text="Label" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <!--line break-->
                            <br />
                            <br />
                            <!--Get a username from the user-->
                            <asp:Label ID="Label3" runat="server" Text="User Name:"></asp:Label>
                            <asp:TextBox ID="userName" runat="server" placeholder="Placecholder" CssClass="form-control"
                                OnTextChanged="userName_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="This is used to identify you on the Cheveux platform">
                                <span class="glyphicon">&#63;</span>
                            </a>
                            <!--line break-->
                            <br />
                            <!--userName Validation-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsrname" runat="server" ErrorMessage="*Required" ControlToValidate="userName" ForeColor="Red"></asp:RequiredFieldValidator>
                            &ensp;
                            <asp:Label ID="LGoogleUsernameExists" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                            <!--line break-->
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Sorry, your username must be between 6 and 30 charates long" ValidationExpression="^[\s\S]{6,30}$" ControlToValidate="userName" ForeColor="Red"></asp:RegularExpressionValidator>
                            <!--line break-->
                            <br />
                            <!--Get a contact number from the user-->
                            <asp:Label ID="Label4" runat="server" Text="Contact Number:"></asp:Label>
                            <asp:TextBox ID="contactNumber" runat="server" placeholder="041 243 8389" CssClass="form-control"></asp:TextBox>
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="10-digit RSA Cellphone number used is contact you and keep you up to date">
                                <span class="glyphicon">&#63; (Optional)</span>
                            </a>
                            <!--line break-->
                            <br />
                            <!--contactNumber Validation-->
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="contactNumber" ErrorMessage="Please enter a valid phone number" ForeColor="Red" ValidationExpression="^0[0-9]{9}$"></asp:RegularExpressionValidator>
                            <!--line break-->
                            <br />
                            <!--Submition Button-->
                            <asp:Button CssClass="btn btn-primary" ID="btnSubmitGoogle" runat="server" Text="Submit" OnClick="btnSubmitGoogle_Click" Font-Bold="true" Width="150" Height="50" />
                        </div>
                    </div>
                    <div class="col-xs-2 col-md-2 text-center"></div>
                </div>
            </div>
        </div>
    </form>

    <!--form for regestering a new email user-->
    <form id="registeEmailUser" runat="server" visible="false">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-xs-2 col-md-2 text-center"></div>
                    <div class="col-xs-8 col-md-8 text-center">
                        <div class="jumbotron bg-dark text-white vertical-center">
                            <!--Logo-->
                            <img src="../IMG_0715.png" alt="logo" width="100" height="100" />
                            <h2>Create your Cheveux Account</h2>
                            <!--line break-->
                            <br />
                            <div class="row">
                                <div class="col-2 text-left">
                                    <!--Get the users last and first name-->
                                    <!-- Lable -->
                                    <asp:Label ID="name" runat="server" Text="Name:"></asp:Label>
                                </div>
                                <div class="col-4">
                                    <!--input -->
                                    &ensp;&ensp;<asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <!--Help-->
                                    <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="Your First Name">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                </div>
                                <div class="col-4 ">
                                    <!--input -->
                                    <asp:TextBox CssClass="form-control" ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <!--Help-->
                                    <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="Your Last Name">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left"></div>
                                <div class="col-5">
                                    <!--Validation-->
                                    &ensp;&ensp;<asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFirstName" runat="server" ErrorMessage="*First Name is Required" ControlToValidate="txtFirstName" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-5">
                                    <!--Validation-->
                                    &ensp;&ensp;&ensp;&ensp;<asp:RequiredFieldValidator ID="RequiredFieldValidatortxtLastName" runat="server" ErrorMessage="*Last Name is Required" ControlToValidate="txtLastName" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left">
                                    <!--Get a username from the user-->
                                    <!-- Lable -->
                                    <asp:Label ID="LUsername" runat="server" Text="Username:"></asp:Label>
                                </div>
                                <div class="col-9">
                                    <!--input -->
                                    &ensp;&ensp;<asp:TextBox CssClass="form-control" ID="txtUsername" runat="server" 
                                        placeholder="Username" OnTextChanged="txtUsername_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <!--Help-->
                                    <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="This is used to identify you on the Cheveux platform">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left"></div>
                                <div class="col-10">
                                    <!--Validation-->
                                    &ensp;&ensp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Username is required" ControlToValidate="txtUsername" ForeColor="Red"></asp:RequiredFieldValidator>
                                    &ensp;
                                    <asp:Label ID="LUsernmaeExists" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left"></div>
                                <div class="col-10">
                                    <!--Validation-->
                                    &ensp;&ensp;<asp:RegularExpressionValidator ID="RegularExpressionValidatortxtUsername" runat="server" ErrorMessage="Sorry, your username must be between 6 and 30 charates long" ValidationExpression="^[\s\S]{6,30}$" ControlToValidate="txtUsername" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left">
                                    <!--Get a email address from the user-->

                                    <!-- Lable -->
                                    <asp:Label ID="lEmail" runat="server" Text="Email:"></asp:Label>
                                </div>
                                <div class="col-9">
                                    <!--input -->
                                    &ensp;&ensp;<asp:TextBox CssClass="form-control" ID="txtEmailAddress" runat="server" AutoPostBack="true" 
                                        placeholder="Email Address" OnTextChanged="txtEmailAddress_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <!--Help-->
                                    <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="This is contact you and keep you up to date">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left"></div>
                                <div class="col-10 text-left">
                                    <!--Validation-->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtEmailAddress" runat="server" ErrorMessage="*Email address is required" ControlToValidate="txtEmailAddress" ForeColor="Red"></asp:RequiredFieldValidator>
                                    &ensp;
                                    <asp:Label ID="LEmailExists" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left"></div>
                                <div class="col-10 text-left">
                                    <!--Validation-->
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtEmailAddress" runat="server" ErrorMessage="Sorry, The email address entered is not in the correct format. The standard email address format is name@example.com"
                                        ValidationExpression="(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|'(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*')@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])"
                                        ControlToValidate="txtEmailAddress" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left">
                                    <!--Get a contact number from the user-->
                                    <!-- Lable -->
                                    <asp:Label ID="LContactNo" runat="server" Text="Contact No.:"></asp:Label>
                                </div>
                                <div class="col-8">
                                    <!--input -->
                                    <asp:TextBox CssClass="form-control" ID="txtContactNumber" runat="server" placeholder="Contact Number"></asp:TextBox>
                                </div>
                                <div class="col-2">
                                    <!--Help-->
                                    <a runat="server" id="contactNoToolTip" href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="10-digit RSA Cellphone number used is contact you and keep you up to date">
                                        <span class="glyphicon">(Optional)</span>
                                    </a>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left"></div>
                                <div class="col-10">
                                    <!--Validation-->
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtContactNumber" runat="server" ErrorMessage="Sorry, please enter a valid 10-digit RSA cellphone number" ValidationExpression="^0[0-9]{9}$" ControlToValidate="txtContactNumber" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2 text-left">
                                    <!--Get the pasword from the user-->

                                    <!-- Lable -->
                                    <asp:Label ID="LPaswrod1" runat="server" Text="Password:"></asp:Label>
                                </div>
                                <div class="col-5">
                                    <!--input -->
                                    <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" placeholder="Password" TextMode="password"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    <!--input -->
                                    <asp:TextBox CssClass="form-control" ID="txtConfirmPassword" runat="server" placeholder="Confirm Password" TextMode="password"></asp:TextBox>
                                </div>
                                <div class="col-1">
                                    <!--Help-->
                                    <a runat="server" id="passToolTip" href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="Used to securely identify you and keep your data safe">
                                        <span class="glyphicon">&#63;</span>
                                    </a>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2"></div>
                                <div class="col-5">
                                    <!--Validation-->
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPassword" runat="server" ErrorMessage="*Password is Required" ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-5">

                                    <!--Validation (Done in code Behind-->
                                    &ensp;&ensp;&ensp;&ensp;<asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match" ForeColor="Red"></asp:CompareValidator>
                                    <asp:Label runat="server" ID="lConfirmPassword" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2"></div>
                                <div class="col-10">
                                    <!--Validation-->
                                    &ensp;&ensp;<asp:RegularExpressionValidator ID="RegularExpressionValidatortxtPassword" runat="server" ErrorMessage="Sorry, please use 8 or more characters with a mix of letters, numbers & symbols" ValidationExpression="^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$" ControlToValidate="txtPassword" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <!--Submition Button-->
                            <asp:Button class='btn btn-primary' ID="btnSubmitEmail" runat="server" Text="Submit" OnClick="btnSubmitEmail_Click" Font-Bold="true" Width="150" Height="50" />

                            <!--line break-->
                            <br />
                            <br />
                            <a href="../Authentication/Accounts.aspx?Type=Email">Sign in instead</a>

                            <!--err-->
                            <div class="container row" runat="server">
                                <asp:PlaceHolder ID="phAddEmpErr" runat="server" Visible="false">
                                    <div class="col-sm-12 col-md-12 alert alert-primary alert-dismissible">
                                        <asp:Label ID="lblAddEmpErr" runat="server" Text="Label"></asp:Label>
                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                    </div>
                                </asp:PlaceHolder>
                            </div>

                        </div>
                    </div>
                    <div class="col-xs-2 col-md-2 text-center"></div>
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

    <!--Old Bootstrap
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
-->
</body>
</html>
