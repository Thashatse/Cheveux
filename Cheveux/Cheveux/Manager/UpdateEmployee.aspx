<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="UpdateEmployee.aspx.cs" Inherits="Cheveux.Manager.UpdateEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Update Employee - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />

        <asp:PlaceHolder ID="phLogIn" runat="server">
            <div class="container" runat="server" id="LoggedOut" visible="true">
                <div class="jumbotron bg-dark text-white">
                    <h1>Please Log-in to proceed</h1>
                    <button type="button" class="btn btn-primary">
                        <a href="../Authentication/Accounts.aspx?PreviousPage=UpdateEmployee.aspx" id="LogedOutButton">Login</a>
                    </button>
                </div>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder runat="server" ID="phMain">
            <div class="container">
                <div class="row">
                    <div class="col-md-2 col-sm-1"></div>
                    <div class="col-md-12 col-sm-10">
                        <div class="jumbotron bg-dark text-white">
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <header>
                                        <h1 id="updateEmpHeading">Update Employee Details
                                        </h1>
                                    </header>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-5">
                                    <asp:Table ID="tblUserImage" runat="server"></asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <!--Error updating user message-->
            <div class="container row" runat="server">
                <asp:PlaceHolder ID="phUpdateErr" runat="server" Visible="false">
                    <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                        <asp:Label ID="lblUpdateErr" runat="server" Text="Label"></asp:Label>
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    </div>
                </asp:PlaceHolder>
            </div>

            <div id="empDetails" class="container">
                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblType" runat="server" Text="Employee Type:"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-4 col-lg-4">
                        <asp:RadioButtonList ID="rdoType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="flow" OnSelectedIndexChanged="rdoType_Change">
                            <asp:ListItem Text="Receptionist&nbsp;" Value="R" />
                            <asp:ListItem Text="Stylist&nbsp;" Value="S" />
                        </asp:RadioButtonList>
                        <!-- RadioButtons Validation-->
                        <asp:RequiredFieldValidator ID="rdoValidator" runat="server" ErrorMessage="*Please select user type" ForeColor="Red" ControlToValidate="rdoType"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <asp:PlaceHolder ID="phStylist" runat="server" Visible="false">
                    <div class="row" runat="server">
                        <div class="col-sm-12 col-md-2 col-lg-2">
                            <asp:Label runat="server" ID="lblBio" Text="Bio"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-6 col-lg-6">
                            <textarea runat="server" id="txtBio" cols="45" rows="5" placeholder="Bio"></textarea>
                            <asp:RequiredFieldValidator ID="validateBio" runat="server" ErrorMessage="*Required" ForeColor="Red" ControlToValidate="txtBio"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-sm-12 col-md-2 col-lg-2">
                            <asp:Label runat="server" ID="lblSpec" Text="Specialisation"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-6 col-lg-6">
                            <asp:DropDownList ID="drpSpecs" runat="server" AutoPostBack="True"
                                CssClass="btn btn-primary dropdown-toggle">
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <br />

                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblAddLine1" runat="server" Text="Address Line 1"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtAddLine1" runat="server" placeholder="AddressLine 1"></asp:TextBox>
                        <!-- AddressLine 1 Validation-->
                        <asp:RequiredFieldValidator ID="ad1Validation" runat="server" ErrorMessage="*Required" ForeColor="Red" ControlToValidate="txtAddLine1"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblAddLine2" runat="server" Text="Address Line 2"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtAddLine2" runat="server" placeholder="AddressLine 2"></asp:TextBox>
                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblSuburb" runat="server" Text="Suburb"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtSuburb" runat="server" placeholder="Suburb"></asp:TextBox>
                        <!-- Suburb Validation-->
                        <asp:RequiredFieldValidator ID="suburbValidation" runat="server" ErrorMessage="*Required" ForeColor="Red" ControlToValidate="txtSuburb"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-4 col-lg-4">
                        <asp:TextBox ID="txtCity" runat="server" placeholder="City"></asp:TextBox>
                        <!-- City Validation-->
                        <asp:RequiredFieldValidator ID="cityValidation" runat="server" ErrorMessage="*Required" ForeColor="Red" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <button class="btn btn-primary">
                            <a href="../Manager/Employee.aspx" style="color: white; text-decoration: none;">Cancel
                            </a>
                        </button>
                    </div>
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Button ID="btnUpdate" class="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    </div>
                </div>
            </div>

        </asp:PlaceHolder>

        <!--Error: If cant display user -->
        <asp:PlaceHolder ID="phUsersErr" runat="server" Visible="false">
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
                                    <asp:Label ID="errorHeader" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="errorMessage" runat="server"></asp:Label>
                                    <asp:Label ID="errorToReport" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>

        <br />
        <br />
    </form>
</asp:Content>
