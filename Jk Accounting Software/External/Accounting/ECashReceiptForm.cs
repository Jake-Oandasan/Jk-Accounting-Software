﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jk_Accounting_Software.Internal.Forms;
using JkComponents;
using System.Data.SqlClient;
using Jk_Accounting_Software.Internal.Classes;

namespace Jk_Accounting_Software.External.Accounting
{
    public partial class ECashReceiptForm : IMasterDetailForm
    {
        public ECashReceiptForm()
        {
            InitializeComponent();
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.CellClick += dataGridView_CellClick;
            dataGridView.CellMouseEnter += dataGridView_CellMouseEnter;

            dataGridViewPaymentDetails.CellEndEdit += dataGridViewPaymentDetails_CellEndEdit;
        }

        private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.GetCellIndex("AmountToApply")
                && dataGridView.EditMode != DataGridViewEditMode.EditProgrammatically
                && double.Parse(dataGridView.CurrentRow.Cells[dataGridView.GetCellIndex("AppliedAmount")].Value.ToString()) == 0)
            {
                MenuItem Apply = new MenuItem();

                Apply.Text = "Apply";

                Apply.Click += delegate(object s, EventArgs ea)
                {
                    int BalanceIndex = dataGridView.GetCellIndex("Balance");
                    int AmountAppliedIndex = dataGridView.GetCellIndex("AppliedAmount");
                    int AmountToApplyIndex = dataGridView.GetCellIndex("AmountToApply");

                    double AmountToApply = double.Parse(dataGridView.CurrentRow.Cells[AmountToApplyIndex].Value.ToString());
                    double Balance = double.Parse(dataGridView.CurrentRow.Cells[BalanceIndex].Value.ToString());

                    if (AmountToApply > Balance)
                        IMessageHandler.ShowError(ISystemMessages.AmountToApplyExceedsBalance);
                    else
                    {
                        dataGridView.CurrentRow.Cells[AmountAppliedIndex].Value = AmountToApply;
                        dataGridView.CurrentRow.Cells[AmountToApplyIndex].Value = 0;
                        DisplaySummary();
                    }
                };

                dataGridView.AddMenuItem(Apply);
            }
            else
                dataGridView.RemoveMenuItem("Apply");

