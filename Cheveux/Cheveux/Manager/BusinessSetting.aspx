<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="BusinessSetting.aspx.cs" Inherits="Cheveux.BusinessSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Business Settings - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="frmSettings" runat="server">
        <div class="bg-secondary text-white" id="Div1">
            <!-- Top Margin & Nav Bar Back Color -->
            <br />
            <br />
        </div>
        <br />
        <div class="row">
            <div class="col-1"></div>
            <div class="col-10">
                <!-- if the user is loged In -->
                <div runat="server" id="LogedIn" visible="false">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <div id="divTabs" runat="server">
                                <!--Tabs-->
                                <ul class="nav nav-tabs" role="tablist">
                                    <li>
                                        <asp:Button ID="btnViewFeaturedItems" runat="server" Text="Featured Items" class="btn btn-light" OnClick="btnViewFeaturedItems_Click" /></li>
                                    <li>
                                        <asp:Button ID="btnViewBS" runat="server" Text="Business Settings" class="btn btn-light" OnClick="btnViewBS_Click" /></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- Line Break -->
                    <br />

                    <!--Tabs Content-->
                    <div class="tab-content">
                        <!-- Business Settings -->
                        <div runat="server" visible="false" id="BS">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="jumbotron bg-dark text-white">
                                        <!--Heading-->
                                        <asp:Label runat="server" ID="PageHeading"> <h2>Business Settings</h2> </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:Table ID="tblBussinesSettings" runat="server">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell Width="250">
                                <!-- Vat Rate -->
                                VAT Rate (%):
                                            </asp:TableHeaderCell>
                                            <asp:TableCell Width="500">
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
                                </div>
                            </div>
                        </div>

                        <!-- Featured Items -->
                        <div runat="server" visible="false" id="FI">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="jumbotron bg-dark text-white">
                                        <div class="row">
                                            <div class="col-lg-9 col-md-12 col-sm-12">
                                                <!--Heading-->
                                                <h2>Featured Items</h2>
                                            </div>
                                            <div class="col-lg-3 col-md-2 col-sm-2">
                                                <asp:Button ID="btnViewHint" runat="server" Text="Hint" class="btn btn-light" OnClick="btnViewHint_Click" />
                                            </div>
                                        </div>
                                        <div runat="server" id="SettingsHint" visible="false">
                                            <!-- Line Break -->
                                            <br />
                                            <p>Featured items are the Services, Stylist and Products you would like to highlight. </p>
                                            <p>They will appear on the homepage and at the top of the Service, Stylist & Products Pages.</p>
                                            <p>Select an item to begin the edit.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h1>Services:</h1>
                            <!-- Line Break -->
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService1" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService2" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService3" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService4" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <!-- Line Break -->
                            <br />
                            <br />
                            <h1>Products:</h1>
                            <!-- Line Break -->
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedProduct1" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedProduct2" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedProduct3" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <!-- Line Break -->
                            <br />
                            <br />
                            <h1>Stylists:</h1>
                            <!-- Line Break -->
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedStylist1" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedStylist2" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedStylist3" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <!-- Line Break -->
                            <br />
                            <br />
                            <h1>Contact Us:</h1>
                            <!-- Line Break -->
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedPhone" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedEmail" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div runat="server" visible="false" id="DivEditFeaturedItem">
                    <!-- Line Break -->
                            <br />
                            <h1><asp:Label ID="LblFeatureEditHeading" runat="server" Text=" Label"></asp:Label></h1>
                            <div class="row">
                                <div class="col-md-6">
                                    <!--Line Break-->
                                    <br />
                                    <h3 style="text-align: left; float: left;">
                                        <asp:Label ID="lblListBoxHeader" runat="server" Text="Label"></asp:Label>: </h3>
                                    <p style="text-align: right; float: right;">
                                        <!-- Search -->
                                        <asp:TextBox ID="txtSearchItems" runat="server" AutoPostBack="true" placeholder="search" CssClass="form-control"></asp:TextBox>
                                    </p>
                                    <!--Line Break-->
                                    <br />
                                    <asp:ListBox runat="server" ID="lblFeatuerdItems" CssClass="form-control" DataTextField="Item" DataValueField="ID"
                                        Height="500"></asp:ListBox>
                                </div>
                                <div class="col-md-6 text-center">
                                    <!--Line Break-->
                                    <br />
                                    <br />
                                    <!--EditImage-->
                                    <img src="/Theam/img/edit-pencil-icon-vector-13483315.jpg" alt="Edit" width="400"
                                        height="400" class="img-fluid" />
                                    <!--Line Break-->
                                    <br />
                                    <br />
                                    <asp:Button ID="btnDoneFatureEdit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnDoneFatureEdit_Click" />
                                </div>
                            </div>
                            <!-- Line Break -->
                            <br />
                        </div>
                    </div>
                </div>
                <!-- if the user is loged Out -->
                <div runat="server" id="LogedOut">
                    <div class="bg-dark text-white jumbotron">
                        <div class="row">
                            <div class="col-10">
                                <h1>Settings</h1>
                                <p>Please log-in to view Settings</p>
                            </div>
                            <div class="col-2">
                                <a class="btn btn-primary" href="../Authentication/Accounts.aspx?PreviousPage=BusinessSetting.aspx" id="LogedOutButton">Login</a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Line Break -->
                <br />
                <br />
            </div>
            <div class="col-1"></div>
        </div>
    </form>
</asp:Content>
