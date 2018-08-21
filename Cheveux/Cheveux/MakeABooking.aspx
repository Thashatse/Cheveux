﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="MakeABooking.aspx.cs" Inherits="Cheveux.MakeABooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Make A Booking - Cheveux
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
                                <asp:Label runat="server" ID="lblErrorSummary" Visible="false"></asp:Label>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-lg-5">
                            <div style="border: solid #F05F40 2px; overflow-x: scroll;">
                                <div class="container">
                                    <h3>Booking Summary</h3>
                                    <br />
                                    <asp:Table ID="tblBookingSummary" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell Text="Date: "></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Time: "></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblTime"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Stylist"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblStylist"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell Text="Service(s):"></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblServices"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>


                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-7">
                            <div runat="server" id="divServices" visible="true">
                               
                                <div class="container">
                                    <div class="container">

                                        <h6><b>Select Service(s)</b></h6>

                                        <!--Nav Pills for external system-->
                                        <ul class="nav nav-pills nav-stacked">
                                            <li><a href="#Natural">Natural &nbsp; &nbsp;</a></li>
                                            <li><a href="#Application">Application  &nbsp; &nbsp;</a></li>
                                            <li><a href="#Braid">Braid &nbsp; &nbsp;</a></li>
                                        </ul>
                                        <a name="Natural"></a>
                                        <br />
                                    </div>


                                    <div style="border: solid #F05F40 2px;height: 400px; overflow-y: scroll;">
                                        <div runat="server" id="divNatural" visible="true">

                                            <h5>Natural</h5>
                                            <asp:CheckBoxList runat="server" ID="cblPickAServiceN" OnSelectedIndexChanged="cblPickAServiceN_SelectedIndexChanged">
                                            </asp:CheckBoxList><br />
                                            <a name="Application"></a>
                                        </div>

                                        <div runat="server" id="divApplication" visible="true">

                                            <h5>Application</h5>
                                            <asp:RadioButtonList runat="server" ID="rblPickAServiceA" OnSelectedIndexChanged="rblPickAServiceA_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:RadioButtonList><br />
                                            <a name="Braid"></a>
                                        </div>

                                        <div runat="server" id="divBraids" visible="true">

                                            <h5>Braids</h5>
                                            <asp:RadioButtonList runat="server" ID="rblPickAServiceB" OnSelectedIndexChanged="rblPickAServiceB_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>


                                </div>
</div>



                                <div runat="server" id="divStylist" visible="false">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-5" style="border: solid #F05F40 2px; width: 575px; height: 400px; overflow-x: scroll;">
                                                <h3>Choose A Hairstylist</h3>
                                                <br />
                                                <asp:RadioButtonList runat="server" ID="rblPickAStylist">
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-2"></div>
                                            <div class="col-5" style="border: solid #F05F40 2px; width: 575px; height: 400px;">
                                                <asp:Table runat="server" ID="tblSide">
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div runat="server" id="divDateTime" visible="false" style="border: solid #F05F40 2px; width: 700px; height: 400px; overflow-x: scroll;">
                                    <div class="container">
                                        <h3>Choose A Date & Time</h3>
                                        <br />
                                        <div class="row">

                                            <div class="col-6">
                                                <asp:Calendar runat="server" ID="calBooking" Width="300" Height="150" OnDayRender="calBooking_DayRender" OnSelectionChanged="calBooking_SelectionChanged"></asp:Calendar>
                                            </div>
                                            <div class="col-6">
                                                <div class="row">

                                                    <div class="col-6">
                                                        <h4>Morning</h4>
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning1" OnClick="btnMorning1_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning2" OnClick="btnMorning2_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning3" OnClick="btnMorning3_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning4" OnClick="btnMorning4_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning5" OnClick="btnMorning5_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning6" OnClick="btnMorning6_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning7" OnClick="btnMorning7_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning8" OnClick="btnMorning8_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning9" OnClick="btnMorning9_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnMorning10" OnClick="btnMorning10_Click" Visible="false" /><br />
                                                        <br />
                                                    </div>
                                                    <div class="col-6">
                                                        <h4>Afternoon</h4>
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon11" OnClick="btnAfternoon11_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon12" OnClick="btnAfternoon12_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon13" OnClick="btnAfternoon13_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon14" OnClick="btnAfternoon14_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon15" OnClick="btnAfternoon15_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon16" OnClick="btnAfternoon16_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon17" OnClick="btnAfternoon17_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon18" OnClick="btnAfternoon18_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon19" OnClick="btnAfternoon19_Click" Visible="false" /><br />
                                                        <br />
                                                        <asp:Button class='btn btn-light' runat="server" ID="btnAfternoon20" OnClick="btnAfternoon20_Click" Visible="false" /><br />
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>

                        </div>
                    </div>

                    <!-- Line Break-->
                    <br />

                    <div class="row">

                        <div class="col-2 text-left">

                            <asp:Button class='btn btn-basic' runat="server" ID="btnPrevious" Visible="false" Text="Choose Service(s)" OnClick="btnPrevious_Click" />

                        </div>

                        <div class="col-8"></div>

                        <div class="col-2 text-right">

                            <asp:Button class='btn btn-primary' runat="server" ID="btnNext" OnClick="btnNext_Click" Text="Choose Hairstylist" />

                        </div>
                    </div>
                </div>



                <!-- Line Break-->
                <br />
                <br />

            </form>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
