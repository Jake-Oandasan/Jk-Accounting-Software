﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using Jk_Accounting_Software.Internal.Forms;
using JkComponents;
using System.Drawing;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Jk_Accounting_Software.Internal.Classes
{
    public static class IAppHandler
    {
        public static List<IParentForm> Classes = new List<IParentForm>();
        public static Panel ParentPanel;
        public static DataTable Categories = new DataTable();
        public static DataTable SubCategories = new DataTable();
        public static ToolStripStatusLabel StatusLabel;
        public static ToolStripProgressBar StatusProgressBar;
        private static String InitialStatus;
        private static int CurrentTag = 0;

        public static String GetSubCategory(String Name, String Result)
        {
            try
            {
                return SubCategories.Select("Name = '" + Name + "'")[0][Result].ToString();
            }
            catch (Exception ex)
            {
                IMessageHandler.ShowError(ISystemMessages.DevelopmentError(ex.Message));
            }

            return null;
        }

        //This will store the opened forms for further retrieving rather than creating a new instance of the form
        public static void AddUsedForm(IParentForm PClassname)
        {
            Classes.Add(PClassname);
        }

        public static IParentForm FindForm(String PClassname, String Caption = null, bool IsReport = false)
        {
            IParentForm form = null;

            //check if form is already opened, to reuse it
            if (Caption != null)
            {
                if (SubCategories.Select(String.Format("ListForm = '{0}'", PClassname)).Length > 0
                    && SubCategories.Select(String.Format("ListForm = '{0}'", PClassname))[0]["Category"].ToString() == "Report")
                    form = Classes.Find(c => c.Name == PClassname && c.Caption == "Report");
                else
                    form = Classes.Find(c => c.Name == PClassname && c.Caption == Caption);

                if (form != null)
                    Classes.Remove(form);
            }

            //open new instance
            if (IsReport)
            {
                if (form == null && Type.GetType("Jk_Accounting_Software.External.Report." + PClassname) != null)
                    form = Activator.CreateInstance(Type.GetType("Jk_Accounting_Software.External.Report." + PClassname)) as IParentForm;
            }
            else
                foreach (DataRow category in Categories.Rows)
                {
                    if (form == null && Type.GetType("Jk_Accounting_Software.External." + category["Name"] + "." + PClassname) != null)
                        form = Activator.CreateInstance(Type.GetType("Jk_Accounting_Software.External." + category["Name"] + "." + PClassname)) as IParentForm;
                }

            //assign tag for navigation purposes of all opened forms
            if (form != null)
            {
                form.Parent = ParentPanel;
                form.Tag = CurrentTag;
                CurrentTag++;
            }

            return form;
        }

        public static IParentForm OpenPreviousForm(IParentForm form)
        {
            if (Classes.Find(c => c.Tag.ToString() == form.Tag.ToString() && c.Name == form.Name) != null)
                Classes.Remove(Classes.Find(c => c.Tag.ToString() == form.Tag.ToString() && c.Name == form.Name));

            if (Classes.Count > 0)
                return Classes[Classes.Count - 1];

            return null;
        }

        public static IParentForm FindActiveForm()
        {
            if (ParentPanel != null)
                foreach (Control control in ParentPanel.Controls)
                    if (control.Visible)
                        return control as IParentForm;

            return null;
        }

        public static void StartBusy(String status)
        {
            if (String.IsNullOrWhiteSpace(InitialStatus))
                InitialStatus = status;

            SetStatusLabel(status);
            if (Cursor.Current == Cursors.WaitCursor)
                return;
            
            Cursor.Current = Cursors.WaitCursor;
            StatusProgressBar.Value = 0;
            StatusProgressBar.Step = 10;
            StatusProgressBar.PerformStep();
        }

        public static void EndBusy(String status)
        {
            if (status != InitialStatus)
                return;

            Cursor.Current = Cursors.Default;
            StatusProgressBar.Step = 100;
            StatusProgressBar.PerformStep();
            SetStatusLabel("Done");
            InitialStatus = String.Empty;
        }

        private static void SetStatusLabel(String status)
        {
            StatusLabel.Text = status + "...";
            StatusLabel.GetCurrentParent().Refresh();
        }

        public static Object GetControlsValue(Control PControl)
        {
            Object result = null;

            if (PControl.GetType().Name == "TextBox")
            {
                if (!String.IsNullOrWhiteSpace((PControl as TextBox).Text))
                    result = (PControl as TextBox).Text;
            }
            else if (PControl.GetType().Name == "CheckBox")
                result = (PControl as CheckBox).Checked;
            else if (PControl.GetType().Name == "DateTimePicker")
            {
                if ((PControl as DateTimePicker).Value != null)
                    result = (PControl as DateTimePicker).Value.ToString();
            }
            else if (PControl.GetType().Name == "PictureBox")
                result = IImageHandler.ConvertImageToByte((PControl as PictureBox).Image);
            else if (PControl.GetType().Name == "JkLookUpComboBox")
            {
                if ((PControl as JkLookUpComboBox).SelectedKey != 0)
                    result = (PControl as JkLookUpComboBox).SelectedKey;
            }
            else if (PControl.GetType().Name == "JkTextBox")
            {
                if (!String.IsNullOrWhiteSpace((PControl as JkTextBox).Text))
                    result = (PControl as JkTextBox).Text;
            }

            return result;
        }

        public static void SetControlsValue(Control PControl, Object PValue)
        {
            if (PControl.GetType().Name == "TextBox")
            {
                if (PValue != null)
                {
                    if (PValue.GetType().Name != "String" && PValue.GetType().Name != "DBNull")
                        (PControl as TextBox).Text = double.Parse(PValue.ToString()).ToString("N2");
                    else
                        (PControl as TextBox).Text = Convert.ToString(PValue);

                }
            }
            else if (PControl.GetType().Name == "CheckBox")
                (PControl as CheckBox).Checked = Convert.ToBoolean(PValue);
            else if (PControl.GetType().Name == "DateTimePicker")
                (PControl as DateTimePicker).Value = new DateTime(Convert.ToDateTime(PValue).Year, Convert.ToDateTime(PValue).Month, Convert.ToDateTime(PValue).Day);
            else if (PControl.GetType().Name == "PictureBox")
                (PControl as PictureBox).Image = IImageHandler.ConvertByteToImage(PValue as byte[]);
            else if (PControl.GetType().Name == "JkLookUpComboBox")
            {
                if (PValue == DBNull.Value)
                    (PControl as JkLookUpComboBox).Text = String.Empty;
                else
                    (PControl as JkLookUpComboBox).SelectedKey = Convert.ToInt32(PValue);
            }
            else if (PControl.GetType().Name == "JkTextBox")
            {
                if (PValue != null)
                {
                    if (PValue.GetType().Name != "String" && PValue.GetType().Name != "DBNull")
                        (PControl as JkTextBox).Text = double.Parse(PValue.ToString()).ToString("N2");
                    else
                        (PControl as JkTextBox).Text = Convert.ToString(PValue);
                }
            }
        }

        public static void ClearControlsValue(Control PControl)
        {
            if (PControl.GetType().Name == "TextBox")
                (PControl as TextBox).Text = String.Empty;
            else if (PControl.GetType().Name == "CheckBox")
                (PControl as CheckBox).Checked = false;
            else if (PControl.GetType().Name == "PictureBox")
                (PControl as PictureBox).Image = null;
            else if (PControl.GetType().Name == "JkLookUpComboBox")
                (PControl as JkLookUpComboBox).Text = String.Empty;
            else if (PControl.GetType().Name == "JkTextBox")
                (PControl as JkTextBox).Text = String.Empty;
        }

        public static Object ConvertMaskValue(Object PDefaultValue)
        {
            Object value;

            switch (PDefaultValue.ToString())
            { 
                case "@CompanyId":
                    value = ISecurityHandler.CompanyId;
                    break;
                case "@SecurityUserId":
                    value = ISecurityHandler.SecurityUserId;
                    break;
                case "@Date":
                    ITransactionHandler VTransactionHandler = new ITransactionHandler();
                    DataTable VDataTable = new DataTable();

                    try
                    {
                        VTransactionHandler.LoadData("SELECT GETDATE() AS CurrentDate", ref VDataTable, null);
                        value = VDataTable.Rows[0][0].ToString();
                    }
                    finally
                    {
                        VDataTable.Dispose();
                    }
                    break;
                default:
                    value = PDefaultValue;
                    break;
            }

            return value;
        }

        public static String SetColumnsDefaultValue(String ColumnName)
        {
            String value = null;

            switch (ColumnName)
            { 
                case "CompanyId":
                    value = "@CompanyId";
                    break;
                case "CreatedById":
                    value = "@SecurityUserId";
                    break;
                case "DateCreated":
                    value = "@Date";
                    break;
                case "ModifiedById":
                    value = "@SecurityUserId";
                    break;
                case "DateModified":
                    value = "@Date";
                    break;
                case "Active":
                    value = "True";
                    break;
                case "Posted":
                    value = "True";
                    break;
                case "Voided":
                    value = "False";
                    break;
            }

            return value;
        }

        public static List<Control> FindControlByType(String type, Control start)
        {
            List<Control> list = new List<Control>();

            foreach (Control control in start.Controls)
            {
                if (control.GetType().Name == type && list.Find(l => (l as Control).Name == control.Name) == null)
                    list.Add(control);

                foreach (Control c in FindControlByType(type, control))
                    list.Add(c);
            }

            return list;
        }

        public static SqlDbType ConvertTypeToSqlType(Type type)
        {
            SqlParameter param;
            TypeConverter converter;

            param = new SqlParameter();
            converter = TypeDescriptor.GetConverter(param.DbType);

            if (converter.CanConvertFrom(type))
                param.DbType = (DbType)converter.ConvertFrom(type.Name);
            else
            {
                try
                {
                    param.DbType = (DbType)converter.ConvertFrom(type.Name);
                }
                catch { }
            }

            return param.SqlDbType;
        }

        public static void ApplyStyleOnGrid(DataGridView gridView)
        {
            gridView.GridColor = Color.Peru;
            gridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(180, 255, 200);
            gridView.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            gridView.BorderStyle = BorderStyle.Fixed3D;
        }

        public static DataGridViewColumn GetColumnByDataPropertyName(DataGridView grid, String DataPropertyName)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (column.DataPropertyName == DataPropertyName)
                    return column;
            }

            return null;
        }
    }
}