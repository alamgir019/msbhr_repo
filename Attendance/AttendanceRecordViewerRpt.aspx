<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttendanceRecordViewerRpt.aspx.cs" Inherits="Attendance_AttendanceRecordViewerRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attendance Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div style="margin-left:20px;margin-right:20px;">
     <div style="font-family:Arial;font-size:34px;">
        Marie Stopes Bangladesh
    </div>
    <div style="font-family:Arial;font-size:20px;">
       Attendance Record
    </div>
        <div style="width:99%; float:left;margin-bottom:5px;border-bottom:solid 1px #FF9933">
            <table>
                <tr>
                    <td>
                       <span style="font-family:Tahoma;font-size:18px;">From Date:</span> 
                    </td>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text="" Font-Names="Tahoma" Font-Size="18px"></asp:Label>
                    </td>
                     <td>
                       <span style="font-family:Tahoma;font-size:18px;">  To Date:</span> 
                    </td>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="" Font-Names="Tahoma" Font-Size="18px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    <asp:GridView id="grAttnAdj" runat="server" Font-Size="16px" Width="99%" AutoGenerateColumns="False" DataKeyNames="SL,SunIn,SunOut,ArvlGrace,AttnPolicyId,LunchBreak,OTStartGrace,IsUpdated" EmptyDataText="No Record Found">
        <HeaderStyle BackColor="#3366CC" Font-Bold="True" HorizontalAlign="Left" ForeColor="whitesmoke" Font-Names="Tahoma" Font-Size="16px" />
        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="16px"/>
        <RowStyle Font-Size="16px" />
        <Columns>
        <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
          <ItemStyle CssClass="ItemStylecss" Width="5%" />
        </asp:BoundField>
        <asp:BoundField DataField="FullName" HeaderText="Employee Name">
          <ItemStyle CssClass="ItemStylecss" Width="16%" />
        </asp:BoundField>
        <asp:BoundField DataField="JobTitle" HeaderText="Designation">
          <ItemStyle CssClass="ItemStylecss" Width="15%" />
        </asp:BoundField>
        <asp:BoundField DataField="DeptName" HeaderText="Team">
          <ItemStyle CssClass="ItemStylecss" Width="14%" />
        </asp:BoundField>
        <asp:BoundField DataField="AttndDate" HeaderText="Attn. Date">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        <asp:BoundField DataField="SignInTime" HeaderText="In Time">
          <ItemStyle CssClass="ItemStylecss" Width="8%" />
        </asp:BoundField>
        <asp:BoundField DataField="SignOutTime" HeaderText="Out Time">
          <ItemStyle CssClass="ItemStylecss" Width="8%" />
        </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="Status">
          <ItemStyle CssClass="ItemStylecss" Width="6%" />
        </asp:BoundField>
        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        <asp:BoundField DataField="PolicyName" HeaderText="Shift Held">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        <asp:BoundField DataField="isUpdatedManually" HeaderText="Is Manual">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        </Columns>
      </asp:GridView>
    </div>
    </form>
</body>
</html>
