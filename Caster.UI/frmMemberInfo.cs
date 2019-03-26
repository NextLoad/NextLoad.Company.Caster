using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Caster.BLL;
using Caster.Model;

namespace Caster.UI
{
    public partial class frmMemberInfo : Form
    {
        private MemberInfoBLL miBll;
        private MemberTypeInfoBLL mtiBll;
        private int rowIndex;

        private static object obj = new object();
        private static frmMemberInfo frmMember;

        public int RowIndex
        {
            get
            {
                if (rowIndex < 0)
                {
                    return 0;
                }
                else if (rowIndex >= dgvList.RowCount)
                {
                    return dgvList.RowCount - 1;
                }
                else
                {
                    return rowIndex;
                }
            }
            set { rowIndex = value; }
        }
        private frmMemberInfo()
        {
            InitializeComponent();
            this.miBll = new MemberInfoBLL();
            this.mtiBll = new MemberTypeInfoBLL();
        }

        public static frmMemberInfo GetFrmMemberInfo()
        {
            if (frmMember == null)
            {
                lock (obj)
                {
                    if (frmMember == null)
                    {
                        frmMember = new frmMemberInfo();
                    }
                }
            }

            return frmMember;
        }

        private void frmMemberInfo_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
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
            if (list.Count < 0) return;
            dgvList.Rows[RowIndex].Selected = true;
        }

        private void LoadTypeList()
        {
            List<MemberTypeInfo> list = mtiBll.GetList();
            ddlType.DataSource = list;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberInfo mi = new MemberInfo();
            mi.MName = txtNameAdd.Text;
            mi.MPhone = txtPhoneAdd.Text;
            mi.MMoney = Convert.ToDecimal(txtMoney.Text);
            //List<MemberInfo> list = dgvList.DataSource as List<MemberInfo>;
            mi.MTypeId = mtiBll.GetMTypeId(ddlType.Text);
            if (txtId.Text == "添加时无编号")//表示增加
            {
                if (miBll.Add(mi))
                {
                    LoadList();
                    LoadTypeList();
                    ClearText();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍后再试！");
                }
            }
            else
            {
                mi.MId = Convert.ToInt32(txtId.Text);
                if (miBll.Eidt(mi))
                {
                    LoadList();
                    LoadTypeList();
                    ClearText();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍后再试！");
                }
            }
        }

        private void ClearText()
        {
            txtId.Text = "添加时无编号";
            txtMoney.Text = "";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择一行要删除的数据！");
            }
            if (MessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (miBll.Remove(Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value)))
                {
                    LoadList();
                    LoadTypeList();
                }
                else
                {
                    MessageBox.Show("删除失败，请稍后再试！");
                }
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmMemberTypeInfo frmMemberType = new frmMemberTypeInfo();
            frmMemberType.MemberTypeInfoChange += LoadList;
            frmMemberType.MemberTypeInfoChange += LoadTypeList;
            frmMemberType.ShowDialog();
        }

        private void frmMemberInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMember = null;
        }
    }
}
