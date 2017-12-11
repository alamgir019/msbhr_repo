<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="PFInterDistributionReport.aspx.cs" Inherits="Payroll_Loan_PFInterDistributionReport" Title="PF Interest Distribution Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <script language="javascript" type="text/javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
</script>
 
  <div class="departmentSetup">
    <div id ="formhead6">
      <div style="width:88%;float:left;">PF Interest Distribution Report</div>
      <div style=" margin:2px; float:left;padding-left:3px;"><a href="../../File/Home.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
    </div>
    <div class="departmentSetupInner">
      <fieldset>
          <asp:HiddenField ID="hfId" runat="server"/>
          <br />
          <table>
            <tr>
                <td class="textlevel">Fiscal Year</td>
                 <td>
                     <asp:DropDownList ID="ddlYear" runat="server" Width="251px">
                     </asp:DropDownList></td>
            </tr>
              <tr>
                  <td class="textlevel">
                      PF Interest Percent
                  </td>
                  <td>
                      <asp:TextBox ID="txtPFRate" runat="server" Width="80px" CssClass="TextBoxAmt60"></asp:TextBox></td>
              </tr>
              <tr>
                  <td class="textlevel">
                  </td>
                  <td style="text-align: right">
                      <asp:Button ID="btnShowCrystalRpt" runat="server" Font-Underline="False" 
                          Text="Crystal Report" OnClick="btnShowCrystalRpt_Click" />
                      <asp:Button ID="btnShow" runat="server" Font-Underline="False" 
                          Text="HTML Report" OnClick="btnShow_Click" /></td>
              </tr>
          </table>

        <%--<cc1:ModalPopupExtender ID="ModalPopupTree" runat="server" BackgroundCssClass="modalBackground"
      CancelControlID="btnClose" DropShadow="True" DynamicServicePath="" Enabled="True"
      OkControlID="btnOk" OnOkScript="onOk()" PopupControlID="PanelTree" TargetControlID="btnShowTree">
      </cc1:ModalPopupExtender>--%>
          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
              FilterType="Custom,Numbers" TargetControlID="txtPFRate" ValidChars="1234567890.-">
          </cc1:FilteredTextBoxExtender>
      </fieldset>
    </div>
  </div>
</asp:Content>
