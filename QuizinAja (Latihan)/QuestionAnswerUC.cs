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
    public partial class QuestionAnswerUC : UserControl
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        Question question;
        Participant participant;
        int id = 1;
        public QuestionAnswerUC(Question question, Participant participant)
        {
            this.participant = participant;
            this.question = question;
            InitializeComponent();
        }

        private void QuestionAnswerUC_Load(object sender, EventArgs e)
        {
            id = question.ID;

            refresh();
        }

        private void refresh()
        {
            var questions = db.Question.Where(f => f.QuizID == participant.QuizID).ToList();

            var questiona = questions.FirstOrDefault(f => f.ID == id);

            int first = questions.First().ID;

            if (questiona != null || id <= 0)
            {
                setQuestion(questiona);
                searchAnswer(questiona);
            } else
            {
                id = first;
                refresh();
            }
        }

        private void setQuestion(Question question)
        {
            label1.Text = question.Question1;

            radioButton1.Text = question.OptionA;
            radioButton2.Text = question.OptionB;
            radioButton3.Text = question.OptionC;
            radioButton4.Text = question.OptionD;
        }

        private void searchAnswer(Question question)
        {
            var questions = db.Question.Where(f => f.QuizID == question.QuizID);

            var joined = (from answer in db.ParticipantAnswer
                          join questiona in questions
                          on answer.QuestionID equals questiona.ID
                          select new
                          {
                              answer,
                              questiona,
                          }).ToList();

            var mine = joined.Where(f => f.answer.ParticipantID == participant.ID).FirstOrDefault(f => f.questiona.ID == id);

            if (mine != null)
            {
                if (radioButton1.Text == mine.answer.Answer)
                {
                    radioButton1.Checked = true;
                }
                else if (radioButton2.Text == mine.answer.Answer)
                {
                    radioButton2.Checked = true;
                }
                else if (radioButton3.Text == mine.answer.Answer)
                {
                    radioButton3.Checked = true;
                }
                else if (radioButton4.Text == mine.answer.Answer)
                {
                    radioButton4.Checked = true;
                } else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
            }
        }
 
        private void button1_Click(object sender, EventArgs e)
        {
            id = id + 1;

            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            id = id - 1;

            refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var question = db.Question.First(f => f.ID == id);

            var answer = db.ParticipantAnswer.Where(f => f.ParticipantID == participant.ID).Where(f => f.QuestionID == id).FirstOrDefault();

            if (answer != default)
            {
                answer.Answer = radioButton1.Text;
                db.ParticipantAnswer.AddOrUpdate(answer);
                db.SaveChanges();
            } else
            {
                ParticipantAnswer answera = new ParticipantAnswer()
                {
                    ParticipantID = participant.ID,
                    QuestionID = id,
                    Answer = radioButton1.Text
                };

                db.ParticipantAnswer.AddOrUpdate(answera);
                db.SaveChanges();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var question = db.Question.First(f => f.ID == id);

            var answer = db.ParticipantAnswer.Where(f => f.ParticipantID == participant.ID).Where(f => f.QuestionID == id).FirstOrDefault();

            if (answer != default)
            {
                answer.Answer = radioButton2.Text;
                db.ParticipantAnswer.AddOrUpdate(answer);
                db.SaveChanges();
            }
            else
            {
                ParticipantAnswer answera = new ParticipantAnswer()
                {
                    ParticipantID = participant.ID,
                    QuestionID = id,
                    Answer = radioButton2.Text
                };

                db.ParticipantAnswer.AddOrUpdate(answera);
                db.SaveChanges();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            var question = db.Question.First(f => f.ID == id);

            var answer = db.ParticipantAnswer.Where(f => f.ParticipantID == participant.ID).Where(f => f.QuestionID == id).FirstOrDefault();

            if (answer != default)
            {
                answer.Answer = radioButton3.Text;
                db.ParticipantAnswer.AddOrUpdate(answer);
                db.SaveChanges();
            }
            else
            {
                ParticipantAnswer answera = new ParticipantAnswer()
                {
                    ParticipantID = participant.ID,
                    QuestionID = id,
                    Answer = radioButton3.Text
                };

                db.ParticipantAnswer.AddOrUpdate(answera);
                db.SaveChanges();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            var question = db.Question.First(f => f.ID == id);

            var answer = db.ParticipantAnswer.Where(f => f.ParticipantID == participant.ID).Where(f => f.QuestionID == id).FirstOrDefault();

            if (answer != default)
            {
                answer.Answer = radioButton4.Text;
                db.ParticipantAnswer.AddOrUpdate(answer);
                db.SaveChanges();
            }
            else
            {
                ParticipantAnswer answera = new ParticipantAnswer()
                {
                    ParticipantID = participant.ID,
                    QuestionID = id,
                    Answer = radioButton4.Text
                };

                db.ParticipantAnswer.AddOrUpdate(answera);
                db.SaveChanges();
            }
        }
    }
}
