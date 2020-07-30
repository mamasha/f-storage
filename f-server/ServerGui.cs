using Accessibility;
using f_core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace f_server
{
    public partial class ServerGui : Form
    {
        private readonly ILogger _log;
        private readonly FServer _server;
        private readonly IUserManagement _users;

        public ServerGui()
        {
            InitializeComponent();
            this.FormClosed += formIsClosed;

            var config = FConfig.LoadFrom("f-config.json");

            var log = Logger.New(config);

            log.Info("fserver.gui", config);

            var server = new FServer(config, log);

            textPort.Text = server.Port.ToString();
            textServerName.Text = server.ServerName;

            _log = log;
            _server = server;
            _users = server;
        }

        private void formIsClosed(object sender, EventArgs e)
        {
            _log.Info("server.gui", "f-server is closed");
            _server.Close();
        }

        private void lockButtons()
        {
            textUserName.Enabled = false;
            textPassword.Enabled = false;
            textFolder.Enabled = false;
            buttonList.Enabled = false;
            buttonCreate.Enabled = false;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void unlockButtons()
        {
            textUserName.Enabled = true;
            textPassword.Enabled = true;
            textFolder.Enabled = true;
            buttonList.Enabled = true;
            buttonCreate.Enabled = true;
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
        }

        private void log(string msg)
        {
            richTextResult.AppendText(msg);
            richTextResult.AppendText("\r\n");
        }

        private async Task invoke(Func<Task> action)
        {
            lockButtons();

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                log($"ERROR: {ex.Message}");
            }
            finally
            {
                unlockButtons();
            }
        }

        private UserInfo getUserInfo()
        {
            var userName = textUserName.Text;
            var password = textPassword.Text.Sha256();
            var folder = textFolder.Text;

            return new UserInfo { 
                UserName = userName,
                Password = password,
                Folder = folder
            };
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await invoke(async () => {
                var list = await _users.List();
                log(list.ToJson());
            });
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var userInfo = getUserInfo();

            await invoke(async () => {
                await _users.Create(userInfo);
            });
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var userInfo = getUserInfo();

            await invoke(async () => {
                await _users.Update(userInfo);
            });
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var userInfo = getUserInfo();

            await invoke(async () => {
                await _users.Delete(userInfo);
            });
        }

        private void ServerGui_Load(object sender, EventArgs e)
        {

        }
    }
}
