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
    public partial class student : Form
    {
        public student()
        {
            InitializeComponent();
            datacon();
        }


        public void getfeesno()
        {
            string feesno;
            string sql = "select stuid from studenttable order by stuid Desc";
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

            sid.Text = feesno.ToString();

        }


        private void student_Load(object sender, EventArgs e)
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


        private void fillDepartment()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select depname from departmenttable", con);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("depname", typeof(string));
            dt.Load(rdr);
            sdep.ValueMember = "depname";
            sdep.DataSource = dt;
            con.Close();
        }

      
        private void populate()
        {
            con.Open();
            string sql = "select * from studenttable";
            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void noduelist()
        {
            con.Open();
            string sql = "select * from studenttable where stufees > " + 0 + " ";
            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        void refresh()
        {
            student stu = new student();
            stu.Show();
            this.Hide();
        }


        void clearall()
        {
            sid.Clear();
            sname.Clear();
            sphone.Clear();
            samount.Clear();

            sid.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sid.Text != "" && sname.Text != "" && sdtp.Text != "" && sphone.Text != "" && sdep.Text != "" && samount.Text != "" && steacher.Text != "")
                {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insert into studenttable value('" + sid.Text + "','" + sname.Text + "','" + sgen.SelectedItem.ToString() + "','" + sdtp.Text + "','" + sphone.Text + "','" + sdep.SelectedValue.ToString() + "','" + samount.Text + "','" + steacher.Text + "')", con);

                // MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("student Successfully Added");
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
                if (sid.Text != "" && sname.Text != "" && sdtp.Text != "" && sphone.Text != "" && sdep.Text != "" && samount.Text != "" && steacher.Text != "")
                {
                con.Open();
                string sql = "delete from studenttable where stuid='" + sid.Text + "'; ";

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

                if (sid.Text != "" && sname.Text != "" && sdtp.Text != "" && sphone.Text != "" && sdep.Text != "" && samount.Text != "" && steacher.Text != "")
                {
                con.Open();



                string sql = "update studenttable set stuname='" + sname.Text + "',stugender='" + sgen.SelectedItem.ToString() + "',studob='" + sdtp.Text + "',stuphone='" + sphone.Text + "',stufees='" + samount.Text + "',teacher='" + steacher.Text + "' where stuid='" + sid.Text + "'";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("student Updated Successfully");
                // dataview();
                refresh();
               // populate();
                clearall();
                con.Close();
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
            sid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            sname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            sgen.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            sdtp.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            sphone.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            sdep.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            samount.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            steacher.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void sref_Click(object sender, EventArgs e)
        {
         //   populate();
        }

        private void ndlist_Click(object sender, EventArgs e)
        {
           // noduelist();
        }

        private void teacher()
        {
            //con.Open();
            string sql = "select * from teachertable where teacherdep='" + sdep.SelectedValue.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                steacher.Text = dr["teachername"].ToString();


            }

         //   con.Close();
        }


        private void sdep_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select * from departmenttable where depname='" + sdep.SelectedValue.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                samount.Text = dr["amount"].ToString();
                //steacher.Text = dr["Teacherdep"].ToString();

                teacher();
            }

            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string sql = "select * from studenttable where stuid like '%" + textBox1.Text + "%' or stuname like '%" + textBox1.Text + "%'  or studob like '%" + textBox1.Text + "%' or studep like '%" + textBox1.Text + "%' ";

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

        private void sid_TextChanged(object sender, EventArgs e)
        {


        }

        private void samount_TextChanged(object sender, EventArgs e)
        {

        }

        private void sid_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters & Numbers");

            }
        }

        private void samount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please Enter Valid Value");

            }
        }

        private void sphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Digit");

            }
        }

        private void sname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters");

            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
            refresh();

        }

        private void sphone_TextChanged(object sender, EventArgs e)
        {
            sphone.MaxLength = 10;
        }

        private void steacher_TextChanged(object sender, EventArgs e)
        {

        }






    }
}
