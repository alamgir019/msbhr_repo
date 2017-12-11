<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveAccural.aspx.cs" Inherits="Leave_LeaveAccural"
    Title="Leave Accural" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
    function ToUpper(ctrl)
    {   
    var t = ctrl.value;
    ctrl.value = t.toUpperCase(); 
    }
    </script>

    <div class="EmpLeaveProfStyle" style="width: 80%;">
        <div id='formhead4'>
            Earn Leave Adjustment</div>
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server" ForeColor="Navy" Font-Bold="True" CssClass="lblMsg"></asp:Label>
        </div>
        <div class="Div950">
            <div style="background-color: #EFF3FB; border: solid 1px #3E506C;">
                <table>
                    <tr>
                        <td class="textlevel">
                            Emp Type :</td>
                        <td class="textlevel">
                            <asp:DropDownList ID="ddlEmpType" runat="server" Width="130px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Leave Type :</td>
                        <td class="textlevel">
                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="200px" 
                                CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Month :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMonth" runat="server" Width="100px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td class="textlevel">
                            Year :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="textlevelleft">
                            </asp:DropDownList></td>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" /></td>
                    </tr>
                </table>
            </div>
            <br />
            <div>
                <asp:GridView ID="grEmpList" runat="server" Width="100%" Font-Size="9px" AutoGenerateColumns="False"
                    DataKeyNames="" EmptyDataText="No Record Found" PageSize="7">
                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                    </SelectedRowStyle>
                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemStyle CssClass="ItemStylecss" Width="40px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkBox" Checked="true" runat="Server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="" HeaderText="SL">
                            <ItemStyle Width="8%" CssClass="ItemStylecssCenter"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                            <ItemStyle Width="10%" CssClass="ItemStylecssCenter"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FullName" HeaderText="Full Name">
                            <ItemStyle Width="20%" CssClass="ItemStylecss" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DesigName" HeaderText="Designation">
                            <ItemStyle Width="20%" CssClass="ItemStylecss" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date">
                            <ItemStyle Width="10%" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="LEntitled" HeaderText="Previous Balance">
                            <ItemStyle Width="10%" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="This Month Entitlement">
                            <ItemStyle Width="10%" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="" HeaderText="Current Balance">
                            <ItemStyle Width="10%" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>                        
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <!--Div for group-->
            <div>
                <div style="padding-left: 0px; padding-top: 3px; width: 45%; float: left;">
                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Refresh"
                        UseSubmitBehavior="False" Width="70px" />
                </div>
                <div style="text-align: right; float: right; width: 45%;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></div>
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
