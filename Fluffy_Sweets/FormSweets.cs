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
    public partial class FormSweets : Form
    {
        public FormSweets()
        {
            InitializeComponent();
            ShowSweets();
        }
        void ShowSweets()
        {
            listViewSweets.Items.Clear();
            foreach (SweetsSet sweetsSet in Program.wftDb.SweetsSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    sweetsSet.Id.ToString() , sweetsSet.Name, sweetsSet.Guantity.ToString()
                });
                item.Tag = sweetsSet;
                listViewSweets.Items.Add(item);
            }
            listViewSweets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SweetsSet sweetsSet = new SweetsSet();
            sweetsSet.Name = textBoxName.Text;
            sweetsSet.Guantity = Convert.ToInt32(textBoxGuantity.Text);
            Program.wftDb.SweetsSet.Add(sweetsSet);
            Program.wftDb.SaveChanges();
            ShowSweets();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSweets.SelectedItems.Count == 1)
            {
                SweetsSet sweetsSet = listViewSweets.SelectedItems[0].Tag as SweetsSet;
                sweetsSet.Name = textBoxName.Text;
                sweetsSet.Guantity = Convert.ToInt32(textBoxGuantity.Text);
                Program.wftDb.SaveChanges();
                ShowSweets();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSweets.SelectedItems.Count == 1)
                {
                    SweetsSet sweetsSet = listViewSweets.SelectedItems[0].Tag as SweetsSet;
                    Program.wftDb.SweetsSet.Remove(sweetsSet);
                    Program.wftDb.SaveChanges();
                    ShowSweets();
                }
                
                textBoxName.Text = " ";
                textBoxGuantity.Text = " ";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эту запись используется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewSweets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSweets.SelectedItems.Count == 1)
            {
                SweetsSet sweetsSet = listViewSweets.SelectedItems[0].Tag as SweetsSet;
                textBoxName.Text = sweetsSet.Name;
                textBoxGuantity.Text = sweetsSet.Guantity.ToString();
            }
            else
            {
                textBoxName.Text = " ";
                textBoxGuantity.Text = " ";
            }
        }
    }
}
