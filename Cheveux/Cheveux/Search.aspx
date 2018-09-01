﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Cheveux.Search" %>

<asp:content id="Content1" contentplaceholderid="PageTitle" runat="server">
    Search - Cheveux
</asp:content>
<asp:content id="Content2" contentplaceholderid="head" runat="server">
    <script>
        function searchOnSearchPage() {
            window.location.href = "Search.aspx?ST=" + document.getElementById('serchTermOnSearchPage').value;
        }
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
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
<div class="jumbotron bg-dark text-white">
  <h1>Search</h1>
    <!--Search Box-->
    <asp:Label ID="JumbotronSearchBox" runat="server" Font-Bold="true"></asp:Label>
    <!--New Line-->
    <br /><br />

  <div class="form-group">
      <!--Search Button & Text Box-->
                        <input id="serchTermOnSearchPage" type="text" placeholder="Search" onblur="searchOnSearchPage()" >
                        <a class="btn btn-primary" href="javascript:searchOnSearchPage()">Search</a>
  </div>
</div>
        <!--Search Results-->
        <div class="row">
                <div class="col-xs-12 col-md-12">
                    <form id="SearchBookings" runat="server">
                    <div class="row">
                        
                    
                    <!--Bookings-->
                        <div class="col-12">
                        <div style="text-align: left; float: left;">
                            <asp:Label runat="server" ID="bookingResultLable"></asp:Label>
                            </div>
                    <div style="text-align: right; float: right;">
                        <asp:Button runat="server" Text="Fillter" id="FillterBookingResults" cssClass="btn btn-default" Visible="false" onclick="showFilterBooking"></asp:Button>
                        </div>
                        </div>
                    <div id="divBookingSearchFilter" Visible="false" runat="server" class="col-12" style="border: solid #F05F40 2px;">
                        <h4>Filter by date</h4>
                        <asp:Label runat="server" id="lCalBookingError" Visible="false"></asp:Label>
                        <div class="row">
                        <div class="col-5">
                                            <p>Start Date: </p>
                                            <asp:Calendar CssClass="bg-secondary text-primary" ID="CalendarDateStrart" runat="server" OnSelectionChanged="searchBookings"></asp:Calendar>
                                        </div>
                                        <div class="col-5">
                                            <p>End Date: </p>
                                            <asp:Calendar CssClass="bg-secondary text-primary" ID="CalendarDateEnd" runat="server" OnSelectionChanged="searchBookings"></asp:Calendar>
                                        </div>
                    </div>
                        <!--New Line-->
                        <br />
                    </div>
                    <div class="col-12">
                        <asp:Table id="bookingSearchResults" runat="server"></asp:Table>
                        </div> 
                    
                    </div>

                    </form>
                    <!--New Line-->
                        <br /><br />
                    
                    <!--Services-->
                    <asp:Label runat="server" ID="serviceResultsLable"></asp:Label>
                    <asp:Table id="serviceSearchResults" runat="server"></asp:Table>
                    <!--New Line-->
                    <br /><br />
                    <!--Products-->
                    <asp:Label runat="server" ID="ProductResultsLable"></asp:Label>
                    <asp:Table id="ProductSearchResults" runat="server"></asp:Table>
                    <!--New Line-->
                    <br /><br />
                    <!--Stylist-->
                    <asp:Label runat="server" ID="StylistResultsLable"></asp:Label>
                    <asp:Table id="StylistSearchResults" runat="server"></asp:Table>
                    </div>
            </div>
    </div>
                <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:content>
