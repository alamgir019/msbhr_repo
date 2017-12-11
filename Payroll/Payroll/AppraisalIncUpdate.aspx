<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="AppraisalIncUpdate.aspx.cs" Inherits="Appraisal_AppraisalIncUpdate"
    Title="Appraisal Increment Salary Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    

    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="empTrainForm">
        <div id="formhead2">
            <div style="width: 96.6%; float: left;">
                Appraisal Increment Salary Update</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
              <a href="../../Default.aspx">
                    <img src="../../Images/close_icon.gif" /></a></div>
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner" style="height: 550px">
            <div style="background-color: #EFF3FB; margin-bottom: 10px">
                <fieldset>
                    <table>
                        <tr>
                            <td class="textlevel">
                                Fiscal Year :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFiscalYr" runat="server" CssClass="textlevelleft" ToolTip="Select the name of project/department of the employee."
                                    Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td class="textlevel">
                            Rating From:
                            </td>
                            <td>
                                <asp:TextBox ID="txtScoreFrom" runat="server"  Width="100px"></asp:TextBox>
                            </td>
                            <td class="textlevel">
                                Rating Upto :
                            </td>
                            <td>
                                <asp:TextBox ID="txtScoreTo" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td class="textlevel">
                                % Of Basic
                            </td>
                            <td>
                                <asp:TextBox ID="txtPercntOfBasic" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" Text="Show" Width="100px" 
                                    onclick="btnShow_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:HiddenField ID="hfIsUpdate" runat="server" />

            <div>
            <fieldset>

            <legend>Appraisal Increment Salary Update List</legend>
            <div style="overflow: scroll; width: 100%; height: 200px">
                <asp:GridView ID="grAppraisalIncUpdate" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                    AutoGenerateColumns="False" DataKeyNames="SalPakId" >
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>                     
                        <asp:BoundField DataField="EmpId" HeaderText="EmpID">
                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                            <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalRating" HeaderText="Total Rating" HtmlEncode="false" DataFormatString="{0:0}">
                            <ItemStyle CssClass="ItemStylecssRight" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField  HeaderText="New Basic">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField  HeaderText="New Allowance">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField  HeaderText="New PF">
                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                        </asp:BoundField>
                       
                    </Columns>
                </asp:GridView>
            </div></fieldset></div>
             <div class="DivCommand1" style="width: 98%; margin-bottom:25px;" >
           
            <div class="DivCommandR">
                <asp:Button ID="btnUpdateSalaryPak" runat="server" Text="Update Salary Package" Width="200px" 
                    ToolTip="Click to Save The Salary Package" 
                    onclick="btnUpdateSalaryPak_Click"  />
                
            </div>
             <br />
        </div>
        </div>
    </div>
</asp:Content>
