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
            FrmPosition position = new FrmPosition();
            this.Hide();
            position.ShowDialog();
            this.Visible = true;
        }

        private void FrmPositionList_Load(object sender, EventArgs e)
        {
            findAll();
            dataGridViewPosition.Columns[0].Visible = false;
            dataGridViewPosition.Columns[1].HeaderText = "Department Name";
            dataGridViewPosition.Columns[2].Visible = false;
            dataGridViewPosition.Columns[3].HeaderText = "Position Name";
        }

        private void findAll()
        {
            List<PositionDTO> positions  = PositionBLL.findAll();
            dataGridViewPosition.DataSource = positions;
        }
    }
}
