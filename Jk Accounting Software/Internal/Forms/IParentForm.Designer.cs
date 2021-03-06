﻿namespace Jk_Accounting_Software.Internal.Forms
{
    partial class IParentForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IParentForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblSystemCaption = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelButton = new System.Windows.Forms.Panel();
            this.btnNavigatorHolder = new System.Windows.Forms.ToolStrip();
            this.btnFilter = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveFilter = new System.Windows.Forms.ToolStripButton();
            this.lblFind = new System.Windows.Forms.ToolStripLabel();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnFirstRecord = new System.Windows.Forms.ToolStripButton();
            this.NavigatorSeparatorFirst = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreviousRecord = new System.Windows.Forms.ToolStripButton();
            this.btnNextRecord = new System.Windows.Forms.ToolStripButton();
            this.NavigatorSeparatorSecond = new System.Windows.Forms.ToolStripSeparator();
            this.btnLastRecord = new System.Windows.Forms.ToolStripButton();
            this.btnHolder = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorPreview = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreview = new System.Windows.Forms.ToolStripSplitButton();
            this.btnCloseToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.flowLayoutPanelTop.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.btnNavigatorHolder.SuspendLayout();
            this.btnHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.flowLayoutPanelTop);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Size = new System.Drawing.Size(852, 476);
            this.splitContainer.SplitterDistance = 58;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 3;
            // 
            // flowLayoutPanelTop
            // 
            this.flowLayoutPanelTop.AutoSize = true;
            this.flowLayoutPanelTop.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelTop.Controls.Add(this.panelTop);
            this.flowLayoutPanelTop.Controls.Add(this.panelButton);
            this.flowLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelTop.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelTop.Name = "flowLayoutPanelTop";
            this.flowLayoutPanelTop.Size = new System.Drawing.Size(852, 58);
            this.flowLayoutPanelTop.TabIndex = 6;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Control;
            this.panelTop.Controls.Add(this.lblSystemCaption);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(852, 30);
            this.panelTop.TabIndex = 7;
            // 
            // lblSystemCaption
            // 
            this.lblSystemCaption.BackColor = System.Drawing.Color.Maroon;
            this.lblSystemCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSystemCaption.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemCaption.ForeColor = System.Drawing.Color.Gold;
            this.lblSystemCaption.Location = new System.Drawing.Point(0, 0);
            this.lblSystemCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblSystemCaption.Name = "lblSystemCaption";
            this.lblSystemCaption.Size = new System.Drawing.Size(815, 30);
            this.lblSystemCaption.TabIndex = 10;
            this.lblSystemCaption.Text = "Parent Form";
            this.lblSystemCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Maroon;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnClose.Location = new System.Drawing.Point(815, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(37, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "x";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCloseToolTip.SetToolTip(this.btnClose, "Close (Ctrl+F4)");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // panelButton
            // 
            this.panelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButton.BackColor = System.Drawing.SystemColors.Control;
            this.panelButton.Controls.Add(this.btnNavigatorHolder);
            this.panelButton.Controls.Add(this.btnHolder);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 30);
            this.panelButton.Margin = new System.Windows.Forms.Padding(0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(852, 26);
            this.panelButton.TabIndex = 12;
            // 
            // btnNavigatorHolder
            // 
            this.btnNavigatorHolder.BackColor = System.Drawing.SystemColors.Control;
            this.btnNavigatorHolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNavigatorHolder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNavigatorHolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFilter,
            this.btnRemoveFilter,
            this.lblFind,
            this.txtFind,
            this.btnReset,
            this.btnFirstRecord,
            this.NavigatorSeparatorFirst,
            this.btnPreviousRecord,
            this.btnNextRecord,
            this.NavigatorSeparatorSecond,
            this.btnLastRecord});
            this.btnNavigatorHolder.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.btnNavigatorHolder.Location = new System.Drawing.Point(340, 0);
            this.btnNavigatorHolder.Name = "btnNavigatorHolder";
            this.btnNavigatorHolder.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btnNavigatorHolder.Size = new System.Drawing.Size(512, 26);
            this.btnNavigatorHolder.Stretch = true;
            this.btnNavigatorHolder.TabIndex = 18;
            this.btnNavigatorHolder.Text = "toolStrip1";
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(53, 23);
            this.btnFilter.Text = "Filter";
            this.btnFilter.ToolTipText = "Shows the filter selection window";
            // 
            // btnRemoveFilter
            // 
            this.btnRemoveFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveFilter.Image")));
            this.btnRemoveFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveFilter.Name = "btnRemoveFilter";
            this.btnRemoveFilter.Size = new System.Drawing.Size(99, 23);
            this.btnRemoveFilter.Text = "Remove Filter";
            // 
            // lblFind
            // 
            this.lblFind.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFind.Image = ((System.Drawing.Image)(resources.GetObject("lblFind.Image")));
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(46, 23);
            this.lblFind.Text = "Find";
            this.lblFind.ToolTipText = "Filters the record (Ctrl+F)";
            // 
            // txtFind
            // 
            this.txtFind.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(125, 26);
            this.txtFind.ToolTipText = "Filters the record (Ctrl+F)";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.SteelBlue;
            this.btnReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(39, 23);
            this.btnReset.Text = "Reset";
            this.btnReset.ToolTipText = "Clears the current search";
            // 
            // btnFirstRecord
            // 
            this.btnFirstRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnFirstRecord.Image")));
            this.btnFirstRecord.ImageTransparentColor = System.Drawing.Color.White;
            this.btnFirstRecord.Margin = new System.Windows.Forms.Padding(0);
            this.btnFirstRecord.Name = "btnFirstRecord";
            this.btnFirstRecord.Size = new System.Drawing.Size(23, 26);
            this.btnFirstRecord.Tag = "1";
            this.btnFirstRecord.ToolTipText = "Shows the first record (Ctrl+Home)";
            // 
            // NavigatorSeparatorFirst
            // 
            this.NavigatorSeparatorFirst.Name = "NavigatorSeparatorFirst";
            this.NavigatorSeparatorFirst.Size = new System.Drawing.Size(6, 26);
            // 
            // btnPreviousRecord
            // 
            this.btnPreviousRecord.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPreviousRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousRecord.Image")));
            this.btnPreviousRecord.ImageTransparentColor = System.Drawing.Color.White;
            this.btnPreviousRecord.Margin = new System.Windows.Forms.Padding(0);
            this.btnPreviousRecord.Name = "btnPreviousRecord";
            this.btnPreviousRecord.Size = new System.Drawing.Size(31, 26);
            this.btnPreviousRecord.Tag = "2";
            this.btnPreviousRecord.Text = " ";
            this.btnPreviousRecord.ToolTipText = "Shows the previous record (Ctrl+Left)";
            // 
            // btnNextRecord
            // 
            this.btnNextRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnNextRecord.Image")));
            this.btnNextRecord.ImageTransparentColor = System.Drawing.Color.White;
            this.btnNextRecord.Margin = new System.Windows.Forms.Padding(0);
            this.btnNextRecord.Name = "btnNextRecord";
            this.btnNextRecord.Size = new System.Drawing.Size(31, 26);
            this.btnNextRecord.Tag = "3";
            this.btnNextRecord.Text = " ";
            this.btnNextRecord.ToolTipText = "Shows the next record (Ctrl+Right)";
            // 
            // NavigatorSeparatorSecond
            // 
            this.NavigatorSeparatorSecond.Name = "NavigatorSeparatorSecond";
            this.NavigatorSeparatorSecond.Size = new System.Drawing.Size(6, 26);
            // 
            // btnLastRecord
            // 
            this.btnLastRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnLastRecord.Image")));
            this.btnLastRecord.ImageTransparentColor = System.Drawing.Color.White;
            this.btnLastRecord.Margin = new System.Windows.Forms.Padding(0);
            this.btnLastRecord.Name = "btnLastRecord";
            this.btnLastRecord.Size = new System.Drawing.Size(31, 26);
            this.btnLastRecord.Tag = "4";
            this.btnLastRecord.Text = " ";
            this.btnLastRecord.ToolTipText = "Shows the last record (Ctrl+End)";
            // 
            // btnHolder
            // 
            this.btnHolder.BackColor = System.Drawing.SystemColors.Control;
            this.btnHolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnEdit,
            this.btnSave,
            this.btnCancel,
            this.toolStripSeparatorPreview,
            this.btnPreview});
            this.btnHolder.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.btnHolder.Location = new System.Drawing.Point(0, 0);
            this.btnHolder.Name = "btnHolder";
            this.btnHolder.Size = new System.Drawing.Size(310, 26);
            this.btnHolder.Stretch = true;
            this.btnHolder.TabIndex = 16;
            this.btnHolder.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(51, 23);
            this.btnNew.Text = "New";
            this.btnNew.ToolTipText = "Create new transaction (Ctrl+N)";
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(47, 23);
            this.btnEdit.Text = "Edit";
            this.btnEdit.ToolTipText = "Modify the transaction (Ctrl+E)";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 23);
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "Save the transaction (Ctrl+S)";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 23);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ToolTipText = "Cancel the current transaction (Esc)";
            // 
            // toolStripSeparatorPreview
            // 
            this.toolStripSeparatorPreview.Name = "toolStripSeparatorPreview";
            this.toolStripSeparatorPreview.Size = new System.Drawing.Size(6, 26);
            // 
            // btnPreview
            // 
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(80, 23);
            this.btnPreview.Text = "Preview";
            this.btnPreview.ToolTipText = "Opens the printing window (Ctrl+P)";
            this.btnPreview.ButtonClick += new System.EventHandler(this.btnPreview_Click);
            // 
            // IParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IParentForm";
            this.Size = new System.Drawing.Size(852, 476);
            this.Resize += new System.EventHandler(this.IParentForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.flowLayoutPanelTop.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.btnNavigatorHolder.ResumeLayout(false);
            this.btnNavigatorHolder.PerformLayout();
            this.btnHolder.ResumeLayout(false);
            this.btnHolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTop;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblSystemCaption;
        protected System.Windows.Forms.ToolStrip btnNavigatorHolder;
        protected System.Windows.Forms.ToolStrip btnHolder;
        public System.Windows.Forms.ToolStripButton btnNew;
        public System.Windows.Forms.ToolStripButton btnEdit;
        public System.Windows.Forms.ToolStripButton btnSave;
        public System.Windows.Forms.ToolStripButton btnCancel;
        public System.Windows.Forms.ToolStripButton btnFirstRecord;
        public System.Windows.Forms.ToolStripButton btnPreviousRecord;
        public System.Windows.Forms.ToolStripButton btnNextRecord;
        public System.Windows.Forms.ToolStripButton btnLastRecord;
        private System.Windows.Forms.ToolTip btnCloseToolTip;
        public System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelButton;
        public System.Windows.Forms.ToolStripLabel lblFind;
        public System.Windows.Forms.ToolStripTextBox txtFind;
        public System.Windows.Forms.ToolStripButton btnReset;
        public System.Windows.Forms.ToolStripSplitButton btnPreview;
        public System.Windows.Forms.ToolStripButton btnFilter;
        public System.Windows.Forms.ToolStripSeparator NavigatorSeparatorFirst;
        public System.Windows.Forms.ToolStripSeparator NavigatorSeparatorSecond;
        public System.Windows.Forms.ToolStripButton btnRemoveFilter;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparatorPreview;
    }
}

