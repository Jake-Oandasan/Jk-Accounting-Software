﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Free_Accounting_Software.Internal.Forms;
using Free_Accounting_Software.External.Datasources;
using Free_Accounting_Software.External.Datasources.ESubsidiaryLedgerReportDSTableAdapters;
using Microsoft.Reporting.WinForms;

namespace Free_Accounting_Software.External.Report
{
    public partial class ESubsidiaryLedgerReportForm : IReportForm
    {
        public ESubsidiaryLedgerReportForm()
        {
            InitializeComponent();
        }

        protected override void RefreshReport()
        {
            base.RefreshReport();

            int CompanyId = int.Parse(Parameters.Find(p => p.Name == "CompanyId").Value);
            DateTime FromDate = DateTime.Parse(Parameters.Find(p => p.Name == "FromDate").Value);
            DateTime ToDate = DateTime.Parse(Parameters.Find(p => p.Name == "ToDate").Value);
            ReportParameter[] reportParam = new ReportParameter[2];

            ESubsidiaryLedgerReportDS slDataSource = new ESubsidiaryLedgerReportDS();
            tblSubsidiaryLedgerTableAdapter SubsidiaryLedgerAdapter = new tblSubsidiaryLedgerTableAdapter();
            tblCompaniesTableAdapter CompanyAdapter = new tblCompaniesTableAdapter();

            SubsidiaryLedgerAdapter.Fill(slDataSource.tblSubsidiaryLedger, CompanyId, FromDate, ToDate);
            CompanyAdapter.Fill(slDataSource.tblCompanies, CompanyId);

            reportViewer.Reset();
            reportViewer.LocalReport.ReportPath = Properties.Settings.Default.ReportPath + "Subsidiary Ledger.rdlc";

            reportParam[0] = new ReportParameter("FromDate", FromDate.ToString("MM'/'dd'/'yyyy"), false);
            reportParam[1] = new ReportParameter("ToDate", ToDate.ToString("MM'/'dd'/'yyyy"), false);

            reportViewer.LocalReport.SetParameters(reportParam);

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("SubsidiaryLedger", slDataSource.Tables["tblSubsidiaryLedger"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Company", slDataSource.Tables["tblCompanies"]));
            reportViewer.RefreshReport();
        }
    }
}
