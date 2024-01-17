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
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }

        TaskDTO dto = new TaskDTO();
        TASK task = new TASK();
        public TaskDetailDTO taskDetailDTO = new TaskDetailDTO();
        public bool isUpdate = false;
        bool comboFull = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lbltaskState.Visible = true;
            cmbTaskState.Visible = true;
            if (task.EmployeeID == 0)
                MessageBox.Show("select an employee !!!");
            else if (txtTaskTitle.Text.Trim() == "")
                MessageBox.Show("insert a task title !!!");
            else if (txtContent.Text.Trim() == "")
                MessageBox.Show("insert a task content !!!");
            else
            {
                if (!isUpdate)
                {
                    lbltaskState.Visible = true;
                    cmbTaskState.Visible = true;
                    task.TaskTitle = txtTaskTitle.Text;
                    task.TaskContent = txtContent.Text;
                    task.TaskStartDate = DateTime.Today;
                    task.TaskDeliveryDate = DateTime.Today;
                    task.TaskState = 1;
                    TaskBLL.save(task);
                    MessageBox.Show("Task saved successfully !!!");
                    txtTaskTitle.Clear();
                    txtContent.Clear();
                    task = new TASK();
                }
                else if (isUpdate)
                {
                    DialogResult dialogResult = MessageBox.Show("are you sure to update !!!", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        TASK update = new TASK();
                        update.ID = taskDetailDTO.taskID;
                        if (Convert.ToInt32(txtUserNo.Text) != taskDetailDTO.userNo)
                            update.EmployeeID = task.EmployeeID;
                        else
                            update.EmployeeID = taskDetailDTO.employeeID;

                        update.TaskTitle = txtTaskTitle.Text;
                        update.TaskContent = txtContent.Text;
                        update.TaskState = Convert.ToInt32(cmbTaskState.SelectedValue);
                        TaskBLL.update(update);
                        MessageBox.Show("task updated !!!");
                        txtTaskTitle.Clear();
                        txtContent.Clear();
                        update = new TASK();
                        fillForm();
                    }
                }
            }
        }

        private void FrmTask_Load(object sender, EventArgs e)
        {
            fillForm();
            if (isUpdate)
            {
                txtUserNo.Text = taskDetailDTO.userNo.ToString();
                txtName.Text = taskDetailDTO.name.ToString();
                txtSurname.Text = taskDetailDTO.surName.ToString();
                txtTaskTitle.Text = taskDetailDTO.title;
                txtContent.Text = taskDetailDTO.content;
                cmbTaskState.SelectedValue = taskDetailDTO.taskStateID;
                task.TaskDeliveryDate = taskDetailDTO.taskDeliveryDate;
            }
        }

        private void fillForm()
        {
            dto = TaskBLL.findAllTasks();
            dataGridViewTasks.DataSource = dto.employees;
            dataGridViewTasks.Columns[0].Visible = false;
            dataGridViewTasks.Columns[1].HeaderText = "user No";
            dataGridViewTasks.Columns[2].Visible = false;
            dataGridViewTasks.Columns[3].HeaderText = "Name";
            dataGridViewTasks.Columns[4].HeaderText = "Surname Name";
            dataGridViewTasks.Columns[5].Visible = false;
            dataGridViewTasks.Columns[6].Visible = false;
            dataGridViewTasks.Columns[7].Visible = false;
            dataGridViewTasks.Columns[8].Visible = false;
            dataGridViewTasks.Columns[9].Visible = false;
            dataGridViewTasks.Columns[10].Visible = false;
            dataGridViewTasks.Columns[11].Visible = false;
            dataGridViewTasks.Columns[12].Visible = false;
            dataGridViewTasks.Columns[13].Visible = false;
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

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                task.DepartmentID = departmentID;
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID == departmentID).ToList();

                List<EmployeeDetailsDTO> employees = dto.employees;
                dataGridViewTasks.DataSource = employees.Where(x => x.departmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void dataGridViewTasks_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridViewTasks.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridViewTasks.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSurname.Text = dataGridViewTasks.Rows[e.RowIndex].Cells[4].Value.ToString();
            task.EmployeeID = Convert.ToInt32(dataGridViewTasks.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailsDTO> employees = dto.employees;
                int positionID = Convert.ToInt32(cmbPosition.SelectedValue);
                task.PositionID = positionID;
                dataGridViewTasks.DataSource = employees.Where(x => x.positionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
        }
    }
}
