<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="MakeABooking.aspx.cs" Inherits="Cheveux.MakeABooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Profile - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="MakeABooking" runat="server">
        <div class="container-fluid">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="jumbotron">
                            <h1>Make A Booking</h1>
                            <br />
                            <asp:PlaceHolder runat="server" ID="BookingSummary"></asp:PlaceHolder>
                        </div>
                        <div class="container" runat="server" id="divServices" visible="true">
                            <h3>Choose Service(s)</h3>
                            <br />
                            <asp:CheckBoxList runat="server" ID="cblPickAService">
                            </asp:CheckBoxList>
                        </div>

                        <div class="container" runat="server" id="divStylist" visible="false">
                            <h3>Choose A Hairstylist</h3>
                            <br />
                            <asp:RadioButtonList runat="server" ID="rblPickAStylist">
                            </asp:RadioButtonList>
                        </div>

                        <div class="container" runat="server" id="divDateTime" visible="false">
                            <h3>Choose A Date & Time</h3>
                            <br />
                            <asp:Calendar runat="server" ID="calBooking" Width="500" Height="500"></asp:Calendar>
                            <asp:PlaceHolder runat="server" ID="AvailableTimes"></asp:PlaceHolder>
                        </div>

                        <div class="container" runat="server" id="divSummary" visible="false">
                            <h3>Booking Summary</h3>
                            <br />
                            <asp:Table ID="tblBookingSummary" runat="server">
                                <asp:TableRow ID="Summary" runat="server">
                                    <asp:TableCell Text="Service(s):"></asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Text="Stylist"></asp:TableCell>
                                    <asp:TableCell></asp:TableCell>
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

                <div class="row">
                    <div class="col-2 text-left">
                        <asp:Button runat="server" ID="btnPrevious" Visible="false" Text="Choose Service(s)" OnClick="btnPrevious_Click" />
                    </div>
                    <div class="col-8"></div>
                    <div class="col-2 text-right">
                        <asp:Button runat="server" ID="btnNext" OnClick="btnNext_Click" Text="Choose Hairstylist" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
