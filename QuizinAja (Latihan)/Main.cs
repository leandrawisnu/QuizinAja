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
    public partial class Main : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        int x;
        public Main(int x)
        {
            this.x = x;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var data = db.User.First(f => f.ID == x);
            label2.Text = data.FullName;
            refresh();
        }

        private void refresh()
        {
            dataGridView1.SuspendLayout();
            quizBindingSource.DataSource = db.Quiz.Where(f => f.UserID == x).ToList();
            dataGridView1.ResumeLayout();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Quiz quiz)
                {
                    if (participantDataGridViewTextBoxColumn.Index == e.ColumnIndex)
                    {
                        if (quiz != null)
                        {
                            var questions = db.Question.Where(f => f.QuizID == quiz.ID).ToList();

                            if (questions.Count > 0)
                            {
                                e.Value = questions.Count.ToString();
                            }
                            else
                            {
                                e.Value = 0.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Quiz quiz)
            {
                if (MessageBox.Show("Apakah anda ingin menghapus quiz ini dari daftar?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                { 
                    foreach ( var item in db.Question.Where(f => f.QuizID == quiz.ID).ToList())
                    {
                        db.Question.Remove(item);
                    }
                    db.SaveChanges();
                    db.Quiz.Remove(quiz);
                    db.SaveChanges();
                    refresh();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            x = 0;
            var from = new Form1();
            from.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var AddQuiz = new AddQuiz(x);
            AddQuiz.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var report = new QuizReport(x);
            report.Show();
            this.Hide();
        }
    }
}
