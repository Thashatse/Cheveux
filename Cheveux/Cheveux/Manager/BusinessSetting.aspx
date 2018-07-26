<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="BusinessSetting.aspx.cs" Inherits="Cheveux.BusinessSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Business Settings - Cheveux
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
            <!-- if the user is loged In -->
            <div runat="server" id="LogedIn" visible="false">
                <div class="row">
                    <div class="col-md-5 col-lg-5">
                        <!--Heading-->
                        <asp:Label runat="server" ID="PageHeading"> <h2>Business Setting</h2> </asp:Label>
                        <!-- Line Breake -->
                        <br />
                    </div>
                    <div class="col-md-1 col-lg-1">
                        <a href="../Help/CheveuxHelpCenter.aspx#BusinessSettings" target="_blank"
                            title="Click the link to get help with business settings">
                            <span class="glyphicon">&#63; Help</span></a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <form id="frmBussinessSettings" runat="server">
                            <asp:Table ID="tblBussinesSettings" runat="server">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Vat Rate -->
                                VAT Rate (%):
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="vatRate" runat="server"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="rfvVateRate" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="vatRate" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revVateRate" runat="server" ErrorMessage="Please enter a percentage"
                                            ControlToValidate="vatRate" ForeColor="Red" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,2}(\.\d{1,2})|(100|100\.0|100\.00)?$"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditvatRate" runat="server" Text="Edit" OnClick="btnEnitvatRate_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Vat Registration Number -->
                                VAT Reg No.:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="vatRegNo" runat="server"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="rfvVatRegNo" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="vatRegNo" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revVatRegNo" runat="server" ErrorMessage="Please enter a valid vat registration number"
                                            ControlToValidate="vatRegNo" ForeColor="Red" ValidationExpression="[0-9]+"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditvatRegNo" runat="server" Text="Edit" OnClick="btnEnitvatRegNo_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Address -->
                                Address: 
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="addLineOne" runat="server" Width="300px"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="rfvAddressLine1" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="addLineOne" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableHeaderCell><!-- Address line 2 --></asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="addLineTwo" runat="server" Width="300px"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="rfvAddressLine2" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="addLineTwo" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditadd" runat="server" Text="Edit" OnClick="btnEditadd_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Phone Number -->
                                Phone Number
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="phoneNumber" runat="server"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="rfvAddressLine12" runat="server" ErrorMessage="*Required"
                                            ControlToValidate="phoneNumber" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" ErrorMessage="Please enter a valid RSA phone number"
                                            ControlToValidate="phoneNumber" ForeColor="Red" ValidationExpression="^0[0-9]{9}$"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditPhoneNum" runat="server" Text="Edit" OnClick="btnEditPhoneNum_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                        <!-- Week Day Hours -->
                                Weekday Hours:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="wDStart" runat="server" ValidationGroup="WDH"></asp:TextBox>
                                        - 
                                <asp:TextBox ID="wDEnd" runat="server" ValidationGroup="WDH"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="rfvWDH" runat="server" ErrorMessage="*Start Time Required"
                                            ControlToValidate="wDStart" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revWDH" runat="server" ErrorMessage="Please enter a valid Start Time"
                                            ControlToValidate="wDStart" ForeColor="Red" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*End Time Required"
                                            ControlToValidate="wDEnd" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid End Time"
                                            ControlToValidate="wDEnd" ForeColor="Red" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditWDHours" runat="server" Text="Edit" OnClick="btnEditWDHours_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Weekend Hours -->
                                Weekend Hours:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="wEStart" runat="server"></asp:TextBox>
                                        - 
                                <asp:TextBox ID="wEEnd" runat="server"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Start Time Required"
                                            ControlToValidate="wEStart" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid Start Time"
                                            ControlToValidate="wEStart" ForeColor="Red" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*End Time Required"
                                            ControlToValidate="wEEnd" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter a valid End Time"
                                            ControlToValidate="wEEnd" ForeColor="Red" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditWEHours" runat="server" Text="Edit" OnClick="btnEditWEHours_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Public Holiday Hours -->
                                Public Holiday Hours:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="pHStart" runat="server"></asp:TextBox>
                                        - 
                                <asp:TextBox ID="pHEnd" runat="server"></asp:TextBox>
                                        <!--Validation-->
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Start Time Required"
                                            ControlToValidate="pHStart" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter a valid Start Time"
                                            ControlToValidate="pHStart" ForeColor="Red" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*End Time Required"
                                            ControlToValidate="pHEnd" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Please enter a valid End Time"
                                            ControlToValidate="pHEnd" ForeColor="Red" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditPHHours" runat="server" Text="Edit" OnClick="btnEditPHHours_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                <!-- Logo -->
                                Logo:
                                    </asp:TableHeaderCell>
                                    <asp:TableCell>
                                    <a href='../IMG_0715.png' target="_blank"><img src="../IMG_0715.png" alt="logo" width="300" height="300" /></a>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button class="btn btn-secondary" ID="btnEditLogo" runat="server" Text="Edit" OnClick="btnEditLogo_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </form>
                    </div>
                </div>
            </div>
            <!-- if the user is loged Out -->
            <div runat="server" id="LogedOut">
                <div class="bg-dark text-white jumbotron">
                    <h1>Business Settings</h1>
                    <p>Please log-in to view Business Settings</p>
                    <button type="button" class="btn btn-Primary">
                        <a href="../Authentication/Accounts.aspx?PreviousPage=BusinessSetting.aspx" id="LogedOutButton">Login</a>
                    </button>
                </div>
            </div>
        </div>
        <div class="col-md-2 col-sm-1"></div>
    </div>
</asp:Content>
