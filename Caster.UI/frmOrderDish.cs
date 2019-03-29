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
        private List<OrderDetailInfo> odInfoList;
        private BindingList<OrderDetailInfo> bindOdInfoList;
        private int index;
        private DishInfoBLL diBll;
        private DishTypeInfoBLL dtiBll;

        private OrderDetailInfoBLL odiBll;

        private OrderInfoBLL odBll;
        //厅包信息
        private ListView hallView;
        //餐桌信息
        private ListViewItem tableItem;
        public event Action ChangeTabPageImage;
        public frmOrderDish(ListView hallView, ListViewItem tableItem)
        {
            InitializeComponent();
            this.hallView = hallView;
            this.tableItem = tableItem;
            this.Text = "厅包：" + this.hallView.Tag + "餐桌：" + tableItem.Text + "-" + this.Text;
            this.diBll = new DishInfoBLL();
            this.dtiBll = new DishTypeInfoBLL();
            this.odiBll = new OrderDetailInfoBLL();
            this.odBll = new OrderInfoBLL();
        }

        private void frmOrderDish_Load(object sender, EventArgs e)
        {
            LoadDishInfoList();
            LoadTypeList();
            LoadOrderDetailInfoList();
            lblMoney.Text = GetOrderSumMoney().ToString();
        }

        private decimal? GetOrderSumMoney()
        {
            decimal? sum = 0;
            foreach (OrderDetailInfo orderDetailInfo in odInfoList)
            {
                sum += orderDetailInfo.Count * orderDetailInfo.ODPrice;
            }

            return sum;
        }

        private void LoadOrderDetailInfoList()
        {
            TableInfo ti = tableItem.Tag as TableInfo;
            int tableId = Convert.ToInt32(ti.TId);
            int orderId = odBll.GetOrderId(tableId);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("OrderId", orderId.ToString());
            odInfoList = odiBll.GetList(dic);
            this.index = odInfoList.Count + 1;
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bindOdInfoList = new BindingList<OrderDetailInfo>(odInfoList);
            dgvOrderDetail.DataSource = bindOdInfoList;
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
            if (e.RowIndex < 0) { return; }
            var row = dgvAllDish.DataSource as List<DishInfo>;
            var orderDetailRow = new OrderDetailInfo()
            {
                ODishId = index++,
                ODTitle = row[e.RowIndex].DTitle,
                DishId = row[e.RowIndex].DId,
                Count = 1,
                ODPrice = Convert.ToDecimal(row[e.RowIndex].DPrice)
            };
            bindOdInfoList.Add(orderDetailRow);
            odInfoList = BindingListToList();
            //odInfoList.Add(orderDetailRow);
            lblMoney.Text = GetOrderSumMoney().ToString();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetail.SelectedRows.Count <= 0)
            {
                MessageBox.Show("还未点菜，无法下单！");
                return;
            }
            OrderInfo oi = new OrderInfo();
            TableInfo ti = tableItem.Tag as TableInfo;
            oi.TableId = Convert.ToInt32(ti.TId);
            oi.OMoney = Convert.ToDecimal(lblMoney.Text);
            if (odBll.Order(ti, oi, odInfoList))
            {
                MessageBox.Show("下单成功！");
                this.Close();
                ChangeTabPageImage?.Invoke();
            }
            else
            {
                MessageBox.Show("下单失败");
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetail.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择需要删除的菜！");
                return;
            }

            if (MessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bindOdInfoList.RemoveAt(dgvOrderDetail.SelectedRows[0].Index);
                BindingListToList();
                odInfoList = BindingListToList();
                lblMoney.Text = GetOrderSumMoney().ToString();
            }
        }
        private List<OrderDetailInfo> BindingListToList()
        {

            List<OrderDetailInfo> list = new List<OrderDetailInfo>((BindingList<OrderDetailInfo>)this.dgvOrderDetail.DataSource);
            return list;
        }
        private void dgvOrderDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
