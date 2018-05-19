<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Cheveux.Search" %>

<asp:content id="Content1" contentplaceholderid="PageTitle" runat="server">
    Cheveux - Search
</asp:content>
<asp:content id="Content2" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="container">
<div class="jumbotron">
  <h1>Search</h1>
    <!--Search Box-->
    <asp:Label ID="JumbotronSearchBox" runat="server" Font-Bold="true"></asp:Label>
    <!--New Line-->
    <br /><br />

  <div class="form-group">
                        <!--Search Button & Text Box-->
                        <input id="serchTerm" type="text" placeholder="Search">
                        <a class="btn btn-default" href="javascript:search()">Search</a>
                    </div>
        <!-- Search Function-->
<script>
    function search() {
        window.location.href = "Search.aspx?ST=" + document.getElementById('serchTerm').value;
    }
    </script>
</div>
        <!--Search Results-->
        <div class="row">
                <div class="col-xs-12 col-md-12">
                    <asp:Label runat="server" ID="ResultsLable"></asp:Label>
                        <asp:Table id="SearchResults" runat="server"></asp:Table>
                    </div>
            </div>
    </div>
</asp:content>
