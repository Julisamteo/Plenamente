using Microsoft.Reporting.WebForms;
using Plenamente.PlenamenteDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Plenamente.Controllers
{
    public class ReportesController : Controller
    {
        
        public ActionResult VerReporte(int id)
        {
            ReportViewer reportViewer =
                    new ReportViewer()
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Percentage(100),
                    };
            PlenamenteDataSet.ResumenAutoEvaluacionDataTable data = new PlenamenteDataSet.ResumenAutoEvaluacionDataTable();
            ResumenAutoEvaluacionTableAdapter adapter = new ResumenAutoEvaluacionTableAdapter();
            adapter.Fill(data,id);


            if (data != null && data.Rows.Count > 0)
            {
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsAutoEvaluacion", data.CopyToDataTable()));
                reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptAutoEvaluacion.rdlc.";
                ViewBag.ReportViewer = reportViewer;
            }
            else
            {
                ViewBag.error("No se encontraron datos para el informe con los filtros utilizados, por favor utilice otros filtros");
            }    
            return View();
        }
    }
}