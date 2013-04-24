<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSerie.ascx.cs" Inherits="CSerie" %>
<%@ Reference Page="~/Series.aspx" %>
<table style="margin: 2px 1px 2px 1px; vertical-align: middle; width: 99%; text-align: left;
    border-style: outset; background-color: #99CCFF" cellpadding="0" 
    cellspacing="0">
    <tr>
        <td>
            &nbsp;<asp:HyperLink ID="hlNombre" runat="server" Height="17px" OnClick="lbNombre_Click"
                ForeColor="Black">HyperLink</asp:HyperLink>
            <asp:TextBox ID="tbnumcap" runat="server" Width="23px" 
                OnTextChanged="numericNumCap_ValueChanged" AutoPostBack="True"></asp:TextBox>
            <asp:ImageButton ID="btnmas" runat="server" Height="17px" ImageUrl="~/images/mas.gif"
                OnClick="btnmas_Click" Width="17px" />
            <asp:ImageButton ID="btnmenos" runat="server" Height="17px" ImageUrl="~/images/menos.gif"
                OnClick="btnmenos_Click" Width="17px" />
            <asp:ImageButton ID="btnModificar" runat="server" Height="17px" ImageUrl="~/images/spin2.GIF"
                OnClick="btnModificar_Click" Width="17px" />
            <asp:ImageButton ID="btnEliminar" runat="server" Height="17px" ImageUrl="~/images/delete2.GIF"
                OnClick="btnEliminar_Click" Width="17px" />
        </td>
    </tr>
</table>
