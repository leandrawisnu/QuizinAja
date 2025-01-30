using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizinAja__Latihan_
{
    public partial class CreateAccount : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User();

            if (textBox1 != null && textBox2 != null)
            {
                user.Username = textBox1.Text;
                user.FullName = textBox2.Text;
                user.DateOfBirth = dateTimePicker1.Value;
                if (textBox3 != null)
                {
                    if (textBox4.Text == textBox3.Text)
                    {
                        user.Password = textBox3.Text;
                        db.User.Add(user);
                        db.SaveChanges();

                        var from = new Main(user.ID);
                        from.Show();
                        this.Hide(); 
                    } else
                    {
                        MessageBox.Show("Password doesn't match", "Warning", MessageBoxButtons.OK);
                    }
                } else
                {
                    MessageBox.Show("Password can't be empty", "Warning", MessageBoxButtons.OK);
                }
            } else
            {
                MessageBox.Show("Username / Fullname can't be empty", "Warning", MessageBoxButtons.OK);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var from = new Form1();
            from.Show();
            this.Hide();
        }
    }
}
