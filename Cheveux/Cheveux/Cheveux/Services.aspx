<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="Cheveux.Cheveux.Services" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Services - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="ViewService">

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
            <div runat="server" id="divCustomerView" visible="true">
                  <div class="jumbotron  bg-dark text-white">
                            <h1>Services</h1>
                            <!-- line Break -->
                            <br />
                            <asp:Label runat="server" ID="lblErrorSummary" Visible="false"></asp:Label>
                                <div class="row">
                                    <div class="col-12">
                                        <!-- View By search tearm -->
                                        <p>View Services By Search Term: </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-5">
                                        <asp:TextBox ID="txtProductSearchTerm" runat="server" class="form-control" AutoPostBack="true"
                                            OnTextChanged="Page_Load"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button ID="btnProductSearch" runat="server" Text="Search" CssClass="btn btn-primary" />
                                    </div>
                                </div>

                  </div>
                <div class="row">
                    <div class="col-md-12">
                        <!-- List Service Table -->
                        <asp:Table ID="tblServicesTable" runat="server">
                        </asp:Table>
                    </div>
                </div>
            </div>
            <div runat="server" id="divViewService" visible="false">
                                  <div class="jumbotron  bg-dark text-white">
                            <h1>View Service</h1>

                                  </div>
                <div runat="server" id="divServiceTable">
                    <asp:Table runat="server" ID="tblServiceDetails2" Visible="true" VerticalAlign="Top">
                        <asp:TableRow>
                            <asp:TableCell Text="Name: " Width="150px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblName"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="No of Slots: " Width="150px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblNoOfSlots"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Price: " Width="150px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblPrice"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                <div runat="server" id="divBraidDetails" visible="false">
                    <asp:Table runat="server" ID="tblServiceDetails">
                        <asp:TableRow>
                            <asp:TableCell Text="Style: " Width="150px" VerticalAlign="Middle"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblStyle"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Width: " Width="150px" VerticalAlign="Middle"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblWidth"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Text="Length: " Width="150px" VerticalAlign="Middle"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblLength"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                <div runat="server" id="divTableDesc" visible="true">
                    <asp:Table runat="server" ID="tblDesc">
                        <asp:TableRow>
                            <asp:TableCell Text="Description: " Width="150px"></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblDescription"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
        </form>
</asp:Content>
