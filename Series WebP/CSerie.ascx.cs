using System;
using System.Web.UI;
using System.Data;

public partial class CSerie : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataRow filax = Globales.datos.Tables["series"].Rows[Globales.pos];
        numSerie = filax["numSerie"].ToString();
        textobusqueda = filax["textobusqueda"].ToString();
        textodespues = filax["textodespues"].ToString();
        Autoinc = Convert.ToInt32(filax["autoinc"]);
        numCap = filax["numcap"].ToString();
        tbnumcap.Text = numCap;
        nombre = filax["nombre"].ToString();
        hlNombre.Text = nombre;
        ConfigLinkButton();
        if (!Globales.casonueva) Globales.pos++;
    }
    Series padre
    {
        get { return (Series)Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent; }
    }

    public string numSerie = "1";
    public string textobusqueda = "";
    public string textodespues = "";
    public int Autoinc = 1;

    public string nombre;
    public string numCap;

    public CSerie()
    {
    }

    public CSerie(string numSeriex, string nombrex, string textobusquedax, string textodespuesx, int numCapx, int Autoincx)
    {
        numSerie = numSeriex;
        nombre = nombrex;
        textobusqueda = textobusquedax;
        textodespues = textodespuesx;
        numCap = numCapx.ToString();
        Autoinc = Autoincx;
    }
    private void ConfigLinkButton()
    {
        hlNombre.Target = "_blank";
        hlNombre.NavigateUrl = getURL();
    }

    private string getURL()
    {//se pierde lo de varios buscadores...revisar...
        DataRow[] filasbuscadores = Globales.datos.Tables["seriesbuscadores"].Select("numSerie=" + numSerie);
        if(filasbuscadores.Length>0)
        {
            DataRow buscador = Globales.datos.Tables["buscadores"].Select("numBuscador=" + filasbuscadores[0]["numBuscador"])[0];
            return buscador["link"].ToString().Replace("[textobusqueda]", textobusqueda + tbnumcap.Text.Trim() + textodespues);
        }
        return "";
    }

    /*protected void lbNombre_Click(object sender, EventArgs e)
    {
        DataRow[] filasbuscadores = Globales.datos.Tables["seriesbuscadores"].Select("numSerie=" + numSerie);
        if (filasbuscadores.Length < 1)
        {
            Funciones.Mensaje(Page, UniqueID, "No tiene ningun buscador asociado");
        }
        else
        {
            for (int i = 0; i < filasbuscadores.Length; i++)
            {
                DataRow buscador = Globales.datos.Tables["buscadores"].Select("numBuscador=" + filasbuscadores[i]["numBuscador"])[0];
                string linkbusqueda = buscador["link"].ToString().Replace("[textobusqueda]", textobusqueda.Replace(" ", "+") + "+" + tbnumcap.Text.Trim());
                Funciones.AbrirURL(Page, UniqueID, linkbusqueda);
            }
            try
            {
                tbnumcap.Text = (Convert.ToInt32(tbnumcap.Text) + Autoinc).ToString();
            }
            catch
            {
                //tbnumcap.Text = "1";
            }
        }
    }*/

    protected void numericNumCap_ValueChanged(object sender, EventArgs e)
    {
        actualizarnumCap();
    }
    protected void actualizarnumCap()
    {
        int filaseries = Globales.EncontrarFila(Globales.datos.Tables["series"], "numSerie", numSerie);
        Globales.datos.Tables["series"].Rows[filaseries]["numCap"] = tbnumcap.Text;
        Globales.GuardarDatos();
        Funciones.RecargarPagina(padre, padre.UniqueID);
    }

    protected void btnModificar_Click(object sender, ImageClickEventArgs e)
    {
        padre.mostrardatosserie(numSerie);
    }
    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        int filaseries = Globales.EncontrarFila(Globales.datos.Tables["series"], "numSerie", numSerie);
        Globales.datos.Tables["series"].Rows[filaseries].Delete();
        Globales.GuardarDatos();

        Visible = false;
        Dispose();
        Funciones.RecargarPagina(padre, padre.UniqueID);
    }
    protected void btnmenos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tbnumcap.Text = (Convert.ToInt32(tbnumcap.Text) - 1).ToString();
        }
        catch
        {
            tbnumcap.Text = "1";
        }
        actualizarnumCap();
    }
    protected void btnmas_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tbnumcap.Text = (Convert.ToInt32(tbnumcap.Text) + 1).ToString();
        }
        catch
        {
            tbnumcap.Text = "1";
        }
        actualizarnumCap();
    }
}
