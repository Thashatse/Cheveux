﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="MakeABooking.aspx.cs" Inherits="Cheveux.MakeABooking" %>

<asp:content id="Content1" contentplaceholderid="PageTitle" runat="server">
    Make A Booking - Cheveux
</asp:content>
<asp:content id="Content2" contentplaceholderid="head" runat="server">
    </script>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-1"></div>
            <div class="col-10">
            <form id="MakeABooking" runat="server">
                
                <div class="container-fluid">
                    <div class="row">

                        <div class="col-12">

                            <div class="jumbotron  bg-dark text-white">
                                <h1><asp:Label runat="server" ID="lblHeading" Visible="true" Text ="Make A Booking"></asp:Label></h1>
                                <asp:Label runat="server" ID="lblErrorSummary" Visible="false"></asp:Label>
                              </div>
                        </div>
                    </div>

                     <div runat="server" id="divManageSchedule" visible ="false">
                          <div runat="server" id="divChooseAStylist">
                                <div class="container">
                                    <div class="row">
                                        <div style="border: solid #F05F40 2px; width: 900px;  height: 400px;">
                                            <h3>Choose A Hairstylist</h3>
                                            <br />
                                                <asp:ListBox runat="server" ID="lbChooseAStylist" CssClass="form-control" DataTextField="FirstName" DataValueField="UserID" Height="300" AutoPostBack="true"></asp:ListBox>
                                        </div>
                                         <div runat="server" id="divSelectDate" style="border: solid #F05F40 2px; height: 400px; overflow-y: scroll;">
                                               <asp:Calendar runat="server" ID="calSchedule" Width="300" Height="150" OnDayRender="calBooking_DayRender" OnSelectionChanged="calBooking_SelectionChanged" ></asp:Calendar>
                                         </div>
                                        <asp:DropDownList runat="server" id="drpWhen" AutoPostBack="true">
                                            <asp:ListItem text="--Select Time Frame--" value="0"></asp:ListItem>
                                            <asp:ListItem text="Whole Day" value="1"></asp:ListItem>
                                            <asp:ListItem text="Morning" value="2"></asp:ListItem>
                                            <asp:ListItem text="Afternoon" value="3"></asp:ListItem>                                            
                                        </asp:DropDownList>
                                        
                                        <asp:Button class='btn btn-primary' runat="server" ID="btnManageSchedule" OnClick="btnNext_Click" Text="Submit" />
                                    </div>
                                </div>
                            </div>
                     </div>

                    <div class="row">

                    <div class="col-2 text-left">

                        <asp:Button class='btn btn-basic' runat="server" ID="btnPrevious" Visible="false" Text="Choose Service(s)" OnClick="btnPrevious_Click" />

                    </div>

                    <div class="col-8 text-center">
                        <asp:label runat="server" ID="lblGoService" Text="Service"></asp:label>&nbsp;-&nbsp;
                           <asp:label runat="server" ID="lblGoStylist"  Text="Hairstylist  "></asp:label>&nbsp;-&nbsp;
                           <asp:label runat="server" ID="lblGoDateTime" Text="Date & Time  "></asp:label>&nbsp;-&nbsp;
                        <asp:label runat="server" ID="lblGoCustomer"  visible="false" Text="Customer  &nbsp;-&nbsp;"></asp:label>
                           <asp:label runat="server" ID="lblGoSummary" Text="Summary"></asp:label>
                    </div>

                    <div class="col-2 text-right">

                        <asp:Button class='btn btn-primary' runat="server" ID="btnNext" OnClick="btnNext_Click" Text="Choose Hairstylist" />

                    </div>
                </div>
                    <!-- Line Break-->
                <br />

                    <div class="row">
                       <div class="col-md-12 col-lg-5"></div>
                        <div class="col-md-12 col-lg-7">
                             <div class="container" runat="server" id="divLabels">
                                        <h6><b>Select Service(s)</b></h6>
                             </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-lg-5">
                            <div style="border: solid #F05F40 2px; height: 400px; overflow-y: scroll;">
                                <div class="container">
                                    <asp:Table ID="tblBookingSummary" runat="server">
                                        <asp:TableHeaderRow ColumnSpan="2">
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblChoose"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableRow>
                                            <asp:TableCell >
                                                <asp:Label runat="server" ID="lblServiceLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            </asp:TableCell>
