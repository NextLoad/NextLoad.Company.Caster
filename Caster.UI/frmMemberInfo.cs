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
using Caster.Model;

namespace Caster.UI
{
    public partial class frmMemberInfo : Form
    {
        private MemberInfoBLL miBll;
        public frmMemberInfo()
        {
            InitializeComponent();
            this.miBll = new MemberInfoBLL();
        }

        private void frmMemberInfo_Load(object sender, EventArgs e)
        {
            LoadList();

        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                dic.Add("MName", txtNameSearch.Text);
            }

            if (!string.IsNullOrEmpty(txtPhoneSearch.Text))
            {
                dic.Add("MPhone", txtPhoneSearch.Text);
            }
            List<MemberInfo> list = miBll.GetList(dic);
            dgvList.AutoGenerateColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.DataSource = list;
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            txtPhoneSearch.Text = "";
            LoadList();
        }

        private void txtPhoneSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
