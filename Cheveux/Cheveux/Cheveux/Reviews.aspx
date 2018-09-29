<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="Cheveux.Reviews" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reviews - Cheveux 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .starRating {
            width: 50px;
            height: 50px;
            cursor: pointer;
            background-repeat: no-repeat;
            display: block;
        }

        .filledStar {
            background-image: url("../Images/filledStar.gif");
        }

        .waitingStar {
            background-image: url("../Images/waitingStar.gif");
        }

        .emptyStar {
            background-image: url("../Images/star.gif");
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />


        <div class="row">
            <div class="col-1"></div>
            <div class="col-10">
                <!-- If user is not logged in-->
                <asp:PlaceHolder ID="phLogin" runat="server">
                    <div class="jumbotron bg-dark text-white" runat="server" id="LoggedOut">
                        <div class="row">
                            <div class="col-lg-9 col-md-12 col-sm-12">
                                <h1>Write and Read Reviews</h1>
                                <p>Please Login/Sign-up To Proceed</p>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Reviews.aspx" id="LogedOutButton">Login/Sign-up</a>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <!-- If user is logged in -->
                <asp:PlaceHolder ID="phMain" runat="server">

                    <!--jumbotron page heading-->
                    <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="OpeningHeader" CssClass="text-center" runat="server" Text="" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <a class="btn btn-primary" href="../Cheveux/Reviews.aspx?Action=MakeAreview" id="btnRev" runat="server">Write A Review</a>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <!--for general errors or telling user what must happen next-->
                        <div class="col-sm-12 col-md-12 alert alert-success alert-dismissible" id="sucNoti" visible="false" runat="server">
                            <asp:Label ID="lblSucNoti" runat="server" Text=" "></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" runat="server" onclick="alertExitClick">&times;</a>
                        </div>
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible" id="erNoti" visible="false" runat="server">
                            <asp:Label ID="lblErNoti" runat="server" Text=" "></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" runat="server" onclick="alertExitClick">&times;</a>
                        </div>
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible" id="erReview" visible="false" runat="server">
                            <asp:Label ID="lblErReview" runat="server" Text=" "></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" runat="server" onclick="alertExitClick">&times;</a>
                            <%--<asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Post Review" OnClick="alertExitClick" />--%>
                        </div>
                    </div>

                    <!--Users reading reviews -->
                    <asp:PlaceHolder ID="readReviews" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-4 col-md-2 col-lg-2">
                                <p>View Reviews: </p>
                            </div>
                            <div class="col-sm-8 col-md-4 col-lg-4">
                                <asp:DropDownList ID="drpReadType" runat="server" AutoPostBack="True"
                                    CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpReadType_SelectedIndexChanged">
                                    <asp:ListItem Text="All Reviews" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="My Reviews" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                    </asp:PlaceHolder>

                    <!--Users writing reviews -->
                    <asp:PlaceHolder ID="makeAreview" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-4 col-md-2 col-lg-2">
                                <p>Review Type: </p>
                            </div>
                            <div class="col-sm-8 col-md-4 col-lg-4">
                                <asp:DropDownList ID="drpReviewType" runat="server" AutoPostBack="True"
                                    CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpReviewType_SelectedIndexChanged">
                                    <asp:ListItem Text="Review Booking" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Review Stylist" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div id="dvStylistNames" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-sm-4 col-md-2 col-lg-2">
                                            <p>Select Stylist: </p>
                                        </div>
                                        <div class="col-sm-8 col-md-4 col-lg-4">
                                            <asp:DropDownList ID="drpStylistNames" runat="server" AutoPostBack="true"
                                                CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpStylistNames_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>


                        </div>

                        <!--Section: Date and listbox -->
                        <div class="row">
                            <asp:PlaceHolder ID="datesPick" runat="server">
                                <div class="col-sm-12 col-md-6 col-lg-6">
                                    <div class="row">
                                        <div class="col-sm-4 col-md-4 col-lg-4">
                                            <p>Select Date: </p>
                                        </div>
                                        <div class="col-sm-8 col-md-3 col-lg-3">
                                            <asp:DropDownList ID="drpMonth" runat="server" AutoPostBack="True"
                                                CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 col-md-3 col-lg-3"></div>
                                        <div class="col-sm-8 col-md-3 col-lg-3">
                                            <asp:Calendar ID="calDay" runat="server" Height="100" Width="200"
                                                OnSelectionChanged="calDay_SelectionChanged"
                                                OnDayRender="cal_DayRender"></asp:Calendar>
                                        </div>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                            <br />
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-12">
                                        <h6 style="text-decoration: underline;" id="choose" runat="server" visible="false">Choose a booking to review</h6>
                                        <p id="lsBksHeader" runat="server"></p>
                                        <asp:Table ID="tblBookings" runat="server"></asp:Table>
                                    </div>

                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <asp:PlaceHolder ID="theReview" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-12">
                                                    <h4 id="rvBoxHeader" runat="server">Write a review and rate the service</h4>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:Label ID="lblBookingID" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblStylistID" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <!--Star rating -->
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                    <asp:UpdatePanel ID="ratingUpdatePanel" runat="server">
                                                        <ContentTemplate>
                                                            <ajaxToolkit:Rating ID="Rating1" runat="server"
                                                                StarCssClass="starRating"
                                                                FilledStarCssClass="filledStar"
                                                                EmptyStarCssClass="emptyStar"
                                                                WaitingStarCssClass="waitingStar">
                                                            </ajaxToolkit:Rating>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>
                                            <!--Comment-->
                                            <div class="row">
                                                <div class="col-12">
                                                    <textarea id="reviewComment" class="form-control" cols="50" rows="5" runat="server" placeholder="Your review here...."></textarea>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12 col-md-3 col-lg-3">
                                                    <asp:Button ID="btnPostReview" CssClass="btn btn-primary" runat="server" Text="Post Review" OnClick="btnPostReview_Click" />
                                                </div>
                                                <br />
                                                <br />
                                                <div class="col-sm-12 col-md-3 col-lg-3">
                                                    <a class="btn btn-primary" href="../Cheveux/Reviews.aspx?Action=MakeAreview" id="btnCancel" runat="server">Cancel Review</a>
                                                </div>
                                            </div>

                                        </asp:PlaceHolder>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <br />

                    </asp:PlaceHolder>






                </asp:PlaceHolder>
            </div>
        </div>
    </form>
</asp:Content>
