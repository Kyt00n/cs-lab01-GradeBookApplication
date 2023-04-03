using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) :base(name) 
        { 
            Type = Enums.GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double grade)
        {
            
            
            
            if (Students.Count < 5) 
            {
                throw new InvalidOperationException();
            }

            
            char letterGrade = getTopInClassPercentile(grade) switch
            {
                < 20 => 'A',
                >= 20 and < 40 => 'B',
                >= 40 and < 60 => 'C',
                >= 60 and <= 80 => 'D',
                _ => 'F'
            };
            return letterGrade;
            
        }
        private double getTopInClassPercentile(double grade) 
        {
            List<double> allGrades = new List<double>();
            foreach (var student in Students)
            {
                allGrades.AddRange(student.Grades);
            }
            allGrades.Sort();

            int counter = 0;
            foreach (double g in allGrades)
            {
                if (g > grade)
                {
                    break;
                }
                counter++;
            }
            double topInClass = 100.0 - (counter / (Students.Count * 1.0) * 100.0);
            return topInClass;
        }
        
    }
}
