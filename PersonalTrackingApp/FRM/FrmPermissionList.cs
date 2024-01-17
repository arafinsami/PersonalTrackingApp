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
    public partial class FrmPermissionList : Form
    {
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        PermissionDTO dto = new PermissionDTO();
        PERMISSION permission = new PERMISSION();
        bool comboFull = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmPermission permission = new FrmPermission();
            this.Hide();
            permission.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmPermission permission = new FrmPermission();
            this.Hide();
            permission.ShowDialog();
            this.Visible = true;
        }

        private void FrmPermissionList_Load(object sender, EventArgs e)
        {
            dto = PermissionBLL.findAllPermission();
            dataGridViewPermission.DataSource = dto.permissions;
            dataGridViewPermission.Columns[0].Visible = false;
            dataGridViewPermission.Columns[1].HeaderText = "User No";
            dataGridViewPermission.Columns[2].HeaderText = "Name";
            dataGridViewPermission.Columns[3].Visible = false;
            dataGridViewPermission.Columns[4].Visible = false;
            dataGridViewPermission.Columns[5].Visible = false;
            dataGridViewPermission.Columns[6].Visible = false;
            dataGridViewPermission.Columns[7].Visible = false;
            dataGridViewPermission.Columns[8].HeaderText = "Start Date";
            dataGridViewPermission.Columns[9].HeaderText = "End Date";
            dataGridViewPermission.Columns[10].HeaderText = "Day Amount";
            dataGridViewPermission.Columns[11].HeaderText = "state Name";
            dataGridViewPermission.Columns[12].Visible = false;
            dataGridViewPermission.Columns[13].Visible = false;
            dataGridViewPermission.Columns[14].Visible = false;
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
            cmbState.DataSource = dto.permissionStates;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "ID";
            cmbState.SelectedIndex = -1;
        }

        private void dataGridViewPermission_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridViewPermission.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridViewPermission.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSurname.Text = dataGridViewPermission.Rows[e.RowIndex].Cells[4].Value.ToString();
            permission.EmployeeID = Convert.ToInt32(dataGridViewPermission.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID == departmentID).ToList();

                List<PermissionDetailsDTO> permissions = dto.permissions;
                dataGridViewPermission.DataSource = permissions.Where(x => x.departmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<PermissionDetailsDTO> permissions = dto.permissions;
                dataGridViewPermission.DataSource = permissions.Where(x => x.positionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailsDTO> permissions = dto.permissions;
            if (cmbDepartment.Text.Trim() != "")
                permissions = permissions.Where(x => x.departmentName.Contains(cmbDepartment.Text)).ToList();
            if (cmbPosition.Text.Trim() != "")
                permissions = permissions.Where(x => x.positionName.Contains(cmbPosition.Text)).ToList();
            if (rbStartDate.Checked)
                permissions = permissions.Where(x => x.startDate < Convert.ToDateTime(dptEndDate.Value)
                && x.startDate > Convert.ToDateTime(dptStartDate.Value)).ToList();
            if (rbDeliveryDate.Checked)
                permissions = permissions.Where(x => x.endDate < Convert.ToDateTime(dptEndDate.Value)
                && x.endDate > Convert.ToDateTime(dptStartDate.Value)).ToList();
            if (cmbState.Text.Trim() != "")
                permissions = permissions.Where(x => x.stateName.Contains(cmbState.Text)).ToList();
            if (txtDayAmount.Text.Trim() != "")
                permissions = permissions.Where(x => x.permissionDayAmount == Convert.ToInt32(txtDayAmount.Text)).ToList();
            dataGridViewPermission.DataSource = permissions;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dptStartDate.Value = DateTime.Now;
            dptEndDate.Value = DateTime.Now;
            txtDayAmount.Clear();
            positionsByDepartment();
            comboFull = true;
            dataGridViewPermission.DataSource = dto.permissions;
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
    }
}
