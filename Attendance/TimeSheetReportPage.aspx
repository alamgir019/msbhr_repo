<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeSheetReportPage.aspx.cs"
    Inherits="Attendance_TimeSheetReportPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time Sheet Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 20px; margin-right: 20px;">
        <asp:Label ID="lblMsg" runat="server" CssClass="msglabel"></asp:Label>
            <div style="width: 99%; font-family: Arial; font-size: 20px; float: left; text-align: center;">
                Marie Stopes Bangladesh
                <br />
                Monthly Time Sheet
            </div>
            <div style="width: 99%; font-family: Arial; font-size: 14px; float: left; text-align: left;">
                <table width="99%">
                    <tr>
                        <td style="width: 33%; text-align: left;">
                            <span style="font-weight: bold;">Employee ID:</span>&nbsp;<asp:Label ID="lblEmpId"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label></td>
                        <td style="width: 33%; text-align: left;">
                            <span style="font-weight: bold;">Employee Name:</span>&nbsp;<asp:Label ID="lblEmpName"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label></td>
                        <td style="width: 34%; text-align: left;">
                            <span style="font-weight: bold;">Project/Department:</span>&nbsp;<asp:Label ID="lblProject"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 33%; text-align: left;">
                            <span style="font-weight: bold;">Pay Period &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                From:</span>&nbsp;<asp:Label ID="lblFrom" runat="server" Text="" Font-Names="Tahoma"
                                    Font-Size="14px"></asp:Label></td>
                        <td style="width: 33%; text-align: left;">
                            <span style="font-weight: bold;">To:</span>&nbsp;<asp:Label ID="lblTo" runat="server"
                                Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label></td>
                        <td style="width: 34%; text-align: left;">
                            <span style="font-weight: bold;">Year:</span>&nbsp;<asp:Label ID="lblYear" runat="server"
                                Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label></td>
                    </tr>
                </table>
                <%--<tr>
                                        <td>
                                            <span style="font-family: Tahoma; font-size: 14px;">Emp Name :</span>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        </tr>
                                    <tr>
                                        <td>
                                            <span style="font-family: Tahoma; font-size: 14px;">,</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPos" runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-family: Tahoma; font-size: 14px;"></span>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>--%>
            </div>
        </div>
        <div style="margin-left: 1%; margin-right: 1%; width: 98%; float: left;">
            <asp:GridView ID="grTimeSheet" runat="server" DataKeyNames="GADCODE,FiscalYrId,EMPID,JUL"
                AutoGenerateColumns="False" EmptyDataText="No Record Found" Font-Size="9px" ShowFooter="true">
                <HeaderStyle BackColor="#C2D69B" Font-Bold="True"></HeaderStyle>
                <FooterStyle BackColor="#C2D69B" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField DataField="GADTITLE" HeaderText="GAD Title">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="%">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Acc Line">
                        <ItemStyle CssClass="ItemStylecss" Width="450px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txt1" Width="35px" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="lb1" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("2") %>'>
                                    <asp:Image ID="imglb1" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                            <FooterTemplate>
                                <asp:Label ID="lbltxt1Total" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt2" Width="35px" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lb2" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("3") %>'>
                                        <asp:Image ID="imglb2" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt3" Width="35px" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lb3" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("4") %>'>
                                        <asp:Image ID="imglb3" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt4" Width="35px" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lb4" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("5") %>'>
                                        <asp:Image ID="imglb4" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt5" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb5" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("6") %>'>
                                <asp:Image ID="imglb5" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt6" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb6" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("7") %>'>
                                <asp:Image ID="imglb6" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt7" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb7" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("8") %>'>
                                <asp:Image ID="imglb7" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt8" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb8" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("9") %>'>
                                <asp:Image ID="imglb8" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt9" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb9" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("10") %>'>
                                <asp:Image ID="imglb9" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt10" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb10" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("11") %>'>
                                <asp:Image ID="imglb10" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt11" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb11" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("12") %>'>
                                <asp:Image ID="imglb11" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt12" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb12" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("13") %>'>
                                <asp:Image ID="imglb12" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt13" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb13" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("14") %>'>
                                <asp:Image ID="imglb13" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt14" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb14" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("15") %>'>
                                <asp:Image ID="imglb14" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt15" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb15" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("16") %>'>
                                <asp:Image ID="imglb15" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt16" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb16" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("17") %>'>
                                <asp:Image ID="imglb16" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt17" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb17" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("18") %>'>
                                <asp:Image ID="imglb17" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt18" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb18" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("19") %>'>
                                <asp:Image ID="imglb18" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt19" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb19" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("20") %>'>
                                <asp:Image ID="imglb19" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt20" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb20" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("21") %>'>
                                <asp:Image ID="imglb20" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt21" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb21" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("22") %>'>
                                <asp:Image ID="imglb21" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt22" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb22" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("23") %>'>
                                <asp:Image ID="imglb22" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt23" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb23" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("24") %>'>
                                <asp:Image ID="imglb23" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt24" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb24" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("25") %>'>
                                <asp:Image ID="imglb24" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt25" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb25" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("26") %>'>
                                <asp:Image ID="imglb25" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt26" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb26" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("27") %>'>
                                <asp:Image ID="imglb26" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt27" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb27" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("28") %>'>
                                <asp:Image ID="imglb27" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt28" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb28" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("29") %>'>
                                <asp:Image ID="imglb28" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt29" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb29" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("30") %>'>
                                <asp:Image ID="imglb29" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt30" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb30" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("31") %>'>
                                <asp:Image ID="imglb30" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt31" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb31" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("32") %>'>
                                <asp:Image ID="imglb31" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    
                    
                    <asp:BoundField HeaderText="Total Work Hour">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="Total Work Hour">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTotal" Width="45px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                         <FooterTemplate>
