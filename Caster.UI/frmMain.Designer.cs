namespace Caster.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tcHallInfo = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ManagerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MemberMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DishMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OrderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TableMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcHallInfo
            // 
            this.tcHallInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHallInfo.Location = new System.Drawing.Point(0, 72);
            this.tcHallInfo.Name = "tcHallInfo";
            this.tcHallInfo.SelectedIndex = 0;
            this.tcHallInfo.Size = new System.Drawing.Size(746, 430);
            this.tcHallInfo.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "desk1.png");
            this.imageList1.Images.SetKeyName(1, "desk2.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::Caster.UI.Properties.Resources.menuBg;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ManagerMenu,
            this.MemberMenu,
            this.DishMenu,
            this.OrderMenu,
            this.TableMenu,
            this.QuitMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(746, 72);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ManagerMenu
            // 
            this.ManagerMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ManagerMenu.Image = global::Caster.UI.Properties.Resources.menuManager;
            this.ManagerMenu.Name = "ManagerMenu";
            this.ManagerMenu.Size = new System.Drawing.Size(76, 68);
            this.ManagerMenu.Text = "toolStripMenuItem1";
            this.ManagerMenu.Click += new System.EventHandler(this.ManagerMenu_Click);
            // 
            // MemberMenu
            // 
            this.MemberMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MemberMenu.Image = global::Caster.UI.Properties.Resources.menuMember;
            this.MemberMenu.Name = "MemberMenu";
            this.MemberMenu.Size = new System.Drawing.Size(76, 68);
            this.MemberMenu.Text = "toolStripMenuItem2";
            this.MemberMenu.Click += new System.EventHandler(this.MemberMenu_Click);
            // 
            // DishMenu
            // 
            this.DishMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DishMenu.Image = global::Caster.UI.Properties.Resources.menuDish;
            this.DishMenu.Name = "DishMenu";
            this.DishMenu.Size = new System.Drawing.Size(76, 68);
            this.DishMenu.Text = "toolStripMenuItem3";
            this.DishMenu.Click += new System.EventHandler(this.DishMenu_Click);
            // 
            // OrderMenu
            // 
            this.OrderMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OrderMenu.Image = global::Caster.UI.Properties.Resources.menuOrder;
            this.OrderMenu.Name = "OrderMenu";
            this.OrderMenu.Size = new System.Drawing.Size(76, 68);
            this.OrderMenu.Text = "toolStripMenuItem4";
            this.OrderMenu.Click += new System.EventHandler(this.OrderMenu_Click);
            // 
            // TableMenu
            // 
            this.TableMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TableMenu.Image = global::Caster.UI.Properties.Resources.menuTable;
            this.TableMenu.Name = "TableMenu";
            this.TableMenu.Size = new System.Drawing.Size(76, 68);
            this.TableMenu.Text = "toolStripMenuItem5";
            this.TableMenu.Click += new System.EventHandler(this.TableMenu_Click);
            // 
            // QuitMenu
            // 
            this.QuitMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.QuitMenu.Image = global::Caster.UI.Properties.Resources.menuQuit;
            this.QuitMenu.Name = "QuitMenu";
            this.QuitMenu.Size = new System.Drawing.Size(76, 68);
            this.QuitMenu.Text = "toolStripMenuItem6";
            this.QuitMenu.Click += new System.EventHandler(this.QuitMenu_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 502);
            this.Controls.Add(this.tcHallInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "主窗体";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ManagerMenu;
        private System.Windows.Forms.ToolStripMenuItem MemberMenu;
        private System.Windows.Forms.ToolStripMenuItem DishMenu;
        private System.Windows.Forms.ToolStripMenuItem OrderMenu;
        private System.Windows.Forms.ToolStripMenuItem TableMenu;
        private System.Windows.Forms.ToolStripMenuItem QuitMenu;
        private System.Windows.Forms.TabControl tcHallInfo;
        private System.Windows.Forms.ImageList imageList1;
    }
}