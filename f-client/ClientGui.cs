using f_core;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace f_client
{
    public partial class ClientForm : Form
    {
        private readonly Control[] _controlsToLock;

        public ClientForm()
        {
            InitializeComponent();

            var config = FConfig.LoadFrom("f-config.json");

            textServerName.Text = Environment.MachineName;
            textPort.Text = config.TcpPort.ToString();

            _controlsToLock = new Control[] {
                textServerName,
                textPort,
                textRemoteFileName,
                textLocalPath,
                buttonList,
                buttonUpload,
                buttonDownload,
                buttonDelete,
                textUserName,
                textPassword,
                buttonBrowse,
                listFiles
            };
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

        private async Task<IClient> getClient()
        {
            var serverName = textServerName.Text;
            var port = int.Parse(textPort.Text);
            var userName = textUserName.Text;
            var password = textPassword.Text.Sha256();

            var client = await FClient.New(serverName, port, userName, password);

            return client;
        }

        private async Task invoke(string opName, Func<IClient, Task> makeRequest)
        {
            lockControls();

            try
            {
                using (var client = await getClient())
                    await makeRequest(client);

                logToGui($"{opName}: OK");
            }
            catch (Exception ex)
            {
                logToGui($"{opName}: ERROR {ex.Message}");
            }
            finally
            {
                unlockControls();
            }
        }

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await invoke("List remote files", async client => {
                var list = await client.ListFiles();
                listFiles.Items.Clear();
                listFiles.Items.AddRange(list);
            });
        }

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            var fileName = Path.GetFileName(textRemoteFileName.Text);
            var srcPath = textLocalPath.Text;
            var srcFileName = Path.GetFileName(srcPath);

            await invoke($"Upload {fileName}", client =>
                client.Upload(fileName, srcPath)
            );
        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            var fileName = Path.GetFileName(textRemoteFileName.Text);
            var dstPath = textLocalPath.Text;

            await invoke($"Download {fileName}", client =>
                client.Download(fileName, dstPath)
            );
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var fileName = Path.GetFileName(textRemoteFileName.Text);

            await invoke($"Delete {fileName}", client =>
                client.Delete(fileName)
            );
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (ofdLocalFile.ShowDialog() == DialogResult.OK)
            {
                textLocalPath.Text = ofdLocalFile.FileName;
                textRemoteFileName.Text = Path.GetFileName(ofdLocalFile.FileName);
            }
        }

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            textRemoteFileName.Text = (string) listFiles.SelectedItem;
        }

        private void textLocalPath_TextChanged(object sender, EventArgs e)
        {
            textRemoteFileName.Text = Path.GetFileName(textLocalPath.Text);
        }
    }
}
