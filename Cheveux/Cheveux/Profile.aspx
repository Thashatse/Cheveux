<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Cheveux.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
</head>
<%--  --%>
<body>
    <form id="form1" runat="server">
        <!--Tabs-->
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <!--Tabs-->
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active"><a href="#Details" role="tab" data-toggle="tab">Details</a></li>
                        <li><a href="#Upcoming Service(s)" role="tab" data-toggle="tab">Upcoming Service(s)</a></li>
                        <li><a href="#Past Service(s)" role="tab" data-toggle="tab">Past Service(s)</a></li>
                    </ul>

                    <!--Tabs Content-->
                    <div class="tab-content">
                        <div class="active tab-pane" id="Details">
                            <h2>Deatils</h2>
                            <!--Content-->
                        </div>

                        <div class="tab-pane" id="Upcoming Service(s)">
                            <h2>Upcoming Service(s)</h2>
                            <!--Content-->
                        </div>

                        <div class="tab-pane" id="Past Service(s)">
                            <h2>Past Service(s)</h2>
                            <!--Content-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
</body>
</html>
