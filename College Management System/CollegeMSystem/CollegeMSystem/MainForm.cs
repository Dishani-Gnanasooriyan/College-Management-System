using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CollegeMSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            datacon();
            abc();
        }


        MySqlConnection con;

        void datacon()
        {
            try
            {
                con = new MySqlConnection("server= localhost; user=root;pwd=; database=college_system");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dataview()
        {

           /* string sql = "select  * from departmenttable";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);*/

           // dataGridView1.DataSource = dt;

        }

        void abc()
        {
            con.Open();
            MySqlDataAdapter sda12 = new MySqlDataAdapter("select sum(payment_fees) from fees", con);
            DataTable dt22 = new DataTable();
            sda12.Fill(dt22);
            feetb.Text = "Rs" +dt22.Rows[0][0].ToString();
            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {
       
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm You Want to Exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                log log = new log();
                log.Show();
                this.Hide();
            }
        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            student stu = new student();
            stu.Show();
            this.Hide();
        }

        private void ovalShape2_Click(object sender, EventArgs e)
        {
            student stu = new student();
            stu.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            teacher teach = new teacher();
            teach.Show();
            this.Hide();
        }

        private void ovalShape3_Click(object sender, EventArgs e)
        {
            teacher teach = new teacher();
            teach.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            dep1 de = new dep1();
            de.Show();
            this.Hide();
        }

        private void ovalShape4_Click(object sender, EventArgs e)
        {
            dep1 de = new dep1();
            de.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            fee1 f = new fee1();
            f.Show();
            this.Hide();
        }

        private void ovalShape5_Click(object sender, EventArgs e)
        {
            fee1 f = new fee1();
            f.Show();
            this.Hide();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            abc();

            con.Open();

            MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from studenttable",con);
            DataTable dt1 = new DataTable();
            sda.Fill(dt1);
            stdtb.Text = dt1.Rows[0][0].ToString();

            MySqlDataAdapter sda1 = new MySqlDataAdapter("select count(*) from teachertable", con);
            DataTable dt2 = new DataTable();
            sda1.Fill(dt2);
            teatb.Text = dt2.Rows[0][0].ToString();

           

            MySqlDataAdapter sda11 = new MySqlDataAdapter("select count(*) from departmenttable", con);
            DataTable dt21 = new DataTable();
            sda11.Fill(dt21);
            deptb.Text = dt21.Rows[0][0].ToString();

            con.Close();

        }

        private void feetb_Click(object sender, EventArgs e)
        {

        }

        private void teatb_Click(object sender, EventArgs e)
        {

        }
    }
}
