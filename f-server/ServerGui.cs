using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace f_server
{
    public partial class ServerGui : Form
    {
        public ServerGui()
        {
            InitializeComponent();
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

        private async void buttonList_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("List is clicked");
                await Task.Delay(1000);
            });
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("Create is clicked");
                await Task.Delay(1000);
            });
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("Update is clicked");
                await Task.Delay(1000);
            });
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            await doIt(async () => {
                log("Delete is clicked");
                await Task.Delay(1000);
                throw new ApplicationException("User is not found");
            });
        }
    }
}
