<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAccount.aspx.cs" Inherits="Cheveux.NewAccount"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - Cheveux</title>
    <!--Bootstrap-->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <!--CSS-->

</head>
<body>
    <!--form for regestering a user-->
    <form id="Register" runat="server">
        <div>
            <div class="container-fluid">
			<div class="container">
			<div class="row">
			<div class="col-md-12">
                <div class="jumbotron">
					<div class="col-xs-2 col-md-2 text-center"></div>
                    <div class="col-xs-8 col-md-8">
						<!--Logo-->
						<img src="../IMG_0715.png" alt="logo" width="100" height="100" />
						<h1>Register</h1>
						<asp:Label ID="almostThere" runat="server" Text="Label"></asp:Label>
						<!--line break-->
                            <br />
                            <br />
                            <!--Get a username from the user-->
                            <asp:Label ID="Label3" runat="server" Text="User Name:"></asp:Label>
                            <asp:TextBox ID="userName" runat="server" placeholder="Placecholder"></asp:TextBox>
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="This is used to identify you on the Cheveux platform">
                                <span class="glyphicon">&#63;</span>
                            </a>
                            <!--userName Validation-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsrname" runat="server" ErrorMessage="*Required" ControlToValidate="userName" ForeColor="Red"></asp:RequiredFieldValidator>
                            <!--line break-->
                            <br />
                            <br />
                            <!--Get a contact number from the user-->
                            <asp:Label ID="Label4" runat="server" Text="Contact Number:"></asp:Label>
                            <asp:TextBox ID="contactNumber" runat="server" placeholder="041 243 8389"></asp:TextBox>
                            <!--Help-->
                            <a href="../Help/CheveuxHelpCenter.aspx#UserAccounts" target="_blank" title="10-digit RSA Cellphone number">
                                <span class="glyphicon">&#63;</span>
                            </a>
                            <!--contactNumber Validation-->
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorContactNumber" runat="server" ErrorMessage="*Required" ControlToValidate="contactNumber" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="contactNumber" ErrorMessage="Please enter a valid phone number" ForeColor="Red" ValidationExpression="^0[0-9]{9}$"></asp:RegularExpressionValidator>
                            <!--line break-->
                            <br />
                            <br />
							<!--Submition Button-->
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Font-Bold="true" width="150" height="50" />
                    </div>
					<div class="col-xs-2 col-md-2 text-center"></div>
				</div>
            </div>
			</div>
			</div>
            </div>
        </div>
    </form>
    <!--Bootstrap-->
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
</body>
</html>
