<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="GlimpseDataBinding.Demo" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Text="some text" />
        <asp:Button ID="Button1" runat="server" Text="Search" />
        <h3>ListView bound with an ObjectDataSource:</h3>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1">
            <LayoutTemplate>
                <ul>
                    <li runat="server" id="itemPlaceholder" />
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li><%# Eval("Id") %></li>
            </ItemTemplate>
        </asp:ListView>
        <br />
        <h3>DropDownList bound to a function call:</h3>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"  SelectMethod="GetItems" TypeName="GlimpseDataBinding.Demo">
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBox1" Name="filter" />
                <asp:QueryStringParameter QueryStringField="sort" Name="order" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
