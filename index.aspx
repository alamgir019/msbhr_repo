<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
<link rel="stylesheet" type="text/css" href="CSS/ie7_StyleSheet.css" />
<![endif]-->
</head>
<body>
    <div id="wrapper">
        <div id="header">
            <div id="banner">
                <div id="ide">                                            
                    <img src="Images/MSB-logo.jpg" width="168px" height="60px" alt="" />
                </div>
                <div id="company">
                    <div id="companyName">
                        HR & Payroll System
                    </div>
                    <div id="companyImg">
                    </div>
                </div>
            </div>
        </div>
        <div id="mainContent">
            <div class="row">
                <div class="ide-img">
                    <%--<img src="Images/ide-img3.png" />--%>
                </div>
                <div class="login-div" style="margin-top: 50px;">
                    <div class="loginbox radius" style="background-color:#2BAEEF;">
                        <h2 style="color: #FFFFFF; text-align: center">
                            Login</h2>
                        <div class="loginboxinner radius">
                            <!--loginheader-->
                            <div class="loginform">
                                <form runat="server">
                                    <div class="form-group" >
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
                                        <asp:Label ID="inputEmail" runat="server" Text="User Name" />
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtuserid" ></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblPass" runat="server" Text="Password" />
                                        <asp:TextBox TextMode="Password" runat="server" CssClass="form-control" ID="txtpassword"></asp:TextBox>
                                    </div>
                                    <div class="checkbox">
                                        <asp:CheckBox ID="chkRemember" runat="server" Text="Remember me" />
                                    </div>
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary"
                                        OnClick="btnLogin_Click" style="width:86%;" />
                                </form>
                            </div>
                            <!--loginform-->
                        </div>
                        <!--loginboxinner-->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
      
       <div class="copyright" >Copyright &copy <a href="http://www.baseltd.com/">BASE Limited</a></div>
    </div>
</body>
</html>
