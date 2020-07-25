namespace f_server
{
    partial class ServerGui
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
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonList = new System.Windows.Forms.Button();
            this.textFolder = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.textUserName = new System.Windows.Forms.TextBox();
            this.richTextResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonDelete.Location = new System.Drawing.Point(853, 354);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(227, 83);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonUpdate.Location = new System.Drawing.Point(533, 354);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(292, 83);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCreate.Location = new System.Drawing.Point(264, 354);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(227, 83);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonList
            // 
            this.buttonList.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonList.Location = new System.Drawing.Point(12, 354);
            this.buttonList.Name = "buttonList";
            this.buttonList.Size = new System.Drawing.Size(227, 83);
            this.buttonList.TabIndex = 1;
            this.buttonList.Text = "List";
            this.buttonList.UseVisualStyleBackColor = true;
            this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
            // 
            // textFolder
            // 
            this.textFolder.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textFolder.Location = new System.Drawing.Point(12, 233);
            this.textFolder.Name = "textFolder";
            this.textFolder.Size = new System.Drawing.Size(1068, 82);
            this.textFolder.TabIndex = 0;
            this.textFolder.Text = "Folder";
            // 
            // textPassword
            // 
            this.textPassword.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textPassword.Location = new System.Drawing.Point(12, 124);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(573, 82);
            this.textPassword.TabIndex = 0;
            this.textPassword.Text = "Password";
            // 
            // textPort
            // 
            this.textPort.Enabled = false;
            this.textPort.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textPort.Location = new System.Drawing.Point(874, 14);
            this.textPort.Name = "textPort";
            this.textPort.ReadOnly = true;
            this.textPort.Size = new System.Drawing.Size(206, 82);
            this.textPort.TabIndex = 0;
            this.textPort.Text = "1234";
            // 
            // textUserName
            // 
            this.textUserName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textUserName.Location = new System.Drawing.Point(12, 14);
            this.textUserName.Name = "textUserName";
            this.textUserName.Size = new System.Drawing.Size(573, 82);
            this.textUserName.TabIndex = 0;
            this.textUserName.Text = "User name";
            // 
            // richTextResult
            // 
            this.richTextResult.Location = new System.Drawing.Point(12, 473);
            this.richTextResult.Name = "richTextResult";
            this.richTextResult.ReadOnly = true;
            this.richTextResult.Size = new System.Drawing.Size(1068, 638);
            this.richTextResult.TabIndex = 2;
            this.richTextResult.Text = "";
            // 
            // ServerGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 1127);
            this.Controls.Add(this.richTextResult);
            this.Controls.Add(this.textUserName);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textFolder);
            this.Controls.Add(this.buttonList);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonDelete);
            this.Name = "ServerGui";
            this.Text = "ServerGui";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonList;
        private System.Windows.Forms.TextBox textFolder;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.RichTextBox richTextResult;
    }
}

