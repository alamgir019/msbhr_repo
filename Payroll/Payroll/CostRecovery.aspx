<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="CostRecovery.aspx.cs" Inherits="Payroll_Payroll_CostRecovery" Title="Cost Recovery Planning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script language ="javascript" type="text/javascript" src ="../../JScripts/Confirmation.js"></script>
<script language="javascript" type="text/javascript" src="../../JScripts/datetimepicker.js"></script>
<div class="formStyle">
   <div id="formhead1"> Cost Recovery Planning</div>
    <div class="MsgBox">
      <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
    </div>
    <div id="Div950" style="margin-left:10px;margin-right:10px;">
        <div style="background-color:#EFF3FB;margin-bottom:5px;">
        <fieldset>
            <table >
                <tr>
                 <td class="textlevelshort">
                        Fiscal Year
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFiscalYear" runat="server" Width="285px">
                        </asp:DropDownList>
                    </td>
                    <td class="textlevelshort">
                        Employee
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="285px" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                    </td>
                </tr>
            </table>
          </fieldset>
         </div>
         <div style="width:100%;">
          <fieldset>
          <table style="border:solid 1px gray;">
            <tr  style="background-color:#C3C8CA;">
                <td class="textlevel">Select GAD</td>
                <td><asp:DropDownList ID="ddlGAD" runat="server" Width="285"></asp:DropDownList></td>
                <td class="textlevel">Select Acc. Line</td>
                <td ><asp:DropDownList ID="ddlAccLine" runat="server" Width="150px"></asp:DropDownList></td>
                <td class="textlevel">Default Percent</td>
                <td><asp:TextBox ID="txtPercent" runat="server" Width="40px"></asp:TextBox></td>
                <td><asp:Button ID="btnAdd" runat="server" Text="Add" Width="120px" OnClick="btnAdd_Click" /></td>
            </tr>
            <tr>
                <td colspan=7><asp:Label ID="lblMsg2" runat="server" ForeColor="red" Font-Bold="true" Font-Size="12px"></asp:Label></td>
            </tr>
          </table>
           <%--<table style="border:solid 1px gray;width:100%;">
                <tr style="background-color:#C3C8CA;">
                    <td>GAD</td>
                    <td align="right">Jul</td>
                    <td align="right">Aug</td>
                    <td align="right">Sep</td>
                    <td align="right">Oct</td>
                    <td align="right">Nov</td>
                    <td align="right">Dec</td>
                    <td align="right">Jan</td>
                    <td align="right">Feb</td>
                    <td align="right">Mar</td>
                    <td align="right">Apr</td>
                    <td align="right">May</td>
                    <td align="right">Jun</td>
                </tr>
                <tr>
                    <td><asp:DropDownList ID="ddlGAD" runat="server" Width="250px"></asp:DropDownList></td>
                    <td align="right"><asp:TextBox ID="TextBox1" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox2" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox3" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox4" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox5" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox6" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox7" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox8" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox9" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox10" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox11" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    <td align="right"><asp:TextBox ID="TextBox12" runat="server" CssClass="TextBoxAmt40"></asp:TextBox></td>
                    
                </tr>
                <tr style="background-color:#C3C8CA;">
                    <%--<td class="textlevel">Is Flat Amount</td>
                    <td align="right"><asp:CheckBox ID="CheckBox1" runat="server" Width="40px" /></td>
                    <td align="right"><asp:CheckBox ID="CheckBox2" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox3" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox4" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox5" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox6" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox7" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox8" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox9" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox10" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox11" runat="server" Width="40px"/></td>
                    <td align="right"><asp:CheckBox ID="CheckBox12" runat="server" Width="40px"/></td>
                </tr>
                <tr>
                    <td colspan="13" align="right"> 
                        </td>
                </tr>
            </table>--%>
            </fieldset>
            </div>
            <div>
            <fieldset>                   
                         <asp:GridView id="grSchedule" runat="server"  DataKeyNames="TRANSID,FiscalYrId,EMPID,PLANACCLINE"  AutoGenerateColumns="False" 
                         EmptyDataText="No Record Found" Font-Size="9px" Width="98%" OnRowCommand="grSchedule_RowCommand" 
                         ShowFooter="true">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="11px"></HeaderStyle>
                            <FooterStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Right" Font-Size="11px"/>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                            <asp:BoundField DataField="" HeaderText="SL">
                              <ItemStyle CssClass="ItemStylecss" Width="3%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="GADCODE" HeaderText="GAD Code">
                              <ItemStyle CssClass="ItemStylecss" Width="8%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="GADTITLE" HeaderText="GAD Title">
                              <ItemStyle CssClass="ItemStylecss" Width="13%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Jul" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJul" Text='<%# Convert.ToString(Eval("JUL")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbJul" runat="server" targetcontrolid="txtJul" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfJul" runat="server" Value='<%# Convert.ToString(Eval("ISJULASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Aug" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAug" Text='<%# Convert.ToString(Eval("AUG")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbAug" runat="server" targetcontrolid="txtAug" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfAug" runat="server" Value='<%# Convert.ToString(Eval("ISAUGASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sep" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSep" Text='<%# Convert.ToString(Eval("SEP")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbSep" runat="server" targetcontrolid="txtSep" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfSep" runat="server" Value='<%# Convert.ToString(Eval("ISSEPASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Oct" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOct" Text='<%# Convert.ToString(Eval("OCT")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbOct" runat="server" targetcontrolid="txtOct" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfOct" runat="server" Value='<%# Convert.ToString(Eval("ISOCTASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nov" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNov" Text='<%# Convert.ToString(Eval("NOV")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbNov" runat="server" targetcontrolid="txtNov" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfNov" runat="server" Value='<%# Convert.ToString(Eval("ISNOVASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dec" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDec" Text='<%# Convert.ToString(Eval("DEC")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbDec" runat="server" targetcontrolid="txtDec" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfDec" runat="server" Value='<%# Convert.ToString(Eval("ISDECASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jan" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJan" Text='<%# Convert.ToString(Eval("JAN")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbJan" runat="server" targetcontrolid="txtJan" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfJan" runat="server" Value='<%# Convert.ToString(Eval("ISJANASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feb" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFeb" Text='<%# Convert.ToString(Eval("FEB")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbFeb" runat="server" targetcontrolid="txtFeb" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfFeb" runat="server" Value='<%# Convert.ToString(Eval("ISFEBASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mar" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMar" Text='<%# Convert.ToString(Eval("MAR")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbMar" runat="server" targetcontrolid="txtMar" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfMar" runat="server" Value='<%# Convert.ToString(Eval("ISMARASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apr" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtApr" Text='<%# Convert.ToString(Eval("APR")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbApr" runat="server" targetcontrolid="txtApr" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfApr" runat="server" Value='<%# Convert.ToString(Eval("ISAPRASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="May" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMay" Text='<%# Convert.ToString(Eval("MAY")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbMay" runat="server" targetcontrolid="txtMay" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfMay" runat="server" Value='<%# Convert.ToString(Eval("ISMAYASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Jun" HeaderStyle-HorizontalAlign="Right">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJun" Text='<%# Convert.ToString(Eval("JUN")) %>' runat="server" CssClass="TextBoxAmt40"></asp:TextBox>
                                    <cc1:filteredtextboxextender id="fltbJun" runat="server" targetcontrolid="txtJun" FilterType ="Custom,Numbers" ValidChars=".,-"></cc1:filteredtextboxextender>
                                    <asp:HiddenField ID="hfJun" runat="server" Value='<%# Convert.ToString(Eval("ISJUNASAMT")) %>' />
                                 </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="" HeaderText="AccLine">
                              <ItemStyle CssClass="ItemStylecss" Width="13%"></ItemStyle>
                            </asp:BoundField>
                            <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="DoubleClick">
                                <ItemStyle Width="5%" CssClass="ItemStylecss"></ItemStyle>
                              </asp:ButtonField> 
                            </Columns>
                          </asp:GridView>
        </fieldset>
        </div>
        <div id="DivCommand1" style="padding-top:3px;">
            <div style="text-align:left;float:left">
              <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False"  />
            </div>
            <div style="text-align:right;">
              <asp:Button ID="btnSave" runat="server" Text="Update" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"   />
              <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClientClick="javascript:return DeleteConfirmation();" OnClick="btnDelete_Click"    />
             </div>
        </div>
    </div>
  </div>
</asp:Content>

