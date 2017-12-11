<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MovementStatementEdit.aspx.cs"
    Inherits="Payroll_Payroll_MovementStatementEdit" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Movement Statement</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 12px; font-family: Arial; font-weight: bold; border: solid 1px dimgray;
        width: 940px;">
        &nbsp;CO Name: Marie Stopes
        <br />
        &nbsp;<asp:Label ID="lblPaymentFor" runat="server" Text=""></asp:Label><asp:Label
            ID="lblBank" runat="server" Text=""></asp:Label>
        <br />
        &nbsp;Statement of salary movement between
        <asp:Label ID="lblMonthBetween" runat="server" Text=""></asp:Label>
    </div>
    <div style="font-size: 12px; font-family: Arial; border: solid 1px dimgray; width: 940px;">
        <asp:GridView ID="grMovement" runat="server" Width="940px" DataKeyNames="" AutoGenerateColumns="False"
            EmptyDataText="No Record Found" ShowFooter="true">
            <HeaderStyle Font-Bold="True" HorizontalAlign="center" Font-Names="Arial"></HeaderStyle>
            <RowStyle HorizontalAlign="right" Font-Names="Arial" />
            <FooterStyle Font-Bold="True" Font-Names="Arial" HorizontalAlign="right"></FooterStyle>
            <Columns>
                <asp:BoundField DataField="DESCRIP" HeaderText="Name of Month">
                    <ItemStyle Width="335px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TOTALSAL" HeaderText="Gross Salary & Benefits">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PFCONT" HeaderText="PF Contribution">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PFLOAN" HeaderText="PF Loan">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IT" HeaderText="IT">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SALADV" HeaderText="Salary Adv.">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="LWP" HeaderText="LWP">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TOTALDEDUCT" HeaderText="Total Deuction">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="NETPAY" HeaderText="Net Pay">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <strong>&nbsp;Details of Increased/(Decreased)</strong>
        <asp:GridView ID="grMovementDetls" runat="server" Width="940px" DataKeyNames="TRANSID,EMPID,MOVEID,VMONTH,VYEAR,PAYID,BRANCHCODE"
            AutoGenerateColumns="False" EmptyDataText="No Record Found" ShowFooter="True">
            <HeaderStyle Font-Bold="True" HorizontalAlign="center" Font-Names="Arial"></HeaderStyle>
            <RowStyle HorizontalAlign="right" Font-Names="Arial" Font-Size="10px" />
            <FooterStyle Font-Bold="True" HorizontalAlign="right" Font-Names="Arial"></FooterStyle>
            <Columns>
                <asp:BoundField DataField="PAYID" HeaderText="Payroll SL#" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="60px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="EMPNAME" HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemStyle Width="140px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDescrip" Text='<%# Convert.ToString(Eval("DESCRIP")) %>' runat="server"
                            Width="150px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TOTALSAL" HeaderText="">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PFCONT" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PFLOAN" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IT" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="SALADV" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="LWP" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TOTALDEDUCT" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="NETPAY" HeaderText="">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:GridView ID="grValidation" runat="server" Width="940px" DataKeyNames="" AutoGenerateColumns="False"
            EmptyDataText="No Record Found" ShowFooter="False" ShowHeader="false">
            <RowStyle HorizontalAlign="Center" Font-Names="Arial" Font-Bold="true" />
            <Columns>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="330px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle Width="80px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <table style="margin-top: 10px;">
            <tr>
                <td style="width: 235px;">
                    Prepared By:
                </td>
            </tr>
            <tr>
                <td style="width: 235px;">
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblPreparedBy" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div style="background-color: DimGray; text-align: center; font-weight: bold;">
            &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Update Movement Statement"
                Width="220px" Font-Bold="true" OnClick="btnUpdate_Click" />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
