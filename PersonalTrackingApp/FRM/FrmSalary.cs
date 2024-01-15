using BLL;
using DAO.DTO;
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
    public partial class FrmSalary : Form
    {
        public FrmSalary()
        {
            InitializeComponent();
        }

        SalaryDTO dto = new SalaryDTO();
        SALARY salary = new SALARY();
        bool comboFull = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (salary.EmployeeID == 0)
                MessageBox.Show("select an employee !!!");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("insert a salary !!!");
            else if (txtYear.Text.Trim() == "")
                MessageBox.Show("insert year !!!");
            else
            {
                salary.Amount = Convert.ToInt32(txtSalary.Text);
                salary.Year = Convert.ToInt32(txtYear.Text);
                int monthID = Convert.ToInt32(cmbMonth.SelectedValue);
                salary.MonthID = monthID;
                SalaryBLL.save(salary);
                MessageBox.Show("salary saved successfully !!!");
                txtSalary.Clear();
                txtYear.Clear();
                salary = new SALARY();
            }
        }

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            dto = SalaryBLL.findAll();
            dataGridViewSalary.DataSource = dto.employees;
            dataGridViewSalary.Columns[0].Visible = false;
            dataGridViewSalary.Columns[1].HeaderText = "user No";
            dataGridViewSalary.Columns[2].Visible = false;
            dataGridViewSalary.Columns[3].HeaderText = "Name";
            dataGridViewSalary.Columns[4].Visible = false;
            dataGridViewSalary.Columns[5].Visible = false;
            dataGridViewSalary.Columns[6].Visible = false;
            dataGridViewSalary.Columns[7].Visible = false;
            dataGridViewSalary.Columns[8].Visible = false;
            dataGridViewSalary.Columns[9].Visible = false;
            dataGridViewSalary.Columns[10].Visible = false;
            dataGridViewSalary.Columns[11].Visible = false;
            dataGridViewSalary.Columns[12].Visible = false;
            dataGridViewSalary.Columns[13].Visible = false;
            comboFull = false;
            positionsByDepartmentAndTaskState();
        }

        private void positionsByDepartmentAndTaskState()
        {

            cmbDepartment.DataSource = dto.departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            cmbMonth.DataSource = dto.months;
            cmbMonth.DisplayMember = "Month Name";
            cmbMonth.ValueMember = "ID";
            cmbMonth.SelectedIndex = -1;
        }

        private void dataGridViewSalary_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridViewSalary.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridViewSalary.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSurName.Text = dataGridViewSalary.Rows[e.RowIndex].Cells[4].Value.ToString();
            salary.EmployeeID = Convert.ToInt32(dataGridViewSalary.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                salary.DepartmentID = departmentID;
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID == departmentID).ToList();

                List<EmployeeDetailsDTO> employees = dto.employees;
                dataGridViewSalary.DataSource = employees.Where(x => x.departmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailsDTO> employees = dto.employees;
                int positionID = Convert.ToInt32(cmbPosition.SelectedValue);
                salary.PositionID = positionID;
                dataGridViewSalary.DataSource = employees.Where(x => x.positionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
        }
    }
}
