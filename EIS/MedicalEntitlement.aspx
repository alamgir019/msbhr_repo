<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="MedicalEntitlement.aspx.cs" Inherits="EIS_MedicalEntitlement" Title="Medical Entitlement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../JScripts/Confirmation.js">
    </script>

    <div class="empSearchForm">
        <div id='formhead4'>
            Medical Entitlement</div>
        <div class="Div950">
            <fieldset>
                <legend>Medical Renew </legend>
                <div class="MsgBox">
                    <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
                </div>
                <table>
                    <tr>                       
                        <td>
                            <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" 
                                Text="Show All Staff" Width="109px">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
                <div id="empSearchResult">
                    <strong>Employee Search Result </strong>
                    <asp:GridView ID="grEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames=""
                        EmptyDataText="No Record Found" Font-Size="9px">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingRowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemStyle CssClass="ItemStylecss" Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBox" runat="Server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="SL.No">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                <ItemStyle CssClass="ItemStylecss" Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GradeName" HeaderText="Grade">
                                <ItemStyle CssClass="ItemStylecss" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JobTitleName" HeaderText="Job Title">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptName" HeaderText="Team">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnitName" HeaderText="Unit">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SalLocName" HeaderText="Salary Location">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PostingDivName" HeaderText="Posting Division">
                                <ItemStyle CssClass="ItemStylecss" Width="12%" />
                            </asp:BoundField>                           
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin-left: 10px">
                    <table>
                        <tr>
                            <td style="width: 3px">
                                <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Total Record Count:"></asp:Label></td>
                            <td style="width: 3px">
                                <asp:Label ID="lblRecordCount" runat="server" Font-Bold="True" Font-Size="Smaller"
                                    ForeColor="Blue"></asp:Label></td>
                        </tr>
                    </table>
                </div>                
                <table style="width: 100%; border-top-style: solid; border-right-style: solid; border-left-style: solid;
                    border-bottom-style: solid;" border="1">
                    <tr>
                        <td style="width: 250px; height: 20px" align="left" class="textlevelleft">
                            Old Medical period</td>
                        <td class="textlevelleft" style="height: 20px; width: 250px;" align="left">
                            New Medical period</td>
                        <td class="textlevel" style="width: 216px; height: 20px">
                            Click Start Button for Medical Renew</td>
                    </tr>
                    <tr>
                        <td style="width: 250px; height: 26px">
                            <asp:GridView ID="GridView1" runat="server" Width="360px" AutoGenerateColumns="False">
                                <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="Small" Font-Bold="True">
                                </HeaderStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="MedicalStartPeriod" HeaderText="Medical Start Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MedicalEndPeriod" HeaderText="Medical End Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="height: 26px; width: 250px;">
                            <asp:GridView ID="GridView2" runat="server" Width="360px" AutoGenerateColumns="False">
                                <HeaderStyle BackColor="#B3CDE4" HorizontalAlign="Center" Font-Size="Small" Font-Bold="True">
                                </HeaderStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="MedicalStartPeriod" HeaderText="Medical Start Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MedicalEndPeriod" HeaderText="Medical End Period">
                                        <ItemStyle CssClass="ItemStylecss" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="width: 216px; height: 26px">
                            <asp:Button ID="btnStart" runat="server" Text="Start" Width="95px" 
                                OnClick="cmdStart_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
