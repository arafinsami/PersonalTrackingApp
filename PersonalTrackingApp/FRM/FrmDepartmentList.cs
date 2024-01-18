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

namespace PersonalTrackingApp.FRM
{
    public partial class FrmDepartmentList : Form
    {
        public FrmDepartmentList()
        {
            InitializeComponent();
        }

        public DEPARTMENT detail = new DEPARTMENT();
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
            if (detail.ID == 0)
                MessageBox.Show("please select a row !!!");
            else
            {
                FrmDepartment frmDepartment = new FrmDepartment();
                frmDepartment.isUpdate = true;
                frmDepartment.detail = detail;
                this.Hide();
                frmDepartment.ShowDialog();
                this.Visible = true;
                findAll();
            }
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

        private void dataGridViewDepartmentList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ID = Convert.ToInt32(dataGridViewDepartmentList.Rows[e.RowIndex].Cells[0].Value);
            detail.DepartmentName = dataGridViewDepartmentList.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
