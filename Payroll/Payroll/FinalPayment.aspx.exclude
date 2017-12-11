<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="FinalPayment.aspx.cs" Inherits="Payroll_Payroll_FinalPayment" Title="Final Payment" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
         function ToUpper(ctrl)
         {   
            var t = ctrl.value;
            ctrl.value = t.toUpperCase(); 
         }
    </script>
    
    <div class="officeSetup">
        <div id="formhead1">
            Final Payment</div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Emp Id :</td>
                        <td>
                            <asp:TextBox ID="txtEmpId" runat="server" Width="80px" onkeyup="ToUpper(this)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpId"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsFromPayroll" CssClass="textlevelleft" runat="server" Text="Is Partial Salary Through Final Payment" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    UseSubmitBehavior="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Preview" Width="70px" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>
