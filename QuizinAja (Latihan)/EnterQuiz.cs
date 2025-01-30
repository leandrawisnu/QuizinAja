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
    public partial class EnterQuiz : Form
    {
        QuizinAjaEntities db = new QuizinAjaEntities();
        public EnterQuiz()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int QuizID = enterCode();
            if (!(QuizID == 0))
            {
                if (textBox2.Text.Length > 0 )
                {
                    var baru = new Participant()
                    {
                        QuizID = QuizID,
                        ParticipantNickname = textBox2.Text,
                        ParticipationDate = DateTime.Now,
                        TimeTaken = 0
                    };

                    db.Participant.Add(baru);
                    db.SaveChanges();

                    var user = db.Participant.OrderByDescending(f => f.ID).First();

                    var QuizForm = new QuizForm(user);
                    QuizForm.Show();
                    this.Hide();

                } else
                {
                    MessageBox.Show("Nickname cannot be empty", "Warnimg", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                MessageBox.Show("Quiz Code not Found", "Warnimg", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int enterCode()
        {
            var data = db.Quiz.First(f => f.Code == textBox1.Text);

            if (data != null)
            {
                return data.ID;
            }
            return 0;
        }
    }
}
