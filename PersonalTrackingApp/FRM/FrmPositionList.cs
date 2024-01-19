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
    public partial class FrmPositionList : Form
    {
        public FrmPositionList()
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
            FrmPosition position = new FrmPosition();
            this.Hide();
            position.ShowDialog();
            this.Visible = true;
            findAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
            {
                MessageBox.Show("please select the row !!!");
            }
            else
            {
                FrmPosition frmPosition = new FrmPosition();
                frmPosition.detail = detail;
                frmPosition.isUpdate = true;
                this.Hide();
                frmPosition.ShowDialog();
                this.Visible = true;
                findAll();
            }
        }

        private void FrmPositionList_Load(object sender, EventArgs e)
        {
            findAll();

        }

        private void findAll()
        {
            List<PositionDTO> positions = PositionBLL.findAll();
            dataGridViewPosition.DataSource = positions;
            dataGridViewPosition.Columns[0].HeaderText = "Department Name";
            dataGridViewPosition.Columns[1].Visible = false;
            dataGridViewPosition.Columns[2].HeaderText = "Position Name";
            dataGridViewPosition.Columns[3].Visible = false;
        }

        private void dataGridViewPosition_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PositionName = dataGridViewPosition.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.ID = Convert.ToInt32(dataGridViewPosition.Rows[e.RowIndex].Cells[1].Value);
            detail.DepartmentID = Convert.ToInt32(dataGridViewPosition.Rows[e.RowIndex].Cells[3].Value);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure to delete !!!", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                PositionBLL.delete(detail.ID);
                MessageBox.Show("position deleted successfully !!!");
                findAll();
            }
        }
    }
}
