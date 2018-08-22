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
                                <div class="row">
                                    <div class="col-md-12 col-lg-5">
                                        <!-- Summary -->
                                        <!--Line Break-->
                                        <br />
                                        <!--Lable-->
                                        <h3>Summary: </h3>
                                        <!--Line Break-->
                                        <br />
                                        <!-- Summary Table-->
                                        <asp:Table ID="tblEditSummary" runat="server"></asp:Table>
                                        <!--Line Break-->
                                        <br />
                                    </div>

                                    <div class="col-md-12 col-lg-7">
                                        <!-- edit Pane -->
                                        <div runat="server" id="divEditNone" visible="false" class="text-center">
                                            <!--Line Break-->
                                            <br />
                                            <br />
                                            <!--EditImage-->
                                            <img src="/Theam/img/edit-pencil-icon-vector-13483315.jpg" alt="Edit" width="200"
                                                height="200" class="img-fluid" />
                                            <!--Line Break-->
                                            <br />
                                            <br />
                                            <p>Select a detail to edit in the summary</p>
                                            <p>Or select done to complete edit</p>
                                            <!--Line Break-->
                                            <br />
                                            <asp:Button ID="btnDoneEdit" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="btnDoneEdit_Click" />
                                        </div>

                                        <div runat="server" id="divEditDateTime" visible="false">
                                            <!-- Date & Time -->
                                            <!--Line Break-->
                                            <br />
                                            <!--Lable-->
                                            <h3>Date & Time: </h3>
                                            <!--Line Break-->
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-6 col-md-12">
                                                    <!-- Calander -->
                                                    <asp:Calendar ID="calMAB" runat="server" CssClass="bg-secondary text-primary"
                                                        SelectedDate="<%#DateTime.Today%>" OnDayRender="calMAB_DayRender"
                                                        OnSelectionChanged="calMAB_SelectionChanged" AutoPostBack="True"></asp:Calendar>
                                                <!--Line Break-->
                                            <br />
                                                </div>
                                                <!-- Times -->
                                                <div class="col-lg-6 col-md-12">
                                                    <div class="row">
                                                        <div class="col-4">
                                                            <h5>Morning</h5>
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning1" OnClick="btnMorning1_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning2" OnClick="btnMorning2_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning3" OnClick="btnMorning3_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning4" OnClick="btnMorning4_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning5" OnClick="btnMorning5_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning6" OnClick="btnMorning6_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning7" OnClick="btnMorning7_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning8" OnClick="btnMorning8_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning9" OnClick="btnMorning9_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnMorning10" OnClick="btnMorning10_Click" Visible="false" AutoPostBack="true" />
                                                        </div>
                                                        <div class="col-4">
                                                            <h5>Afternoon</h5>
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon11" OnClick="btnAfternoon11_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon12" OnClick="btnAfternoon12_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon13" OnClick="btnAfternoon13_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon14" OnClick="btnAfternoon14_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon15" OnClick="btnAfternoon15_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon16" OnClick="btnAfternoon16_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon17" OnClick="btnAfternoon17_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon18" OnClick="btnAfternoon18_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon19" OnClick="btnAfternoon19_Click" Visible="false" AutoPostBack="true" /><br />
                                                            <br />
                                                            <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon20" OnClick="btnAfternoon20_Click" Visible="false" AutoPostBack="true" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--Line Break-->
                                            <br />
                                            <asp:Button ID="btnCancelEditDateAndTime" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnSaveEditDateAndTime" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="btnSaveEditDateAndTime_Click" />
                                        </div>

                                        <div runat="server" id="diveditStylist" visible="false">
                                            <!-- Stylist -->
                                            <!--Line Break-->
                                            <br />
                                            <!--Lable-->
                                            <h3>Stylist: </h3>
                                            <!--Line Break-->
                                            <br />
                                            <asp:RadioButtonList runat="server" ID="rblPickAStylist" CssClass="form-control"></asp:RadioButtonList>
                                            <!--Line Break-->
                                            <br />
                                            <asp:Button ID="btnCancelEditStylist" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnSaveEditStylist" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="btnSaveEditStylist_Click" />
                                        </div>

                                        <div runat="server" id="divEditService" visible="false">
                                            <!-- Service -->
                                            <!--Line Break-->
                                            <br />
                                            <!--Lable-->
                                            <h3>Service: </h3>
                                            <!--Line Break-->
                                            <br />
                                            <asp:Button ID="btnCancelEditService" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnSaveEditService" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="btnSaveEditService_Click" />
                                        </div>

                                        <div runat="server" id="divEditError" visible="false" class="text-center">
                                            <!--Line Break-->
                                            <br />
                                            <br />
                                            <!--EditImage-->
                                            <img src="https://cdn4.iconfinder.com/data/icons/smiley-vol-3-2/48/134-512.png" alt="Error" width="200"
                                                height="200" class="img-fluid" />
                                            <!--Line Break-->
                                            <br />
                                            <br />
                                            <p>An error occurred, please try again later.</p>
                                            <!--Line Break-->
                                            <br />
                                            <asp:Button ID="btnDoneEditError" runat="server" Text="Done" CssClass="btn btn-primary" OnClick="btnDoneEdit_Click" />
                                        </div>
                                    </div>
                                </div>
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
                                        <asp:Table ID="tblCheckOut" runat="server">
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true"> Billed To: </asp:TableCell>
                                                <asp:TableCell>
                                    <!--Billed To-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true"> Time & Date: </asp:TableCell>
                                                <asp:TableCell>
                                    <!--Booking Date-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Font-Bold="true" Width="250"> Service: </asp:TableCell>
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
                                                <asp:TableCell Font-Bold="true">
                                        <!--Payment Type Lable--> Payment Type: 
                                                </asp:TableCell>
                                                <asp:TableCell>
                                        <!--Payment Type-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell ColumnSpan="2">
                                        <!--Invoice-->
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Height="50">
                                                <asp:TableCell></asp:TableCell>
                                                <asp:TableCell>
                                                    <!-- Line Breake -->
                                                    <br />
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
                                        </asp:Table>
                                    </div>
                                </div>
                                <!-- Line Break -->
                                <br />
                                <!-- Payment Type-->
                                <div style="border: solid #F05F40 2px;" runat="server" id="divPamentType">
                                    <div class="col-12">
                                        <h2>Payment Type:</h2>
                                        <asp:Table ID="tblPaymentType" runat="server">
                                            <asp:TableRow Height="50">
                                                <asp:TableCell Width="250">
                                                    <!--Payment Type-->
                                                    <asp:RadioButtonList ID="PaymentType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PaymentType_SelectedIndexChanged">
                                                        <asp:ListItem> Cash </asp:ListItem>
                                                        <asp:ListItem> Credit </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    <!--Save Payment Type-->
                                                    <asp:Button ID="btnSavePaymentType" runat="server" Text="Save" OnClick="btnSavePaymentType_Click" class="btn btn-primary" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <div runat="server" id="divCalcuateChange" visible="false">
                                                        <p style="text-align: left; float: left;">Amount Due: &nbsp; </p>
                                                        <p style="text-align: right; float: right;">
                                                            <asp:Label runat="server" ID="lAmountDue"></asp:Label>
                                                        </p>
                                                        <br />
                                                        <br />
                                                        <p style="text-align: left; float: left;">Amount Tenderd: &nbsp; </p>
                                                        <p style="text-align: right; float: right;">
                                                            <asp:TextBox runat="server" ID="txtAmounTenderd" OnTextChanged="txtAmounTenderd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </p>
                                                        <br />
                                                        <p style="text-align: left; float: left;">Change Due: &nbsp; </p>
                                                        <p style="text-align: right; float: right;">
                                                            <asp:Label runat="server" ID="lChangeDue"></asp:Label>
                                                        </p>
                                                        <br />
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
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
                                <h3 style="text-align: left; float: left;">Products: </h3>
                                <p style="text-align: right; float: right;">
                                    <!-- Search -->
                                    <asp:TextBox ID="txtProductSearch" runat="server" AutoPostBack="true" placeholder="search"
                                        OnDataBinding="btnAddProduct_Click" OnTextChanged="btnAddProduct_Click" CssClass="form-control"></asp:TextBox>
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

                                <asp:Button ID="btnAddProductToSale" runat="server" Text="Add Product" CssClass="btn btn-Secondary" OnClick="btnAddProductToSale_Click" />

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
                                </asp:DropDownList>

                                <!-- Line Break -->
                                <br />
                                <br />

                                <asp:Button ID="btnRemoveProductFromSale" runat="server" Text="Remove Product" CssClass="btn" OnClick="btnRemoveProductFromSale_Click" />

                                <!-- Line Break -->
                                <br />
                                <br />

                                <asp:Button ID="btnSaveSale" runat="server" Text="Return To Check Out" CssClass="btn btn-primary" OnClick="btnSaveSale_Click" />
                            </div>
                            <div class="col-md-12 col-lg-5">
                                <!--Line Break-->
                                <br />
                                <!-- Invoice -->
                                <!--Invoice Lable-->
                                <h3>Invoice: </h3>
                                <!--Line Break-->
                                <br />
                                <asp:ListBox runat="server" ID="lProductsOnSale" CssClass="form-control" DataTextField="Name" DataValueField="ID" Height="300"></asp:ListBox>
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
                </div>
                <div class="col-md-2 col-sm-1"></div>
            </div>
        </div>
    </form>
</asp:Content>
