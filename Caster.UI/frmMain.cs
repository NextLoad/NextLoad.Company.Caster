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

namespace Caster.UI
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 权限设置
        /// </summary>
        private int type;

        private HallInfoBLL hiBll;

        private TableInfoBLL tiBll;
        public frmMain(int type)
        {
            InitializeComponent();
            this.type = type;
            this.hiBll = new HallInfoBLL();
            this.tiBll = new TableInfoBLL();
        }

        private void QuitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadtcHallInfo();
            this.ManagerMenu.Visible = false;
            if (this.type == 1)
            {
                this.ManagerMenu.Visible = true;
            }
        }

        private void LoadtcHallInfo()
        {
            this.tcHallInfo.TabPages.Clear();
            //1、获取厅包的集合
            var hallInfolist = hiBll.GetList();
            for (int i = 0; i < hallInfolist.Count; i++)
            {
                var tabPage = new TabPage(hallInfolist[i].HTitle);
                this.tcHallInfo.TabPages.Add(tabPage);

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("THallId", hallInfolist[i].HId.ToString());
                //2、获取一个厅包下的所有餐桌的集合
                var tableInfoList = tiBll.GetList(dic);
                ListView lvTableInfo = new ListView();
                lvTableInfo.Dock = DockStyle.Fill;
                lvTableInfo.LargeImageList = imageList1;
                lvTableInfo.Tag = hallInfolist[i].HId;
                //3、往各个tabpage中添加餐桌。
                foreach (var tableInfo in tableInfoList)
                {
                    var table = new ListViewItem(tableInfo.TTitle, tableInfo.TIsFree ? 0 : 1);
                    lvTableInfo.Items.Add(table);
                    tabPage.Controls.Add(lvTableInfo);
                }
                lvTableInfo.DoubleClick += LvTableInfo_DoubleClick;
            }
        }

        private void LvTableInfo_DoubleClick(object sender, EventArgs e)
        {
            ListView table = sender as ListView;
            //MessageBox.Show(table.SelectedItems[0].Text);
            frmOrderDish frmOrderDish = new frmOrderDish(table, table.SelectedItems[0]);
            frmOrderDish.Show();
        }

        private void ManagerMenu_Click(object sender, EventArgs e)
        {
            frmManagerInfo frmManager = frmManagerInfo.GetFrmManagerInfo();
            frmManager.Focus();
            frmManager.Show();
        }

        private void MemberMenu_Click(object sender, EventArgs e)
        {
            frmMemberInfo frmMemberInfo = frmMemberInfo.GetFrmMemberInfo();
            frmMemberInfo.Focus();
            frmMemberInfo.Show();
        }

        private void DishMenu_Click(object sender, EventArgs e)
        {
            frmDishInfo frmDish = frmDishInfo.GetFrmDishInfo();
            frmDish.Focus();
            frmDish.Show();
        }

        private void TableMenu_Click(object sender, EventArgs e)
        {
            frmTableInfo frmTableInfo = frmTableInfo.GetFrmTableInfo();
            frmTableInfo.ChangeFrmMainState += LoadtcHallInfo;
            frmTableInfo.Focus();
            frmTableInfo.Show();

        }
    }
}
