<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="MakeABooking.aspx.cs" Inherits="Cheveux.MakeABooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
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
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <form id="MakeABooking" runat="server">
                <div class="container-fluid">
                    <div class="row">
                                           
                <div class="col-12">
                                           
                    <div class="jumbotron  bg-dark text-white">
                        <h1>Make A Booking</h1>
                        <br />
                        <asp:Label runat="server" ID="BookingSummary"></asp:Label>
                    </div>
                    <div class="container" runat="server" id="divServices" visible="true">
                        <h3>Choose Service(s)</h3>
                        <br />
                        <asp:DropDownList runat="server" ID="drpPickAService">
                        </asp:DropDownList>
                    </div>

                    <div runat="server" id="divStylist" visible="false">
                        <h3>Choose A Hairstylist</h3>
                        <br />
                        <asp:RadioButtonList runat="server" ID="rblPickAStylist">
                        </asp:RadioButtonList>
                    </div>

                    <div runat="server" id="divDateTime" visible="false">
                        <h3>Choose A Date & Time</h3>
                        <br />
                        <asp:Calendar runat="server" ID="calBooking" Width="500" Height="500"></asp:Calendar>
                        <asp:PlaceHolder runat="server" ID="AvailableTimes">
                            <asp:DropDownList runat="server" ID="drpAvailableTimes">
                            </asp:DropDownList>
                        </asp:PlaceHolder>
                    </div>

                    <div runat="server" id="divSummary" visible="false">
                        <h3>Booking Summary</h3>
                        <br />
                        <asp:Table ID="tblBookingSummary" runat="server">
                            <asp:TableRow ID="Summary" runat="server">
                                <asp:TableCell Text="Service(s):"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="lblServices"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Stylist"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="lblStylist"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Text="Date & Time"></asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label>&ensp;&ensp;<asp:Label runat="server" ID="lblTime"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>

                </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-2 text-left">

                        <asp:Button class='btn btn-basic' runat="server" ID="btnPrevious" Visible="false" Text="Choose Service(s)" OnClick="btnPrevious_Click" />

                    </div>

                    <div class="col-8"></div>

                    <div class="col-2 text-right">

                        <asp:Button class='btn btn-primary' runat="server" ID="btnNext" OnClick="btnNext_Click" Text="Choose Hairstylist" />

                    </div>
                </div>
            </form>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
