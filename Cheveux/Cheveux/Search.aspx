<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Cheveux.Search" %>

<asp:content id="Content1" contentplaceholderid="PageTitle" runat="server">
    Search - Cheveux
</asp:content>
<asp:content id="Content2" contentplaceholderid="head" runat="server">
    <script>
        function searchOnSearchPage() {
            window.location.href = "Search.aspx?ST=" + document.getElementById('serchTermOnSearchPage').value;
        }
    </script>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="container">
<div class="jumbotron bg-dark text-white">
  <h1>Search</h1>
    <!--Search Box-->
    <asp:Label ID="JumbotronSearchBox" runat="server" Font-Bold="true"></asp:Label>
    <!--New Line-->
    <br /><br />

  <div class="form-group">
      <!--Search Button & Text Box-->
                        <input id="serchTermOnSearchPage" type="text" placeholder="Search">
                        <a class="btn btn-primary" href="javascript:searchOnSearchPage()">Search</a></div>
</div>
        <!--Search Results-->
        <div class="row">
                <div class="col-xs-12 col-md-12">
                    <!--Services-->
                                        <asp:Label runat="server" ID="serviceResultsLable"></asp:Label>
                        <asp:Table id="serviceSearchResults" runat="server"></asp:Table>
                    <!--New Line-->
    <br /><br />
                    <!--Products-->
                                        <asp:Label runat="server" ID="ProductResultsLable"></asp:Label>
                        <asp:Table id="ProductSearchResults" runat="server"></asp:Table>
                                                            <!--New Line-->
    <br /><br />
                                        <!--Stylist-->
                    <asp:Label runat="server" ID="StylistResultsLable"></asp:Label>
                    <asp:Table id="StylistSearchResults" runat="server"></asp:Table>
                    </div>
            </div>
    </div>
</asp:content>
