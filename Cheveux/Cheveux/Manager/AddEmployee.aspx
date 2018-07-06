<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="Cheveux.Manager.AddEmpolyee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Add Employee - Cheveux
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="block">
            <header>
                <h3>Add Employee</h3>
            </header>
            
             <form id="id_AddEmp" runat="server">
                 <div class="form-group">
                    
                    <asp:TextBox ID="userID" runat="server" placeholder="Employee ID"></asp:TextBox>
                     <!--- Create algorithm to generate UserID must be generated --->
                     <asp:Button ID="btnGenUserID" runat="server" class="btn btn-default" Text="Generate" />
                    <br /><br />

                    <asp:TextBox ID="firstName" runat="server" placeholder="First Name"></asp:TextBox>
                    <br /><br />

                    <asp:TextBox ID="lastName" runat="server" placeholder="Last Name"></asp:TextBox>
                    <br /><br />

                    <asp:TextBox ID="userName" runat="server" placeholder="UserName"></asp:TextBox>
                    <br /><br />

                    <asp:TextBox ID="email" runat="server" placeholder="Email Address"></asp:TextBox>
                      or <a href="https://accounts.google.com/SignUpWithoutGmail?hl=en" target="_blank">Create Email Account</a>
                    <br /><br />

                    <asp:TextBox ID="contactNo" runat="server" placeholder="Contact Number"></asp:TextBox>
                    <br /><br />

                     <img src="../Images/userImg.png" alt="Employee Picture Placeholder" id="myImg" height="100" width="100" />
                        <input type="file" runat="server"/>
                    <br /><br />

                    <asp:TextBox ID="addLine1" runat="server" placeholder="Address Line 1"></asp:TextBox>
                    <br /><br />

                    <asp:TextBox ID="addLine2" runat="server" placeholder="Address Line 2"></asp:TextBox>
                    <br /><br />

                    <!--- Type:'R:M:S' --->
                     <fieldset>
                         <legend>Employee Type</legend>
                         <asp:RadioButton ID="rdoManager" runat="server" />
                         <label for="rdoManager">Manager</label>
                         <br />
                         <asp:RadioButton ID="rdoReceptionist" runat="server" />
                         <label for="rdoReceptionist">Receptionist</label>
                         <br />
                         <asp:RadioButton ID="rdoStylist" runat="server" />
                         <label for="rdoStylist">Stylist</label>
                         <br />
                     </fieldset>
                    <br /><br />

                     <asp:Button ID="btnSave" runat="server" class="btn btn-default" Text="Add" />
                </div>
             </form>
        </div>
    </div>

</asp:Content>
