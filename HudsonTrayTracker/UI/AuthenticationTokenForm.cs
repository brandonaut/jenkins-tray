﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Spring.Context.Support;
using Hudson.TrayTracker.Utils;
using Hudson.TrayTracker.Entities;
using Common.Logging;

namespace Hudson.TrayTracker.UI
{
    public partial class AuthenticationTokenForm : DevExpress.XtraEditors.XtraForm
    {
        static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Project referenceProject { get; set; }

        public static AuthenticationTokenForm Instance
        {
            get
            {
                AuthenticationTokenForm instance = (AuthenticationTokenForm)ContextRegistry.GetContext().GetObject("AuthenticationTokenForm");
                return instance;
            }
        }

        public AuthenticationTokenForm()
        {
            InitializeComponent();

        }

        public void UpdateValues()
        {
            if (referenceProject != null)
            {
                projectLabel.Text = referenceProject.Name;
                TokentextBox.Text = referenceProject.AuthenticationToken;
                CausetextBox.Text = referenceProject.CauseText;
            }
        }

        public static void ShowDialogOrFocus(Project project)
        {
            Instance.referenceProject = project;
            Instance.UpdateValues();

            if (Instance.Visible)
                Instance.Focus();
            else
                Instance.ShowDialog();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            if (referenceProject != null)
            {
                if (referenceProject.AuthenticationToken != TokentextBox.Text.Trim())
                    referenceProject.AuthenticationToken = TokentextBox.Text.Trim();

                if (referenceProject.CauseText != CausetextBox.Text.Trim())
                    referenceProject.CauseText = CausetextBox.Text.Trim();
            }
            this.Close();
        }
    }
}
