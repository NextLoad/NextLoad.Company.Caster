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
using Caster.Common;
using Caster.Model;

namespace Caster.UI
{
    public partial class frmDishInfo : Form
    {
        private DishInfoBLL diBll;
        private DishTypeInfoBLL dtiBll;
        private static frmDishInfo frmDish;
        private static object obj = new object();


        private frmDishInfo()
        {
            InitializeComponent();
            this.diBll = new DishInfoBLL();
            this.dtiBll = new DishTypeInfoBLL();
        }

        public static frmDishInfo GetFrmDishInfo()
        {
            if (frmDish == null)
            {
                lock (obj)
                {
                    if (frmDish == null)
                    {
                        frmDish = new frmDishInfo();
                    }
                }
            }

            return frmDish;
        }
        private void frmDishInfo_Load(object sender, EventArgs e)
        {
            LoadList();
            LoadTypeList();
        }

        private void LoadTypeList()
        {
            List<DishTypeInfo> list = dtiBll.GetList();
            ddlTypeAdd.DataSource = list;

            List<DishTypeInfo> list2 = dtiBll.GetList();
            list2.Insert(0, new DishTypeInfo());
            ddlTypeSearch.DataSource = list2;
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtTitleSearch.Text))
            {
                dic.Add("DTitle", txtTitleSearch.Text);
            }

            if (!string.IsNullOrEmpty(ddlTypeSearch.Text))
            {
                dic.Add("DTypeId", dtiBll.GetTypeId(ddlTypeSearch.Text).ToString());
            }
            List<DishInfo> list = diBll.GetList(dic);
            dgvList.DataSource = list;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DishInfo di = new DishInfo();
            di.DTitle = txtTitleSave.Text;
            di.DPrice = Convert.ToDecimal(txtPrice.Text);
            di.DTypeId = dtiBll.GetTypeId(ddlTypeAdd.Text);
            di.DChar = txtChar.Text;
            if (txtId.Text == "添加时无编号")//表示添加
            {
                if (diBll.Add(di))
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
                di.DId = Convert.ToInt32(txtId.Text);
                if (diBll.Edit(di))
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
            txtChar.Text = "";
            txtPrice.Text = "";
            txtTitleSave.Text = "";
            txtTitleSearch.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            ddlTypeSearch.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void txtTitleSave_Leave(object sender, EventArgs e)
        {
            try
            {
                txtChar.Text = PinYinHelper.GetPinYin(txtTitleSave.Text);
            }
            catch
            {
                txtChar.Text = txtTitleSave.Text;
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择一行需要删除的数据！");
                return;
            }

            if (MessageBox.Show("是否删除数据？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (diBll.Remove(Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value)))
                {
                    LoadList();
                    LoadTypeList();
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
            txtTitleSearch.Text = "";
            ddlTypeSearch.Text = "";
            LoadList();
        }

        private void txtTitleSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmDishTypeInfo frmDishType = new frmDishTypeInfo();
            if (frmDishType.ShowDialog() == DialogResult.OK)
            {
                LoadList();
                LoadTypeList();
            }
        }

        private void frmDishInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmDish = null;
        }
    }
}
