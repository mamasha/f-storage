namespace f_client
{
    partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textServerName = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.textLocalPath = new System.Windows.Forms.TextBox();
            this.textRemoteFileName = new System.Windows.Forms.TextBox();
            this.buttonList = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.richTextResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textServerName
            // 
            this.textServerName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textServerName.Location = new System.Drawing.Point(23, 21);
            this.textServerName.Name = "textServerName";
            this.textServerName.Size = new System.Drawing.Size(782, 82);
            this.textServerName.TabIndex = 0;
            this.textServerName.Text = "Server name";
            // 
            // textPort
            // 
            this.textPort.Enabled = false;
            this.textPort.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textPort.Location = new System.Drawing.Point(885, 21);
            this.textPort.Name = "textPort";
            this.textPort.ReadOnly = true;
            this.textPort.Size = new System.Drawing.Size(206, 82);
            this.textPort.TabIndex = 0;
            this.textPort.Text = "1234";
            // 
            // textLocalPath
            // 
            this.textLocalPath.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textLocalPath.Location = new System.Drawing.Point(23, 131);
            this.textLocalPath.Name = "textLocalPath";
            this.textLocalPath.Size = new System.Drawing.Size(1068, 82);
            this.textLocalPath.TabIndex = 0;
            this.textLocalPath.Text = "Local path";
            // 
            // textRemoteFileName
            // 
            this.textRemoteFileName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textRemoteFileName.Location = new System.Drawing.Point(23, 240);
            this.textRemoteFileName.Name = "textRemoteFileName";
            this.textRemoteFileName.Size = new System.Drawing.Size(1068, 82);
            this.textRemoteFileName.TabIndex = 0;
            this.textRemoteFileName.Text = "Remote file name";
            // 
            // buttonList
            // 
            this.buttonList.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonList.Location = new System.Drawing.Point(23, 361);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(227, 83);
            this.buttonList.TabIndex = 1;
            this.buttonList.Text = "List";
            this.buttonList.UseVisualStyleBackColor = true;
            this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonUpload.Location = new System.Drawing.Point(275, 361);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(227, 83);
            this.buttonUpload.TabIndex = 1;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDownload.Location = new System.Drawing.Point(544, 361);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(292, 83);
            this.buttonDownload.TabIndex = 1;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDelete.Location = new System.Drawing.Point(864, 361);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(227, 83);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // richTextResult
            // 
            this.richTextResult.Location = new System.Drawing.Point(23, 480);
            this.richTextResult.Name = "richTextResult";
            this.richTextResult.ReadOnly = true;
            this.richTextResult.Size = new System.Drawing.Size(1068, 638);
            this.richTextResult.TabIndex = 2;
            this.richTextResult.Text = "";
            // 
            // ClientGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 1148);
            this.Controls.Add(this.richTextResult);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.buttonList);
            this.Controls.Add(this.textRemoteFileName);
            this.Controls.Add(this.textLocalPath);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textServerName);
            this.Name = "ClientGui";
            this.Text = "ClientGui";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textServerName;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.TextBox textLocalPath;
        private System.Windows.Forms.TextBox textRemoteFileName;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.RichTextBox richTextResult;
    }
}

