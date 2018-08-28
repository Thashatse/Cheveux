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

             <div class="row">
             <div class="col-md-2 col-sm-1"> </div>
             <div class="col-md-8 col-sm-10">
             <div runat="server" id="addandedit" visible="true">
                <form id="formAddEditProduct" runat="server">
                
                <!-- ADD EDIT PRODUCTS FORM -->
                <div class="jumbotron bg-dark text-white">
                <br />
                <div class="row">
                <div class="col-12">
                <header>
                <h1>Add Product </h1>
                </header>
                <br />
                  <asp:Table runat="server" ID="tblProductDetails">
                         <asp:TableRow>
                          <asp:TableCell Text="Product Type:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                            <asp:TableCell >
                                     <asp:DropDownList runat="server" ID="drpProductType" AutoPostBack="true">
                                     </asp:DropDownList>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                 <asp:TableRow>
                                        <asp:TableCell Text="Name: " Width="150px"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                                            <br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell Text="Product Description: " Width="150px"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtProductDescription"></asp:TextBox>
                                            <br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     <asp:TableRow>
                                        <asp:TableCell Text="Price: " Width="150px"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox>
                                            <br />
                                     <asp:TableRow>
                                    <asp:TableCell Text="Brand:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                            <asp:TableCell >
                                     <asp:DropDownList runat="server" ID="drpBrandList" AutoPostBack="true">
                                     </asp:DropDownList>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Text="Supplier:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                            <asp:TableCell >
                                     <asp:DropDownList runat="server" ID="drpListSupplier" AutoPostBack="true">
                                     </asp:DropDownList>
                                            <br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                     
                                   

                                   </asp:Table>

                              </form>
            </div>
    
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
