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
    public partial class FormConfectioner : Form
    {
        public FormConfectioner()
        {
            InitializeComponent();
            ShowConfectioner();
        }
        void ShowConfectioner()
        {
            listViewConfectioner.Items.Clear();
            foreach (ConfectionerSet confectionerSet in Program.wftDb.ConfectionerSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    confectionerSet.Id.ToString() , confectionerSet.FirstName, confectionerSet.MiddleName, confectionerSet.LastName, confectionerSet.Phone
                });
                item.Tag = confectionerSet;
                listViewConfectioner.Items.Add(item);
            }
            listViewConfectioner.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            ConfectionerSet confectionerSet = new ConfectionerSet();
            confectionerSet.FirstName = textBoxFirstName.Text;
            confectionerSet.MiddleName = textBoxMiddleName.Text;
            confectionerSet.LastName = textBoxLastName.Text;
            confectionerSet.Phone = textBoxPhone.Text;
            Program.wftDb.ConfectionerSet.Add(confectionerSet);
            Program.wftDb.SaveChanges();
            ShowConfectioner();
        }

        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewConfectioner.SelectedItems.Count == 1)
            {
                ConfectionerSet confectionerSet = listViewConfectioner.SelectedItems[0].Tag as ConfectionerSet;
                textBoxFirstName.Text = confectionerSet.FirstName;
                textBoxMiddleName.Text = confectionerSet.MiddleName;
                textBoxLastName.Text = confectionerSet.LastName;
                textBoxPhone.Text = confectionerSet.Phone;
            }
            else
            {
                
                textBoxFirstName.Text = " ";
                textBoxMiddleName.Text = " ";
                textBoxLastName.Text = " ";
                textBoxPhone.Text = " ";
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewConfectioner.SelectedItems.Count == 1)
            {
                
                ConfectionerSet confectionerSet = listViewConfectioner.SelectedItems[0].Tag as ConfectionerSet;
                
                confectionerSet.FirstName = textBoxFirstName.Text;
                confectionerSet.MiddleName = textBoxMiddleName.Text;
                confectionerSet.LastName = textBoxLastName.Text;
                confectionerSet.Phone = textBoxPhone.Text;
                Program.wftDb.SaveChanges();
                ShowConfectioner();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewConfectioner.SelectedItems.Count == 1)
                {
                    ConfectionerSet confectionerSet = listViewConfectioner.SelectedItems[0].Tag as ConfectionerSet;
                    Program.wftDb.ConfectionerSet.Remove(confectionerSet);
                    Program.wftDb.SaveChanges();
                    ShowConfectioner();
                }

                textBoxFirstName.Text = " ";
                textBoxMiddleName.Text = " ";
                textBoxLastName.Text = " ";
                textBoxPhone.Text = " ";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эту запись используется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
