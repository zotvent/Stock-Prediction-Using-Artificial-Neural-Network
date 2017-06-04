<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockManager.aspx.cs" Inherits="WebTest.StockManager" MasterPageFile="Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">

        <asp:FileUpload ID="csvFile" runat="server" />
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ok" />
        </p>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" />
        </p>
</asp:Content>