            if (e.ColumnIndex == dataGridView.GetCellIndex("AppliedAmount")
                && dataGridView.EditMode != DataGridViewEditMode.EditProgrammatically
                && double.Parse(dataGridView.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) > 0)
            {
                MenuItem Unapply = new MenuItem();

                Unapply.Text = "Unapply";

                Unapply.Click += delegate(object s, EventArgs ea)
                {
                    int BalanceIndex = dataGridView.GetCellIndex("Balance");
                    int AppliedAmountIndex = dataGridView.GetCellIndex("AppliedAmount");
                    int AmountToApplyIndex = dataGridView.GetCellIndex("AmountToApply");

                    double appliedAmount = double.Parse(dataGridView.CurrentRow.Cells[AppliedAmountIndex].Value.ToString());

                    dataGridView.CurrentRow.Cells[BalanceIndex].Value = dstDetail.DataTable.Rows[e.RowIndex]["OldBalance"];
                    dataGridView.CurrentRow.Cells[AppliedAmountIndex].Value = 0;
                    dataGridView.CurrentRow.Cells[AmountToApplyIndex].Value = ComputeAmountToApply();
                    DisplaySummary();
                };

                dataGridView.AddMenuItem(Unapply);
            }
            else
                dataGridView.RemoveMenuItem("Unapply");
        }

        private void cmbSubsidiary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormState == FormStates.fsView || !EditingReady)
                return;

            String CommandText = "EXEC uspGetUnpaidInvoice @Id, @SubsidiaryId";
            SqlDataAdapter Adapter = new SqlDataAdapter(CommandText, Properties.Settings.Default.FreeAccountingSoftwareConnectionString);
            DataTable table = new DataTable();
            int Id = 0;

            try
            {
                Adapter.SelectCommand.Parameters.AddWithValue("@Id", Id);
                Adapter.SelectCommand.Parameters.AddWithValue("@SubsidiaryId", cmbSubsidiary.SelectedValue);
                Adapter.Fill(table);

                if (table.Rows.Count == 0)
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Index != dataGridView.NewRowIndex)
                            dataGridView.Rows.Remove(row); //clear the grid rather than the datatable
                    }

                    IMessageHandler.Inform(ISystemMessages.NoPendingInvoice);
                    return;
                }

                if (FormState == FormStates.fsNew ||
                    (FormState == FormStates.fsEdit && IMessageHandler.Confirm(ISystemMessages.RemovePaidInvoice) == DialogResult.Yes))
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Index != dataGridView.NewRowIndex)
                        {
                            dataGridView.Rows.Remove(row);
                        }
                    }
                }

                foreach (DataRow row in table.Rows)
                {
                    DataRow newRow = dstDetail.DataTable.NewRow();

                    foreach (DataColumn column in table.Columns)
                    {
                        newRow[column.ColumnName] = row[column.ColumnName];
                    }
                    dstDetail.DataTable.Rows.Add(newRow);
                }
            }
            finally
            {
                Adapter.Dispose();
                table.Dispose();
            }
        }

        private void LoadInvoices()
        {
            if (cmbSubsidiary.SelectedValue == null || cmbSubsidiary.SelectedValue == DBNull.Value)
                return;

            String CommandText = "EXEC uspGetUnpaidInvoice @Id, @SubsidiaryId";
            SqlDataAdapter Adapter = new SqlDataAdapter(CommandText, Properties.Settings.Default.FreeAccountingSoftwareConnectionString);
            DataTable table = new DataTable();
            int Id = int.Parse(Parameters.Find(p => p.Name == "Id").Value);

            try
            {
                Adapter.SelectCommand.Parameters.AddWithValue("@Id", Id);
                Adapter.SelectCommand.Parameters.AddWithValue("@SubsidiaryId", cmbSubsidiary.SelectedValue);
                Adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataRow dtRow in dstDetail.DataTable.Rows)
                    {
                        if (dtRow["SourceId"].ToString() == row["SourceId"].ToString())
                        {
                            dtRow.BeginEdit();
                            foreach (DataColumn column in table.Columns)
                            {
                                if (dstDetail.Columns.Find(c => c.Name == column.ColumnName).Temporary)
                                {
                                    dtRow[column.ColumnName] = row[column.ColumnName];
                                }
                            }
                            dtRow.EndEdit();
                        }
                    }
                }
            }
            finally
            {
                Adapter.Dispose();
                table.Dispose();
            }
        }

        private void dataGridViewPaymentDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DisplaySummary();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].DataPropertyName == "AmountToApply"
                && e.RowIndex != dataGridView.NewRowIndex)
            {
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ComputeAmountToApply();
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].DataPropertyName == "AmountToApply"
                && e.RowIndex != dataGridView.NewRowIndex)
            {
                int AppliedAmountIndex = dataGridView.GetCellIndex("AppliedAmount");

                //set readonly if Amount Applied has value
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = double.Parse(dataGridView.Rows[e.RowIndex].Cells[AppliedAmountIndex].Value.ToString()) > 0;
            }
        }

        protected override void UpdateControls()
        {
            base.UpdateControls();

            dataGridView.AllowUserToAddRows = false;
        }

        private void ShowAmountToApply()
        {
            DataGridViewColumn column = null;

            foreach (DataGridViewColumn c in dataGridView.Columns)
            {
                if (c.DataPropertyName == "AmountToApply")
                    column = c;
            }
            
            column.Visible = (FormState != FormStates.fsView);
        }

        private void ECashReceiptVoucherForm_SetupControl()
        {
            LoadInvoices();
            ShowAmountToApply();
            DisplaySummary();

            //load journal entry
            if (FormState == FormStates.fsView)
            {
                if (!dstJournalEntry.ZLoadGrid)
                    dstJournalEntry.ZLoadGrid = true;

                dstJournalEntry.Parameters[0].Value = this.MasterColumns.Find(mc => mc.Name == "JournalId").Value.ToString();
                dstJournalEntry.DataTable = VTransactionHandler.LoadData(dstJournalEntry.CommandText, dstJournalEntry.Parameters);
                dataGridViewJournalEntry.DataSource = dstJournalEntry.DataTable;
                dataGridViewJournalEntry.AutoGenerateColumns = false;

                tabPageJournalEntry.Text = String.Format("Journal Entry ({0})", dstJournalEntry.DataTable.Rows[0]["TransactionNo"].ToString());

                if (!tabControlDetails.TabPages.Contains(tabPageJournalEntry))
                    tabControlDetails.TabPages.Insert(1, tabPageJournalEntry);
            }
            else
            {
                tabPageJournalEntry.Text = "Journal Entry";
                tabControlDetails.TabPages.Remove(tabPageJournalEntry);
            }
        }

        private void DisplaySummary()
        {
            txtAmountPaid.Text = ComputeAmountPaid().ToString("#,##0.00;(#,##0.00)");
            txtAmountApplied.Text = ComputeAmountApplied().ToString("#,##0.00;(#,##0.00)");
            txtBalance.Text = (ComputeAmountPaid() - ComputeAmountApplied()).ToString("#,##0.00;(#,##0.00)");
        }

        private Double ComputeAmountPaid()
        {
            double value = 0;
            int AmountIndex = dataGridViewPaymentDetails.GetCellIndex("Amount");

            //get total amount of payment
            foreach (DataGridViewRow row in dataGridViewPaymentDetails.Rows)
            {
                if (row.Index != dataGridViewPaymentDetails.NewRowIndex)
                    value += double.Parse(row.Cells[AmountIndex].Value.ToString());
            }

            return value;
        }

        private Double ComputeAmountApplied()
        {
            double value = 0;
            int AppliedAmountIndex = dataGridView.GetCellIndex("AppliedAmount");

            //deduct total applied
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index != dataGridView.NewRowIndex)
                    value += double.Parse(row.Cells[AppliedAmountIndex].Value.ToString());
            }

            return value;
        }

        private Double ComputeRemainingBalance()
        {
            return ComputeAmountPaid() - ComputeAmountApplied();
        }

        private Double ComputeAmountToApply()
        {
            int BalanceIndex = dataGridView.GetCellIndex("Balance");
            double balance = double.Parse(dataGridView.CurrentRow.Cells[BalanceIndex].Value.ToString());

            if (balance > ComputeRemainingBalance())
                return ComputeRemainingBalance();
            else
                return balance;
        }

        private void ECashReceiptVoucherForm_ValidateSave()
        {
            if (ComputeAmountPaid() < ComputeAmountApplied())
            {
                IMessageHandler.ShowError(ISystemMessages.AmountPaidInsufficient);
                ValidationFails = true;
            }
        }

        private void Post(bool IsPost)
        {
            SqlCommand Command = new SqlCommand();

            //do not add try and catch statement, so that if error occurs
            //the exception will be raised on btnSave.Click event,
            //which will trigger transaction rollback
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "uspUpdateJournals";
            Command.Parameters.AddWithValue("@JournalTypeId", 4);
            Command.Parameters.AddWithValue("@Id", Parameters[0].Value);
            Command.Parameters.AddWithValue("@IsPost", IsPost);
            Command.Connection = VTransactionHandler.VConnection;
            Command.Transaction = VTransactionHandler.VTransaction;
            Command.ExecuteNonQuery();
        }

        protected override void Post()
        {
            base.Post();
            Post(true);
        }

        protected override void UnPost()
        {
            base.UnPost();
            Post(false);
        }

        private void ECashReceiptVoucherForm_BeforeSave()
        {
            for (int i = 0; i <= dataGridView.DataSet.DataTable.Rows.Count - 1; i++)
            {
                DataRow row = dataGridView.DataSet.DataTable.Rows[i];

                if (double.Parse(row["AppliedAmount"].ToString()) == 0)
                    row.Delete();
            }
        }
    }
}
