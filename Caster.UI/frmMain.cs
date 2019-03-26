using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caster.UI
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 权限设置
        /// </summary>
        private int type;
        public frmMain(int type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void ManagerMenu_Click(object sender, EventArgs e)
        {
            frmManagerInfo frmManager = frmManagerInfo.GetFrmManagerInfo();
            frmManager.Focus();
            frmManager.Show();
        }

        private void QuitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.ManagerMenu.Visible = false;
            if (this.type == 1)
            {
                this.ManagerMenu.Visible = true;
            }
        }

        private void MemberMenu_Click(object sender, EventArgs e)
        {
            frmMemberInfo frmMemberInfo = frmMemberInfo.GetFrmMemberInfo();
            frmMemberInfo.Focus();
            frmMemberInfo.Show();
        }
    }
}
