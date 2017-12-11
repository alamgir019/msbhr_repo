<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PayrollReportWithLeave.aspx.cs" Inherits="Payroll_Payroll_PayrollReportWithLeave" Title="Payroll Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script language="javascript" type="text/javascript" src="../../JScripts/Confirmation.js"></script>
  <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
  <script language="javascript" type="text/javascript">
  window.onload=SearchByChanged;

        function SearchByChanged()
        {
            var ddlSB = document.getElementById('<%= ddlGeneratefor.ClientID %>');
            var ddlSV=document.getElementById('<%=ddlGenerateValue.ClientID%>');
            var ddlB=document.getElementById('<%=ddlBank.ClientID%>');
            var txtVT=document.getElementById('<%=txtTextValue.ClientID%>');
            var txtlbl=document.getElementById('<%=lblEmpID.ClientID%>');
            
            var myindex  = ddlSB.selectedIndex;
            var SelValue = ddlSB.options[myindex].value;
            
            if(SelValue=="O")
            {   
                ddlSV.disabled=false;
                ddlB.disabled=true;  
                txtVT.disabled =true;      
                txtlbl.disabled =true;               
            }
            if(SelValue=="E")
            {  
                ddlSV.disabled=true;
                ddlB.disabled=true;  
                txtVT.disabled =false;
                txtlbl.disabled =false;          
            }   
           if(SelValue=="A")
            {  
                ddlSV.disabled=true;
                txtVT.disabled =true;
                txtlbl.disabled =true;
                ddlB.disabled=true;          
            }
            if(SelValue=="B")
            {  
                 ddlSV.disabled=true;
                 ddlB.disabled=false;
                 txtVT.disabled =true;      
                 txtlbl.disabled =true;        
            }  
         }
         
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
  
<div class="formStyle" style="width:150%">
    <div id="formhead4">Payroll Report With Leave</div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" Width="500px" Font-Bold="True" ForeColor="Fuchsia"></asp:Label>
    </div>
   <div class="Div950" >
      <fieldset >
        <table>
            <tr>
                <td class="textlevel">
                    Generate For
                </td>
                <td >
                    <asp:DropDownList ID="ddlGeneratefor" runat="server" Width="130px" onchange="SearchByChanged()">
                        <asp:ListItem Value="O">Location Wise</asp:ListItem>
                        <asp:ListItem Value="B">Bank Wise</asp:ListItem>
                        <asp:ListItem Value="E">Employee Wise</asp:ListItem>
                        <asp:ListItem Value="A">All</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="">
                    <asp:DropDownList ID="ddlGenerateValue" runat="server" Width="270px" >
                    </asp:DropDownList>&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="100px">
                    </asp:DropDownList></td>
                <td class="textlevelauto">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="80px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblEmpID" runat="server" Text="Emp. ID" CssClass="textlevel"></asp:Label>
                </td>
                <td style="font-size: 11px">
                    <asp:TextBox ID="txtTextValue" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox></td>
                <td><asp:DropDownList ID="ddlBank" runat="server" Width="270px" Font-Size="10px">
                </asp:DropDownList></td>
                <td colspan="2">
                    <asp:CheckBox ID="chkBonus" runat="server" Text="Only Bonus"/>
                    </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="font-size: 11px">
                </td>
                <td style="background-color: #ff9933">
                    <asp:Label ID="lblGroup" runat="server" ForeColor="Navy" Text="Select Group" Width="184px"></asp:Label><asp:DropDownList
                        ID="ddlGroup" runat="server" CssClass="textlevel" Width="185px">
                    </asp:DropDownList></td>
                <td colspan="2">
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" Width="184px" OnClick="btnGenerate_Click" /></td>
            </tr>
        </table>
        
      </fieldset>
      <hr style="border:solid 1px #3399FF;" />
      <div style="text-align:left;width:100%">
            <asp:Button ID="btnPrintTop" runat="server" Text="Print" Width="100px" onclientclick="printDiv('PrintMe')" />
            <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel File" Width="200px" ForeColor="blue" OnClick="btnExportExcel_Click"  />
      </div>
      <fieldset >
             <div id="PrintMe" style="width:99.99%;page-break-before: always;height: 100%;">
               <table width="100%" style="border-collapse:collapse;border:solid 1px white;">
                <tr>
                    <td style="width:40%;text-align:left;font-family:Arial;font-size:20px;padding-left:5px;">
                        Marie Stopes
                    </td>
                    <td rowspan="3" style="width:60%;" valign="top">
                        <table width="100%" style="border-collapse:collapse;border:solid 1px DimGray;">
                            <tr>
                                <td style="font-family:Arial;font-size:12px;background-color:#C6D0D3;font-weight:bold;">Prepared By</td>
                                <td style="font-family:Arial;font-size:12px;background-color:#E7C4A4;font-weight:bold;">Reviewed By P&C Manager</td>
                                <td style="font-family:Arial;font-size:12px;background-color:#6AADD3;font-weight:bold;">Reviewed By Assistant Finance Manager</td>
                                <td style="font-family:Arial;font-size:12px;background-color:#99BB7D;font-weight:bold;">Approved By</td>
                                <td style="font-family:Arial;font-size:11px;background-color:#F6A74B;font-weight:bold;">Disbursed By</td>
                            </tr>
                            <tr>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblPreparedBy" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblReviewedBy" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblCheckedBy" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblApprovedBy" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:11px;"><asp:Label ID="lblDisburseBy" runat="server"></asp:Label></td>
                            </tr>
                             <tr>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblPreparedDate" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblReviewDate" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblCheckDate" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:12px;"><asp:Label ID="lblApproveDate" runat="server"></asp:Label></td>
                                <td style="font-family:Arial;font-size:11px;"><asp:Label ID="lblDisburseDate" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width:40%;font-family:Arial;font-size:14px;padding-left:5px;">
                        <asp:Label ID="lblGenerateFor" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                    </td>
                    
                </tr>
                <tr>
                    <td style="font-family:Arial;font-size:14px;padding-left:5px;">
                        <asp:Label ID="lblPayrollMonth" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
              </table>
                <br/>
                <asp:GridView id="grPayroll" runat="server"  EmptyDataText="No Record Found" Font-Size="10px" Width="99%" 
                Font-Names="Arial" AutoGenerateColumns="true" ShowFooter="true"
                            OnRowCommand="grPayslipMst_RowCommand">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <FooterStyle BackColor="#B3CDE4" Font-Bold="True"></FooterStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                </asp:GridView>
                
             </div>
      </fieldset>
     <div style="text-align:left;"><asp:Button ID="btnPrintBottom" runat="server" Width="100px" Text="Print" onclientclick="printDiv('PrintMe')" /></div>
   </div>
</div>
</asp:Content>

