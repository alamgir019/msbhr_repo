<%@ Page Language="C#" MasterPageFile="~/MasterBTMS.master" AutoEventWireup="true"
    CodeFile="AttendancePolicy.aspx.cs" Inherits="Attendance_AttendancePolicy" Title="Attendance Policy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="attendancePolicy">
        <div id="formhead1">
            Attendance Policy
        </div>
        <div class="MsgBox">
            <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="260px"
            Width="98%">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" Height="260px">
                <HeaderTemplate>
                    Attendance Policy Setup
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="Div650">
                        <fieldset>
                            <table>
                                <tr>
                                    <td style="width: 15px">
                                        <asp:Label ID="Label1" runat="server" Text="Policy Title" CssClass="textlevelshort"></asp:Label>
                                        <asp:HiddenField ID="hfAttnPolicyId" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPolicyTitle" runat="server" MaxLength="50" CssClass="ItemTexbox"></asp:TextBox></td>
                                    <td style="width: 3px">
                                        <asp:CheckBox ID="chkInActive" runat="server" CssClass="textlevel" Text="Make Inactive"
                                            Width="90px" /></td>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label4" runat="server" CssClass="textlevel" Text="Over time start after"></asp:Label></td>
                                    <td style="width: 2px">
                                        <asp:TextBox ID="txtOTGrace" runat="server" Width="60px" CssClass="textlevelright"
                                            MaxLength="3">0</asp:TextBox></td>
                                    <td style="width: 2px">
                                        <asp:Label ID="Label8" runat="server" CssClass="textlevelshort" Text="minute(s)"
                                            Width="50px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 120px">
                                        <asp:CheckBox ID="chkIsDefault" runat="server" CssClass="textlevel" Text="Mark As General Shift"
                                            Width="125px" /></td>
                                </tr>
                                <tr>
                                    <td rowspan="2" style="width: 15px">
                                        <asp:Label ID="Label2" runat="server" CssClass="textlevelshort" Text="Description"></asp:Label></td>
                                    <td colspan="2" rowspan="2">
                                        <asp:TextBox ID="txtPolicyDesc" runat="server" CssClass="ItemTexbox" TextMode="MultiLine" Width="230px"
                                            MaxLength="100"></asp:TextBox></td>
                                    <td style="width: 3px">
                                        <asp:Label ID="Label3" runat="server" CssClass="textlevel" Text="Arrival grace time"></asp:Label></td>
                                    <td style="width: 2px">
                                        <asp:TextBox ID="txtArrGrace" runat="server" Width="60px" CssClass="textlevelright"
                                            MaxLength="3">0</asp:TextBox></td>
                                    <td style="width: 2px">
                                        <asp:Label ID="Label9" runat="server" CssClass="textlevelshort" Text="minute(s)"
                                            Width="50px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 3px; height: 48px;">
                                        <asp:Label ID="Label6" runat="server" CssClass="textlevel" Text="Lunch Break"></asp:Label></td>
                                    <td style="width: 2px; height: 48px;">
                                        <asp:TextBox ID="txtLunch" runat="server" Width="60px" CssClass="textlevelright" MaxLength="3">0</asp:TextBox></td>
                                    <td style="width: 2px; height: 48px;">
                                        <asp:Label ID="Label10" runat="server" CssClass="textlevelshort" Text="minute(s)"
                                            Width="50px"></asp:Label></td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td style="width: 5px">
                                        <asp:Label ID="Label14" runat="server" Text="Arrival Time" CssClass="textlevelshort"></asp:Label></td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlArrivalHour" runat="server" CssClass="textlevelleft" Width="45px">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>00</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlArrivalMin" runat="server" CssClass="textlevelright" Width="45px">
                                            <asp:ListItem>0</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 100px; _width: 109px;">
                                    </td>
                                    <td style="width: 3px">
                                        <asp:CheckBox ID="chkIsLunchTime" runat="server" CssClass="textlevel" Text="Lunch Time(hh:mm)"
                                            Width="113px" /></td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlLunchHour" runat="server" Width="45px" CssClass="textlevelleft">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>00</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 3px">
                                        <asp:DropDownList ID="ddlLunchMin" runat="server" Width="45px" CssClass="textlevelright">
                                            <asp:ListItem>0</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 3px">&nbsp;
                                        </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height: 48px;">
                                        <asp:Label ID="Label16" runat="server" Text="Departure Time" CssClass="textlevelshort"></asp:Label></td>
                                    <td style="width: 3px; height: 48px;">
                                        <asp:DropDownList ID="ddlDeptHour" runat="server" Width="45px" CssClass="textlevelleft">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>00</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 3px; height: 48px;">
                                        <asp:DropDownList ID="ddlDeptMin" runat="server" Width="45px" CssClass="textlevelright">
                                            <asp:ListItem>0</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 3px; height: 48px;">
                                        <asp:CheckBox ID="chkIsNextDay" runat="server" CssClass="textlevel" Text="Next day"
                                            Width="70px" /></td>
                                    <td style="width: 3px; height: 48px;">
                                        <asp:Label ID="Label7" runat="server" CssClass="textlevel" Text="Working Time" Width="113px"></asp:Label></td>
                                    <td colspan="2" style="height: 48px">
                                        <asp:TextBox ID="txtWorking" runat="server" Width="60px" CssClass="textlevelright"
                                            MaxLength="3">0</asp:TextBox></td>
                                    <td style="width: 3px; height: 48px;">
                                        <asp:Label ID="Label11" runat="server" CssClass="textlevelleft" Text="hour(s)" Width="50px"></asp:Label></td>
                                </tr>
                            </table>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtOTGrace">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtArrGrace">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtLunch">
                            </cc1:FilteredTextBoxExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtWorking" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </fieldset>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" Height="380px"
                Width="100%">
                <HeaderTemplate>
                    Policy List
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="Grid650">
                        <asp:GridView ID="grAttnPolicy" runat="server" AutoGenerateColumns="False" DataKeyNames="AttnPolicyId"
                            EmptyDataText="No Record Found" Font-Size="9px" OnRowCommand="grAttnPolicy_RowCommand"
                            Width="640px" OnSelectedIndexChanged="grAttnPolicy_SelectedIndexChanged">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" />
                            <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
                            <AlternatingRowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:ButtonField CommandName="DoubleClick" HeaderText="Edit" Text="Edit">
                                    <ItemStyle CssClass="ItemStylecss" Width="40px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="PolicyName" HeaderText="Policy Title">
                                    <ItemStyle CssClass="ItemStylecss" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTStartGrace" HeaderText="OT Grace Min.">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ArvlGrace" HeaderText="Arrival Grace Min.">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LunchBreak" HeaderText="Lucnch Break Min.">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InTime" HeaderText="In Time">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OutTime" HeaderText="Out Time">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IsActive" HeaderText="Active">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ISDefault" HeaderText="General Shift">
                                    <ItemStyle CssClass="ItemStylecss" Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfDescription" runat="server" Value='<%# Convert.ToString(Eval("PoliDesc")) %>' />
                                        <asp:HiddenField ID="hfIsNextDay" runat="server" Value='<%# Convert.ToString(Eval("IsNextDay")) %>' />
                                        <asp:HiddenField ID="hfLunchTime" runat="server" Value='<%# Convert.ToString(Eval("LunchTime")) %>' />
                                        <asp:HiddenField ID="hfWorkingHr" runat="server" Value='<%# Convert.ToString(Eval("WorkingHr")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="ItemStylecss" Width="10px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <div class="DivCommand1">
            <div class="DivCommandL">
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="70px" CausesValidation="False"
                    OnClick="btnRefresh_Click" />
            </div>
            <div class="DivCommandR">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" UseSubmitBehavior="False"
                    OnClick="btnSave_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="70px" OnClick="btnDelete_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" Width="70px" />
            </div>
        </div>
    </div>
</asp:Content>
