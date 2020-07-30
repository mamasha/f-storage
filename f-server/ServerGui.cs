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

        private readonly Control[] _controlsToLock;

        public ServerGui()
        {
            InitializeComponent();
            this.FormClosed += formIsClosed;

            var config = FConfig.LoadFrom("f-config.json");

            var log = Logger.New(config);

            log.Info("fserver.gui", config);

            var server = new FServer(log, config);

            textPort.Text = server.Port.ToString();
            textServerName.Text = server.ServerName;

            _log = log;
            _server = server;
            _users = server;

            _controlsToLock = new Control[] {
                textPort,
                textUserName,
                textPassword,
                textFolder,
                buttonList,
                buttonCreate,
                buttonUpdate,
                buttonDelete,
                listUsers
            };
        }

        private void formIsClosed(object sender, EventArgs e)
        {
            _log.Info("server.gui", "f-server is closed");
            _server.Close();
        }

        private void lockControls()
        {
            Array.ForEach(_controlsToLock,
                control => control.Enabled = false);
        }

        private void unlockControls()
        {
            Array.ForEach(_controlsToLock,
                control => control.Enabled = true);
        }

        private void logToGui(string msg)
        {
            richTextResult.AppendText(msg);
            richTextResult.AppendText("\r\n");

            richTextResult.SelectionStart = richTextResult.Text.Length;
            richTextResult.ScrollToCaret();
        }

        private async Task invoke(string opName, Func<Task> action)
        {
            lockControls();

            try
            {
                await action();

                logToGui($"{opName}: OK");
            }
            catch (Exception ex)
            {
                _log.Error("fserver.gui", ex);
                logToGui($"{opName}: ERROR ({ex.Message})");
            }
            finally
            {
                unlockControls();
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
            await invoke("List users", async () => {
                var list = await _users.List();
                listUsers.Items.Clear();
                listUsers.Items.AddRange(list);
            });
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var userInfo = getUserInfo();

            await invoke($"Create {userInfo.UserName}", async () => {
                await _users.Create(userInfo);
            });
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            var userInfo = getUserInfo();

            await invoke($"Update {userInfo.UserName}", async () => {
                await _users.Update(userInfo);
            });
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var userInfo = getUserInfo();

            await invoke($"Delete {userInfo.UserName}", async () => {
                await _users.Delete(userInfo);
            });
        }

        private void listUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userAndFolder = (string) listUsers.SelectedItem;
            var split = userAndFolder.Split(":");

            textUserName.Text = split[0].Trim();

            if (split.Length > 1)
                textFolder.Text = split[1].Trim();
        }
    }
}
