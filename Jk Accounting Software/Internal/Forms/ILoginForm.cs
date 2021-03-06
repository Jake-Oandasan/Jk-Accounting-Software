﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jk_Accounting_Software.Internal.Classes;
using System.Threading;

namespace Jk_Accounting_Software.Internal.Forms
{
    public partial class ILoginForm : Form
    {
        public ILoginForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ISecurityHandler.LoginCredentialAccepted(txtUsername.Text, txtPassword.Text))
            {
                AnimateProgressBar();
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                IMessageHandler.Inform(ISystemMessages.InvalidLoginCredential);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public static DialogResult LoginSuccessful()
        {
            ILoginForm login = new ILoginForm();
            try
            {
                IMessageHandler.MessageCaption = "Application";
                login.ShowDialog();
            }
            finally
            {
                login.Dispose();
            }

            return login.DialogResult;
        }

        private void txtKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk.PerformClick();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            IAppHandler.SetLabelColorOnEnter(lblUsername);
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            IAppHandler.SetLabelColorOnLeave(lblUsername);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            IAppHandler.SetLabelColorOnEnter(lblPassword);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            IAppHandler.SetLabelColorOnLeave(lblPassword);
        }

        private void AnimateProgressBar()
        {
            progressBar.Visible = true;

            for (int i = 10; i <= 100; i += 10)
            {
                progressBar.Value = i;
                progressBar.PerformStep();
                progressBar.Refresh();
                Thread.Sleep(50);
            }
        }
    }
}
