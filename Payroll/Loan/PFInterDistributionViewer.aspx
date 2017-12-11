<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PFInterDistributionViewer.aspx.cs" Inherits="Payroll_Loan_PFInterDistributionViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PF Interest Distribution Report</title>
    <script language="javascript" type="text/javascript">
         function ToUpper(ctrl)
         {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
         }
        function printDiv(divName) 
        {
             var printContents = document.getElementById(divName).innerHTML;
             var originalContents = document.body.innerHTML;

             document.body.innerHTML = printContents;

             window.print();

             document.body.innerHTML = originalContents;
        }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:Gray;text-align:center;width:100%;">
            <asp:Button ID="btnPrint" runat="server" Text="Print" onclientclick="printDiv('PrintMe')" Width="200px" Height="30px" OnClick="btnPrint_Click"/>
            <asp:Button ID="btnExport" runat="server" Font-Bold="true" Height="30px"
                  Text="Export to Excel" CausesValidation="false" OnClick="btnExport_Click" />
    </div>
    <div id="PrintMe" style="width:100%;margin-top:5px;">
        <div style="font-size:14px;text-align:left;font-weight:bold;">
            PF Interest Distribution For <asp:Label ID="lblFisYr" runat="server" Text=""></asp:Label>
        </div>
        <asp:GridView id="grPFInterDis" runat="server"  EmptyDataText="No Record Found" Font-Size="10px" Width="100%" AutoGenerateColumns="False"
                            DataKeyNames="EMPID" ShowFooter="true">
            <HeaderStyle BackColor="Gray" Font-Bold="True"></HeaderStyle>
            <Columns>
            <asp:BoundField DataField="" HeaderText="SL">
              <ItemStyle Width="2%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EmpId" HeaderText="Emp Code">
              <ItemStyle Width="8%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="FullName" HeaderText="Name of Employee">
              <ItemStyle Width="15%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="CMPFCARE" HeaderText="Opening Contribution">
              <ItemStyle Width="15%"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="CUPFINTREST" HeaderText="Opening Interest">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="CMTOTAL" HeaderText="Current Yr. Contribution">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="MIDTERMCMPFINTREST" HeaderText="Mid Term Interest">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="CMPFINTREST" HeaderText="Current Yr. Interest">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
             <asp:BoundField DataField="TOTALBALANCE" HeaderText="Total Balance">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="TOTALPAY" HeaderText="Total Payment">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="NETBALANCE" HeaderText="Balance on June 30">
              <ItemStyle Width="6%"></ItemStyle>
            </asp:BoundField>
            </Columns>
          </asp:GridView>
    </div>
    <div>
        &nbsp;&nbsp;</div>
    </form>
</body>
</html>
