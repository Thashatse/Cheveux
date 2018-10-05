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
                                        <asp:Button ID="btnViewSK" runat="server" Text="Stock Managment" class="btn btn-light" OnClick="btnViewSK_Click1" /></li>
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
                                    <asp:Label ID="lblFeaturedService1" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService2" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService3" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedService4" runat="server" Text="Error"></asp:Label>
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
                                    <asp:Label ID="lblFeaturedProduct1" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedProduct2" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedProduct3" runat="server" Text="Error"></asp:Label>
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
                                    <asp:Label ID="lblFeaturedStylist1" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedStylist2" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedStylist3" runat="server" Text="Error"></asp:Label>
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
                                    <asp:Label ID="lblFeaturedPhone" runat="server" Text="Error"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFeaturedEmail" runat="server" Text="Error"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div runat="server" visible="false" id="DivEditFeaturedItem">
                            <!-- Line Break -->
                            <br />
                            <h1>
                                <asp:Label ID="LblFeatureEditHeading" runat="server" Text=" Label"></asp:Label></h1>
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
                                    <asp:ListBox runat="server" ID="lblFeatuerdItems" CssClass="form-control" Height="500"></asp:ListBox>
                                    <!--Line Break-->
                                    <br />
                                    <asp:Button ID="btnCancelFatureEdit" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancelFatureEdit_Click" />
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
                                    <asp:Label ID="lblErrorFeaturedItem" runat="server" Text="Please Select a Item" ForeColor="Red" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Button ID="btnDoneFatureEdit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnDoneFatureEdit_Click" />
                                </div>
                            </div>
                            <!-- Line Break -->
                            <br />
                        </div>

                        <div runat="server" visible="false" id="SK">
                            <div class="jumbotron bg-dark text-white">
                                <!--Heading-->
                                <asp:Label runat="server" ID="lblManageStockSettins"> <h2>Stock Management</h2> </asp:Label>
                            </div>

                            <br />
                            
                            <div class="row">
                                <div class="col-md-3">
                                    Low Stock Warning:
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" type="number" ID="txtLowStock" CssClass="form-control" Width="100" Text="10" OnTextChanged="showSaveStockSetting" AutoPostBack="true" /><br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    Default Purchase Order Qty:
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" type="number" ID="txtPurchaseQty" CssClass="form-control" Width="100" Text="5" OnTextChanged="showSaveStockSetting" AutoPostBack="true"/><br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    Auto Purchase Order:
                                </div>
                                <div class="col-md-9">
                                    <asp:CheckBox ID="cbAutoLowStockOnOff" runat="server" Text="On/Off" OnCheckedChanged="showSaveStockSetting" AutoPostBack="true" /><br />
                                    <br />
                                </div>
                            </div>
                            <div class="row" id="AutoLowInfo" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-9">
                                    <p>This Allows the system to automatically make purchase order on your behalf </p>
                                    <p>from the supplier based on the ‘Low Stock Warning‘.</p>
                                    <p>The Quantity of the products ordered will be the ‘Default Purchase Order Qty’.</p>
                                    <p>You can set the frequency and the products that are to be automatically ordered.</p>
                                </div>
                            </div>
                            <div id="divAutoStockOrder" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-2">
                                        Frequency:
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlAutoStockOrderFrequency" runat="server" OnSelectedIndexChanged="updateStockManagement" AutoPostBack="true" CssClass="btn btn-primary" >
                                            <asp:ListItem Text ="Daily" Value="Asn"></asp:ListItem>
                                            <asp:ListItem Text ="Weekly" Value="Ewe"></asp:ListItem>
                                            <asp:ListItem Text ="Biweekly" Value="E2w"></asp:ListItem>
                                            <asp:ListItem Text ="Monthly" Value="Emo"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        Next Auto Order: <asp:Label ID="lblNextAutoOrderDate" runat="server" Text=""></asp:Label>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-2">
                                        Products:
                                    </div>
                                    <div class="col-md-9">
                                        <asp:RadioButtonList ID="rblAutoStockOrderProducts" runat="server" OnSelectedIndexChanged="updateStockManagement" AutoPostBack="true">
                                            <asp:ListItem Text ="All" Value="False"></asp:ListItem>
                                            <asp:ListItem Text ="Custom" Value="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row" id="divAutoStockOrderProducts" runat="server" visible="false">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-11">
                                        <div class="row">
                                            <div class="col-md-5">
                                                <!--Line Break-->
                                                <br />
                                                <h3 style="text-align: left; float: left;">
                                                    Products: </h3>
                                                <p style="text-align: right; float: right;">
                                                    <!-- Search -->
                                                    <asp:TextBox ID="TxtSearchProductForAutoLowStock" runat="server" AutoPostBack="true" placeholder="search" CssClass="form-control"></asp:TextBox>
                                                </p>
                                                <!--Line Break-->
                                                <br />
                                                <asp:ListBox runat="server" ID="lblProductsForAutoOrder" CssClass="form-control" Height="300"></asp:ListBox>
                                            </div>
                                            <div class="col-md-2 text-center">
                                                <!--Line Break-->
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                Qty to Order:
                                <asp:DropDownList runat="server" ID="ddlQty" CssClass="btn btn-outline-secondary dropdown-toggle">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="21">21</asp:ListItem>
                                    <asp:ListItem Value="22">22</asp:ListItem>
                                    <asp:ListItem Value="23">23</asp:ListItem>
                                    <asp:ListItem Value="24">24</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="26">26</asp:ListItem>
                                    <asp:ListItem Value="27">27</asp:ListItem>
                                    <asp:ListItem Value="28">28</asp:ListItem>
                                    <asp:ListItem Value="29">29</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="31">31</asp:ListItem>
                                    <asp:ListItem Value="32">32</asp:ListItem>
                                    <asp:ListItem Value="33">33</asp:ListItem>
                                    <asp:ListItem Value="34">34</asp:ListItem>
                                    <asp:ListItem Value="35">35</asp:ListItem>
                                    <asp:ListItem Value="36">36</asp:ListItem>
                                    <asp:ListItem Value="37">37</asp:ListItem>
                                    <asp:ListItem Value="38">38</asp:ListItem>
                                    <asp:ListItem Value="39">39</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="41">41</asp:ListItem>
                                    <asp:ListItem Value="42">42</asp:ListItem>
                                    <asp:ListItem Value="43">43</asp:ListItem>
                                    <asp:ListItem Value="44">44</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                    <asp:ListItem Value="46">46</asp:ListItem>
                                    <asp:ListItem Value="47">47</asp:ListItem>
                                    <asp:ListItem Value="48">48</asp:ListItem>
                                    <asp:ListItem Value="49">49</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Button ID="btnAddProductToAutoOrder" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddProductToAutoOrder_Click" />
                                                <!--Line Break-->
                                                <br />
                                                <br />
                                                <asp:Button ID="btnAddRemoveToAutoOrder" runat="server" Text="Remove" CssClass="btn btn-secondary" OnClick="btnAddRemoveToAutoOrder_Click" />
                                            </div>
                                            <div class="col-md-5">
                                                <!--Line Break-->
                                                <br />
                                                <br />
                                                <h3 style="text-align: left; float: left;">
                                                    Auto Order Products: </h3>
                                                <asp:ListBox runat="server" ID="lblProductsOnAutoOrder" CssClass="form-control" Height="300"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                             <br />

                                <p style="text-align: right; float: right;">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="updateStockManagement" Visible="false" CssClass="btn btn-primary" />
                                    </p>
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
