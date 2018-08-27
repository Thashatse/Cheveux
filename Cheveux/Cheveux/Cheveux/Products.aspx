<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Cheveux.Cheveux.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Products - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <!-- Top Margin -->
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">

            <div runat="server" id="divViewAll">
                <div class="jumbotron bg-dark text-white">
                    <!-- VIEW ALL PRODUCTS TABLE -->
                    <header class="text-center">
                        <h1 runat="server" id="lblHeader">Products</h1>
                    </header>
                </div>
                <asp:PlaceHolder runat="server" ID="phProducts">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="productJumbotronLable" runat="server">   </asp:Label>

                            <!-- List Product Table -->
                            <asp:Table ID="tblProductTable" runat="server">
                            </asp:Table>
                        </div>

                    </div>
                </asp:PlaceHolder>
            </div>

            <!--View specific product-->
            <div runat="server" id="DisplayProduct" visible="false">
                <asp:PlaceHolder ID="phSpecProduct" runat="server">

                    <div class="row">
                        <div class="col-sm-auto col-md-auto col-lg-auto">

                            <asp:Table ID="tblProducts" runat="server"></asp:Table>

                        </div>
                    </div>

                </asp:PlaceHolder>
            </div>

            <div runat="server" id="addandedit" visible="true">
                <form id="formAddEditProduct" runat="server">
                    <!-- ADD EDIT PRODUCTS FORM -->
                    <asp:Label runat="server" Text="ProductType">Product Type</asp:Label>
                    <asp:DropDownList ID="drpListProductType" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblName" runat="server" Text="Name">Name</asp:Label>
                    <asp:TextBox ID="TxtName" runat="server"></asp:TextBox>
                    <asp:Label ID="lblDesc" runat="server" Text="LblDescription">Product Description</asp:Label>
                    <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
                    <asp:Label ID="Label" runat="server" Text="Price">Price</asp:Label>
                    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                    <asp:Label ID="lblBrand" runat="server" Text="Brand">Brand</asp:Label>
                    <asp:DropDownList ID="drplistBrand" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblSupplier" runat="server" Text="Supplier">Supplier</asp:Label>
                    <asp:DropDownList ID="DrpSupplier" runat="server" AutoPostBack=" true">
                    </asp:DropDownList>
                    <asp:Label ID="LblColour" runat="server" Text="Colour">Colour</asp:Label>
                    <asp:TextBox ID="txtColour" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="Add " OnClick="Button1_Click"\/>
                </form>
            </div>
    </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
