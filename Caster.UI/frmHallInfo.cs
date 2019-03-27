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
    public partial class frmHallInfo : Form
    {
        private HallInfoBLL hiBll;

        public event Action ChangeList; 
        public frmHallInfo()
        {
            InitializeComponent();
            this.hiBll = new HallInfoBLL();
        }

        private void frmHallInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.DataSource = hiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo hi = new HallInfo();
            hi.HTitle = txtTitle.Text;
            if (txtId.Text == "添加时无编号") //增加
            {
                if (hiBll.Add(hi))
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
                hi.HId = Convert.ToInt32(txtId.Text);
                if (hiBll.Edit(hi))
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void ClearText()
        {
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择一行需要删除的数据！");
                return;
            }

            if (MessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (hiBll.Remove(Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value)))
                {
                    LoadList();
                    ClearText();
                }
                else
                {
                    MessageBox.Show("删除失败，请稍后再试！");
                }
            }
        }

        private void frmHallInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChangeList?.Invoke();
        }
    }
}
