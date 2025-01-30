using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace QuizinAja__Latihan_
{
    public partial class QuizReport : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        List<QuizReportDTO> participants = new List<QuizReportDTO>();
        List<QuizReportDTO> second = new List<QuizReportDTO>();
        List<double> averageCorrectList = new List<double>();
        int x;
        public QuizReport(int x)
        {
            this.x = x;
            InitializeComponent();
        }

        private void QuizReport_Load(object sender, EventArgs e)
        {
            refresh();
            comboBox1.SelectedIndex = 0;

            averageTime();
            averageCorrect();
            totalParticipant();
        }

        private void refresh()
        {
            comboBox1.DataSource = db.Quiz.Where(f => f.UserID == x).Select(f => new
            {
                Code = f.ID,
                Name = f.Name,
            }).ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Code";

            int QuizID = int.Parse(comboBox1.SelectedValue.ToString());

            var joined = (from answer in db.Participant
                          join person in db.ParticipantAnswer on answer.ID equals person.ParticipantID
                          select new
                          {
                              answer,
                              person
                          }).ToList();

            foreach (var item in joined.Where(f => f.answer.QuizID == QuizID).ToList()) {

                var baru = new QuizReportDTO()
                {
                    QuizID = item.answer.QuizID,
                    ParticipantName = item.answer.ParticipantNickname,
                    TimeTaken = item.answer.TimeTaken,
                    CorrectPercentage = calculatePercentage(item.person.ParticipantID, item.answer.QuizID)
                };

                participants.Add(baru);
            }

            var grouped = participants.GroupBy(f => f.ParticipantName).ToList();


            foreach (var item in grouped)
            {
                second.Add(new QuizReportDTO
                {
                    QuizID = item.First().QuizID,
                    ParticipantName = item.First().ParticipantName,
                    TimeTaken = item.First().TimeTaken,
                    CorrectPercentage = item.First().CorrectPercentage
                });
            }

            bindingSource1.DataSource = second;
        }

        private int whichQuiz()
        {
            int QuizID = int.Parse(comboBox1.SelectedValue.ToString());

            int data = db.Quiz.First(f => f.ID == QuizID).ID;
            return data;
        }

        private void totalParticipant()
        {
            label5.Text += $"{second.Count} Participant(s)";
        }

        private void averageTime()
        {
            int id = whichQuiz();

            int average = (participants.Select(f => f.TimeTaken).Sum()) / participants.Count();
            label3.Text += formatTime(average);
        }

        private void averageCorrect()
        {
            var average = second.Select(f => f.CorrectPercentage).ToArray();

            List<int> sum = new List<int>();

            foreach( var item in average)
            {
                sum.Add(int.Parse(item.TrimEnd('%')));
            }

            label4.Text += (sum.Sum() / (double)second.Count()).ToString() + "%";
        }

        private string calculatePercentage(int x, int y)
        {
            var thisQuiz = db.Question.Where(f => f.QuizID == y).ToList();

            var thisPerson = db.ParticipantAnswer.Where(f => f.ParticipantID == x).ToList();

            var joined = (from answer in thisPerson
                          join correct in thisQuiz on answer.QuestionID equals correct.ID
                          select new
                          {
                              Answer = answer.Answer,
                              Correct = correct.CorrectAnswer
                          }).ToList();

            var C = joined.Where(f => f.Answer == f.Correct).Count();

            double percentage = ((double)C / (double)thisQuiz.Count) * 100;

            return percentage.ToString() + "%";
        }

        private string formatTime(int x)
        {
            int hours = x / 3600;
            int minutes = (x % 3600) / 60;
            int seconds = x % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is QuizReportDTO quizReportDTO)
            {
                if (e.ColumnIndex == correctPercentageDataGridViewTextBoxColumn.Index)
                {
                    e.Value = quizReportDTO.CorrectPercentage + "%";
                }
            }
        }
    }
}
