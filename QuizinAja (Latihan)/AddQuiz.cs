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
    public partial class AddQuiz : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        int id;
        int quizId;
        List<Question> questions = new List<Question>();
        public AddQuiz(int x)
        {
            this.id = x;
            InitializeComponent();
        }

        private void AddQuiz_Load(object sender, EventArgs e)
        {
            int quizId = db.Quiz.OrderByDescending(f => f.ID).First().ID + 1;
            this.quizId = quizId;
            refresh();
        }

        private void refresh()
        {
            questionBindingSource.DataSource = questions.ToList();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsUpper(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ValidateQuiz())
            {
                Quiz quiz = new Quiz()
                {
                    UserID = id,
                    Name = textBox1.Text,
                    Code = textBox2.Text,
                    Description = textBox3.Text,
                    CreatedAt = DateTime.Now,
                };

                db.Quiz.Add(quiz);
                db.SaveChanges();

                foreach (Question question in questions)
                {
                    question.QuizID = quiz.ID;
                    db.Question.Add(question);
                    db.SaveChanges();
                }
            }

            var Main = new Main(id);
            this.Hide();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Question question)
            {
                if (e.ColumnIndex == iDDataGridViewTextBoxColumn.Index)
                {
                    e.Value = e.RowIndex + 1;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Question question)
            {
                if (e.ColumnIndex == Delete.Index)
                {
                    if (MessageBox.Show($"Do you want to delete Question no {e.RowIndex + 1} from the Quiz?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        questions.Remove(question);
                        refresh();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                if (textBox5.Text.Length > 0 && textBox6.Text.Length > 0 && textBox7.Text.Length > 0 && textBox8.Text.Length > 0)
                {
                    Question question = new Question()
                    {
                        Question1 = textBox4.Text,
                        OptionA = textBox5.Text,
                        OptionB = textBox6.Text,
                        OptionC = textBox7.Text,
                        OptionD = textBox8.Text,
                        CorrectAnswer = CorrectAnswer()
                    };
                    questions.Add(question);

                    refresh();
                } else
                {
                    MessageBox.Show("All answers must be given", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                MessageBox.Show("Question cannot be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string CorrectAnswer()
        {
            if (radioButton1.Checked)
            {
                return textBox5.Text;
            } else if (radioButton2.Checked)
            {
                return textBox6.Text;
            } else if (radioButton3.Checked)
            {
                return textBox7.Text;
            } else {
                return textBox8.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Main = new Main(id);
            Main.Show();
            this.Hide();
        }

        private bool ValidateQuiz()
        {
            var code = db.Quiz.Where(f => f.Code == textBox2.Text).ToList();
            if (!code.Any())
            {
                if (textBox1.Text.Length > 0)
                {
                    if (questions.Count >= 1)
                    {
                        return true;
                    } else
                    {
                        MessageBox.Show("Quiz should have a minimum of 1 Question", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Quiz Name cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                } 
            } else
            {
                MessageBox.Show("Quiz Code is used Already", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return false;
        }
    }
}
