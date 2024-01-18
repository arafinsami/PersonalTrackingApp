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
using DAO.DTO;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmSalaryList : Form
    {
        public FrmSalaryList()
        {
            InitializeComponent();
        }

        SalaryDTO dto = new SalaryDTO();
        SALARY task = new SALARY();
        SalaryDetailsDTO salaryDetailsDTO = new SalaryDetailsDTO();
        bool comboFull = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSalary salary = new FrmSalary();
            this.Hide();
            salary.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (salaryDetailsDTO.salaryID == 0)
            {
                MessageBox.Show("please select a salary !!!");
            }
            else
            {
                FrmSalary frmSalary = new FrmSalary();
                frmSalary.salaryDetailsDTO = salaryDetailsDTO;
                frmSalary.isUpdate = true;
                this.Hide();
                frmSalary.ShowDialog();
                this.Visible = true;
                fillForm();
                clearForm();
            }

        }

        private void FrmSalaryList_Load(object sender, EventArgs e)
        {
            fillForm();
        }

        private void fillForm()
        {
            dto = SalaryBLL.findAll();
            dataGridViewSaraly.DataSource = dto.salaries;
            dataGridViewSaraly.Columns[0].Visible = false;
            dataGridViewSaraly.Columns[1].HeaderText = "User No";
            dataGridViewSaraly.Columns[2].HeaderText = "Name";
            dataGridViewSaraly.Columns[3].Visible = false;
            dataGridViewSaraly.Columns[4].Visible = false;
            dataGridViewSaraly.Columns[5].HeaderText = "Department";
            dataGridViewSaraly.Columns[6].Visible = false;
            dataGridViewSaraly.Columns[7].HeaderText = "Position";
            dataGridViewSaraly.Columns[8].HeaderText = "Month";
            dataGridViewSaraly.Columns[9].Visible = false;
            dataGridViewSaraly.Columns[10].HeaderText = "Salary";
            dataGridViewSaraly.Columns[11].HeaderText = "Salary Year";
            dataGridViewSaraly.Columns[12].Visible = false;
            dataGridViewSaraly.Columns[13].Visible = false;
            comboFull = false;
            positionsByDepartmentAndMonth();
        }

        private void positionsByDepartmentAndMonth()
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
            cmbMonth.DisplayMember = "MonthName";
            cmbMonth.ValueMember = "ID";
            cmbMonth.SelectedIndex = -1;
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID == departmentID).ToList();

                List<SalaryDetailsDTO> salaries = dto.salaries;
                dataGridViewSaraly.DataSource = salaries.Where(x => x.departmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<SalaryDetailsDTO> salaries = dto.salaries;
                dataGridViewSaraly.DataSource = salaries.Where(x => x.positionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
        }

        private void dataGridViewSaraly_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            salaryDetailsDTO.userNo = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[1].Value);
            salaryDetailsDTO.name = dataGridViewSaraly.Rows[e.RowIndex].Cells[3].Value.ToString();
            salaryDetailsDTO.surName = dataGridViewSaraly.Rows[e.RowIndex].Cells[4].Value.ToString();
            salaryDetailsDTO.employeeID = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[0].Value.ToString());
            salaryDetailsDTO.salaryID = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[12].Value.ToString());
            salaryDetailsDTO.salaryYear = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[11].Value.ToString());
            salaryDetailsDTO.monthID = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[9].Value.ToString());
            salaryDetailsDTO.salaryAmount = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[10].Value.ToString());
            salaryDetailsDTO.oldSalary = Convert.ToInt32(dataGridViewSaraly.Rows[e.RowIndex].Cells[10].Value.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            txtYear.Clear();
            txtSalary.Clear();
            comboFull = false;
            positionsByDepartment();
            comboFull = true;
            dataGridViewSaraly.DataSource = dto.salaries;
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
            List<SalaryDetailsDTO> salaries = dto.salaries;
            if (txtYear.Text.Trim() != "")
                salaries = salaries.Where(x => x.salaryYear == Convert.ToInt32(txtYear.Text)).ToList();
            if (txtSalary.Text.Trim() != "")
                salaries = salaries.Where(x => x.salaryAmount == Convert.ToInt32(txtSalary.Text)).ToList();
            if (cmbDepartment.Text.Trim() != "")
                salaries = salaries.Where(x => x.departmentName.Contains(cmbDepartment.Text)).ToList();
            if (cmbPosition.Text.Trim() != "")
                salaries = salaries.Where(x => x.positionName.Contains(cmbPosition.Text)).ToList();
            if (cmbMonth.Text.Trim() != "")
                salaries = salaries.Where(x => x.monthName.Contains(cmbMonth.Text)).ToList();
            dataGridViewSaraly.DataSource = salaries;
        }
    }
}
