<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="UpdateService.aspx.cs" Inherits="Cheveux.Manager.UpdateService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Update Service - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="UpdateService">

        <div class="bg-secondary text-white" id="divUpdateService">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />

        <asp:PlaceHolder ID="phLogIn" runat="server">
            <div class="container" runat="server" id="LoggedOut" visible="true">
                <div class="jumbotron bg-dark text-white">
                    <h1>Update Service</h1>
                </div>
            </div>
        </asp:PlaceHolder>
        <div runat="server" id="divServiceDetails">
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
                         <asp:DropDownList runat="server" ID="drpNoOfSlots" AutoPostBack="true">
                                                <asp:ListItem Text="1" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="9"></asp:ListItem>
                                            </asp:DropDownList>
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Text="Price: " Width="150px"></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox>
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
        <div runat="server" id="divTableDesc" visible="false">
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

        <div class="row">

            <div class="col-2 text-left">

                <asp:Button class='btn btn-basic' runat="server" ID="btnCancel" Visible="true" Text="Cancel" OnClick="btnCancel_Click" />

            </div>

            <div class="col-8"></div>

            <div class="col-2 text-right">

                <asp:Button class='btn btn-primary' runat="server" ID="btnUpdate" Visible="true" Text="Update" OnClick="btnUpdate_Click" />

            </div>
        </div>

    </form>








































</asp:Content>
