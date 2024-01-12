
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            FrmEmployeeList employeeList = new FrmEmployeeList();
            this.Hide();
            employeeList.ShowDialog();
            this.Visible = true;
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            FrmTaskList taskList = new FrmTaskList();
            this.Hide();
            taskList.ShowDialog();
            this.Visible = true;
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            FrmSalaryList salaryList = new FrmSalaryList();
            this.Hide();
            salaryList.ShowDialog();
            this.Visible = true;
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            FrmDepartmentList departmentList = new FrmDepartmentList();
            this.Hide();
            departmentList.ShowDialog();
            this.Visible = true;
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            FrmPositionList positionList = new FrmPositionList();
            this.Hide();
            positionList.ShowDialog();
            this.Visible = true;
        }

        private void btnPermission_Click(object sender, EventArgs e)
        {
            FrmPermissionList permissionList = new FrmPermissionList();
            this.Hide();
            permissionList.ShowDialog();
            this.Visible = true;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            this.Hide();
            login.ShowDialog();
            this.Visible = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
