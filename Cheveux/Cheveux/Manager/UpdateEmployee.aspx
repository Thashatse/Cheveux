<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="UpdateEmployee.aspx.cs" Inherits="Cheveux.Manager.UpdateEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Update Employee - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<form runat="server">
    <div>
        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />

        <asp:PlaceHolder ID="phLogIn" runat="server">
            <div class="container" runat="server" id="LoggedOut" visible="true">
                <div class="jumbotron bg-dark text-white">
                    <h1>Please Log-in to proceed</h1>
                    <button type="button" class="btn btn-default">
                        <a href="../Authentication/Accounts.aspx?PreviousPage=UpdateEmployee.aspx" id="LogedOutButton">Login</a>
                    </button>
                </div>
            </div>
         </asp:PlaceHolder>

        <asp:PlaceHolder runat="server"  ID="phMain">
            <div class="container">
                <div class="row">
                    <div class="col-md-2 col-sm-1"></div>
                    <div class="col-md-12 col-sm-10">
                        <div class="jumbotron bg-dark text-white">
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <header>
                                        <h1 id="updateEmpHeading">
                                            Update Employee Details
                                        </h1>
                                    </header>
                                    <br />
                                </div>
                            </div>
                                <div class="row">
                                    <div class="col-5">
                                        <asp:table id="tblUserImage" runat="server"></asp:table>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!--Error updating user message-->
                <div class="container row" runat="server">
                <asp:PlaceHolder ID="phUpdateErr" runat="server" Visible="false">
                        <div class="col-sm-12 col-md-12 alert alert-danger alert-dismissible">
                            <asp:Label ID="lblUpdateErr" runat="server" Text="Label"></asp:Label>
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        </div>
                </asp:PlaceHolder>
                </div>

            <div id="empDetails" class="container">
                <div class="row">
                    <div class="col-md-12 form-group">
                        <asp:label id="lblAddLine1" runat="server" text="Address Line 1"></asp:label>&nbsp; &nbsp; &nbsp; 
                        <asp:textbox id="txtAddLine1" runat="server" placeholder="Address Line 1"></asp:textbox>
                        <!-- AddressLine 1 Validation-->
                        <asp:RequiredFieldValidator ID="ad1Validation" runat="server" ErrorMessage="*Required" ForeColor="Red" ControlToValidate="txtAddLine1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 form-group">
                        <asp:label id="lblAddLine2" runat="server" text="Address Line 2"></asp:label>&nbsp; &nbsp; &nbsp; 
                        <asp:textbox id="txtAddLine2" runat="server" placeholder="Address Line 2"></asp:textbox>
                        <!-- AddressLine 2 Validation-->
                        <asp:RequiredFieldValidator ID="ad2Validation" runat="server" ErrorMessage="*Required" ForeColor="Red" ControlToValidate="txtAddLine2"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 form-group">
                        <asp:label id="lblType" runat="server" text="Employee Type"></asp:label>&nbsp; &nbsp; &nbsp; &nbsp; 
                        <asp:radiobuttonlist id="rdoType" runat="server" CellSpacing="5" RepeatDirection="Horizontal" RepeatLayout="flow">
                            <asp:listitem id="TypeOption1" runat="server" Text="Receptionist" value="R"/>
                            <asp:listitem id="TypeOption2" runat="server" Text="Stylist" value="S"/>
                        </asp:radiobuttonlist>
                        <!-- RadioButtons Validation-->
                        <asp:RequiredFieldValidator ID="rdoValidator" runat="server" ErrorMessage="*Please select user type" ForeColor="Red" ControlToValidate="rdoType"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:button id="btnBack" class="btn btn-primary" runat="server" text="Back" OnClick="btnBack_Click" />&nbsp; &nbsp; &nbsp; 
                        <asp:button id="btnUpdate" class="btn btn-primary" runat="server" text="Update" OnClick="btnUpdate_Click" />
                    </div>
                </div>

                

            </div>
        </asp:PlaceHolder>

        <!--Error: If cant display user -->
                <asp:PlaceHolder ID="phUsersErr" runat="server" Visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="jumbotron">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <!-- Error Image-->
                                            <img src="https://cdn4.iconfinder.com/data/icons/smiley-vol-3-2/48/134-512.png" alt="Error" width="100" height="100"></img>
                                        </div>
                                        <div class="col-md-10">
                                            <!--Error details placehoders-->
                                            <asp:Label ID="errorHeader" runat="server"></asp:Label>
                                                <br />
                                            <asp:Label ID="errorMessage" runat="server"></asp:Label>
                                            <asp:Label ID="errorToReport" runat="server"></asp:Label>    
                                            </div>
                                    </div>
                                </div>
                             </div>
                         </div>
                    </div>
                </asp:PlaceHolder>

            <br />
            <br />
        </div>
</form>
</asp:Content>
