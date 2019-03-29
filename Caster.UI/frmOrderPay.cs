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
    public partial class frmOrderPay : Form
    {
        private MemberInfoBLL miBll;
        private OrderInfoBLL oiBll;
        private OrderInfo oi;
        public event Action ChangeTabPageImage;
        public frmOrderPay()
        {
            InitializeComponent();
            this.miBll = new MemberInfoBLL();
            this.oiBll = new OrderInfoBLL();
        }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            gbMember.Enabled = cbkMember.Checked;
            if (cbkMember.Checked)
            {
                lblPayMoneyDiscount.Text = (Convert.ToDecimal(lblPayMoney.Text) * Convert.ToDecimal(lblDiscount.Text)).ToString();
            }
            else
            {
                lblPayMoneyDiscount.Text = Convert.ToDecimal(lblPayMoney.Text).ToString();
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            int memberId = Convert.ToInt32(txtId.Text);
            MemberInfo memberInfo = miBll.GetMemberInfoByMId(memberId);
            if (memberInfo == null)
            {
                MessageBox.Show("会员信息有误！");
                return;
            }
            oi.MemberId = memberId;
            lblMoney.Text = memberInfo.MMoney.ToString();
            lblDiscount.Text = memberInfo.Mdiscount.ToString();
            lblTypeTitle.Text = memberInfo.Mtitle;
            lblPayMoneyDiscount.Text = (Convert.ToDecimal(lblPayMoney.Text) * Convert.ToDecimal(lblDiscount.Text)).ToString();
        }

        private void frmOrderPay_Load(object sender, EventArgs e)
        {
            TableInfo ti = this.Tag as TableInfo;
            int orderId = oiBll.GetOrderId(ti.TId);
            oi = oiBll.GetOrderInfo(orderId);
            lblPayMoney.Text = GetPayMoney().ToString();
            lblPayMoneyDiscount.Text = (Convert.ToDecimal(lblPayMoney.Text) * Convert.ToDecimal(lblDiscount.Text)).ToString();
        }

        private decimal GetPayMoney()
        {
            TableInfo ti = this.Tag as TableInfo;
            OrderDetailInfoBLL odiBll = new OrderDetailInfoBLL();
            return odiBll.GetPayMoney(oi.OId);
        }

        private void cbkMoney_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkMoney.Checked)
            {
                oi.OMoney = Convert.ToDecimal(lblPayMoneyDiscount.Text);
                oi.Discount = Convert.ToDecimal(lblDiscount.Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrderPay_Click(object sender, EventArgs e)
        {


            if (oiBll.PayOrder(oi, cbkMoney.Checked))
            {
                MessageBox.Show("结账成功");
                this.Close();
                ChangeTabPageImage?.Invoke();
            }
            else
            {
                MessageBox.Show("结账失败，请稍后再试！");
            }

        }
    }
}
