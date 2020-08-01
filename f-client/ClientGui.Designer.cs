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
            this.textUserName = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.ofdLocalFile = new System.Windows.Forms.OpenFileDialog();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textServerName
            // 
            this.textServerName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textServerName.Location = new System.Drawing.Point(23, 46);
            this.textServerName.Name = "textServerName";
            this.textServerName.Size = new System.Drawing.Size(600, 82);
            this.textServerName.TabIndex = 0;
            // 
            // textPort
            // 
            this.textPort.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textPort.Location = new System.Drawing.Point(687, 46);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(547, 82);
            this.textPort.TabIndex = 1;
            // 
            // textLocalPath
            // 
            this.textLocalPath.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textLocalPath.Location = new System.Drawing.Point(23, 307);
            this.textLocalPath.Name = "textLocalPath";
            this.textLocalPath.Size = new System.Drawing.Size(1153, 45);
            this.textLocalPath.TabIndex = 5;
            this.textLocalPath.Text = "Microsoft.EntityFrameworkCore.dll";
            this.textLocalPath.TextChanged += new System.EventHandler(this.textLocalPath_TextChanged);
            // 
            // textRemoteFileName
            // 
            this.textRemoteFileName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textRemoteFileName.Location = new System.Drawing.Point(23, 399);
            this.textRemoteFileName.Name = "textRemoteFileName";
            this.textRemoteFileName.Size = new System.Drawing.Size(1211, 82);
            this.textRemoteFileName.TabIndex = 7;
            this.textRemoteFileName.Text = "Microsoft.EntityFrameworkCore.dll";
            // 
            // buttonList
            // 
            this.buttonList.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonList.Location = new System.Drawing.Point(23, 514);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(227, 83);
            this.buttonList.TabIndex = 8;
            this.buttonList.Text = "List";
            this.buttonList.UseVisualStyleBackColor = true;
            this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonUpload.Location = new System.Drawing.Point(418, 514);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(241, 83);
            this.buttonUpload.TabIndex = 9;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDownload.Location = new System.Drawing.Point(687, 514);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(292, 83);
            this.buttonDownload.TabIndex = 10;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDelete.Location = new System.Drawing.Point(1007, 514);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(227, 83);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // richTextResult
            // 
            this.richTextResult.Location = new System.Drawing.Point(417, 625);
            this.richTextResult.Name = "richTextResult";
            this.richTextResult.ReadOnly = true;
            this.richTextResult.Size = new System.Drawing.Size(817, 718);
            this.richTextResult.TabIndex = 2;
            this.richTextResult.TabStop = false;
            this.richTextResult.Text = "";
            // 
            // textUserName
            // 
            this.textUserName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textUserName.Location = new System.Drawing.Point(23, 173);
            this.textUserName.Name = "textUserName";
            this.textUserName.Size = new System.Drawing.Size(600, 82);
            this.textUserName.TabIndex = 3;
            this.textUserName.Text = "goga";
            // 
            // textPassword
            // 
            this.textPassword.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textPassword.Location = new System.Drawing.Point(687, 173);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(547, 82);
            this.textPassword.TabIndex = 4;
            this.textPassword.Text = "aabbcc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server to connect to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(687, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tcp port (copy from server)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "User name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(687, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(377, 30);
            this.label5.TabIndex = 3;
            this.label5.Text = "Local path (upload from / download to)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 366);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(176, 30);
            this.label6.TabIndex = 3;
            this.label6.Text = "Remote file name";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(1182, 307);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(52, 45);
            this.buttonBrowse.TabIndex = 6;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // ofdLocalFile
            // 
            this.ofdLocalFile.FileName = "Local file";
            // 
            // listFiles
            // 
            this.listFiles.FormattingEnabled = true;
            this.listFiles.ItemHeight = 30;
            this.listFiles.Location = new System.Drawing.Point(26, 625);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(378, 724);
            this.listFiles.TabIndex = 12;
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 1355);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUserName);
            this.Controls.Add(this.richTextResult);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.buttonList);
            this.Controls.Add(this.textRemoteFileName);
            this.Controls.Add(this.textLocalPath);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textServerName);
            this.Name = "ClientForm";
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
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog ofdLocalFile;
        private System.Windows.Forms.ListBox listFiles;
    }
}

