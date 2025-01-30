using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizinAja__Latihan_
{
    public partial class QuizForm : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        int timer;
        Participant participant;
        public QuizForm(Participant participant)
        {
            this.participant = participant;
            InitializeComponent();
        }

        private void QuizForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();

            label1.Text = participant.ParticipantNickname;

            refresh();
        }

        private void refresh()
        {
            flowLayoutPanel2.Controls.Clear();

            getQuestions();
            addQuestion();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            label2.Text = "Time Elapsed: " + formatTime();
        }

        private string formatTime()
        {
            int hours = timer / 3600;
            int minutes = (timer % 3600) / 60;
            int seconds = timer % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void getQuestions()
        {
            var questions = db.Question.Where(f => f.QuizID == participant.QuizID).ToList();

            int no = 0;

            foreach (var question in questions)
            {
                no++;

                QuestionUC questionUC = new QuestionUC(question.QuizID, no, participant, question.ID);
                flowLayoutPanel2.Controls.Add(questionUC);
            }
        }

        private void addQuestion()
        {
            var questions = db.Question.First(f => f.QuizID == participant.QuizID);

            QuestionAnswerUC questionAnswerUC = new QuestionAnswerUC(questions, participant);
            panel1.Controls.Add(questionAnswerUC);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            participant.TimeTaken = timer;

            db.Participant.AddOrUpdate(participant);
            db.SaveChanges();
        }
    }
}
