using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();

            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
        }

        Random rd = new Random();
        private void button4_Click(object sender, EventArgs e)   //陣列分類
        {
            int[] nums = { rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101),
                           rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101), rd.Next(0, 101)};
            var q2 = from n in nums group n by ad(n) into r orderby r.Key select new { Key = r.Key, Count = r.Count(), Avg = $"{r.Average():n2}" };
            var q1 = from n in nums group n by ad(n) into r orderby r.Key select new { Mykey = r.Key, MyCount = r.Count(), MyGroup = r };
           
            dataGridView1.DataSource = q2.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string t = $"{group.Mykey}({group.MyCount})";
                TreeNode x = treeView1.Nodes.Add(t);
                foreach (var item in group.MyGroup)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }

        private string ad(int n)
        {
            if (n < 40) return "低標";
            else if (n < 70) return "均標";
            else return "前標";
        }

        private void button38_Click(object sender, EventArgs e)  //依 檔案大小 分組檔案 (大=>小)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q2 = from f in files orderby f.Length descending select f;
            var q = from f in files group f by fl(f.Length) into j orderby j.Key ascending select new { Key = j.Key, Count = j.Count() , Group = j};
            this.dataGridView1.DataSource = q.ToList();
            dataGridView1.Columns[2].Visible = false;
            this.dataGridView2.DataSource = q2.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.Group)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        private string fl(long n)
        {
            if (n <= 1000) return "small";
            else if (n <= 100000) return "medium";
            else return "large";
        }

        private void button6_Click(object sender, EventArgs e)     //  依 年 分組檔案 (大=>小)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q2 = from f in files orderby f.CreationTime.Year descending select f;            
            var q = from f in files group f by f2(f.CreationTime.Year) into j orderby j.Key ascending select new { Key = j.Key, Count = j.Count() , Group = j};
            this.dataGridView1.DataSource = q.ToList();
            dataGridView1.Columns[2].Visible = false;
            this.dataGridView2.DataSource = q2.ToList();
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Key}({group.Count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.Group)
                {
                    x.Nodes.Add(item.ToString());
                }
            }
        }
        private string f2(long n)
        {
            if (n <= 2010) return "2010年以前";
            else if (n <= 2020) return "2020以前";
            else return "2020以後";
        }

        NorthwindEntities nw = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)     //NW Products 低中高 價產品 
        {
            var q = from p in nw.Products.AsEnumerable() where p.UnitPrice > 0 orderby p.UnitPrice ascending select p;
            this.dataGridView2.DataSource = q.ToList();
            var q2 = from p in nw.Products.AsEnumerable() group p by po(Convert.ToDouble(p.UnitPrice)) into o orderby o.Key descending select new { mkey = o.Key, mcount = o.Count() , mgroup = o};
            this.dataGridView1.DataSource = q2.ToList();
            dataGridView1.Columns[2].Visible = false;
            treeView1.Nodes.Clear();
            foreach (var group in q2)
            {
                string g = $"{group.mkey}({group.mcount})";
                TreeNode x = treeView1.Nodes.Add(g);
                foreach (var item in group.mgroup)
                {
                    string y = $"{"ID: ".PadLeft(5) + item.ProductID + "".PadRight(10)}{"Product Name:".PadRight(15) + item.ProductName}({item.UnitPrice:n2})";
                    x.Nodes.Add(y);
                }
            }
        }
        private string po(double n)
        {
            if (n <= 25) return "Low Price";
            else if (n <= 60) return "Just Ok";
            else return "High Price";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var q1 = from y in nw.Orders.AsEnumerable()
                     orderby y.OrderDate.Value.Year
                     select y;
            var q = from y in nw.Orders.AsEnumerable()
                    group y by y.OrderDate.Value.Year into o
                    select new { key = o.Key, count = o.Count(), };
            this.dataGridView2.DataSource = q1.ToList();
            this.dataGridView1.DataSource = q.ToList();
            var q2 = from y in nw.Orders.AsEnumerable()
                     group y by y.OrderDate.Value.Year into o
                     select new { key = o.Key, count = o.Count(), house = o };
            treeView1.Nodes.Clear();
            foreach (var group in q2)
            {
                string s = $"{group.key}({group.count})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.house)
                {
                    string y = $"{"Order ID: ".PadLeft(5) + item.OrderID + "".PadRight(10)}{"Order Date:".PadRight(15) + item.OrderDate}";
                    x.Nodes.Add(y);
                }
            }
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            var q = (from y in nwDataSet1.Orders
                     orderby y.OrderDate.Year ascending
                     select y.OrderDate.Year).Distinct();
            comboBox1.DataSource = q.ToList();
        }
             
        private void button10_Click(object sender, EventArgs e)
        {
            var q1 = this.nw.Orders.OrderBy(p => p.OrderDate.Value.Year).ThenBy(p => p.OrderDate.Value.Month);
            this.dataGridView2.DataSource = q1.ToList();
            var q6 = from p in nw.Orders
                     group p by p.OrderDate.Value.Year into k
                     select new 
                     { ykey = k.Key, ycount = k.Count(), 
                         mm = k.GroupBy(m => m.OrderDate.Value.Month).Select(m => new { mkey = m.Key, mcount = m.Count(), monthp = m }) };
            this.dataGridView1.DataSource = q6.ToList();
            dataGridView1.Columns[2].Visible = false;
            treeView1.Nodes.Clear();
            foreach (var group in q6)
            {
                string s = $"{group.ykey}({group.ycount})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.mm)
                {
                    string t = $"{item.mkey}({item.mcount})";
                    TreeNode y = x.Nodes.Add(t);
                    foreach (var mon in item.monthp)
                    {
                        y.Nodes.Add(mon.CustomerID);
                    }
                }
            };
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = (from n in nw.Order_Details.AsEnumerable()
                     where n.OrderID > 0
                     select (int)(Convert.ToDecimal(n.UnitPrice) * Convert.ToDecimal(n.Quantity) * Convert.ToDecimal((1 - n.Discount)))).Sum();
            var q1 = from n in nw.Order_Details
                     where n.OrderID > 0
                     select n;
            this.dataGridView2.DataSource = q1.ToList();            
            var q2 = nw.Order_Details.AsEnumerable().GroupBy(p => p.OrderID).Select(p => new { Order_ID = p.Key, Total =(int) (p.Sum(o => Convert.ToDecimal(o.UnitPrice) * Convert.ToDecimal(o.Quantity) * (Convert.ToDecimal((1 - o.Discount)))) )});
            dataGridView1.DataSource = q2.ToList();
            MessageBox.Show("年銷售額：" + q.ToString() + "元");
        }

        private void button1_Click(object sender, EventArgs e)  //銷售最高前五名銷售員
        {           
            var q = (from w in nw.Orders.AsEnumerable()
                    join p in nw.Order_Details on w.OrderID equals p.OrderID
                    group p by w.EmployeeID into k
                    select new { EmployeeID = k.Key, Total = (int)(k.Sum(b => b.UnitPrice * b.Quantity * (decimal)(1 - b.Discount)))}).OrderByDescending(u=>u.Total);
            var q1 = q.Take(5);
            dataGridView2.DataSource = q.ToList();
            dataGridView1.DataSource = q1.ToList();            
        }

        private void button9_Click(object sender, EventArgs e)  //單價最高前五筆商品
        {
            var q = nw.Products.OrderByDescending(p=>p.UnitPrice).Select(p => new {ProductName=p.ProductName,CategoryName=p.Category.CategoryName, Price =p.UnitPrice});
            dataGridView2.DataSource = q.ToList();
            var q1 = q.Take(5);
            dataGridView1.DataSource = q1.ToList();
        }

        private void button3_Click(object sender, EventArgs e)  //單價>300
        {
            var q = nw.Products.Where(p => p.UnitPrice > 300).Select(p => p).Any();
            if (q)
            {
                MessageBox.Show("單價超過300的商品:"+q.ToString());
            }
            else
            {
                MessageBox.Show("沒有商品的單價超過300");
            }
        }

        private void comboBox2_MouseDown(object sender, MouseEventArgs e)  //抓年分
        {
            var q = (nw.Orders.AsEnumerable().GroupBy(p=>p.OrderDate.Value.Year).Select(p =>p.Key)).ToList();
            dataGridView1.DataSource = q.ToList();            
            comboBox2.DataSource = q;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            //var q1 = nw.Orders.AsEnumerable().GroupBy(p => p.OrderDate.Value.Year).Select(p => new { Year = p.Key, Total = p.Sum(a => a.Order_Details.) }));

            var q= nw.Orders.AsEnumerable().GroupBy(p => p.OrderDate.Value.Year).Select(p => new { Year=p.Key ,Total=(int)(p.Sum(a=>a.Order_Details.Sum(v=>v.Quantity*v.UnitPrice*(decimal)(1-v.Discount)))),AnnulOrder=p.Count(),group=p});
            dataGridView1.DataSource = q.ToList();
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "Year";
            this.chart1.Series[0].YValueMembers = "Total";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.Year}({group.AnnulOrder})";
                TreeNode x = treeView1.Nodes.Add(s);
                foreach (var item in group.group)
                {
                    string t = $"{"OrderDate:".PadLeft(5) + item.OrderDate+"".PadRight(5)}{"ProductID:" + (item.Order_Details.Sum(p => p.ProductID)) + "".PadRight(5)}{"數量:".PadRight(5) + item.Order_Details.Sum(p => p.Quantity) + "".PadRight(5)}";
                    x.Nodes.Add(t);
                }
            }

        }
    }
}
