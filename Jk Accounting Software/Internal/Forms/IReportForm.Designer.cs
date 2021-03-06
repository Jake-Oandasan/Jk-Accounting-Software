﻿namespace Jk_Accounting_Software.Internal.Forms
{
    partial class IReportForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerReport = new System.Windows.Forms.SplitContainer();
            this.toolStripReportParam = new System.Windows.Forms.ToolStrip();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FormFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VMasterDataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReport)).BeginInit();
            this.splitContainerReport.Panel1.SuspendLayout();
            this.splitContainerReport.Panel2.SuspendLayout();
            this.splitContainerReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormFooter
            // 
            this.FormFooter.Location = new System.Drawing.Point(0, 447);
            // 
            // splitContainer
            // 
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainerReport);
            this.splitContainer.Size = new System.Drawing.Size(836, 481);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.Location = new System.Drawing.Point(803, 0);
            // 
            // splitContainerReport
            // 
            this.splitContainerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerReport.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerReport.IsSplitterFixed = true;
            this.splitContainerReport.Location = new System.Drawing.Point(0, 0);
            this.splitContainerReport.Name = "splitContainerReport";
            this.splitContainerReport.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerReport.Panel1
            // 
            this.splitContainerReport.Panel1.Controls.Add(this.toolStripReportParam);
            // 
            // splitContainerReport.Panel2
            // 
            this.splitContainerReport.Panel2.Controls.Add(this.reportViewer);
            this.splitContainerReport.Size = new System.Drawing.Size(836, 422);
            this.splitContainerReport.SplitterDistance = 25;
            this.splitContainerReport.SplitterWidth = 1;
            this.splitContainerReport.TabIndex = 0;
            // 
            // toolStripReportParam
            // 
            this.toolStripReportParam.Location = new System.Drawing.Point(0, 0);
            this.toolStripReportParam.Name = "toolStripReportParam";
            this.toolStripReportParam.Size = new System.Drawing.Size(836, 25);
            this.toolStripReportParam.TabIndex = 6;
            this.toolStripReportParam.Text = "toolStrip1";
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(836, 376);
            this.reportViewer.TabIndex = 2;
            this.reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            this.reportViewer.ReportRefresh += new System.ComponentModel.CancelEventHandler(this.reportViewer_ReportRefresh);
            this.reportViewer.Back += new Microsoft.Reporting.WinForms.BackEventHandler(this.reportViewer_Back);
            this.reportViewer.Drillthrough += new Microsoft.Reporting.WinForms.DrillthroughEventHandler(this.reportViewer_Drillthrough);
            // 
            // IReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Caption = "Report";
            this.Name = "IReportForm";
            this.Size = new System.Drawing.Size(836, 481);
            this.SetupData += new Jk_Accounting_Software.Internal.Forms.IParentForm.SetupDataHandler(this.IReportForm_SetupData);
            this.SetupControl += new Jk_Accounting_Software.Internal.Forms.IParentForm.SetupControlHandler(this.IReportForm_SetupControl);
            this.Resize += new System.EventHandler(this.IReportForm_Resize);
            this.FormFooter.ResumeLayout(false);
            this.FormFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VMasterDataTable)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainerReport.Panel1.ResumeLayout(false);
            this.splitContainerReport.Panel1.PerformLayout();
            this.splitContainerReport.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerReport)).EndInit();
            this.splitContainerReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerReport;
        protected Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        protected System.Windows.Forms.ToolStrip toolStripReportParam;


    }
}
