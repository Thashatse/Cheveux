<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Cheveux.Manager.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Manage Products - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <!-- if the user is loged In -->
            <div runat="server" id="LogedIn" visible="false">


                <!-- Jumbo Tron -->
                <div class="jumbotron bg-dark text-white">

                    <h1>Manage Products</h1>
                    <!-- line Break -->
                    <br />
                    <form runat="server">

                        <!-- View By Selector -->
                        <p>View Products By: </p>
                        <asp:DropDownList ID="drpProductType" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                        <!-- line Break -->
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-12">
                        <!-- View By search tearm -->
                        <p>View Products By Search Term: </p>
                                </div>
                            </div>
                        <div class="row">
                            <div class="col-5">
                                <asp:TextBox class="form-control" ID="txtProductSearchTerm" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-2">
                                <asp:Button CssClass="btn btn-primary" ID="btnProductSearch" runat="server" Text="Search" />
                            </div>
                        </div>
                    </form>
                    <!-- line Break -->
                    <br />
                    <!--Help-->
                    <a href="../Help/CheveuxHelpCenter.aspx#ManageProducts" target="_blank" title="How To Manage Products">
                        <span class="glyphicon">&#63; Help</span></a>

                </div>



                <div class="row">
                    <div class="col-md-10">
                        <asp:Label ID="productJumbotronLable" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <!--add product btn -->
                        <button type='button' class='btn btn-primary'><a class='btn btn-primary' href='#'>New Product </a></button>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <!-- List Product Table -->
                        <asp:Table ID="tblProductTable" runat="server">
                        </asp:Table>
                    </div>
                </div>
            </div>

            <!-- if the user is loged Out -->
            <div runat="server" id="LogedOut">
                <div class="bg-dark text-white jumbotron">
                    <h1>Manage Products</h1>
                    <p>Please log-in to Manage Products</p>
                    <button type="button" class="btn btn-default">
                        <a href="../Authentication/Accounts.aspx?PreviousPage=Products.aspx" id="LogedOutButton">Login</a>
                    </button>
                </div>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
