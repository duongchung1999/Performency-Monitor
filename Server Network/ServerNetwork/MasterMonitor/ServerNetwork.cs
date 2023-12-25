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
using SQL_Test;

namespace MasterMonitor
{
    public partial class ServerNetwork : Form
    {
        UIAdaptiveSize uias;
        Express sql = new Express();
        public ServerNetwork()
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
            timer1.Enabled = true;
        }

        bool flag1 = false;
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!flag1) return;
            var newx = Width;
            uias.UpdateSize(Width, Height, this);
            uias.X = newx;

        }

        private void ResetAllComputerBit()
        {
            string cmd = $"Update Network set status1 = 'disconnected'";
            DataSet ds = MySqlHelper.Query(cmd);
            Thread.Sleep(100);
        }
        private void readLog()
        {
            string cmd = $"Select * from Network_History";
            DataSet ds = MySqlHelper.Query(cmd);
            dataGridView1.DataSource = ds.Tables[0];
            //Thread.Sleep(1000);
        }
        bool flag2 = false;
        private void CheckComputer_SQL()
        {
            //update_Bit("0");
            //Thread.Sleep(1500);
            if (!flag2) BeforeConnectList = GetListCPU_Connect();

            flag2 = true;
            ResetAllComputerBit();
            Thread.Sleep(3000);
            //update_Bit("1");
            AfterConnectList = GetListCPU_Connect();
            if (BeforeConnectList != null)
            {
                foreach (string computer in BeforeConnectList)
                {
                    if (!(AfterConnectList.Contains(computer)))
                    {
                        InsertStatus_SQL(computer, "disconnected");
                    }
                }
            }
            if (AfterConnectList != null)
            {
                foreach (string computer in AfterConnectList)
                {
                    if (!(BeforeConnectList.Contains(computer)))
                    {
                        InsertStatus_SQL(computer, "connected");
                    }
                }
            }
            BeforeConnectList = AfterConnectList;


        }
        List<string> AfterConnectList = new List<string>();
        List<string> BeforeConnectList = new List<string>();
        private List<string> GetListCPU_Connect()
        {
            List<string> List = new List<string>();
            string cmd = $"Select computer,status1 from Network";
            
            DataSet ds = MySqlHelper.Query(cmd);
            try
            {
                dataGridView1.DataSource = ds.Tables[0];
                var a = dataGridView1.Rows.Count;
                for (int i = 0; i <= a - 2; i++)
                {
                    var status = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    var CPU = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (status == "connected")
                    {
                        List.Add(CPU);
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return List;
        }
        private void InsertStatus_SQL(string computer, string status)
        {
            string  model= computer.Split(' ')[0];
            string cmd = $"insert into Network_History (computer,status1,time_change,model) values ('{computer}','{status}','{DateTime.Now:yyyy-MM-dd HH:mm:ss}','{model}')";
            DataSet ds = MySqlHelper.Query(cmd);
        }
        private void update_Bit(string bit)
        {
            string cmd = $"update Model set connect = {bit} where model = '1'";
            try
            {
                MySqlHelper.Query(cmd);
            }
            catch
            {
                MessageBox.Show("update_bit fail");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            update_Bit("0");
            CheckComputer_SQL();
            update_Bit("1");
            //readLog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ServerNetwork_FormClosed(object sender, FormClosedEventArgs e)
        {
            fDiaglog f = new fDiaglog("关闭程序会影响到产线程序的上转记录信息\n请及时重新打开\nTắt chương trình sẽ ảnh hưởng đến quá trình gửi dữ liệu của chương trình trên chuyền\nVui lòng mở lại kịp thời");

        }
    }
}
