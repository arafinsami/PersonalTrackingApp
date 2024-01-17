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
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
        {
            InitializeComponent();
        }

        TaskDTO dto = new TaskDTO();
        TaskDetailDTO taskDetailsDTO = new TaskDetailDTO();
        TASK task = new TASK();
        bool comboFull = false;

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
            fillForm();
        }

        private void fillForm()
        {
            dto = TaskBLL.findAllTasks();
            dataGridViewTasks.DataSource = dto.tasks;
            dataGridViewTasks.Columns[0].Visible = false;
            dataGridViewTasks.Columns[1].HeaderText = "User No";
            dataGridViewTasks.Columns[2].Visible = false;
            dataGridViewTasks.Columns[3].Visible = false;
            dataGridViewTasks.Columns[4].Visible = false;
            dataGridViewTasks.Columns[5].HeaderText = "Department";
            dataGridViewTasks.Columns[6].Visible = false;
            dataGridViewTasks.Columns[7].HeaderText = "Position";
            dataGridViewTasks.Columns[8].Visible = false;
            dataGridViewTasks.Columns[9].HeaderText = "Title";
            dataGridViewTasks.Columns[10].Visible = false;
            dataGridViewTasks.Columns[11].HeaderText = "Task State";
            dataGridViewTasks.Columns[12].Visible = false;
            dataGridViewTasks.Columns[13].HeaderText = "Start Date";
            dataGridViewTasks.Columns[14].HeaderText = "Delivery Date";
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
            cmbTaskState.DataSource = dto.taskStates;
            cmbTaskState.DisplayMember = "State Name";
            cmbTaskState.ValueMember = "ID";
            cmbTaskState.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmTask frmTask = new FrmTask();
            this.Hide();
            frmTask.ShowDialog();
            this.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (taskDetailsDTO.taskID == 0)
                MessageBox.Show("please select a task !!!");
            else
            {
                FrmTask frmTask = new FrmTask();
                frmTask.isUpdate = true;
                frmTask.taskDetailDTO = taskDetailsDTO;
                this.Hide();
                frmTask.ShowDialog();
                fillForm();
                this.Visible = true;
            }
        }

        private void dataGridViewTasks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            taskDetailsDTO.employeeID = Convert.ToInt32(dataGridViewTasks.Rows[e.RowIndex].Cells[0].Value);
            taskDetailsDTO.taskID = Convert.ToInt32(dataGridViewTasks.Rows[e.RowIndex].Cells[8].Value);
            taskDetailsDTO.taskStateID = Convert.ToInt32(dataGridViewTasks.Rows[e.RowIndex].Cells[12].Value);
            taskDetailsDTO.userNo = Convert.ToInt32(dataGridViewTasks.Rows[e.RowIndex].Cells[1].Value);
            taskDetailsDTO.name = dataGridViewTasks.Rows[e.RowIndex].Cells[3].Value.ToString();
            taskDetailsDTO.surName = dataGridViewTasks.Rows[e.RowIndex].Cells[4].Value.ToString();
            taskDetailsDTO.title = dataGridViewTasks.Rows[e.RowIndex].Cells[9].Value.ToString();
            taskDetailsDTO.content = dataGridViewTasks.Rows[e.RowIndex].Cells[10].Value.ToString();
            taskDetailsDTO.taskStartDate = Convert.ToDateTime(dataGridViewTasks.Rows[e.RowIndex].Cells[13].Value);
            taskDetailsDTO.taskDeliveryDate = Convert.ToDateTime(dataGridViewTasks.Rows[e.RowIndex].Cells[14].Value);
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID == departmentID).ToList();

                List<TaskDetailDTO> tasks = dto.tasks;
                dataGridViewTasks.DataSource = tasks.Where(x => x.departmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<TaskDetailDTO> tasks = dto.tasks;
                dataGridViewTasks.DataSource = tasks.Where(x => x.positionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<TaskDetailDTO> tasks = dto.tasks;
            if (txtUserNo.Text.Trim() != "")
                tasks = tasks.Where(x => x.userNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtName.Text.Trim() != "")
                tasks = tasks.Where(x => x.name.Contains(txtName.Text)).ToList();
            if (txtSurName.Text.Trim() != "")
                tasks = tasks.Where(x => x.surName.Contains(txtSurName.Text)).ToList();
            if (cmbDepartment.Text.Trim() != "")
                tasks = tasks.Where(x => x.departmentName.Contains(cmbDepartment.Text)).ToList();
            if (cmbPosition.Text.Trim() != "")
                tasks = tasks.Where(x => x.positionName.Contains(cmbPosition.Text)).ToList();
            dataGridViewTasks.DataSource = tasks;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurName.Clear();
            comboFull = false;
            positionsByDepartment();
            comboFull = true;
            dataGridViewTasks.DataSource = dto.tasks;
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
