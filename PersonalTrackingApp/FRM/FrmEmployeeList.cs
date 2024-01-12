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
using BLL;
using DAO;
using DAO.DTO;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmEmployeeList : Form
    {
        public FrmEmployeeList()
        {
            InitializeComponent();
        }

        EmployeeDTO dto = EmployeeBLL.findAllWithPositionsAndDepartments();
        bool comboFull = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmEmployee frmEmployee = new FrmEmployee();
            this.Hide();
            frmEmployee.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmEmployee frmEmployee = new FrmEmployee();
            this.Hide();
            frmEmployee.ShowDialog();
            this.Visible = true;

        }

        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            positionsByDepartment();

            dataGridViewEmployeeList.DataSource = dto.employees;
            dataGridViewEmployeeList.Columns[0].Visible = false;
            dataGridViewEmployeeList.Columns[1].HeaderText = "user No";
            dataGridViewEmployeeList.Columns[2].Visible = false;
            dataGridViewEmployeeList.Columns[3].HeaderText = "Name";
            dataGridViewEmployeeList.Columns[4].HeaderText = "Surname Name";
            dataGridViewEmployeeList.Columns[5].Visible = false;
            dataGridViewEmployeeList.Columns[6].HeaderText = "Department";
            dataGridViewEmployeeList.Columns[7].Visible = false;
            dataGridViewEmployeeList.Columns[8].HeaderText = "Position";
            dataGridViewEmployeeList.Columns[9].HeaderText = "Salary";
            dataGridViewEmployeeList.Columns[10].Visible = false;
            dataGridViewEmployeeList.Columns[11].HeaderText = "Is Admin";
            dataGridViewEmployeeList.Columns[12].Visible = false;
            dataGridViewEmployeeList.Columns[13].HeaderText = "Birth day";
            comboFull = true;
        }

        private void positionsByDepartment()
        {
            cmbDepartment.DataSource = dto.departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailsDTO> employees = dto.employees;
            if (txtUserNo.Text.Trim() != "")
                employees = employees.Where(x => x.userNo == Convert.ToInt32(txtUserNo.Text)).ToList();

            if (txtName.Text.Trim() != "")
                employees = employees.Where(x => x.name.Contains(txtName.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                employees = employees.Where(x => x.surName.Contains(txtSurname.Text)).ToList();
            if (cmbDepartment.Text.Trim() != "")
                employees = employees.Where(x => x.departmentName.Contains(cmbDepartment.Text)).ToList();
            if (cmbPosition.Text.Trim() != "")
                employees = employees.Where(x => x.positionName.Contains(cmbPosition.Text)).ToList();
            dataGridViewEmployeeList.DataSource = employees;
        }

        private void btnDataClear_Click(object sender, EventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            comboFull = false;

            positionsByDepartment();

            comboFull = true;
            dataGridViewEmployeeList.DataSource = dto.employees;
        }
    }
}
