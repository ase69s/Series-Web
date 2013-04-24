using System;
using System.Data;

public static class Globales
{
    public static bool casonueva;
    public static int pos;
    public static DataSet datos = new DataSet("Datos");
    public static string diractual;
    public static void InicializarDatos(string diractualx)
    {
        datos.Tables.Clear();
        //diractual = System.IO.Directory.GetCurrentDirectory();
        diractual = diractualx;
        DataTable series = new DataTable("series");
        series.Columns.Add("numSerie", typeof(int));
        series.Columns.Add("Nombre", typeof(string));
        series.Columns.Add("Textobusqueda", typeof(string));
        series.Columns.Add("Textodespues", typeof(string));
        series.Columns.Add("numCap", typeof(int));
        series.Columns.Add("Autoinc", typeof(int));
        series.Columns[0].AutoIncrement = true;
        series.Columns[0].Unique = true;
        datos.Tables.Add(series);

        DataTable buscadores = new DataTable("buscadores");
        buscadores.Columns.Add("numBuscador", typeof(int));
        buscadores.Columns.Add("Nombre", typeof(string));
        buscadores.Columns.Add("Link", typeof(string));
        buscadores.Columns[0].AutoIncrement = true;
        buscadores.Columns[0].Unique = true;
        datos.Tables.Add(buscadores);

        DataTable seriesbuscadores = new DataTable("seriesbuscadores");
        seriesbuscadores.Columns.Add("numSerie", typeof(int));
        seriesbuscadores.Columns.Add("numBuscador", typeof(int));
        datos.Tables.Add(seriesbuscadores);
        DataTable navegador = new DataTable("navegador");
        navegador.Columns.Add("pathnavegador", typeof(string));
        datos.Tables.Add(navegador);
    }
    public static void CargarDatos()
    {
        if (!System.IO.File.Exists(diractual + "/DataSeries.xml")) return;
        string xmlData = System.IO.File.ReadAllText(diractual + "/DataSeries.xml"); //"<XmlDS><table1><col1>Value1</col1></table1><table1><col1>Value2</col1></table1></XmlDS>";
        System.IO.StringReader xmlSR = new System.IO.StringReader(xmlData);
        datos.ReadXml(xmlSR, XmlReadMode.IgnoreSchema);
    }
    public static void GuardarDatos(System.Web.UI.Page page)
    {
        try
        {
            GuardarDatos();
        }
        catch(Exception ex)
        {
            Funciones.EnviarMailError(ex);
            //Funciones.Mensaje(page, page.UniqueID, "Problema al guardar las series");
        }
    }
    public static void GuardarDatos()
    {
        datos.AcceptChanges();
        //string xml = datos.GetXml();
        System.IO.File.Delete(diractual + "/DataSeries.xml");
        datos.WriteXml(diractual + "/DataSeries.xml");
    }
    public static void AsociarSerieBuscador(string numSeriex, string numBuscadorx, System.Web.UI.Page page)
    {
        DataRow nuevaasociacion = datos.Tables["seriesbuscadores"].NewRow();
        nuevaasociacion["numSerie"] = numSeriex;
        nuevaasociacion["numBuscador"] = numBuscadorx;
        datos.Tables["seriesbuscadores"].Rows.Add(nuevaasociacion);

        GuardarDatos(page);
    }
    public static void DesasociarSerieBuscador(string numSerie, string numBuscador, System.Web.UI.Page page)
    {
        for (int i = 0; i < datos.Tables["seriesbuscadores"].Rows.Count; i++)
        {
            if (datos.Tables["seriesbuscadores"].Rows[i]["numSerie"].ToString() == numSerie || datos.Tables["seriesbuscadores"].Rows[i]["numBuscador"].ToString() == numBuscador)
            {
                datos.Tables["seriesbuscadores"].Rows[i].Delete();
            }
        }
        GuardarDatos(page);
    }
    public static int EncontrarFila(DataTable tablax, string campo, string valor)
    {
        int fila = -1;
        for (int i = 0; i < tablax.Rows.Count; i++)
        {
            if (tablax.Rows[i][campo].ToString() != valor) continue;
            fila = i;
            break;
        }
        return fila;
    }
}