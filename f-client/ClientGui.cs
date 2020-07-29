using f_core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace f_client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();

            var config = FConfig.LoadFrom("f-config.json");

            textPort.Text = config.TcpPort.ToString();
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

        private async Task<IClient> getClient()
        {
            var serverName = textServerName.Text;
            var port = int.Parse(textPort.Text);
            var userName = textUserName.Text;
            var password = textPassword.Text.AsSha();

            var client = await FClient.New(serverName, port, userName, password);

            return client;
        }

        private async Task invoke(Func<IClient, Task> makeRequest)
        {
            lockButtons();

            try
            {
                using (var client = await getClient())
                    await makeRequest(client);
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

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await invoke(async client => {
                var list = await client.ListFiles();
                log(list.ToJson());
            });
        }

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            var fileName = textRemoteFileName.Text;
            var srcPath = textLocalPath.Text;

            await invoke(client =>
                client.Upload(fileName, srcPath)
            );
        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            var fileName = textRemoteFileName.Text;
            var dstPath = textLocalPath.Text;

            await invoke(client =>
                client.Download(fileName, dstPath)
            );
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var fileName = textRemoteFileName.Text;

            await invoke(client =>
                client.Delete(fileName)
            );
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }
    }
}
