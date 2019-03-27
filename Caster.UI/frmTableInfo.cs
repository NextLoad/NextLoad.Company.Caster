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
    public partial class frmTableInfo : Form
    {
        private TableInfoBLL tiBll;
        private HallInfoBLL hiBll;
        public frmTableInfo()
        {
            InitializeComponent();
            this.tiBll = new TableInfoBLL();
            this.hiBll = new HallInfoBLL();
        }

        private void frmTableInfo_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (ddlFreeSearch.SelectedIndex > 0)
            {
                dic.Add("TIsFree", ddlFreeSearch.SelectedValue.ToString());
            }

            if (ddlHallSearch.SelectedIndex > 0)
            {
                dic.Add("THallId", ddlHallSearch.SelectedValue.ToString());
            }
            dgvList.DataSource = tiBll.GetList(dic);


            
        }

        private void LoadTypeList()
        {
            List<TableInfo> tableInfos = new List<TableInfo>();
            tableInfos.Add(new TableInfo() { THTitle = "全部" });
            tableInfos.Add(new TableInfo() { THTitle = "空闲", TId = 1 });
            tableInfos.Add(new TableInfo() { THTitle = "使用中", TId = 0 });
            ddlFreeSearch.ValueMember = "TId";
            ddlFreeSearch.DisplayMember = "THTitle";
            ddlFreeSearch.DataSource = tableInfos;

            ddlHallSearch.ValueMember = "HId";
            ddlHallSearch.DisplayMember = "HTitle";
            List<HallInfo> listHallInfos = hiBll.GetList();
            listHallInfos.Insert(0, new HallInfo() { HTitle = "全部" });
            ddlHallSearch.DataSource = listHallInfos;

            ddlHallAdd.ValueMember = "HId";
            ddlHallAdd.DisplayMember = "HTitle";
            ddlHallAdd.DataSource = hiBll.GetList();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "√" : "×";
            }
        }

        private void btnAddHall_Click(object sender, EventArgs e)
        {
            frmHallInfo frmHall = new frmHallInfo();
            frmHall.ChangeList += LoadList;
            frmHall.ShowDialog();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            rbFree.Checked = Convert.ToBoolean(row.Cells[3].Value);
            rbUnFree.Checked = !rbFree.Checked;
            btnSave.Text = "修改";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void ClearText()
        {
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TableInfo ti = new TableInfo();
            ti.THallId = Convert.ToInt32(ddlHallAdd.SelectedValue);
            ti.TIsFree = rbFree.Checked;
            ti.TTitle = txtTitle.Text;
            if (txtId.Text == "添加时无编号")//添加
            {
                if (tiBll.Add(ti))
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
                ti.TId = Convert.ToInt32(txtId.Text);
                if (tiBll.Edit(ti))
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
            if (dgvList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择一行要删除的数据！");
                return;
            }

            if (MessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (tiBll.Remove(Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value)))
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

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
            ddlFreeSearch.SelectedIndex = 0;
            ddlHallSearch.SelectedIndex = 0;
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
