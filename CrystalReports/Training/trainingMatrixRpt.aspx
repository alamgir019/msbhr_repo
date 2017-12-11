<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="trainingMatrixRpt.aspx.cs" Inherits="CrystalReports_Training_trainingMatrixRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;
            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <div style="text-align: left; width: 100%">
        <asp:Button ID="btnPrintTop" runat="server" Text="Print" Width="100px" OnClientClick="printDiv('PrintMe')" />
        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel File" Width="200px"
            ForeColor="blue" OnClick="btnExportExcel_Click" />
    </div>
    <div style="border: solid 1px #3E506C;">
        <div id="PrintMe" style="width: 99.99%; page-break-before: always; height: 100%;">
            <asp:GridView ID="grPayroll" runat="server" EmptyDataText="No Record Found" Font-Size="9px"
                Width="100%" Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
            </asp:GridView>
        </div>
    </div>
    <div>
        <fieldset>
            <asp:Label ID="Label1" runat="server" style="width:80%; font-size:large;font-weight:bold;padding-left:40%;padding-top:100px;" Text="TRAINING PRIORITIES"></asp:Label>
            <table style="padding-left:40%;">
            <tr><td>1 - Within 3 months of joining</td><td>must to do</td></tr>
            <tr><td>2 - Within 4 to 5 months of joining</td><td>must to do</td></tr>
            <tr><td>3 - Within 6 months</td><td>must to do</td></tr>
            <tr><td>4 - Customized training within 6-12 months</td><td>need to do</td></tr>
            <tr><td>5 - Need based within 6-12 months</td><td>need to do</td></tr>
            <tr><td>6 - Within 10 months</td><td>need to do</td></tr>
            <tr><td>7 - Within 7 months</td><td>need to do</td></tr>
            </table>
        </fieldset>
    </div>
</asp:Content>

