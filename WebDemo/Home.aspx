<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebDemo.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebDemo Home</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Home Page</h2>

            <p>Welcome~ <label id="userNameLabel" runat="server"></label></p>

            <asp:button ID="linkBtn" runat="server" OnClick="linkBtn_Click" Text="Link Fangcloud Account"/>
            <br/>
            <br/>
            <asp:button ID="logoutBtn" runat="server" OnClick="logoutBtn_Click" Text="Logout" />

            <br/>
            <br/>
            <label id="userInfoLabel" hidden="hidden" runat="server"> </label>
        </div>
    </form>
</body>
</html>
