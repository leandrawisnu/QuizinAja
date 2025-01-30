using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizinAja__Latihan_
{
    internal class QuizReportDTO
    {
        public int QuizID {  get; set; }
        public string ParticipantName { get; set; }
        public int TimeTaken { get; set; }
        public string CorrectPercentage { get; set; }
    }
}
