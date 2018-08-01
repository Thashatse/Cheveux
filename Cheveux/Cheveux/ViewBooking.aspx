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
                        <asp:Label runat="server" ID="BookingLable"></asp:Label>
                    </div>

                    <div class="row">
                        <div class="col-xs-12 col-md-12">
                            <!--Booking Table-->
                            <asp:Table ID="BookingTable" runat="server"></asp:Table>
                            <!--Edit Booking Table -->
                            <div runat="server" id="Edit" visible="false">
                                <asp:Table ID="editBookingTable" runat="server">
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
                                </asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divCheckOut" visible="false">
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <!-- Check out -->
                                <asp:Table ID="tblCheckOut" runat="server">
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true" Width="300"> Service: </asp:TableCell>
                                        <asp:TableCell>
                                        <!--Service Name-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"> Service Description: </asp:TableCell>
                                        <asp:TableCell>
                                            <!--Service Description-->
                                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
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
                                        <asp:TableCell Font-Bold="true"> Date: </asp:TableCell>
                                        <asp:TableCell>
                                    <!--Booking Date-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true"> Time: </asp:TableCell>
                                        <asp:TableCell>
                                    <!--Booking Time-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell Font-Bold="true" ColumnSpan="2">
                                        <!--Invoice Lable--> Invoice: 
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
                                        <asp:TableCell Font-Bold="true">
                                        <!--Payment Type Lable--> Payment Type: 
                                        </asp:TableCell>
                                        <asp:TableCell>
                                        <!--Payment Type-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50">
                                        <asp:TableCell>
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
                                    <asp:TableRow Height="50">
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell>
                                        <!--Print-->
                                        <a href='#' onClick='window.print()' >Print This Page  </a>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divAddProducts" visible="false">
                        <div class="row">
                            <div class="col-xs-12 col-md-6">
                                Products:
                                <asp:ListBox runat="server" ID="lbProducts" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="500"></asp:ListBox>
                            </div>
                            <div class="col-xs-12 col-md-6">
                                <!-- Invoice -->
                                <!--Invoice Lable-->
                                Invoice:
                                <asp:Table ID="tblSale" runat="server">
                                    <asp:TableRow Height="50">
                                        <asp:TableCell>
                                        <!--Invoice-->
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-6">
                                <asp:Button ID="btnAddProductToSale" runat="server" Text="Add Product" CssClass="btn btn-Secondary" OnClick="btnAddProductToSale_Click" />
                            </div>
                            <div class="col-xs-12 col-md-6">
                                <asp:Button ID="btnRemoveProductFromSale" runat="server" Text="Remove Product" CssClass="btn" OnClick="btnRemoveProductFromSale_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-12">
                                <asp:Button ID="btnSaveSale" runat="server" Text="Save" CssClass="btn btn-Primary" OnClick="btnSaveSale_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Display the Booking for the client -->
                            <asp:Label ID="BackButton" runat="server"></asp:Label>
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
                            <asp:Label ID="confirmHeaderPlaceHolder" runat="server"></asp:Label>
                            <asp:Label ID="confirmPlaceHolder" runat="server"></asp:Label>
                            <!--Line Break-->
                            <br />
                            <br />
                            <!-- Edit -->
                            <asp:Button ID="no" runat="server" Text="No" class="btn btn-default" OnClick="showEdit" />
                            <asp:Button ID="yes" runat="server" Text="Yes" class="btn btn-primary" OnClick="commitEdit" />
                            <asp:Button ID="OK" runat="server" Text="OK" class="btn btn-primary" OnClick="OK_Click" Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="col-md-2 col-sm-1"></div>
            </div>
        </div>
    </form>
</asp:Content>
