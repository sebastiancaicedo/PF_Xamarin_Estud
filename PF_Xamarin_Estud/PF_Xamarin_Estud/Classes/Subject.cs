using System;
using System.Collections.Generic;
using System.Text;

namespace PF_Xamarin_Estud
{
    public class Subject
    {
        public string Name { get; private set; }
        public string ProfessorId { get; private set; }
        public string ProfessorFullName { get; private set; }
        public List<string> StudentsKeys { get; private set; } = new List<string>();
        public List<string> EvaluationsKeys { get; private set; } = new List<string>();

        public Subject(string name, string professorFullName, string professorId)
        {
            Name = name;
            ProfessorId = professorId;
            ProfessorFullName = professorFullName;
        }
    }
}
