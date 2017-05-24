<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebDemo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebDemo Login</title>
</head>
<body>
    <h2>Your application login page</h2>

    <form id="form1" runat="server">
        <div>
            <label>UserName:</label>
            <asp:textbox ID="userNameTextbox" runat="server"/>
            <br/>
            <br/>
            <label>Password:</label>
            <input type="password"/>
            <p>No password required for this tiny example.</p>
            <asp:button ID="btn" runat="server" OnClick="btn_Click" Text="Login"/>
            <p>
                <asp:RequiredFieldValidator
                    runat="server"
                    Text="UserName is required!"
                    ControlToValidate="userNameTextbox" />
            </p>
        </div>
    </form>
</body>
</html>
