<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CheveuxManager.Master" AutoEventWireup="true" CodeBehind="ReportTwo.aspx.cs" Inherits="Cheveux.Manager.ReportTwo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chart</title>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,5"/>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <%--<link href="../../css/StyleSheetMaster.css" rel="stylesheet" />--%>
    <link href="../../css/helpStyle.css" rel="stylesheet" />
</head>
<body class ="container center">
<div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <h4 class="navbar-text">WatchDog</h4>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-left">
                    <li><a href ="../Home/Home.aspx">Home</a></li>   
                    <li><a href ="../Profile/ProfileTemp.aspx">Prof</a></li>
                    <li><a href ="../Incident/Incident.aspx">Upload Incident</a></li>
                  
                    <li><a href ="ReportsTwo.aspx">Reports</a></li>
                    <li><a href ="../Help/Help.aspx">Help</a></li>
                    <li><a href ="../Register/UpdateProfile.aspx">Profile</a></li>
                </ul>
                <ul class="nav navbar-nav navbar-right  ">
                   <%-- <li class="navbar-text"><asp:Label runat="server" id="userName"></asp:Label></li>--%>
                    <li><a href ="../Login/Login.aspx">Log Out</a></li>
                </ul>
                
            </div>
        </div>

    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
     <br />
    <h2 class="text-center">Reports</h2>
    <form runat="server">  
                <asp:TextBox ID="txtDate" runat="server" Width="220px" TextMode="date" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>

   <table>
    <tr>
        <td>
        </td>
        <td> 
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
        </td>
        </tr>
 
</table>
        <div>
          <div>
        <asp:Chart ID="Chart1" runat="server" Width="650px" Height="600px">
           <Titles>
               <asp:Title Text="Top Product " Font="arial, 18pt"> </asp:Title>
           </Titles>
            <series>
                <asp:Series Name="Series1" XValueMember="incidentName" YValueMembers="TotalIncidents" Color="Purple">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
              </div>
         </div>
        <br />
        <br />
        <asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClick="btnExport_Click" />
    </form>
</body>
</html>

