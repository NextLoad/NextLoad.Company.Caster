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
    public partial class frmOrderDish : Form
    {
        List<OrderDetailInfo> odInfoList = new List<OrderDetailInfo>();
        private int index;
        private DishInfoBLL diBll;
        private DishTypeInfoBLL dtiBll;
        //厅包信息
        private ListView hallView;
        //餐桌信息
        private ListViewItem tableItem;
        public frmOrderDish(ListView hallView, ListViewItem tableItem)
        {
            InitializeComponent();
            this.hallView = hallView;
            this.tableItem = tableItem;
            this.Text = "厅包：" + this.hallView.Tag + "餐桌：" + tableItem.Text + "-" + this.Text;
            this.diBll = new DishInfoBLL();
            this.dtiBll = new DishTypeInfoBLL();
            this.index = odInfoList.Count +1;
        }

        private void frmOrderDish_Load(object sender, EventArgs e)
        {
            LoadDishInfoList();
            LoadTypeList();
        }

        private void LoadTypeList()
        {
            ddlType.ValueMember = "DId";
            ddlType.DisplayMember = "DTitle";
            var list = dtiBll.GetList();
            list.Insert(0, new DishTypeInfo() { DTitle = "全部" });
            ddlType.DataSource = list;
        }

        private void LoadDishInfoList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                dic.Add("DChar", txtTitle.Text);
            }

            if (ddlType.SelectedIndex > 0)
            {
                dic.Add("DTypeId", ddlType.SelectedValue.ToString());
            }
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllDish.DataSource = diBll.GetList(dic);
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfoList();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfoList();
        }

        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvAllDish.Rows[e.RowIndex];
            odInfoList.Add(new OrderDetailInfo()
            {
                ODishId = index++,
                ODTitle = row.Cells[1].Value.ToString(),
                Count = 1,
                ODPrice = Convert.ToDecimal(row.Cells[2].Value)
            });
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderDetail.DataSource = null;
            dgvOrderDetail.DataSource = odInfoList;
            dgvOrderDetail.Refresh();
        }
    }
}
