<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="GrossSalaryHeadSetup.aspx.cs" Inherits="Payroll_HeadSetup_GrossSalaryHeadSetup"
    Title="Gross Salary Items Setup" %>

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
            Gross Salary Items
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <div id="PayrollConfigInner">
            <fieldset>
                <legend>Gross Salary Items</legend>
                <br />
                <span style="color: Blue;">Please select the Salary Items which will be inculded in
                    Gross Salary</span>
                <br />
                <br />
                Select <a id="A1" href="#" onclick="javascript: selectAllNone ('<%= grGrossSalary.ClientID %>',true)">
                    All</a> | <a id="A2" href="#" onclick="javascript: selectAllNone ('<%= grGrossSalary.ClientID %>',false)">
                        None</a>
                <hr />
                <div style="width: 100%; overflow: scroll; height: 300px;">
                    <asp:GridView ID="grGrossSalary" runat="server" DataKeyNames="SHEADID" AutoGenerateColumns="False"
                        EmptyDataText="No Record Found" Font-Size="9px" Width="99%">
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
                            <%--<asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                      <asp:HiddenField ID ="hfSHEADID" Value='<%# Convert.ToString(Eval("SHEADID"))%>' runat="server" />                      
                    </ItemTemplate>
                  </asp:TemplateField> --%>
                        </Columns>
                    </asp:GridView>
                </div>
                <hr />
                Select <a id="A3" href="#" onclick="javascript: CheckBoxListSelect ('<%= grGrossSalary.ClientID %>',true)">
                    All</a> | <a id="A4" href="#" onclick="javascript: CheckBoxListSelect ('<%= grGrossSalary.ClientID %>',false)">
                        None</a>
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
