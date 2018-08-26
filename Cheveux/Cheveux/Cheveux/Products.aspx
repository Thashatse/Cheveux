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
                <asp:PlaceHolder runat="server" ID="phProducts" >
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
        <asp:PlaceHolder ID="phSpecProduct" runat="server">
        <div runat="server" id="DisplayProduct">
                <div class="row">
                    <div class="col-sm-auto col-md-auto col-lg-auto">
                        
                            <asp:Table ID="tblProducts" runat="server"></asp:Table>
                        
                    </div>
                </div>
            </div>
            </asp:PlaceHolder>





            <div runat="server" id="addandedit">
                <form id="formAddEditProduct" runat="server">
                    <!-- ADD EDIT PRODUCTS FORM --> 
                    <div>
                                    <asp:Image ID="Image2" runat="server" />
                        <b> <asp:Label Text="ProductType">Product Type</asp:Label></b>
                                    <td><td>
                                     <asp:DropDownList ID="drpListProductType" runat="server" AutoPostBack="true">
                                     <asp:ListItem Enabled="true" Text="Select Product Type" Value="-1"></asp:ListItem>
                                          <asp:ListItem Text="Accessory" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="Treatment" Value="2"></asp:ListItem>
                                       </asp:DropDownList>

                           </td>      </td>  </div>
                            <div>
                               <b>    <asp:Label ID="lblName" runat="server" Text="Name">Name</asp:Label> </b>  <td>  <td> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> </td>  </td>
                                </div>
                                    
                            <b>         <asp:Label ID="lblDesc" runat="server" Text="LblDescription">Product Description</asp:Label> </b>
                           <td>     <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>   </td>
                            <div>
                     <b>    <asp:Label ID="Label" runat="server" Text="Price">Price</asp:Label> </b>
                                    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                                </div>
                             <b>    <asp:Label ID="lblBrand" runat="server" Text="Brand">Brand</asp:Label> </b>

                                    <asp:DropDownList ID="drplistBrand" runat="server" AutopostBack="true" >
                                        <asp:ListItem Enabled="true" Text="Select Brand" Value="-1"></asp:ListItem>
                                          <asp:ListItem Text="ORS" Value="1"></asp:ListItem>
                                          <asp:ListItem Text="X-Pression Ultra" Value="2"></asp:ListItem>
                                         <asp:ListItem Text="Cantu" Value="3"></asp:ListItem>
                                          <asp:ListItem Text="Joedir Nature" Value="4"></asp:ListItem>
                                         <asp:ListItem Text="Afro Botanics" Value="5"></asp:ListItem>
                                          <asp:ListItem Text="Darling" Value="6"></asp:ListItem>
                                          <asp:ListItem Text="Shea Moisture" Value="7"></asp:ListItem>
                                          <asp:ListItem Text="Dark and Lovely" Value="8"></asp:ListItem>
                                         <asp:ListItem Text="Frika" Value="9"></asp:ListItem>
                                          <asp:ListItem Text="Inecto" Value="10"></asp:ListItem>
                                         <asp:ListItem Text="Aunt Jackie's" Value="11"></asp:ListItem>
                                          <asp:ListItem Text="Long & Lasting" Value="12"></asp:ListItem>
                                         <asp:ListItem Text="Head & Shoulders" Value="13"></asp:ListItem>
                                          <asp:ListItem Text="Chocolate Hair" Value="14"></asp:ListItem>
                                         <asp:ListItem Text="Vicher human Hair" Value="15"></asp:ListItem>
                                          <asp:ListItem Text="Joedir Magic" Value="16"></asp:ListItem>
                                         <asp:ListItem Text="New Crown" Value="17"></asp:ListItem>
                                          <asp:ListItem Text="Hair Decoration" Value="18"></asp:ListItem>
                                         <asp:ListItem Text="Isoplus" Value="19"></asp:ListItem>
                                          <asp:ListItem Text="Donna" Value="20"></asp:ListItem>
                                         <asp:ListItem Text="Nikki" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="Vicher" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="Tropic Isle Living" Value="23"></asp:ListItem>
                                        </asp:DropDownList>
                            <div>
                                <b>  <asp:Label ID="lblSupplier" runat="server" Text="Supplier">Supplier</asp:Label> </b>
                                <td>   <asp:DropDownList ID="DrpSupplier" runat ="server" AutoPostBack =" true"> 
                                    <asp:ListItem Enabled="true" Text="Select Supplier" Value="-1"></asp:ListItem>
                                     <asp:ListItem Text="Hair Supplies International" Value="1"></asp:ListItem>
                                </asp:DropDownList>

                                </div>
                                   
                                    
                              <b>    <asp:Label ID="LblColour" runat="server" Text="Colour">Colour</asp:Label> </b>
                                    <asp:TextBox ID="txtColour" runat="server"></asp:TextBox>
                        </div>
            <div>
                
            </div>
            <asp:Image ID="Image1" runat="server" />
            <div><div></div>
             
           <b>   <asp:Button ID="Button1" runat="server" Text="Add "/></b>  <td><b> <asp:Button ID="Button2" runat="server" Text="Update" /></b> </td>
                </div>
            </div>

   </form>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
        </div>
</asp:Content>
