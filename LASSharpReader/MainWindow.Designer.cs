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
            this.loadLASButton = new System.Windows.Forms.Button();
            this.fileLabel = new System.Windows.Forms.Label();
            this.loadedFileLabelTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // openLASDialog
            // 
            this.openLASDialog.FileName = "openFileDialog1";
            this.openLASDialog.Filter = "LAS files|*.las|Text files|*.txt";
            this.openLASDialog.Title = "Load LAS file";
            // 
            // loadLASButton
            // 
            this.loadLASButton.Location = new System.Drawing.Point(13, 13);
            this.loadLASButton.Name = "loadLASButton";
            this.loadLASButton.Size = new System.Drawing.Size(75, 23);
            this.loadLASButton.TabIndex = 0;
            this.loadLASButton.Text = "Load LAS";
            this.loadLASButton.UseVisualStyleBackColor = true;
            this.loadLASButton.Click += new System.EventHandler(this.loadLASButton_Click);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(94, 18);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(62, 13);
            this.fileLabel.TabIndex = 1;
            this.fileLabel.Text = "Loaded file:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.loadLASButton);
            this.Name = "MainWindow";
            this.Text = "LASSharpReader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openLASDialog;
        private System.Windows.Forms.Button loadLASButton;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.ToolTip loadedFileLabelTooltip;
    }
}

