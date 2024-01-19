using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO.DTO;
using PersonalTrackingApp.UTILS;
using BLL;
using DAO;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        TimeSpan permissionDay;
        public bool isUpdate = false;
        public PermissionDetailsDTO detailsDTO = new PermissionDetailsDTO();

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("please insert permission !!!");
            else if (Convert.ToInt32(txtDayAmount.Text) <= 0)
                MessageBox.Show("permission daye must be bigger than zero !!!");
            else if (txtExplanation.Text.Trim() == "")
                MessageBox.Show("please insert explanation !!!");
            else
            {
                PERMISSION permission = new PERMISSION();
                if (!isUpdate)
                {
                    permission.EmployeeID = UserDTO.employeeID;
                    permission.PermissionState = 1;
                    permission.PermissionStartDate = dptStartDate.Value;
                    permission.PermissionEndDate = dptEndDate.Value;
                    permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                    permission.PermissionExplanation = txtExplanation.Text;
                    PermissionBLL.save(permission);
                    MessageBox.Show("permission saved successfully !!!");
                    permission = clearForm();
                }
                else if (isUpdate)
                {
                    DialogResult dialogResult = MessageBox.Show("are you sure to update !!!", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        permission.ID = detailsDTO.permissionID;
                        permission.PermissionStartDate = dptStartDate.Value;
                        permission.PermissionEndDate = dptEndDate.Value;
                        permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                        permission.PermissionExplanation = txtExplanation.Text;
                        PermissionBLL.update(permission);
                        MessageBox.Show("permission updated successfully !!!");
                        permission = clearForm();
                    }
                }
            }
        }

        private PERMISSION clearForm()
        {
            PERMISSION permission = new PERMISSION();
            dptStartDate.Value = DateTime.Today;
            dptEndDate.Value = DateTime.Today;
            txtExplanation.Clear();
            return permission;
        }

        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserDTO.userNo.ToString();
            if (isUpdate)
            {
                dptStartDate.Value = detailsDTO.startDate.Value;
                dptEndDate.Value = detailsDTO.endDate.Value;
                txtDayAmount.Text = detailsDTO.permissionDayAmount.ToString();
                txtExplanation.Text = detailsDTO.explanation;
                txtUserNo.Text = detailsDTO.userNo.ToString();
            }
        }

        private void dptStartDate_ValueChanged(object sender, EventArgs e)
        {
            permissionDay = dptEndDate.Value.Date - dptStartDate.Value.Date;
            txtDayAmount.Text = permissionDay.TotalDays.ToString();
        }

        private void dptEndDate_ValueChanged(object sender, EventArgs e)
        {
            permissionDay = dptEndDate.Value.Date - dptStartDate.Value.Date;
            txtDayAmount.Text = permissionDay.TotalDays.ToString();
        }
    }
}
