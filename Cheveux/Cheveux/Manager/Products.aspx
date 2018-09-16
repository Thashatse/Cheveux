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
                                    <li><asp:Button ID="btnViewAllProducts" runat="server" Text="Manage Products" class="btn btn-light" OnClick="btnViewAllProducts_Click" /></li>
                                    <li><asp:Button ID="btnViewNewOrder" runat="server" Text="New Order" class="btn btn-light" OnClick="btnViewNewOrder_Click" /></li>
                                    <li><asp:Button ID="btnViewOutstandingOrders" runat="server" Text="Accept Order" class="btn btn-light" OnClick="btnViewOutstandingOrders_Click" /></li>
                                    <li><asp:Button ID="btnViewPastOrders" runat="server" Text="Past Orders" class="btn btn-light" OnClick="btnViewPastOrders_Click" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- Line Break -->
                    <br />

                        <div runat="server" visible="false" id="ViewAllProducts">

                            <!-- Jumbo Tron -->
                            <div class="jumbotron bg-dark text-white">

                                <h1>Manage Products</h1>
                                <!-- line Break -->
                                <br />


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
                                        <asp:TextBox class="form-control" ID="txtProductSearchTerm" runat="server" AutoPostBack="true"
                                            OnTextChanged="Page_Load"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button CssClass="btn btn-primary" ID="btnProductSearch" runat="server" Text="Search" />
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
                                <h1>New Order</h1>
                            </div>

                            <h3>Supplier: </h3>
                            <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" CssClass="btn btn-primary dropdown-toggle">
                            </asp:DropDownList>

                            <!-- Line Break -->
                            <br />
                            <br />

                            <div class="row">

                                <div class="col-md-12 col-lg-5">
                                    <!--Line Break-->
                                    <br />
                                    <h3 style="text-align: left; float: left;">Products: </h3>&#09;
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

                                    <asp:Button ID="btnAddProductToOrder" runat="server" Text="Add Product" CssClass="btn btn-Secondary" />

                                    <!-- Line Break -->
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

                                    <asp:Button ID="btnRemoveProductFromOrder" runat="server" Text="Remove Product" CssClass="btn" />

                                    <!-- Line Break -->
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnNewProd" runat="server" OnClick="btnNewProd_Click" 
                                        Text="Craete New Product" CssClass="btn btn-sm"/>
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

                            <asp:Button ID="btnSaveOrder" runat="server" Text="Submit" CssClass="btn btn-primary" />
                        </div>

                        <div runat="server" visible="false" id="OutstandingOrders">
                            <div class="jumbotron bg-dark text-white">
                                <h1>Accept Order</h1>
                            </div>
                        </div>

                        <div runat="server" visible="false" id="PastOrders">
                            <div class="jumbotron bg-dark text-white">
                                <h1>Past Orders</h1>
                            </div>
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
</asp:Content>
