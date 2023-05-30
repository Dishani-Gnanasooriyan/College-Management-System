using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CollegeMSystem
{
    public partial class dep1 : Form
    {
        public dep1()
        {
            InitializeComponent();
            datacon();
            dataview();
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

            string sql = "select  * from departmenttable";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        void clearall()
        {
            depnametb.Clear();
            depdesctb.Clear();
            depdurationtb.Clear();
            depamounttb.Clear();

            depnametb.Focus();
        }


        private void dep1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                if (depnametb.Text != "" && depdesctb.Text != "" && depdurationtb.Text != "" && depamounttb.Text != "")
                {

                    con.Open();
                    string sql = "insert into departmenttable values('" + depnametb.Text + "','" + depdesctb.Text + "','" + depdurationtb.Text + "','" + depamounttb.Text + "')";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("department Successfully Added");
                    dataview();
                    clearall();

                
                }
                else
                {
                    MessageBox.Show("Enter the All Data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong");
            }
        
            con.Close();
            
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm You Want to Exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           try
            {
               
                if (depnametb.Text != "" && depdesctb.Text != "" && depdurationtb.Text != "" && depamounttb.Text != "")
                {

                con.Open();
                string sql = "delete from departmenttable where depname='" + depnametb.Text + "' ";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully deleted", "confirmation");
                dataview();
                clearall();
                
                }
                    else
                    {
                        MessageBox.Show("Enter the All Data");
                    }
                }

           
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong");
            }

            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (depnametb.Text != "" && depdesctb.Text != "" && depdurationtb.Text != "" && depamounttb.Text != "")
                {

                    con.Open();
                    string sql = "update departmenttable set depduration='" + depdurationtb.Text + "',depdesc='" + depdesctb.Text + "',amount='" + depamounttb.Text + "' where depname='" + depnametb.Text + "'";

                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Department Updated Successfully");
                    dataview();
                    clearall();
                }
                else
                {
                    MessageBox.Show("Enter the All Data");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong");
            }
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            depnametb.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            depdesctb.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            depdurationtb.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            depamounttb.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {

            //    string sql = "select * from departmenttable where depname like '%" + textBox1.Text + "%' or depdesc like '%" + textBox1.Text + "%'  or amount like '%" + textBox1.Text + "%' or depduration like '%" + textBox1.Text + "%' ";
                string sql = "select * from departmenttable where depname like '%" + textBox1.Text + "%' ";



                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                DataTable dt = new DataTable();

                da.Fill(dt);


                dataGridView1.DataSource = dt;

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void depamounttb_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void depamounttb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Valid Value");

            }
        }

        private void depnametb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters");

            }
        }

        private void depdesctb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters");

            }
        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
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

        private void label8_Click(object sender, EventArgs e)
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

        private void label12_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void depdurationtb_TextChanged(object sender, EventArgs e)
        {
            depdurationtb.MaxLength = 2;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
