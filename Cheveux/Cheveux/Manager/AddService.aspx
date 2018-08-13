<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="AddService.aspx.cs" Inherits="Cheveux.Manager.AddService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-secondary text-white" id="Div1">
        <!-- Top Margin & Nav Bar Back Color -->
        <br />
        <br />
        <br />
    </div>
    <br />
    <div class="row">
        <div class="col-md-2 col-sm-1"></div>
        <div class="col-md-8 col-sm-10">
            <form id="AddService" runat="server">
                <div class="jumbotron bg-dark text-white">
                    <br />
                    <div class="row">
                        <div class="col-12">
                            <header>
                                <h1>Add Service</h1>
                            </header>
                            <br />
                            <div class="container" runat="server" id="divServiceDetails" visible="true">
                                <asp:Table runat="server" ID="tblServiceDetails">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Service Type"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList runat="server" ID="drpType" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Name: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    </asp:Table>
                                <div class="container" runat="server" id="divBraidDetails" visible="false">
                                <asp:Table runat="server" ID="tblBraidDetails">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Style: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RadioButtonList runat="server" ID="rblStyle"></asp:RadioButtonList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Width: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RadioButtonList runat="server" ID="rblWidth"></asp:RadioButtonList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Length: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RadioButtonList runat="server" ID="rblLength"></asp:RadioButtonList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                                <asp:Table runat="server" ID="tblServiceDetails2" Visible="true">
                                    <asp:TableRow>
                                        <asp:TableCell Text="No of Slots: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtNoOfSlots"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Price: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtPrice"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Description: "></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox runat="server" ID="txtDescription"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                            
                            <div class="row">

                    <div class="col-2 text-left">

                        <asp:Button class='btn btn-basic' runat="server" ID="btnCancel" Visible="true" Text="Cancel" OnClick="btnCancel_Click"/>

                    </div>

                    <div class="col-8"></div>

                    <div class="col-2 text-right">

                        <asp:Button class='btn btn-primary' runat="server" ID="btnAdd" Visible="true" Text="Add Service" OnClick="btnAdd_Click" />

                    </div>
                </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
