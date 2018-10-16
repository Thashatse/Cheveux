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
    </div>
    <br />
    <div class="row">
        <div class="col-1"></div>
        <div class="col-10">
            <form id="AddService" runat="server">

                <div class="row">
                    <div class="col-12">
                        <asp:PlaceHolder ID="phLogIn" runat="server">
                            <div runat="server" id="LoggedOut" visible="true">
                                <div class="jumbotron bg-dark text-white">
                                    <h1>Add Service</h1>
                                </div>
                            </div>
                        </asp:PlaceHolder>
                        <div runat="server" id="divServiceDetails" visible="true">
                            <asp:Table runat="server" ID="tblServiceDetails">
                                <asp:TableRow>
                                    <asp:TableCell Text="Service Type:" Width="150px" VerticalAlign="Top"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="drpType" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true" CssClass="btn btn-primary dropdown-toggle">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" ID="lblTypeValidation" Text="*Service Type is required" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <div class="container" runat="server" id="divBraidDetails" visible="false">
                                <asp:Table runat="server" ID="tblBraidDetails">
                                    <asp:TableRow>
                                        <asp:TableCell Text="Style: " Width="150px" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RadioButtonList runat="server" ID="rblStyle"></asp:RadioButtonList>
                                            <br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorrblStyle" runat="server"
                                                ErrorMessage="*Braid style is required" ControlToValidate="rblStyle"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Width: " Width="150px" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RadioButtonList runat="server" ID="rblWidth"></asp:RadioButtonList>
                                            <br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorrblWidth" runat="server"
                                                ErrorMessage="*Braid width is required" ControlToValidate="rblWidth"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell Text="Length: " Width="150px" VerticalAlign="Middle"></asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RadioButtonList runat="server" ID="rblLength"></asp:RadioButtonList>
                                            <br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorrblLength" runat="server"
                                                ErrorMessage="*Braid length is required" ControlToValidate="rblLength"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                            <asp:Table runat="server" ID="tblServiceDetails2" Visible="true" VerticalAlign="Top">
                                <asp:TableRow>
                                    <asp:TableCell Text="Name: " Width="150px"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                        <br />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtName" runat="server"
                                            ErrorMessage="*Service name is required" ControlToValidate="txtName"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Text="No of Slots: " Width="150px"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList runat="server" ID="drpNoOfSlots" AutoPostBack="true" CssClass="btn btn-primary dropdown-toggle">
                                            <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Text="Price: " Width="150px"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control"></asp:TextBox>
                                        <br />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtPrice" runat="server"
                                            ErrorMessage="*Service price is required" ControlToValidate="txtPrice"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Text="Description: " Width="150px"></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control"></asp:TextBox>
                                        <br />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDescription" runat="server"
                                            ErrorMessage="*Service name is required" ControlToValidate="txtDescription"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                      <asp:TableCell Text="Image: " Width="150px"></asp:TableCell>
                                      <asp:TableCell>
                                                              <asp:FileUpload ID="flUploadServiceimg" runat="server" CssClass="btn-primary"/><br />
                                      </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Button class='btn btn-basic' runat="server" ID="btnCancel" Visible="true" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-sm-0 col-md-4 col-lg-4"></div>
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <asp:Button class='btn btn-primary' runat="server" ID="btnAdd" Visible="true" Text="Add Service" OnClick="btnAdd_Click" />
                    </div>
                </div>
            </form>
        </div>
        <div class="col-1"></div>
    </div>
</asp:Content>
