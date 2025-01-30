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
    public partial class QuestionUC : UserControl
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        int id, no, questionid;
        Participant participant;
        public QuestionUC(int id, int no, Participant participant, int questionid)
        {
            this.questionid = questionid;
            this.id = id;
            this.no = no;
            this.participant = participant;
            InitializeComponent();
        }

        private void QuestionUC_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void QuestionUC_Load(object sender, EventArgs e)
        {
            label1.Text = no.ToString();

            refresh();
        }

        private void refresh()
        {
            var questions = db.Question.Where(f => f.QuizID == id);

            var joined = (from answer in db.ParticipantAnswer
                          join question in questions
                          on answer.QuestionID equals question.ID
                          select new
                          {
                              answer,
                              question
                          }).ToList();

            var mine = joined.Where(f => f.question.ID == questionid).Where(f => f.answer.ParticipantID == participant.ID).ToList();

            if (mine.Any())
            {
                this.BackColor = Color.Green;
            }
            else
            {
                this.BackColor = Color.Gray;
            }
        }
    }
}
