<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportsViewers.aspx.vb" Inherits="WebIsomedik.ReportsViewers" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="450px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1252px">
        <LocalReport ReportPath="Report\DaftarTransaksi.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</form>

