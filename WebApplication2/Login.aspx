<%@  Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebIsomedik.Login" %>

<!DOCTYPE html>
<meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Isomedik Proapps Login</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- favicon
		============================================ -->
    <html class="no-js" lang="en">
    <%--custome Loading--%>
        <script type="text/javascript" src="jscript/jquery1.2.3.js"></script>
        <script type="text/javascript" src="Loading/Loading.js"></script>
        <link rel="stylesheet" type="text/css" href="Loading/Loading.css" />

        <%--custome msgbox--%>
        <script type="text/javascript" src="jscript/jQuery1.8.3.js"></script>
        <script type="text/javascript" src="Cusmsgbox/Cusmsgbox.js"></script>
        <link rel="stylesheet" type="text/css" href="Cusmsgbox/Cusmsgbox.css" />
    <head>
    <link rel="shortcut icon" type="image/x-icon" href="images/icon.png" />
    		<link rel="stylesheet" href="bootstraps/bootstrap.css">
		<link rel="stylesheet" href="bootstraps/bootstrap-responsive.css">
        <link rel="stylesheet" href="bootstraps/maruti-login.css">
<%--        <link rel="stylesheet" type="text/css" href="bootstraps/maruti-style.css" />
        <link rel="stylesheet" type="text/css" href="bootstraps/maruti-media.css" class="skin-color" />--%>
        <link href="bootstraps/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <script src="bootstraps/jquery.js"></script>  
        <script src="bootstraps/maruti.js"></script> 
        <script src="bootstraps/bootstrap.js" type="text/javascript"></script>
</head>

<body >
    <!--[if lt IE 8]>
            <p class="browserupgrade">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
        <![endif]-->

<div id="loginbox">  
            <form  runat="server"  >
				 <div class="control-group normal_text"> <h3><img src="bootstraps/img/logoname.png" alt="Logo"></h3></div>
                    <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on"><i class="fa fa-user"></i></span><asp:TextBox ID="txtUserid" runat="server"  placeholder="Id Pengguna Testing.." name="UserId"  data-original-title="Isi Id Pengguna.." MaxLength="50" CssClass=" tip-bottom"></asp:TextBox>
                    
                        </div>
                    </div>
                </div>
                    <div class="control-group">
                    <div class="controls">
                        <div class="main_input_box">
                            <span class="add-on"><i class="fa fa-lock"></i></span><asp:TextBox ID="txtPassword" runat="server"  placeholder="Sandi Testing.." name="Sandi.." type="password"  data-original-title="Input your password" MaxLength="25" CssClass=" tip-bottom"></asp:TextBox>
                    
                        </div>
                    </div>
                </div>
                    <div class="form-actions normal_text">
                    <span class="pull-right"><asp:Button ID="BtnLogin" class="btn btn-custon-four btn-success tip-bottom" runat="server" Text="Masuk"    data-original-title="Click untuk Masuk"/>
                    </span>
                
            </form>
</div>
<%--<div id="loader" class="loading" style="text-align: 'center'">
            <table>
                <tr>
                    <td>
                        <i class='fa fa-refresh fa-spin'></i></td>
                    <td>&nbsp;&nbsp;Loading. Please wait...</td>
                </tr>
            </table>
        </div>
 --%>
</body>

</html>
