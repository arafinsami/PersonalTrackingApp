using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAO;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        public bool isUpdate = false;
        public DEPARTMENT detail = new DEPARTMENT();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim() == "")
            {
                MessageBox.Show("Please add the Department !!!");
            }
            else
            {
                DEPARTMENT department = new DEPARTMENT();
                if (!isUpdate)
                {
                    department.DepartmentName = txtDepartment.Text;
                    DepartmentBLL.addDepartment(department);
                    MessageBox.Show("Department Added successfully !!!");
                    txtDepartment.Clear();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("are you sure to update !!!", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        department.ID = detail.ID;
                        department.DepartmentName = txtDepartment.Text;
                        DepartmentBLL.update(department);
                        MessageBox.Show("department updated successfully !!!");
                        department = new DEPARTMENT();
                        txtDepartment.Clear();
                    }
                }
            }
        }

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isUpdate)
                txtDepartment.Text = detail.DepartmentName;
        }
    }
}
