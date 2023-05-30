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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            datacon();

           // upw.PasswordChar = '*';
           upw.MaxLength = 6;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (uname.Text == "" && upw.Text == "")
            {
                MessageBox.Show("Invalid Input");
            }

            else
            {

                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    MainForm home = new MainForm();
                    con.Open();
                    MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from usertable where username='" + uname.Text + "' and password='" + upw.Text + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        home.Show();
                        this.Hide();


                    }
                    else
                    {
                        MessageBox.Show("Wrong UserName or Password");
                    }
                    con.Close();

                }

                else
                {
                    MessageBox.Show(uname.Text, "Please Enter Your UserName", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                upw.UseSystemPasswordChar = false;
            }
            else
            {
                upw.UseSystemPasswordChar = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            uname.Clear();
            upw.Clear();
        }

        private void uname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(uname.Text))
            {
                e.Cancel = true;
                uname.Focus();
                ErrorProvider.Equals(uname, "Please enter your UserName");
            }

            else
            {
                e.Cancel = false;
                ErrorProvider.Equals(uname, null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           

        }
    }
}
