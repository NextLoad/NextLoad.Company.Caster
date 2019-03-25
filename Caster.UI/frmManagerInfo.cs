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
    public partial class frmManagerInfo : Form
    {
        private ManagerInfoBLL miBll;
        public frmManagerInfo()
        {
            InitializeComponent();
            miBll = new ManagerInfoBLL();
        }

        private void frmManagerInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        /// <summary>
        /// 加载数据列表
        /// </summary>
        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.DataSource = miBll.GetList();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                e.Value = e.Value.ToString() == "0" ? "店员" : "经理";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ManagerInfo mi = new ManagerInfo();
            //1、构造ManagerInfo对象
            mi.MName = txtName.Text;
            mi.MPwd = txtPwd.Text;
            mi.MType = rb1.Checked ? 1 : 0;


            if (txtId.Text.Equals("添加时无编号"))//表示增加数据
            {

                //表示增加成功
                if (miBll.Add(mi))
                {
                    LoadList();
                    ClearText();
                }
                //表示增加失败
                else
                {
                    MessageBox.Show("添加失败，请稍后重试！");
                }
            }
            else//表示修改数据
            {
                mi.MId = Convert.ToInt32(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
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
            btnSave.Text = "添加";
            txtPwd.Text = "";
            txtName.Text = "";
            txtId.Text = "添加时无编号";
            rb2.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtPwd.Text = "这是一个不可能被设置的密码";
            rb1.Checked = row.Cells[2].Value.ToString() == "0" ? false : true;
            rb2.Checked = !rb1.Checked;
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
                int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
                miBll.Remove(id);
                LoadList();
                ClearText();
            }
        }
    }
}
