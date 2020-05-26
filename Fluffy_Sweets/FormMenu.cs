using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fluffy_Sweets
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void buttonConfectioner_Click(object sender, EventArgs e)
        {
            Form formConfectioner = new FormConfectioner();
            formConfectioner.Show();
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            Form formClient = new FormClient();
            formClient.Show();
        }

        private void buttonSweets_Click(object sender, EventArgs e)
        {
            Form formSweets = new FormSweets();
            formSweets.Show();
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            Form formRecord = new FormRecord();
            formRecord.Show();
        }
    }
}
