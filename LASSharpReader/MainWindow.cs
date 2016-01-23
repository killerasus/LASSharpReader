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
        private LASFileReader lasReader;
        private BindingSource versionSource;
        private BindingSource wellSource;
        private BindingSource curveSource;
        private BindingSource parametersSource;

        public MainWindow()
        {
            InitializeComponent();

            lasReader = new LASFileReader();

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

                resetInterface();

                bool valid = lasReader.ReadFile(filePath, ref errorMessage);

                if(!valid)
                {
                    MessageBox.Show("Error loading file: " + filePath + '\n' + errorMessage, "LASSharpReader");
                }
                else
                {
                    fileLabel.Text = "Loaded file: " + filePath;
                    loadedFileLabelTooltip.SetToolTip(fileLabel, filePath);

                    populateVersionDataGridView();
                    populateWellDataGridView();
                    populateCurveDataGridView();
                    populateParametersDataGridView();
                    populateOtherTextBox();
                    populateASCIIDataGridView();
                }
            }
        }

        private void resetInterface()
        {
            fileLabel.Text = "Loaded file: No file loaded.";
            loadedFileLabelTooltip.SetToolTip(fileLabel, "No file loaded.");

            versionDataGridView.DataSource = null;
            versionDataGridView.Enabled = false;
            wellDataGridView.DataSource = null;
            wellDataGridView.Enabled = false;
            curveDataGridView.DataSource = null;
            curveDataGridView.Enabled = false;
            parametersDataGridView.DataSource = null;
            parametersDataGridView.Enabled = false;

            otherTextBox.Text = "";

            ASCIIDataGridView.DataSource = null;
            ASCIIDataGridView.Enabled = false;
        }

        /// <summary>
        /// Populates the Version Information Section Data Grid View
        /// </summary>
        private void populateVersionDataGridView()
        {
            versionSource = new BindingSource();

            foreach( LASField field in lasReader.VersionInformation )
            {
                versionSource.Add(field);
            }

            versionDataGridView.Enabled = true;
            versionDataGridView.DataSource = versionSource;
        }

        /// <summary>
        /// Populates the Well Information Section Data Grid View
        /// </summary>
        private void populateWellDataGridView()
        {
            wellSource = new BindingSource();

            foreach (LASField field in lasReader.WellInformation)
            {
                wellSource.Add(field);
            }

            wellDataGridView.Enabled = true;
            wellDataGridView.DataSource = wellSource;
        }

        /// <summary>
        /// Populates the Curve Information Section Data Grid View
        /// </summary>
        private void populateCurveDataGridView()
        {
            curveSource = new BindingSource();

            foreach (LASField field in lasReader.CurveInformation)
            {
                curveSource.Add(field);
            }

            curveDataGridView.Enabled = true;
            curveDataGridView.DataSource = curveSource;
        }

        /// <summary>
        /// Populates the Parameters Information Section Data Grid View
        /// </summary>
        private void populateParametersDataGridView()
        {
            parametersSource = new BindingSource();

            foreach (LASField field in lasReader.ParametersInformation)
            {
                parametersSource.Add(field);
            }

            parametersDataGridView.Enabled = true;
            parametersDataGridView.DataSource = parametersSource;
        }

        /// <summary>
        /// Populates the Other Information Section
        /// </summary>
        private void populateOtherTextBox()
        {
            otherTextBox.Text = lasReader.OtherInformation;
        }

        /// <summary>
        /// Populates the ASCII Data Grid View
        /// </summary>
        private void populateASCIIDataGridView()
        {
            DataTable table = new DataTable("ASCII Data");

            // Builds DataTable columns
            foreach(LASField field in lasReader.CurveInformation)
            {
                table.Columns.Add(field.Mnemonic, typeof(double));
            }

            int curves = lasReader.CurveInformation.Count;
            int entries = lasReader.ASCIIData.Count;

            // Builds the DataTable rows
            for (int i = 0; i < lasReader.ASCIIData.Count; i = i + curves)
            {
                DataRow row = table.NewRow();

                for (int j = 0; j < curves; j++)
                {
                    row[j] = lasReader.ASCIIData[i + j];
                }

                table.Rows.Add(row);
            }

            ASCIIDataGridView.Enabled = true;
            ASCIIDataGridView.DataSource = table;
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