<asp:Label ID="lblTotal" runat="server" />
</FooterTemplate>
                    </asp:TemplateField>--%>
                    
                    
                    <asp:BoundField HeaderText="% of Work">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="% of Work">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRatio" Width="45px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <div style="width: 99%; font-family: Arial; font-size: 14px; float: left; text-align: center;">
                Number of Hours of Paid Time Leave
            </div>
            <br />
            <asp:GridView ID="grLeave" runat="server" DataKeyNames="Ltype" AutoGenerateColumns="False"
                EmptyDataText="Leave records not available" ShowFooter="true" ShowHeader="True" Font-Size="9px">
                <HeaderStyle BackColor="#C2D69B" Font-Bold="True"></HeaderStyle>
                <FooterStyle BackColor="#C2D69B" Font-Bold="True" />
                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" CssClass="ListHeader" Font-Bold="True">
                </SelectedRowStyle>
                <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField DataField="LTypeTitle">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Count">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Acc Line">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:TextBox ID="txt1" Width="35px" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="lb1" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("2") %>'>
                                    <asp:Image ID="imglb1" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                            <FooterTemplate>
                                <asp:Label ID="lbltxt1Total" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt2" Width="35px" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lb2" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("3") %>'>
                                        <asp:Image ID="imglb2" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt3" Width="35px" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lb3" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("4") %>'>
                                        <asp:Image ID="imglb3" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txt4" Width="35px" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="lb4" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("5") %>'>
                                        <asp:Image ID="imglb4" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt5" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb5" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("6") %>'>
                                <asp:Image ID="imglb5" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt6" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb6" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("7") %>'>
                                <asp:Image ID="imglb6" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt7" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb7" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("8") %>'>
                                <asp:Image ID="imglb7" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt8" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb8" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("9") %>'>
                                <asp:Image ID="imglb8" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField>
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt9" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb9" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("10") %>'>
                                <asp:Image ID="imglb9" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt10" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb10" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("11") %>'>
                                <asp:Image ID="imglb10" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt11" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb11" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("12") %>'>
                                <asp:Image ID="imglb11" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt12" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb12" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("13") %>'>
                                <asp:Image ID="imglb12" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt13" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb13" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("14") %>'>
                                <asp:Image ID="imglb13" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt14" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb14" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("15") %>'>
                                <asp:Image ID="imglb14" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt15" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb15" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("16") %>'>
                                <asp:Image ID="imglb15" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt16" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb16" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("17") %>'>
                                <asp:Image ID="imglb16" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt17" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb17" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("18") %>'>
                                <asp:Image ID="imglb17" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt18" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb18" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("19") %>'>
                                <asp:Image ID="imglb18" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt19" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb19" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("20") %>'>
                                <asp:Image ID="imglb19" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt20" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb20" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("21") %>'>
                                <asp:Image ID="imglb20" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt21" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb21" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("22") %>'>
                                <asp:Image ID="imglb21" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt22" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb22" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("23") %>'>
                                <asp:Image ID="imglb22" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt23" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb23" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("24") %>'>
                                <asp:Image ID="imglb23" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt24" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb24" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("25") %>'>
                                <asp:Image ID="imglb24" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt25" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb25" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("26") %>'>
                                <asp:Image ID="imglb25" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt26" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb26" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("27") %>'>
                                <asp:Image ID="imglb26" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt27" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb27" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("28") %>'>
                                <asp:Image ID="imglb27" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt28" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb28" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("29") %>'>
                                <asp:Image ID="imglb28" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt29" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb29" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("30") %>'>
                                <asp:Image ID="imglb29" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt30" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb30" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("31") %>'>
                                <asp:Image ID="imglb30" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txt31" Width="35px" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="lb31" runat="server" CommandName="Click" CommandArgument='<%# Convert.ToString("32") %>'>
                                <asp:Image ID="imglb31" runat="server" ImageUrl="~/Images/calendar_task.png" ImageAlign="Right" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <asp:BoundField HeaderText="Total Hour">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:TemplateField HeaderText="Total Work Hour">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTotal" Width="45px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField HeaderText="% of Work">
                        <ItemStyle CssClass="ItemStylecss" Width="250px"></ItemStyle>
                    </asp:BoundField>--%>
                    <%--<asp:TemplateField HeaderText="% of Work">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRatio" Width="45px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <%--<tr>
                                        <td>
                                            <span style="font-family: Tahoma; font-size: 14px;">Emp Name :</span>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        </tr>
                                    <tr>
                                        <td>
                                            <span style="font-family: Tahoma; font-size: 14px;">,</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPos" runat="server" Text="" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-family: Tahoma; font-size: 14px;"></span>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>--%>
                                    
                                    <br />
                                    
                                    <fieldset>
                <legend>Time Sheet Summary</legend>
                <table>
                    <tr>
                        <td>
                            Assigned Hour (A) :</td>
                        <td>
                            <asp:Label ID="lblAssignedHour" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            Worked Hour (B) :</td>
                        <td>
                            <asp:Label ID="lblWorkedHour" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            Leave Hour (C) :</td>
                        <td>
                            <asp:Label ID="lblLeaveHour" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            (B + C) Hour :</td>
                        <td>
                            <asp:Label ID="lblWLHr" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            A - (B + C) Hour :</td>
                        <td>
                            <asp:Label ID="lblDiffHr" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                    </tr>
                </table>
            </fieldset>
                                    
                                    
            <div style="font-weight: bold; font-family: Arial; font-size: 12px; float: left; text-align:left";>
                Notes (If any):
                <br /><br />
                I certify by my signature that the above information is accurate. Timesheet must be signed by employee and supervisor.
                <br /><br /><br />                
            </div>
            
            <div style="width: 99%; font-family: Arial; font-size: 14px; float: left; text-align: left;">
                <table width="99%">
                    <tr>
                        <td style="width: 45%; text-align: left;">
                            <span style="font-weight: normal;">Signature:</span></td>
                            <td style="width: 45%; text-align: left;">
                            <span style="font-weight: normal;">Signature:</span></td>
                            </tr> 
                            <tr>
                        <td style="width: 45%; text-align: left;">
                            <span style="font-weight:normal;">Employee:</span>&nbsp;<asp:Label ID="lblEmpSig"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                                <td style="width: 45%; text-align: left;">
                            <span style="font-weight: normal;">Supervisor:</span>&nbsp;<asp:Label ID="lblSup"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                                </tr> 
                                <tr>
                        <td style="width: 45%; text-align: left;">
                            <span style="font-weight: normal;">Date:</span>&nbsp;<asp:Label ID="lblDate"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                                <td style="width: 45%; text-align: left;">
                            <span style="font-weight: normal;">Date:</span>&nbsp;<asp:Label ID="lblDateSup"
                                runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label></td>
                    </tr>                    
                </table>
                </div>
        </div>
    </form>
</body>
</html>