</asp:TableRow>

                                            <asp:TableRow >
                                            <asp:TableCell >
                                                <asp:Label runat="server" ID="lCustomerLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lCustomer"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        
                                        <asp:TableRow>
                                            <asp:TableCell>
                                            <asp:Label runat="server" ID="lblStylistLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblStylist"></asp:Label>
                                            </asp:TableCell>
                                            
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblDateLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                                            </asp:TableCell>
                                                
                                        </asp:TableRow>
                                        <asp:TableRow >
                                            <asp:TableCell >
                                                <asp:Label runat="server" ID="lblTimeLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblTime"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell >
                                                <asp:Label runat="server" ID="lblNoOfHoursLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label runat="server" ID="lblNoOfHours"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                                <asp:TableCell >
                                                <asp:Label runat="server" ID="lblTotalCostLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell >
                                                <asp:Label runat="server" ID="lblTotalCost"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        
                                        <asp:TableRow>
                                            <asp:TableCell >
                                                <asp:Label runat="server" ID="lblCommentLabel"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox runat="server" ID="txtComment"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-lg-7">
                            <div runat="server" id="divServices" visible="true">

                                <div class="container">
                                    <asp:Label runat="server" ID="lblServices" visible="true"></asp:Label>
                                    <div style="border: solid #F05F40 2px; height: 400px; overflow-y: scroll;">
                                        <div runat="server" id="divNatural" visible="true">
                                            <a name="Natural"></a>
                                            <h5>Natural</h5>
                                            <asp:CheckBoxList runat="server" ID="cblPickAServiceN" OnSelectedIndexChanged="LoadSummary" AutoPostBack="true">
                                            </asp:CheckBoxList><br />
                                            <a name="Application"></a>
                                        </div>

                                        <div runat="server" id="divApplication" visible="true">

                                            <h5>Application</h5>
                                            <asp:RadioButtonList runat="server" ID="rblPickAServiceA" OnSelectedIndexChanged="LoadSummary" AutoPostBack="true">
                                            </asp:RadioButtonList><br />
                                            <a name="Braid"></a>
                                        </div>

                                        <div runat="server" id="divBraids" visible="true">

                                            <h5>Braids</h5>
                                            <asp:RadioButtonList runat="server" ID="rblPickAServiceB" OnSelectedIndexChanged="LoadSummary" AutoPostBack="true">
                                            </asp:RadioButtonList><br />
                                            <a = name="Other"></a>
                                        </div>
                                        <div runat="server" id="divOther" visible="false">
                                            <h5>Other</h5>
                                            <asp:RadioButtonList runat="server" ID="rblPickOtherService" OnSelectedIndexChanged="LoadSummary" AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </div>
                                        <div runat ="server" id="divSickLeave" visible ="false">
                                            <h5>Sick Leave Type</h5>
                                            <asp:RadioButtonList runat="server" ID="rblSickLeave" OnSelectedIndexChanged="LoadSummary" AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>


                                </div>
                            </div>

                            <div runat="server" id="divStylist" visible="false">
                                <div class="container">
                                    <div class="row">
                                        <div style="border: solid #F05F40 2px; width: 900px;  height: 400px;">
                                            <h3>Choose A Hairstylist</h3>
                                            <br />
                                    <asp:ListBox runat="server" ID="lbPickAStylist" CssClass="form-control" DataTextField="FirstName" DataValueField="UserID" Height="300" OnSelectedIndexChanged="lbPickAStylist_SelectionIndexChanged" AutoPostBack="true"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div runat="server" id="divDateTime" visible="false" style="border: solid #F05F40 2px; height: 400px; overflow-y: scroll;">
                                <div class="container">
                                    <h3>Choose A Date & Time</h3>
                                    <br />
                                    <div class="row">

                                        <div class="col-lg-6  col-md-12">
                                            <asp:Calendar runat="server" ID="calBooking" Width="300" Height="150" OnDayRender="calBooking_DayRender" OnSelectionChanged="calBooking_SelectionChanged" ></asp:Calendar>
                                            <br />
                                        </div>
                                        <div class="col-lg-6 col-md-12">
                                            <div class="row">

                                                <div class="col-6">
                                                    <h4>Morning</h4>
                                                    <br />
                                                    <asp:label runat="server" ID="lblMorningUnavailable"  Text="Unavailable" Visible="false"></asp:label>
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
                                                    
                                                </div>
                                                <div class="col-6">
                                                    <h4>Afternoon</h4>
                                                    <br />
                                                    <asp:label runat="server" ID="lblAfternoonUnavailable"  Text="Unavailable" Visible="false"></asp:label>
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
                             <div runat="server" id="divSummaryPic" visible="false" style="height: 400px; " class="text-center">
                                 <br />
                                        <img src="http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/checkbox.png" alt="Edit" width="300"
                                                height="200" class="img-fluid" />
                                 <br />
                                 <br />
                                     <asp:Button runat="server" Text="Submit" id="btnMakeBooking" onclick="btnNext_Click" class='btn btn-primary'></asp:Button>
                              </div>
                            <div runat="server" id="divSelectUser" visible="false" 
                                style="border: solid #F05F40 2px; height: 400px;">
                                <!--User-->
                                <div class="container">
                                    <!--Line Break-->
                                    <br />
                                    <h3 style="text-align: left; float: left;">Select Customer: </h3>
                                    <p style="text-align: right; float: right;">
                                        <!-- Search -->
                                        <asp:TextBox ID="txtCustomerSearch" runat="server" AutoPostBack="true" placeholder="search"
                                            OnDataBinding="txtCustomerSearch_DataBinding" OnTextChanged="txtCustomerSearch_DataBinding" CssClass="form-control"></asp:TextBox>
                                    </p>
                                    <!--Line Break-->
                                    <br />
                                    <asp:ListBox runat="server" ID="lbCustomers" CssClass="form-control" DataTextField="Name" DataValueField="ID" 
                                        Height="250"></asp:ListBox>
                                    <!--Line Break-->
                                    <br />
                                    <p style="text-align: right; float: right;">
                                        <asp:Button ID="btnNewCust" runat="server" OnClick="btnNewCust_Click" 
                                        Text="New Customer" CssClass="btn btn-secondary btn-sm"/>
                                    </p>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
        </form>
        </div>
        <div class="col-1"></div>
    </div>
    
</asp:content>
