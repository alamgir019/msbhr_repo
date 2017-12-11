<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="LeaveTakenValidation.aspx.cs" Inherits="Leave_LeaveTakenValidation" Title="Leave Taken Validation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Script/datetimepicker.js">
    </script>
    <script language="javascript" type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }
    </script>
    <div class="formStyle" style="height: 340px; width: 48%">
        <div id="formhead4">
            Leave Taken Validation</div>
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="iesEmp" style="margin-top: 40px;">
            <fieldset>
                <table>
                    <tr>
                        <td class="textlevel">
                            Previous Date Leave Type :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" CausesValidation="True" CssClass="textlevelleft"
                                Width="150px" ID="ddlPLeaveType">
                                <asp:ListItem Value="99999">Nil</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPLeaveType"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="textlevel">
                            Next Date Leave Type :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" CausesValidation="True" CssClass="textlevelleft"
                                Width="150px" ID="ddlNLeaveType">
                                <asp:ListItem Value="99999">Nil</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNLeaveType"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:HiddenField ID="hfId" runat="server" />
                        </td>
                    </tr>
                    </table>
                <div class="DivCommand1">
                    <div class="DivCommandL">
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                            UseSubmitBehavior="False" CausesValidation="false" />
                    </div>
                    <div class="DivCommandR">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                            OnClick="btnDelete_Click" CausesValidation="false" />
                    </div>
                </div>
            </fieldset>           
            <asp:GridView ID="grLeave" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="No Record Found" DataKeyNames="Id,PLTypeId,NLTypeId" 
                Font-Size="9px" Width="95%" onrowcommand="grLeave_RowCommand" >
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                        <ItemStyle CssClass="ItemStylecss" Width="5%" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="PLTypeTitle" HeaderText="Prev Leave Type">
                        <ItemStyle CssClass="ItemStylecss" Width="30%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NLTypeTitle" HeaderText="Next Leave Type">
                        <ItemStyle CssClass="ItemStylecss" Width="30%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
