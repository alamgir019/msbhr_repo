<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="TotalSalaryHeadSetup.aspx.cs" Inherits="Payroll_HeadSetup_TotalSalaryHeadSetup"
    Title="Payroll Salary Items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function selectAllNone(grID, value) {
            var tvNodes = document.getElementById(grID);
            var chBoxes = tvNodes.getElementsByTagName("input");
            for (var i = 0; i < chBoxes.length; i++) {
                var chk = chBoxes[i];
                if (chk.type == "checkbox") {
                    chk.checked = value;
                    //alert(tvNodes[i].href);
                }
            }
        }
    </script>
    <div id="PayrollConfigForm">
        <div id="formhead1">
            Payroll Salary Items
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <fieldset>
                <legend>Payroll Salary Items</legend>
                <br />
                <span style="color: Blue;">Please select the Salary Item which will be inculded in Total
                    Salary</span>
                <br />
                <br />
                Select <a id="A1" href="#" onclick="javascript: selectAllNone ('<%= grTotalSalary.ClientID %>',true)">
                    All</a> | <a id="A2" href="#" onclick="javascript: selectAllNone ('<%= grTotalSalary.ClientID %>',false)">
                        None</a>
                <hr />
                <div style="width: 98%; overflow: scroll; height: 300px;">
                    <asp:GridView ID="grTotalSalary" runat="server" DataKeyNames="SHEADID,HeadNature"
                        AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" Width="92%">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                        </SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="Server"></asp:CheckBox>
                                    <asp:HiddenField ID="hfSHEADID" Value='<%# Convert.ToString(Eval("SHEADID"))%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="HEADNAME" HeaderText="Item Title">
                                <ItemStyle CssClass="ItemStylecss" Width="25%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Type">
                                <ItemStyle CssClass="ItemStylecss" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Seq. No">
                                <ItemStyle CssClass="ItemStylecss" Width="15%" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSeqNo" runat="server" Text="" Width="60px"></asp:TextBox>
                                    <asp:HiddenField ID="hfDispType" Value="" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <hr />
            </fieldset>
            <div id="DivCommand1" style="padding-top: 3px;">
                <div style="text-align: left; float: left">
                </div>
                <div style="text-align: right;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                        OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
