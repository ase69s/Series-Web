<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Series.aspx.cs" Inherits="Series" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link id="StyleSheet" href="StyleSheet.css" rel="stylesheet" type="text/css" />
<head runat="server">
    <title></title>
    <style type="text/css">
        .style6
        {
            width: 65px;
        }
        .style10
        {
            height: 28px;
        }
        .style11
        {
            width: 65px;
            height: 28px;
        }
        .style12
        {
            height: 26px;
        }
        .UpdateProgress1 
        {
        	position:absolute; 
        	z-index:1; 
            width: 250px; 
            top: 100px;
            text-align:center;
        }
        .style13
        {
            height: 32px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 250px; height: 300px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
        <ProgressTemplate>
        <div class="UpdateProgress1"><img src="images/ajax-loader2a.gif" width="40px" alt="" /></div>
        
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="border-style: ridge; border-width: medium; width: 100%; height: 100%;"
                border="1" cellpadding="0" cellspacing="0">
                <tr>

                        
                                <td style="border-style: none">
                                    <table id="tablaseries" runat="server" 
                                        style="width: 250px; height: 100%; background-color: #A9D8AB;" cellpadding="0" 
                                        cellspacing="0">
                                        <tr>
                                            <td style="border-bottom-style: ridge; border-bottom-width: medium">
                                                &nbsp;<asp:ImageButton ID="btnNuevaSerie" runat="server" 
                                                    ImageUrl="~/images/anadir.gif" onclick="btnNuevaSerie_Click" 
                                                    Width="20px" />
                                                &nbsp;<asp:ImageButton ID="btnBuscadores" runat="server" 
                                                    ImageUrl="~/images/finders.gif" onclick="btnBuscadores_Click" 
                                                    Width="20px" />
                                                &nbsp;<asp:ImageButton ID="btnDescargar" runat="server" 
                                                    ImageUrl="~/images/download.gif" onclick="btnDescargar_Click" 
                                                    Width="20px" />
                                                &nbsp;<asp:ImageButton ID="btnSubirFichs" runat="server" 
                                                    ImageUrl="~/images/update.gif" onclick="btnSubirFichs_Click" 
                                                    Width="20px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-style: none">
                                    <table style="width: 250px; height: 100%; background-color: #CCCCFF;" id="tablabuscadores"
                                        runat="server" visible="false" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="style11">
                                                &nbsp; Nombre:
                                            </td>
                                            <td align="center" class="style10">
                                                <asp:TextBox ID="tbNombreBuscador" runat="server" AutoPostBack="True" OnTextChanged="tbNombreBuscador_TextChanged"
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style11">
                                                &nbsp; Link:
                                            </td>
                                            <td align="center" class="style10">
                                                <asp:TextBox ID="tbLinkBuscador" runat="server" Width="170px" OnTextChanged="tbLinkBuscador_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style11">
                                                &nbsp; Actuales:
                                            </td>
                                            <td class="style10">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style6" align="center">
                                                <asp:Button ID="btnAñadir" runat="server" OnClick="btnAñadir_Click" Text="Añadir"
                                                    Width="60px" />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Text="Cambiar"
                                                    Width="60px" />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnQuitar" runat="server" OnClick="btnQuitar_Click" Text="Quitar"
                                                    Width="60px" />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnVolverBus" runat="server" OnClick="btnVolverBus_Click" 
                                                    Text="Volver" Width="60px" />
                                            </td>
                                            <td align="center">
                                                <asp:ListBox ID="lbbuscadores" runat="server" AutoPostBack="True" Height="150px"
                                                    OnSelectedIndexChanged="lbbuscadores_SelectedIndexChanged" Width="150px"></asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-style: none">
                                    <table style="width: 250px; height: 100%; background-color: #99CCFF;" id="tabladatosserie"
                                        runat="server" visible="false" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="style10">
                                                &nbsp; Nombre:
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="tbNombreSerie" runat="server" AutoPostBack="True" OnTextChanged="tbNombreSerie_TextChanged"
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style10">
                                                &nbsp; Busqued:
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="tbTextoBusquedaSerie" runat="server" AutoPostBack="True" OnTextChanged="tbTextoBusquedaSerie_TextChanged"
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                                                                <tr>
                                            <td class="style10">
                                                &nbsp; Txtdesp:
                                            </td>
                                            <td class="style10">
                                                <asp:TextBox ID="tbTextoDespues" runat="server" AutoPostBack="False" 
                                                    Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style12" colspan="2">
                                                &nbsp; Capitulo actual:
                                                <asp:TextBox ID="numCapSerie" runat="server" AutoPostBack="True" OnTextChanged="numCapSerie_TextChanged"
                                                    Width="60px">1</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style10" colspan="2">
                                                &nbsp; Autoincremento:
                                                <asp:TextBox ID="AutoincSerie" runat="server" AutoPostBack="True" OnTextChanged="AutoincSerie_TextChanged"
                                                    Width="60px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="style10" colspan="2" valign="middle">
                                                <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar"
                                                    Width="60px" Height="22px" Enabled="False" />
                                                &nbsp;<asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar"
                                                    Width="60px" Height="22px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table style="width: 100%;" id="tablaasociarbuscadores" runat="server">
                                                    <tr>
                                                        <td>
                                                            Asignados
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            Asignables
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ListBox ID="lbAsignados" runat="server" Height="95px"
                                                                Width="110px"></asp:ListBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnAsignar" runat="server" OnClick="btnAsignar_Click" Text="&lt;"
                                                                Width="15px" />
                                                            <br />
                                                            <br />
                                                            <asp:Button ID="btnDesasignar" runat="server" OnClick="btnDesasignar_Click"
                                                                Text="&gt;" Width="15px" />
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lbAsignables" runat="server" Height="95px"
                                                                Width="110px"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border-style: none">
                                    <table style="width: 250px; height: 100%; background-color: #FFCC99;" id="tablaactualizar"
                                        runat="server" visible="false" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="style10">
                                                &nbsp; Carpetas Posibles:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style12" align="center">
                                                <asp:ListBox ID="lbDirectorio" runat="server" 
                                                    Height="185px" 
                                                    Width="240px"></asp:ListBox>
                                                
                                            </td>
                                        </tr>
                                        <tr id="filarenombrar" runat="server" visible="false">
                                            <td align="center" valign="middle">
<asp:TextBox ID="tbRenombrarFich" runat="server" 
                                                    OnTextChanged="tbTextoBusquedaSerie_TextChanged" Width="240px"></asp:TextBox>
                                                <br />
                                                <asp:Button ID="btnAceptarRenombrar0" runat="server" 
                                                    OnClick="btnAceptarRenombrar_Click" Text="Aceptar" Width="55px" 
                                                    onclientclick="22px" />
                                                &nbsp;<asp:Button ID="btnCancelarRenombrar" runat="server" 
                                                    OnClick="btnCancelarRenombrar_Click" onclientclick="22px" Text="Cancelar" 
                                                    Width="55px" />
                                            </td>
                                        </tr>
                                        <tr id="filaupload" runat="server">
                                            <td class="style13" align="center" valign="middle">
                                               <!-- <asp:FileUpload ID="FileUpload1" runat="server" Width="240px" /> -->
                                            </td>
                                        </tr>
                                        <tr  id="filafich" runat="server" visible="false">
                                            <td align="center" class="style10" valign="middle">
                                                <asp:Button ID="btnRenombrarFich" runat="server" Height="22px" 
                                                    OnClick="btnRenombrarFich_Click" Text="Renombrar" Width="75px" />
                                                &nbsp;<asp:Button ID="btnBorrarFich" runat="server" Height="22px" 
                                                    OnClick="btnBorrarFich_Click" Text="Borrar" Width="60px" />
                                            </td>
                                        </tr>
                                        <tr  id="filabase" runat="server">
                                            <td align="center" class="style10" valign="middle">
                                                <asp:Button ID="btnSubir" runat="server" Height="22px" OnClick="btnSubir_Click" 
                                                    Text="Subir" Width="60px" />
                                                &nbsp;<asp:Button ID="btnVolverDir" runat="server" Height="22px" 
                                                    OnClick="btnVolverDir_Click" Text="Volver" Width="60px" />
                                                &nbsp;<asp:Button ID="btnVistaFich" runat="server" Height="22px" 
                                                    OnClick="btnVistaFich_Click" Text="Vista Fich" Width="70px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDescargar" />
            <asp:PostBackTrigger ControlID="btnSubir" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
