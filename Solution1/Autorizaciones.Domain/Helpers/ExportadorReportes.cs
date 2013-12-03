using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Linq;

namespace Core.Export
{
    /// <summary>
    /// Objeto para exportar reportes de Microsoft.
    /// </summary>
    [Serializable]
    public class ExportadorReportes : WebControl
    {
        #region Miembros

        /// <summary>
        /// Lista Fuertemente Tipada de los Parametros que recibe el reporte.
        /// </summary>
        public List<ReportParameter> Parameters { get; set; }

        /// <summary>
        /// El report viewer que renderea el reporte.
        /// </summary>
        public ReportViewer Report_Viewer { get; set; }

        /// <summary>
        /// Fuente de datos del reporte y subreportes. 
        /// </summary>
        public DataSet DataSet { get; set; }

        /// <summary>
        /// Ruta fisica en la que se encuentra el reporte. 
        /// se especifica de la forma "/~RuteFisicaDeReporte". 
        /// </summary>
        public string RutaReporte { get; set; }

        /// <summary>
        /// Formato de salida del reporte
        /// </summary>
        public Formato Formato { get; set; }

        /// <summary>
        /// Especifica la instancia de la pagina de la cual se esta llamando el exportador.
        /// </summary>
        public Page Page { get; set; }

        #endregion

        # region constructores
        /// </summary>
        /// <param name="parameters">Lista de parametros de reporte</param>
        /// <param name="ds">Data set </param>
        /// <param name="rutareporte">ruta fisica del reporte en el servidor</param>
        /// <param name="formato">formato de salida</param>
        /// <param name="page">Pagina aspx donde se va a renderizar el reporte</param>
        public ExportadorReportes(List<ReportParameter> parametros, DataSet ds, string rutareporte, Formato formato, System.Web.UI.Page page)
        {
            this.Parameters = parametros;
            this.DataSet = ds;
            this.RutaReporte = rutareporte;
            this.Formato = formato;
            this.Page = page;

            this.Report_Viewer = new ReportViewer();

            Report_Viewer.LocalReport.EnableExternalImages = true;

            Report_Viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(CargarSubReporte);
        }

        #endregion

        /// <summary>
        /// Metodo que genera el reporte para descargar
        /// </summary>
        public void GenerarReporte(bool descargar = false)
        {
            AddToPageForm();

            Report_Viewer.LocalReport.ReportPath = this.Page.MapPath(this.RutaReporte);

            SetDataSource();

            SetParameters();

            RenderReport(descargar);
        }

        private void SetParameters()
        {
            if (this.Parameters != null && this.Parameters.Count > 0)
            {
                Report_Viewer.LocalReport.SetParameters(this.Parameters);
            }
        }

        private void RenderReport(bool descargar)
        {
            Report_Viewer.DataBind();

            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            string[] streamIDs = null;
            Warning[] warnings = null;

            var FormatoNombre = GetNombreFormato(Formato);

            byte[] bytes = Report_Viewer.LocalReport.Render(FormatoNombre, null, out mimeType,
                 out encoding, out extension, out streamIDs, out warnings);

            Report_Viewer.Visible = false;

            this.Page.Response.Clear();
            this.Page.Response.ContentType = GetContentType(Formato);//"application/vnd.xls";
            this.Page.Response.AddHeader("Content-Length", bytes.Length.ToString());

            string disposition = descargar ? "attachment;filename=file." : "inline;filename=file.";

            this.Page.Response.AddHeader("content-disposition", disposition + GetExtencion(Formato));

            this.Page.Response.BinaryWrite(bytes);
        }

        /// <summary>
        /// Toma el DataSource correspondiente del Reporte de la Coleccion de tablas
        /// del Miembro DataSet.
        /// </summary>
        private void SetDataSource()
        {
            IList<string> DataSourceList = this.Report_Viewer.LocalReport.GetDataSourceNames();

            foreach (string DataSourceName in DataSourceList)
            {
                foreach (DataTable dt in this.DataSet.Tables)
                {
                    if (DataSourceName == dt.TableName)
                    {
                        this.Report_Viewer.LocalReport.DataSources.Add(new ReportDataSource(dt.TableName, dt));
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Este metodo agruega el control ReportViewer al formulario del cual se esta llamando
        /// para poder rendearlo.
        /// </summary>
        private void AddToPageForm()
        {
            foreach (System.Web.UI.Control ctrl in Page.Controls)
            {
                if (ctrl is System.Web.UI.HtmlControls.HtmlForm)
                {
                    ctrl.Controls.Add(this.Report_Viewer);
                    break;
                }
            }
        }

        /// <summary>
        /// Configura Sub-Reportes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CargarSubReporte(object sender, SubreportProcessingEventArgs e)
        {
            //Setiando DataSouces de SubReportes;
            IList<string> DataSouceNames = e.DataSourceNames;

            foreach (string DataSouceName in DataSouceNames)
            {
                foreach (DataTable dt in this.DataSet.Tables)
                {
                    if (DataSouceName == dt.TableName)
                    {
                        e.DataSources.Add(new ReportDataSource(dt.TableName, dt));
                    }
                }
            }
        }

        private string GetNombreFormato(Formato f)
        {
            switch (f)
            {
                case Formato.Excel:
                    {
                        return "Excel";
                    }
                case Formato.Pdf:
                    {
                        return "PDF";
                    }
                default:
                    {
                        return "Excel";
                    }
            }
        }

        private string GetContentType(Formato f)
        {
            switch (f)
            {
                case Formato.Excel:
                    {
                        return "application/vnd.xls"; ;
                    }
                case Formato.Pdf:
                    {
                        return "application/pdf";
                    }
                default:
                    {
                        return "application/vnd.xls"; ;
                    }
            }
        }

        private string GetExtencion(Formato f)
        {
            switch (f)
            {
                case Formato.Excel:
                    {
                        return "xls";
                    }
                case Formato.Pdf:
                    {
                        return "pdf";
                    }
                default:
                    {
                        return "xls";
                    }
            }
        }
    }

    /// <summary>
    /// Especifica el tipo de formato de salida al que se desea
    /// exportar el reporte.
    /// </summary>
    public enum Formato
    {
        Excel,
        Pdf
    }
}
