using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Games_Question
{
    class Person
    {
        private int birthYear;
        private int endYear;

        public Person(int _birthYear, int _endYear)
        {
            birthYear = _birthYear;
            endYear = _endYear;
        }

        public int BirthYear
        {
            get { return birthYear; }
        }
        public int EndYear
        {
            get { return endYear; }
        }
    }
}
