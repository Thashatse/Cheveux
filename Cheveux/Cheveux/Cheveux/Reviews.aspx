<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="Cheveux.Reviews" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reviews - Cheveux 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .starRating {
            width: 25px;
            height: auto;
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

        .dvbox {
            /*border: 1px solid #ddd;
            border-radius: 0;
            padding: 5px;*/
            /*height: 400px;*/
            height: auto;
            /*overflow-y: scroll;*/
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
                    </div>


                    <!--for general errors or telling user what must happen next-->
                    <div class="row">
                        <div class="col-sm-12 col-md-12 alert alert-success alert-dismissible" id="sucNoti" visible="false" runat="server">
                            <asp:Label ID="lblSucNoti" runat="server" Text=" "></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" runat="server">&times;</a>
                        </div>
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible" id="erNoti" visible="false" runat="server">
                            <asp:Label ID="lblErNoti" runat="server" Text=" "></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" runat="server">&times;</a>
                        </div>
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible" id="erReview" visible="false" runat="server">
                            <asp:Label ID="lblErReview" runat="server" Text=" "></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close" runat="server">&times;</a>
                        </div>
                    </div>

                    <!--Users reading reviews -->
                    <asp:PlaceHolder ID="readReviews" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-sm-4 col-md-2 col-lg-2">
                                <p>View Reviews: </p>
                            </div>
                            <div class="col-sm-8 col-md-4 col-lg-8">
                                <asp:DropDownList ID="drpReadType" runat="server" AutoPostBack="True" Visible="false"
                                    CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpReadType_SelectedIndexChanged">
                                    <asp:ListItem Text="All Reviews" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="My Reviews" Value="1"></asp:ListItem>
                                </asp:DropDownList>&nbsp;
                                <asp:DropDownList ID="drpRev" runat="server" AutoPostBack="True" Visible="false"
                                    CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpRev_SelectedIndexChanged">
                                    <asp:ListItem Text="Stylist Reviews" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Booking Reviews" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div id="names" runat="server">
                            <div class="row">
                                <div class="col-sm-4 col-md-2 col-lg-2">
                                    <p>Select Stylist: </p>
                                </div>
                                <div class="col-sm-8 col-md-4 col-lg-4">
                                    &nbsp;<asp:DropDownList ID="drpsNames" runat="server" AutoPostBack="True"
                                        CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="drpsNames_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>


                        <!--Stylist Reviews-->
                        <div class="row">
                            <div class="col-12">
                                <asp:Panel ID="stylistPanel" CssClass="dvbox" runat="server" Visible="false">
                                    <asp:Table ID="tblViewEmployee" runat="server"></asp:Table>
                                    <br />
                                    <h3>Reviews for stylist</h3>
                                    <asp:Table ID="tblStylistReviews" runat="server"></asp:Table>
                                </asp:Panel>
                            </div>
                        </div>

                        <!--Booking Reviews-->
                        <div class="row">
                            <div class="col-12">
                                <asp:Panel ID="bookingsPanel" CssClass="dvbox" runat="server" Visible="false">
                                </asp:Panel>
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
                        <br />
                        <!--Section: Booking user would like to review-->
                        <div class="row">
                            <!--bookings-->
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-6 col-lg-6">
                                        <p id="choose" runat="server">Choose a booking to review</p>
                                    </div>
                                    <div class="col-sm-12 col-md-12 col-lg-12">
                                        <asp:Table ID="tblBookings" runat="server"></asp:Table>
                                    </div>
                                </div>
                            </div>

                            <!--write review area-->
                            <div class="col-sm-12 col-md-6 col-lg-6">
                                <div class="row">
                                    <div class="col-sm-12 col-md-12 col-lg-12">
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
                                                    <asp:Label ID="lblReviewID" runat="server" Text="" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <!--Star rating -->
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                    <asp:UpdatePanel ID="ratingUpdatePanel" runat="server">
                                                        <ContentTemplate>
                                                            <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="RatingBehavior1"
                                                                StarCssClass="starRating"
                                                                FilledStarCssClass="filledStar"
                                                                EmptyStarCssClass="emptyStar"
                                                                WaitingStarCssClass="waitingStar">
                                                            </ajaxToolkit:Rating>
                                                        </ContentTemplate>
                                                        
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Label ID="valRating" CssClass="text-center" runat="server" Text="*Please select rating" ForeColor="Red" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <!--Comment-->
                                            <div class="row">
                                                <div class="col-12">
                                                    <textarea id="reviewComment" class="form-control" cols="50" rows="5" runat="server" placeholder="Your review here...."></textarea>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:Label ID="valComment" CssClass="text-center" runat="server" Text="*Please enter a short review of your experience" ForeColor="Red" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12 col-md-3 col-lg-3">
                                                    <asp:Button ID="btnPostReview" CssClass="btn btn-primary" runat="server" Text="Post Review" OnClick="btnPostReview_Click" /><asp:Button ID="btnUpdateReview" CssClass="btn btn-primary" runat="server" Text="Update Review" OnClick="btnUpdateReview_Click" Visible="false" />
                                                </div>
                                                <br />
                                                <br />
                                                <div class="col-sm-12 col-md-1 col-lg-1"></div>
                                                <div class="col-sm-12 col-md-3 col-lg-3">
                                                    <a class="btn btn-secondary" href="../Cheveux/Reviews.aspx?Action=MakeAreview" id="btnCancel" runat="server">Cancel Review</a>
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
