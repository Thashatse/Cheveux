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
    </div>
    <br />

    <div class="row">
        <div class="col-1"></div>
        <div class="col-10">
            <form runat="server">
                <!-- if the user is loged In -->
                <div runat="server" id="LogedIn" visible="false">

                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div id="divTabs" runat="server">
                                <!--Tabs-->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li>
                                        <asp:Button ID="btnViewAllProducts" runat="server" Text="Products" class="btn btn-light" OnClick="btnViewAllProducts_Click" /></li>
                                    <li>
                                        <asp:Button ID="btnViewOutstandingOrders" runat="server" Text="Purchase Orders" class="btn btn-light" OnClick="btnViewOutstandingOrders_Click" /></li>
                                    <li>
                                        <asp:Button ID="btnViewBrands" runat="server" Text="Brands" class="btn btn-light" OnClick="btnViewBrands_Click" /></li>
                                    <li>
                                        <asp:Button ID="btnViewSuppliers" runat="server" Text="Suppliers" class="btn btn-light" OnClick="btnViewSuppliers_Click" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- Line Break -->
                    <br />

                    <div runat="server" visible="false" id="ViewAllProducts">
                        <!-- Jumbo Tron -->
                        <div class="jumbotron bg-dark text-white">
                            <div class="row">
                                <div class="col-lg-9 col-md-12 col-sm-12">
                                    <asp:Label ID="lblViewAllProductsHeading" runat="server" Text="<h1>Manage Products</h1>"></asp:Label>
                                </div>
                                <div class="col-lg-3 col-md-2 col-sm-2">
                                    <asp:Button ID="btnViewFillterAllProducts" runat="server" Text="Fillter" class="btn btn-light" OnClick="btnViewFillterAllProducts_Click" />
                                </div>
                            </div>

                            <div runat="server" id="divAllProductsFilter" visible="false">
                                <!-- line Break -->
                                <br />
                                <!-- View By Selector -->
                                <p>Product Type: </p>
                                <asp:DropDownList ID="drpProductType" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="ddlAllProdsSuppliers_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <!-- line Break -->
                                <br />
                                <br />
                                <p>Product Supplier: </p>
                                <asp:DropDownList ID="ddlAllProdsSuppliers" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="ddlAllProdsSuppliers_SelectedIndexChanged">
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
                                        <asp:TextBox class="form-control" ID="txtProductSearchTerm" runat="server" AutoPostBack="true"
                                            OnTextChanged="ddlAllProdsSuppliers_SelectedIndexChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button CssClass="btn btn-primary" ID="btnProductSearch" runat="server" Text="Search" OnClick="ddlAllProdsSuppliers_SelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-md-10">
                                <asp:Label ID="productJumbotronLable" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <!--add product btn -->
                                <a class='btn btn-primary' href='../Cheveux/Products.aspx?Action=Add'>New Product </a>

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

                    <div runat="server" visible="false" id="NewOrder">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Create Purchase Order</h1>
                            <p style="color: red;" runat="server" id="NoProductSelectedOnOrder" visible="false">Select Product First </p>
                        </div>

                        <h3>Supplier: </h3>
                        <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                        </asp:DropDownList>

                        <!-- Line Break -->
                        <br />
                        <br />

                        <div class="row">

                            <div class="col-md-12 col-lg-5">
                                <!--Line Break-->
                                <br />
                                <h3 style="text-align: left; float: left;">Products: </h3>
                                &#09;
                                    <asp:DropDownList ID="ddlOrdersProductType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdersProductType_SelectedIndexChanged" CssClass="btn btn-Secondary dropdown-toggle">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                <p style="text-align: right; float: right;">
                                    <!-- Search -->
                                    <asp:TextBox ID="txtProductSearch" runat="server" AutoPostBack="true" placeholder="search" CssClass="form-control" OnTextChanged="ddlOrdersProductType_SelectedIndexChanged"></asp:TextBox>
                                </p>
                                <!--Line Break-->
                                <br />
                                <asp:ListBox runat="server" ID="lbProducts" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="300"></asp:ListBox>
                            </div>

                            <div class="col-md-12 col-lg-2 text-center">
                                <!-- Line Break -->
                                <br />
                                <br />
                                <br />
                                <br />

                                Qty:
                                <asp:DropDownList runat="server" ID="Qty" CssClass="btn btn-outline-secondary dropdown-toggle">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                    <asp:ListItem Value="32">32</asp:ListItem>
                                    <asp:ListItem Value="33">33</asp:ListItem>
                                    <asp:ListItem Value="34">34</asp:ListItem>
                                    <asp:ListItem Value="35">35</asp:ListItem>
                                    <asp:ListItem Value="36">36</asp:ListItem>
                                    <asp:ListItem Value="37">37</asp:ListItem>
                                    <asp:ListItem Value="38">38</asp:ListItem>
                                    <asp:ListItem Value="39">39</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="41">41</asp:ListItem>
                                    <asp:ListItem Value="42">42</asp:ListItem>
                                    <asp:ListItem Value="43">43</asp:ListItem>
                                    <asp:ListItem Value="44">44</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                    <asp:ListItem Value="46">46</asp:ListItem>
                                    <asp:ListItem Value="47">47</asp:ListItem>
                                    <asp:ListItem Value="48">48</asp:ListItem>
                                    <asp:ListItem Value="49">49</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList>

                                <!-- Line Break -->
                                <br />
                                <br />

                                <asp:Button ID="btnAddProductToOrder" runat="server" Text="Add Product" CssClass="btn btn-Secondary" OnClick="btnAddProductToOrder_Click" />

                                <!-- Line Break -->
                                <br />
                                <br />
                                <br />

                                <asp:Button ID="btnRemoveProductFromOrder" runat="server" Text="Remove Product" CssClass="btn" OnClick="btnRemoveProductFromOrder_Click" />
                            </div>


                            <div class="col-md-12 col-lg-5">
                                <!--Line Break-->
                                <br />
                                <!-- Order -->
                                <h3>Order: </h3>
                                <!--Line Break-->
                                <br />
                                <asp:ListBox runat="server" ID="lProductsOnOrder" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="300"></asp:ListBox>
                            </div>

                        </div>

                        <!-- Line Break -->
                        <br />
                        <asp:Button Style="float: left;" ID="btnNewProd" runat="server" OnClick="btnNewProd_Click" Text="Craete New Product" CssClass="btn btn-Secondary" />
                        <asp:Button Style="float: right;" ID="btnSaveOrder" runat="server" Text="Submit Purchase Order" CssClass="btn btn-primary" OnClick="btnSaveOrder_Click" />
                    </div>

                    <div runat="server" visible="false" id="OutstandingOrders">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Accept Purchase Order</h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label runat="server" ID="outstandingOrdersLable"></asp:Label>
                            </div>
                            <div class="col-md-6 text-center">
                                <div id="div2" runat="server">
                                    <!--Tabs-->
                                    <ul class="nav" role="tablist">
                                        <li>
                                            <asp:Button ID="btnOut1" runat="server" Text="Outstanding" class="btn btn-secondary" OnClick="btnViewOutstandingOrders_Click" /></li>
                                        <li>
                                            <asp:Button ID="btnPast1" runat="server" Text="Past" class="btn btn-light" OnClick="btnViewPastOrders_Click" /></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-primary js-scroll-trigger" href="?Action=NewOrder">Create Purchase Order</a>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <br />
                                <asp:Table ID="tblOutstandingOrders" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="PastOrders">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Past Purchase Orders</h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label runat="server" ID="lblPastOrder"></asp:Label>
                            </div>
                            <div class="col-md-6 text-center">
                                <div id="div3" runat="server">
                                    <!--Tabs-->
                                    <ul class="nav" role="tablist">
                                        <li>
                                            <asp:Button ID="btnout2" runat="server" Text="Outstanding" class="btn btn-light" OnClick="btnViewOutstandingOrders_Click" /></li>
                                        <li>
                                            <asp:Button ID="btnpast2" runat="server" Text="Past" class="btn btn-secondary" OnClick="btnViewPastOrders_Click" /></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-primary js-scroll-trigger" href="?Action=NewOrder">Create Purchase Order</a>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <br />
                                <asp:Table ID="tblPastOrders" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="Suppliers">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Suppliers</h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="lblSuppliers"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <!--add product btn -->
                                <a class='btn btn-primary' href='?Action=NewSupp'>New Supplier </a>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <asp:Table ID="tblSuppliers" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="Brands">
                        <div class="jumbotron bg-dark text-white">
                            <h1>Brands</h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-md-10">
                                <asp:Label runat="server" ID="lblBrands"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <!--add product btn -->
                                <a class='btn btn-primary' href='?Action=NewBrand'>New Brand </a>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <asp:Table ID="tblBrand" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="divViewOrder">
                        <div class="jumbotron bg-dark text-white">
                            <h1>
                                <asp:Label runat="server" ID="lblOrder" Text="Product Order"></asp:Label></h1>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <asp:Table ID="tblViewOrder" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="divviewSupplier">
                        <div class="jumbotron bg-dark text-white">
                            <h1>
                                <asp:Label runat="server" ID="lblSupplier" Text="Supplier"></asp:Label></h1>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Line Break -->
                                <br />
                                <asp:Table ID="tblSupplier" runat="server"></asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="divAddBrand">
                        <div class="jumbotron bg-dark text-white">
                            <h1>
                                <asp:Label runat="server" ID="lblNewBrandHeader" Text="New Brand"></asp:Label></h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Name:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtBrandName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtBrandName" runat="server"
                                    ErrorMessage="*Name is required" ControlToValidate="txtBrandName"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Type:
                            </div>
                            <div class="col-6">
                                <asp:DropDownList ID="ddlAddBrandProductType" runat="server" CssClass="btn btn-primary"></asp:DropDownList>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:Button Style="float: right;" ID="btnAddBrand" runat="server" Text="Create Brand" CssClass="btn btn-primary" OnClick="btnAddBrand_Click" />
                            </div>
                            <div class="col-4"></div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="divAddSupplier">
                        <div class="jumbotron bg-dark text-white">
                            <h1>
                                <asp:Label runat="server" ID="lblNewSuppHeader" Text="New Supplier"></asp:Label></h1>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Supplier Name:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtSupName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSupName" runat="server"
                                    ErrorMessage="*Name is required" ControlToValidate="txtSupName"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Contact Name:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="TxtSupContactName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTxtSupContactName" runat="server"
                                    ErrorMessage="*Contact Name is required" ControlToValidate="TxtSupContactName"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Contact Number:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtSupContactNum" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-4">
                                <a href="#" target="_blank" title="10-digit RSA phone number">
                                    <span class="glyphicon">&#63;</span>
                                </a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSupContactNum" runat="server"
                                    ErrorMessage="*Contact Number is required" ControlToValidate="txtSupContactNum"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtSupContactNum" runat="server" ControlToValidate="txtSupContactNum" ErrorMessage="Please enter a valid phone number" ForeColor="Red" ValidationExpression="^0[0-9]{9}$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Contact Email: 
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtSupContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtSupContactEmail" runat="server"
                                    ErrorMessage="*Contact Email is required" ControlToValidate="txtSupContactEmail"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatortxtSupContactEmail" runat="server" ErrorMessage="Sorry, The email address entered is not in the correct format. The standard email address format is name@example.com"
                                    ValidationExpression="(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|'(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*')@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])"
                                    ControlToValidate="txtSupContactEmail" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                Address:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtAddLine1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddLine1" runat="server"
                                    ErrorMessage="*Address Line One is required" ControlToValidate="txtAddLine1"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:TextBox ID="txtAddLine2" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                Suburb:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtAddLineSuburb" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2">
                                City:
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtAddLineCity" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAddLineCity" runat="server"
                                    ErrorMessage="*City is required" ControlToValidate="txtAddLineCity"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <!-- Line Break -->
                        <br />
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-6">
                                <asp:Button Style="float: right;" ID="btnAddSupp" runat="server" Text="Create Supplier" CssClass="btn btn-primary" OnClick="btnAddSupp_Click" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" visible="false" id="divAcceptOrder">
                        Accept Order
                    </div>
                    </div>

                    <!-- if the user is loged Out -->
                    <div runat="server" id="LogedOut">
                        <div class="bg-dark text-white jumbotron">
                            <div class="row">
                                <div class="col-10">
                                    <h1>Manage Products</h1>
                                    <p>Please log-in to Manage Products</p>
                                </div>
                                <div class="col-2">
                                    <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Products.aspx" id="LogedOutButton">Login</a>
                                </div>
                            </div>
                        </div>
                    </div>
            </form>
        </div>
        <div class="col-1"></div>
    </div>

    <!-- Line Break -->
    <br />
    <br />
</asp:Content>
