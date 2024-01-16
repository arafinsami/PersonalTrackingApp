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
using PersonalTrackingApp.UTILS;

namespace PersonalTrackingApp.FRM
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }


        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = General.isNumber(e);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show(" please fill the user no !!!");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show(" please fill the password !!!");
            else
            {
                List<EMPLOYEE> employees = EmployeeBLL.findByUserNoAndPassword (
                    Convert.ToInt32(txtUserNo.Text),
                    txtPassword.Text
                );
                if (employees.Count == 0)
                {
                    MessageBox.Show("user info not found !!!");
                }
                else
                {
                    EMPLOYEE employee = employees.First();
                    UserDTO.employeeID = employee.ID;
                    UserDTO.userNo = employee.UserNo;
                    UserDTO.isAdmin = employee.isAdmin;
                    FrmMain frm = new FrmMain();
                    frm.Hide();
                    frm.ShowDialog();
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
