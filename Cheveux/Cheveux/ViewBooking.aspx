<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="ViewBooking.aspx.cs" Inherits="Cheveux.ViewBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Booking - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function goBack() {
            window.history.back()
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
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
                <!-- if the user is loged In -->
                <!-- Display the Booking for the client -->
                <div runat="server" id="LogedIn" visible="false">
                    <div class="jumbotron bg-dark text-white">
                        <!--Bookings Heading-->
                        <asp:label runat="server" id="BookingLable"></asp:label>
                    </div>

                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <!--Booking Table-->
                            <asp:table id="BookingTable" runat="server"></asp:table>
                            <!--Edit Booking Table -->
                            <div runat="server" id="Edit" visible="false">
                                <asp:table id="editBookingTable" runat="server">
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"></asp:TableCell>
                                        <asp:TableCell>
                                            <!--Service Name-->
                                            <asp:DropDownList ID="DDLService" runat="server"></asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"></asp:TableCell>
                                        <asp:TableCell>
                                            <!--Service Description-->
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"></asp:TableCell>
                                        <asp:TableCell>
                                            <!--Service Price-->
                                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"></asp:TableCell>
                                        <asp:TableCell>
                                            <!--Service Stylist-->
                                            <asp:DropDownList ID="DDLStylist" runat="server"></asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"></asp:TableCell>
                                        <asp:TableCell>
                                    <!--Booking Date-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"></asp:TableCell>
                                        <asp:TableCell>
                                    <!--Booking Time-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell>
                                            <asp:DropDownList ID="drpEmpNames" runat="server" AutoPostBack="True">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <!--Cancel Updates-->
                                            <a href='javascript:history.back()'>Cancel   </a>
                                            <!--Save Updates-->
                                            <asp:Button ID="Save" runat="server" Text="Save" class="btn btn-default" OnClick="Save_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divCheckOut" visible="false">
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Check out -->
                                <div style="border: solid #F05F40 2px;" runat="server" id="divCheckOutInvoice">
                                    <div class="col-12">
                                        <h2>Invoice: </h2>
                                        <asp:table id="tblCheckOut" runat="server">
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true" Width="300"> Service: </asp:TableCell>
                                                <asp:TableCell>
                                        <!--Service Name-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true"> Stylist: </asp:TableCell>
                                                <asp:TableCell>
                                                    <!--Service Stylist-->
                                                    <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true"> Time & Date: </asp:TableCell>
                                                <asp:TableCell>
                                    <!--Booking Date-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true">
                                        <!--Payment Type Lable--> Payment Type: 
                                        </asp:TableCell>
                                                <asp:TableCell>
                                        <!--Payment Type-->
                                        </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
                                        <!--Invoice-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
                                                    <!--Save Payment Type-->
                                                    <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" class="btn btn-secondary" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
                                        <!--Print-->
                                        <a href='#' onClick='window.print()' >Print Invoice  </a>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:table>
                                    </div>
                                </div>
                                <!-- Line Break -->
                                <br />
                                <!-- Payment Type-->
                                <div style="border: solid #F05F40 2px;" runat="server" id="divPamentType">
                                    <div class="col-12">
                                        <h2>Payment Type:</h2>
                                        <asp:table id="tblPaymentType" runat="server">
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Width="300">
                                                    <!--Payment Type-->
                                                    <asp:RadioButtonList ID="PaymentType" runat="server">
                                                        <asp:ListItem Selected="True"> Cash </asp:ListItem>
                                                        <asp:ListItem> Credit </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <!--Save Payment Type-->
                                                    <asp:Button ID="btnSavePaymentType" runat="server" Text="Save" OnClick="btnSavePaymentType_Click" class="btn btn-primary" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divAddProducts" visible="false">
                        <div class="row">
                            <div class="col-md-12 col-lg-5">
                                <!--Line Break-->
                                <br />
                                <h3>Products: </h3>
                                <!--Line Break-->
                                <br />
                                <asp:listbox runat="server" id="lbProducts" cssclass="form-control" datatextfield="Name" datavaluefield="ID" height="300"></asp:listbox>
                                <!--Line Break-->
                                <br />
                                <br />
                                <!-- Search -->
                                <asp:textbox id="txtProductSearch" runat="server"></asp:textbox>
                                <asp:button id="btnSearchProduct" runat="server" text="Search" cssclass="btn btn-Secondary" onclick="btnAddProduct_Click" />
                            </div>
                            <div class="col-md-12 col-lg-2 text-center">
                                <!-- Line Break -->
                                <br />
                                <br />
                                <br />
                                <br />

                                <asp:button id="btnAddProductToSale" runat="server" text="Add Product" cssclass="btn btn-Secondary" onclick="btnAddProductToSale_Click" />


                                <!-- Line Break -->
                                <br />
                                <br />

                                Qty:
                                <asp:dropdownlist runat="server" id="Qty" cssclass="btn btn-outline-secondary dropdown-toggle">
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
                                </asp:dropdownlist>

                                <!-- Line Break -->
                                <br />
                                <br />

                                <asp:button id="btnRemoveProductFromSale" runat="server" text="Remove Product" cssclass="btn" onclick="btnRemoveProductFromSale_Click" />

                                <!-- Line Break -->
                                <br />
                                <br />

                                <asp:button id="btnSaveSale" runat="server" text="Return To Check Out" cssclass="btn btn-primary" onclick="btnSaveSale_Click" />
                            </div>
                            <div class="col-md-12 col-lg-5">
                                <!--Line Break-->
                                <br />
                                <!-- Invoice -->
                                <!--Invoice Lable-->
                                <h3>Invoice: </h3>
                                <!--Line Break-->
                                <br />
                                <asp:listbox runat="server" id="lProductsOnSale" cssclass="form-control" datatextfield="Name" datavaluefield="ID" height="300"></asp:listbox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Display the Booking for the client -->
                            <asp:label id="BackButton" runat="server"></asp:label>
                        </div>
                    </div>


                    <!-- if the user is loged Out -->
                    <div runat="server" id="LogedOut">
                        <div class="jumbotron bg-dark text-white">
                            <h2>View Booking Details</h2>
                            <p>Please log-in to view booking details</p>
                            <button type="button" class="btn btn-primary"><a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Bookings.aspx" id="LogedOutButton">Login / Sign Up</a></button>
                        </div>
                    </div>

                    <div runat="server" id="confirm" visible="false">
                        <div class="jumbotron bg-dark text-white">
                            <asp:label id="confirmHeaderPlaceHolder" runat="server"></asp:label>
                            <asp:label id="confirmPlaceHolder" runat="server"></asp:label>
                            <!--Line Break-->
                            <br />
                            <br />
                            <!-- Edit -->
                            <asp:button id="no" runat="server" text="No" class="btn btn-default" onclick="showEdit" />
                            <asp:button id="yes" runat="server" text="Yes" class="btn btn-primary" onclick="commitEdit" />
                            <asp:button id="OK" runat="server" text="OK" class="btn btn-primary" onclick="OK_Click" visible="false" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 col-sm-1"></div>
            </div>
        </div>
    </form>
</asp:Content>
