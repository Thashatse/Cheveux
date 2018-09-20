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
                <asp:placeholder runat="server" id="phProducts">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="productJumbotronLable" runat="server">   </asp:Label>

                            <!-- List Product Table -->
                            <asp:Table ID="tblProductTable" runat="server">
                            </asp:Table>
                        </div>

                    </div>
                </asp:placeholder>
            </div>

            <!--View specific product-->
            <div runat="server" id="DisplayProduct" visible="false">
                <asp:placeholder id="phSpecProduct" runat="server">

                    <div class="row">
                        <div class="col-sm-auto col-md-auto col-lg-auto">

                            <asp:Table ID="tblProducts" runat="server"></asp:Table>

                        </div>
                    </div>

                </asp:placeholder>
            </div>

            <div class="row">
                <div class="col-md-2 col-sm-1"></div>
                <div class="col-md-8 col-sm-10">
                    <div runat="server" id="addandedit" visible="true">
                        <form id="formAddEditProduct" runat="server">

                            <!-- ADD EDIT PRODUCTS FORM -->
                            <div class="jumbotron bg-dark text-white">
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                       
                                        <br />
                                        <asp:table runat="server" id="tblProductDetails">
                                            <asp:TableRow>
                                                <asp:TableCell Text="Product Type:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList runat="server" ID="drpProductType" AutoPostBack="true" OnSelectedIndexChanged="drpProductType_Change">
                                                      
                                                        <asp:ListItem Text ="Application service" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text ="Treatment" Value ="1"></asp:ListItem>
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
                                             <asp:TableCell Width="150px" runat="server" ID="QtyLabel" Text="Qty"></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox runat="server" ID="txtQty"></asp:TextBox>
                                                </asp:TableCell>
                                                 </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Text="Brand:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList runat="server" ID="drpBrandList" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Text="Supplier:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:DropDownList runat="server" ID="drpListSupplier" AutoPostBack="true">
                                                    <asp:ListItem Text ="Hair Supplies International" Value ="0"></asp:ListItem>
                                                    <asp:ListItem Text ="Hair Supplies International" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell Width="150px" runat="server" ID="productLabel" Text="Colour"></asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox runat="server" ID="productTextBox"></asp:TextBox>
                                                </asp:TableCell>
                                               
                                            </asp:TableRow>  
                                        </asp:table>
                                        <asp:button id="btnAddProduct" runat="server" text="Add" onclick="btnAddProduct_Click" /><td></td>
                                        <asp:button id="btnCancel" runat="server" text="Cancel" OnClick="btnCancel_Click" />
                        </form>
                    </div>

                    <div class="col-1"></div>
                </div>
</asp:Content>
