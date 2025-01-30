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
    public partial class Form1 : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            } else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var data = db.User.First(f => f.Username == textBox1.Text);

            if (data != null)
            {
                if (textBox2.Text == data.Password)
                {
                    var from = new Main(data.ID);
                    from.Show();
                    this.Hide();
                } else
                {
                    MessageBox.Show("Password Incorrect", "Warning", MessageBoxButtons.OK);
                }
            } else
            {
                MessageBox.Show("User not found", "Warning", MessageBoxButtons.OK);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var from = new CreateAccount();
            from.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var QuizForm = new EnterQuiz();
            QuizForm.Show();
            this.Hide();
        }
    }
}
