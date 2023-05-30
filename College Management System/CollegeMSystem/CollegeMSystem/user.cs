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
    public partial class user : Form
    {
        public user()
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
            string sql = "select  * from usertable";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        public void getfeesno()
        {
            string feesno;
            string sql = "select userid from usertable order by userid Desc";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                feesno = id.ToString("0000");

            }
            else if (Convert.IsDBNull(dr))
            {
                feesno = ("0001");
            }
            else
            {
                feesno = ("0001");
            }

            con.Close();

            UidTb.Text = feesno.ToString();

        }



        private void user_Load(object sender, EventArgs e)
        {
            getfeesno();
        }

         void clearall()
        {
            UidTb.Clear();
            UnameTb.Clear();
            UpassTb.Clear();
          
            UidTb.Focus();
        }

         void refresh()
         {
             user stu = new user();
             stu.Show();
             this.Hide();
         }
        private void button1_Click(object sender, EventArgs e)
        {
             try
            {if (UidTb.Text != "" && UnameTb.Text != "" && UpassTb.Text != "")
                {

                con.Open();
                string sql = "insert into usertable values('" + UidTb.Text + "','" + UnameTb.Text + "','" + UpassTb.Text + "')";

                    MySqlCommand cmd = new MySqlCommand(sql,con);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added");
                    dataview();
                    refresh();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             UidTb.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            UnameTb.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            UpassTb.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
              try
            
              {if (UidTb.Text != "" && UnameTb.Text != "" && UpassTb.Text != "")
                {

                con.Open();
                string sql = "delete from usertable where userid='" + UidTb.Text + "' ";

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
                if (UidTb.Text != "" && UnameTb.Text != "" && UpassTb.Text != "")
                {

                con.Open();
                string sql = "update usertable set username='" + UnameTb.Text + "',password='" + UpassTb.Text + "' where userid='" + UidTb.Text + "'";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("User Updated Successfully");
                dataview();
                refresh();
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
             UidTb.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            UnameTb.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            UpassTb.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
             MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm You Want to Exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "select * from usertable where userid like '%" + textBox1.Text + "%' or username like '%" + textBox1.Text + "%'  or password like '%" + textBox1.Text + "%' ";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);

                DataTable dt = new DataTable();

                da.Fill(dt);


                dataGridView1.DataSource = dt;

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            con.Close();
        }

        private void UidTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Digit");

            }
        }

        private void UnameTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters");

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void ovalShape1_Click(object sender, EventArgs e)
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

        private void label10_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        }
    }

