using Microsoft.Reporting.WebForms;
using Plenamente.App_Tool;
using Plenamente.Models;
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
		private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Admin")]
        public ActionResult VerReporte(int id)
		{
            try
            {
                ReportViewer reportViewer =
                    new ReportViewer()
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Percentage(100),
                    };
                PlenamenteDataSet.ResumenCriteriosAutoEvaluacionDataTable data1 = new PlenamenteDataSet.ResumenCriteriosAutoEvaluacionDataTable();
                ResumenCriteriosAutoEvaluacionTableAdapter adapter1 = new ResumenCriteriosAutoEvaluacionTableAdapter();
                adapter1.Fill(data1, id, AccountData.NitEmpresa);
                if (data1 != null && data1.Rows.Count > 0)
                {
                    PlenamenteDataSet.ResumenAutoEvaluacionDataTable data = new PlenamenteDataSet.ResumenAutoEvaluacionDataTable();
                    ResumenAutoEvaluacionTableAdapter adapter = new ResumenAutoEvaluacionTableAdapter();
                    adapter.Fill(data, id);


                    if (data != null && data.Rows.Count > 0)
                    {
                        PlenamenteDataSet.ResumenEmpresaDataTable data2 = new PlenamenteDataSet.ResumenEmpresaDataTable();
                        ResumenEmpresaTableAdapter adapter2 = new ResumenEmpresaTableAdapter();
                        adapter2.Fill(data2, AccountData.NitEmpresa);

                        if (data2 != null && data2.Rows.Count > 0)
                        {
                            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsDatosEmpresa", data2.CopyToDataTable()));
                        }
                        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsAutoEvaluacion", data.CopyToDataTable()));

                    }
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DsResumenCriterios", data1.CopyToDataTable()));
                    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\rptAutoEvaluacion.rdlc.";
                    ViewBag.ReportViewer = reportViewer;
                }
                else
                {
                    ViewBag.TextError = "No hay data valida para esta auto evaluacion";
                }            
            }
                
            catch (Exception ex)
            {
                ViewBag.TextError = ex.Message;
            }

            return View();
		}
	}
}