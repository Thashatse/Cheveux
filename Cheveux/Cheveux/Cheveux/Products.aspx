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
        <div class="col-1"></div>
        <div class="col-10">

            <div runat="server" id="divViewAll">
                <div class="jumbotron bg-dark text-white">
                    <!-- VIEW ALL PRODUCTS TABLE -->
                    <header class="text-left">
                        <h1 runat="server" id="lblHeader">Add Products</h1>
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

            <div class="row">
                <div class="col-md-8 col-sm-10">
                    <div runat="server" id="addandedit" visible="false">
                        <form id="formAddEditProduct" runat="server">
                            <!-- ADD EDIT PRODUCTS FORM -->
                            <div class="row">
                                <div class="col-12">
                                    <asp:Table runat="server" ID="tblProductDetails" CssClass="pull-left">
                                        <asp:TableRow>
                                            <asp:TableCell Text="Supplier:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                            <asp:TableCell>
                                             <asp:DropDownList runat="server" ID="drpListSupplier" CssClass="btn btn-primary dropdown-toggle" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Product Type:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList runat="server" ID="drpProductType" CssClass="btn btn-primary dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="drpProductType_Change">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Name: " Width="150px"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Product Description: " Width="150px"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtProductDescription"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Price: " Width="150px"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Brand:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList runat="server" CssClass="btn btn-primary dropdown-toggle" ID="drpBrandList" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="150px" runat="server" ID="productLabel" Text="Colour"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="productTextBox"></asp:TextBox>
                                            </asp:TableCell>

                                        </asp:TableRow>
                                    </asp:Table>


                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-4">
                                    <asp:Button ID="btnCancel" CssClass="btn btn-primary" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <asp:Button ID="btnAddProduct" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAddProduct_Click" />
                                </div>
                            </div>
                        </form>
                    </div>

                    <div class="col-1"></div>
                </div>
                </div>

            <!---Upate Product div-->

            <!-- at the top of this page change the text displayed in the jumbotron based on the action-->

            <!--start of update div-->
            <!-- use bootstrap not a table-->

            <!--have a label and its corresponding textbox/dropdown in a single row -->
            <!-- for lables make the the div col-2 and for textboxes/dropdowns col-6 -->

            <!-- at the bottom add cancel and update button-->
            <!-- cancel button on the left-->
            <!--update button on the right-->

            <!-- end of update div -->

</asp:Content>
