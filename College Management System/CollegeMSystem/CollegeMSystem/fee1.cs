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
    public partial class fee1 : Form
    {


        public fee1()
        {
            InitializeComponent();
            datacon();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm You Want to Exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        MySqlConnection con;
        MySqlCommand cmd;

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
            MySqlCommand cmd = new MySqlCommand("select stuid from studenttable", con);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("stuid", typeof(int));
            dt.Load(rdr);
            fid.ValueMember = "stuid";
            fid.DataSource = dt;
            con.Close();
        }

        private void populate()
        {
            con.Open();
            string sql = "select feesnum,stuid,stuname,payment_date,Dep_Fees,payment_fees,balance from fees";
            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

            clearall();
        }


        void refresh()
        {
            fee1 f = new fee1();
            f.Show();
            this.Hide();
        }

        private void updatestu()
        {
            /* con.Open();
             string sql = "update studenttable set stufees='" + famount.Text + "' where stuid='" + fid.SelectedValue.ToString() + "'";
             MySqlCommand cmd = new MySqlCommand(sql, con);
             cmd.ExecuteNonQuery();
             con.Close();*/
        }


        public void getfeesno()
        {
            string feesno;
            string sql = "select feesnum from fees order by feesnum Desc";
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

            fno.Text = feesno.ToString();

        }


        private void fee1_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
            updatestu();


            getfeesno();

         
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            fno.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            fid.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            fname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            fdate.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            famount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            fppay.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
          //  fpay.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            fbalance.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }

        private void fid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pay()
        {
            string sql1 = "select * from fees where stuid='" + fid.SelectedValue.ToString() + "'";
            MySqlCommand cmd1 = new MySqlCommand(sql1, con);
            DataTable dt1 = new DataTable();
            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
            da1.Fill(dt1);

            foreach (DataRow dr in dt1.Rows)
            {
                // fname.Text = dr["stuname"].ToString();
                // famount.Text = dr["stufees"].ToString();
                fppay.Text = dr["payment_fees"].ToString();

            }
        }


        private void fid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string sql = "select * from studenttable where stuid='" + fid.SelectedValue.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                fname.Text = dr["stuname"].ToString();
                famount.Text = dr["stufees"].ToString();
                //fppay.Text = dr["prepayment"].ToString();

            }

            pay();

            con.Close();

        }


        void clearall()
        {

           // fid.Items.Clear();
            fname.Clear();
            famount.Clear();
            fdate.Checked = false;
            fpay.Clear();
            fbalance.Clear();

            fid.Focus();
        }

        private void noduelist()
        {
            con.Open();
        // string sql = "select * from fees where prepayment > " + 0 + " ";
      // string sql = "select * from fees where balance > " + 0 + " ";
            string sql = "select feesnum,stuid,stuname,payment_date,Dep_Fees,payment_fees,balance from fees where balance <= " + 0 + " ";
           //string sql = "select * from fees where balance IS NULL";


            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

       

        private void ndlist_Click(object sender, EventArgs e)
        {
           noduelist();
         
        }

        private void sref_Click(object sender, EventArgs e)
        {
            populate();
        }

        public void paysave()
        {
            string total = famount.Text;
            string pay = fpay.Text;
            string balance = fbalance.Text;


            string sql1;
            string sql2;

            sql1 = "insert into fees(Dep_Fees,payment,balance)values(@amount,@payment,@balance)select @@identity;";
            con.Open();

            cmd = new MySqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("@amount", total);
            cmd.Parameters.AddWithValue("@payment",pay);
            cmd.Parameters.AddWithValue("@balance", balance);

            int lastid = int.Parse(cmd.ExecuteScalar().ToString());

           



        }

        

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                int x = 0;
                int c = int.Parse(fppay.Text);
                int d = int.Parse(fpay.Text);

                x = c + d;


                 if (fno.Text != "" && fid.Text != "" && fname.Text != "" && fdate.Text != "" && famount.Text != "" && fpay.Text != "")
                {

                con.Open();

            

                string sql = "insert into fees values('" + fno.Text + "','" + fid.SelectedValue.ToString() + "','" + fname.Text + "','" + fdate.Text + "','" + famount.Text + "','" + x.ToString() + "','" + fpay.Text + "','" + fbalance.Text + "')";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                MessageBox.Show("fees Successfully Added");

                if (fbalance.Text == "0")
                {
                    MessageBox.Show("Your Amount was Completely Payed");
                }
                // dataview();
                refresh();
                populate();
                }
                 else
                 {
                     MessageBox.Show("Enter the All Data");
                 }

            }
            catch (Exception ex)
            {
               ;
            }

            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           /* fno.Clear();
            fname.Clear();
            famount.Clear();*/

            fid.Focus();
            getfeesno();

            /*
            MySqlDataAdapter sda = new MySqlDataAdapter("Select isnull (max(cast(feesnum as int)))+1 from fees", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            fno.Text = dt.Rows[0][0].ToString();

            this.ActiveControl = fno;*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm home = new MainForm();
            home.Show();
            this.Hide();
        }

        private void fno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Digit");

            }
        }

        private void famount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Numbers");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                 if (fno.Text != "" && fid.Text != "" && fname.Text != "" && fdate.Text != "" && famount.Text != "" && fpay.Text != "")
                {
                con.Open();
                string sql = "update fees set payment_fees='" + fppay.Text + "', where feesno='" + fno.Text + "'";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("fees Updated Successfully");
                //dataview();
                refresh();
                populate();
              
                clearall();

                }
                 else
                 {
                     MessageBox.Show("Enter the All Data");
                 }


            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Something Went Wrong");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (fno.Text != "" && fid.Text != "" && fname.Text != "" && fdate.Text != "" && famount.Text != "" && fpay.Text != "")
                {
                con.Open();
                string sql = "delete from fees where feesnum='" + fno.Text + "' ";

                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully deleted", "confirmation");
                // dataview();
                refresh();
                populate();
                clearall();

                   }
                 else
                 {
                     MessageBox.Show("Enter the All Data");
                 }


            }
            catch (Exception ex)
            {
               // MessageBox.Show("Something Went Wrong");
            }

            con.Close();
        }

        private void ndlist_Click_1(object sender, EventArgs e)
        {
            noduelist();
        }

        private void sref_Click_1(object sender, EventArgs e)
        {
            populate();
            refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "select * from fees where stuid like '%" + textBox1.Text + "%' or stuname like '%" + textBox1.Text + "%'  or payment_date like '%" + textBox1.Text + "%' ";

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

        private void ovalShape1_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            user u = new user();
            u.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           // fbalance.Text = famount.Text - fpay.Text;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("print();", new Font("Batang", 25, FontStyle.Bold), Brushes.Black, new Point(10, 10));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fno_TextChanged(object sender, EventArgs e)
        {

        }


        public void print()
        {

            txtresult.Clear();
            txtresult.Text += "*************************************\n\n";
            txtresult.Text += "********** Fees Receipt System **********\n\n";
            txtresult.Text += "*************************************\n\n";

            txtresult.Text += "Date     : " + DateTime.Now + "\n\n\n";


            txtresult.Text += "Receipt No                  :  " + fno.Text + "\n\n";
            txtresult.Text += "Student ID                   :  " + fid.Text + "\n\n";
            txtresult.Text += "Student Name              :  " + fname.Text + "\n\n";
            txtresult.Text += "Payment Date              :  " + fdate.Text + "\n\n";
            txtresult.Text += "Dep_Fees                     :  " + famount.Text + "\n\n";
            txtresult.Text += "Payment_fees               :  " + fppay.Text + "\n\n";
            txtresult.Text += "balance                        :  " + fbalance.Text + "\n\n";

            txtresult.Text += "\n\n                                                ................  ";
            txtresult.Text += "\n                                                  Signature   ";

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            print();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtresult.Clear();
          /* fno.Text = "";
            fid.Text = "";
            fname.Text = "";
            fdate.Text = "";
            famount.Text = "";
            fpay.Text = "";
            fbalance.Text = "";*/
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            preview f = new preview();
            f.Show();

            f.print.Text = txtresult.Text;

            //printPreviewDialog1.Document = printDocument1;
          //  printPreviewDialog1.ShowDialog();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void fpay_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters & Numbers");

            }
        }

        private void fbalance_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only Allowed Letters & Numbers");

            }
        }

        private void txtresult_TextChanged(object sender, EventArgs e)
        {

        }

        private void fpay_TextChanged(object sender, EventArgs e)
        {
            /*  if (fpay.Text == "")
            {
                fpay.Text = "0";
            }

            int total = int.Parse(famount.Text);
            int pay = int.Parse(fpay.Text);
            int balance = total - pay;

            fbalance.Text = balance.ToString(); */

          if (fpay.Text == "")
            {
                fpay.Text = "0";
            }

          if (fppay.Text == "")
          {
              fppay.Text = "0";
          }
          if (famount.Text == "")
          {
              famount.Text = "0";
          }

           int total = int.Parse(famount.Text);
           int prepay = int.Parse(fppay.Text);
           int currentpay = int.Parse(fpay.Text);

           int balance = total- (prepay+currentpay);

           fbalance.Text = balance.ToString();


          

          






        }

        private void fppay_TextChanged(object sender, EventArgs e)
        {

        }

        private void fname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void noduelist1()
        {/*
            con.Open();
           string sql = "select feesnum,stuid,stuname,payment_date,Dep_Fees,payment_fees,balance from fees where balance > " + 0 + " ";
              


            MySqlDataAdapter sda = new MySqlDataAdapter(sql, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();*/
        }
        private void button8_Click(object sender, EventArgs e)
        {
          //  noduelist();
        }
      }

    }
