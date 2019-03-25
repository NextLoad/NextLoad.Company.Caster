using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caster.BLL;

namespace Caster.UI
{
    public partial class frmLogin : Form
    {
        private ManagerInfoBLL miBll;
        public frmLogin()
        {
            InitializeComponent();
            miBll = new ManagerInfoBLL();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string pwd = txtPwd.Text;
            if (miBll.Login(name, pwd))
            {
                frmMain frmMain = new frmMain();
                this.Hide();
                frmMain.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
