<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxStylist.Master" AutoEventWireup="true" CodeBehind="Stylist.aspx.cs" Inherits="Cheveux.Stylist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Stylist - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">



        <!--jumbotron page heading-->
        <div class="jumbotron">
            <asp:Label ID="Welcome" runat="server" Font-Bold="true" Font-Size="X-Large">
                <h1>Welcome</h1>
                <h2 id="theDate" runat="server"></h2>
            </asp:Label>
            
            <!--Line Break-->
                <div class="container">
                    <br />
                </div>
        </div>
       

        <div id="Agenda">
            <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:table runat="server" ID="AgendaTable"></asp:table>
                </div>
            </div>
        </div>


        
        <!--Line Break-->
                <div class="container">
                    <br />
                </div>


    </div>

</asp:Content>
