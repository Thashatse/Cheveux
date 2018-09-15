<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="Cheveux.Manager.AddEmpolyee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Add Employee - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-1"></div>
        <div class="col-10">
            <form runat="server">

                <asp:PlaceHolder ID="phLogIn" runat="server">
                    <div class="container" runat="server" id="LoggedOut" visible="true">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Please Log-in to proceed</h1>
                            <button type="button" class="btn btn-default">
                                <a href="../Authentication/Accounts.aspx?PreviousPage=AddEmployee.aspx" id="LogedOutButton">Login</a>
                            </button>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder ID="phMain" runat="server">
                    <!-- Jumbotron -->
                    <div class="jumbotron bg-dark text-white">
                        <br />
                        <div id="search">
                            <div class="row">
                                <div class="col-12">
                                    <header>
                                        <h1>Add Employee</h1>
                                    </header>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--- List of Users --->
                    <div class="row">
                        <div class="col-md-12">
                            <asp:PlaceHolder ID="phUsers" runat="server" Visible="true">
                                <asp:Table ID="tblUsers" runat="server">
                                </asp:Table>
                                <asp:Label ID="valLabel" runat="server" Text="Label" Visible="false"></asp:Label>
                            </asp:PlaceHolder>
                        </div>
                    </div>

                    <!--Error adding user message-->
                    <div class="container row" runat="server">
                        <asp:PlaceHolder ID="phAddErr" runat="server" Visible="false">
                            <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                                <asp:Label ID="lblAddErr" runat="server" Text="Label"></asp:Label>
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            </div>
                        </asp:PlaceHolder>
                    </div>

                    <!--Error: If cant display list of users-->
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                </asp:PlaceHolder>
            </form>
        </div>
        <div class="col-1"></div>
    </div>
</asp:Content>
