using System;
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
        private LASFileReader lasReader = new LASFileReader();

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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openLASDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string filePath = openLASDialog.FileName;
                string errorMessage = "";
                bool valid = lasReader.ReadFile(filePath, ref errorMessage);

                if(!valid)
                {
                    MessageBox.Show( "Error loading file: " + filePath + '\n' + errorMessage, "LASSharpReader");
                    fileLabel.Text = "Loaded file: No file loaded.";
                    loadedFileLabelTooltip.SetToolTip(fileLabel, filePath);
                }
                else
                {
                    fileLabel.Text = "Loaded file: " + filePath;
                    loadedFileLabelTooltip.SetToolTip(fileLabel, filePath);
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LASSharpReader.AboutBox about = new LASSharpReader.AboutBox();
            about.ShowDialog();
        }
    }
}
