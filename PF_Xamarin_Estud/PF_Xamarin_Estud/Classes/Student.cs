using System;
using System.Collections.Generic;
using System.Text;

namespace PF_Xamarin_Estud
{
    public class Student
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string FullName { get { return string.Format("{0} {1}", Name, LastName); } }
        public List<string> SubjectsKeys { get; private set; }

        public Student(string name, string lastName, string email, List<string> subjectsKeys)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            SubjectsKeys = subjectsKeys;
        }
    }
}
