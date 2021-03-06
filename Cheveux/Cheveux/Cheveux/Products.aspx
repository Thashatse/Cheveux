﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Cheveux.Cheveux.Products" %>

<asp:content id="Content1" contentplaceholderid="PageTitle" runat="server">
    Products - Cheveux
</asp:content>
<asp:content id="Content2" contentplaceholderid="head" runat="server">
    <script type="text/javascript" src=" http://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
    <script type="text/javascript" src=" http://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <form runat="server">
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
                <div class="jumbotron bg-dark text-white">
                    <!-- VIEW ALL PRODUCTS TABLE -->
                    <header class="text-left">
                        <h1><asp:Label runat="server" Text="Add Products" id="lblHeadera"></asp:Label></h1>
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

            <!--View specific product-->
            <div runat="server" id="DisplayProduct" visible="false">
                <asp:PlaceHolder ID="phSpecProduct" runat="server">

                    <div class="row">
                                    <div class="col-lg-6 col-md-12">
                            <asp:Table ID="tblProducts" runat="server"></asp:Table>
                            
                            <div class="row">
                                <div class="row">
                                    <div class="col-5"></div>
                                    <div class="col-6">
                            <br />
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            <br />
                            <asp:Label runat="server" id="EditProductBtn" style="float: right"></asp:Label>
                                    </div>
                                    </div>
                            </div>
                                        </div>
                                    <div class="col-lg-6 col-md-12">
                            <asp:PlaceHolder runat="server" ID="phProductImage"></asp:PlaceHolder>
                        </div>
                        </div>
                </asp:PlaceHolder>
            </div>
           
                    <div runat="server" id="addandedit" visible="false">
                            <!-- ADD EDIT PRODUCTS FORM -->
                            <div class="row">
                                <div class="col-12">
                                    <asp:Table runat="server" ID="tblProductDetails" CssClass="pull-left">
                                        <asp:TableRow>
                                            <asp:TableCell Text="Supplier:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                            <asp:TableCell>
                                             <asp:DropDownList runat="server" ID="drpListSupplier" CssClass="btn btn-primary dropdown-toggle" AutoPostBack="true">
                                                 
                                                </asp:DropDownList>
                                                <asp:Label runat="server" id="lbleditProdSupp"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Product Type:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList runat="server" ID="drpProductType" CssClass="btn btn-primary dropdown-toggle" AutoPostBack="true" OnSelectedIndexChanged="drpProductType_Change">
                                                 
                                                </asp:DropDownList>
                                                <asp:Label runat="server" id="lbleditProdType"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Name: " Width="150px"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        
                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell><asp:RequiredFieldValidator ID="RequiredFieldValidatortxtName" runat="server"
                                            ErrorMessage="*Name is required" ControlToValidate="txtName"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell Text="Product Description: " Width="150px"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtProductDescription"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell><asp:RequiredFieldValidator ID="RequiredFieldValidatortxtProductDescription" runat="server"
                                            ErrorMessage="*Description is required" ControlToValidate="txtProductDescription"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell Text="Price: " Width="150px"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell><asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPrice" runat="server"
                                            ErrorMessage="*Price is required" ControlToValidate="txtPrice"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell Text="Brand:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList runat="server" CssClass="btn btn-primary dropdown-toggle" ID="drpBrandList" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:Label runat="server" id="lblEditProdBrand"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Width="150px" runat="server" ID="productLabel" Text="Colour"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtcolour"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblEditProdCol"></asp:Label>
                                            </asp:TableCell>
</asp:TableRow>

                                            <asp:TableRow>
                                            <asp:TableCell></asp:TableCell>
                                            <asp:TableCell><asp:RequiredFieldValidator ID="RequiredFieldValidatortxtcolour" runat="server"
                                            ErrorMessage="*Required" ControlToValidate="txtcolour"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        
                                <asp:TableRow>
                                      <asp:TableCell Text="Image: " Width="150px"></asp:TableCell>
                                      <asp:TableCell>
                                                              <asp:FileUpload ID="flUploadServiceimg" runat="server"/><br />
                                      </asp:TableCell>
                                </asp:TableRow>
                                    </asp:Table>


                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-4">
                                    <asp:Button ID="btnCancel" CssClass="btn btn-secondary" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                                <div class="col-sm-12 col-md-4">
                                    <asp:Button ID="btnAddProduct" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAddProduct_Click" />
                                </div>
                            </div>
                    </div>

                    <div class="col-1"></div>
            </div>
        </div>
    </form>
</asp:content>
