using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonalTrackingApp.UTILS;

namespace PersonalTrackingApp
{
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            //pnlAdmin.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmTask task = new FrmTask();
            this.Hide();
            task.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmTask task = new FrmTask();
            this.Hide();
            task.ShowDialog();
            this.Visible = true;
        }
    }
}
