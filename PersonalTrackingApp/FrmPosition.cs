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

namespace PersonalTrackingApp
{
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtPosition.Text.Trim() == "")
            {
                MessageBox.Show("Please Insert the Position !!!");
            } else if(cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Department !!!");
            }
            else
            {
                POSITION position = new POSITION();
                position.PositionName = txtPosition.Text;
                position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                PositionBLL.save(position);
                MessageBox.Show("Position saved successfully !!!");
                txtPosition.Clear();
            }
        }

        private void findAllDepartments()
        {
            List<DEPARTMENT> departments = DepartmentBLL.findAll();
            cmbDepartment.DataSource = departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
        }

        private void FrmPosition_Load(object sender, EventArgs e)
        {
            findAllDepartments();
        }
    }
}
