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
    public partial class frmMemberTypeInfo : Form
    {
        private MemberTypeInfoBLL mtiBll;
        public event Action MemberTypeInfoChange;
        public frmMemberTypeInfo()
        {
            InitializeComponent();
            this.mtiBll = new MemberTypeInfoBLL();
        }

        private void frmMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            List<MemberTypeInfo> list = mtiBll.GetList();
            dgvList.AutoGenerateColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.DataSource = list;
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
            btnSave.Text = "修改";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void ClearText()
        {
            txtId.Text = "添加时无编号";
            txtDiscount.Text = "";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemberTypeInfo mti = new MemberTypeInfo();
            mti.MDiscount = Convert.ToDecimal(txtDiscount.Text);
            mti.MTitle = txtTitle.Text;
            if (txtId.Text == "添加时无编号")//表示增加
            {
                if (mtiBll.Add(mti))
                {
                    LoadList();
                    ClearText();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍后再试！");
                }
            }
            else
            {
                mti.MId = Convert.ToInt32(txtId.Text);
                if (mtiBll.Edit(mti))
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            if (mtiBll.Remove(id))
            {
                LoadList();
                ClearText();
            }
            else
            {
                MessageBox.Show("删除失败，请稍后再试！");
            }
        }

        private void frmMemberTypeInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            MemberTypeInfoChange?.Invoke();
        }
    }
}
