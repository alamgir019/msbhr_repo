<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true" CodeFile="RoasterShiftShedule.aspx.cs" Inherits="Attendance_RoasterShiftShedule" Title="Roaster Shift" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="width:1000px;margin-left:10px;margin-right:10px; text-align:left;background-color:White;">
    <fieldset >
     <div id="formhead">
            Raoster Shift
      </div>
        <div style="text-align:right; height:20px;">
        <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
      </div>
      
       <div style="margin:0px 0px 0px 0px;height:40px; width:972px;_width:985px; border:solid 1px black;">
           <fieldset style="height:23px; width:952px;_width:975px;">
               <table>
                   <tr>
                       <td style="width: 3px; height: 24px;">
                           <asp:Label ID="Label1" runat="server" Text="Shift Schedule" CssClass="textlevelshort"></asp:Label></td>
                       <td style="width: 81px; height: 24px;">
                           <asp:DropDownList ID="ddlMonth" runat="server" Width="100px">
                               <asp:ListItem Value="-1">Month</asp:ListItem>
                               <asp:ListItem Value="1">January</asp:ListItem>
                               <asp:ListItem Value="2">February</asp:ListItem>
                               <asp:ListItem Value="3">March</asp:ListItem>
                               <asp:ListItem Value="4">April</asp:ListItem>
                               <asp:ListItem Value="5">May</asp:ListItem>
                               <asp:ListItem Value="6">June</asp:ListItem>
                               <asp:ListItem Value="7">July</asp:ListItem>
                               <asp:ListItem Value="8">August</asp:ListItem>
                               <asp:ListItem Value="9">September</asp:ListItem>
                               <asp:ListItem Value="10">October</asp:ListItem>
                               <asp:ListItem Value="11">November</asp:ListItem>
                               <asp:ListItem Value="12">December</asp:ListItem>
                           </asp:DropDownList></td>
                            <td style="width: 3px; height: 24px;">
                           <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlMonth" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator></td>
                       <td style="height: 24px">
                           <asp:DropDownList ID="ddlYear" runat="server" Width="80px">
                               <asp:ListItem>2008</asp:ListItem>
                               <asp:ListItem>2009</asp:ListItem>
                           </asp:DropDownList></td>
                       <td style="width: 3px; height: 24px;">
                           <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlYear" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="-1"></asp:CompareValidator></td>
                       <td style="width: 3px; height: 24px;">
                           <asp:Label ID="Label4" runat="server" CssClass="textlevelshort" Text="Department"></asp:Label></td>
                       <td style="width: 3px; height: 24px;">
                           <asp:DropDownList ID="ddlDept" runat="server" Width="200px">
                       </asp:DropDownList></td>
                       <td style="width: 3px; height: 24px;">
                           <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDept"
                               ErrorMessage="*" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="-1"></asp:CompareValidator></td>
                       <td style="width: 3px; height: 24px;">
                           </td>
                   </tr>
                   <tr>
                       <td style="width: 3px; height: 24px">
                           </td>
                       <td colspan="3" style="height: 24px">
                           </td>
                       <td style="width: 3px; height: 24px">
                       </td>
                       <td style="width: 3px; height: 24px">
                           </td>
                       <td style="width: 3px; height: 24px"></td>
                       <td style="width: 3px; height: 24px">
                       </td>
                       <td style="width: 3px; height: 24px">
                       </td>
                   </tr>
               </table>
           </fieldset>
       </div>
       <div style="margin:5px 0px 0px 0px;height:40px; width:972px;_width:985px; border:solid 1px black;">
           <fieldset style="height:23px; width:952px;_width:975px;">
               <table>
                   <tr>
                       <td style="width: 3px">
                           <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" /></td>
                       <td style="width: 3px">
                           <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload"
                               Width="80px" /></td>
                       <td style="width: 3px">
                           <asp:Button ID="btnRemove" runat="server" OnClick="btnClose_Click" Text="Remove"
                               Width="80px" /></td>
                        <td style="width: 3px"></td>
                        <td style="width: 3px"></td>
                        <td style="width: 3px"></td>
                        <td style="width: 3px"></td>
                        <td style="width: 3px"></td>
                   </tr>
               </table>
           </fieldset>
       </div>
       <div style="margin:5px 0px 0px 0px;height:400px; width:972px;_width:985px;overflow:scroll; border:solid 1px black;">
          <asp:GridView ID="grRoaster" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="EmpId,ID1,ID2,ID3,ID4,ID5,ID6,ID7,ID8,ID9,ID10,ID11,ID12,ID13,ID14,ID15,ID16,ID17,ID18,ID19,ID20,ID21,ID22,ID23,ID24,ID25,ID26,ID27,ID28,ID29,ID30,ID31" EmptyDataText="No Record Found" Font-Size="9px" 
                                  Width="2100px">
                                 
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingRowStyle BackColor="#EFF3FB" />
                                <Columns>
                                  <asp:BoundField DataField="EmpId" HeaderText="EmpId">
                                        <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                        <ItemStyle CssClass="ItemStylecss" Width="160px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Day1" HeaderText="01">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day2" HeaderText="02">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day3" HeaderText="03">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day4" HeaderText="04">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day5" HeaderText="05">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day6" HeaderText="06">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day7" HeaderText="07">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day8" HeaderText="08">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day9" HeaderText="09">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day10" HeaderText="10">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day11" HeaderText="11">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day12" HeaderText="12">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day13" HeaderText="13">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day14" HeaderText="14">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day15" HeaderText="15">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day16" HeaderText="16">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day17" HeaderText="17">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day18" HeaderText="Day18">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day19" HeaderText="Day19">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day20" HeaderText="20">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day21" HeaderText="21">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day22" HeaderText="22">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day23" HeaderText="23">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day24" HeaderText="24">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day25" HeaderText="25">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day26" HeaderText="26">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day27" HeaderText="27">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day28" HeaderText="28">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day29" HeaderText="29">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day30" HeaderText="30">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Day31" HeaderText="31">
                                        <ItemStyle CssClass="ItemStylecssCenter" Width="60px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
            
       </div>
       <div style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 20px 5px 5px 5px; padding-top: 0px; height:30px;">
                       
                    <div style="text-align:left;float:left">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px"  CausesValidation="False" />
                    </div>
                    <div style="text-align:right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px"  UseSubmitBehavior="False" OnClick="btnSave_Click"  />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px"  />
                        <asp:Button ID="btnClose" runat="server" Text="Close" Width="70px" />
                    </div>
                   
        </div>             
        <asp:HiddenField ID="hfEmpId" runat="server" />
        <asp:DropDownList ID="ddlAttnPolicy" runat="server">
        </asp:DropDownList></fieldset>
</div>
</asp:Content>

