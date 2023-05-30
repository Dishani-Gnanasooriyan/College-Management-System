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
    public partial class teacher : Form
    {
        public teacher()
        {
            InitializeComponent();
            datacon();
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

        public void getfeesno()
        {
            string feesno;
            string sql = "select teacherid from teachertable order by teacherid Desc";
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

            tid.Text = feesno.ToString();

        }

        private void teacher_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
            getfeesno();
        }

        private void label7_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Confirm You Want to Exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

       

        private void fillDepartment()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select depname from departmenttable", con);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("depname", typeof(string));
            dt.Load(rdr);
            tdep.ValueMember = "depname";
            tdep.DataSource = dt;
             con.Close();
        }

       private void populate()
        {
            con.Open();
            string sql = "select * from teachertable";
            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

       void refresh()
       {
           teacher teach = new teacher();
           teach.Show();
           this.Hide();
       }

        void clearall()
        {
            tid.Clear();
            tname.Clear();
            tphone.Clear();
            tadd.Clear();

            tid.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                 if (tid.Text != "" && tname.Text != "" && tgender.Text != "" && tdtp.Text != "" && tphone.Text != "" && tdep.Text != ""&& tadd.Text != "")
                {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insert into teachertable value('" + tid.Text + "','" + tname.Text + "','" + tgender.SelectedItem.ToString() + "','" + tdtp.Text + "','" + tphone.Text + "','" + tdep.SelectedValue.ToString() + "','" + tadd.Text + "')", con);

                // MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("teacher Successfully Added");
                //  dataview();
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
              if (tid.Text != "" && tname.Text != "" && tgender.Text != "" && tdtp.Text != "" && tphone.Text != "" && tdep.Text != ""&& tadd.Text != "")
                {
                con.Open();
                string sql = "delete from teachertable where teacherid='" + tid.Text + "'; ";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully deleted", "confirmation");
                // dataview();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
               if (tid.Text != "" && tname.Text != "" && tgender.Text != "" && tdtp.Text != "" && tphone.Text != "" && tdep.Text != ""&& tadd.Text != "")
                {

              con.Open();
            string sql = "update teachertable set teachername='" + tname.Text + "',teachergen='" + tgender.SelectedItem.ToString() + "', teacherdob='" + tdtp.Text + "' ,teacherphone='" + tphone.Text + "' ,teacheraddress='" + tadd.Text + "' where teacherid='" + tid.Text + "'";


                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("teacher Updated Successfully");
               // dataview();
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
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            tname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tgender.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tdtp.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            tphone.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            tdep.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            tadd.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "select * from teachertable where teacherid like '%" + textBox1.Text + "%' or teachername like '%" + textBox1.Text + "%'  or teacherdep like '%" + textBox1.Text + "%' or teacherdob like '%" + textBox1.Text + "%' ";

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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters & Numbers");

            }
        }

        private void tphone_TextChanged(object sender, EventArgs e)
        {
            tphone.MaxLength = 10;
         
        }

        private void tphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Digit");

            }
        }

        private void tname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters");

            }
        }

        private void tadd_KeyPress(object sender, KeyPressEventArgs e)
        {

          /*  if(!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters & Numbers");

            }*/
        }

        private void label17_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
            refresh();
        }

        private void tdtp_ValueChanged(object sender, EventArgs e)
        {

        }








    }
}
