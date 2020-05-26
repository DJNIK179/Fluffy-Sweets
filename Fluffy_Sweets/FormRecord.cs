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
    public partial class FormRecord : Form
    {
        public FormRecord()
        {
            InitializeComponent();
            ShowClient();
            ShowConfectioner();
            ShowSweets();
            ShowRecordSet();
        }

        void ShowConfectioner()
        {
            comboBoxConfectioner.Items.Clear();
            foreach (ConfectionerSet confectionerSet in Program.wftDb.ConfectionerSet)
            {
                string[] item = { confectionerSet.Id.ToString() + ".", confectionerSet.FirstName, confectionerSet.MiddleName, confectionerSet.LastName };
                comboBoxConfectioner.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClient()
        {
            comboBoxClient.Items.Clear();
            foreach (ClientSet clientSet in Program.wftDb.ClientSet)
            {
                string[] item = { clientSet.Id.ToString() + ".", clientSet.FirstName, clientSet.MiddleName, clientSet.LastName };
                comboBoxClient.Items.Add(string.Join(" ", item));
            }
        }
        void ShowSweets()
        {
            comboBoxSweets.Items.Clear();
            foreach (SweetsSet sweetsSet in Program.wftDb.SweetsSet)
            {
                string[] item = { sweetsSet.Id.ToString() + ".", sweetsSet.Name, sweetsSet.Guantity.ToString() };
                comboBoxSweets.Items.Add(string.Join(" ", item));
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxConfectioner.SelectedItem != null && comboBoxClient.SelectedItem != null && comboBoxSweets.SelectedItem != null)
            {
                RecordSet recordSet = new RecordSet();
                recordSet.IdConfectioner = Convert.ToInt32(comboBoxConfectioner.SelectedItem.ToString().Split('.')[0]);
                recordSet.IdClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                recordSet.IdSweets = Convert.ToInt32(comboBoxSweets.SelectedItem.ToString().Split('.')[0]);
                recordSet.Prise = Convert.ToInt32(textBoxPrise.Text);
                Program.wftDb.RecordSet.Add(recordSet);
                Program.wftDb.SaveChanges();
                ShowRecordSet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void ShowRecordSet()
        {
            listViewRecord.Items.Clear();
            foreach (RecordSet recordSet in Program.wftDb.RecordSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    recordSet.ClientSet.LastName+" "+recordSet.ClientSet.FirstName+" "+recordSet.ClientSet.MiddleName,
                    recordSet.ConfectionerSet.LastName+" "+recordSet.ConfectionerSet.FirstName+" "+recordSet.ConfectionerSet.MiddleName,
                    recordSet.SweetsSet.Name,
                    recordSet.SweetsSet.Guantity.ToString(),
                    recordSet.Prise.ToString(),
                });
                item.Tag = recordSet;
                listViewRecord.Items.Add(item);
            }
            listViewRecord.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewRecord.SelectedItems.Count == 1)
            {
                RecordSet recordSet = listViewRecord.SelectedItems[0].Tag as RecordSet;
                recordSet.IdClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                recordSet.IdConfectioner = Convert.ToInt32(comboBoxConfectioner.SelectedItem.ToString().Split('.')[0]);
                recordSet.IdSweets = Convert.ToInt32(comboBoxSweets.SelectedItem.ToString().Split('.')[0]);
                recordSet.Prise = Convert.ToInt32(textBoxPrise.Text);
                Program.wftDb.SaveChanges();
                ShowRecordSet();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewRecord.SelectedItems.Count == 1)
                {
                    RecordSet recordSet = listViewRecord.SelectedItems[0].Tag as RecordSet;
                    Program.wftDb.RecordSet.Remove(recordSet);
                    Program.wftDb.SaveChanges();
                    ShowRecordSet();
                }
                comboBoxClient.SelectedItem = null;
                comboBoxConfectioner.SelectedItem = null;
                comboBoxSweets.SelectedItem = null;
                textBoxPrise.Text = "";
                
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эту запись используется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRecord.SelectedItems.Count == 1)
            {
                RecordSet recordSet = listViewRecord.SelectedItems[0].Tag as RecordSet;
                comboBoxConfectioner.SelectedIndex = comboBoxConfectioner.FindString(recordSet.IdConfectioner.ToString());
                comboBoxClient.SelectedIndex = comboBoxClient.FindString(recordSet.IdClient.ToString());
                comboBoxSweets.SelectedIndex = comboBoxSweets.FindString(recordSet.IdSweets.ToString());
                textBoxPrise.Text = recordSet.Prise.ToString();
            }
            else
            {
                comboBoxConfectioner.SelectedItem = null;
                comboBoxClient.SelectedItem = null;
                comboBoxSweets.SelectedItem = null;
                textBoxPrise.Text = " ";
            }
        }

        private void comboBoxSweets_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }
        void Deductions()
        {
            try
            {
                double prise = 0;
                SweetsSet sweetsSet = Program.wftDb.SweetsSet.Find(Convert.ToInt32(comboBoxSweets.SelectedItem.ToString().Split('.')[0]));
                if (sweetsSet.Name == "Пончики") { prise = 80; }
                if (sweetsSet.Name == "Торт") { prise = 1000; }
                if (sweetsSet.Name == "Эклеры") { prise = 30; }
                if (sweetsSet.Name == "Кексы") { prise = 50; }
                double AllPrise = sweetsSet.Guantity * prise;
                textBoxPrise.Text = AllPrise.ToString();
            }
            catch
            {
                textBoxPrise.Text = " ";
            }
        }
    }
}
