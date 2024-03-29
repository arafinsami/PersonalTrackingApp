﻿using System;
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
using System.IO;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailsDTO details = new EmployeeDetailsDTO();
        public bool isUpdate = false;
        bool comboFull = false;
        bool isUniqueUserNo = false;
        string fileName = "";
        string imagePath = "";

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User No can not be empty !!!");
            if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password can not be empty !!!");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name can not be empty !!!");
            else if (txtSurName.Text.Trim() == "")
                MessageBox.Show("Sur Name can not be empty !!!");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("salary can not be empty !!!");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Departmen should be selected !!!");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Position should be selected !!!");
            else
            {
                if (!isUpdate)
                {
                    EMPLOYEE employee = new EMPLOYEE();
                    employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                    employee.Password = txtPassword.Text;
                    employee.isAdmin = chkIsAdmin.Checked;
                    employee.Name = txtName.Text;
                    employee.SurName = txtSurName.Text;
                    employee.Salary = Convert.ToInt32(txtSalary.Text);
                    employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                    employee.Address = txtAddress.Text;
                    employee.BirthDay = dptBirthDate.Value;
                    employee.ImagePath = fileName;
                    EmployeeBLL.save(employee);
                    File.Copy(txtImagePath.Text, @"images\\" + fileName);
                    MessageBox.Show("Employee saved successfully !!!");
                    clearField();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("are you sure to update !!!", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        EMPLOYEE employee = new EMPLOYEE();
                        if (txtImagePath.Text != imagePath)
                        {
                            if (File.Exists(@"images\\" + details.imagePath))
                            {
                                File.Delete(@"images\\" + details.imagePath);
                                File.Copy(txtImagePath.Text, @"images\\" + fileName);
                                employee.ImagePath = fileName;
                            }
                            else
                            {
                                employee.ImagePath = details.imagePath;
                            }
                        }
                        employee.ID = details.employeeID;
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.Name = txtName.Text;
                        employee.SurName = txtSurName.Text;
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.Address = txtAddress.Text;
                        employee.isAdmin = chkIsAdmin.Checked;
                        employee.BirthDay = dptBirthDate.Value;
                        EmployeeBLL.update(employee);
                        MessageBox.Show("employee updated successfully !!!");
                        employee = new EMPLOYEE();
                        clearField();

                    }
                }
            }
        }

        private void clearField()
        {
            txtUserNo.Clear();
            txtPassword.Clear();
            chkIsAdmin.Checked = false;
            txtName.Clear();
            txtSurName.Clear();
            txtSalary.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            dptBirthDate.Value = DateTime.Today;
            txtImagePath.Clear();
            txtAddress.Clear();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.findAllWithPositionsAndDepartments();
            cmbDepartment.DataSource = dto.departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            if (isUpdate)
            {
                txtUserNo.Text = details.userNo.ToString();
                txtPassword.Text = details.password.ToString();
                txtName.Text = details.name.ToString();
                txtSurName.Text = details.surName.ToString();
                cmbDepartment.SelectedValue = details.departmentID;
                cmbPosition.SelectedValue = details.positionID;
                txtSalary.Text = details.salary.ToString();
                txtAddress.Text = details.address;
                chkIsAdmin.Checked = Convert.ToBoolean(details.isAdmin);
                imagePath = Application.StartupPath + "\\images" + details.imagePath;
                txtImagePath.Text = imagePath;
                pictureBoxUserImage.ImageLocation = imagePath;
                dptBirthDate.Value = Convert.ToDateTime(details.birthDate);
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialogUserImage.ShowDialog() == DialogResult.OK)
            {
                pictureBoxUserImage.Load(openFileDialogUserImage.FileName);
                txtImagePath.Text = openFileDialogUserImage.FileName;
                string unique = Guid.NewGuid().ToString();
                fileName += unique + openFileDialogUserImage.SafeFileName;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User No can not be empty !!!");
            else
            {
                isUniqueUserNo = EmployeeBLL.isUserNoUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUniqueUserNo)
                    MessageBox.Show("This user is exist !!!");
                else
                    MessageBox.Show("This user is usable !!!");
            }
        }
    }
}
