<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttendanceViewerRpt.aspx.cs" Inherits="Attendance_AttendanceViewerRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Attendance Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div style="margin-left:20px;margin-right:20px;">
     <div style="font-family:Arial;font-size:18px;">
        Marie Stopes Bangladesh
    </div>
    <div style="font-family:Arial;font-size:12px;">
       Attendance Record
    </div>
        <div style="width:90%; float:left;margin-bottom:5px;border-bottom:solid 1px #FF9933">
            <table>
                <tr>
                    <td>
                       <span style="font-family:Tahoma;font-size:12px;">From Date:</span> 
                    </td>
                    <td>
                        <asp:Label ID="lblFrom" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                    </td>
                     <td>
                       <span style="font-family:Tahoma;font-size:12px;">  To Date:</span> 
                    </td>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    <asp:GridView id="grAttnAdj" runat="server" Font-Size="12px" Width="90%" AutoGenerateColumns="False" DataKeyNames="SL,SunIn,SunOut,ArvlGrace,AttnPolicyId,LunchBreak,OTStartGrace,IsUpdated" EmptyDataText="No Record Found">
        <HeaderStyle BackColor="#3366CC" Font-Bold="True" HorizontalAlign="Left" ForeColor="whitesmoke" Font-Names="Tahoma" Font-Size="11px" />
        <SelectedRowStyle BackColor="#D1DDF1" CssClass="ListHeader" Font-Bold="True" ForeColor="#333333" />
        <AlternatingRowStyle BackColor="#EFF3FB" />
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
          <ItemStyle CssClass="ItemStylecss" Width="15%" />
        </asp:BoundField>
        <asp:BoundField DataField="AttndDate" HeaderText="Attn. Date">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        <asp:BoundField DataField="SignInTime" HeaderText="In Time">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        <asp:BoundField DataField="SignOutTime" HeaderText="Out Time">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
        </asp:BoundField>
        <asp:BoundField DataField="Status" HeaderText="Status">
          <ItemStyle CssClass="ItemStylecss" Width="7%" />
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
