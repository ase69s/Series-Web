using System;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class Series : Page
{
    private static bool modificacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            {
                padre = this;
                Globales.InicializarDatos(Server.MapPath(""));
                Globales.CargarDatos();
                //CargarSeries();
                ScriptManager1.SetFocus(PlaceHolder1);
                //Funciones.RecargarPagina(Page, UniqueID);
                //Funciones.Mensaje(this, UniqueID, "epatu");
            }
            CargarSeries();
        
    }
    #region buscadores
    protected void btnBuscadores_Click(object sender, ImageClickEventArgs e)
    {
        mostrarbuscadores();
    }
    protected void mostrarbuscadores()
    {
        cargarbuscadores();
        tbNombreBuscador.Text = "";
        tbLinkBuscador.Text = "[textobusqueda]";
        tablabuscadores.Visible = true;
        tablaseries.Visible = false;
    }
    protected void cargarbuscadores()
    {
        lbbuscadores.Items.Clear();
        foreach (DataRow filabuscador in Globales.datos.Tables["buscadores"].Rows)
        {
            lbbuscadores.Items.Add(filabuscador["Nombre"].ToString());
        }
        btnModificar.Enabled = false;
        btnQuitar.Enabled = false;
    }
    protected void btnAñadir_Click(object sender, EventArgs e)
    {
        DataRow nuevobuscador = Globales.datos.Tables["buscadores"].NewRow();
        nuevobuscador["Nombre"] = tbNombreBuscador.Text.Trim();
        nuevobuscador["Link"] = tbLinkBuscador.Text.Trim();
        Globales.datos.Tables["buscadores"].Rows.Add(nuevobuscador);
        cargarbuscadores();
        Globales.GuardarDatos(this);
    }

    protected void lbbuscadores_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbNombreBuscador.Text = Globales.datos.Tables["buscadores"].Rows[lbbuscadores.SelectedIndex]["Nombre"].ToString();
        tbLinkBuscador.Text = Globales.datos.Tables["buscadores"].Rows[lbbuscadores.SelectedIndex]["Link"].ToString();
        if (lbbuscadores.SelectedIndex <= -1) return;
        btnModificar.Enabled = true;
        btnQuitar.Enabled = true;
    }

    protected void btnQuitar_Click(object sender, EventArgs e)
    {
        try
        {
            Globales.DesasociarSerieBuscador("", Globales.datos.Tables["buscadores"].Rows[lbbuscadores.SelectedIndex]["numBuscador"].ToString(), this);
            Globales.datos.Tables["buscadores"].Rows[lbbuscadores.SelectedIndex].Delete();
            cargarbuscadores();
            Globales.GuardarDatos(this);
        }
        catch (Exception ex)
        {
            Funciones.EnviarMailError(ex);
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Globales.datos.Tables["buscadores"].Rows[lbbuscadores.SelectedIndex]["Nombre"] = tbNombreBuscador.Text.Trim();
            Globales.datos.Tables["buscadores"].Rows[lbbuscadores.SelectedIndex]["Link"] = tbLinkBuscador.Text.Trim();
            cargarbuscadores();
            Globales.GuardarDatos(this);
        }
        catch(Exception ex)
        {
            Funciones.EnviarMailError(ex);
        }
    }

    protected void tbNombreBuscador_TextChanged(object sender, EventArgs e)
    {
        btnAñadir.Enabled = comprobarcamposbuscador();
        ScriptManager1.SetFocus(tbLinkBuscador);
    }

    protected void tbLinkBuscador_TextChanged(object sender, EventArgs e)
    {
        btnAñadir.Enabled = comprobarcamposbuscador();
        ScriptManager1.SetFocus(lbbuscadores);
    }
    private bool comprobarcamposbuscador()
    {
        if (!string.IsNullOrEmpty(tbNombreBuscador.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(tbLinkBuscador.Text.Trim()))
            {
                if (tbLinkBuscador.Text.Contains("[textobusqueda]"))
                {
                    return true;
                }
                tbLinkBuscador.Text = "[textobusqueda]";
            }
        }
        return false;
    }

    protected void btnVolverBus_Click(object sender, EventArgs e)
    {
        tablabuscadores.Visible = false;
        tablaseries.Visible = true;
    }
    #endregion

    #region datosserie
    protected void btnNuevaSerie_Click(object sender, ImageClickEventArgs e)
    {
        mostrardatosserie("");
    }
    protected void tbNombreSerie_TextChanged(object sender, EventArgs e)
    {
        btnAceptar.Enabled = comprobarcamposserie();
        ScriptManager1.SetFocus(tbTextoBusquedaSerie);
    }
    protected void tbTextoBusquedaSerie_TextChanged(object sender, EventArgs e)
    {
        btnAceptar.Enabled = comprobarcamposserie();
        ScriptManager1.SetFocus(numCapSerie);
    }
    protected void numCapSerie_TextChanged(object sender, EventArgs e)
    {
        btnAceptar.Enabled = comprobarcamposserie();
        ScriptManager1.SetFocus(AutoincSerie);
    }
    protected void AutoincSerie_TextChanged(object sender, EventArgs e)
    {
        btnAceptar.Enabled = comprobarcamposserie();
    }
    private bool comprobarcamposserie()
    {
        if (!string.IsNullOrEmpty(tbNombreSerie.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(tbTextoBusquedaSerie.Text.Trim()))
            {
                int resul;
                if (int.TryParse(numCapSerie.Text, out resul))
                {
                    if (int.TryParse(AutoincSerie.Text, out resul))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    static int filaserie = -1;
    static string numSerie = "";
    public static Series padre;
    public void mostrardatosserie(String numSeriex)
    {
        if (string.IsNullOrEmpty(numSeriex))
        {
            modificacion = false;
            tablaasociarbuscadores.Visible = false;
            tbNombreSerie.Text = "";
            tbTextoBusquedaSerie.Text = "";
            numCapSerie.Text = "1";
            AutoincSerie.Text = "0";
        }
        else
        {
            numSerie = numSeriex;
            modificacion = true;
            tablaasociarbuscadores.Visible = true;
            filaserie = Globales.EncontrarFila(Globales.datos.Tables["series"], "numSerie", numSerie);
            if (filaserie == -1)
            {
                Funciones.EnviarMailError(new Exception("Problema al encontrar la serie a modificar en la tabla.NumSerie=" + numSerie));
                //Funciones.Mensaje(this, UniqueID, "Problema al encontrar la serie a modificar en la tabla.NumSerie=" + numSerie);
            }
            else
            {
                CargarDatosSerie(filaserie);
                CargarBuscadores();
            }
        }
        btnAceptar.Enabled = modificacion;
        tablaseries.Visible = false;
        tabladatosserie.Visible = true;
    }
    protected void CargarBuscadores()
    {
        lbAsignables.Items.Clear();
        lbAsignados.Items.Clear();
        string buscadoresasignados = "";
        if (!String.IsNullOrEmpty(numSerie))
        {
            buscadoresasignados = CargarBuscadoresAsignados();
        }
        CargarBuscadoresNoAsignados(buscadoresasignados);
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        tablaseries.Visible = true;
        tabladatosserie.Visible = false;
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (!modificacion)
        {
            DataRow nuevaserie = Globales.datos.Tables["series"].NewRow();
            nuevaserie["Nombre"] = tbNombreSerie.Text.Trim();
            nuevaserie["TextoBusqueda"] = tbTextoBusquedaSerie.Text.Trim();
            nuevaserie["TextoDespues"] =tbTextoDespues.Text.Trim();
            nuevaserie["numCap"] = numCapSerie.Text;
            nuevaserie["autoinc"] = AutoincSerie.Text;
            Globales.datos.Tables["series"].Rows.Add(nuevaserie);
        }
        else
        {
            Globales.datos.Tables["series"].Rows[filaserie]["Nombre"] = tbNombreSerie.Text.Trim();
            Globales.datos.Tables["series"].Rows[filaserie]["Textobusqueda"] = tbTextoBusquedaSerie.Text.Trim();
            Globales.datos.Tables["series"].Rows[filaserie]["TextoDespues"] = tbTextoDespues.Text.Trim();
            Globales.datos.Tables["series"].Rows[filaserie]["numCap"] = numCapSerie.Text;
            Globales.datos.Tables["series"].Rows[filaserie]["Autoinc"] = AutoincSerie.Text;
        }
        tablaseries.Visible = true;
        tabladatosserie.Visible = false;
        Globales.casonueva = true;
        CargarSeries();
        Globales.casonueva = false;
        Globales.GuardarDatos(this);
    }
    protected void CargarDatosSerie(int filaseriex)
    {
        tbNombreSerie.Text = Globales.datos.Tables["series"].Rows[filaseriex]["Nombre"].ToString();
        tbTextoBusquedaSerie.Text = Globales.datos.Tables["series"].Rows[filaseriex]["Textobusqueda"].ToString();
        tbTextoDespues.Text = Globales.datos.Tables["series"].Rows[filaseriex]["TextoDespues"].ToString();
        numCapSerie.Text = Globales.datos.Tables["series"].Rows[filaseriex]["numCap"].ToString();
        AutoincSerie.Text = Globales.datos.Tables["series"].Rows[filaseriex]["Autoinc"].ToString();
    }
    protected string CargarBuscadoresAsignados()
    {
        string buscadoresasignados = "";
        DataRow[] filasasignados = Globales.datos.Tables["seriesbuscadores"].Select("numSerie=" + numSerie);

        for (int i = 0; i < filasasignados.Length; i++)
        {
            string numbuscador = filasasignados[i]["numBuscador"].ToString();
            DataRow[] buscadorx = Globales.datos.Tables["buscadores"].Select("numBuscador=" + numbuscador);
            //lbAsignados.Items.Add(buscadorx[0]["Nombre"].ToString());
            lbAsignados.Items.Add(numbuscador + "-" + buscadorx[0]["Nombre"]);
            buscadoresasignados += "'" + numbuscador + "'";
            if (i != filasasignados.Length - 1)
            {
                buscadoresasignados += ",";
            }
        }
        return buscadoresasignados;
    }
    protected void CargarBuscadoresNoAsignados(string buscadoresasignadosx)
    {
        string where = String.IsNullOrEmpty(buscadoresasignadosx) ? "" : "numBuscador not in(" + buscadoresasignadosx + ")";
        DataRow[] buscadoresnoasignados = Globales.datos.Tables["buscadores"].Select(where);
        foreach (DataRow t in buscadoresnoasignados)
        {
            lbAsignables.Items.Add(t["numBuscador"] + "-" + t["Nombre"]);
        }
    }

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        char[] separador = new char[1];
        separador[0] = '-';
        for (int i = 0; i < lbAsignables.Items.Count; i++)
        {
            if (!lbAsignables.Items[i].Selected) continue;
            string item = lbAsignables.Items[i].Text;
            DataRow nuevaasociacion = Globales.datos.Tables["seriesbuscadores"].NewRow();
            nuevaasociacion["numSerie"] = numSerie;
            nuevaasociacion["numBuscador"] = item.Split(separador)[0];
            Globales.datos.Tables["seriesbuscadores"].Rows.Add(nuevaasociacion);
        }
        CargarBuscadores();
        Globales.GuardarDatos(this);
        CargarSeries();
    }
    protected void btnDesasignar_Click(object sender, EventArgs e)
    {
        char[] separador = new char[1];
        separador[0] = '-';
        for (int j = 0; j < lbAsignados.Items.Count; j++)
        {
            if (!lbAsignados.Items[j].Selected) continue;
            string item = lbAsignados.Items[j].Text;
            for (int i = 0; i < Globales.datos.Tables["seriesbuscadores"].Rows.Count; i++)
            {
                if (Globales.datos.Tables["seriesbuscadores"].Rows[i]["numSerie"].ToString() == numSerie &&
                    Globales.datos.Tables["seriesbuscadores"].Rows[i]["numBuscador"].ToString() ==
                    item.Split(separador)[0])
                {
                    Globales.datos.Tables["seriesbuscadores"].Rows[i].Delete();
                }
            }
        }
        CargarBuscadores();
        Globales.GuardarDatos(this);
        CargarSeries();
    }
    #endregion

    #region listaseries
    public void CargarSeries()
    {
        PlaceHolder1.Controls.Clear();
        for (Globales.pos = 0; Globales.pos < Globales.datos.Tables["series"].Rows.Count; Globales.pos++)
        {
            Control serie = LoadControl("CSerie.ascx");
            PlaceHolder1.Controls.Add(serie);
        }
        Globales.pos = 0;
    }
    protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
    {
        string ruta = Globales.diractual + "\\DataSeries.xml";
        Response.ClearContent();

        FileInfo file = new FileInfo(ruta);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/xml";
        Response.TransmitFile(file.FullName);
        Response.End();
    }
    protected void btnSubirFichs_Click(object sender, ImageClickEventArgs e)
    {
        tablaseries.Visible = false;
        tablaactualizar.Visible = true;
        CargarDirs();
        filarenombrar.Visible = false;
        filafich.Visible = false;
        filaupload.Visible = true;
        btnSubir.Visible = true;
        filabase.Visible = true;
    }
    #endregion

    #region actualizarfichs
    protected void CargarDirs()
    {
        lbDirectorio.Items.Clear();
        lbDirectorio.Items.Add("Raiz");
        lbDirectorio.Items[0].Value = Globales.diractual;
        string[] dirs = Directory.GetDirectories(Globales.diractual);
        foreach (string t in dirs)
        {
            lbDirectorio.Items.Add("");
            lbDirectorio.Items[lbDirectorio.Items.Count - 1].Value = t;
            lbDirectorio.Items[lbDirectorio.Items.Count - 1].Text = t.Replace(Globales.diractual, "Raiz");
        }
        lbDirectorio.SelectedIndex = 0;
    }
    protected void CargarFichs()
    {
        lbDirectorio.Items.Clear();
        //lbDirectorio.Items.Add("Raiz");
        //lbDirectorio.Items[0].Value = Globales.diractual;
        string[] fichs = Directory.GetFiles(Globales.diractual, "*", SearchOption.AllDirectories);
        foreach (string t in fichs)
        {
            lbDirectorio.Items.Add("");
            lbDirectorio.Items[lbDirectorio.Items.Count - 1].Value = t;
            lbDirectorio.Items[lbDirectorio.Items.Count - 1].Text = t.Replace(Globales.diractual, "Raiz");
        }
        lbDirectorio.SelectedIndex = 0;
    }
    protected void btnSubir_Click(object sender, EventArgs e)
    {
        /*if (FileUpload1.HasFile)
        {
            try
            {
                string destino = lbDirectorio.SelectedValue + "\\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
                File.Delete(destino);
                FileUpload1.SaveAs(destino);
                if (!File.Exists(destino))
                {
                    Funciones.EnviarMailError(new Exception("El archivo no se ha subido correctamente..."));
                    //Funciones.Mensaje(Page, UniqueID, "El archivo no se ha subido correctamente...");
                }
            }
            catch (Exception ex)
            {
                Funciones.EnviarMailError(ex);
                //Funciones.Mensaje(Page, UniqueID, "Error:" + ex.Message);
            }
        }
        else
        {
            Funciones.EnviarMailError(new Exception("El fileupload no encontro ningun archivo a subir..."));
            //Funciones.Mensaje(Page, UniqueID, "El fileupload no encontro ningun archivo a subir...");
        }*/
    }
    protected void btnVolverDir_Click(object sender, EventArgs e)
    {
        tablaactualizar.Visible = false;
        tablaseries.Visible = true;
    }
    protected void btnVistaFich_Click(object sender, EventArgs e)
    {
        if (btnVistaFich.Text.Contains("Fich"))
        {
            btnVistaFich.Text = "Vista Dir";
            filaupload.Visible = false;
            filafich.Visible = true;
            btnSubir.Visible = false;
            CargarFichs();
        }
        else
        {
            btnVistaFich.Text = "Vista Fich";
            filaupload.Visible = true;
            filafich.Visible = false;
            btnSubir.Visible = true;
            CargarDirs();
        }
    }
    protected void btnRenombrarFich_Click(object sender, EventArgs e)
    {
        lbDirectorio.Visible = false;
        filarenombrar.Visible = true;
        filaupload.Visible = false;
        filafich.Visible = false;
        filabase.Visible = false;
        tbRenombrarFich.Text = lbDirectorio.SelectedValue;//.Substring(lbDirectorio.SelectedValue.LastIndexOf("\\") + 1);
    }
    protected void btnAceptarRenombrar_Click(object sender, EventArgs e)
    {
        lbDirectorio.Visible = true;
        filarenombrar.Visible = false;
        filaupload.Visible = true;
        filafich.Visible = true;
        filabase.Visible = true;
        if (string.IsNullOrEmpty(tbRenombrarFich.Text)) return;
        File.Move(lbDirectorio.SelectedValue, tbRenombrarFich.Text);
        CargarFichs();
    }
    protected void btnCancelarRenombrar_Click(object sender, EventArgs e)
    {
        lbDirectorio.Visible = true;
        filarenombrar.Visible = false;
        filaupload.Visible = true;
        filafich.Visible = true;
        filabase.Visible = true;
    }
    protected void btnBorrarFich_Click(object sender, EventArgs e)
    {
        try
        {
            File.Delete(lbDirectorio.SelectedValue);
            CargarFichs();
        }
        catch (Exception ex)
        {
            Funciones.EnviarMailError(ex);
            //Funciones.Mensaje(Page, UniqueID, "Error:" + ex.Message);
        }
    }
    #endregion
}
