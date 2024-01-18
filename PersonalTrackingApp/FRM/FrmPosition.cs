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
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }

        public PositionDTO detail = new PositionDTO();
        public bool isUpdate = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() == "")
            {
                MessageBox.Show("Please Insert the Position !!!");
            }
            else if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Department !!!");
            }
            else
            {
                if (!isUpdate)
                {
                    POSITION position = new POSITION();
                    position.PositionName = txtPosition.Text;
                    position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    PositionBLL.save(position);
                    MessageBox.Show("Position saved successfully !!!");
                    txtPosition.Clear();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("are you sure to update !!!", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        POSITION position = new POSITION();
                        position.ID = detail.ID;
                        position.PositionName = txtPosition.Text;
                        position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        PositionBLL.update(position);
                        MessageBox.Show("position updated successfully !!!");
                        position = new POSITION();
                        txtPosition.Clear();
                    }
                }
            }
        }

        private void findAllDepartments()
        {
            List<DEPARTMENT> departments = DepartmentBLL.findAll();
            cmbDepartment.DataSource = departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            if (isUpdate)
            {
                txtPosition.Text = detail.PositionName;
                cmbDepartment.SelectedValue = detail.DepartmentID;
            }
        }

        private void FrmPosition_Load(object sender, EventArgs e)
        {
            findAllDepartments();
        }
    }
}
