using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MerryTest.Entity;

namespace MasterMonitor
{
    public partial class Form1 : Form
    {
        UIAdaptiveSize uias;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uias = new UIAdaptiveSize
            {
                Width = Width,
                Height = Height,
                FormsName = this.Name,
                X = Width,
            };
            uias.SetInitSize(this);
            flag1 = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.WindowState = FormWindowState.Maximized;
            LoadModel();
            LoadComputer();
            timer1.Enabled = true;
            btnName.Enabled = false;
        }
        string model_name;
        
        bool flag1 = false;
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!flag1) return;
            var newx = Width;
            uias.UpdateSize(Width, Height, this);
            uias.X = newx;
            

        }
        //List<string> Model_name;
        
        private void LoadModel()
        {
            string cmd = $"Select computer,type_mode,test_times,pass_times,performency from Network where status1 = 'connected'";
            DataSet ds = MySqlHelper.Query(cmd);
            try
            {
                dataGridView1.DataSource = ds.Tables[0];
                var a = dataGridView1.Rows.Count;
                for (int i = 0; i <= a - 2; i++)
                {
                    //cbModel.Items.Clear();
                    string md = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string[] model = md.Split(' ');
                    if (!(cbModel.Items.Contains(model[0])))
                    {
                        cbModel.Items.Add(model[0]);
                    }
                    //cbModel.Items.Add(md);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect to SQL Fail");
            }
        }
        private void LoadComputer()
        {
            string cmd = "";
            switch (cbModel.Text)
            {
                case "":
                    cmd = $"Select computer,type_mode,test_times,pass_times,performency from Network where status1 = 'connected'";
                    break;
                case "ALL":
                    cmd = $"Select computer,type_mode,test_times,pass_times,performency from Network where status1 = 'connected'";

                    break;
                default:
                    cmd = $"Select computer,type_mode,test_times,pass_times,performency from Network where status1 = 'connected' and model = '{cbModel.Text }'";
                    break;
            }

            DataSet ds = MySqlHelper.Query(cmd);
            try
            {

                dataGridView1.DataSource = ds.Tables[0];
                //排列顺序Datagridview里的数据
                if (arrange == "name")
                {
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                }
                if (arrange == "performency")
                {
                    dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);
                }
                var a = dataGridView1.Rows.Count;
                //cbModel.Items.Clear();
                for (int i = 0; i <= a - 2; i++)
                {
                    double per=0;
                    string[] percentage = dataGridView1.Rows[i].Cells[4].Value.ToString().Split('%');
                    try
                    {
                        per = Convert.ToDouble(percentage[0]);
                    }
                    catch { }
                    
                    if(per == null || (per >= 90 && per <= 100))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if ((per >= 80 && per <= 90))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gold;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkRed;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    }


                    string md = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string[] model = md.Split(' ');
                    if (!(cbModel.Items.Contains(model[0])))
                    {
                        cbModel.Items.Add(model[0]);
                    }
                }
                

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Connect to SQL Fail");
            }
        }
        private string Check_Bit()
        {
            string cmd = $"Select connect from Model where model = '1'";
            try
            {
                DataSet ds = MySqlHelper.Query(cmd);
                dataGridView2.DataSource = ds.Tables[0];
                var a = dataGridView2.Rows[0].Cells[0].Value.ToString();
                return a;
            }
            catch
            {
                //MessageBox.Show("Get Bit fail");
                return "0";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Check_Bit() == "1")
            {
                //MessageBox.Show("ok");
                LoadComputer();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComputer();
        }
        string arrange = "name";
        private void btnName_Click(object sender, EventArgs e)
        {
            btnName.Enabled = false;
            btnName.BackColor = Color.OliveDrab;
            btnPerformency.Enabled = true;
            btnPerformency.BackColor = Color.LightGray;
            arrange = "name";
            timer1_Tick(null, null);
        }

        private void btnPerformency_Click(object sender, EventArgs e)
        {
            btnName.Enabled = true;
            btnName.BackColor = Color.LightGray;
            btnPerformency.Enabled = false;
            btnPerformency.BackColor = Color.OliveDrab;
            arrange = "performency";
            timer1_Tick(null, null);
        }
    }
}
