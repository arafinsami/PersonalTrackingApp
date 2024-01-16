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
                permission.EmployeeID = UserDTO.employeeID;
                permission.PermissionState = 1;
                permission.PermissionStartDate = dptStartDate.Value.Date;
                permission.PermissionEndDate = dptEndDate.Value.Date;
                permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                permission.PermissionExplanation = txtExplanation.Text;
                PermissionBLL.save(permission);
                MessageBox.Show("permission saved successfully !!!");
                permission = new PERMISSION();
                dptStartDate.Value = DateTime.Today;
                dptEndDate.Value = DateTime.Today;
                txtExplanation.Clear();
            }
        }

        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserDTO.userNo.ToString();
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
