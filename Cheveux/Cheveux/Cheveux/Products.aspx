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
                            <h1>Products</h1>
                        </header>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="productJumbotronLable" runat="server"></asp:Label>
                        <!-- List Product Table -->
                        <asp:Table ID="tblProductTable" runat="server">
                        </asp:Table>
                    </div>

            </div>
         </div>
       <!--View specific product-->
        <div runat="server" id="DisplayProduct">
                <div class="row">
                    <div class="col-sm-auto col-md-auto col-lg-auto">
                        <asp:PlaceHolder ID="prProducts" runat="server">
                            <asp:Table ID="tblProducts" runat="server"></asp:Table>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </div>
            





            <div runat="server" id="addandedit">
                <form id="formAddEditProduct" runat="server">
                    <!-- ADD EDIT PRODUCTS FORM --> 





                </form>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
        </div>
</asp:Content>
