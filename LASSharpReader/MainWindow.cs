﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LASSharpReader
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            string text;
            if (string.Compare(fileLabel.Text, "Loaded file: ") <= 0)
            {
                text = "No file loaded";
            }
            else
            {
                text = fileLabel.Text;
            }
            loadedFileLabelTooltip.SetToolTip(fileLabel, text);
        }

        private void loadLASButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openLASDialog.ShowDialog( this );

            if (result == DialogResult.OK)
            {
                fileLabel.Text = "File loaded: " + openLASDialog.FileName;
                loadedFileLabelTooltip.SetToolTip(fileLabel, openLASDialog.FileName);
            }
        }
    }
}