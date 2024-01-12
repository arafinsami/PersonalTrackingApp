using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAO;

namespace PersonalTrackingApp
{
    public partial class FrmDepartmentList : Form
    {
        public FrmDepartmentList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmDepartment department = new FrmDepartment();
            this.Hide();
            department.ShowDialog();
            this.Visible = true;
            findAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmDepartment department = new FrmDepartment();
            this.Hide();
            department.ShowDialog();
            this.Visible = true;
            findAll();
        }

        private void FrmDepartmentList_Load(object sender, EventArgs e)
        {
            findAll();
            dataGridViewDepartmentList.Columns[0].HeaderText = "Department ID";
            dataGridViewDepartmentList.Columns[1].HeaderText = "Department Name";
        }

        private void findAll()
        {
            List<DEPARTMENT> departments = DepartmentBLL.findAll();
            dataGridViewDepartmentList.DataSource = departments;
        }
    }
}
