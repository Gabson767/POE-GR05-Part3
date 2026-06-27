using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTask
{
    public class Quiz
  
        {
            public string Question { get; set; }
            public string Answer { get; set; }

            public Quiz(string question, string answer)
            {
                Question = question;
                Answer = answer;
            }

        }
    }


