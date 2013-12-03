using Autorizaciones.Domain.Entities;
using Core.Export;
using Microsoft.Reporting.WebForms;
using Sigs.AutorizacionesOnline.ReportesDsTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigs.AutorizacionesOnline
{
    public partial class Imprimir : System.Web.UI.Page
    {
        //IReportesDataRepository repo;
        ExportadorReportes exporter;


        public string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["ArsDataContext"].ConnectionString;
            }
        }

        public Imprimir()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var valid = ValidatePeticion();

                if (valid)
                {
                    var reporte = (Reporte)Convert.ToInt32(GetNumeroReporte());

                    List<ReportParameter> param = new List<ReportParameter>();

                    switch (reporte)
                    {
                        case Reporte.FormularioAutorizacion:
                            {
                                var autorizacionId = int.Parse(GetParametro("autorizacionId"));

                                TbAutorizacionTableAdapter adp = new TbAutorizacionTableAdapter();
                                Sigs.AutorizacionesOnline.ReportesDs.TbAutorizacionDataTable table = new ReportesDs.TbAutorizacionDataTable();

                                adp.Fill(table, autorizacionId);

                                DataSet ds = new DataSet();
                                ds.Tables.Add(table);

                                param.Add(new ReportParameter("autorizacionId", autorizacionId.ToString()));
                                exporter = new ExportadorReportes(param, ds, "/Reportes/Autorizacion.rdlc", Formato.Pdf, this.Page);

                                exporter.GenerarReporte();

                                break;
                            }
                    }
                }
            }
        }

        public string GetNumeroReporte()
        {
            return Request.QueryString["Reporte"];
        }

        public string GetParametro(string parametro)
        {
            return Request.QueryString[parametro];
        }

        bool ValidatePeticion()
        {
            bool valid = true;
            var reporte = GetNumeroReporte();

            if (string.IsNullOrEmpty(reporte) || reporte == "0")
            {
                Response.Write("No ha especificado ningún reporte para mostrar. <br />");
                valid = false;
            }

            int numero = 0;
            if (!int.TryParse(reporte, out numero))
            {
                Response.Write("El número del reporte especificado nó es un número entero válido. <br />");
                valid = false;
            }

            return valid;
        }
    }
}