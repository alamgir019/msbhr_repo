<%@ Page Title="" Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="EmpKPI.aspx.cs" Inherits="KPI_EmpKPI" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>
    <div class="empTrainForm">
        <div id="formhead1">
            <div style="width: 96%; float: left;">
                Employee KPI</div>
            <div style="margin: 2px; float: left; padding-right: 3px;">
                <a href="../Default.aspx">
                    <img src="../Images/close_icon.gif" /></a></div>
        </div>
        <!--Div for group-->
        <div class="MsgBox">
            <!--Div for msg-->
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="officeSetupInner">
            <fieldset>
                <!--Div for Controls-->
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                <table>
                    
                    <tr>
                    
                    </tr>

                    <tr>
                    <td></td><td>Employee ID</td> <td>Year</td><td></td><td>Quarter</td><td>Group</td><td></td><td></td>
                    </tr>
                    <tr>
                    <td></td>
                    <td><asp:TextBox ID="txtEmpID" runat="server" Width="80px"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtYear" runat="server" Width="80px" ></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtYear" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    <td><asp:DropDownList ID="ddlQuarter" runat="server" CssClass="textlevelleft" ToolTip="Select Grade" Width="180px"></asp:DropDownList></td>
                    <td><asp:DropDownList ID="ddlGroup" runat="server" CssClass="textlevelleft" ToolTip="Select Group." Width="180px"></asp:DropDownList></td>
                    <td><asp:Button ID="btnView" runat="server" Text="View" Width="70px" OnClick="btnView_Click" /></td>
                    </tr>
                    
                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td style="height: 22px" colspan="3">
                            <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevelleft" Text="Mark Inactive" />
                        </td>
                        <td style="height: 22px">
                        </td>
                    </tr>
                </table>
            </fieldset>
          
        </div>
        <div class="GridFormat1">
            <!--Grid view Code starts-->
            <asp:GridView ID="grEmpKpi" runat="server" DataKeyNames="GroupId,KpiId,EKPIID" AutoGenerateColumns="False"
                EmptyDataText="No Record Found" Font-Size="9px" Width="99%">
                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                      <ItemStyle Width="3%" CssClass="ItemStylecss" />
                        <ItemTemplate>
                          <asp:CheckBox ID="ChkBox" runat="Server" Checked='<%# Eval("ChkStatus").ToString()=="1" ? true : false %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>
 
                    <asp:BoundField DataField="IndicatirTypeName" HeaderText="Indicator">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="KpiId" HeaderText="" Visible="false">
                        <ItemStyle CssClass="ItemStylecss" ></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="KpiName" HeaderText="KPI">
                        <ItemStyle CssClass="ItemStylecss" Width="35%"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="Measur" HeaderText="Measure">
                        <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                    </asp:BoundField>

                     <asp:BoundField DataField="Target" HeaderText="Target">
                        <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                    </asp:BoundField>


                    <asp:TemplateField HeaderText="Achievement">
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                          <ItemTemplate>
                             <asp:TextBox ID="txtAchive" runat="server" Style="text-align: left;" Width="150px"
                                       Text='<%# Bind("Achive") %>'></asp:TextBox>
                          </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="% of Achievement">
                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                          <ItemTemplate>
                             <asp:TextBox ID="txtAchivePercent" runat="server" Style="text-align: left;" Width="50px"
                                       Text='<%# Bind("AchivePercent") %>'></asp:TextBox>
                          </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <!--Grid view Code Ends-->
        </div>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnClear" runat="server" Text="Refresh" Width="70px" OnClick="btnClear_Click"
                    CausesValidation="False" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();"
                    OnClick="btnDelete_Click" />
            </div>
        </div>
    </div>
</asp:Content>







