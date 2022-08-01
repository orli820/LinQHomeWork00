using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs.作業;
using MyHomeWork;

namespace prjAdoDotNetDemo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Frm作業_1 f = new Frm作業_1();
            f.MdiParent = this;    //告訴他他爸是誰
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Frm作業_3 f3 = new Frm作業_3();
            f3.MdiParent = this;    
            f3.WindowState = FormWindowState.Maximized;
            f3.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }

        private void 水平排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 垂直排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void 階梯式排列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void 關閉目前視窗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild!=null)  //如果不是null關閉視窗
                this.ActiveMdiChild.Close();
        }

        private void 關閉所有視窗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (this.ActiveMdiChild != null)    //只要是null關閉視窗
                this.ActiveMdiChild.Close();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //(new login()).ShowDialog();
            
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Frm作業_2 f2 = new Frm作業_2();
            f2.MdiParent = this;    
            f2.WindowState = FormWindowState.Maximized;
            f2.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Frm作業_4 f4 = new Frm作業_4();
            f4.MdiParent = this;
            f4.WindowState = FormWindowState.Maximized;
            f4.Show();
        }
    }
}
