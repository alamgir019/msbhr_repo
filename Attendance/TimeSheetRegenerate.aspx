<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TimeSheetRegenerate.aspx.cs" Inherits="Attendance_TimeSheetRegenerate"
    Title="TimeSheet Regenerate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js">
        //Date Time Picker script
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 97%; float: left;">
                Time Sheet Re Generate</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div class="iesEmp" style="margin-top: 0px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Fiscal Year :
                        </td>
                        <td class="textlevel">
                            <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="130px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>    
                         <td class="textlevel">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Emp Type:</td>
                        <td class="textlevel">
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="130px" 
                                CssClass="textlevelleft" AutoPostBack="True" 
                                onselectedindexchanged="ddlEmpType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                             Emp ID:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="200px" CssClass="textlevelleft">
                            </asp:DropDownList>
                        </td>
                        <td class="textlevel">
                            &nbsp;</td>
                        <td>
                            <asp:RadioButtonList ID="radEmp" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                ForeColor="Blue" RepeatDirection="Horizontal" AutoPostBack="True" 
                                onselectedindexchanged="radEmp_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="A">Active</asp:ListItem>
                                <asp:ListItem Value="I">Seperated</asp:ListItem>                                
                            </asp:RadioButtonList>
                        </td>    
                         <td class="textlevel">
                            <asp:Button ID="btnView" runat="server" Text="Show" Width="70px" OnClick="btnView_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset style="margin-bottom: 10px;">
                <legend>List</legend>
                <div style="overflow: scroll; width: 100%; height: 250px">
                    <asp:GridView ID="grList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="SalarySourceId,Salary,Bonus,PF,IT,PFLoan,FringePF,Medical,Gratuity">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Emp. Name">
                                <ItemStyle CssClass="ItemStylecss" Width="12%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SalSourceCode" HeaderText="Salary Source Code">
                                <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectCode" HeaderText="Project Code">
                                <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Percentage" HeaderText="Percentage">
                                <ItemStyle CssClass="ItemStylecssRight" Width="8%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VMonth" HeaderText="Month">
                                <ItemStyle CssClass="ItemStylecssRight" Width="6%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VYear" HeaderText="VYear">
                                <ItemStyle CssClass="ItemStylecssRight" Width="5%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="DivCommand1">
                    <div class="DivCommandL">
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnRefresh_Click"
                            CausesValidation="false" />
                    </div>
                    <div class="DivCommandR">
                        <asp:Button ID="btnGenerate" runat="server" Text="Re Generate Time Sheet" 
                            OnClick="btnGenerate_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
