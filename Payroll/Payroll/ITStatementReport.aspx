<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITStatementReport.aspx.cs"
    Inherits="Payroll_Payroll_ITStatementReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT Statement</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:95%;text-align:right;"><asp:Label ID="lblDate" runat="server" Text=""></asp:Label></div>
    
    <div style="width:95%;font-family:Arial;font-size:16px;text-align:center;font-weight:bold;margin-bottom:20px;">
        TO WHOM IT MAY CONCERN
    </div>
    <div style="width:95%;font-family:Arial;font-size:14px;text-align:justify;">
     This is to certify that &nbsp;<asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>,
        TIN No.&nbsp;<asp:Label ID="lblTIN" runat="server" Text=""></asp:Label> &nbsp; is an employee 
     of this Organisation has drawn a total salary and allowances during the fiscal year ended on <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>&nbsp; as follows:- 
     <br />
    </div>
    <div style="width:99%;font-family:Arial;font-size:14px;">
      <div style="margin-left:80px;">
      
        <asp:GridView ID="grPayroll" runat="server" AutoGenerateColumns="False" 
                              EmptyDataText="No Record Found" Font-Size="14px" DataKeyNames=""  Font-Names="Arial"
                              Width="50%" ShowHeader="false" ShowFooter="true" GridLines="None">
                          <RowStyle Font-Size="13px" Font-Names="Arial" />
                          <FooterStyle Font-Bold="true" Font-Size="13px" Font-Names="Arial" Height="30px"/>
                          <Columns>
                              <asp:BoundField DataField="HEADNAME" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="68%"  HorizontalAlign="Left" Height="25px"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="" HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="2%"  HorizontalAlign="Right" Height="25px"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="PAYAMT" HeaderText="Amount" HeaderStyle-HorizontalAlign="right">
                                <ItemStyle Width="25%" HorizontalAlign="right" Height="25px"/>
                              </asp:BoundField>
                          </Columns>
        </asp:GridView>
      </div>  
      &nbsp;<asp:Label ID="lblPayrollInWord" runat="server" Width="793px"></asp:Label> 
    </div>
    <div style="width:95%;font-family:Arial;font-size:14px;text-align:justify;margin-top:10px;">
     Also certified that as per the Income Tax Ordinance 1984, that we have deducted advance Tax at source and deposited into 
     Bangladesh Bank in &nbsp;<asp:Label ID="lblGender" runat="server" Text=""></asp:Label>&nbsp;favor in accordance with the following Challan # during the said fiscal year:-
     
    <%-- During the income year amount of Taka &nbsp;<asp:Label ID="lblTotalChallanAmt" runat="server" Text=""></asp:Label>&nbsp;(
     <asp:Label ID="lblChallanInWord" runat="server" Text=""></asp:Label>)&nbsp; was deducted from 
    </asp:Label>&nbsp;salary as tax and that amount was duly deposited into
     <asp:Label ID="lblChallanBankName" runat="server" Text=""></asp:Label>.
     Details of the deposit are given below:--%>
     <br />
    </div>
    <div style="width:99%;font-family:Arial;font-size:14px;margin-top:10px;">
      <div style="margin-left:80px;">
        <asp:GridView ID="grChallan" runat="server" AutoGenerateColumns="False" 
                              EmptyDataText="No Record Found" Font-Size="14px" DataKeyNames="VYEAR"  Font-Names="Arial"
                              Width="70%" ShowHeader="true" ShowFooter="true" GridLines="None">
                          <RowStyle Font-Size="13px" Font-Names="Arial" />
                          <FooterStyle Font-Bold="true" Font-Size="13px" Font-Names="Arial" Height="30px" />
                          <Columns>
                              <asp:BoundField DataField="CHALLANNO" HeaderText="Deposited Challan #" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Underline="true">
                                <ItemStyle Width="25%"  HorizontalAlign="Left" Height="25px"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="CHALLANDATE" HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Underline="true">
                                <ItemStyle Width="25%"  HorizontalAlign="Left" Height="25px"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="VMONTH" HeaderText="Month" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Underline="true">
                                <ItemStyle Width="23%"  HorizontalAlign="Left" Height="25px"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="" HeaderText="" HeaderStyle-HorizontalAlign="right" HeaderStyle-Font-Underline="true">
                                <ItemStyle Width="2%"  HorizontalAlign="right" Height="25px"/>
                              </asp:BoundField>
                              <asp:BoundField DataField="PAYAMT" HeaderText="Amount" HeaderStyle-HorizontalAlign="right" HeaderStyle-Font-Underline="true">
                                <ItemStyle Width="25%" HorizontalAlign="right" Height="25px"/>
                              </asp:BoundField>
                          </Columns>
        </asp:GridView>
                <br />
                <br />
                 <div style="width:90%;font-family:Arial;font-size:14px;margin-top:10px;">
                <hr />
                This is a computer generated IT Statement; descrepancies (if any) should be brought
                to the notice of the Payroll Administrator (payadmin@bd.care.org), CBHQ.
                </div>
      </div>
    </div>
    </form>
</body>
</html>