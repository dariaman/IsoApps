<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Download.aspx.vb" Inherits="WebIsomedik.Download" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FrameContent" runat="server">
   
    <%--<asp:Button ID="Button1" runat="server" Text="Button" />--%>
    <div id="DivBody">
        <table>
            <tr>
                <td><asp:LinkButton ID="LinkAPK" runat="server"><i class='fa fa-android fa-fw'></i> &nbsp;Download Apk</asp:LinkButton></td>
            </tr>
            <tr>
                <td><asp:LinkButton ID="LinkPanduanPolis" runat="server"><i class='fa fa-book fa-fw'></i> &nbsp;Panduan Polis</asp:LinkButton></td>
            </tr>
        </table>
        
        
        
    </div>

</asp:Content>
