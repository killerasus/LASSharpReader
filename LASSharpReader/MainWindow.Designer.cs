namespace LASSharpReader
{
    partial class MainWindow
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
            this.openLASDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileLabel = new System.Windows.Forms.Label();
            this.loadedFileLabelTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionInformationSectionLabel = new System.Windows.Forms.Label();
            this.versionDataGridView = new System.Windows.Forms.DataGridView();
            this.Mnemonic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wellInformationSectionLabel = new System.Windows.Forms.Label();
            this.wellDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curveInformationSectionLabel = new System.Windows.Forms.Label();
            this.curveDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parametersDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parametersInformationSectionLabel = new System.Windows.Forms.Label();
            this.otherTextBox = new System.Windows.Forms.TextBox();
            this.otherInformationLabel = new System.Windows.Forms.Label();
            this.asciiDataLabel = new System.Windows.Forms.Label();
            this.ASCIIDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.versionDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curveDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parametersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASCIIDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // openLASDialog
            // 
            this.openLASDialog.FileName = "openFileDialog1";
            this.openLASDialog.Filter = "LAS files|*.las|Text files|*.txt";
            this.openLASDialog.Title = "Load LAS file";
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(12, 24);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(62, 13);
            this.fileLabel.TabIndex = 1;
            this.fileLabel.Text = "Loaded file:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(595, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // versionInformationSectionLabel
            // 
            this.versionInformationSectionLabel.AutoSize = true;
            this.versionInformationSectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionInformationSectionLabel.Location = new System.Drawing.Point(10, 41);
            this.versionInformationSectionLabel.Name = "versionInformationSectionLabel";
            this.versionInformationSectionLabel.Size = new System.Drawing.Size(167, 13);
            this.versionInformationSectionLabel.TabIndex = 3;
            this.versionInformationSectionLabel.Text = "Version Information Section:";
            // 
            // versionDataGridView
            // 
            this.versionDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.versionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.versionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mnemonic,
            this.Unit,
            this.Data,
            this.Comment});
            this.versionDataGridView.Enabled = false;
            this.versionDataGridView.Location = new System.Drawing.Point(12, 57);
            this.versionDataGridView.Name = "versionDataGridView";
            this.versionDataGridView.RowHeadersVisible = false;
            this.versionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.versionDataGridView.Size = new System.Drawing.Size(574, 119);
            this.versionDataGridView.TabIndex = 4;
            // 
            // Mnemonic
            // 
            this.Mnemonic.DataPropertyName = "Mnemonic";
            this.Mnemonic.HeaderText = "Mnemonic";
            this.Mnemonic.Name = "Mnemonic";
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            // 
            // Comment
            // 
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            // 
            // wellInformationSectionLabel
            // 
            this.wellInformationSectionLabel.AutoSize = true;
            this.wellInformationSectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wellInformationSectionLabel.Location = new System.Drawing.Point(9, 179);
            this.wellInformationSectionLabel.Name = "wellInformationSectionLabel";
            this.wellInformationSectionLabel.Size = new System.Drawing.Size(150, 13);
            this.wellInformationSectionLabel.TabIndex = 5;
            this.wellInformationSectionLabel.Text = "Well Information Section:";
            // 
            // wellDataGridView
            // 
            this.wellDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.wellDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wellDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.wellDataGridView.Enabled = false;
            this.wellDataGridView.Location = new System.Drawing.Point(12, 195);
            this.wellDataGridView.Name = "wellDataGridView";
            this.wellDataGridView.RowHeadersVisible = false;
            this.wellDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.wellDataGridView.Size = new System.Drawing.Size(574, 119);
            this.wellDataGridView.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Mnemonic";
            this.dataGridViewTextBoxColumn1.HeaderText = "Mnemonic";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Unit";
            this.dataGridViewTextBoxColumn2.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Data";
            this.dataGridViewTextBoxColumn3.HeaderText = "Data";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn4.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // curveInformationSectionLabel
            // 
            this.curveInformationSectionLabel.AutoSize = true;
            this.curveInformationSectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curveInformationSectionLabel.Location = new System.Drawing.Point(10, 317);
            this.curveInformationSectionLabel.Name = "curveInformationSectionLabel";
            this.curveInformationSectionLabel.Size = new System.Drawing.Size(158, 13);
            this.curveInformationSectionLabel.TabIndex = 7;
            this.curveInformationSectionLabel.Text = "Curve Information Section:";
            // 
            // curveDataGridView
            // 
            this.curveDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.curveDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.curveDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.curveDataGridView.Enabled = false;
            this.curveDataGridView.Location = new System.Drawing.Point(12, 333);
            this.curveDataGridView.Name = "curveDataGridView";
            this.curveDataGridView.RowHeadersVisible = false;
            this.curveDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.curveDataGridView.Size = new System.Drawing.Size(574, 119);
            this.curveDataGridView.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Mnemonic";
            this.dataGridViewTextBoxColumn5.HeaderText = "Mnemonic";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Unit";
            this.dataGridViewTextBoxColumn6.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Data";
            this.dataGridViewTextBoxColumn7.HeaderText = "Data";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn8.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // parametersDataGridView
            // 
            this.parametersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.parametersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.parametersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.parametersDataGridView.Enabled = false;
            this.parametersDataGridView.Location = new System.Drawing.Point(13, 471);
            this.parametersDataGridView.Name = "parametersDataGridView";
            this.parametersDataGridView.RowHeadersVisible = false;
            this.parametersDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.parametersDataGridView.Size = new System.Drawing.Size(574, 119);
            this.parametersDataGridView.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Mnemonic";
            this.dataGridViewTextBoxColumn9.HeaderText = "Mnemonic";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Unit";
            this.dataGridViewTextBoxColumn10.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Data";
            this.dataGridViewTextBoxColumn11.HeaderText = "Data";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Comment";
            this.dataGridViewTextBoxColumn12.HeaderText = "Comment";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // parametersInformationSectionLabel
            // 
            this.parametersInformationSectionLabel.AutoSize = true;
            this.parametersInformationSectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parametersInformationSectionLabel.Location = new System.Drawing.Point(12, 455);
            this.parametersInformationSectionLabel.Name = "parametersInformationSectionLabel";
            this.parametersInformationSectionLabel.Size = new System.Drawing.Size(188, 13);
            this.parametersInformationSectionLabel.TabIndex = 10;
            this.parametersInformationSectionLabel.Text = "Parameters Information Section:";
            // 
            // otherTextBox
            // 
            this.otherTextBox.Location = new System.Drawing.Point(12, 609);
            this.otherTextBox.Multiline = true;
            this.otherTextBox.Name = "otherTextBox";
            this.otherTextBox.Size = new System.Drawing.Size(575, 83);
            this.otherTextBox.TabIndex = 11;
            // 
            // otherInformationLabel
            // 
            this.otherInformationLabel.AutoSize = true;
            this.otherInformationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otherInformationLabel.Location = new System.Drawing.Point(12, 593);
            this.otherInformationLabel.Name = "otherInformationLabel";
            this.otherInformationLabel.Size = new System.Drawing.Size(156, 13);
            this.otherInformationLabel.TabIndex = 12;
            this.otherInformationLabel.Text = "Other Information Section:";
            // 
            // asciiDataLabel
            // 
            this.asciiDataLabel.AutoSize = true;
            this.asciiDataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asciiDataLabel.Location = new System.Drawing.Point(12, 695);
            this.asciiDataLabel.Name = "asciiDataLabel";
            this.asciiDataLabel.Size = new System.Drawing.Size(121, 13);
            this.asciiDataLabel.TabIndex = 13;
            this.asciiDataLabel.Text = "ASCII Data Section:";
            // 
            // ASCIIDataGridView
            // 
            this.ASCIIDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ASCIIDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ASCIIDataGridView.Enabled = false;
            this.ASCIIDataGridView.Location = new System.Drawing.Point(12, 711);
            this.ASCIIDataGridView.Name = "ASCIIDataGridView";
            this.ASCIIDataGridView.RowHeadersVisible = false;
            this.ASCIIDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.ASCIIDataGridView.Size = new System.Drawing.Size(574, 119);
            this.ASCIIDataGridView.TabIndex = 14;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(612, 350);
            this.Controls.Add(this.ASCIIDataGridView);
            this.Controls.Add(this.asciiDataLabel);
            this.Controls.Add(this.otherInformationLabel);
            this.Controls.Add(this.otherTextBox);
            this.Controls.Add(this.parametersInformationSectionLabel);
            this.Controls.Add(this.parametersDataGridView);
            this.Controls.Add(this.curveDataGridView);
            this.Controls.Add(this.curveInformationSectionLabel);
            this.Controls.Add(this.wellDataGridView);
            this.Controls.Add(this.wellInformationSectionLabel);
            this.Controls.Add(this.versionDataGridView);
            this.Controls.Add(this.versionInformationSectionLabel);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "LASSharpReader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.versionDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wellDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curveDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parametersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASCIIDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openLASDialog;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.ToolTip loadedFileLabelTooltip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label versionInformationSectionLabel;
        private System.Windows.Forms.DataGridView versionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mnemonic;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.Label wellInformationSectionLabel;
        private System.Windows.Forms.DataGridView wellDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Label curveInformationSectionLabel;
        private System.Windows.Forms.DataGridView curveDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridView parametersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Label parametersInformationSectionLabel;
        private System.Windows.Forms.TextBox otherTextBox;
        private System.Windows.Forms.Label otherInformationLabel;
        private System.Windows.Forms.Label asciiDataLabel;
        private System.Windows.Forms.DataGridView ASCIIDataGridView;
    }
}

