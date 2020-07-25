using f_core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace f_client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void lockButtons()
        {
            textServerName.Enabled = false;
            textRemoteFileName.Enabled = false;
            textLocalPath.Enabled = false;
            buttonList.Enabled = false;
            buttonUpload.Enabled = false;
            buttonDownload.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void unlockButtons()
        {
            textServerName.Enabled = true;
            textRemoteFileName.Enabled = true;
            textLocalPath.Enabled = true;
            buttonList.Enabled = true;
            buttonUpload.Enabled = true;
            buttonDownload.Enabled = true;
            buttonDelete.Enabled = true;
        }

        private void log(string msg)
        {
            richTextResult.AppendText(msg);
            richTextResult.AppendText("\r\n");
        }

        private async Task doIt(Func<Task> action)
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

        private IStorage getServer()
        {
            var serverName = textServerName.Text;
            var port = int.Parse(textPort.Text);
            var server = FClient.New(serverName, port);

            return server;
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            var request = new SrvListRequest();

            var server = getServer();

            await doIt(async () => {
                log("List is clicked");
                await Task.Delay(1000);
                var list = await server.ListFiles(request);
            });
        }

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("Upload is clicked");
                await Task.Delay(5000);
            });
        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("Download is clicked");
                await Task.Delay(2000);
            });
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("Delete is clicked");
                await Task.Delay(3000);
                throw new ApplicationException("User has no access");
            });
        }
    }
}
