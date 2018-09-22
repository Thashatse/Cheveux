<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cheveux.Master" AutoEventWireup="true" CodeBehind="Reviews.aspx.cs" Inherits="Cheveux.Reviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Reviews - Cheveux 
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
        <div class="col-1"></div>
        <div class="col-10">
            <!-- If user is not logged in-->
            <asp:PlaceHolder id="phLogin" runat="server">
                    <div class="jumbotron bg-dark text-white" runat="server" id="LoggedOut">
                        <div class="row">
                            <div class="col-lg-9 col-md-12 col-sm-12">
                                <h1>Write and Read Reviews</h1>
                                <p>Please Login To Proceed</p>
                            </div>
                            <div class="col-lg-3 col-md-2 col-sm-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=Diary.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
            </asp:PlaceHolder>

            <!-- If user is logged in -->
            <asp:PlaceHolder id="phMain" runat="server">
                <!--jumbotron page heading-->
                <div class="jumbotron bg-dark text-white" runat="server" id="LoggedIn">
                    <asp:Label ID="OpeningHeader" CssClass="text-center" runat="server" Text="Behind every review is an experience that matters" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                </div>

                <!--Users reading reviews -->
                <asp:PlaceHolder ID="readReviews" runat="server"></asp:PlaceHolder>
                
                <!--Users writing reviews -->
                    <!--Review Booking -->
                    <!--Review Stylist -->

                <!--Users viewing their own reviews-->

            </asp:PlaceHolder>

        </div>
    </div>

</asp:Content>
